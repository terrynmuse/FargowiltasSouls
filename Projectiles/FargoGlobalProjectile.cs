using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class FargoGlobalProjectile : GlobalProjectile
    {
        private static int adamantiteCD = 0;
        public bool CanSplit = true;

        private int counter;

        private bool firstTick = true;
        public bool IsRecolor = false;
        private bool ninjaTele;

        private int numSpeedups = 3;
        private int numSplits = 1;
        private int oriDir = 1;

        public bool Rotate = false;
        public int RotateDir = 1;
        public int RotateDist = 64;
        private bool squeakyToy = false;
        private bool stormBoosted = false;
        private int stormTimer;
        public bool TimeFrozen = false;
        public override bool InstancePerEntity => true;

        public override void SetDefaults(Projectile projectile)
        {
            if (FargoWorld.MasochistMode)
            {
                switch (projectile.type)
                {
                    case ProjectileID.SaucerLaser:
                        projectile.tileCollide = false;
                        break;

                    case ProjectileID.FallingStar:
                        projectile.hostile = true;
                        break;

                    default:
                        break;
                }
            }
        }

        private static int[] noSplit = { ProjectileID.CrystalShard, ProjectileID.SandnadoFriendly, ProjectileID.LastPrism, ProjectileID.LastPrismLaser, ProjectileID.FlowerPetal, ProjectileID.BabySpider, ProjectileID.CrystalLeafShot };

        public override bool PreAI(Projectile projectile)
        {
            bool retVal = true;
            FargoPlayer modPlayer = Main.LocalPlayer.GetModPlayer<FargoPlayer>();
            counter++;

            if (projectile.owner == Main.myPlayer && projectile.friendly && !projectile.hostile)
            {
                if (firstTick)
                {
                    if ((modPlayer.AdamantiteEnchant || modPlayer.TerrariaSoul) && CanSplit && !Rotate && projectile.damage > 0 && !projectile.minion && projectile.aiStyle != 19 && Soulcheck.GetValue("Adamantite Splitting") && Array.IndexOf(noSplit, projectile.type) <= -1)
                    {
                        if (adamantiteCD != 0)
                        {
                            adamantiteCD--;
                        }

                        if (adamantiteCD == 0)
                        {
                            firstTick = false;
                            adamantiteCD = modPlayer.TerrariaSoul ? 4 : 8;
                            SplitProj(projectile, 3);
                        }
                    }

                    if (projectile.bobber)
                    {
                        firstTick = false;

                        if (modPlayer.FishSoul1)
                        {
                            SplitProj(projectile, 5);
                        }
                        if (modPlayer.FishSoul2)
                        {
                            SplitProj(projectile, 11);
                        }
                    }

                    if (Rotate && !modPlayer.TerrariaSoul)
                    {
                        projectile.timeLeft = 600;
                    }

                    firstTick = false;
                }

                if (modPlayer.ForbiddenEnchant && projectile.damage > 0 && projectile.type != ProjectileID.SandnadoFriendly && !stormBoosted)
                {
                    Projectile nearestProj = null;
                    float distance = 5 * 16;

                    List<Projectile> projs = Main.projectile.Where(x => x.type == ProjectileID.SandnadoFriendly && x.active).ToList();

                    foreach (Projectile p in projs)
                    {
                        if (Vector2.Distance(p.Center, projectile.Center) <= distance)
                        {
                            nearestProj = p;
                            break;
                        }
                    }

                    if (nearestProj != null)
                    {
                        if (projectile.maxPenetrate != -1)
                        {
                            projectile.maxPenetrate *= 2;
                        }

                        projectile.damage = (int)(projectile.damage * 1.5);

                        stormBoosted = true;
                        stormTimer = 120;
                    }
                }

                if (stormTimer > 0)
                {
                    stormTimer--;

                    if (stormTimer == 0)
                    {
                        if (projectile.maxPenetrate != -1)
                        {
                            projectile.maxPenetrate /= 2;
                        }

                        projectile.damage = projectile.damage / 3;
                        stormBoosted = false;
                    }
                }

                if (modPlayer.GladEnchant && (projectile.thrown || modPlayer.WillForce) && CanSplit && projectile.damage > 0 && projectile.minionSlots == 0 && projectile.aiStyle != 19 && Array.IndexOf(noSplit, projectile.type) <= -1 &&
                    Soulcheck.GetValue("Gladiator Speedup") && numSpeedups > 0 && counter % 10 == 0)
                {
                    numSpeedups--;
                    projectile.velocity = Vector2.Multiply(projectile.velocity, 1.5f);
                }

                if (modPlayer.Jammed && projectile.ranged && projectile.type != ProjectileID.ConfettiGun)
                {
                    Projectile.NewProjectile(projectile.Center, projectile.velocity, ProjectileID.ConfettiGun, 0, 0f);
                    projectile.damage = 0;
                    projectile.position = new Vector2(Main.maxTilesX);
                    projectile.Kill();
                }

                if (modPlayer.Atrophied && projectile.thrown)
                {
                    projectile.damage = 0;
                    projectile.position = new Vector2(Main.maxTilesX);
                    projectile.Kill();
                }
            }

            if (modPlayer.SpookyEnchant && !modPlayer.TerrariaSoul && Soulcheck.GetValue("Spooky Scythes") && projectile.minion && projectile.minionSlots > 0 && counter % 60 == 0 && Main.rand.Next(8 + Main.player[projectile.owner].maxMinions) == 0)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
                Projectile[] projs = XWay(8, projectile.Center, mod.ProjectileType("SpookyScythe"), 5, (int)(projectile.damage / 2), 2f);
                counter = 0;

                for(int i = 0; i <projs.Length; i++)
                {
                    projs[i].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
            }

            //projectile.owner usually means Main.myPlayer, doesn't apply to npc arrayyyyyy
            /*if(projectile.hostile && Main.npc[projectile.owner].active && Main.npc[projectile.owner].GetGlobalNPC<FargoGlobalNPC>().SqueakyToy)
            {
                projectile.damage = 1;
                squeakyToy = true;
            }*/

            if(Rotate)
            {
                projectile.tileCollide = false;
                projectile.usesLocalNPCImmunity = true;

                Player p = Main.player[projectile.owner];

                //Factors for calculations
                double deg = projectile.ai[1];
                double rad = deg * (Math.PI / 180);

                projectile.position.X = p.Center.X - (int)(Math.Cos(rad) * RotateDist) - projectile.width / 2;
                projectile.position.Y = p.Center.Y - (int)(Math.Sin(rad) * RotateDist) - projectile.height / 2;

                //Increase the counter/angle in degrees by 1 point, you can change the rate here too, but the orbit may look choppy depending on the value
                if(RotateDir == 1)
                {
                    projectile.ai[1] += 2.5f;
                }
                else
                {
                    projectile.ai[1] -= 2.5f;
                }

                //projectile.rotation = projectile.ai[1] * 0.0174f;

                if (modPlayer.OriEnchant && projectile.type != ProjectileID.Blizzard && projectile.type != ProjectileID.Bone)
                {
                    projectile.penetrate = -1;

                    if (oriDir == 1)
                    {
                        RotateDist++;
                    }
                    else
                    {
                        RotateDist--;
                    }

                    if (RotateDist >= 256)
                    {
                        oriDir = 0;
                    }
                    if (RotateDist <= 64)
                    {
                        oriDir = 1;
                    }
                }

                if (modPlayer.FrostEnchant && projectile.type == ProjectileID.Blizzard && Soulcheck.GetValue("Frost Icicles"))
                {
                    //projectile.rotation = (Main.MouseWorld - projectile.Center).ToRotation() + 90;
                    projectile.timeLeft = 2;
                }

                if (modPlayer.TerrariaSoul && Soulcheck.GetValue("Orichalcum Fireballs"))
                {
                    projectile.timeLeft = 2;
                }

                retVal = false;
            }

            if (TimeFrozen)
            {
                projectile.position = projectile.oldPosition;
                projectile.frameCounter--;
                projectile.timeLeft++;
                retVal = false;
            }

            return retVal;
        }

        public static void SplitProj(Projectile projectile, int number)
        {
            //if its odd, we just keep the original 
            if (number % 2 != 0)
            {
                number--;
            }

            Projectile split;

            double spread = 0.6 / number;

            for (int i = 0; i < number / 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    int factor;

                    if (j == 0)
                    {
                        factor = 1;
                    }
                    else
                    {
                        factor = -1;
                    }

                    split = Projectile.NewProjectileDirect(projectile.Center, projectile.velocity.RotatedBy(factor * spread * (i + 1)), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
                    split.GetGlobalProjectile<FargoGlobalProjectile>().numSplits = projectile.GetGlobalProjectile<FargoGlobalProjectile>().numSplits;
                    split.GetGlobalProjectile<FargoGlobalProjectile>().firstTick = projectile.GetGlobalProjectile<FargoGlobalProjectile>().firstTick;
                }

            }
        }

        private void KillPet(Projectile projectile, Player player, int buff, bool enchant, string toggle)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (player.FindBuffIndex(buff) == -1)
            {
                if (!(enchant || modPlayer.TerrariaSoul) || !Soulcheck.GetValue(toggle))
                {
                    projectile.Kill();
                }
            }
        }

        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            switch (projectile.type)
            {
                #region pets

                case ProjectileID.BabyHornet:
                    KillPet(projectile, player, BuffID.BabyHornet, modPlayer.BeeEnchant, "Baby Hornet Pet");
                    break;

                case ProjectileID.Sapling:
                    KillPet(projectile, player, BuffID.PetSapling, modPlayer.ChloroEnchant, "Seedling Pet");
                    break;

                case ProjectileID.BabyFaceMonster:
                    KillPet(projectile, player, BuffID.BabyFaceMonster, modPlayer.CrimsonEnchant, "Baby Face Monster Pet");
                    break;

                case ProjectileID.CrimsonHeart:
                    KillPet(projectile, player, BuffID.CrimsonHeart, modPlayer.CrimsonEnchant, "Crimson Heart Pet");
                    break;

                case ProjectileID.MagicLantern:
                    KillPet(projectile, player, BuffID.MagicLantern, modPlayer.MinerEnchant, "Magic Lantern Pet");
                    break;

                case ProjectileID.MiniMinotaur:
                    KillPet(projectile, player, BuffID.MiniMinotaur, modPlayer.GladEnchant, "Mini Minotaur Pet");
                    break;

                case ProjectileID.BlackCat:
                    KillPet(projectile, player, BuffID.BlackCat, modPlayer.NinjaEnchant, "Black Cat Pet");
                    break;

                case ProjectileID.Wisp:
                    KillPet(projectile, player, BuffID.Wisp, modPlayer.SpectreEnchant, "Wisp Pet");
                    break;

                case ProjectileID.CursedSapling:
                    KillPet(projectile, player, BuffID.CursedSapling, modPlayer.SpookyEnchant, "Cursed Sapling Pet");
                    break;

                case ProjectileID.EyeSpring:
                    KillPet(projectile, player, BuffID.EyeballSpring, modPlayer.SpookyEnchant, "Eye Spring Pet");
                    break;

                case ProjectileID.Turtle:
                    KillPet(projectile, player, BuffID.PetTurtle, modPlayer.TurtleEnchant, "Turtle Pet");
                    break;

                case ProjectileID.PetLizard:
                    KillPet(projectile, player, BuffID.PetLizard, modPlayer.TurtleEnchant, "Lizard Pet");
                    break;

                case ProjectileID.Truffle:
                    KillPet(projectile, player, BuffID.BabyTruffle, modPlayer.ShroomEnchant, "Truffle Pet");
                    break;

                case ProjectileID.Spider:
                    KillPet(projectile, player, BuffID.PetSpider, modPlayer.SpiderEnchant, "Spider Pet");
                    break;

                case ProjectileID.Squashling:
                    KillPet(projectile, player, BuffID.Squashling, modPlayer.PumpkinEnchant, "Squashling Pet");
                    break;

                case ProjectileID.BlueFairy:
                    KillPet(projectile, player, BuffID.FairyBlue, modPlayer.HallowEnchant, "Fairy Pet");
                    break;

                case ProjectileID.StardustGuardian:
                    KillPet(projectile, player, BuffID.StardustGuardianMinion, modPlayer.StardustEnchant, "Stardust Guardian");
                    break;

                case ProjectileID.TikiSpirit:
                    KillPet(projectile, player, BuffID.TikiSpirit, modPlayer.TikiEnchant, "Tiki Pet");
                    break;

                case ProjectileID.Penguin:
                    KillPet(projectile, player, BuffID.BabyPenguin, modPlayer.FrostEnchant, "Baby Penguin Pet");
                    break;

                case ProjectileID.BabySnowman:
                    KillPet(projectile, player, BuffID.BabySnowman, modPlayer.FrostEnchant, "Baby Snowman Pet");
                    break;

                case ProjectileID.DD2PetGato:
                    KillPet(projectile, player, BuffID.PetDD2Gato, modPlayer.ShinobiEnchant, "Gato Pet");
                    break;

                case ProjectileID.Parrot:
                    KillPet(projectile, player, BuffID.PetParrot, modPlayer.GoldEnchant, "Parrot Pet");
                    break;

                case ProjectileID.Puppy:
                    KillPet(projectile, player, BuffID.Puppy, modPlayer.RedEnchant, "Puppy Pet");
                    break;

                case ProjectileID.CompanionCube:
                    KillPet(projectile, player, BuffID.CompanionCube, modPlayer.VortexEnchant, "Companion Cube Pet");
                    break;

                case ProjectileID.DD2PetDragon:
                    KillPet(projectile, player, BuffID.PetDD2Dragon, modPlayer.ValhallaEnchant, "Dragon Pet");
                    break;

                case ProjectileID.BabySkeletronHead:
                    KillPet(projectile, player, BuffID.BabySkeletronHead, modPlayer.NecroEnchant, "Baby Skeletron  Pet");
                    break;

                case ProjectileID.BabyDino:
                    KillPet(projectile, player, BuffID.BabyDinosaur, modPlayer.FossilEnchant, "Baby Dino Pet");
                    break;

                case ProjectileID.BabyEater:
                    KillPet(projectile, player, BuffID.BabyEater, modPlayer.ShadowEnchant, "Baby Eater Pet");
                    break;

                case ProjectileID.ShadowOrb:
                    KillPet(projectile, player, BuffID.ShadowOrb, modPlayer.ShadowEnchant, "Shadow Orb Pet");
                    break;

                case ProjectileID.SuspiciousTentacle:
                    KillPet(projectile, player, BuffID.SuspiciousTentacle, modPlayer.CosmoForce, "Suspicious Looking Eye Pet");
                    break;

                case ProjectileID.DD2PetGhost:
                    KillPet(projectile, player, BuffID.PetDD2Ghost, modPlayer.DarkEnchant, "Flickerwick Pet");
                    break;

                /*case ProjectileID.ZephyrFish:
                    if (player.FindBuffIndex(127) == -1)
                    {
                        if (!modPlayer.FishPet)
                        {
                            projectile.Kill();
                            return;
                        }
                    }
                    break;

                case ProjectileID.BabyGrinch:
                    if (player.FindBuffIndex(92) == -1)
                    {
                        if (!modPlayer.GrinchPet)
                        {
                            projectile.Kill();
                            return;
                        }
                    }
                    break;*/

                #endregion

                case ProjectileID.FlamingWood:
                    projectile.position += projectile.velocity * .5f;
                    break;

                default:
                        break;
            }

            //does this projectile even exist
            /*if (projectile.type == mod.ProjectileType("HallowShield"))
            {
                if (!modPlayer.HallowEnchant || !Soulcheck.GetValue("Hallowed Shield Familiar"))
                {
                    projectile.Kill();
                    return;
                }
            }*/

            if (stormBoosted)
            {
                int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, DustID.GoldFlame, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1.5f);
                Main.dust[dustId].noGravity = true;
            }
        }

        public override Color? GetAlpha(Projectile projectile, Color lightColor)
        {
            if (IsRecolor)
            {
                if (projectile.type == ProjectileID.HarpyFeather)
                {
                    return Color.Brown;
                }

                if (projectile.type == ProjectileID.SpikyBall)
                {
                    return Color.LimeGreen;
                }

                if (projectile.type == ProjectileID.PineNeedleFriendly)
                {
                    return Color.GreenYellow;
                }

                if(projectile.type == ProjectileID.SporeCloud)
                {
                    return Color.Blue;
                }

                if(projectile.type == ProjectileID.Bone || projectile.type == ProjectileID.BoneGloveProj)
                {
                    return Color.SandyBrown;
                }
            }

            return null;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.SqueakyToy)
            {
                return;
            }

            //spawn proj on hit
            if (modPlayer.ShroomEnchant && !modPlayer.TerrariaSoul && CountProj(ProjectileID.SporeCloud) < 5 && modPlayer.IsStandingStill && Main.rand.Next(5) == 0)
            {
                Vector2 pos = new Vector2(projectile.Center.X + Main.rand.Next(-30, 30), projectile.Center.Y + Main.rand.Next(-40, 40));

                Projectile spore = Projectile.NewProjectileDirect(pos, Vector2.Zero, ProjectileID.SporeCloud, (int)(projectile.damage / 3), 0f, projectile.owner);
                spore.ranged = true;
                spore.melee = false;
                spore.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                spore.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
            }

            if (modPlayer.OriEnchant && !modPlayer.TerrariaSoul && !Rotate && Main.rand.Next(6) == 0)
            {
                int[] fireballs = { ProjectileID.BallofFire, ProjectileID.BallofFrost, ProjectileID.CursedFlameFriendly };
                const int MAXBALLS = 10;

                int ballCount = 0;

                Projectile[] balls = new Projectile[MAXBALLS];

                for(int i = 0; i < 1000; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.GetGlobalProjectile<FargoGlobalProjectile>().Rotate)
                    {
                        ballCount++;
                        
                        if(ballCount >= MAXBALLS)
                        {
                            break;
                        }

                        balls[ballCount] = p;
                    }
                }

                ballCount++;

                if(ballCount <= MAXBALLS)
                {
                    int dist = 63;

                    for(int i = 0; i < balls.Length; i++)
                    {
                        if(balls[i] != null)
                        {
                            if(dist == 63)
                            {
                                dist = balls[i].GetGlobalProjectile<FargoGlobalProjectile>().RotateDist;
                            }

                            balls[i].Kill();
                        }
                    }

                    float degree;
                    for (int i = 0; i < ballCount; i++)
                    {
                        degree = (360 / ballCount) * i;
                        Projectile fireball = Projectile.NewProjectileDirect(player.Center, Vector2.Zero, fireballs[Main.rand.Next(3)], (int)(10 * player.magicDamage), 0f, projectile.owner, 0, degree);
                        fireball.GetGlobalProjectile<FargoGlobalProjectile>().Rotate = true;
                        fireball.GetGlobalProjectile<FargoGlobalProjectile>().RotateDist = dist;
                    }
                }
            }

            if (projectile.minion && modPlayer.UniverseEffect)
            {
                target.AddBuff(BuffID.Ichor, 240, true);
                target.AddBuff(BuffID.CursedInferno, 240, true);
                target.AddBuff(BuffID.Confused, 120, true);
                target.AddBuff(BuffID.Venom, 240, true);
                target.AddBuff(BuffID.ShadowFlame, 240, true);
                target.AddBuff(BuffID.OnFire, 240, true);
                target.AddBuff(BuffID.Frostburn, 240, true);
                target.AddBuff(BuffID.Daybreak, 240, true);
            }
        }

        public override bool OnTileCollide(Projectile projectile, Vector2 oldVelocity)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.NinjaEnchant && projectile.type == ProjectileID.SmokeBomb && !ninjaTele)
            {
                ninjaTele = true;

                var teleportPos = new Vector2();

                teleportPos.X = projectile.position.X;
                teleportPos.Y = projectile.position.Y;

                //spiral out to find a save spot
                int count = 0;
                int increase = 10;
                while (Collision.SolidCollision(teleportPos, player.width, player.height))
                {
                    switch (count)
                    {
                        case 0:
                            teleportPos.X -= increase;
                            break;
                        case 1:
                            teleportPos.X += increase * 2;
                            break;
                        case 2:
                            teleportPos.Y += increase;
                            break;
                        default:
                            teleportPos.Y -= increase * 2;
                            increase += 10;
                            break;
                    }
                    count++;

                    if (count >= 4)
                    {
                        count = 0;
                    }

                }

                if (teleportPos.X > 50 && teleportPos.X < (double)(Main.maxTilesX * 16 - 50) && teleportPos.Y > 50 && teleportPos.Y < (double)(Main.maxTilesY * 16 - 50))
                {
                    player.Teleport(teleportPos, 1);
                    NetMessage.SendData(65, -1, -1, null, 0, player.whoAmI, teleportPos.X, teleportPos.Y, 1);
                }
            }

            return true;
        }

        public override void ModifyHitPlayer(Projectile projectile, Player target, ref int damage, ref bool crit)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (FargoWorld.MasochistMode)
            {
                switch(projectile.type)
                {
                    case ProjectileID.JavelinHostile:
                        target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(60, 90));
                        break;

                    case ProjectileID.DemonSickle:
                        target.AddBuff(BuffID.Darkness, Main.rand.Next(900, 1800));
                        break;

                    case ProjectileID.HarpyFeather:
                        if (Main.rand.Next(2) == 0)
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(60, 480));
                        break;

                    //so only antlion sand and not falling sand 
                    case ProjectileID.SandBallFalling:
                        if (projectile.velocity.X != 0)
                            target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(60, 120));
                        break;

                    case ProjectileID.Stinger:
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.beeBoss, NPCID.QueenBee))
                            target.AddBuff(BuffID.Venom, Main.rand.Next(180, 900));
                            target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(120, 1200));
                        break;

                    case ProjectileID.Skull:
                        if (Main.rand.Next(4) == 0)
                            target.AddBuff(BuffID.Cursed, Main.rand.Next(60, 360));
                        break;

                    case ProjectileID.EyeLaser:
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.wallBoss, NPCID.WallofFlesh))
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 600));
                        break;

                    case ProjectileID.DeathSickle:
                        if (!player.HasBuff(mod.BuffType("MarkedforDeath")))
                        {
                            target.AddBuff(mod.BuffType("MarkedforDeath"), 1800);
                            target.AddBuff(mod.BuffType("LivingWasteland"), 1800);
                        }
                        break;

                    case ProjectileID.DrManFlyFlask:
                        switch (Main.rand.Next(7))
                        {
                            case 0:
                                target.AddBuff(BuffID.Venom, Main.rand.Next(60, 600));
                                break;
                            case 1:
                                target.AddBuff(BuffID.Confused, Main.rand.Next(60, 600));
                                break;
                            case 2:
                                target.AddBuff(BuffID.CursedInferno, Main.rand.Next(60, 600));
                                break;
                            case 3:
                                target.AddBuff(BuffID.OgreSpit, Main.rand.Next(60, 600));
                                break;
                            case 4:
                                target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(60, 600));
                                break;
                            case 5:
                                target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(60, 600));
                                break;
                            case 6:
                                target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(60, 600));
                                break;

                            default:
                                break;
                        }

                        target.AddBuff(BuffID.Stinky, Main.rand.Next(900, 1200));
                        break;

                    //CULTIST OP
                    case ProjectileID.CultistBossLightningOrb:
                        target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(300, 900));
                        break;

                    case ProjectileID.CultistBossLightningOrbArc:
                        target.AddBuff(BuffID.Electrified, Main.rand.Next(60, 300));
                        break;

                    case ProjectileID.CultistBossIceMist:
                        if (!target.HasBuff(BuffID.Frozen))
                            target.AddBuff(BuffID.Frozen, Main.rand.Next(30, 120));
                        break;

                    case ProjectileID.CultistBossFireBall:
                        target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(90, 900));
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(120, 600));
                        break;

                    case ProjectileID.CultistBossFireBallClone:
                        target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.PaladinsHammerHostile:
                        target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(480, 720));
                        break;

                    case ProjectileID.RuneBlast:
                        target.AddBuff(mod.BuffType("FlamesoftheUniverse"), Main.rand.Next(300));
                        break;

                    case ProjectileID.ThornBall:
                    case ProjectileID.PoisonSeedPlantera:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.Venom, Main.rand.Next(60, 300));

                        if (target.HasBuff(mod.BuffType("Infested")))
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(180, 360));
                        else
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(90, 180));
                        break;

                    case ProjectileID.DesertDjinnCurse:
                        if (target.ZoneCorrupt)
                            target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 900));
                        else if (target.ZoneCrimson)
                            target.AddBuff(BuffID.Ichor, Main.rand.Next(900, 1800));
                        break;

                    case ProjectileID.PhantasmalDeathray:
                        target.AddBuff(mod.BuffType("FlamesoftheUniverse"), Main.rand.Next(60, 300));
                        break;

                    case ProjectileID.BrainScramblerBolt:
                        target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(15, 60));
                        target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(60, 180));
                        break;

                    case ProjectileID.MartianTurretBolt:
                    case ProjectileID.GigaZapperSpear:
                        target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.SaucerMissile:
                        target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(120, 180));
                        target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(120, 180));
                        break;

                    case ProjectileID.SaucerLaser:
                        target.AddBuff(BuffID.Electrified, Main.rand.Next(240, 480));
                        break;

                    case ProjectileID.UFOLaser:
                    case ProjectileID.SaucerDeathray:
                        target.AddBuff(mod.BuffType("MarkedforDeath"), 600);
                        break;

                    case ProjectileID.FlamingWood:
                    case ProjectileID.GreekFire1:
                    case ProjectileID.GreekFire2:
                    case ProjectileID.GreekFire3:
                        int duration = Main.rand.Next(90, 120);
                        target.AddBuff(BuffID.OnFire, duration);
                        target.AddBuff(BuffID.CursedInferno, duration);
                        target.AddBuff(BuffID.ShadowFlame, duration);
                        break;

                    default:
                        break;
                }
            }

            if(squeakyToy)
            {
                modPlayer.Squeak(target.Center);
            }
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if(modPlayer.NecroEnchant && projectile.type == mod.ProjectileType("DungeonGuardian") && projectile.penetrate == 1)
            {
                crit = true;
            }

            if(modPlayer.FrostEnchant && projectile.type == ProjectileID.Blizzard && player.HeldItem.type != ItemID.BlizzardStaff)
            {
                target.AddBuff(BuffID.Chilled, 300);
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }

        private static int[] noShard = { ProjectileID.CrystalShard };

        public override void Kill(Projectile projectile, int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.CobaltEnchant && Soulcheck.GetValue("Cobalt Shards") && CanSplit && Array.IndexOf(noShard, projectile.type) <= -1 && projectile.friendly && projectile.damage > 0  && !projectile.minion && projectile.aiStyle != 19 && !Rotate && Main.rand.Next(4) == 0)
            {
                int damage = 50;

                if(modPlayer.EarthForce)
                {
                    damage = 100;
                }

                Main.NewText(projectile.Name);

                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 27);
                XWay(8, projectile.Center, ProjectileID.CrystalShard, 5, damage, 2f);
            }

            if(Rotate && projectile.type == ProjectileID.Blizzard)
            {
                modPlayer.IcicleCount--;
            }

            if(modPlayer.TerrariaSoul && Rotate && projectile.type != ProjectileID.Blizzard && projectile.type != ProjectileID.Bone)
            {
                modPlayer.OriSpawn = false;
            }
        }

        public static Projectile[] XWay(int num, Vector2 pos, int type, float speed, int damage, float knockback)
        {
            float[] _x = {0, speed, 0, -speed, speed, -speed, speed, -speed, speed / 2, speed, -speed, speed / 2, speed, -speed / 2, -speed, -speed / 2};
            float[] _y = {speed, 0, -speed, 0, speed, -speed, -speed, speed, speed, speed / 2, speed / 2, -speed, -speed / 2, speed, -speed / 2, -speed};

            Projectile[] projs = new Projectile[16];

            for (int i = 0; i < num; i++)
            {
                Projectile p = Projectile.NewProjectileDirect(pos, new Vector2(_x[i], _y[i]), type, damage, knockback, Main.myPlayer);
                projs[i] = p;
            }

            return projs;
        }

        public static int CountProj(int type)
        {
            int count = 0;

            for (int i = 0; i < 1000; i++)
                if (Main.projectile[i].type == type)
                    count++;

            return count;
        }
    }
}