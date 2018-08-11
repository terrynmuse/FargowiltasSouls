using System;
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
        private int numSplits = 1;
        private bool instantSplit;
        private int numSpeedups = 3;
        private bool ninjaTele;
        public bool IsRecolor = false;
        private bool stormBoosted = false;
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

        public override bool PreAI(Projectile projectile)
        {
            bool retVal = true;
            FargoPlayer modPlayer = Main.LocalPlayer.GetModPlayer<FargoPlayer>();
            counter++;

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

            if (projectile.owner == Main.myPlayer)
            {
                if (Soulcheck.GetValue("Splitting Projectiles") && modPlayer.AdamantiteEnchant && !projectile.minion && projectile.damage > 0 && Main.rand.Next(4) == 0 && projectile.aiStyle != 19 && !instantSplit && 
                    !Rotate)
                {
                    instantSplit = true;
                    SplitProj(projectile, 3);
                    retVal = false;
                }

                if (projectile.bobber && !instantSplit)
                {
                    instantSplit = true;

                    if (modPlayer.FishSoul1)
                    {
                        SplitProj(projectile, 5);
                    }
                    if (modPlayer.FishSoul2)
                    {
                        SplitProj(projectile, 11);
                    }
                }

                if (modPlayer.GladEnchant && (projectile.thrown || modPlayer.WillForce) && numSpeedups > 0 && counter % 10 == 0)
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

                instantSplit = true;
            }

            if(modPlayer.ForbiddenEnchant && projectile.type != ProjectileID.SandnadoFriendly)
            {
                Projectile nearestProj = null;
                float distance = 5 * 16;

                Main.projectile.Where(x => x.type == ProjectileID.SandnadoFriendly && x.active).ToList().ForEach(x =>
                {
                    if (nearestProj == null && Vector2.Distance(x.Center, projectile.Center) <= distance)
                    {
                        nearestProj = x;
                        distance = Vector2.Distance(x.Center, projectile.Center);
                    }
                    else if (nearestProj != null && Vector2.Distance(nearestProj.Center, projectile.Center) <= distance)
                    {
                        nearestProj = x;
                        distance = Vector2.Distance(nearestProj.Center, projectile.Center);
                    }
                });

                if (nearestProj != null && !stormBoosted)
                {
                    if(projectile.penetrate != -1)
                    {
                        projectile.penetrate *= 2;
                    }

                    projectile.damage = (int)(projectile.damage * 1.5);

                    stormBoosted = true;
                }
            }

            if (modPlayer.SpookyEnchant && Soulcheck.GetValue("Spooky Scythes") && projectile.minion && projectile.minionSlots > 0 && counter % 60 == 0 && Main.rand.Next(8 + Main.player[projectile.owner].maxMinions) == 0)
            {
                Main.PlaySound(2/**/, (int)projectile.position.X, (int)projectile.position.Y, 62);
                XWay(8, projectile.Center, mod.ProjectileType("SpookyScythe"), 5, (int)(projectile.damage / 2), 2f);
                counter = 0;
            }

            if(modPlayer.RedEnchant && (projectile.ranged || modPlayer.WillForce) && projectile.penetrate != -1 && firstTick)
            {
                projectile.penetrate += 5;
            }

            if(projectile.hostile && Main.npc[projectile.owner].active && Main.npc[projectile.owner].GetGlobalNPC<FargoGlobalNPC>().SqueakyToy)
            {
                projectile.damage = 1;
                squeakyToy = true;
            }

            if(Rotate)
            {
                if(firstTick)
                {
                    projectile.timeLeft = 600;
                }

                projectile.tileCollide = false;
                projectile.usesLocalNPCImmunity = true;

                Player p = Main.player[projectile.owner];

                //Factors for calculations
                double deg = (double)projectile.ai[1];
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

                if (modPlayer.OriEnchant && projectile.type != ProjectileID.Blizzard)
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

                if(modPlayer.FrostEnchant)
                {
                    //projectile.rotation = (Main.MouseWorld - projectile.Center).ToRotation() + 90;
                    projectile.timeLeft++;
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
                    split.GetGlobalProjectile<FargoGlobalProjectile>().instantSplit = projectile.GetGlobalProjectile<FargoGlobalProjectile>().instantSplit;
                }

            }
        }

        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            #region pets

            if (projectile.type == ProjectileID.BabyHornet && player.FindBuffIndex(BuffID.BabyHornet) == -1)
            {
                if (!modPlayer.BeeEnchant || !Soulcheck.GetValue("Baby Hornet Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == mod.ProjectileType("Chlorofuck"))
            {
                if (!modPlayer.ChloroEnchant || !Soulcheck.GetValue("Leaf Crystal"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Sapling && player.FindBuffIndex(BuffID.PetSapling) == -1)
            {
                if (!modPlayer.ChloroEnchant || !Soulcheck.GetValue("Seedling Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BabyFaceMonster && player.FindBuffIndex(BuffID.BabyFaceMonster) == -1)
            {
                if (!modPlayer.CrimsonEnchant || !Soulcheck.GetValue("Baby Face Monster Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.CrimsonHeart && player.FindBuffIndex(BuffID.CrimsonHeart) == -1)
            {
                if (!modPlayer.CrimsonEnchant || !Soulcheck.GetValue("Crimson Heart Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.MagicLantern && player.FindBuffIndex(BuffID.MagicLantern) == -1)
            {
                if (!modPlayer.MinerEnchant || !Soulcheck.GetValue("Magic Lantern Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.MiniMinotaur && player.FindBuffIndex(BuffID.MiniMinotaur) == -1)
            {
                if (!modPlayer.GladEnchant || !Soulcheck.GetValue("Mini Minotaur Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BlackCat && player.FindBuffIndex(BuffID.BlackCat) == -1)
            {
                if (!modPlayer.NinjaEnchant || !Soulcheck.GetValue("Black Cat Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Wisp && player.FindBuffIndex(BuffID.Wisp) == -1)
            {
                if (!modPlayer.SpectreEnchant || !Soulcheck.GetValue("Wisp Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if(projectile.type == ProjectileID.CursedSapling && (player.FindBuffIndex(BuffID.CursedSapling) == -1))
			{
				if (!modPlayer.SpookyEnchant || !Soulcheck.GetValue("Cursed Sapling Pet"))
                {
					projectile.Kill();
					return;
				}
			}

            if (projectile.type == ProjectileID.EyeSpring && (player.FindBuffIndex(BuffID.EyeballSpring) == -1))
            {
                if (!modPlayer.SpookyEnchant || !Soulcheck.GetValue("Eye Spring Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Turtle && player.FindBuffIndex(BuffID.PetTurtle) == -1)
            {
                if (!modPlayer.TurtleEnchant || !Soulcheck.GetValue("Turtle Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.PetLizard && player.FindBuffIndex(BuffID.PetLizard) == -1)
            {
                if (!modPlayer.TurtleEnchant || !Soulcheck.GetValue("Lizard Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Truffle && player.FindBuffIndex(BuffID.BabyTruffle) == -1)
            {
                if (!modPlayer.ShroomEnchant || !Soulcheck.GetValue("Truffle Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Spider && player.FindBuffIndex(BuffID.PetSpider) == -1)
            {
                if (!modPlayer.SpiderEnchant || !Soulcheck.GetValue("Spider Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Squashling && player.FindBuffIndex(BuffID.Squashling) == -1)
            {
                if (!modPlayer.PumpkinEnchant || !Soulcheck.GetValue("Squashling Pet"))
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

            if (projectile.type == ProjectileID.BlueFairy)
            {
                if (!modPlayer.HallowEnchant || !Soulcheck.GetValue("Fairy Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.StardustGuardian && player.FindBuffIndex(BuffID.StardustGuardianMinion) == -1)
            {
                if (!modPlayer.StardustEnchant || !Soulcheck.GetValue("Stardust Guardian")) 
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.TikiSpirit && player.FindBuffIndex(BuffID.TikiSpirit) == -1)
            {
                if (!modPlayer.TikiEnchant || !Soulcheck.GetValue("Tiki Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Penguin && player.FindBuffIndex(BuffID.BabyPenguin) == -1)
            {
                if (!modPlayer.FrostEnchant || !Soulcheck.GetValue("Baby Penguin Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BabySnowman && player.FindBuffIndex(BuffID.BabySnowman) == -1)
            {
                if (!modPlayer.FrostEnchant || !Soulcheck.GetValue("Baby Snowman Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.DD2PetGato && (player.FindBuffIndex(BuffID.PetDD2Gato) == -1))
            {
                if (!modPlayer.ShinobiEnchant || !Soulcheck.GetValue("Gato Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Parrot && (player.FindBuffIndex(BuffID.PetParrot) == -1))
            {
                if (!modPlayer.GoldEnchant || !Soulcheck.GetValue("Parrot Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.Puppy && player.FindBuffIndex(BuffID.Puppy) == -1)
            {
                if (!modPlayer.RedEnchant || !Soulcheck.GetValue("Puppy Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.CompanionCube && player.FindBuffIndex(BuffID.CompanionCube) == -1)
            {
                if (!modPlayer.VortexEnchant || !Soulcheck.GetValue("Companion Cube Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.DD2PetDragon && player.FindBuffIndex(BuffID.PetDD2Dragon) == -1)
            {
                if (!modPlayer.ValhallaEnchant || !Soulcheck.GetValue("Dragon Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BabySkeletronHead && player.FindBuffIndex(BuffID.BabySkeletronHead) == -1)
            {
                if (!modPlayer.NecroEnchant || !Soulcheck.GetValue("Baby Skeletron  Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BabyDino && player.FindBuffIndex(BuffID.BabyDinosaur) == -1)
            {
                if (!modPlayer.FossilEnchant || !Soulcheck.GetValue("Baby Dino Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.BabyEater && player.FindBuffIndex(BuffID.BabyEater) == -1)
            {
                if (!modPlayer.ShadowEnchant || !Soulcheck.GetValue("Baby Eater Pet"))
                {
                    projectile.Kill();
                    return;
                }
            }

            if (projectile.type == ProjectileID.ShadowOrb && player.FindBuffIndex(BuffID.ShadowOrb) == -1)
            {
                if (!modPlayer.ShadowEnchant || !Soulcheck.GetValue("Shadow Orb Pet"))
                {
                    projectile.Kill();
                }
            }

            if (projectile.type == ProjectileID.SuspiciousTentacle && player.FindBuffIndex(BuffID.SuspiciousTentacle) == -1)
            {
                if (!modPlayer.CosmoForce || !Soulcheck.GetValue("Suspicious Looking Eye Pet"))
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
            }

            */


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
            if (modPlayer.ShroomEnchant && modPlayer.IsStandingStill && Main.rand.Next(5) == 0)
            {
                Vector2 pos = new Vector2(projectile.Center.X + Main.rand.Next(-30, 30), projectile.Center.Y + Main.rand.Next(-40, 40));

                Projectile spore = Projectile.NewProjectileDirect(pos, Vector2.Zero, ProjectileID.SporeCloud, (int)(projectile.damage / 3), 0f, projectile.owner);
                spore.ranged = true;
                spore.melee = false;
                spore.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
            }

            if (Soulcheck.GetValue("Orichalcum Fireball") && modPlayer.OriEnchant && !Rotate && Main.rand.Next(6) == 0)
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

            //Main.NewText(projectile.type.ToString(), 206, 12, 15);

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

        public override void Kill(Projectile projectile, int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.CobaltEnchant && projectile.type != ProjectileID.CrystalShard && projectile.friendly && projectile.damage > 0  && !Rotate && Main.rand.Next(4) == 0)
            {
                int damage = 50;

                if(modPlayer.EarthForce)
                {
                    damage = 100;
                }

                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 27);
                XWay(8, projectile.Center, ProjectileID.CrystalShard, 5, damage, 2f);
            }

            if(modPlayer.FrostEnchant && Rotate && projectile.type == ProjectileID.Blizzard)
            {
                modPlayer.IcicleCount--;
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
    }
}