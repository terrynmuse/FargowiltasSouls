using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Events;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.NPCs
{
    public class FargoGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }

        //debuffs
        public bool SBleed;
        public bool Shock;
        public bool Rotting;
        public bool LeadPoison;
        public bool SqueakyToy;
        public bool SolarFlare;
        public bool TimeFrozen;
        public bool Chilled;
        public bool HellFire;

        public bool PillarSpawn = true;

        //masochist doom
        public byte masoAI = 0;
        public byte masoDeathAI = 0;
        public byte masoHurtAI = 0;
        public bool Transform = false;
        public int RegenTimer = 0;
        public ushort Counter = 0;
        public int Timer = 600;
        public byte SharkCount = 0;
        private static MethodInfo _startSandstormMethod;

        public override void ResetEffects(NPC npc)
        {
            SBleed = false;
            Shock = false;
            Rotting = false;
            LeadPoison = false;
            SqueakyToy = false;
            SolarFlare = false;
            Chilled = false;
            HellFire = false;
            npc.color = default(Color);
        }

        public override void SetDefaults(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                ResetRegenTimer(npc);

                switch (npc.type)
                {
                    case NPCID.Salamander: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander2: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander3: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander4: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander5: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander6: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander7: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander8: masoHurtAI = 1; npc.Opacity /= 25; break;
                    case NPCID.Salamander9: masoHurtAI = 1; npc.Opacity /= 25; break;

                    case NPCID.GiantShelly: masoHurtAI = 2; break;
                    case NPCID.GiantShelly2: masoHurtAI = 2; break;

                    case NPCID.RainbowSlime:
                        npc.scale = 3f;
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.VoodooDemon:
                        npc.buffImmune[BuffID.OnFire] = false;
                        break;

                    case NPCID.Hellhound:
                        npc.lavaImmune = true;
                        break;

                    case NPCID.WalkingAntlion:
                        npc.knockBackResist = 0f;
                        break;

                    case NPCID.DesertBeast:
                        npc.knockBackResist = 0f;
                        break;

                    case NPCID.DetonatingBubble:
                        if (!NPC.downedBoss3)
                        {
                            npc.noTileCollide = false;
                        }
                        break;

                    default:
                        break;
                }

                #region masomode AI inits
                switch (npc.type) //initializing masomode AI
                {
                    case NPCID.Tim:
                        masoAI = 1;
                        break;

                    case NPCID.RuneWizard:
                        masoAI = 2;
                        break;

                    case NPCID.CochinealBeetle:
                        masoAI = 3;
                        break;
                    case NPCID.CyanBeetle:
                        masoAI = 3;
                        break;
                    case NPCID.LacBeetle:
                        masoAI = 3;
                        break;

                    case NPCID.EnchantedSword:
                        masoAI = 4;
                        break;
                    case NPCID.CursedHammer:
                        masoAI = 4;
                        break;
                    case NPCID.CrimsonAxe:
                        masoAI = 4;
                        break;

                    case NPCID.Ghost:
                        masoAI = 5;
                        break;

                    case NPCID.Mummy:
                        masoAI = 6;
                        break;
                    case NPCID.DarkMummy:
                        masoAI = 6;
                        break;
                    case NPCID.LightMummy:
                        masoAI = 6;
                        break;

                    case NPCID.Derpling:
                        masoAI = 7;
                        break;

                    case NPCID.ChaosElemental:
                        masoAI = 8;
                        break;

                    case NPCID.MeteorHead:
                        masoAI = 9;
                        break;

                    case NPCID.BoneSerpentHead:
                        masoAI = 10;
                        break;

                    case NPCID.Vulture:
                        masoAI = 11;
                        break;

                    case NPCID.DoctorBones:
                        masoAI = 12;
                        break;

                    case NPCID.Crab:
                        masoAI = 13;
                        break;

                    case NPCID.ArmoredViking:
                        masoAI = 14;
                        break;

                    case NPCID.Crawdad:
                        masoAI = 15;
                        break;
                    case NPCID.Crawdad2:
                        masoAI = 15;
                        break;

                    case NPCID.BloodCrawlerWall:
                        masoAI = 16;
                        break;
                    case NPCID.WallCreeperWall:
                        masoAI = 16;
                        break;

                    case NPCID.SeekerHead:
                        masoAI = 17;
                        break;

                    case NPCID.Demon:
                        masoAI = 18;
                        break;

                    case NPCID.VoodooDemon:
                        masoAI = 19;
                        npc.buffImmune[BuffID.OnFire] = false;
                        break;

                    case NPCID.Piranha:
                        masoAI = 20;
                        break;

                    case NPCID.Shark:
                        masoAI = 21;
                        break;

                    case NPCID.BlackRecluse:
                        masoAI = 22;
                        break;
                    case NPCID.BlackRecluseWall:
                        masoAI = 22;
                        break;

                    case NPCID.EyeofCthulhu:
                        masoAI = 23;
                        break;

                    case NPCID.Retinazer:
                        masoAI = 24;
                        break;

                    case NPCID.Spazmatism:
                        masoAI = 25;
                        break;

                    case NPCID.LunarTowerNebula:
                        masoAI = 26;
                        break;

                    case NPCID.LunarTowerSolar:
                        masoAI = 27;
                        break;

                    case NPCID.LunarTowerStardust:
                        masoAI = 28;
                        break;

                    case NPCID.LunarTowerVortex:
                        masoAI = 29;
                        break;

                    case NPCID.CultistBoss:
                        masoAI = 30;
                        break;

                    default:
                        break;
                }

                switch (npc.type) //initializing death IDs
                {
                    case NPCID.Drippler:
                        masoDeathAI = 1;
                        break;

                    case NPCID.GoblinPeon:
                        masoDeathAI = 2;
                        break;
                    case NPCID.GoblinWarrior:
                        masoDeathAI = 2;
                        break;

                    case NPCID.AngryBones:
                        masoDeathAI = 3;
                        break;
                    case NPCID.AngryBonesBig:
                        masoDeathAI = 3;
                        break;
                    case NPCID.AngryBonesBigHelmet:
                        masoDeathAI = 3;
                        break;
                    case NPCID.AngryBonesBigMuscle:
                        masoDeathAI = 3;
                        break;

                    case NPCID.DungeonSlime:
                        masoDeathAI = 4;
                        break;

                    case NPCID.BlueSlime:
                        switch (npc.netID)
                        {
                            case NPCID.YellowSlime:
                                masoDeathAI = 5;
                                break;

                            case NPCID.PurpleSlime:
                                masoDeathAI = 6;
                                break;

                            case NPCID.RedSlime:
                                masoDeathAI = 7;
                                break;

                            default:
                                break;
                        }
                        break;

                    case NPCID.DrManFly:
                        masoDeathAI = 8;
                        break;

                    case NPCID.EyeofCthulhu:
                        masoDeathAI = 9;
                        break;

                    case NPCID.KingSlime:
                        masoDeathAI = 10;
                        break;

                    case NPCID.EaterofWorldsHead:
                        masoDeathAI = 11;
                        break;

                    case NPCID.BrainofCthulhu:
                        masoDeathAI = 12;
                        break;

                    case NPCID.QueenBee:
                        masoDeathAI = 13;
                        break;

                    case NPCID.SkeletronHead:
                        masoDeathAI = 14;
                        break;

                    case NPCID.WallofFlesh:
                        masoDeathAI = 15;
                        break;

                    case NPCID.TheDestroyer:
                        masoDeathAI = 16;
                        break;

                    case NPCID.SkeletronPrime:
                        masoDeathAI = 17;
                        break;

                    case NPCID.Retinazer:
                        masoDeathAI = 18;
                        break;

                    case NPCID.Spazmatism:
                        masoDeathAI = 19;
                        break;

                    case NPCID.Plantera:
                        masoDeathAI = 20;
                        break;

                    case NPCID.Golem:
                        masoDeathAI = 21;
                        break;

                    case NPCID.DukeFishron:
                        masoDeathAI = 22;
                        break;

                    case NPCID.CultistBoss:
                        masoDeathAI = 23;
                        break;

                    case NPCID.MoonLordCore:
                        masoDeathAI = 24;
                        break;

                    case NPCID.RainbowSlime:
                        masoDeathAI = 25;
                        break;

                    default:
                        break;
                }
                #endregion

                #region boss scaling
                // +2.5% hp each kill 
                // +1.25% damage each kill
                // max of 4x hp and 2.5x damage

                //pre hm get 8x and 5x
                switch (npc.type)
                {
                    case NPCID.EyeofCthulhu:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;
                    case NPCID.ServantofCthulhu:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;


                    case NPCID.KingSlime:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SlimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.SlimeCount * .0125));
                        break;


                    case NPCID.EaterofWorldsHead:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;
                    case NPCID.EaterofWorldsBody:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;
                    case NPCID.EaterofWorldsTail:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;


                    case NPCID.BrainofCthulhu:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BrainCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.BrainCount * .0125));
                        break;
                    case NPCID.Creeper:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BrainCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.BrainCount * .0125));
                        break;


                    case NPCID.QueenBee:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BeeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.BeeCount * .0125));
                        break;


                    case NPCID.SkeletronHead:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SkeletronCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.SkeletronCount * .0125));
                        break;
                    case NPCID.SkeletronHand:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SkeletronCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.SkeletronCount * .0125));
                        break;


                    case NPCID.WallofFlesh:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.WallCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.WallCount * .0125));
                        break;
                    case NPCID.WallofFleshEye:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.WallCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.WallCount * .0125));
                        break;


                    case NPCID.TheDestroyer:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                        break;
                    case NPCID.TheDestroyerBody:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                        break;
                    case NPCID.TheDestroyerTail:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                        break;


                    case NPCID.SkeletronPrime:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                        break;
                    case NPCID.PrimeCannon:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                        break;
                    case NPCID.PrimeLaser:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                        break;
                    case NPCID.PrimeSaw:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                        break;
                    case NPCID.PrimeVice:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                        break;


                    case NPCID.Retinazer:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.TwinsCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.TwinsCount * .0125));
                        break;
                    case NPCID.Spazmatism:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.TwinsCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.TwinsCount * .0125));
                        break;


                    case NPCID.Plantera:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                        break;
                    case NPCID.PlanterasHook:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                        break;
                    case NPCID.PlanterasTentacle:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                        break;

                    case NPCID.Golem:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                        break;
                    case NPCID.GolemFistLeft:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                        break;
                    case NPCID.GolemFistRight:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                        break;
                    case NPCID.GolemHead:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                        break;
                    case NPCID.GolemHeadFree:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                        break;


                    case NPCID.CultistBoss:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.CultistCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.CultistCount * .0125));
                        break;


                    case NPCID.DukeFishron:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.FishronCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.FishronCount * .0125));
                        break;


                    case NPCID.MoonLordCore:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                        break;
                    case NPCID.MoonLordHand:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                        break;
                    case NPCID.MoonLordHead:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                        break;
                    case NPCID.MoonLordFreeEye:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                        break;
                    case NPCID.MoonLordLeechBlob:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                        break;


                    default:
                        break;
                }
                #endregion
            }
        }

        public override bool PreAI(NPC npc)
        {
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if(TimeFrozen)
            {
                npc.position = npc.oldPosition;
                //npc.frameCounter--;
                npc.frameCounter = 0;
                return false;
            }

            return true;
        }

        public override void AI(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                if (RegenTimer > 0)
                {
                    RegenTimer--;
                }

                //transformations
                if(!Transform)
                {
                    int npcType = 0;

                    if (npc.type == NPCID.BlueSlime && npc.netID == 1 && NPC.downedSlimeKing && Main.rand.Next(5) == 0)
                    {
                        npcType = NPCID.SlimeSpiked;
                    }

                    int[] transforms = { NPCID.Zombie, NPCID.ArmedZombie, NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo, NPCID.PincushionZombie, NPCID.ArmedZombiePincussion, NPCID.FemaleZombie, NPCID.ArmedZombieCenx, NPCID.SlimedZombie, NPCID.ArmedZombieSlimed, NPCID.TwiggyZombie, NPCID.ArmedZombieTwiggy, NPCID.SwampZombie, NPCID.ArmedZombieSwamp, NPCID.Skeleton, NPCID.BoneThrowingSkeleton, NPCID.HeadacheSkeleton, NPCID.BoneThrowingSkeleton2, NPCID.MisassembledSkeleton, NPCID.BoneThrowingSkeleton3, NPCID.PantlessSkeleton, NPCID.BoneThrowingSkeleton4, NPCID.JungleSlime, NPCID.SpikedJungleSlime, NPCID.IceSlime, NPCID.SpikedIceSlime };

                    if(Array.IndexOf(transforms, npc.type) % 2 == 0 && NPC.downedBoss1 && Main.rand.Next(5) == 0)
                    {
                        npcType = transforms[Array.IndexOf(transforms, npc.type) + 1];
                    }

                    //zombie horde
                    if (npc.type == NPCID.Zombie && NPC.downedBoss1 && Main.rand.Next(5) == 0)
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            Vector2 pos = new Vector2(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y);

                            if (!Collision.SolidCollision(pos, npc.width, npc.height))
                            {
                                int j = NPC.NewNPC((int)pos.X, (int)pos.Y, NPCID.Zombie);
                                NPC zombie = Main.npc[j];
                                zombie.GetGlobalNPC<FargoGlobalNPC>().Transform = true;
                            }
                        }
                    }

                    if ((npc.type == NPCID.Crawdad || npc.type == NPCID.GiantShelly) && Main.rand.Next(5) == 0)
                    {
                        //pick a random salamander
                        npcType = Main.rand.Next(498, 507);
                    }
                    else if ((npc.type == NPCID.Salamander || npc.type == NPCID.Salamander2 || npc.type == NPCID.Salamander3 || npc.type == NPCID.Salamander4 || npc.type == NPCID.GiantShelly2) && Main.rand.Next(5) == 0)
                    {
                        //pick a random crawdad
                        npcType = Main.rand.Next(494, 496);
                    }
                    else if ((npc.type == NPCID.Salamander5 || npc.type == NPCID.Salamander6 || npc.type == NPCID.Salamander7 || npc.type == NPCID.Salamander8 || npc.type == NPCID.Crawdad2) && Main.rand.Next(5) == 0)
                    {
                        //pick a random shelly
                        npcType = Main.rand.Next(496, 498);
                    }
                    //random sharks
                    else if((npc.type == NPCID.Goldfish || npc.type == NPCID.GoldfishWalker || npc.type == NPCID.BlueJellyfish) && Main.rand.Next(6) == 0)
                    {
                        npcType = NPCID.Shark;
                    }
                    //mimic swapping
                    else if(npc.type == NPCID.BigMimicCorruption && Main.rand.Next(4) == 0)
                    {
                        npcType = NPCID.BigMimicCrimson;
                    }
                    else if (npc.type == NPCID.BigMimicCrimson && Main.rand.Next(4) == 0)
                    {
                        npcType = NPCID.BigMimicCorruption;
                    }

                    if(npcType != 0)
                    {
                        npc.Transform(npcType);
                    }
                    
                    Transform = true;
                }

                switch (masoAI)
                {
                    case 0: //default
                        break;

                    case 1: //tim
                        Aura(npc, 800, BuffID.Silenced);
                        break;

                    case 2: //runewizard
                        Aura(npc, 300, mod.BuffType("Hexed"), true);
                        break;

                    case 3: //beetles
                        Aura(npc, 400, mod.BuffType("Lethargic"));
                        break;

                    case 4: //enchanted sword family
                        Aura(npc, 400, BuffID.WitheredWeapon);
                        break;

                    case 5: //ghost
                        Aura(npc, 400, BuffID.Cursed);
                        break;

                    case 6: //mummies
                        Aura(npc, 500, BuffID.Slow);
                        break;

                    case 7: //derpling
                        Aura(npc, 400, BuffID.Confused, true);
                        break;

                    case 8: //chaos ele
                        Aura(npc, 1000, mod.BuffType("Flipped"));
                        break;

                    case 9: //meteor head
                        Counter++;
                        if (Counter >= 120)
                        {
                            Counter = 0;

                            Player player = Main.player[npc.FindClosestPlayer()];
                            if (npc.Distance(player.Center) < 600)
                            {
                                npc.velocity *= 5;
                            }
                        }

                        Aura(npc, 100, BuffID.Burning);
                        break;

                    case 10: //bone serpent head
                        Counter++;
                        if (Counter >= 300)
                        {
                            Counter = 0;

                            if (npc.Distance(Main.player[npc.FindClosestPlayer()].Center) < 600)
                            {
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.BurningSphere);
                            }
                        }
                        break;

                    case 11: //vulture
                        Counter++;
                        if (Counter >= 300)
                        {
                            Shoot(npc, 500, 10, ProjectileID.HarpyFeather, npc.damage / 2, 1, true);
                        }
                        break;

                    case 12: //dr bones
                        Counter++;
                        if (Counter >= 600)
                        {
                            Shoot(npc, 1000, 14, ProjectileID.Boulder, npc.damage * 4, 2);
                        }
                        break;

                    case 13: //crab
                        Counter++;
                        if (Counter >= 300)
                        {
                            Shoot(npc, 800, 14, ProjectileID.Bubble, npc.damage / 2, 1, false, true);
                        }
                        break;

                    case 14: //armored viking
                        Counter++;
                        if (Counter >= 10)
                        {
                            Shoot(npc, 200, 10, ProjectileID.IceSickle, npc.damage / 2, 1, false, true);
                        }
                        break;

                    case 15: //crawdads
                        Counter++;
                        if (Counter >= 300)
                        {
                            Counter = 0;

                            Player player = Main.player[npc.FindClosestPlayer()];
                            if (npc.Distance(player.Center) < 800)
                            {
                                Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * 10;
                                int bubble = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.DetonatingBubble);
                                Main.npc[bubble].velocity = velocity;
                                Main.npc[bubble].damage = npc.damage / 2;
                            }
                        }
                        break;

                    case 16: //blood crawler, wall creeper (on walls only)
                        Counter++;
                        if (Counter >= 600)
                        {
                            Shoot(npc, 400, 14, ProjectileID.WebSpit, npc.damage / 4, 0);
                        }
                        break;

                    case 17: //seekerhead
                        Counter++;
                        if (Counter >= 10)
                        {
                            Counter = 0;

                            Player player = Main.player[npc.FindClosestPlayer()];
                            if (npc.Distance(player.Center) < 500)
                            {
                                Projectile.NewProjectile(npc.Center, npc.velocity, ProjectileID.EyeFire, npc.damage / 3, 0f, Main.myPlayer);
                            }
                        }
                        break;

                    case 18: //demon
                        Counter++;
                        if (Counter >= 600)
                        {
                            Counter = 0;

                            Player player = Main.player[npc.FindClosestPlayer()];
                            if (npc.Distance(player.Center) < 800)
                            {
                                FargoGlobalProjectile.XWay(8, npc.Center, ProjectileID.DemonSickle, 1, npc.damage / 2, .5f);
                            }
                        }
                        break;

                    case 19: //voodoo demon
                        if (npc.lavaWet && !npc.HasBuff(BuffID.OnFire))
                        {
                            npc.AddBuff(BuffID.OnFire, 300);
                        }
                        else if (npc.HasBuff(BuffID.OnFire) && npc.buffTime[npc.FindBuffIndex(BuffID.OnFire)] < 60 && !NPC.AnyNPCs(NPCID.WallofFlesh))
                        {
                            NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.WallofFlesh);
                            Main.NewText("Wall of Flesh has awoken!", 175, 75);
                            npc.Transform(NPCID.Demon);
                        }
                        break;

                    case 20: //piranha
                        Counter++;
                        if (Counter >= 120)
                        {
                            Counter = 0;

                            if (NPC.CountNPCS(NPCID.Piranha) <= 10 && Main.rand.Next(2) == 0)
                            {
                                Player player = Main.player[npc.FindClosestPlayer()];
                                if (player.wet && player.HasBuff(BuffID.Bleeding))
                                {
                                    int piranha = NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-20, 20), (int)npc.Center.Y + Main.rand.Next(-20, 20), NPCID.Piranha);
                                    Main.npc[piranha].GetGlobalNPC<FargoGlobalNPC>().Counter = 0;
                                }
                            }
                        }
                        break;

                    case 21: //shark
                        Counter++;
                        if (Counter >= 240)
                        {
                            Counter = 0;

                            if (SharkCount < 5)
                            {
                                Player player = Main.player[npc.FindClosestPlayer()];
                                if (player.HasBuff(BuffID.Bleeding) && player.wet && npc.wet)
                                {
                                    npc.damage = (int)(npc.damage * 1.5);
                                    SharkCount++;
                                }
                            }
                        }
                        break;

                    case 22: //blackrecluse (on and off wall)
                        Counter++;
                        if (Counter >= 240)
                        {
                            Counter = 0;

                            Player player = Main.player[npc.FindClosestPlayer()];
                            player.AddBuff(BuffID.Webbed, 600);

                            if (npc.Distance(player.Center) < 2000f && player.HasBuff(BuffID.Webbed))
                            {
                                player.AddBuff(mod.BuffType("Defenseless"), 300);
                                npc.velocity *= 2;
                                //meme?
                                npc.damage = (int)(npc.damage * 1.5);
                            }
                        }
                        break;

                    case 23: //eye of cthulhu
                        Counter++;
                        if (Counter >= 240)
                        {
                            Counter = 0;

                            if (npc.life <= npc.lifeMax * 0.65 && NPC.CountNPCS(NPCID.ServantofCthulhu) < 12)
                            {
                                int[] eyes = new int[4];

                                for (int i = 0; i < 4; i++)
                                {
                                    eyes[i] = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.ServantofCthulhu);
                                }

                                Main.npc[eyes[0]].velocity = npc.velocity * new Vector2(2, 2);
                                Main.npc[eyes[1]].velocity = npc.velocity * new Vector2(-2, 2);
                                Main.npc[eyes[2]].velocity = npc.velocity * new Vector2(-2, -2);
                                Main.npc[eyes[3]].velocity = npc.velocity * new Vector2(2, -2);
                            }
                        }
                        break;

                    case 24: //retinazer
                        if (!NPC.AnyNPCs(NPCID.Spazmatism))
                        {
                            Timer--;

                            if (Timer == 0)
                            {
                                int spawn = NPC.NewNPC((int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), NPCID.Spazmatism);
                                Main.npc[spawn].life = Main.npc[spawn].lifeMax / 2;
                                Main.NewText("Spazmatism has been revived!", 175, 75);
                                Timer = 600;
                            }
                        }
                        break;

                    case 25: //spazmatism
                        if (!NPC.AnyNPCs(NPCID.Retinazer))
                        {
                            Timer--;

                            if (Timer == 0)
                            {
                                int spawn = NPC.NewNPC((int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), NPCID.Retinazer);
                                Main.npc[spawn].life = Main.npc[spawn].lifeMax / 2;
                                Main.NewText("Retinazer has been revived!", 175, 75);
                                Timer = 600;
                            }
                        }
                        break;

                    case 26: //nebula pill
                        foreach (Player p in Main.player)
                        {
                            if (npc.Distance(p.Center) < 5000)
                            {
                                p.AddBuff(mod.BuffType("Atrophied"), 2);
                                p.AddBuff(mod.BuffType("Jammed"), 2);
                                p.AddBuff(mod.BuffType("Antisocial"), 2);
                            }
                        }
                        break;

                    case 27: //solar pill
                        foreach (Player p in Main.player)
                        {
                            if (npc.Distance(p.Center) < 5000)
                            {
                                p.buffImmune[BuffID.Silenced] = false;
                                p.AddBuff(BuffID.Silenced, 2);
                                p.AddBuff(mod.BuffType("Jammed"), 2);
                                p.AddBuff(mod.BuffType("Antisocial"), 2);
                            }
                        }
                        break;

                    case 28: //stardust pill
                        foreach (Player p in Main.player)
                        {
                            if (npc.Distance(p.Center) < 5000)
                            {
                                p.AddBuff(mod.BuffType("Atrophied"), 2);
                                p.AddBuff(mod.BuffType("Jammed"), 2);
                                p.buffImmune[BuffID.Silenced] = false;
                                p.AddBuff(BuffID.Silenced, 2);
                            }
                        }
                        break;

                    case 29: //vortex pill
                        foreach (Player p in Main.player)
                        {
                            if (npc.Distance(p.Center) < 5000)
                            {
                                p.AddBuff(mod.BuffType("Atrophied"), 2);
                                p.buffImmune[BuffID.Silenced] = false;
                                p.AddBuff(BuffID.Silenced, 2);
                                p.AddBuff(mod.BuffType("Antisocial"), 2);
                            }
                        }
                        break;

                    case 30: //cultistboss
                        Aura(npc, 2000, mod.BuffType("ClippedWings"));
                        break;

                    default:
                        break;
                }
            }
        }

        private void Aura(NPC npc, float distance, int buff, bool reverse = false)
        {
            foreach (Player p in Main.player.Where(x => x.active && !x.dead))
            {
                if((reverse && npc.Distance(p.Center) > distance) || (!reverse && npc.Distance(p.Center) < distance))
                {
                    p.AddBuff(buff, 2);
                }
            }  
        }

        private void Shoot(NPC npc, float distance, int speed, int proj, int dmg, float kb, bool recolor = false, bool hostile = false)
        {
            Player player = Main.player[npc.FindClosestPlayer()];

            if (npc.Distance(player.Center) < distance)
            {
                Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * speed;
                Projectile p = Projectile.NewProjectileDirect(npc.Center, velocity, proj, dmg, kb, Main.myPlayer);

                if(recolor)
                {
                    p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                }

                if(hostile)
                {
                    p.friendly = false;
                    p.hostile = true;
                }
            }

            Counter = 0;
        }

        public override Color? GetAlpha(NPC npc, Color drawColor)
        {
            if (SharkCount != 0)
            {
                Color r = new Color(0, 0, 0, 255);
                r.R = (byte)(SharkCount * 20 + 155);
                return r;
            }

            return null;
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (LeadPoison)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.Lead, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Dust expr_1CCF_cp_0 = Main.dust[dust];
                    expr_1CCF_cp_0.velocity.Y = expr_1CCF_cp_0.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }

            if (HellFire)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, DustID.SolarFlare, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);

                    Dust expr_1CCF_cp_0 = Main.dust[dust];
                    expr_1CCF_cp_0.velocity.Y = expr_1CCF_cp_0.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }

            if (SqueakyToy)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, 44, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.LimeGreen, 1f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Dust expr_1CCF_cp_0 = Main.dust[dust];
                    expr_1CCF_cp_0.velocity.Y = expr_1CCF_cp_0.velocity.Y - 0.5f;
                    if (Main.rand.Next(4) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }

                Lighting.AddLight((int)(npc.position.X / 16f), (int)(npc.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
            }
        }

        public override void OnHitPlayer(NPC npc, Player target, int damage, bool crit)
        {
            if(FargoWorld.MasochistMode)
            {
                if (Main.hardMode && Main.rand.Next(10) == 0)
                {
                    if (npc.type == NPCID.SlimeSpiked && !NPC.AnyNPCs(NPCID.KingSlime))
                    {
                        npc.Transform(NPCID.KingSlime);
                        npc.velocity.Y = -20f;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                    }

                    if (npc.type == NPCID.WanderingEye && !NPC.AnyNPCs(NPCID.EyeofCthulhu))
                    {
                        npc.Transform(NPCID.EyeofCthulhu);
                        npc.velocity.Y = -5f;
                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                    }
                }

                if (npc.netID == NPCID.Pinky && !target.noKnockback)
                {
                    Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 30;
                    target.velocity = velocity;
                }
            }
        }

        public void ResetRegenTimer(NPC npc)
        {
            if (npc.boss)
            {
                //5 sec
                RegenTimer = 300;
            }
            else
            {
                //10 sec
                RegenTimer = 600;
            }
        }
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			int dmg;

            if (FargoWorld.MasochistMode)
            {
                if(!npc.dontTakeDamage && RegenTimer <= 0)
                {
                    npc.lifeRegen += 1 + npc.lifeMax / 25;
                }

                if(npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                {
                    npc.lifeRegen += npc.lifeMax / 15;
                }
            }

            if (SBleed)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 10)
				{
					damage = 5;
				}
				
				if(Main.rand.Next(4) == 0)
				{
                    dmg = 20;
					
					int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 40, 0f + Main.rand.Next(-5, 5), -5f, mod.ProjectileType("SuperBlood"), dmg, 0f, Main.myPlayer);

                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
			}

            if (Rotting)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 100;

                if (damage < 5)
                { 
                    damage = 5;
                }
            }

            if(LeadPoison)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 10;
            }

            if(SolarFlare)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 200;

                if (damage < 20)
                {
                    damage = 20;
                }
            }

            if(HellFire)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 50 * npc.lifeMax / npc.life;

                if (damage < 4)
                {
                    damage = 4;
                }
            }
		}
		
		public override void EditSpawnRate (Player player, ref int spawnRate, ref int maxSpawns)
		{
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
            if(FargoWorld.MasochistMode)
            {
                if(!Main.hardMode)
                {
                    //1.3x spawn rate
                    spawnRate = (int)(spawnRate * 0.75);
                    //2x max spawn
                    maxSpawns = (int)(maxSpawns * 2f);
                }
                else
                {
                    //2x spawn rate
                    spawnRate = (int)(spawnRate * 0.5);
                    //3x max spawn
                    maxSpawns = (int)(maxSpawns * 3f);
                }
            }

            if (modPlayer.Bloodthirst)
            {
                //20x spawn rate
                spawnRate = (int)(spawnRate * 0.05);
                //20x max spawn
                maxSpawns = (int)(maxSpawns * 20f);
            }

            if (modPlayer.BuilderMode)
			{
				maxSpawns = (int)(maxSpawns * 0f);
			}
		}
		
		public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
		{
            //layers
			int y = spawnInfo.spawnTileY;
			bool cavern = y >= Main.maxTilesY * 0.4f && y <= Main.maxTilesY * 0.8f;
            bool underground = y > Main.worldSurface && y <= Main.maxTilesY * 0.4f;
            bool surface = y < Main.worldSurface && !spawnInfo.sky;
            bool underworld = spawnInfo.player.ZoneUnderworldHeight;
            bool sky = spawnInfo.sky;
            
            //times
            bool night = !Main.dayTime;
            bool day = Main.dayTime;

            //biomes
            bool noBiome = Fargowiltas.NoBiomeNormalSpawn(spawnInfo);
            bool ocean = spawnInfo.player.ZoneBeach;
            bool dungeon = spawnInfo.player.ZoneDungeon;
            bool meteor = spawnInfo.player.ZoneMeteor;
            bool spiderCave = spawnInfo.spiderCave;
            bool mushroom = spawnInfo.player.ZoneGlowshroom;
            bool jungle = spawnInfo.player.ZoneJungle;
            bool granite = spawnInfo.granite;
            bool marble = spawnInfo.marble;
            bool corruption = spawnInfo.player.ZoneCorrupt;
            bool crimson = spawnInfo.player.ZoneCrimson;
            bool snow = spawnInfo.player.ZoneSnow;
            bool hallow = spawnInfo.player.ZoneHoly;
            bool desert = spawnInfo.player.ZoneDesert;

            bool nebulaTower = spawnInfo.player.ZoneTowerNebula;
            bool vortexTower = spawnInfo.player.ZoneTowerVortex;
            bool stardustTower = spawnInfo.player.ZoneTowerStardust;
            bool solarTower = spawnInfo.player.ZoneTowerSolar;

            bool water = spawnInfo.water;

            //events
            bool goblinArmy = Main.invasionType == 1;
            bool frostLegion = Main.invasionType == 2;
            bool pirates = Main.invasionType == 3;
            bool martianMadness = Main.invasionType == 4;
            bool oldOnesArmy = DD2Event.Ongoing && spawnInfo.player.ZoneOldOneArmy;
            bool frostMoon = surface && night && Main.snowMoon;
            bool pumpkinMoon = surface && night && Main.pumpkinMoon;
            bool solarEclipse = surface && day && Main.eclipse;
            

            //no work?
            //is lava on screen
            bool nearLava = Collision.LavaCollision(spawnInfo.player.position, spawnInfo.spawnTileX, spawnInfo.spawnTileY);


            //ZoneCorrupt
            //ZoneHoly
            //ZoneMeteor
            //ZoneJungle
            //ZoneSnow
            //ZoneCrimson

            //ZoneDesert
            //ZoneGlowshroom
            //ZoneUndergroundDesert
            //ZoneSkyHeight
            //ZoneOverworldHeight
            //ZoneDirtLayerHeight
            //ZoneRockLayerHeight
            //ZoneUnderworldHeight
            //ZoneBeach
            //ZoneRain
            //ZoneSandstorm
            //Main.raining
            //NPC.AnyNPCs(NPCID.IceGolem)
            //TileID.Sets.Conversion.Sand[spawnInfo.spawnTileType]
            //spawnInfo.granite
            //spawnInfo.lihzahrd
            //spawnInfo.marble
            //spawnInfo.sky
            //spawnInfo.spiderCave
            //spawnInfo.player.adjLava
			
			//MASOCHIST MODE
			if(FargoWorld.MasochistMode)
			{
				//all the pre hardmode
				if(!Main.hardMode)
				{
                    //bosses
					if(surface && day && NPC.downedSlimeKing && NPC.downedBoss2 && Main.slimeRain)
					{
                        if(!NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            pool[NPCID.KingSlime] = .04f;
                        }
                        else
                        {
                            pool[NPCID.KingSlime] = .01f;
                        }
					}
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && surface && night && NPC.downedBoss1 && NPC.downedBoss3)
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .004f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .001f;
                        }
                    }
					
					if(surface && night && NPC.downedBoss1 && NPC.downedBoss3 && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .01f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .0025f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && corruption && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.EaterofWorldsHead))
                        {
                            pool[NPCID.EaterofWorldsHead] = .005f;
                        }
                        else
                        {
                            pool[NPCID.EaterofWorldsHead] = .00125f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedBoss2 && NPC.downedBoss3 && NPC.downedQueenBee && crimson && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.BrainofCthulhu))
                        {
                            pool[NPCID.BrainofCthulhu] = .005f;
                        }
                        else
                        {
                            pool[NPCID.BrainofCthulhu] = .00125f;
                        }
                    }

                    //random
                    if(noBiome && cavern && NPC.downedBoss3)
                    {
                        pool[NPCID.DarkCaster] = .1f;
                    }

                    //maybe working?
                    if(nearLava && !surface)
                    {
                        pool[NPCID.FireImp] = .1f;
                        pool[NPCID.LavaSlime] = .1f;
                    }

                    if(night && surface && noBiome)
                    {
                        pool[NPCID.CorruptBunny] = .1f;
                        pool[NPCID.CrimsonBunny] = .1f;
                    }

                    if(night && ocean)
                    {
                        pool[NPCID.CorruptGoldfish] = .1f;
                        pool[NPCID.CrimsonGoldfish] = .1f;
                    }

                    if(night && snow && surface)
                    {
                        pool[NPCID.CorruptPenguin] = .1f;
                        pool[NPCID.CrimsonPenguin] = .1f;
                    }

                    if(marble && NPC.downedBoss2)
                    {
                        pool[NPCID.Medusa] = .1f;
                    }

                    if(Fargowiltas.NormalSpawn(spawnInfo) && night && jungle && NPC.downedBoss1)
                    {
                        pool[NPCID.DoctorBones] = .05f;
                    }

                    if(granite)
                    {
                        pool[NPCID.GraniteFlyer] = .1f;
                        pool[NPCID.GraniteGolem] = .1f;
                    }

                    if (sky)
                    {
                        pool[NPCID.AngryNimbus] = .05f;
                    }

                    if (mushroom)
                    {
                        pool[NPCID.FungiBulb] = .1f;
                        pool[NPCID.MushiLadybug] = .1f;
                        pool[NPCID.ZombieMushroom] = .1f;
                        pool[NPCID.ZombieMushroomHat] = .1f;
                        pool[NPCID.AnomuraFungus] = .1f;
                    }

                    if(underworld)
                    {
                        pool[NPCID.LeechHead] = .05f;
                    }

                    if(corruption && NPC.downedBoss2)
                    {
                        pool[NPCID.SeekerHead] = .01f;
                    }

                    if (crimson && NPC.downedBoss2)
                    {
                        pool[NPCID.IchorSticker] = .01f;
                    }

                    if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedMechBoss2 && !surface)
                    {
                        pool[NPCID.Mimic] = .01f;
                    }

                }
				//all the hardmode
                else
				{
                    //bosses
					if(Fargowiltas.NormalSpawn(spawnInfo) && surface && day && Main.slimeRain)
					{
                        if (!NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            pool[NPCID.KingSlime] = .1f;
                        }
                        else
                        {
                            pool[NPCID.KingSlime] = .025f;
                        }
                    }
				
					if(surface && day && noBiome)
					{
                        if (!NPC.AnyNPCs(NPCID.KingSlime))
                        {
                            pool[NPCID.KingSlime] = .04f;
                        }
                        else
                        {
                            pool[NPCID.KingSlime] = .01f;
                        }
                    }
					
					if(surface && night && Fargowiltas.NormalSpawn(spawnInfo))
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .02f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .005f;
                        }
                    }
					
					if(surface && night && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.EyeofCthulhu))
                        {
                            pool[NPCID.EyeofCthulhu] = .05f;
                        }
                        else
                        {
                            pool[NPCID.EyeofCthulhu] = .0125f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && corruption && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.EaterofWorldsHead))
                        {
                            pool[NPCID.EaterofWorldsHead] = .01f;
                        }
                        else
                        {
                            pool[NPCID.EaterofWorldsHead] = .0025f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && crimson && !underworld)
					{
                        if (!NPC.AnyNPCs(NPCID.BrainofCthulhu))
                        {
                            pool[NPCID.BrainofCthulhu] = .01f;
                        }
                        else
                        {
                            pool[NPCID.BrainofCthulhu] = .0025f;
                        }
                    }			
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && night && spawnInfo.player.ZoneDungeon && NPC.downedMechBossAny)
					{
                        if (!NPC.AnyNPCs(NPCID.SkeletronHead))
                        {
                            pool[NPCID.SkeletronHead] = .005f;
                        }
                        else
                        {
                            pool[NPCID.SkeletronHead] = .00125f;
                        }
                    }
					
					if(Fargowiltas.NormalSpawn(spawnInfo) && day && spawnInfo.player.ZoneJungle && NPC.downedMechBossAny)
					{
                        if (!NPC.AnyNPCs(NPCID.QueenBee))
                        {
                            pool[NPCID.QueenBee] = .005f;
                        }
                        else
                        {
                            pool[NPCID.QueenBee] = .00125f;
                        }
                    }
				
					//all the hard mode bosses
					if(Fargowiltas.NormalSpawn(spawnInfo) && surface && night && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3 && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.Retinazer) || !NPC.AnyNPCs(NPCID.Spazmatism) || !NPC.AnyNPCs(NPCID.SkeletronPrime) || !NPC.AnyNPCs(NPCID.TheDestroyer))
                        {
                            pool[NPCID.Retinazer] = .005f;
                            pool[NPCID.Spazmatism] = .005f;
                            pool[NPCID.TheDestroyer] = .005f;
                            pool[NPCID.SkeletronPrime] = .005f;
                        }
                        else
                        {
                            pool[NPCID.Retinazer] = .0025f;
                            pool[NPCID.Spazmatism] = .0025f;
                            pool[NPCID.TheDestroyer] = .0025f;
                            pool[NPCID.SkeletronPrime] = .0025f;
                        }
                    }
					
					if(surface && night && NPC.downedPlantBoss && Main.bloodMoon)
					{
                        if (!NPC.AnyNPCs(NPCID.Retinazer) || !NPC.AnyNPCs(NPCID.Spazmatism) || !NPC.AnyNPCs(NPCID.SkeletronPrime) || !NPC.AnyNPCs(NPCID.TheDestroyer))
                        {
                            pool[NPCID.Retinazer] = .05f;
                            pool[NPCID.Spazmatism] = .05f;
                            pool[NPCID.TheDestroyer] = .05f;
                            pool[NPCID.SkeletronPrime] = .05f;
                        }
                        else
                        {
                            pool[NPCID.Retinazer] = .025f;
                            pool[NPCID.Spazmatism] = .025f;
                            pool[NPCID.TheDestroyer] = .025f;
                            pool[NPCID.SkeletronPrime] = .025f;
                        }
                    }

                    if(ocean && NPC.downedFishron)
                    {
                        pool[NPCID.DukeFishron] = .005f;
                    }

                    if(jungle && !surface && NPC.downedGolemBoss)
                    {
                        pool[NPCID.Plantera] = .005f;
                    }

                    if(underworld && NPC.downedGolemBoss)
                    {
                        pool[NPCID.DD2Betsy] = .005f;
                    }

                    //random
                    if(surface && day && NPC.downedMechBossAny)
                    {
                        pool[NPCID.CultistArcherWhite] = .05f;
                    }

                    if(!surface && jungle)
                    {
                        pool[NPCID.BigMimicJungle] = .05f;
                    }

                    if(desert && surface && night)
                    {
                        pool[NPCID.DesertBeast] = .05f;
                    }

                    if(NPC.AnyNPCs(NPCID.MoonLordHand))
                    {
                        pool[NPCID.AncientCultistSquidhead] = .05f;
                        pool[NPCID.CultistDragonHead] = .05f;
                    }

                    //weather bois
                    if (hallow && surface)
                    {
                        pool[NPCID.RainbowSlime] = .01f;
                    }                    

                    if (snow && surface)
                    {
                        pool[NPCID.IceGolem] = .01f;
                    }

                    //pumpkin and frost moon bois
                    if (NPC.downedMechBossAny)
                    {
                        if (surface)
                        {
                            if(night)
                            {
                                if (noBiome)
                                {
                                    pool[NPCID.Scarecrow1] = .01f;
                                    pool[NPCID.Scarecrow2] = .01f;
                                    pool[NPCID.Scarecrow3] = .01f;
                                    pool[NPCID.Scarecrow4] = .01f;
                                    pool[NPCID.Scarecrow5] = .01f;
                                    pool[NPCID.Scarecrow6] = .01f;
                                    pool[NPCID.Scarecrow7] = .01f;
                                    pool[NPCID.Scarecrow8] = .01f;
                                    pool[NPCID.Scarecrow9] = .01f;
                                    pool[NPCID.Scarecrow10] = .01f;

                                    if(NPC.downedHalloweenKing)
                                    {
                                        pool[NPCID.HeadlessHorseman] = .002f;
                                        pool[NPCID.Pumpking] = .001f;
                                    }
                                }
                                if (crimson || corruption)
                                {
                                    pool[NPCID.Splinterling] = .05f;

                                    if (NPC.downedHalloweenTree)
                                    {
                                        pool[NPCID.MourningWood] = .002f;
                                    }
                                }
                                if (hallow)
                                {
                                    pool[NPCID.PresentMimic] = .05f;
                                    pool[NPCID.GingerbreadMan] = .05f;
                                }
                                if (snow)
                                {
                                    pool[NPCID.ZombieElf] = .02f;
                                    pool[NPCID.ZombieElfBeard] = .02f;
                                    pool[NPCID.ZombieElfGirl] = .02f;
                                    pool[NPCID.Yeti] = .01f;

                                    if (NPC.downedChristmasSantank)
                                    {
                                        pool[NPCID.SantaNK1] = .002f;
                                    }
                                }
                            }
                            if (day && snow)
                            {
                                pool[NPCID.ElfArcher] = .05f;
                                pool[NPCID.ElfCopter] = .01f;

                                if (NPC.downedChristmasTree)
                                {
                                    pool[NPCID.Everscream] = .002f;
                                }
                            }
                        }
                        if(snow)
                        {
                            if(underground)
                            {
                                pool[NPCID.Nutcracker] = .05f;
                            }
                            if(cavern)
                            {
                                pool[NPCID.Krampus] = .05f;

                                if (NPC.downedChristmasIceQueen)
                                {
                                    pool[NPCID.IceQueen] = .001f;
                                }
                            }
                            
                        }
                        if(underworld)
                        {
                            pool[NPCID.Hellhound] = .05f;
                        }
                        if(cavern)
                        {
                            pool[NPCID.Poltergeist] = .05f;
                        }
                        
                        

                        
                    }

                    if(NPC.downedPlantBoss)
                    {
                        if(surface && night && Fargowiltas.NormalSpawn(spawnInfo))
                        {
                            pool[NPCID.SkeletonSniper] = .05f;
                            pool[NPCID.SkeletonCommando] = .05f;
                            pool[NPCID.TacticalSkeleton] = .05f;
                        }
                        if(!surface)
                        {
                            pool[NPCID.DiabolistRed] = .005f;
                            pool[NPCID.DiabolistWhite] = .005f;
                            pool[NPCID.Necromancer] = .005f;
                            pool[NPCID.NecromancerArmored] = .005f;
                            pool[NPCID.RaggedCaster] = .005f;
                            pool[NPCID.RaggedCasterOpenCoat] = .005f;
                        }
                    }

                    if(meteor && NPC.downedGolemBoss)
                    {
                        pool[NPCID.SolarCorite] = .01f;
                        pool[NPCID.SolarSroller] = .01f;
                    }

                    if(sky)
                    {
                        pool[NPCID.SolarCrawltipedeHead] = .01f;
                        pool[NPCID.VortexHornetQueen] = .01f;
                        pool[NPCID.NebulaBrain] = .01f;
                        pool[NPCID.StardustJellyfishBig] = .01f;
                    }
                }
			}
		
			
		
		}
		
		public override bool PreNPCLoot (NPC npc)
		{
            if(!Main.hardMode && (npc.type == NPCID.Medusa || npc.type == NPCID.IchorSticker || npc.type == NPCID.SeekerHead || npc.type == NPCID.Mimic || npc.type == NPCID.AngryNimbus))
            {
                return false;
            }

			return true;
		}

        private bool firstLoot = true;

		public override void NPCLoot(NPC npc)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if(modPlayer.PlatinumEnchant && firstLoot)
            {
                bool midas = npc.HasBuff(BuffID.Midas);
                int chance = 10;
                int bonus = 3;

                if(midas)
                {
                    chance/= 2;
                    bonus *= 2;
                }

                if(Main.rand.Next(chance) == 0)
                {
                    firstLoot = false;
                    for (int i = 0; i < bonus; i++)
                    {
                        npc.NPCLoot();
                        NPC.killCount[Item.NPCtoBanner(npc.BannerID())]--;
                    }
                }
            }

            firstLoot = false;
        }
		
		public override bool CheckDead(NPC npc)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if(TimeFrozen)
            {
                npc.life = 1;
                return false;
            }

            if (FargoWorld.MasochistMode)
            {
                switch (masoDeathAI)
                {
                    case 0:
                        break;

                    case 1: //drippler
                        int[] eyes = new int[4];

                        for (int i = 0; i < 4; i++)
                        {
                            eyes[i] = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.DemonEye);
                            Main.npc[eyes[i]].velocity = new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
                        }
                        break;

                    case 2: //goblin peon/warrior
                        Projectile ball = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-5, 6), -5), ProjectileID.SpikyBall, 15, 0, Main.myPlayer);
                        ball.hostile = true;
                        ball.friendly = true;
                        ball.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                        break;

                    case 3: //angry bones
                        if (Main.rand.Next(10) == 0)
                        {
                            NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.CursedSkull);
                        }
                        break;

                    case 4: //dungeon slime
                        if (NPC.downedPlantBoss)
                        {
                            int paladin = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.Paladin);
                            Main.npc[paladin].scale = .65f;
                            Main.npc[paladin].defense /= 2;
                        }
                        break;

                    case 5: //yellow slime
                        for (int i = 0; i < 2; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                            Main.npc[spawn].SetDefaults(NPCID.PurpleSlime);
                            Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                            Main.npc[spawn].velocity.Y = npc.velocity.Y;

                            NPC spawn2 = Main.npc[spawn];
                            spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                            NPC spawn3 = Main.npc[spawn];
                            spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                            Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                        }
                        break;

                    case 6: //purple slime
                        for (int i = 0; i < 2; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                            Main.npc[spawn].SetDefaults(NPCID.RedSlime);
                            Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                            Main.npc[spawn].velocity.Y = npc.velocity.Y;

                            NPC spawn2 = Main.npc[spawn];
                            spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                            NPC spawn3 = Main.npc[spawn];
                            spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                            Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                        }
                        break;

                    case 7: //red slime
                        for (int i = 0; i < 2; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);
                            Main.npc[spawn].SetDefaults(NPCID.GreenSlime);
                            Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                            Main.npc[spawn].velocity.Y = npc.velocity.Y;

                            NPC spawn2 = Main.npc[spawn];
                            spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                            NPC spawn3 = Main.npc[spawn];
                            spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                            Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                        }
                        break;


                    case 8: //dr manfly
                        for (int i = 0; i < 10; i++)
                        {
                            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-5, 6), Main.rand.Next(-5, 6)), ProjectileID.DrManFlyFlask, npc.damage * 2, 1f, Main.myPlayer);
                        }
                        break;

                    case 9: //eye of cthulhu
                        if (FargoWorld.EyeCount < 280)
                        {
                            FargoWorld.EyeCount++;
                        }
                        break;

                    case 10: //king slime
                        if (FargoWorld.SlimeCount < 280)
                        {
                            FargoWorld.SlimeCount++;
                        }
                        break;

                    case 11: //eater of worlds head
                        if (FargoWorld.EaterCount < 280 && !NPC.AnyNPCs(NPCID.EaterofWorldsBody) && !NPC.AnyNPCs(NPCID.EaterofWorldsTail))
                        {
                            FargoWorld.EaterCount++;
                        }
                        break;

                    case 12: //brain of cthulhu
                        if (FargoWorld.BrainCount < 280)
                        {
                            FargoWorld.BrainCount++;
                        }
                        break;

                    case 13: //queen bee
                        if (FargoWorld.BeeCount < 280)
                        {
                            FargoWorld.BeeCount++;
                        }
                        break;

                    case 14: //skeletron head
                        if (FargoWorld.SkeletronCount < 280)
                        {
                            FargoWorld.SkeletronCount++;
                        }
                        break;

                    case 15: //wall of flesh
                        if (FargoWorld.WallCount < 280)
                        {
                            FargoWorld.WallCount++;
                        }
                        break;

                    case 16: //the destroyer
                        if (FargoWorld.DestroyerCount < 120)
                        {
                            FargoWorld.DestroyerCount++;
                        }
                        break;

                    case 17: //skeletron prime
                        if (npc.ai[1] != 2f)
                        {
                            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
                            npc.life = 100;

                            npc.defDefense = 9999;
                            npc.defDamage = 1000;
                            npc.defense = 9999;
                            npc.damage = 1000;

                            for (int k = 0; k < npc.buffImmune.Length; k++)
                            {
                                npc.buffImmune[k] = true;
                            }

                            while (npc.buffTime[0] != 0)
                            {
                                npc.DelBuff(0);
                            }

                            npc.ai[1] = 2f;
                            npc.netUpdate = true;
                            return false;
                        }
                        else if (FargoWorld.PrimeCount < 120)
                        {
                            FargoWorld.PrimeCount++;
                        }
                        break;

                    case 18: //retinazer
                        if (FargoWorld.TwinsCount < 120 && !NPC.AnyNPCs(NPCID.Spazmatism))
                        {
                            FargoWorld.TwinsCount++;
                        }
                        break;

                    case 19: //spazmatism
                        if (FargoWorld.TwinsCount < 120 && !NPC.AnyNPCs(NPCID.Retinazer))
                        {
                            FargoWorld.TwinsCount++;
                        }
                        break;

                    case 20: //plantera
                        if (FargoWorld.PlanteraCount < 120)
                        {
                            FargoWorld.PlanteraCount++;
                        }
                        break;

                    case 21: //golem
                        if (FargoWorld.GolemCount < 120)
                        {
                            FargoWorld.GolemCount++;
                        }
                        break;

                    case 22: //fishron
                        if (FargoWorld.FishronCount < 120)
                        {
                            FargoWorld.FishronCount++;
                        }
                        break;

                    case 23: //cultistboss
                        if (FargoWorld.CultistCount < 120)
                        {
                            FargoWorld.CultistCount++;
                        }
                        break;

                    case 24: //moon lord core
                        if (FargoWorld.MoonlordCount < 120)
                        {
                            FargoWorld.MoonlordCount++;
                        }
                        break;

                    case 25: //rainbow slime
                        masoDeathAI = 0;
                        npc.active = false;
                        Main.PlaySound(npc.DeathSound);

                        for (int i = 0; i < 4; i++)
                        {
                            int slimeIndex = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.RainbowSlime);
                            NPC slime = Main.npc[slimeIndex];

                            Vector2 center = slime.Center;
                            slime.width /= (int)slime.scale;
                            slime.height /= (int)slime.scale;
                            slime.scale = 1f;
                            slime.Center = center;

                            slime.lifeMax /= 5;
                            slime.life = slime.lifeMax;

                            slime.GetGlobalNPC<FargoGlobalNPC>().masoDeathAI = 0;
                            slime.velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                        }

                        for (int i = 0; i < 20; i++)
                        {
                            int num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, DustID.RainbowMk2, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 5f);
                            Main.dust[num469].noGravity = true;
                            Main.dust[num469].velocity *= 2f;
                            num469 = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width, npc.height, DustID.RainbowMk2, -npc.velocity.X * 0.2f, -npc.velocity.Y * 0.2f, 100, default(Color), 2f);
                            Main.dust[num469].velocity *= 2f;
                        }
                        return false;

                    default:
                        break;
                }
            }
            
            return true;
		}

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            if (FargoWorld.MasochistMode)
            {
                switch (masoHurtAI)
                {
                    case 0:
                        break;

                    case 1:
                        masoHurtAI = 0;
                        npc.Opacity *= 25;
                        break;

                    case 2:
                        player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was impaled by a Giant Shelly."), damage / 4, 0);
                        break;

                    default:
                        break;
                }
            }
        }

        public override void ModifyHitByProjectile (NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            Player player = Main.player[Main.myPlayer];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (FargoWorld.MasochistMode)
            {
                switch (masoHurtAI)
                {
                    case 0:
                        break;

                    case 1:
                        masoHurtAI = 0;
                        npc.Opacity *= 25;
                        break;

                    default:
                        break;
                }
            }

            //bees ignore defense
            if (modPlayer.BeeEnchant && projectile.type == ProjectileID.GiantBee)
            {
                damage = (int)(damage + npc.defense * .5);
            }
		}

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            FargoPlayer modPlayer = target.GetModPlayer<FargoPlayer>(mod);

            if (FargoWorld.MasochistMode)
            {
                switch (npc.type)
                {
                    case NPCID.BlueSlime:
                        switch (npc.netID)
                        {
                            case NPCID.BlackSlime:
                                target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                                target.AddBuff(BuffID.Darkness, Main.rand.Next(120, 1200));
                                break;

                            case NPCID.BabySlime:
                                target.AddBuff(BuffID.Slimed, Main.rand.Next(240));
                                break;

                            case NPCID.Pinky:
                                target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                                break;

                            default:
                                target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                                break;
                        }
                        break;

                    case NPCID.SpikedIceSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 600));
                        break;

                    case NPCID.SpikedJungleSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Venom, Main.rand.Next(60, 600));
                        break;

                    case NPCID.MotherSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(mod.BuffType("Antisocial"), Main.rand.Next(1800));
                        break;

                    case NPCID.LavaSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(90, 900));
                        break;

                    case NPCID.DungeonSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Blackout, Main.rand.Next(120, 1200));
                        break;

                    case NPCID.KingSlime:
                        target.AddBuff(BuffID.Slimed, 601);
                        target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300, 600));
                        if(Main.rand.Next(10) == 0)
                        {
                            target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(30, 90));
                        }
                        break;

                    case NPCID.ToxicSludge:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300, 600));
                        break;

                    case NPCID.CorruptSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(120, 1200));
                        break;

                    case NPCID.Crimslime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(600));
                        break;

                    case NPCID.Gastropod:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Fused"), 1800);
                        break;

                    case NPCID.IlluminantSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(600));
                        break;

                    case NPCID.RainbowSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(mod.BuffType("FlamesoftheUniverse"), Main.rand.Next(300, 600));
                        break;

                    case NPCID.DemonEye:
                        if (NPC.downedBoss2 && Main.rand.Next(4) == 0 && !target.HasBuff(BuffID.Stoned))
                        {
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(30, 120));
                        }
                        break;

                    case NPCID.DemonEyeOwl:
                        if (NPC.downedBoss2 && Main.rand.Next(4) == 0 && !target.HasBuff(BuffID.Stoned))
                        {
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(30, 120));
                        }
                        break;

                    case NPCID.DemonEyeSpaceship:
                        if (NPC.downedBoss2 && Main.rand.Next(4) == 0 && !target.HasBuff(BuffID.Stoned))
                        {
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(30, 120));
                        }
                        break;

                    case NPCID.EaterofSouls:
                        if (Main.rand.Next(3) == 0)
                        {
                            target.AddBuff(BuffID.Weak, Main.rand.Next(600, 1800));
                        }
                        break;

                    case NPCID.Crimera:
                        if (Main.rand.Next(3) == 0)
                        {
                            target.AddBuff(BuffID.Weak, Main.rand.Next(600, 1800));
                        }
                        break;

                    case NPCID.EyeofCthulhu:
                        if(!target.HasBuff(mod.BuffType("Berserked")))
                        {
                            target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(60, 600));
                        }
                        break;

                    case NPCID.ServantofCthulhu:
                        target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(300));
                        break;

                    case NPCID.QueenBee:
                        target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(60, 240));
                        break;

                    case NPCID.WallofFlesh:
                        if (!target.HasBuff(mod.BuffType("Unstable")))
                        {
                            target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(60, 240));
                        }
                        break;

                    case NPCID.WallofFleshEye:
                        if (!target.HasBuff(mod.BuffType("Unstable")))
                        {
                            target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(60, 240));
                        }
                        break;

                    case NPCID.TheHungry:
                        target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(60, 120));
                        break;

                    case NPCID.TheHungryII:
                        target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(60, 120));
                        break;

                    case NPCID.EaterofWorldsHead:
                        target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 600));
                        break;

                    case NPCID.EaterofWorldsBody:
                        target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 600));
                        break;

                    case NPCID.EaterofWorldsTail:
                        target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 600));
                        break;

                    case NPCID.CursedSkull:
                        target.AddBuff(BuffID.Cursed, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Snatcher:
                        if (Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 1200));
                        }
                        break;

                    case NPCID.ManEater:
                        if (target.statLife < 100)
                        {
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Man Eater."), 999, 0);
                        }
                        break;

                    case NPCID.TombCrawlerHead:
                        if (target.statLife < 60)
                        {
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Tomb Crawler."), 999, 0);
                        }
                        break;

                    case NPCID.DevourerHead:
                        target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(90, 900));
                        if (target.statLife < 50)
                        {
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Devourer."), 999, 0);
                        }
                        break;

                    case NPCID.AngryTrapper:
                        if (target.statLife < 180)
                        {
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by an Angry Trapper."), 999, 0);
                        }
                        break;

                    case NPCID.SkeletronHead:
                        if (Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(150, 300));
                        }
                        break;

                    case NPCID.SkeletronHand:
                        if (Main.rand.Next(4) == 0)
                        {
                            target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(60));
                        }
                        break;

                    case NPCID.CaveBat:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(600, 1200));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.Hellbat:
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.JungleBat:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.IceBat:
                        if(target.HasBuff(BuffID.Chilled) && !target.HasBuff(BuffID.Frozen))
                        {
                            target.AddBuff(BuffID.Frozen, Main.rand.Next(120));
                        }
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.Lavabat:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        target.AddBuff(BuffID.CursedInferno, Main.rand.Next(300, 600));
                        break;

                    case NPCID.GiantBat:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        if(Main.rand.Next(3) == 0)
                        {
                            target.AddBuff(BuffID.Confused, Main.rand.Next(600));
                        }
                        break;

                    case NPCID.IlluminantBat:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));

                        if (Main.rand.Next(4) == 0)
                        {
                            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(180, 1800));
                        }
                        break;

                    case NPCID.GiantFlyingFox:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(600));
                        break;

                    case NPCID.VampireBat:
                        target.AddBuff(BuffID.Darkness, Main.rand.Next(900, 1800));
                        target.AddBuff(BuffID.Weak, Main.rand.Next(900, 1800));
                        target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        break;

                    case NPCID.Vampire:
                        target.AddBuff(BuffID.Darkness, Main.rand.Next(900, 1800));
                        target.AddBuff(BuffID.Weak, Main.rand.Next(900, 1800));
                        target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        break;

                    case NPCID.SnowFlinx:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(60, 600));
                        break;

                    case NPCID.Piranha:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Medusa:
                        if(!target.HasBuff(BuffID.Stoned))
                        {
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(30, 120));
                        }
                        target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(60, 180));
                        break;

                    case NPCID.SpikeBall:
                        target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(300, 1200));
                        break;

                    case NPCID.BlazingWheel:
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Shark:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(60, 300));
                        break;

                    case NPCID.BlueJellyfish:
                        if (target.wet && Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(BuffID.Electrified, Main.rand.Next(120, 240));
                        }
                        break;

                    case NPCID.PinkJellyfish:
                        if (target.wet && Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(BuffID.Electrified, Main.rand.Next(120, 240));
                        }
                        break;

                    case NPCID.GraniteFlyer:
                        if (Main.rand.Next(8) == 0 && !target.HasBuff(BuffID.Stoned))
                        {
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(120));
                        }
                        break;

                    case NPCID.GraniteGolem:
                        if (Main.rand.Next(2) == 0 && !target.HasBuff(BuffID.Stoned))
                        {
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(120));
                        }
                        break;

                    case NPCID.LeechHead:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 900));
                        break;

                    case NPCID.AnomuraFungus:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(300, 600));
                        break;

                    case NPCID.WaterSphere:
                        if (Main.rand.Next(3) == 0)
                        {
                            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(900));
                        }
                        break;

                    case NPCID.GiantShelly:
                        target.AddBuff(BuffID.Slow, Main.rand.Next(120, 600));
                        break;

                    case NPCID.GiantShelly2:
                        target.AddBuff(BuffID.Slow, Main.rand.Next(120, 600));
                        break;

                    case NPCID.Squid:
                        target.AddBuff(BuffID.Obstructed, Main.rand.Next(150, 300));
                        break;

                    case NPCID.BloodZombie:
                        if (Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(240));     
                        }
                        break;

                    case NPCID.Drippler:
                        if (Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 600));
                        }
                        break;

                    case NPCID.ChaosBall:
                        target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Tumbleweed:
                        target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(120, 480));
                        break;

                    case NPCID.CorruptBunny:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.CrimsonBunny:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.CorruptGoldfish:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.CrimsonGoldfish:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.CorruptPenguin:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.CrimsonPenguin:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.MothronSpawn:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.PigronCorruption:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.PigronCrimson:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;
                    case NPCID.PigronHallow:
                        target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(600));
                        break;

                    case NPCID.FaceMonster:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(1800));
                        break;

                    case NPCID.Harpy:
                        if (Main.rand.Next(4) == 0)
                        {
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(60, 480));
                        }
                        break;

                    case NPCID.SeaSnail:
                        target.AddBuff(BuffID.OgreSpit, Main.rand.Next(600));
                        break;

                    case NPCID.BrainofCthulhu:
                        target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(30, 90));
                        target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(60, 300));
                        target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300, 600));
                        target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(60, 240));
                        break;

                    case NPCID.Creeper:
                        switch (Main.rand.Next(15))
                        {
                            case 0:
                                target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(60, 240));
                                break;

                            case 1:
                                target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(60, 240));
                                break;

                            case 2:
                                target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(60, 240));
                                break;

                            case 3:
                                target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 240));
                                break;

                            case 4:
                                target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(60, 240));
                                break;

                            default:
                                break;
                        }
                        break;

                    case NPCID.SwampThing:
                        target.AddBuff(BuffID.OgreSpit, Main.rand.Next(60, 300));
                        break;

                    case NPCID.Frankenstein:
                        target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(120, 600));
                        break;

                    case NPCID.Reaper:
                        if (Main.rand.Next(5) == 0 && !target.HasBuff(mod.BuffType("MarkedforDeath")))
                        {
                            target.AddBuff(mod.BuffType("MarkedforDeath"), 1800);
                        }
                        target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        break;

                    case NPCID.Butcher:
                        target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(120, 300));
                        break;

                    case NPCID.ThePossessed:
                        if (Main.rand.Next(2) == 0)
                        {
                            target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(300));
                        }
                        break;

                    case NPCID.Wolf:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(3000));
                        break;

                    case NPCID.Werewolf:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(3000));
                        break;

                    //all armored bones
                    case 269: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 270: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 271: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 272: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 273: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 274: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 275: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 276: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 277: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 278: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 279: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;
                    case 280: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120)); break;

                    case NPCID.GiantTortoise:
                        target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(120, 300));
                        break;

                    case NPCID.IceTortoise:
                        target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(120, 300));

                        if(Main.rand.Next(4) == 0)
                        {
                            target.AddBuff(BuffID.Frozen, Main.rand.Next(120));
                        }
                        break;

                    //CULTIST OP
                    case NPCID.AncientDoom:
                        if (!target.HasBuff(mod.BuffType("MarkedforDeath")))
                        {
                            target.AddBuff(mod.BuffType("MarkedforDeath"), Main.rand.Next(600, 1200));
                        }
                        break;
                    case NPCID.AncientLight:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(120, 240));
                        break;
                    case NPCID.CultistBossClone:
                        target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(60, 240));
                        break;

                    case NPCID.MossHornet:
                        target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(150, 300));
                        break;

                    case NPCID.Paladin:
                        target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(480, 720));
                        break;

                    case NPCID.DukeFishron:
                        target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(480, 720));
                        break;

                    case NPCID.Hellhound:
                        target.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(900));
                        target.AddBuff(BuffID.Obstructed, Main.rand.Next(300));
                        break;

                    case NPCID.Mimic:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 180));
                        break;
                    case NPCID.PresentMimic:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 180));
                        break;
                    case NPCID.BigMimicCorruption:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 180));
                        break;
                    case NPCID.BigMimicCrimson:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 180));
                        break;
                    case NPCID.BigMimicHallow:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 180));
                        break;
                    case NPCID.BigMimicJungle:
                        target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 180));
                        break;

                    case NPCID.RuneWizard:
                        target.AddBuff(BuffID.Cursed, Main.rand.Next(480));
                        break;

                    case NPCID.Nutcracker:
                        target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(90, 900));
                        break;
                    case NPCID.NutcrackerSpinning:
                        target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(90, 900));
                        break;

                    case NPCID.Wraith:
                        target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        break;

                    case NPCID.Plantera:
                        if (target.HasBuff(mod.BuffType("Infested")))
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(720, 1080));
                        }
                        else
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(240, 480));
                        }
                        break;

                    case NPCID.PlanterasHook:
                        if (target.HasBuff(mod.BuffType("Infested")))
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(240, 480));
                        }
                        else
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 180));
                        }
                        break;

                    case NPCID.PlanterasTentacle:
                        if (target.HasBuff(mod.BuffType("Infested")))
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(240, 480));
                        }
                        else
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 180));
                        }
                        break;

                    case NPCID.Spore:
                        if (target.HasBuff(mod.BuffType("Infested")))
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(240, 480));
                        }
                        else
                        {
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 180));
                        }
                        break;

                    case NPCID.ChaosElemental:
                        if (Main.rand.Next(10) == 0)
                        {
                            target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(900));
                        }
                        break;

                    case NPCID.Flocko:
                        target.AddBuff(BuffID.Chilled, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 600));
                        break;

                    case NPCID.GoblinPeon:
                        if (Main.hardMode && Main.rand.Next(5) == 0)
                        {
                            target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        }
                        break;
                    case NPCID.PirateDeckhand:
                        if (Main.hardMode && Main.rand.Next(5) == 0)
                        {
                            target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        }
                        break;
                    case NPCID.GrayGrunt:
                        if (Main.hardMode && Main.rand.Next(5) == 0)
                        {
                            target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(450, 900));
                        }
                        break;

                    case NPCID.Zombie:
                        target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(30, 300));
                        break;

                    case NPCID.Corruptor:
                        target.AddBuff(BuffID.Weak, Main.rand.Next(60, 7200));
                        break;

                    case NPCID.Mummy:
                        if (!target.HasBuff(BuffID.Webbed))
                        {
                            target.AddBuff(BuffID.Webbed, Main.rand.Next(30, 300));
                        }
                        break;
                    case NPCID.LightMummy:
                        if (!target.HasBuff(BuffID.Webbed))
                        {
                            target.AddBuff(BuffID.Webbed, Main.rand.Next(30, 300));
                        }
                        break;
                    case NPCID.DarkMummy:
                        if (!target.HasBuff(BuffID.Webbed))
                        {
                            target.AddBuff(BuffID.Webbed, Main.rand.Next(30, 300));
                        }
                        break;

                    case NPCID.Derpling:
                        target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(300, 600));
                        break;

                    case NPCID.SkeletronPrime:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.PrimeCannon:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.PrimeLaser:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.PrimeSaw:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.PrimeVice:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.TheDestroyer:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.TheDestroyerBody:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.TheDestroyerTail:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.Spazmatism:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;
                    case NPCID.Retinazer:
                        target.AddBuff(mod.BuffType("ClippedWings"), 30);
                        break;

                    case NPCID.MoonLordLeechBlob:
                        target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(600, 1200));
                        break;

                    case NPCID.BlackRecluse:
                        target.AddBuff(BuffID.Venom, Main.rand.Next(150, 900));
                        break;

                    case NPCID.DesertBeast:
                        target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(300, 900));
                        break;

                    case NPCID.Krampus:
                        break;

                    default:
                        break;
                }
            }
           
            if(target.HasBuff(mod.BuffType("ShellHide")))
            {
                damage *= 2;
            }

            if(SqueakyToy)
            {
                damage = 1;
                modPlayer.Squeak(target.Center);
            }
        }


        public override bool StrikeNPC (NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
            bool retValue = true;

			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
			if (FargoWorld.MasochistMode)
            {
                NPC damagedNpc = npc;
                //basically, is this a segment?
                if (npc.realLife >= 0)
                {
                    damagedNpc = Main.npc[damagedNpc.realLife];
                }

                if(damagedNpc.type == NPCID.SkeletronPrime)
                {
                    int armCount = 0;
                    bool[] arms = { NPC.AnyNPCs(NPCID.PrimeCannon), NPC.AnyNPCs(NPCID.PrimeLaser), NPC.AnyNPCs(NPCID.PrimeSaw), NPC.AnyNPCs(NPCID.PrimeVice) };

                    for(int i = 0; i < 4; i++)
                    {
                        if(arms[i])
                        {
                            armCount++;
                        }
                    }

                    switch (armCount)
                    {
                        case 4:
                            damage *= .05;
                            break;
                        case 3:
                            damage *= .10;
                            break;
                        case 2:
                            damage *= .25;
                            break;
                        case 1:
                            damage *= .50;
                            break;
                        default:
                            break;
                    }

                }

                ResetRegenTimer(damagedNpc);
			}

			if(modPlayer.UniverseEffect)
			{
				if(crit)
				{
					damage *= 5;
                    retValue = false;
				}
			}

            if(modPlayer.RedEnchant)
            {
                switch (npc.life / npc.lifeMax * 100)
                {
                    case 50:
                        damage *= 1.1;
                        break;
                    case 25:
                        damage *= 1.4;
                        break;
                    case 10:
                        damage *= 2;
                        break;
                }
            }
			
			if(crit && modPlayer.ShroomEnchant && !modPlayer.TerrariaSoul && modPlayer.IsStandingStill)
			{
			    damage *= 4;
                retValue = false;
			}
			
			if(modPlayer.FirstStrike && npc.lifeMax == npc.life)
			{
                crit = true;
                retValue = false;
            }
			
			//normal damage calc
			return retValue;
		}
		
		public override void OnHitByItem (NPC npc, Player player, Item item, int damage, float knockback, bool crit)
		{
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
            if(modPlayer.ValhallaEnchant && Soulcheck.GetValue("Valhalla Knockback") && npc.type != NPCID.TargetDummy && npc.knockBackResist < 1)
            {
                npc.knockBackResist += .1f;
            }
		}

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            FargoPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<FargoPlayer>(mod);

            //spears
            if(modPlayer.ValhallaEnchant && Soulcheck.GetValue("Valhalla Knockback") && (projectile.aiStyle == 19 || modPlayer.WillForce) && npc.type != NPCID.TargetDummy && npc.knockBackResist < 1)
            {
                npc.knockBackResist += .1f;
            }
            /*else
            {
                NPC n = new NPC();
                n.SetDefaults(npc.type);
                npc.knockBackResist = n.knockBackResist;
            }*/
        }
    }
}