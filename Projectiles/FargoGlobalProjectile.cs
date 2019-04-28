using System;
using System.Collections.Generic;
using System.Linq;
using FargowiltasSouls.NPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.Buffs.Masomode;
using Microsoft.Xna.Framework.Graphics;
using ThoriumMod;

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

        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
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

        private bool firstTick = true;
        private bool squeakyToy = false;
        public int TimeFrozen = 0;

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

                    case ProjectileID.CultistBossFireBallClone:
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.cultBoss, NPCID.CultistBoss))
                            projectile.timeLeft = 1;
                        break;

                    /*case ProjectileID.Sharknado:
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.fishBoss, NPCID.DukeFishron))
                            projectile.damage = projectile.damage * 3 / 2;
                        break;*/

                    default:
                        break;
                }
            }
        }

        private static int[] noSplit = {
            ProjectileID.CrystalShard,
            ProjectileID.SandnadoFriendly,
            ProjectileID.LastPrism,
            ProjectileID.LastPrismLaser,
            ProjectileID.FlowerPetal,
            ProjectileID.BabySpider,
            ProjectileID.CrystalLeafShot,
            ProjectileID.Phantasm,
            ProjectileID.VortexBeater,
            ProjectileID.ChargedBlasterCannon,
            ProjectileID.MedusaHead,
            ProjectileID.WireKite,
            ProjectileID.DD2PhoenixBow
        };

        public override bool PreAI(Projectile projectile)
        {
            bool retVal = true;
            FargoPlayer modPlayer = Main.LocalPlayer.GetModPlayer<FargoPlayer>();
            counter++;

            if (projectile.owner == Main.myPlayer)
            {
                if (firstTick)
                {
                    firstTick = false;

                    if ((modPlayer.AdamantiteEnchant || modPlayer.TerrariaSoul) && CanSplit && projectile.friendly && !projectile.hostile
                        && !Rotate && projectile.damage > 0 && !projectile.minion && projectile.aiStyle != 19 && projectile.aiStyle != 99
                        && Soulcheck.GetValue("Adamantite Splitting") && Array.IndexOf(noSplit, projectile.type) <= -1)
                    {
                        if (adamantiteCD != 0)
                        {
                            adamantiteCD--;
                        }

                        if (adamantiteCD == 0)
                        {
                            adamantiteCD = modPlayer.TerrariaSoul ? 4 : 8;
                            SplitProj(projectile, 3);
                        }
                    }

                    if (projectile.bobber)
                    {
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

                    /*if (modPlayer.UniverseEffect)
                    {
                        projectile.melee = true;
                        projectile.ranged = true;
                        projectile.magic = true;
                        projectile.minion = true;
                        projectile.thrown = true;

                        /*if (Fargowiltas.Instance.ThoriumLoaded)
                        {


                        }
                    }*/

                    if (modPlayer.BeeEnchant && (projectile.type == ProjectileID.GiantBee || projectile.type == ProjectileID.Bee) && Main.rand.Next(3) == 0)
                    {
                        projectile.usesLocalNPCImmunity = true;
                        projectile.localNPCHitCooldown = 5;
                        projectile.penetrate *= 2;
                        projectile.timeLeft *= 2;
                        projectile.scale *= 3;
                        projectile.damage = (int)(projectile.damage * 1.5);
                    }
                }
                
                if (projectile.friendly && !projectile.hostile)
                {
                    if (modPlayer.ForbiddenEnchant && projectile.damage > 0 && projectile.type != ProjectileID.SandnadoFriendly && !stormBoosted)
                    {
                        Projectile nearestProj = null;

                        List<Projectile> projs = Main.projectile.Where(x => x.type == ProjectileID.SandnadoFriendly && x.active).ToList();

                        foreach (Projectile p in projs)
                        {
                            Vector2 stormDistance = p.Center - projectile.Center;

                            if (Math.Abs(stormDistance.X) < p.width / 2 && Math.Abs(stormDistance.Y) < p.height / 2)
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

                            projectile.damage = (int)(projectile.damage * (2 / 3));
                            stormBoosted = false;
                        }
                    }

                    if (modPlayer.GladEnchant && !modPlayer.TerrariaSoul && (projectile.thrown || modPlayer.WillForce) && CanSplit && projectile.damage > 0 && projectile.minionSlots == 0 && projectile.aiStyle != 19 && Array.IndexOf(noSplit, projectile.type) <= -1 &&
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

                    if (modPlayer.SpookyEnchant && Soulcheck.GetValue("Spooky Scythes") && projectile.minion && projectile.minionSlots > 0 && counter % 60 == 0 && Main.rand.Next(8 + Main.player[projectile.owner].maxMinions) == 0)
                    {
                        Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 62);
                        Projectile[] projs = XWay(8, projectile.Center, mod.ProjectileType("SpookyScythe"), 5, projectile.damage / 2, 2f);
                        counter = 0;

                        for (int i = 0; i < 8; i++)
                        {
                            if (projs[i] == null) continue;
                            projs[i].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                        }
                    }
                }
            }

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

                //increase/decrease degrees
                if(RotateDir == 1)
                {
                    projectile.ai[1] += 2.5f;
                }
                else
                {
                    projectile.ai[1] -= 2.5f;
                }

                if (modPlayer.FrostEnchant && projectile.type == ProjectileID.Blizzard && Soulcheck.GetValue("Frost Icicles"))
                {
                    projectile.rotation = (Main.MouseWorld - projectile.Center).ToRotation() - 5;
                    projectile.timeLeft = 2;
                }

                if (modPlayer.OriEnchant && (projectile.type == ProjectileID.BallofFire || projectile.type == ProjectileID.CursedFlameFriendly || projectile.type == ProjectileID.BallofFrost) && Soulcheck.GetValue("Orichalcum Fireballs"))
                {
                    projectile.timeLeft = 2;
                }

                retVal = false;
            }

            if (TimeFrozen > 0)
            {
                projectile.position = projectile.oldPosition;
                projectile.frameCounter--;
                projectile.timeLeft++;
                TimeFrozen--;
                retVal = false;
            }

            //masomode unicorn meme
            if (FargoWorld.MasochistMode && projectile.type == ProjectileID.RainbowBack && projectile.hostile)
            {
                projectile.tileCollide = false;

                counter++;
                if (counter >= 5)
                {
                    projectile.velocity = Vector2.Zero;
                }
                if (counter >= 60)
                {
                    projectile.Kill();
                }
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
                    int factor = (j == 0) ? 1 : -1;
                    split = NewProjectileDirectSafe(projectile.Center, projectile.velocity.RotatedBy(factor * spread * (i + 1)), projectile.type, projectile.damage, projectile.knockBack, projectile.owner, projectile.ai[0], projectile.ai[1]);
                    if (split != null)
                    {
                        split.GetGlobalProjectile<FargoGlobalProjectile>().numSplits = projectile.GetGlobalProjectile<FargoGlobalProjectile>().numSplits;
                        split.GetGlobalProjectile<FargoGlobalProjectile>().firstTick = projectile.GetGlobalProjectile<FargoGlobalProjectile>().firstTick;
                    }
                }

            }
        }

        private void KillPet(Projectile projectile, Player player, int buff, bool enchant, string toggle, bool minion = false)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (player.FindBuffIndex(buff) == -1)
            {
                if (!(enchant || modPlayer.TerrariaSoul) || !Soulcheck.GetValue(toggle) || (!modPlayer.PetsActive && !minion))
                    projectile.Kill();
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
                    KillPet(projectile, player, BuffID.BabyHornet, modPlayer.BeeEnchant, "Hornet Pet");
                    break;

                case ProjectileID.Sapling:
                    KillPet(projectile, player, BuffID.PetSapling, modPlayer.ChloroEnchant, "Seedling Pet");
                    break;

                case ProjectileID.BabyFaceMonster:
                    KillPet(projectile, player, BuffID.BabyFaceMonster, modPlayer.CrimsonEnchant, "Face Monster Pet");
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
                    KillPet(projectile, player, BuffID.StardustGuardianMinion, modPlayer.StardustEnchant, "Stardust Guardian", true);
                    break;

                case ProjectileID.TikiSpirit:
                    KillPet(projectile, player, BuffID.TikiSpirit, modPlayer.TikiEnchant, "Tiki Pet");
                    break;

                case ProjectileID.Penguin:
                    KillPet(projectile, player, BuffID.BabyPenguin, modPlayer.FrostEnchant || modPlayer.IcyEnchant, "Penguin Pet");
                    break;

                case ProjectileID.BabySnowman:
                    KillPet(projectile, player, BuffID.BabySnowman, modPlayer.FrostEnchant, "Snowman Pet");
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
                    KillPet(projectile, player, BuffID.BabySkeletronHead, modPlayer.NecroEnchant, "Skeletron Pet");
                    break;

                case ProjectileID.BabyDino:
                    KillPet(projectile, player, BuffID.BabyDinosaur, modPlayer.FossilEnchant, "Dino Pet");
                    break;

                case ProjectileID.BabyEater:
                    KillPet(projectile, player, BuffID.BabyEater, modPlayer.ShadowEnchant, "Eater Pet");
                    break;

                case ProjectileID.ShadowOrb:
                    KillPet(projectile, player, BuffID.ShadowOrb, modPlayer.ShadowEnchant, "Shadow Orb Pet");
                    break;

                case ProjectileID.SuspiciousTentacle:
                    KillPet(projectile, player, BuffID.SuspiciousTentacle, modPlayer.CosmoForce, "Suspicious Eye Pet");
                    break;

                case ProjectileID.DD2PetGhost:
                    KillPet(projectile, player, BuffID.PetDD2Ghost, modPlayer.DarkEnchant, "Flickerwick Pet");
                    break;

                case ProjectileID.ZephyrFish:
                    KillPet(projectile, player, BuffID.ZephyrFish, modPlayer.FishSoul2, "Zephyr Fish Pet");
                    break;

                /*case ProjectileID.BabyGrinch:
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

                case ProjectileID.JavelinHostile:
                case ProjectileID.FlamingWood:
                    if (FargoWorld.MasochistMode)
                        projectile.position += projectile.velocity * .5f;
                    break;

                case ProjectileID.VortexAcid:
                    if (FargoWorld.MasochistMode)
                        projectile.position += projectile.velocity * .25f;
                    break;

                case ProjectileID.BombSkeletronPrime:
                    if (FargoWorld.MasochistMode)
                    {
                        projectile.damage = 40;
                        //projectile.Damage();
                    }
                    break;

                case ProjectileID.CultistRitual:
                    if (FargoWorld.MasochistMode)
                    {
                        if (projectile.ai[0] > 120f && projectile.ai[0] < 299f)
                        {
                            projectile.ai[0] = 299f;
                            projectile.netUpdate = true;
                        }
                    }
                    break;

                case ProjectileID.MoonLeech:
                    if (projectile.ai[0] > 0f)
                    {
                        Vector2 distance = Main.player[(int)projectile.ai[1]].Center - projectile.Center - projectile.velocity;
                        if (distance != Vector2.Zero)
                            projectile.position += Vector2.Normalize(distance) * Math.Min(16f, distance.Length());
                    }
                    break;

                case ProjectileID.SandnadoHostile:
                    if (projectile.timeLeft == 1199 && Main.netMode != 1)
                    {
                        int n = NPC.NewNPC((int)projectile.Center.X, (int)projectile.Center.Y, NPCID.SandShark);
                        if (n < 200)
                        {
                            Main.npc[n].velocity.X = Main.rand.NextFloat(-10, 10);
                            Main.npc[n].velocity.Y = Main.rand.NextFloat(-20, -10);
                            Main.npc[n].netUpdate = true;
                            if (Main.netMode == 2)
                                NetMessage.SendData(23, -1, -1, null, n);
                        }
                    }
                    break;

                default:
                        break;
            }

            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                //switch wouldnt work because IDs not constant RIP
                /*if(projectile.type == thorium.ProjectileType("Omega"))
                {
                    KillPet(projectile, player, thorium.BuffType("OmegaBuff"), modPlayer.MeteorEnchant, "Omega Pet");
                }
                else */if (projectile.type == thorium.ProjectileType("IFO"))
                {
                    KillPet(projectile, player, thorium.BuffType("Identified"), modPlayer.MeteorEnchant, "I.F.O. Pet");
                }
                else if (projectile.type == thorium.ProjectileType("BioFeederPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("BioFeederBuff"), modPlayer.MeteorEnchant, "Bio-Feeder Pet");
                }
                else if (projectile.type == thorium.ProjectileType("BlisterPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("BlisterBuff"), modPlayer.CrimsonEnchant, "Blister Pet");
                }
                else if (projectile.type == thorium.ProjectileType("WyvernPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("WyvernPetBuff"), modPlayer.ShadowEnchant, "Wyvern Pet");
                }
                else if (projectile.type == thorium.ProjectileType("SupportLantern"))
                {
                    KillPet(projectile, player, thorium.BuffType("SupportLanternBuff"), modPlayer.MinerEnchant, "Inspiring Lantern Pet");
                }
                else if (projectile.type == thorium.ProjectileType("LockBoxPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("LockBoxBuff"), modPlayer.MinerEnchant, "Lock Box Pet");
                }
                else if (projectile.type == thorium.ProjectileType("Devil"))
                {
                    KillPet(projectile, player, thorium.BuffType("DevilBuff"), modPlayer.WarlockEnchant, "Li'l Devil Minion", true);
                }
                else if (projectile.type == thorium.ProjectileType("Angel"))
                {
                    KillPet(projectile, player, thorium.BuffType("AngelBuff"), modPlayer.SacredEnchant, "Li'l Cherub Minion", true);
                }
                else if (projectile.type == thorium.ProjectileType("LifeSpirit"))
                {
                    KillPet(projectile, player, thorium.BuffType("LifeSpiritBuff"), modPlayer.SacredEnchant, "Life Spirit Pet");
                }
                else if (projectile.type == thorium.ProjectileType("HolyGoat"))
                {
                    KillPet(projectile, player, thorium.BuffType("HolyGostBuff"), modPlayer.BinderEnchant, "Holy Goat Pet");
                }
                else if (projectile.type == thorium.ProjectileType("MinionSapling"))
                {
                    KillPet(projectile, player, thorium.BuffType("SaplingBuff"), modPlayer.LivingWoodEnchant, "Sapling Minion", true);
                }
                else if (projectile.type == thorium.ProjectileType("SnowyOwlPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("SnowyOwlBuff"), modPlayer.IcyEnchant, "Owl Pet");
                }
                else if (projectile.type == thorium.ProjectileType("JellyfishPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("JellyPet"), modPlayer.DepthEnchant, "Jellyfish Pet");
                }
                else if (projectile.type == thorium.ProjectileType("LilMog"))
                {
                    KillPet(projectile, player, thorium.BuffType("LilMogBuff"), modPlayer.KnightEnchant, "Moogle Pet");
                }
                else if (projectile.type == thorium.ProjectileType("Maid1"))
                {
                    KillPet(projectile, player, thorium.BuffType("MaidBuff"), modPlayer.DreamEnchant, "Maid Pet");
                }
                else if (projectile.type == thorium.ProjectileType("PinkSlime"))
                {
                    KillPet(projectile, player, thorium.BuffType("PinkSlimeBuff"), modPlayer.IllumiteEnchant, "Pink Slime Pet");
                }
                else if (projectile.type == thorium.ProjectileType("ShinyPet"))
                {
                    KillPet(projectile, player, thorium.BuffType("ShineDust"), modPlayer.PlatinumEnchant, "Glitter Pet");
                }
                else if (projectile.type == thorium.ProjectileType("DrachmaBag"))
                {
                    KillPet(projectile, player, thorium.BuffType("DrachmaBuff"), modPlayer.GoldEnchant, "Coin Bag Pet");
                }
            }

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
                    projectile.Name = "Vulture Feather";
                    return Color.Brown;
                }

                else if (projectile.type == ProjectileID.SpikyBall)
                {
                    return Color.Red;
                }

                else if (projectile.type == ProjectileID.PineNeedleFriendly)
                {
                    return Color.GreenYellow;
                }

                else if(projectile.type == ProjectileID.Bone || projectile.type == ProjectileID.BoneGloveProj)
                {
                    return Color.SandyBrown;
                }

                else if (projectile.type == ProjectileID.DemonSickle || projectile.type == ProjectileID.DemonScythe)
                {
                    projectile.Name = "Blood Scythe";
                    return Color.Red;
                }
            }

            return null;
        }

        public override void OnHitNPC(Projectile projectile, NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.SqueakyToy)
                return;

            if (projectile.minion)
            {
                if (modPlayer.UniverseEffect || modPlayer.Eternity)
                {
                    target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 240, true);
                }
            }

            if (Fargowiltas.Instance.ThoriumLoaded) ThoriumOnHit(projectile, crit);
        }

        private void ThoriumOnHit(Projectile projectile, bool crit)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (Soulcheck.GetValue("Jester Bell"))
            {
                //jester effect
                if (modPlayer.MidgardForce && crit)
                {
                    for (int m = 0; m < 1000; m++)
                    {
                        Projectile projectile2 = Main.projectile[m];
                        if (projectile2.active && projectile2.type == thorium.ProjectileType("JestersBell"))
                        {
                            return;
                        }
                    }
                    Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 35, 1f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 50f, 0f, 0f, thorium.ProjectileType("JestersBell"), 0, 0f, projectile.owner, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("JestersBell2"), 0, 0f, projectile.owner, 0f, 0f);
                }
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
            //FargoPlayer modPlayer = target.GetModPlayer<FargoPlayer>(mod);

            /*if (FargoWorld.MasochistMode)
            {
                switch(projectile.type)
                {
                    

                    default:
                        break;
                }
            }*/

            if(squeakyToy)
            {
                damage = 1;
                target.GetModPlayer<FargoPlayer>().Squeak(target.Center);
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, int damage, bool crit)
        {
            if (FargoWorld.MasochistMode)
            {
                switch(projectile.type)
                {
                    case ProjectileID.JavelinHostile:
                        target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(60, 90));
                        break;

                    case ProjectileID.DemonSickle:
                        if (IsRecolor)
                        {
                            target.AddBuff(BuffID.ShadowFlame, 300);
                            target.AddBuff(BuffID.Bleeding, 600);
                        }
                        else
                        {
                            target.AddBuff(BuffID.Darkness, Main.rand.Next(900, 1800));
                            target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 600));
                        }
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
                    case ProjectileID.GoldenShowerHostile:
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.wallBoss, NPCID.WallofFlesh))
                        {
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(120, 240));
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(120, 240));
                            target.velocity = Vector2.Zero;
                        }
                        break;

                    case ProjectileID.DeathSickle:
                        target.AddBuff(mod.BuffType("MarkedforDeath"), 300);
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

                    case ProjectileID.SpikedSlimeSpike:
                        target.AddBuff(BuffID.Slimed, 120);
                        break;

                    //CULTIST OP
                    case ProjectileID.CultistBossLightningOrb:
                    case ProjectileID.CultistBossLightningOrbArc:
                        target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.Electrified, Main.rand.Next(60, 300));
                        break;

                    case ProjectileID.CultistBossIceMist:
                        if (!target.HasBuff(BuffID.Frozen))
                            target.AddBuff(BuffID.Frozen, Main.rand.Next(30, 90));
                        target.AddBuff(BuffID.Chilled, Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.CultistBossFireBall:
                        target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(90, 900));
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(120, 600));
                        damage = damage * 3 / 2;
                        break;

                    case ProjectileID.CultistBossFireBallClone:
                        target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 600));
                        damage = damage * 3 / 2;
                        break;

                    case ProjectileID.PaladinsHammerHostile:
                        target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(60));
                        break;

                    case ProjectileID.RuneBlast:
                        target.AddBuff(mod.BuffType("FlamesoftheUniverse"), Main.rand.Next(30, 120));
                        break;

                    case ProjectileID.ThornBall:
                    case ProjectileID.PoisonSeedPlantera:
                    case ProjectileID.SeedPlantera:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(60, 300));
                        bool isVenomed = false;
                        for (int i = 0; i < 22; i++)
                        {
                            if (target.buffType[i] == BuffID.Venom && target.buffTime[i] > 1)
                            {
                                isVenomed = true;
                                target.buffTime[i] += Main.rand.Next(60, 180);
                                break;
                            }
                        }
                        if (!isVenomed)
                        {
                            target.AddBuff(BuffID.Venom, Main.rand.Next(60, 180));
                        }
                        target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 300));
                        break;

                    case ProjectileID.DesertDjinnCurse:
                        if (target.ZoneCorrupt)
                            target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 900));
                        else if (target.ZoneCrimson)
                            target.AddBuff(BuffID.Ichor, Main.rand.Next(900, 1800));
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
                        target.AddBuff(mod.BuffType("MarkedforDeath"), 180);
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

                    case ProjectileID.VortexAcid:
                    case ProjectileID.VortexLaser:
                        target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(30, 180));
                        target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(30, 180));
                        break;

                    case ProjectileID.VortexLightning:
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.moonBoss, NPCID.MoonLordCore))
                        {
                            damage *= 3;
                            target.AddBuff(BuffID.Electrified, Main.rand.Next(300, 600));
                        }
                        else
                        {
                            damage *= 2;
                            target.AddBuff(BuffID.Electrified, Main.rand.Next(30, 300));
                        }
                        break;

                    case ProjectileID.LostSoulHostile:
                        target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(30, 240));
                        break;

                    case ProjectileID.InfernoHostileBlast:
                    case ProjectileID.InfernoHostileBolt:
                        if (Main.rand.Next(5) == 0)
                            target.AddBuff(mod.BuffType("Fused"), 1800);
                        break;

                    case ProjectileID.ShadowBeamHostile:
                        target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(1800, 3600));
                        target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.PhantasmalDeathray:
                        if (Main.npc[(int)projectile.ai[1]].active)
                        {
                            if (Main.npc[(int)projectile.ai[1]].type == NPCID.MoonLordHead)
                            {
                                int d = Main.rand.Next(Fargowiltas.DebuffIDs.Length);
                                target.AddBuff(Fargowiltas.DebuffIDs[d], Main.rand.Next(60, 300));

                                target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 600);
                                target.AddBuff(mod.BuffType("CurseoftheMoon"), 1800);
                                target.AddBuff(mod.BuffType("GodEater"), Main.expertMode ? 210 : 420);
                            }
                            else if (Main.npc[(int)projectile.ai[1]].type == NPCID.MoonLordFreeEye)
                            {
                                goto case ProjectileID.PhantasmalSphere;
                            }
                        }
                        break;

                    case ProjectileID.PhantasmalBolt:
                    case ProjectileID.PhantasmalEye:
                    case ProjectileID.PhantasmalSphere:
                        target.AddBuff(mod.BuffType("CurseoftheMoon"), Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.RocketSkeleton:
                        target.AddBuff(BuffID.Dazed, Main.rand.Next(30, 150));
                        target.AddBuff(BuffID.Confused, Main.rand.Next(60, 300));
                        break;

                    case ProjectileID.FlamesTrap:
                    case ProjectileID.GeyserTrap:
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 600));
                        if (NPC.golemBoss != -1 && Main.npc[NPC.golemBoss].active && Main.npc[NPC.golemBoss].type == NPCID.Golem)
                        {
                            if (Main.tile[(int)Main.npc[NPC.golemBoss].Center.X / 16, (int)Main.npc[NPC.golemBoss].Center.Y / 16] == null || //outside temple
                                Main.tile[(int)Main.npc[NPC.golemBoss].Center.X / 16, (int)Main.npc[NPC.golemBoss].Center.Y / 16].wall != WallID.LihzahrdBrickUnsafe)
                            {
                                target.AddBuff(BuffID.Burning, Main.rand.Next(60, 300));
                            }
                        }
                        break;

                    case ProjectileID.SpikyBallTrap:
                        if (NPC.golemBoss != -1 && Main.npc[NPC.golemBoss].active && Main.npc[NPC.golemBoss].type == NPCID.Golem)
                            target.AddBuff(BuffID.Venom, Main.rand.Next(60, 600));
                        break;

                    case ProjectileID.DD2BetsyFireball:
                    case ProjectileID.DD2BetsyFlameBreath:
                        target.AddBuff(BuffID.WitheredArmor, Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.Ichor, Main.rand.Next(600, 900));
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(900, 1800));
                        break;

                    case ProjectileID.DD2DrakinShot:
                        target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.NebulaSphere:
                        target.AddBuff(BuffID.VortexDebuff, Main.rand.Next(300, 540));
                        break;

                    case ProjectileID.NebulaLaser:
                        target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(60, 120));
                        break;

                    case ProjectileID.NebulaBolt:
                        target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.StardustJellyfishSmall:
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.StardustSoldierLaser:
                        target.AddBuff(BuffID.WitheredArmor, Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(300, 600));
                        break;

                    case ProjectileID.Sharknado:
                        target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(600, 900));
                        if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.fishBossEX, NPCID.DukeFishron))
                        {
                            target.GetModPlayer<FargoPlayer>(mod).MaxLifeReduction += 100;
                            target.AddBuff(mod.BuffType("OceanicMaul"), Main.rand.Next(1800, 3600));
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        /*public override void ModifyHitNPC(Projectile projectile, NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.NecroEnchant && projectile.type == mod.ProjectileType("DungeonGuardian") && projectile.penetrate == 1)
                crit = true;

            if (modPlayer.FrostEnchant && projectile.type == ProjectileID.Blizzard && player.HeldItem.type != ItemID.BlizzardStaff)
            {
                target.AddBuff(BuffID.Chilled, 300);
                target.AddBuff(BuffID.Frostburn, 300);
            }
        }*/

        public override bool PreKill(Projectile projectile, int timeLeft)
        {
            if (FargoWorld.MasochistMode && projectile.type == ProjectileID.CrystalBullet && projectile.owner == Main.myPlayer)
            {
                if (Main.player[projectile.owner].GetModPlayer<FargoPlayer>().MasomodeCrystalTimer <= 0)
                {
                    Main.player[projectile.owner].GetModPlayer<FargoPlayer>().MasomodeCrystalTimer = 15;
                    return true;
                }
                else
                {
                    Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0.0f);
                    for (int index1 = 0; index1 < 5; ++index1) //vanilla dusts
                    {
                        int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, 0.0f, 0.0f, 0, new Color(), 1f);
                        Main.dust[index2].noGravity = true;
                        Dust dust1 = Main.dust[index2];
                        dust1.velocity = dust1.velocity * 1.5f;
                        Dust dust2 = Main.dust[index2];
                        dust2.scale = dust2.scale * 0.9f;
                    }
                    return false;
                }
            }
            return true;
        }

        public override void Kill(Projectile projectile, int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.CobaltEnchant && Soulcheck.GetValue("Cobalt Shards") && modPlayer.CobaltCD == 0 && CanSplit && projectile.friendly && projectile.damage > 0  && !projectile.minion && projectile.aiStyle != 19 && !Rotate && Main.rand.Next(4) == 0)
            {
                int damage = 40;
                if(modPlayer.EarthForce)
                    damage = 80;

                Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 27);

                for (int i = 0; i < 3; i++)
                {
                    float velX = -projectile.velocity.X * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    float velY = -projectile.velocity.Y * Main.rand.Next(40, 70) * 0.01f + Main.rand.Next(-20, 21) * 0.4f;
                    int p = Projectile.NewProjectile(projectile.position.X + velX, projectile.position.Y + velY, velX, velY, ProjectileID.CrystalShard, damage, 0f, projectile.owner);

                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }

                modPlayer.CobaltCD = 60;
            }

            switch (projectile.type)
            {
                case ProjectileID.Blizzard:
                    if (Rotate)
                        modPlayer.IcicleCount--;
                    break;

                case ProjectileID.Bone:
                    break;

                default:
                    if (modPlayer.OriEnchant && Rotate)
                        modPlayer.OriSpawn = false;
                    break;
            }
        }

        public static Projectile[] XWay(int num, Vector2 pos, int type, float speed, int damage, float knockback)
        {
            float[] _x = { 0, speed, 0, -speed, speed, -speed, speed, -speed, speed / 2, speed, -speed, speed / 2, speed, -speed / 2, -speed, -speed / 2 };
            float[] _y = { speed, 0, -speed, 0, speed, -speed, -speed, speed, speed, speed / 2, speed / 2, -speed, -speed / 2, speed, -speed / 2, -speed };

            Projectile[] projs = new Projectile[16];

            for (int i = 0; i < num; i++)
                projs[i] = NewProjectileDirectSafe(pos, new Vector2(_x[i], _y[i]), type, damage, knockback, Main.myPlayer);

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

        public static Projectile NewProjectileDirectSafe(Vector2 pos, Vector2 vel, int type, int damage, float knockback, int owner = 255, float ai0 = 0f, float ai1 = 0f)
        {
            int p = Projectile.NewProjectile(pos, vel, type, damage, knockback, owner, ai0, ai1);
            return (p < 1000) ? Main.projectile[p] : null;
        }
    }
}
