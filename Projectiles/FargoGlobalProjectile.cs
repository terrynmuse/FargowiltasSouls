using System;
using System.Collections.Generic;
using System.Linq;
using FargowiltasSouls.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class FargoGlobalProjectile : GlobalProjectile
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        private int counter;
        public bool CanSplit = true;
        private int numSplits = 1;
        private static int adamantiteCD = 0;

        private int numSpeedups = 3;
        private bool ninjaTele;
        public bool IsRecolor = false;
        private bool stormBoosted = false;
        private int stormTimer;

        public bool Rotate = false;
        public int RotateDist = 64;
        public int RotateDir = 1;
        private int oriDir = 1;

        private bool firstTick = true;
        private bool squeakyToy = false;
        public bool TimeFrozen = false;

        public override void SetDefaults(Projectile projectile)
        {
            if (FargoWorld.MasochistMode)
            {
                if (projectile.type == ProjectileID.SaucerLaser)
                {
                    projectile.tileCollide = false;
                }

                if (projectile.type == ProjectileID.FallingStar)
                {
                    projectile.hostile = true;
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
                if (Soulcheck.GetValue("Adamantite Splitting") && (modPlayer.AdamantiteEnchant || modPlayer.TerrariaSoul) && firstTick && projectile.damage > 0 && !projectile.minion && projectile.aiStyle != 19 && Array.IndexOf(noSplit, projectile.type) <= -1 && CanSplit &&
                    !Rotate)
                {
                    if (adamantiteCD != 0)
                    {
                        adamantiteCD--;
                    }

                    if(adamantiteCD == 0)
                    {
                        firstTick = false;
                        adamantiteCD = modPlayer.TerrariaSoul ? 4 : 8;
                        SplitProj(projectile, 3);
                    }
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

            if (projectile.owner == Main.myPlayer && projectile.friendly && !projectile.hostile)
            {
                if(projectile.damage > 0 && projectile.minionSlots == 0 && projectile.aiStyle != 19 && Array.IndexOf(noSplit, projectile.type) <= -1 && CanSplit)
                {

                    if (modPlayer.GladEnchant && Soulcheck.GetValue("Gladiator Speedup") && (projectile.thrown || modPlayer.WillForce) && numSpeedups > 0 && counter % 10 == 0)
                    {
                        numSpeedups--;
                        projectile.velocity = Vector2.Multiply(projectile.velocity, 1.5f);
                    }

                    if (modPlayer.ThrowSoul && projectile.thrown && numSplits > 0 && counter == 20 * (1 + projectile.extraUpdates))
                    {
                        numSplits--;
                        SplitProj(projectile, 3);
                        retVal = false;
                    }
                }
                
                if (projectile.bobber && firstTick)
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
            }

            if (modPlayer.SpookyEnchant && !modPlayer.TerrariaSoul && Soulcheck.GetValue("Spooky Scythes") && projectile.minion && projectile.minionSlots > 0 && counter % 60 == 0 && Main.rand.Next(8 + Main.player[projectile.owner].maxMinions) == 0)
            {
                Main.PlaySound(2/**/, (int)projectile.position.X, (int)projectile.position.Y, 62);
                Projectile[] projs = XWay(8, projectile.Center, mod.ProjectileType("SpookyScythe"), 5, (int)(projectile.damage / 2), 2f);
                counter = 0;

                for(int i = 0; i <projs.Length; i++)
                {
                    projs[i].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
            }

            if(projectile.hostile && Main.npc[projectile.owner].active && Main.npc[projectile.owner].GetGlobalNPC<FargoGlobalNPC>().SqueakyToy)
            {
                projectile.damage = 1;
                squeakyToy = true;
            }

            if(Rotate)
            {
                if(firstTick && !modPlayer.TerrariaSoul)
                {
                    projectile.timeLeft = 600;
                }

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

                if (projectile.type == ProjectileID.Blizzard && modPlayer.FrostEnchant && Soulcheck.GetValue("Frost Icicles"))
                {
                    //projectile.rotation = (Main.MouseWorld - projectile.Center).ToRotation() + 90;
                    projectile.timeLeft = 2;
                }

                if ((modPlayer.TerrariaSoul && Soulcheck.GetValue("Orichalcum Fireballs")))
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

            firstTick = false;

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

        private void KillPet(Projectile projectile, Player player, int proj, int buff, bool enchant, string toggle)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (projectile.type == proj && player.FindBuffIndex(buff) == -1)
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

            #region pets

            KillPet(projectile, player, ProjectileID.BabyHornet, BuffID.BabyHornet, modPlayer.BeeEnchant, "Baby Hornet Pet");
            KillPet(projectile, player, ProjectileID.Sapling, BuffID.PetSapling, modPlayer.ChloroEnchant, "Seedling Pet");
            KillPet(projectile, player, ProjectileID.BabyFaceMonster, BuffID.BabyFaceMonster, modPlayer.CrimsonEnchant, "Baby Face Monster Pet");
            KillPet(projectile, player, ProjectileID.CrimsonHeart, BuffID.CrimsonHeart, modPlayer.CrimsonEnchant, "Crimson Heart Pet");
            KillPet(projectile, player, ProjectileID.MagicLantern, BuffID.MagicLantern, modPlayer.MinerEnchant, "Magic Lantern Pet");
            KillPet(projectile, player, ProjectileID.MiniMinotaur, BuffID.MiniMinotaur, modPlayer.GladEnchant, "Mini Minotaur Pet");
            KillPet(projectile, player, ProjectileID.BlackCat, BuffID.BlackCat, modPlayer.NinjaEnchant, "Black Cat Pet");
            KillPet(projectile, player, ProjectileID.Wisp, BuffID.Wisp, modPlayer.SpectreEnchant, "Wisp Pet");
            KillPet(projectile, player, ProjectileID.CursedSapling, BuffID.CursedSapling, modPlayer.SpookyEnchant, "Cursed Sapling Pet");
            KillPet(projectile, player, ProjectileID.EyeSpring, BuffID.EyeballSpring, modPlayer.SpookyEnchant, "Eye Spring Pet");
            KillPet(projectile, player, ProjectileID.Turtle, BuffID.PetTurtle, modPlayer.TurtleEnchant, "Turtle Pet");
            KillPet(projectile, player, ProjectileID.PetLizard, BuffID.PetLizard, modPlayer.TurtleEnchant, "Lizard Pet");
            KillPet(projectile, player, ProjectileID.Truffle, BuffID.BabyTruffle, modPlayer.ShroomEnchant, "Truffle Pet");
            KillPet(projectile, player, ProjectileID.Spider, BuffID.PetSpider, modPlayer.SpiderEnchant, "Spider Pet");
            KillPet(projectile, player, ProjectileID.Squashling, BuffID.Squashling, modPlayer.PumpkinEnchant, "Squashling Pet");
            KillPet(projectile, player, ProjectileID.BlueFairy, BuffID.FairyBlue, modPlayer.HallowEnchant, "Fairy Pet");
            KillPet(projectile, player, ProjectileID.StardustGuardian, BuffID.StardustGuardianMinion, modPlayer.StardustEnchant, "Stardust Guardian");
            KillPet(projectile, player, ProjectileID.TikiSpirit, BuffID.TikiSpirit, modPlayer.TikiEnchant, "Tiki Pet");
            KillPet(projectile, player, ProjectileID.Penguin, BuffID.BabyPenguin, modPlayer.FrostEnchant, "Baby Penguin Pet");
            KillPet(projectile, player, ProjectileID.BabySnowman, BuffID.BabySnowman, modPlayer.FrostEnchant, "Baby Snowman Pet");
            KillPet(projectile, player, ProjectileID.DD2PetGato, BuffID.PetDD2Gato, modPlayer.ShinobiEnchant, "Gato Pet");
            KillPet(projectile, player, ProjectileID.Parrot, BuffID.PetParrot, modPlayer.GoldEnchant, "Parrot Pet");
            KillPet(projectile, player, ProjectileID.Puppy, BuffID.Puppy, modPlayer.RedEnchant, "Puppy Pet");
            KillPet(projectile, player, ProjectileID.CompanionCube, BuffID.CompanionCube, modPlayer.VortexEnchant, "Companion Cube Pet");
            KillPet(projectile, player, ProjectileID.DD2PetDragon, BuffID.PetDD2Dragon, modPlayer.ValhallaEnchant, "Dragon Pet");
            KillPet(projectile, player, ProjectileID.BabySkeletronHead, BuffID.BabySkeletronHead, modPlayer.NecroEnchant, "Baby Skeletron  Pet");
            KillPet(projectile, player, ProjectileID.BabyDino, BuffID.BabyDinosaur, modPlayer.FossilEnchant, "Baby Dino Pet");
            KillPet(projectile, player, ProjectileID.BabyEater, BuffID.BabyEater, modPlayer.ShadowEnchant, "Baby Eater Pet");
            KillPet(projectile, player, ProjectileID.ShadowOrb, BuffID.ShadowOrb, modPlayer.ShadowEnchant, "Shadow Orb Pet");
            KillPet(projectile, player, ProjectileID.SuspiciousTentacle, BuffID.SuspiciousTentacle, modPlayer.CosmoForce, "Suspicious Looking Eye Pet");
            KillPet(projectile, player, ProjectileID.DD2PetGhost, BuffID.PetDD2Ghost, modPlayer.DarkEnchant, "Flickerwick Pet");

            if (projectile.type == mod.ProjectileType("Chlorofuck"))
            {
                if (!(modPlayer.ChloroEnchant || modPlayer.TerrariaSoul) || !Soulcheck.GetValue("Chlorophyte Leaf Crystal"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == mod.ProjectileType("SilverSword"))
            {
                if (!modPlayer.SilverEnchant || !Soulcheck.GetValue("Silver Sword Familiar"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == mod.ProjectileType("HallowSword"))
            {
                if (!modPlayer.HallowEnchant || !Soulcheck.GetValue("Enchanted Sword Familiar"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == mod.ProjectileType("HallowShield"))
            {
                if (!modPlayer.HallowEnchant || !Soulcheck.GetValue("Hallowed Shield Familiar"))
                {
                    projectile.Kill();
                    return;
                }
            }


            /*
            if (projectile.type == ProjectileID.ZephyrFish && player.FindBuffIndex(127) == -1)
            {
                if (!modPlayer.FishPet)
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BabyGrinch && player.FindBuffIndex(92) == -1)
            {
                if (!modPlayer.GrinchPet)
                {
                    projectile.Kill();
                    return;
                }
            }*/

            #endregion

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
                if (projectile.type == ProjectileID.JavelinHostile)
                {
                    target.AddBuff(mod.BuffType("Defenseless"), 600);
                    target.AddBuff(mod.BuffType("Stunned"), 90);
                }

                if (projectile.type == ProjectileID.DemonSickle)
                {
                    target.AddBuff(BuffID.Darkness, 1800);
                }

                if (projectile.type == ProjectileID.HarpyFeather && Main.rand.Next(2) == 0)
                {
                    target.AddBuff(mod.BuffType("ClippedWings"), 480);
                }

                //so only antlion sand and not falling sand 
                if (projectile.type == ProjectileID.SandBallFalling && projectile.velocity.X != 0)
                {
                    target.AddBuff(mod.BuffType("Stunned"), 120);
                }

                if (projectile.type == ProjectileID.Stinger && NPC.AnyNPCs(NPCID.QueenBee))
                {
                    target.AddBuff(BuffID.Venom, 900);
                    target.AddBuff(BuffID.BrokenArmor, 1200);
                }

                if (projectile.type == ProjectileID.Skull && Main.rand.Next(10) == 0)
                {
                    target.AddBuff(BuffID.Cursed, 360);
                }

                if (projectile.type == ProjectileID.EyeLaser && NPC.AnyNPCs(NPCID.WallofFlesh))
                {
                    target.AddBuff(BuffID.OnFire, 600);
                }

                if (projectile.type == ProjectileID.DeathSickle && !player.HasBuff(mod.BuffType("MarkedforDeath")))
                {
                    target.AddBuff(mod.BuffType("MarkedforDeath"), 900);
                    target.AddBuff(mod.BuffType("LivingWasteland"), 1800);
                }

                if (projectile.type == ProjectileID.DrManFlyFlask)
                {
                    int[] buffs = { BuffID.Venom, BuffID.Confused, BuffID.CursedInferno, BuffID.OgreSpit, mod.BuffType("LivingWasteland"), mod.BuffType("Defenseless"), mod.BuffType("Purified") };

                    target.AddBuff(buffs[Main.rand.Next(buffs.Length)], 600);
                    target.AddBuff(BuffID.Stinky, 1200);
                }

                //CULTIST OP
                if (projectile.type == ProjectileID.CultistBossLightningOrb)
                {
                    target.AddBuff(mod.BuffType("LightningRod"), 600);
                }
                if (projectile.type == ProjectileID.CultistBossLightningOrbArc)
                {
                    target.AddBuff(BuffID.Electrified, 300);
                }
                if (projectile.type == ProjectileID.CultistBossIceMist)
                {
                    target.AddBuff(BuffID.Frozen, 300);
                }
                if (projectile.type == ProjectileID.CultistBossFireBall)
                {
                    target.AddBuff(mod.BuffType("Berserked"), 300);
                    target.AddBuff(BuffID.BrokenArmor, 900);
                    target.AddBuff(BuffID.OnFire, 600);
                }
                if (projectile.type == ProjectileID.CultistBossFireBallClone)
                {
                    target.AddBuff(BuffID.ShadowFlame, 600);
                }

                if (projectile.type == ProjectileID.PaladinsHammerHostile)
                {
                    target.AddBuff(mod.BuffType("Lethargic"), 600);
                }

                if (projectile.type == ProjectileID.RuneBlast)
                {
                    target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 300);
                }

                if (projectile.type == ProjectileID.ThornBall || projectile.type == ProjectileID.PoisonSeedPlantera)
                {
                    target.AddBuff(mod.BuffType("Infested"), 600);
                }

                if (projectile.type == ProjectileID.DesertDjinnCurse && target.ZoneCorrupt)
                {
                    target.AddBuff(BuffID.ShadowFlame, 900);
                }

                if (projectile.type == ProjectileID.DesertDjinnCurse && target.ZoneCrimson)
                {
                    target.AddBuff(BuffID.Ichor, 1800);
                }

                if (projectile.type == ProjectileID.PhantasmalDeathray)
                {
                    target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 300);
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
            float[] _x = { 0, speed, 0, -speed, speed, -speed, speed, -speed, speed / 2, speed, -speed, speed / 2, speed, -speed / 2, -speed, -speed / 2 };
            float[] _y = { speed, 0, -speed, 0, speed, -speed, -speed, speed, speed, speed / 2, speed / 2, -speed, -speed / 2, speed, -speed / 2, -speed };

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
            {
                if (Main.projectile[i].type == type)
                {
                    count++;
                }
            }

            return count;
        }
    }
}