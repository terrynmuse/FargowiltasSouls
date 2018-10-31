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

using FargowiltasSouls.Buffs.Masomode;

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
        public byte masoState = 0;
        public bool[] masoBool = new bool[4];
        public bool Transform = false;
        public bool dropLoot = true;
        public bool PaladinsShield = false;
        public int RegenTimer = 0;
        public int Counter = 0;
        public int Timer = 600;
        public byte SharkCount = 0;
        private static MethodInfo _startSandstormMethod;

        public static int slimeBoss = -1;
        public static int eyeBoss = -1;
        public static int eaterBoss = -1;
        public static int brainBoss = -1;
        public static int beeBoss = -1;
        public static int skeleBoss = -1;
        public static int wallBoss = -1;
        public static int retiBoss = -1;
        public static int spazBoss = -1;
        public static int destroyBoss = -1;
        public static int primeBoss = -1;
        public static int fishBoss = -1;
        public static int cultBoss = -1;
        public static int moonBoss = -1;

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
            PaladinsShield = false;
            //BLACK SLIMES
            //npc.color = default(Color);
        }

        public override void SetDefaults(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                ResetRegenTimer(npc);

                switch (npc.type)
                {
                    case NPCID.Salamander:
                    case NPCID.Salamander2:
                    case NPCID.Salamander3:
                    case NPCID.Salamander4:
                    case NPCID.Salamander5:
                    case NPCID.Salamander6:
                    case NPCID.Salamander7:
                    case NPCID.Salamander8:
                    case NPCID.Salamander9: masoHurtAI = 1; npc.Opacity /= 25; break;

                    case NPCID.GiantShelly:
                    case NPCID.GiantShelly2: masoHurtAI = 2; break;

                    case NPCID.BoneLee: masoHurtAI = 3; break;

                    case NPCID.ForceBubble: masoHurtAI = 4; break;

                    case NPCID.SolarSolenian: masoHurtAI = 5; npc.knockBackResist = 0f; break;

                    case NPCID.EaterofWorldsBody:
                    case NPCID.EaterofWorldsTail: masoHurtAI = 6; break;
                    //case NPCID.TheDestroyerBody:
                    //case NPCID.TheDestroyerTail: masoHurtAI = 6; break;

                    case NPCID.GolemFistLeft:
                    case NPCID.GolemFistRight: masoHurtAI = 7; npc.scale += 0.5f; break;

                    case NPCID.SkeletronPrime: masoHurtAI = 8; npc.trapImmune = true; break;

                    case NPCID.BrainofCthulhu: masoHurtAI = 9; break;

                    case NPCID.IceTortoise: masoHurtAI = 10; break;

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
                        npc.lavaImmune = true;
                        npc.buffImmune[BuffID.OnFire] = true;
                        if (!NPC.downedBoss3)
                        {
                            npc.noTileCollide = false;
                        }
                        break;

                    case NPCID.Golem:
                        npc.lifeMax *= 2;
                        npc.life = npc.lifeMax;
                        break;

                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeVice:
                    case NPCID.PrimeSaw:
                        npc.trapImmune = true;
                        break;

                    case NPCID.MoonLordLeechBlob:
                        npc.lifeMax *= 10;
                        npc.life = npc.lifeMax;
                        break;

                    case NPCID.StardustCellSmall:
                        npc.defense = 80;
                        break;

                    case NPCID.Parrot:
                        npc.noTileCollide = true;
                        break;

                    case NPCID.Medusa:
                    case NPCID.IchorSticker:
                    case NPCID.Mimic:
                    case NPCID.SeekerHead:
                    case NPCID.AngryNimbus:
                        dropLoot = Main.hardMode;
                        break;

                    case NPCID.SolarSroller:
                        npc.lifeMax *= 3;
                        npc.life = npc.lifeMax;
                        npc.defDefense *= 2;
                        npc.defense *= 2;
                        npc.scale += 0.5f;
                        break;

                    case NPCID.Probe:
                        dropLoot = false;
                        break;

                    case NPCID.Sharkron:
                    case NPCID.Sharkron2:
                        npc.lifeMax *= 5;
                        npc.life = npc.lifeMax;
                        npc.buffImmune[BuffID.OnFire] = true;
                        npc.lavaImmune = true;
                        break;

                    case NPCID.ServantofCthulhu:
                        npc.lifeMax *= 5;
                        npc.life = npc.lifeMax;
                        break;

                    case NPCID.VileSpit:
                        npc.dontTakeDamage = true;
                        break;

                    case NPCID.TheHungry:
                        npc.lifeMax *= 4;
                        npc.life = npc.lifeMax;
                        npc.knockBackResist = 0f;
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
                    case NPCID.CyanBeetle:
                    case NPCID.LacBeetle:
                        masoAI = 3;
                        break;

                    case NPCID.EnchantedSword:
                    case NPCID.CursedHammer:
                    case NPCID.CrimsonAxe:
                        masoAI = 4;
                        break;

                    case NPCID.Ghost:
                        masoAI = 5;
                        break;

                    case NPCID.Mummy:
                    case NPCID.DarkMummy:
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
                    case NPCID.SandShark:
                    case NPCID.SandsharkCorrupt:
                    case NPCID.SandsharkCrimson:
                    case NPCID.SandsharkHallow:
                        masoAI = 21;
                        break;

                    case NPCID.BlackRecluse:
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

                    case NPCID.KingSlime:
                        masoAI = 31;
                        break;

                    case NPCID.EaterofWorldsHead:
                        masoAI = 32;
                        break;

                    case NPCID.QueenBee:
                        masoAI = 33;
                        break;

                    case NPCID.SkeletronHead:
                        masoAI = 34;
                        break;

                    case NPCID.WallofFlesh:
                        masoAI = 35;
                        break;

                    case NPCID.TheDestroyer:
                        masoAI = 36;
                        npc.lifeMax = npc.lifeMax * 3 / 2;
                        npc.life = npc.lifeMax;
                        break;

                    case NPCID.DukeFishron:
                        masoAI = 37;
                        break;

                    case NPCID.MoonLordCore:
                        masoAI = 38;
                        break;

                    case NPCID.Splinterling:
                        masoAI = 39;
                        break;

                    case NPCID.Golem:
                        masoAI = 40;
                        npc.trapImmune = true;
                        break;

                    case NPCID.GolemHeadFree:
                        masoAI = 41;
                        npc.trapImmune = true;
                        break;

                    case NPCID.GolemFistLeft:
                    case NPCID.GolemFistRight:
                    case NPCID.GolemHead:
                        masoAI = 42;
                        npc.trapImmune = true;
                        break;

                    case NPCID.FlyingSnake:
                        masoAI = 43;
                        break;

                    case NPCID.Lihzahrd:
                    case NPCID.LihzahrdCrawler:
                        masoAI = 44;
                        break;

                    case NPCID.StardustCellSmall:
                        masoAI = 45;
                        break;

                    case NPCID.CultistDragonBody1:
                    case NPCID.CultistDragonBody2:
                    case NPCID.CultistDragonBody3:
                    case NPCID.CultistDragonBody4:
                    case NPCID.CultistDragonTail:
                        masoAI = 46;
                        break;

                    case NPCID.MothronSpawn:
                        masoAI = 47;
                        break;

                    case NPCID.Pumpking:
                        masoAI = 48;
                        break;

                    case NPCID.SolarCorite:
                        masoAI = 49;
                        break;

                    case NPCID.NebulaHeadcrab:
                        masoAI = 50;
                        break;

                    case NPCID.Plantera:
                        masoAI = 51;
                        break;

                    case NPCID.PrimeSaw:
                        masoAI = 52;
                        break;

                    case NPCID.IceQueen:
                        masoAI = 53;
                        break;

                    case NPCID.Eyezor:
                        masoAI = 54;
                        break;

                    case NPCID.SkeletronPrime:
                        masoAI = 55;
                        Timer = 0;
                        break;

                    case NPCID.VortexHornetQueen:
                        masoAI = 56;
                        break;

                    case NPCID.SolarCrawltipedeTail:
                        masoAI = 57;
                        npc.trapImmune = true;
                        break;

                    case NPCID.Nailhead:
                        masoAI = 58;
                        break;

                    case NPCID.VortexRifleman:
                        masoAI = 59;
                        break;

                    case NPCID.ElfCopter:
                        masoAI = 60;
                        break;

                    case NPCID.TheDestroyerBody:
                    case NPCID.TheDestroyerTail:
                        masoAI = 61;
                        break;

                    case NPCID.TacticalSkeleton:
                        masoAI = 62;
                        break;

                    case NPCID.SkeletonSniper:
                        masoAI = 63;
                        break;

                    case NPCID.SkeletonArcher:
                        masoAI = 64;
                        break;

                    case NPCID.SkeletonCommando:
                        masoAI = 65;
                        break;

                    case NPCID.ElfArcher:
                        masoAI = 66;
                        break;

                    case NPCID.PirateCrossbower:
                        masoAI = 67;
                        break;

                    case NPCID.PirateDeadeye:
                        masoAI = 68;
                        break;

                    case NPCID.PirateCaptain:
                        masoAI = 69;
                        break;

                    case NPCID.SolarGoop:
                        masoAI = 70;
                        npc.noTileCollide = true;
                        npc.buffImmune[BuffID.OnFire] = true;
                        break;
                        
                    case NPCID.BrainofCthulhu:
                        masoAI = 71;
                        npc.scale += 0.25f;
                        break;

                    case NPCID.Creeper:
                        masoAI = 72;
                        break;
                        
                    case NPCID.Pixie:
                        masoAI = 73;
                        break;

                    case NPCID.Clown:
                        masoAI = 74;
                        npc.lifeMax *= 5;
                        npc.life = npc.lifeMax;
                        break;

                    case NPCID.Paladin:
                        masoAI = 75;
                        break;

                    case NPCID.Mimic:
                        masoAI = 76;
                        break;

                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeVice:
                        masoAI = 77;
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
                    case NPCID.GoblinWarrior:
                    case NPCID.GoblinArcher:
                    case NPCID.GoblinScout:
                    case NPCID.GoblinSorcerer:
                    case NPCID.GoblinThief:
                        masoDeathAI = 2;
                        break;

                    case NPCID.AngryBones:
                    case NPCID.AngryBonesBig:
                    case NPCID.AngryBonesBigHelmet:
                    case NPCID.AngryBonesBigMuscle:
                        masoDeathAI = 3;
                        break;

                    case NPCID.DungeonSlime:
                        masoDeathAI = 4;
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

                    case NPCID.FlyingSnake:
                        masoDeathAI = 26;
                        break;

                    case NPCID.Lihzahrd:
                    case NPCID.LihzahrdCrawler:
                        masoDeathAI = 27;
                        break;

                    case NPCID.StardustJellyfishBig:
                    case NPCID.StardustSoldier:
                    case NPCID.StardustSpiderBig:
                    case NPCID.StardustWormHead:
                        masoDeathAI = 28;
                        break;

                    case NPCID.SolarSpearman:
                        masoDeathAI = 29;
                        break;

                    case NPCID.NebulaBrain:
                        masoDeathAI = 30;
                        break;

                    case NPCID.VortexHornet:
                        masoDeathAI = 31;
                        break;

                    case NPCID.NebulaHeadcrab:
                        masoDeathAI = 32;
                        break;

                    case NPCID.SkeletronHand:
                        masoDeathAI = 33;
                        break;

                    case NPCID.EaterofWorldsBody:
                        masoDeathAI = 34;
                        break;

                    case NPCID.GoblinSummoner:
                        masoDeathAI = 35;
                        break;

                    case NPCID.Clown:
                        masoDeathAI = 36;
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


                    //case NPCID.TheDestroyer:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                    //    break;
                    //case NPCID.TheDestroyerBody:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                    //    break;
                    //case NPCID.TheDestroyerTail:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                    //    break;


                    //case NPCID.SkeletronPrime:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                    //    break;
                    //case NPCID.PrimeCannon:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                    //    break;
                    //case NPCID.PrimeLaser:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                    //    break;
                    //case NPCID.PrimeSaw:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                    //    break;
                    //case NPCID.PrimeVice:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                    //    break;


                    //case NPCID.Retinazer:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.TwinsCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.TwinsCount * .0125));
                    //    break;
                    //case NPCID.Spazmatism:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.TwinsCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.TwinsCount * .0125));
                    //    break;


                    //case NPCID.Plantera:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                    //    break;
                    //case NPCID.PlanterasHook:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                    //    break;
                    //case NPCID.PlanterasTentacle:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                    //    break;

                    //case NPCID.Golem:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                    //    break;
                    //case NPCID.GolemFistLeft:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                    //    break;
                    //case NPCID.GolemFistRight:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                    //    break;
                    //case NPCID.GolemHead:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                    //    break;
                    //case NPCID.GolemHeadFree:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                    //    break;


                    //case NPCID.CultistBoss:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.CultistCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.CultistCount * .0125));
                    //    break;


                    //case NPCID.DukeFishron:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.FishronCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.FishronCount * .0125));
                    //    break;


                    //case NPCID.MoonLordCore:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                    //    break;
                    //case NPCID.MoonLordHand:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                    //    break;
                    //case NPCID.MoonLordHead:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                    //    break;
                    //case NPCID.MoonLordFreeEye:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                    //    break;
                    //case NPCID.MoonLordLeechBlob:
                    //    npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.MoonlordCount * .025));
                    //    npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                    //    break;


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

                    int[] transforms = { NPCID.Zombie, NPCID.ArmedZombie, NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo, NPCID.PincushionZombie, NPCID.ArmedZombiePincussion, NPCID.FemaleZombie, NPCID.ArmedZombieCenx, NPCID.SlimedZombie, NPCID.ArmedZombieSlimed, NPCID.TwiggyZombie, NPCID.ArmedZombieTwiggy, NPCID.SwampZombie, NPCID.ArmedZombieSwamp, NPCID.Skeleton, NPCID.BoneThrowingSkeleton, NPCID.HeadacheSkeleton, NPCID.BoneThrowingSkeleton2, NPCID.MisassembledSkeleton, NPCID.BoneThrowingSkeleton3, NPCID.PantlessSkeleton, NPCID.BoneThrowingSkeleton4, NPCID.JungleSlime, NPCID.SpikedJungleSlime, NPCID.IceSlime, NPCID.SpikedIceSlime };

                    if(Array.IndexOf(transforms, npc.type) % 2 == 0 && NPC.downedBoss1 && Main.rand.Next(5) == 0)
                    {
                        npcType = transforms[Array.IndexOf(transforms, npc.type) + 1];
                    }

                    switch (npc.type)
                    {
                        case NPCID.CorruptSlime:
                            if (npc.netID == NPCID.Slimeling || npc.netID == NPCID.Slimer2)
                                masoAI = 47;
                            break;

                        case NPCID.BlueSlime:
                            switch (npc.netID)
                            {
                                case 1:
                                    if (NPC.downedSlimeKing && Main.rand.Next(5) == 0)
                                        npcType = NPCID.SlimeSpiked;
                                    break;

                                case NPCID.BabySlime:
                                    masoAI = 47;
                                    break;

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

                        case NPCID.Zombie:
                            if (NPC.downedBoss1 && Main.rand.Next(5) == 0) //zombie horde
                            {
                                for (int i = 0; i < 6; i++)
                                {
                                    Vector2 pos = new Vector2(npc.Center.X + Main.rand.Next(-20, 20), npc.Center.Y);

                                    if (!Collision.SolidCollision(pos, npc.width, npc.height))
                                    {
                                        int j = NPC.NewNPC((int)pos.X, (int)pos.Y, NPCID.Zombie);
                                        if (j != 200)
                                        {
                                            NPC zombie = Main.npc[j];
                                            zombie.GetGlobalNPC<FargoGlobalNPC>().Transform = true;
                                        }
                                    }
                                }
                            }
                            break;

                        case NPCID.Crawdad:
                        case NPCID.GiantShelly:
                            if (Main.rand.Next(5) == 0) //pick a random salamander
                                npcType = Main.rand.Next(498, 507);
                            break;

                        case NPCID.Salamander:
                        case NPCID.Salamander2:
                        case NPCID.Salamander3:
                        case NPCID.Salamander4:
                        case NPCID.GiantShelly2:
                            if (Main.rand.Next(5) == 0) //pick a random crawdad
                                npcType = Main.rand.Next(494, 496);
                            break;

                        case NPCID.Salamander5:
                        case NPCID.Salamander6:
                        case NPCID.Salamander7:
                        case NPCID.Salamander8:
                        case NPCID.Crawdad2:
                            if (Main.rand.Next(5) == 0) //pick a random shelly
                                npcType = Main.rand.Next(496, 498);
                            break;

                        case NPCID.Goldfish:
                        case NPCID.GoldfishWalker:
                        case NPCID.BlueJellyfish:
                            if (Main.rand.Next(6) == 0) //random sharks
                                npcType = NPCID.Shark;
                            break;

                        //mimic swapping
                        case NPCID.BigMimicCorruption:
                            if (Main.rand.Next(4) == 0)
                                npcType = NPCID.BigMimicCrimson;
                            break;
                        case NPCID.BigMimicCrimson:
                            if (Main.rand.Next(4) == 0)
                                npcType = NPCID.BigMimicCorruption;
                            break;

                        case NPCID.VortexLarva:
                            npcType = NPCID.VortexHornet;
                            break;

                        case NPCID.Bee:
                        case NPCID.BeeSmall:
                            switch (Main.rand.Next(11))
                            {
                                case 0: npcType = NPCID.Hornet; break;
                                case 1: npcType = NPCID.HornetFatty; break;
                                case 2: npcType = NPCID.HornetHoney; break;
                                case 3: npcType = NPCID.HornetLeafy; break;
                                case 4: npcType = NPCID.HornetSpikey; break;
                                case 5: npcType = NPCID.HornetStingy; break;
                                case 6: npcType = NPCID.LittleHornetFatty; break;
                                case 7: npcType = NPCID.LittleHornetHoney; break;
                                case 8: npcType = NPCID.LittleHornetLeafy; break;
                                case 9: npcType = NPCID.LittleHornetSpikey; break;
                                case 10: npcType = NPCID.LittleHornetStingy; break;
                            }
                            break;

                        default:
                            break;
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
                        Aura(npc, 300, mod.BuffType<Hexed>(), true);
                        break;

                    case 3: //beetles
                        Aura(npc, 400, mod.BuffType<Lethargic>());
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
                        Aura(npc, 500, mod.BuffType<Flipped>());
                        break;

                    case 9: //meteor head
                        Counter++;
                        if (Counter >= 120)
                        {
                            Counter = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && npc.Distance(Main.player[t].Center) < 600)
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

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && npc.Distance(Main.player[t].Center) < 600)
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

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                if (npc.Distance(player.Center) < 800)
                                {
                                    Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * 10;
                                    int bubble = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.DetonatingBubble);
                                    Main.npc[bubble].velocity = velocity;
                                    Main.npc[bubble].damage = npc.damage / 2;
                                }
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

                    case 17: //seekerhead (cursed flamethrower)
                        Counter++;
                        if (Counter >= 10)
                        {
                            Counter = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && npc.Distance(Main.player[t].Center) < 500)
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

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && npc.Distance(Main.player[t].Center) < 800)
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
                        else if (npc.HasBuff(BuffID.OnFire) && npc.buffTime[npc.FindBuffIndex(BuffID.OnFire)] < 60 && !BossIsAlive(ref wallBoss, NPCID.WallofFlesh))
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

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && NPC.CountNPCS(NPCID.Piranha) <= 10 && Main.rand.Next(2) == 0)
                            {
                                Player player = Main.player[t];
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

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && SharkCount < 5)
                            {
                                Player player = Main.player[t];
                                if (player.HasBuff(BuffID.Bleeding) && (npc.type != NPCID.Shark || (player.wet && npc.wet)))
                                {
                                    npc.damage = (int)(npc.damage * 1.5);
                                    SharkCount++;
                                }
                            }
                        }
                        break;

                    case 22: //blackrecluse (on and off wall)
                        Counter++;
                        if (Counter >= 10)
                        {
                            Counter = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                int b = player.FindBuffIndex(BuffID.Webbed);
                                masoBool[0] = (b != -1); //remember if target is webbed until counter activates again

                                if (masoBool[0])
                                    player.AddBuff(mod.BuffType<Defenseless>(), player.buffTime[b]);
                            }
                        }

                        if (masoBool[0])
                        {
                            npc.position += npc.velocity;
                            SharkCount = 1;
                        }
                        else
                        {
                            SharkCount = 0;
                        }
                        break;

                    case 23: //eye of cthulhu
                        eyeBoss = npc.whoAmI;

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
                        retiBoss = npc.whoAmI;
                        bool spazAlive = BossIsAlive(ref spazBoss, NPCID.Spazmatism);

                        if (!masoBool[0]) //spawn in phase 2
                        {
                            masoBool[0] = true;
                            npc.ai[0] = 1f;
                            npc.ai[1] = 0.0f;
                            npc.ai[2] = 0.0f;
                            npc.ai[3] = 0.0f;
                            npc.netUpdate = true;
                        }

                        if (!masoBool[1]) //going to phase 3
                        {
                            if (npc.life <= npc.lifeMax / 2)
                            {
                                masoBool[1] = true;
                                Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                            }
                        }
                        else
                        {
                            npc.dontTakeDamage = false;

                            bool partnerP3 = true;
                            if (spazAlive && !Main.npc[spazBoss].GetGlobalNPC<FargoGlobalNPC>().masoBool[1]) //if twin not in phase3
                            {
                                npc.dontTakeDamage = true;
                                partnerP3 = false;
                            }

                            if (!masoBool[3] && partnerP3) //when both entering phase3
                            {
                                npc.defense = 9999;
                                npc.localAI[2]++;

                                if (npc.localAI[2] >= 6f) //heal every 6 ticks
                                {
                                    npc.localAI[2] = 0f;

                                    int heal = npc.lifeMax * Main.rand.Next(90, 111) / 100 / 2 / 20;
                                    npc.life += heal;
                                    if (npc.life > npc.lifeMax) //stop healing when fully recovered
                                    {
                                        npc.life = npc.lifeMax;
                                        
                                        //if spaz dead, OR (spaz must be alive, then is spaz at full health OR spaz done regen?) -> stop regenerating
                                        if (!spazAlive || Main.npc[spazBoss].life >= Main.npc[spazBoss].lifeMax || Main.npc[spazBoss].GetGlobalNPC<FargoGlobalNPC>().masoBool[3])
                                            masoBool[3] = true;
                                    }
                                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);
                                }
                            }

                            //2*pi * (# of full circles) / (seconds to finish rotation) / (ticks per sec)
                            const float rotationInterval = 2f * (float)Math.PI * 1.5f / 4f / 60f;

                            Counter++;
                            switch (masoState) //laser code idfk
                            {
                                case 0:
                                    if (Counter >= 600)
                                    {
                                        Counter = 0;
                                        if (npc.HasPlayerTarget)
                                        {
                                            masoState++;
                                            npc.localAI[3] = npc.rotation;
                                            masoBool[2] = (Main.player[npc.target].Center.X - npc.Center.X < 0);
                                        }
                                    }
                                    break;

                                case 1: //slowing down, beginning rotation
                                    npc.velocity *= (1f - Counter / 120f);
                                    npc.localAI[1] = 0f;
                                    npc.localAI[3] += Counter / 120f * rotationInterval * (masoBool[2] ? 1f : -1f);
                                    npc.rotation = npc.localAI[3];

                                    if (Counter >= 120) //FIRE LASER
                                    {
                                        if (Main.netMode != 1)
                                        {
                                            Vector2 speed = Vector2.UnitX.RotatedBy(npc.rotation);
                                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType<Projectiles.Masomode.PhantasmalDeathray>(), npc.damage / 2, 0f, Main.myPlayer, 0f, npc.whoAmI);
                                        }

                                        masoState++;
                                        Counter = 0;
                                    }
                                    break;

                                case 2: //spinning full speed
                                    npc.velocity = Vector2.Zero;
                                    npc.localAI[1] = 0f;
                                    npc.localAI[3] += rotationInterval * (masoBool[2] ? 1f : -1f);
                                    npc.rotation = npc.localAI[3];

                                    if (Counter >= 240)
                                    {
                                        masoState++;
                                        Counter = 0;
                                    }
                                    break;

                                case 3: //laser done, slowing down spin, moving again
                                    npc.velocity *= Counter / 60f;
                                    npc.localAI[1] = 0f;
                                    npc.localAI[3] += (1f - Counter / 60f) * rotationInterval * (masoBool[2] ? 1f : -1f);
                                    npc.rotation = npc.localAI[3];

                                    if (Counter >= 60)
                                    {
                                        masoState = 0;
                                        Counter = 0;
                                    }
                                    break;

                                default:
                                    masoState = 0;
                                    Counter = 0;
                                    break;
                            }

                            npc.position += npc.velocity / 2f;

                            //if (Counter == 600 && Main.netMode != 1 && npc.HasPlayerTarget)
                            //{
                            //    Vector2 vector200 = Main.player[npc.target].Center - npc.Center;
                            //    vector200.Normalize();
                            //    float num1225 = -1f;
                            //    if (vector200.X < 0f)
                            //    {
                            //        num1225 = 1f;
                            //    }
                            //    vector200 = vector200.RotatedBy(-num1225 * 1.04719755f, default(Vector2));
                            //    Projectile.NewProjectile(npc.Center, vector200, mod.ProjectileType<Projectiles.Masomode.PhantasmalDeathray>(), npc.damage / 2, 0f, Main.myPlayer, num1225 * 0.0104719755f, npc.whoAmI);
                            //    npc.netUpdate = true;
                            //}

                            //dust code
                            if (Main.rand.Next(4) < 3)
                            {
                                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 90, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].velocity *= 1.8f;
                                Main.dust[dust].velocity.Y -= 0.5f;
                                if (Main.rand.Next(4) == 0)
                                {
                                    Main.dust[dust].noGravity = false;
                                    Main.dust[dust].scale *= 0.5f;
                                }
                            }
                            SharkCount = 253;
                        }

                        if (!BossIsAlive(ref spazBoss, NPCID.Spazmatism))
                        {
                            Timer--;

                            if (Timer <= 0)
                            {
                                int spawn = NPC.NewNPC((int)npc.position.X + Main.rand.Next(-1000, 1000), (int)npc.position.Y + Main.rand.Next(-400, -100), NPCID.Spazmatism);
                                Main.npc[spawn].life = Main.npc[spawn].lifeMax / 2;
                                Main.NewText("Spazmatism has been revived!", 175, 75);
                                Timer = 600;
                            }
                        }
                        break;

                    case 25: //spazmatism
                        spazBoss = npc.whoAmI;
                        bool retiAlive = BossIsAlive(ref retiBoss, NPCID.Retinazer);

                        if (!masoBool[0]) //spawn in phase 2
                        {
                            masoBool[0] = true;
                            npc.ai[0] = 1f;
                            npc.ai[1] = 0.0f;
                            npc.ai[2] = 0.0f;
                            npc.ai[3] = 0.0f;
                            npc.netUpdate = true;
                        }

                        if (!masoBool[1])
                        {
                            if (npc.life <= npc.lifeMax / 2) //going to phase 3
                            {
                                masoBool[1] = true;
                                Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);

                                int index = npc.FindBuffIndex(BuffID.CursedInferno);
                                if (index != -1)
                                    npc.DelBuff(index); //remove cursed inferno debuff if i have it

                                npc.buffImmune[BuffID.CursedInferno] = true;
                                npc.buffImmune[BuffID.OnFire] = true;
                            }
                        }
                        else
                        {
                            npc.position += npc.velocity / 2f;
                            npc.dontTakeDamage = false;

                            bool partnerP3 = true;
                            if (retiAlive && !Main.npc[retiBoss].GetGlobalNPC<FargoGlobalNPC>().masoBool[1]) //if twin not in phase3
                            {
                                npc.dontTakeDamage = true;
                                partnerP3 = false;
                            }

                            if (!masoBool[3] && partnerP3) //when both entering phase3
                            {
                                npc.defense = 9999;
                                npc.localAI[2]++;

                                if (npc.buffTime[0] != 0) //cleanse debuff
                                    npc.DelBuff(0);

                                if (npc.localAI[2] >= 12f) //heal every 6 ticks
                                {
                                    npc.localAI[2] = 0f;

                                    int heal = npc.lifeMax * Main.rand.Next(90, 111) / 100 / 2 / 20;
                                    npc.life += heal;
                                    if (npc.life > npc.lifeMax)
                                    {
                                        npc.life = npc.lifeMax;

                                        //if reti dead, OR (reti must be alive, then is reti at full health OR reti already done regen?) -> stop regenerating
                                        if (!retiAlive || Main.npc[retiBoss].life >= Main.npc[retiBoss].lifeMax || Main.npc[retiBoss].GetGlobalNPC<FargoGlobalNPC>().masoBool[3])
                                            masoBool[3] = true;
                                    }
                                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);
                                }
                            }

                            if (npc.ai[1] == 0f) //not dashing
                            {
                                int ai2 = (int)npc.ai[2]; //timer, counts up to 400
                                switch (ai2)
                                {
                                    case 5:
                                    case 55:
                                    case 105:
                                    case 155:
                                    case 205:
                                    case 255:
                                    case 305:
                                    case 355:
                                        if (Main.netMode != 1 && npc.HasPlayerTarget) //vanilla spaz p1 shoot fireball code
                                        {
                                            Vector2 Speed = Main.player[npc.target].Center - npc.Center;
                                            Speed.Normalize();
                                            int Damage;
                                            if (Main.expertMode)
                                            {
                                                Speed *= 14f;
                                                Damage = 22;
                                            }
                                            else
                                            {
                                                Speed *= 12f;
                                                Damage = 25;
                                            }
                                            Speed.X += Main.rand.Next(-40, 41) * 0.05f;
                                            Speed.Y += Main.rand.Next(-40, 41) * 0.05f;
                                            Projectile.NewProjectile(npc.Center + Speed * 4f, Speed, ProjectileID.CursedFlameHostile, Damage, 0f, Main.myPlayer);
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                            else //cursed flamethrower when dashing
                            {
                                Counter++;
                                if (Counter == 4)
                                {
                                    Counter = 0;

                                    Projectile.NewProjectile(npc.Center, npc.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-2, 3))), ProjectileID.EyeFire, npc.damage / 5, 0f, Main.myPlayer);
                                }
                            }

                            //dust code
                            if (Main.rand.Next(4) < 3)
                            {
                                int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 89, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 3.5f);
                                Main.dust[dust].noGravity = true;
                                Main.dust[dust].velocity *= 1.8f;
                                Main.dust[dust].velocity.Y -= 0.5f;
                                if (Main.rand.Next(4) == 0)
                                {
                                    Main.dust[dust].noGravity = false;
                                    Main.dust[dust].scale *= 0.5f;
                                }
                            }
                            SharkCount = 254;
                        }

                        if (!retiAlive)
                        {
                            Timer--;

                            if (Timer <= 0)
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
                            if (p.active && npc.Distance(p.Center) < 5000)
                            {
                                p.AddBuff(mod.BuffType<Atrophied>(), 2);
                                p.AddBuff(mod.BuffType<Jammed>(), 2);
                                p.AddBuff(mod.BuffType<Antisocial>(), 2);
                            }
                        }
                        break;

                    case 27: //solar pill
                        foreach (Player p in Main.player)
                        {
                            if (p.active && npc.Distance(p.Center) < 5000)
                            {
                                p.buffImmune[BuffID.Silenced] = false;
                                p.AddBuff(BuffID.Silenced, 2);
                                p.AddBuff(mod.BuffType<Jammed>(), 2);
                                p.AddBuff(mod.BuffType<Antisocial>(), 2);
                            }
                        }
                        break;

                    case 28: //stardust pill
                        foreach (Player p in Main.player)
                        {
                            if (p.active && npc.Distance(p.Center) < 5000)
                            {
                                p.AddBuff(mod.BuffType<Atrophied>(), 2);
                                p.AddBuff(mod.BuffType<Jammed>(), 2);
                                p.buffImmune[BuffID.Silenced] = false;
                                p.AddBuff(BuffID.Silenced, 2);
                            }
                        }
                        break;

                    case 29: //vortex pill
                        foreach (Player p in Main.player)
                        {
                            if (p.active && npc.Distance(p.Center) < 5000)
                            {
                                p.AddBuff(mod.BuffType<Atrophied>(), 2);
                                p.buffImmune[BuffID.Silenced] = false;
                                p.AddBuff(BuffID.Silenced, 2);
                                p.AddBuff(mod.BuffType<Antisocial>(), 2);
                            }
                        }
                        break;

                    case 30: //lunatic cultist
                        cultBoss = npc.whoAmI;
                        foreach (Player p in Main.player.Where(x => x.active && !x.dead))
                        {
                            if (npc.Distance(p.Center) < 2000)
                            {
                                p.AddBuff(mod.BuffType<ClippedWings>(), 2);

                                if (p.mount.Active)
                                    p.mount.Dismount(p);
                            }
                        }

                        Counter++;
                        if (Counter >= 240)
                        {
                            int maxTimeShorten = (int)(210 * (1f - (float)npc.life / npc.lifeMax));
                            Counter = (short)Main.rand.Next(maxTimeShorten, maxTimeShorten + 30);

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && Main.player[t].active)
                            {
                                Point tileCoordinates = Main.rand.Next(2) == 0 ? npc.Top.ToTileCoordinates() : Main.player[t].Top.ToTileCoordinates();

                                tileCoordinates.X += Main.rand.Next(-25, 25);
                                tileCoordinates.Y -= 15 + Main.rand.Next(-5, 5);

                                for (int index = 0; index < 10 && !WorldGen.SolidTile(tileCoordinates.X, tileCoordinates.Y) && tileCoordinates.Y > 10; ++index)
                                {
                                    tileCoordinates.Y -= 1;
                                }

                                Projectile.NewProjectile(tileCoordinates.X * 16 + 8, tileCoordinates.Y * 16 + 17, 0f, 0f, 578, 0, 1f, Main.myPlayer);
                            }
                        }

                        Timer++;
                        if (Timer >= 1200)
                        {
                            Timer = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && Main.player[t].active && !NPC.AnyNPCs(NPCID.AncientCultistSquidhead))
                                NPC.SpawnOnPlayer(t, NPCID.AncientCultistSquidhead);
                        }
                        break;

                    case 31: //king slime
                        slimeBoss = npc.whoAmI;

                        if (!masoBool[0])
                        {
                            if (npc.HasPlayerTarget)
                            {
                                Player player = Main.player[npc.target];
                                if (player.active && !player.dead && player.Center.Y < npc.position.Y && npc.Distance(player.Center) < 1000f)
                                {
                                    Counter++; //timer runs if player is above me and nearby

                                    if (Counter >= 600) //go berserk
                                    {
                                        masoBool[0] = true;
                                        SharkCount = 1;
                                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);

                                        npc.defDamage *= 20;
                                        npc.defDefense *= 999;

                                        npc.netUpdate = true;
                                    }
                                }
                                else
                                {
                                    Counter = 0;
                                }
                            }
                        }
                        else
                        {
                            npc.damage = npc.defDamage;
                            npc.defense = npc.defDefense;

                            if (npc.HasPlayerTarget)
                            {
                                Player p = Main.player[npc.target];

                                Counter++;
                                if (Counter >= 3) //spray random slime spikes
                                {
                                    Counter = 0;

                                    Vector2 speed = p.Center - npc.Center;
                                    speed.Normalize();
                                    speed *= 16f + Main.rand.Next(-50, 51) * 0.04f;
                                    if (speed.X < 0)
                                        speed = speed.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-15, 31)));
                                    else
                                        speed = speed.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 16)));
                                    Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height), speed.X, speed.Y, ProjectileID.SpikedSlimeSpike, npc.damage / 4, 0f, Main.myPlayer);
                                }

                                if (p.active && !p.dead)
                                {
                                    p.pulley = false;
                                    p.controlHook = false;
                                    if (p.mount.Active)
                                        p.mount.Dismount(p);

                                    p.AddBuff(BuffID.Slimed, 2);
                                    p.AddBuff(mod.BuffType<Crippled>(), 2);
                                    p.AddBuff(mod.BuffType<ClippedWings>(), 2);
                                }
                            }
                        }
                        break;

                    case 32: //eater of worlds head
                        eaterBoss = npc.whoAmI;

                        Counter++;
                        if (Counter >= 6) //cursed flamethrower, roughly same direction as head
                        {
                            Counter = 0;

                            if (!Collision.EmptyTile((int)npc.Center.X / 16, (int)npc.Center.Y / 16))
                            {
                                Vector2 velocity = new Vector2(5f, 0f).RotatedBy(npc.rotation - Math.PI / 2.0 + MathHelper.ToRadians(Main.rand.Next(-15, 16)));
                                Projectile.NewProjectile(npc.Center, velocity, ProjectileID.EyeFire, npc.damage / 6, 0f, Main.myPlayer);
                            }
                        }
                        break;

                    case 33: //queen bee
                        beeBoss = npc.whoAmI;
                        break;

                    case 34: //skeletron head
                        skeleBoss = npc.whoAmI;

                        if (Counter != 0)
                        {
                            Timer++;

                            if (Timer >= 1800)
                            {
                                Timer = 0;

                                bool otherHandStillAlive = false;
                                for (int i = 0; i < 200; i++) //look for hand that belongs to me
                                {
                                    if (Main.npc[i].active && Main.npc[i].type == NPCID.SkeletronHand && Main.npc[i].ai[1] == npc.whoAmI)
                                    {
                                        otherHandStillAlive = true;
                                        break;
                                    }
                                }

                                if (Main.netMode != 1)
                                {
                                    int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.SkeletronHand, npc.whoAmI, 0f, 0f, 0f, 0f, npc.target);

                                    if (n != 200)
                                    {
                                        Main.npc[n].ai[0] = (Counter == 1) ? 1f : -1f;
                                        Main.npc[n].ai[1] = npc.whoAmI;
                                        Main.npc[n].life = Main.npc[n].lifeMax / 2;
                                        Main.npc[n].netUpdate = true;
                                    }
                                }

                                if (!otherHandStillAlive)
                                {
                                    if (Counter == 1)
                                        Counter = 2;
                                    else
                                        Counter = 1;

                                    //Main.NewText("reviving other hand");
                                }
                                else
                                {
                                    Counter = 0;
                                }
                            }
                        }

                        if (npc.ai[1] == 1f) //spinning
                        {
                            npc.localAI[2]++;

                            float ratio = (float)npc.life / npc.lifeMax;
                            float threshold = 5f + 25f * ratio;
                            if (npc.localAI[2] >= threshold) //spray bones
                            {
                                npc.localAI[2] = 0f;

                                if (threshold > 0)
                                {
                                    Vector2 speed = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                                    speed.Normalize();
                                    speed *= 6f;
                                    speed += npc.velocity * 1.5f * (1f - ratio);
                                    speed.Y -= Math.Abs(speed.X) * 0.2f;

                                    if (Main.netMode != 1)
                                        Projectile.NewProjectile(npc.Center, speed, ProjectileID.SkeletonBone, npc.damage / 9 * 2, 0f, Main.myPlayer);
                                }
                            }
                        }
                        break;

                    case 35: //wall of flesh mouth
                        wallBoss = npc.whoAmI;
                        break;

                    case 36: //destroyer head
                        destroyBoss = npc.whoAmI;

                        if (!masoBool[0])
                        {
                            if (npc.life < npc.lifeMax / 2)
                            {
                                masoBool[0] = true;
                                npc.netUpdate = true;
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }

                            RegenTimer = 2;
                        }
                        else
                        {
                            if (npc.HasPlayerTarget && !Main.dayTime && !Main.player[npc.target].dead)
                            {
                                int cornerX1 = (int)npc.position.X / 16 - 1;
                                int cornerX2 = (int)(npc.position.X + npc.width) / 16 + 2;
                                int cornerY1 = (int)npc.position.Y / 16 - 1;
                                int cornerY2 = (int)(npc.position.Y + npc.height) / 16 + 2;

                                //out of bounds checks
                                if (cornerX1 < 0)
                                    cornerX1 = 0;
                                if (cornerX2 > Main.maxTilesX)
                                    cornerX2 = Main.maxTilesX;
                                if (cornerY1 < 0)
                                    cornerY1 = 0;
                                if (cornerY2 > Main.maxTilesY)
                                    cornerY2 = Main.maxTilesY;

                                bool isOnSolidTile = false;

                                //for every tile this npc occupies
                                for (int x = cornerX1; x < cornerX2; ++x)
                                {
                                    for (int y = cornerY1; y < cornerY2; ++y)
                                    {
                                        Tile tile = Main.tile[x, y];
                                        if (tile != null && (tile.nactive() && (Main.tileSolid[tile.type] || Main.tileSolidTop[tile.type] && tile.frameY == 0) || tile.liquid > 64))
                                        {
                                            Vector2 tilePos = new Vector2(x * 16f, y * 16f);
                                            if (npc.position.X + npc.width > tilePos.X && npc.position.X < tilePos.X + 16f && npc.position.Y + npc.height > tilePos.Y && npc.position.Y < tilePos.Y + 16f)
                                            {
                                                isOnSolidTile = true;
                                                break;
                                            }
                                        }
                                    }

                                    if (isOnSolidTile)
                                        break;
                                }

                                if (!isOnSolidTile)
                                {
                                    //negating default air behaviour
                                    npc.velocity.Y -= 0.15f;

                                    float num14 = 16f;
                                    float num15 = 0.1f;
                                    float num16 = 0.15f;
                                    float num17 = Main.player[npc.target].Center.X;
                                    float num18 = Main.player[npc.target].Center.Y;

                                    float num21 = num17 - npc.Center.X;
                                    float num22 = num18 - npc.Center.Y;
                                    float num23 = (float)Math.Sqrt((double)num21 * (double)num21 + (double)num22 * (double)num22);

                                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num14 * 0.4f)
                                    {
                                        if (npc.velocity.X < 0f)
                                        {
                                            npc.velocity.X += num15 * 1.1f;
                                        }
                                        else
                                        {
                                            npc.velocity.X -= num15 * 1.1f;
                                        }
                                    }
                                    else if (npc.velocity.Y == num14)
                                    {
                                        if (npc.velocity.X < num21)
                                        {
                                            npc.velocity.X -= num15;
                                        }
                                        else if (npc.velocity.X > num21)
                                        {
                                            npc.velocity.X += num15;
                                        }
                                    }
                                    else if (npc.velocity.Y > 4f)
                                    {
                                        if (npc.velocity.X < 0f)
                                        {
                                            npc.velocity.X -= num15 * 0.9f;
                                        }
                                        else
                                        {
                                            npc.velocity.X += num15 * 0.9f;
                                        }
                                    }

                                    //ground movement code but it only runs when airborne
                                    float num2 = (float)Math.Sqrt(num21 * num21 + num22 * num22);
                                    float num3 = Math.Abs(num21);
                                    float num4 = Math.Abs(num22);

                                    float num5 = num14 / num2;
                                    float num6 = num21 * num5;
                                    float num7 = num22 * num5;

                                    if ((npc.velocity.X > 0f && num6 > 0f || npc.velocity.X < 0f && num6 < 0f) && (npc.velocity.Y > 0f && num7 > 0f || npc.velocity.Y < 0f && num7 < 0f))
                                    {
                                        if (npc.velocity.X < num6)
                                        {
                                            npc.velocity.X += num16;
                                        }
                                        else if (npc.velocity.X > num6)
                                        {
                                            npc.velocity.X -= num16;
                                        }
                                        if (npc.velocity.Y < num7)
                                        {
                                            npc.velocity.Y += num16;
                                        }
                                        else if (npc.velocity.Y > num7)
                                        {
                                            npc.velocity.Y -= num16;
                                        }
                                    }

                                    if (npc.velocity.X > 0f && num6 > 0f || npc.velocity.X < 0f && num6 < 0f || npc.velocity.Y > 0f && num7 > 0f || npc.velocity.Y < 0f && num7 < 0f)
                                    {
                                        if (npc.velocity.X < num6)
                                        {
                                            npc.velocity.X += num15;
                                        }
                                        else if (npc.velocity.X > num6)
                                        {
                                            npc.velocity.X -= num15;
                                        }
                                        if (npc.velocity.Y < num7)
                                        {
                                            npc.velocity.Y += num15;
                                        }
                                        else if (npc.velocity.Y > num7)
                                        {
                                            npc.velocity.Y -= num15;
                                        }

                                        if (Math.Abs(num7) < num14 * 0.2f && (npc.velocity.X > 0f && num6 < 0f || npc.velocity.X < 0f && num6 > 0f))
                                        {
                                            if (npc.velocity.Y > 0f)
                                            {
                                                npc.velocity.Y += num15 * 2f;
                                            }
                                            else
                                            {
                                                npc.velocity.Y -= num15 * 2f;
                                            }
                                        }

                                        if (Math.Abs(num6) < num14 * 0.2f && (npc.velocity.Y > 0f && num7 < 0f || npc.velocity.Y < 0f && num7 > 0f))
                                        {
                                            if (npc.velocity.X > 0f)
                                            {
                                                npc.velocity.X += num15 * 2f;
                                            }
                                            else
                                            {
                                                npc.velocity.X -= num15 * 2f;
                                            }
                                        }
                                    }
                                    else if (num3 > num4)
                                    {
                                        if (npc.velocity.X < num6)
                                        {
                                            npc.velocity.X += num15 * 1.1f;
                                        }
                                        else if (npc.velocity.X > num6)
                                        {
                                            npc.velocity.X -= num15 * 1.1f;
                                        }
                                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num14 * 0.5f)
                                        {
                                            if (npc.velocity.Y > 0f)
                                            {
                                                npc.velocity.Y += num15;
                                            }
                                            else
                                            {
                                                npc.velocity.Y -= num15;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (npc.velocity.Y < num7)
                                        {
                                            npc.velocity.Y += num15 * 1.1f;
                                        }
                                        else if (npc.velocity.Y > num7)
                                        {
                                            npc.velocity.Y -= num15 * 1.1f;
                                        }

                                        if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num14 * 0.5f)
                                        {
                                            if (npc.velocity.X > 0f)
                                            {
                                                npc.velocity.X += num15;
                                            }
                                            else
                                            {
                                                npc.velocity.X -= num15;
                                            }
                                        }
                                    }

                                    npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;

                                    npc.netUpdate = true;
                                    npc.localAI[0] = 1f;
                                }
                            }

                            npc.position += npc.velocity * (1f - (float)npc.life / npc.lifeMax);
                        }
                        break;

                    case 37: //fishron
                        fishBoss = npc.whoAmI;
                        npc.position += npc.velocity * 0.25f;

                        switch ((int)npc.ai[0])
                        {
                            case 0: //phase 1
                                break;

                            case 1: //p1 dash
                                Counter++;
                                if (Counter >= 6)
                                {
                                    Counter = 0;

                                    if (Main.netMode != 1)
                                        NPC.NewNPC((int)npc.position.X + Main.rand.Next(npc.width), (int)npc.position.Y + Main.rand.Next(npc.height), NPCID.DetonatingBubble);
                                }
                                break;

                            case 2: //p1 bubbles
                                break;

                            case 3: //p1 drop nados
                                break;

                            case 4: //phase 2 transition
                                npc.dontTakeDamage = true;

                                masoBool[1] = false;

                                if (npc.buffTime[0] != 0)
                                    npc.DelBuff(0);

                                if (npc.ai[2] >= 120)
                                {
                                    Counter++;
                                    if (Counter >= 6) //display healing effect
                                    {
                                        Counter = 0;
                                        int heal = npc.lifeMax * Main.rand.Next(100, 121) / 1000;
                                        npc.life += heal;
                                        if (npc.life > npc.lifeMax)
                                            npc.life = npc.lifeMax;
                                        CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);
                                    }
                                }
                                break;

                            case 5: //phase 2
                                if (!masoBool[1])
                                    npc.dontTakeDamage = false;
                                break;

                            case 6: //p2 dash
                                goto case 1;

                            case 7: //p2 spin & bubbles
                                Counter++;
                                if (Counter >= 8)
                                {
                                    Counter = 0;

                                    if (Main.netMode != 1) //spawn sharkron flying up with random velocity
                                    {
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.Sharkron2, 0, 1f, 1f, 0f, 0f, npc.target);

                                        if (n != 200)
                                        {
                                            Main.npc[n].velocity.X = Main.rand.Next(-30, 31) / 3f;
                                            Main.npc[n].velocity.Y = Main.rand.Next(-45, -16) / 3f;
                                            Main.npc[n].direction = -1;
                                            //if (Main.npc[n].velocity.X > 0f) Main.npc[n].direction = -1;

                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                    }
                                }
                                break;

                            case 8: //p2 cthulhunado
                                if (Main.netMode != 1 && npc.ai[2] == 60)
                                {
                                    Vector2 spawnPos = Vector2.UnitX * npc.direction;
                                    spawnPos = spawnPos.RotatedBy(npc.rotation);
                                    spawnPos *= npc.width + 20f;
                                    spawnPos /= 2f;
                                    spawnPos += npc.Center;
                                    Projectile.NewProjectile(spawnPos.X, spawnPos.Y, npc.direction * 2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                                    Projectile.NewProjectile(spawnPos.X, spawnPos.Y, npc.direction * -2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                                }
                                break;

                            case 9: //phase 3 transition
                                npc.dontTakeDamage = true;

                                if (npc.buffTime[0] != 0)
                                    npc.DelBuff(0);

                                if (npc.ai[2] >= 120)
                                {
                                    if (!masoBool[0])
                                    {
                                        masoBool[0] = true;
                                        for (int i = 0; i < npc.buffImmune.Length; i++)
                                            npc.buffImmune[i] = true;
                                    }

                                    Counter++;
                                    if (Counter >= 6) //display healing effect
                                    {
                                        Counter = 0;
                                        int heal = npc.lifeMax * Main.rand.Next(100, 121) / 1000;
                                        npc.life += heal;
                                        if (npc.life > npc.lifeMax)
                                            npc.life = npc.lifeMax;
                                        CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);
                                    }
                                }
                                break;

                            case 10: //phase 3
                                //vanilla fishron has x1.1 damage in p3. p2 has x1.2 damage...
                                npc.damage = (int)(npc.defDamage * 1.3 * (Main.expertMode ? 0.6f * Main.damageMultiplier : 1f));
                                npc.defense = npc.defDefense;
                                npc.position += npc.velocity * 0.25f;

                                Timer++;
                                if (Timer >= 30 + 570 * npc.life / npc.lifeMax) //spawn cthulhunado
                                {
                                    Timer = 0;

                                    if (Main.netMode != 1)
                                    {
                                        if (Main.rand.Next(2) == 0)
                                        {
                                            Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, npc.target + 1);
                                        }
                                        else
                                        {
                                            Vector2 spawnPos = Vector2.UnitX * npc.direction;
                                            spawnPos = spawnPos.RotatedBy(npc.rotation);
                                            spawnPos *= npc.width + 20f;
                                            spawnPos /= 2f;
                                            spawnPos += npc.Center;
                                            Projectile.NewProjectile(spawnPos.X, spawnPos.Y, npc.direction * 2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                                            Projectile.NewProjectile(spawnPos.X, spawnPos.Y, npc.direction * -2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                                        }
                                    }
                                }
                                break;

                            case 11: //p3 dash
                                Counter++;
                                if (Counter >= 3)
                                {
                                    Counter = 0;

                                    if (Main.netMode != 1)
                                        NPC.NewNPC((int)npc.position.X + Main.rand.Next(npc.width), (int)npc.position.Y + Main.rand.Next(npc.height), NPCID.DetonatingBubble);
                                }
                                goto case 10;

                            case 12: //p3 *teleports behind you*
                                goto case 10;

                            default:
                                break;
                        }
                        break;

                    case 38: //moon lord core
                        moonBoss = npc.whoAmI;

                        if (!masoBool[0])
                        {
                            masoBool[0] = !npc.dontTakeDamage; //remembers even if core becomes invulnerable again
                        }
                        else
                        {
                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                if (player.moonLeech) //replace moon bite with mutant nibble
                                {
                                    int buffIndex = player.FindBuffIndex(BuffID.MoonLeech);
                                    if (buffIndex != -1)
                                    {
                                        player.AddBuff(mod.BuffType<MutantNibble>(), player.buffTime[buffIndex]);
                                        player.DelBuff(buffIndex);
                                    }
                                }
                            }

                            foreach (Player p in Main.player.Where(x => x.active && !x.dead))
                            {
                                p.AddBuff(BuffID.WaterCandle, 2);
                                p.AddBuff(BuffID.Battle, 2);
                            }

                            Timer++;
                            if (Timer >= 1200)
                            {
                                Timer = 0;

                                //spawn visions from hands, dragon from head
                                for (int i = 0; i < 3; i++)
                                {
                                    NPC bodyPart = Main.npc[(int)npc.localAI[i]];

                                    if (i == 2)
                                    {
                                        if (bodyPart.active && bodyPart.type == NPCID.MoonLordHead)
                                        {
                                            NPC.NewNPC((int)bodyPart.Center.X, (int)bodyPart.Center.Y, NPCID.CultistDragonHead, 0, 0f, 0f, 0f, 0f, npc.target);
                                        }
                                    }
                                    else
                                    {
                                        if (bodyPart.active && bodyPart.type == NPCID.MoonLordHand)
                                        {
                                            NPC.NewNPC((int)bodyPart.Center.X, (int)bodyPart.Center.Y, NPCID.AncientCultistSquidhead, 0, 0f, 0f, 0f, 0f, npc.target);
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 39: //splinterling
                        Counter++;
                        if (Counter >= 24)
                        {
                            Counter = 0;

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-3, 4), Main.rand.Next(-10, 0), Main.rand.Next(326, 329), (int)(npc.damage * .4), 0f, Main.myPlayer);
                        }
                        break;

                    case 40: //golem body
                        if (npc.ai[0] == 0f && npc.velocity.Y == 0f && !npc.dontTakeDamage) //manipulating golem jump ai when vulnerable
                        {
                            if (npc.ai[1] > 0f)
                            {
                                npc.ai[1] += 5f; //count up to initiate jump faster
                            }
                            else
                            {
                                float threshold = -2f - (float)Math.Round(18.0 * npc.life / npc.lifeMax);

                                if (npc.ai[1] < threshold) //jump activates at npc.ai[1] == -1
                                    npc.ai[1] = threshold;
                            }
                        }

                        Counter++;
                        if (Counter >= 600)
                        {
                            Counter = 0;

                            if (!npc.dontTakeDamage) //p2 spiky balls
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height), Main.rand.Next(-2, 3), Main.rand.Next(-2, 3), ProjectileID.SpikyBallTrap, 35, 0f, Main.myPlayer);
                                    //Main.projectile[p].friendly = false;
                                }
                            }
                        }

                        if (!npc.dontTakeDamage)
                        {
                            npc.life += 17;
                            if (npc.life > npc.lifeMax)
                                npc.life = npc.lifeMax;

                            Timer++;
                            if (Timer >= 75)
                            {
                                Timer = Main.rand.Next(30);
                                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, 1000, false, false);
                            }
                        }
                        break;

                    case 41: //golem head flying
                        npc.position += npc.velocity * 0.25f;

                        Timer++;
                        if (Timer >= 4) //flamethrower if player roughly below me
                        {
                            Timer = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                Vector2 distance = player.Center - npc.Center;
                                if (Math.Abs(distance.X) < npc.width)
                                {
                                    double angle = distance.ToRotation();
                                    if (angle > Math.PI * .25 && angle < Math.PI * .75)
                                    {
                                        distance.Normalize();
                                        distance *= Main.rand.Next(4, 9);

                                        Projectile.NewProjectile(npc.Center.X + npc.velocity.X * 5, npc.position.Y + npc.velocity.Y * 5, distance.X, distance.Y, ProjectileID.FlamesTrap, npc.damage / 5, 0f, Main.myPlayer);
                                        //Main.projectile[p].friendly = false;

                                        Main.PlaySound(SoundID.Item34, npc.Center);
                                    }
                                }
                            }
                        }

                        Counter++;
                        if (Counter >= 360)
                        {
                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1 && NPC.golemBoss != -1 && Main.npc[NPC.golemBoss].active && Main.npc[NPC.golemBoss].type == NPCID.Golem)
                            {
                                Counter = (short)(300 * (1f - (float)Main.npc[NPC.golemBoss].life / Main.npc[NPC.golemBoss].lifeMax));

                                Vector2 velocity = Main.player[t].Center - npc.Center;
                                velocity.Normalize();
                                velocity *= 11f;

                                velocity = velocity.RotatedBy(MathHelper.ToRadians(-15));

                                for (int i = 0; i < 4; i++)
                                {
                                    int p = Projectile.NewProjectile(npc.Center, velocity, ProjectileID.EyeBeam, 27, 0f, Main.myPlayer);
                                    Main.projectile[p].timeLeft = 300;

                                    velocity = velocity.RotatedBy(MathHelper.ToRadians(10));
                                }
                            }
                            else
                            {
                                Counter = 0; //failsafe
                            }
                        }
                        break; //invincible anyway, so no regen needed

                    case 42: //all vulnerable golem parts
                        if (!npc.dontTakeDamage)
                        {
                            bool isFist = npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight;
                            npc.life += isFist ? 167 : 8;
                            if (npc.life > npc.lifeMax)
                                npc.life = npc.lifeMax;

                            Timer++;
                            if (Timer >= 75)
                            {
                                Timer = Main.rand.Next(30);
                                int healDisplay = isFist ? 9999 : 500;
                                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, healDisplay, false, false);
                            }
                        }
                        break;

                    case 43: //flying snake
                        if (masoDeathAI == 0) //after reviving
                        {
                            npc.position += npc.velocity;
                            npc.knockBackResist = 0f;
                        }
                        break;

                    case 44: //lihzahrd
                        Counter++;
                        if (Counter >= 200)
                        {
                            Counter = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                if (player.active)
                                {
                                    Vector2 velocity = player.Center - npc.Center;
                                    velocity.Normalize();
                                    velocity *= 12f;

                                    int p = Projectile.NewProjectile(npc.Center, velocity, ProjectileID.PoisonDartTrap, 30, 0f, Main.myPlayer);
                                    Main.projectile[p].friendly = false;
                                }
                            }
                        }
                        break;

                    case 45: //stardust cell baby
                        if (npc.ai[0] >= 240f)
                        {
                            if (Main.netMode == 1)
                                return;

                            int newType;
                            switch (Main.rand.Next(4))
                            {
                                case 0: newType = NPCID.StardustJellyfishBig; break;
                                case 1: newType = NPCID.StardustSpiderBig; break;
                                case 2: newType = NPCID.StardustWormHead; break;
                                case 3: newType = NPCID.StardustCellBig; break;
                                default: newType = NPCID.StardustCellBig; break;
                            }

                            npc.Transform(newType);
                        }
                        break;

                    case 46: //phantasm dragon non-head segments
                        npc.dontTakeDamage = true;
                        break;

                    case 47: //promotion over time
                        Counter++;

                        switch (npc.netID)
                        {
                            case NPCID.BabySlime:
                                if (Counter >= 600)
                                    npc.Transform(NPCID.MotherSlime);
                                break;

                            case NPCID.Slimeling:
                                if (Counter >= 600)
                                    npc.Transform(NPCID.CorruptSlime);
                                break;

                            case NPCID.Slimer2:
                                if (Counter >= 300)
                                    npc.Transform(NPCID.Slimer);
                                break;

                            case NPCID.MothronSpawn:
                                if (Counter >= 300)
                                    npc.Transform(NPCID.Mothron);
                                break;

                            default:
                                break;
                        }
                        break;

                    case 48: //pumpking
                        Timer++;
                        if (Timer >= 12)
                        {
                            Timer = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                Vector2 distance = player.Center - npc.Center;
                                if (Math.Abs(distance.X) < npc.width) //flame rain if player roughly below me
                                {
                                    Projectile.NewProjectile(npc.Center.X, npc.position.Y, Main.rand.Next(-3, 4), Main.rand.Next(-4, 0), Main.rand.Next(326, 329), 40, 0f, Main.myPlayer);
                                }
                            }
                        }
                        break;

                    case 49: //corite
                        Aura(npc, 250, BuffID.Burning);
                        break;

                    case 50: //brain suckler
                        Counter++;
                        if (Counter >= 300)
                        {
                            if (npc.ai[0] != 5f) //if not latched on player
                            {
                                Shoot(npc, 1000, 9, ProjectileID.NebulaLaser, (int)(npc.damage * 0.4f), 0);
                            }

                            Counter = (short)Main.rand.Next(120);
                        }
                        break;

                    case 51: //plantera
                        if (npc.life < npc.lifeMax / 2) //phase 2
                        {
                            npc.defense += 42;

                            Counter++;
                            if (Counter >= 20)
                            {
                                Counter = 0;

                                int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                                if (t != -1)
                                {
                                    int damage = 22;
                                    int type = ProjectileID.SeedPlantera;

                                    if (Main.rand.Next(2) == 0)
                                    {
                                        damage = 27;
                                        type = ProjectileID.PoisonSeedPlantera;
                                    }
                                    else if (Main.rand.Next(6) == 0)
                                    {
                                        damage = 31;
                                        type = ProjectileID.ThornBall;
                                    }

                                    if (!Main.player[t].ZoneJungle)
                                        damage *= 2;
                                    else if (Main.expertMode)
                                        damage *= 9 / 10;

                                    Vector2 velocity = Main.player[t].Center - npc.Center;
                                    velocity.Normalize();
                                    velocity *= Main.expertMode ? 17f : 15f;

                                    int p = Projectile.NewProjectile(npc.Center + velocity * 3f, velocity, type, damage, 0f, Main.myPlayer);
                                    if (type != ProjectileID.ThornBall)
                                        Main.projectile[p].timeLeft = 300;
                                }
                            }

                            Timer++;
                            if (Timer >= 300)
                            {
                                Timer = 0;

                                int tentaclesToSpawn = 12;
                                for (int i = 0; i < 200; i++)
                                {
                                    if (Main.npc[i].active && Main.npc[i].type == NPCID.PlanterasTentacle && Main.npc[i].ai[3] == 0f)
                                    {
                                        tentaclesToSpawn--;
                                    }
                                }

                                for (int i = 0; i < tentaclesToSpawn; i++)
                                {
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PlanterasTentacle, npc.whoAmI);
                                }
                            }

                            SharkCount = 0;

                            if (npc.HasPlayerTarget)
                            {
                                if (Main.player[npc.target].GetModPlayer<FargoPlayer>().Infested)
                                {
                                    npc.position += npc.velocity;
                                    npc.defense *= 3;
                                    Counter++;
                                    SharkCount = 1;

                                    if (RegenTimer > 60)
                                        RegenTimer = 60;
                                }
                            }
                        }
                        break;

                    case 52: //prime saw
                        Counter++;
                        if (Counter >= 3) //flamethrower in same direction that saw is pointing
                        {
                            Counter = 0;

                            Vector2 velocity = new Vector2(7f, 0f).RotatedBy(npc.rotation + Math.PI / 2.0);
                            Projectile.NewProjectile(npc.Center, velocity, ProjectileID.FlamesTrap, npc.damage / 4, 0f, Main.myPlayer);
                            //Main.projectile[p].friendly = false;

                            Main.PlaySound(SoundID.Item34, npc.Center);
                        }
                        goto case 77;

                    case 53: //ice queen
                        Counter++;

                        short countCap = 7;
                        if (npc.life < npc.lifeMax * 3 / 4)
                            countCap--;
                        if (npc.life < npc.lifeMax / 2)
                            countCap -= 2;
                        if (npc.life < npc.lifeMax / 4)
                            countCap -= 3;
                        if (npc.life < npc.lifeMax / 10)
                            countCap -= 4;

                        if (Counter > countCap)
                        {
                            Counter = 0;

                            Vector2 speed = new Vector2(Main.rand.Next(-1000, 1001), Main.rand.Next(-1000, 1001));
                            speed.Normalize();
                            speed *= 15f;

                            Vector2 spawn = npc.Center;
                            spawn.Y -= 20f;
                            spawn += speed * 4f;

                            if (Main.netMode != 1)
                                Projectile.NewProjectile(spawn, speed, ProjectileID.FrostShard, 35, 0f, Main.myPlayer);
                        }
                        break;

                    case 54: //eyezor
                        Counter++;
                        if (Counter >= 8)
                        {
                            Counter = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                if (player.active)
                                {
                                    Vector2 velocity = player.Center - npc.Center;
                                    velocity.Normalize();
                                    velocity *= 4f;

                                    Projectile.NewProjectile(npc.Center, velocity, ProjectileID.EyeFire, npc.damage / 5, 0f, Main.myPlayer);
                                }
                            }
                        }
                        break;

                    case 55: //skeletron prime
                        primeBoss = npc.whoAmI;

                        if (npc.ai[1] == 1f && npc.ai[2] > 2f)
                        {
                            if (npc.velocity.Length() < 10f)
                            {
                                npc.velocity.Normalize();
                                npc.velocity *= 10f;
                            }
                        }

                        if (!masoBool[0])
                        {
                            if (npc.life < npc.lifeMax / 4) //enter phase 2
                            {
                                masoBool[0] = true;
                                masoHurtAI = 0;
                                npc.netUpdate = true;
                                Counter = 0;

                                if (!NPC.AnyNPCs(NPCID.PrimeLaser))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeLaser, npc.whoAmI, 1f, npc.whoAmI, 0f, 150f, npc.target);
                                if (!NPC.AnyNPCs(NPCID.PrimeSaw))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeSaw, npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, npc.target);
                                if (!NPC.AnyNPCs(NPCID.PrimeCannon))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeCannon, npc.whoAmI, -1f, npc.whoAmI, 0f, 0f, npc.target);
                                if (!NPC.AnyNPCs(NPCID.PrimeVice))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeVice, npc.whoAmI, -1f, npc.whoAmI, 0f, 150f, npc.target);

                                int heal = npc.lifeMax * 3 / 4 - npc.life;
                                npc.life = npc.lifeMax * 3 / 4;
                                if (heal > 0)
                                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);

                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                return;
                            }

                            if (Counter == 0) //this entire section becomes unnecessary at critical health (arms are basically immortal)
                            {
                                if (!NPC.AnyNPCs(NPCID.PrimeLaser))
                                    Counter = 4;
                                else if (!NPC.AnyNPCs(NPCID.PrimeSaw))
                                    Counter = 2;
                                else if (!NPC.AnyNPCs(NPCID.PrimeCannon))
                                    Counter = 1;
                                else if (!NPC.AnyNPCs(NPCID.PrimeVice))
                                    Counter = 3;
                            }
                            else
                            {
                                Timer++;

                                if (Timer >= 1500)
                                {
                                    Timer = 0;
                                    int n = 200;

                                    switch (Counter)
                                    {
                                        case 1:
                                            n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeCannon, npc.whoAmI, -1f, npc.whoAmI, 0f, 0f, npc.target);
                                            break;

                                        case 2:
                                            n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeSaw, npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, npc.target);
                                            break;

                                        case 3:
                                            n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeVice, npc.whoAmI, -1f, npc.whoAmI, 0f, 150f, npc.target);
                                            break;

                                        case 4:
                                            n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeLaser, npc.whoAmI, 1f, npc.whoAmI, 0f, 150f, npc.target);
                                            break;

                                        default:
                                            break;
                                    }

                                    if (n != 200)
                                    {
                                        Main.npc[n].life = Main.npc[n].lifeMax / 2;
                                        Main.npc[n].netUpdate = true;
                                    }

                                    Counter = 0;
                                }
                            }
                        }
                        else
                        {
                            if (!masoBool[1])
                            {
                                Counter++;
                                if (Counter >= 180)
                                {
                                    masoBool[1] = true;
                                    npc.netUpdate = true;
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeLaser, npc.whoAmI, -1f, npc.whoAmI, 0f, 150f, npc.target);
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeSaw, npc.whoAmI, -1f, npc.whoAmI, 0f, 0f, npc.target);
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeCannon, npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, npc.target);
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeVice, npc.whoAmI, 1f, npc.whoAmI, 0f, 150f, npc.target);
                                    Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                }
                            }

                            Timer++;
                            if (Timer >= 180)
                            {
                                Timer = Main.rand.Next(90);

                                if (npc.HasPlayerTarget) //skeleton commando rockets LUL
                                {
                                    Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                    speed.X += Main.rand.Next(-20, 21);
                                    speed.Y += Main.rand.Next(-20, 21);
                                    speed.Normalize();

                                    int damage = npc.damage * 2 / 7;

                                    Projectile.NewProjectile(npc.Center, 4f * speed, ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                    Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(10f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                    Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(-10f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);

                                    Main.PlaySound(SoundID.Item11, npc.Center);
                                }
                            }
                        }
                        break;

                    case 56: //alien queen
                        Timer++;
                        if (Timer >= 180)
                        {
                            Timer = Main.rand.Next(150);

                            Projectile.NewProjectile(npc.Center, Vector2.Zero, 578, 0, 0f, Main.myPlayer);
                        }
                        break;

                    case 57: //crawltipede tail
                        Counter++;
                        if (Counter >= 4)
                        {
                            Counter = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Vector2 distance = Main.player[t].Center - npc.Center;

                                if (distance.Length() < 300f)
                                {
                                    distance.Normalize();
                                    distance *= 6f;
                                    int p = Projectile.NewProjectile(npc.Center, distance, ProjectileID.FlamesTrap, npc.damage / 4, 0f, Main.myPlayer);
                                    Main.projectile[p].friendly = false;

                                    Main.PlaySound(SoundID.Item34, npc.Center);
                                }
                            }
                        }
                        break;

                    case 58: //nailhead
                        Counter++;
                        if (Main.netMode != 1 && Counter >= 90)
                        {
                            Counter = (short)Main.rand.Next(60);

                            //this entire block is fucked
                            int length = Main.rand.Next(3, 6);
                            int[] numArray = new int[length];
                            int maxValue = 0;
                            for (int index = 0; index < (int)byte.MaxValue; ++index)
                            {
                                if (Main.player[index].active && !Main.player[index].dead && Collision.CanHitLine(npc.position, npc.width, npc.height, Main.player[index].position, Main.player[index].width, Main.player[index].height))
                                {
                                    numArray[maxValue] = index;
                                    ++maxValue;
                                    if (maxValue == length)
                                        break;
                                }
                            }
                            if (maxValue > 1)
                            {
                                for (int index1 = 0; index1 < 100; ++index1)
                                {
                                    int index2 = Main.rand.Next(maxValue);
                                    int index3 = index2;
                                    while (index3 == index2)
                                        index3 = Main.rand.Next(maxValue);
                                    int num1 = numArray[index2];
                                    numArray[index2] = numArray[index3];
                                    numArray[index3] = num1;
                                }
                            }

                            Vector2 vector2_1 = new Vector2 (-1f, -1f);

                            for (int index = 0; index < maxValue; ++index)
                            {
                                Vector2 vector2_2 = Main.npc[numArray[index]].Center - npc.Center;
                                vector2_2.Normalize();
                                vector2_1 += vector2_2;
                            }

                            vector2_1.Normalize();

                            for (int index = 0; index < length; ++index)
                            {
                                float num1 = Main.rand.Next(8, 13);
                                Vector2 vector2_2 = new Vector2(Main.rand.Next(-100, 101), Main.rand.Next(-100, 101));
                                vector2_2.Normalize();

                                if (maxValue > 0)
                                {
                                    vector2_2 += vector2_1;
                                    vector2_2.Normalize();
                                }
                                vector2_2 *= num1;

                                if (maxValue > 0)
                                {
                                    --maxValue;
                                    vector2_2 = Main.player[numArray[maxValue]].Center - npc.Center;
                                    vector2_2.Normalize();
                                    vector2_2 *= num1;
                                }

                                Projectile.NewProjectile(npc.Center.X, npc.position.Y + npc.width / 4f, vector2_2.X, vector2_2.Y, ProjectileID.Nail, (int)(npc.damage * 0.15), 1f);
                            }
                        }
                        break;

                    case 59: //storm diver
                        if (Main.netMode != 1)
                        {
                            //default: if (npc.localAI[2] >= 360f + Main.rand.Next(360) && etc)
                            if (npc.localAI[2] >= 180f + Main.rand.Next(180) && npc.Distance(Main.player[npc.target].Center) < 400f && Math.Abs(npc.DirectionTo(Main.player[npc.target].Center).Y) < 0.5f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                            {
                                npc.localAI[2] = 0f;
                                Vector2 vector2_1 = npc.Center;
                                vector2_1.X += npc.direction * 30f;
                                vector2_1.Y += 2f;

                                Vector2 vec = npc.DirectionTo(Main.player[npc.target].Center) * 7f;
                                if (vec.HasNaNs())
                                    vec = new Vector2(npc.direction * 8f, 0f);

                                int Damage = Main.expertMode ? 50 : 75;
                                for (int index = 0; index < 4; ++index)
                                {
                                    Vector2 vector2_2 = vec + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                                    int p = Projectile.NewProjectile(vector2_1.X, vector2_1.Y, vector2_2.X, vector2_2.Y, ProjectileID.MoonlordBullet, Damage, 1f, Main.myPlayer);
                                    Main.projectile[p].hostile = true;
                                    Main.projectile[p].friendly = false;
                                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().masoProj = true;
                                }

                                Main.PlaySound(SoundID.Item36, npc.Center);
                            }
                        }
                        break;

                    case 60: //elf copter
                        if (npc.localAI[0] >= 14f)
                        {
                            npc.localAI[0] = 0f;

                            float num8 = Main.player[npc.target].Center.X - npc.Center.X;
                            float num9 = Main.player[npc.target].Center.Y - npc.Center.Y;
                            float num10 = num8 + Main.rand.Next(-35, 36);
                            float num11 = num9 + Main.rand.Next(-35, 36);
                            float num12 = num10 * (1f + Main.rand.Next(-20, 21) * 0.015f);
                            float num13 = num11 * (1f + Main.rand.Next(-20, 21) * 0.015f);
                            float num14 = 10f / (float)Math.Sqrt(num12 * num12 + num13 * num13);
                            float num15 = num12 * num14;
                            float num16 = num13 * num14;
                            float SpeedX = num15 * (1f + Main.rand.Next(-20, 21) * 0.0125f);
                            float SpeedY = num16 * (1f + Main.rand.Next(-20, 21) * 0.0125f);
                            int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, SpeedX, SpeedY, ProjectileID.ExplosiveBullet, 32, 0f, Main.myPlayer);
                            Main.projectile[p].hostile = true;
                            Main.projectile[p].friendly = false;
                        }
                        break;

                    case 61: //destroyer body/tail
                        if (npc.realLife != -1)
                        {
                            int cap = Main.npc[npc.realLife].lifeMax / Main.npc[npc.realLife].life;
                            if (cap > 0)
                            {
                                Counter += Main.rand.Next(2 + cap);
                                if (Counter >= Main.rand.Next(1400, 26000))
                                {
                                    Counter = 0;
                                    npc.localAI[0] = 26000f;
                                }
                            }
                            else
                            {
                                npc.life = 0;
                                npc.active = false;
                                //npc.checkDead();
                                return;
                            }

                            if (!masoBool[0] && Main.npc[npc.realLife].GetGlobalNPC<FargoGlobalNPC>().masoBool[0])
                            {
                                masoBool[0] = true;
                                masoHurtAI = 6;
                                int heal = Main.npc[npc.realLife].lifeMax * Main.rand.Next(100, 121) / 4 / 81 / 100; //sum 81 segments healing to regain 25% life, with up to +20% variance
                                Main.npc[npc.realLife].life += heal;
                                CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                        }

                        if (npc.ai[2] != 0) //if probe is released
                        {
                            Timer--;
                            if (Timer <= 0)
                            {
                                Timer = 900 + Main.rand.Next(900);
                                npc.ai[2] = 0;
                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 62: //tactical skeleton, num3 = 120, damage = 40/50, num8 = 0
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 65f)
                            {
                                for (int index = 0; index < 6; ++index)
                                {
                                    float num6 = Main.player[npc.target].Center.X - npc.Center.X;
                                    float num10 = Main.player[npc.target].Center.Y - npc.Center.Y;
                                    float num11 = 15f / (float)Math.Sqrt(num6 * num6 + num10 * num10);
                                    float num12;
                                    float num18 = num12 = num6 + Main.rand.Next(-40, 41);
                                    float num19;
                                    float num20 = num19 = num10 + Main.rand.Next(-40, 41);
                                    float SpeedX = num18 * num11;
                                    float SpeedY = num20 * num11;
                                    int damage = Main.expertMode ? 40 : 50;
                                    int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, SpeedX, SpeedY, ProjectileID.MeteorShot, damage, 0f, Main.myPlayer);
                                    Main.projectile[p].hostile = true;
                                    Main.projectile[p].friendly = false;
                                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().masoProj = true;
                                }

                                Main.PlaySound(SoundID.Item38, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.ai[3] = 0f; //specific to me

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 63: //skeleton sniper, num3 = 200, num8 = 0
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 105f)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-40, 41) * 0.2f;
                                speed.Y += Main.rand.Next(-40, 41) * 0.2f;
                                speed.Normalize();
                                speed *= 11f;

                                int damage = Main.expertMode ? 80 : 100;

                                int p = Projectile.NewProjectile(npc.Center, speed, ProjectileID.SniperBullet, damage, 0f, Main.myPlayer);
                                Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().masoProj = true;

                                Main.PlaySound(SoundID.Item40, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 64: //skeleton archer, damage = 28/35, ID.VenomArrow
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 40f)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.Y -= Math.Abs(speed.X) * 0.075f; //account for gravity (default *0.1f)
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();
                                speed *= 11f;

                                int damage = Main.expertMode ? 28 : 35;

                                int p = Projectile.NewProjectile(npc.Center, speed, ProjectileID.VenomArrow, damage, 0f, Main.myPlayer);
                                Main.projectile[p].hostile = true;
                                Main.projectile[p].friendly = false;
                                Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().masoProj = true;

                                Main.PlaySound(SoundID.Item5, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 65: //skeleton commando, num3 = 90, num5 = 4f, damage = 48/60, ID.RocketSkeleton
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 50f)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();

                                int damage = Main.expertMode ? 48 : 60;

                                Projectile.NewProjectile(npc.Center, 4f * speed, ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(10f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(-10f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);

                                Main.PlaySound(SoundID.Item11, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 66: //elf archer, num3 = 110, damage = 36/45, tsunami
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 60f)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.Y -= Math.Abs(speed.X) * 0.1f; //account for gravity
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();
                                Vector2 spinningpoint = speed;
                                speed *= 11f;

                                int damage = Main.expertMode ? 36 : 45;

                                //tsunami code lol
                                float num3 = 0.3141593f;
                                int num4 = 5;
                                spinningpoint *= 40f;
                                bool flag4 = Collision.CanHit(npc.Center, 0, 0, npc.Center + spinningpoint, 0, 0);
                                for (int index1 = 0; index1 < num4; ++index1)
                                {
                                    float num8 = index1 - (num4 - 1f) / 2f;
                                    Vector2 vector2_5 = spinningpoint.RotatedBy(num3 * num8, new Vector2());
                                    if (!flag4)
                                        vector2_5 -= spinningpoint;
                                    int p = Projectile.NewProjectile(npc.Center + vector2_5, speed, ProjectileID.ChlorophyteArrow, damage, 0f, Main.myPlayer);
                                    Main.projectile[p].hostile = true;
                                    Main.projectile[p].friendly = false;
                                    Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().masoProj = true;
                                }

                                Main.PlaySound(SoundID.Item5, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 67: //pirate crossbower, num3 = 80, num5 = 16f, num8 = Math.Abs(num7) * .08f, damage = 32/40, num12 = 800f?
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 45f)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();
                                speed *= 11f;

                                int damage = Main.expertMode ? 32 : 40;

                                int p = Projectile.NewProjectile(npc.Center, speed, ProjectileID.JestersArrow, damage, 0f, Main.myPlayer);
                                Main.projectile[p].hostile = true;
                                Main.projectile[p].friendly = false;

                                Main.PlaySound(SoundID.Item5, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 68: //pirate deadeye, num3 = 40, num5 = 14f, num8 = 0f, damage = 20/25, num12 = 550f?
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.ai[1] <= 25f)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();
                                speed *= 14f;

                                int damage = Main.expertMode ? 20 : 25;

                                int p = Projectile.NewProjectile(npc.Center, speed, ProjectileID.MeteorShot, damage, 0f, Main.myPlayer);
                                Main.projectile[p].hostile = true;
                                Main.projectile[p].friendly = false;

                                Main.PlaySound(SoundID.Item11, npc.Center);

                                npc.ai[2] = 0f;
                                npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 69: //pirate captain, 60 delay for cannonball, 8 for bullets
                        if (Main.netMode != 1)
                        {
                            if (npc.ai[2] > 0f && npc.localAI[2] >= 20f && npc.ai[1] <= 30)
                            {
                                //npc.localAI[2]++;

                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.Y -= Math.Abs(speed.X) * 0.2f; //account for gravity
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();

                                speed *= 11f;
                                npc.localAI[2] = 0f;
                                for (int i = 0; i < 15; i++)
                                {
                                    Vector2 cannonSpeed = speed;
                                    cannonSpeed.X += Main.rand.Next(-10, 11) * 0.3f;
                                    cannonSpeed.Y += Main.rand.Next(-10, 11) * 0.3f;
                                    Projectile.NewProjectile(npc.Center, cannonSpeed, ProjectileID.CannonballHostile, Main.expertMode ? 80 : 100, 0f, Main.myPlayer);
                                }

                                //npc.ai[2] = 0f;
                                //npc.ai[1] = 0f;

                                npc.netUpdate = true;
                            }
                        }
                        break;

                    case 70: //solar goop
                        Counter++;
                        if (Counter >= 300)
                        {
                            npc.life = 0;
                            npc.checkDead();
                            npc.active = false;
                        }

                        if (npc.HasPlayerTarget)
                        {
                            Vector2 speed = Main.player[npc.target].Center - npc.Center;
                            speed.Normalize();
                            speed *= 12f;

                            npc.velocity.X += speed.X / 100f;

                            if (npc.velocity.Length() > 16f)
                            {
                                npc.velocity.Normalize();
                                npc.velocity *= 16f;
                            }
                        }
                        else
                        {
                            npc.TargetClosest(false);
                        }
                        break;

                    case 71: //brain of cthulhu
                        if (!npc.dontTakeDamage) //vulnerable
                        {
                            npc.position += npc.velocity / 4f;
                        }
                        break;

                    case 72: //creeper
                        Timer++;
                        if (Timer >= 600)
                        {
                            Timer = Main.rand.Next(300);

                            if (npc.HasPlayerTarget && Main.netMode != 1 && Collision.CanHit(npc.position, npc.width, npc.height, Main.player[npc.target].position, Main.player[npc.target].width, Main.player[npc.target].head))
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.Y -= Math.Abs(speed.X) * 0.1f; //account for gravity
                                speed.X += Main.rand.Next(-10, 11);
                                speed.Y += Main.rand.Next(-30, 21);
                                speed.Normalize();
                                speed *= 10f;
                                Projectile.NewProjectile(npc.Center, speed, ProjectileID.GoldenShowerHostile, npc.damage / 4, 0f, Main.myPlayer);
                            }

                            npc.netUpdate = true;
                        }
                        break;
                        
                    case 73: //pixie
                        if(npc.HasPlayerTarget && Vector2.Distance(Main.player[npc.target].Center, npc.Center) < 200)
                        {
                            Counter++;
                        }
                        if(Counter >= 60)
                        {
                            Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Navi").WithVolume(1f).WithPitchVariance(.5f), npc.Center);
                            Counter = 0;
                        }
                        break;

                    case 74: //clown
                        if (!masoBool[0]) //roar when spawn
                        {
                            masoBool[0] = true;
                            Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);

                            if (Main.netMode == 0)
                                Main.NewText("A Clown has begun ticking!", 175, 75, byte.MaxValue);
                        }

                        Counter++;
                        if (Counter >= 240)
                        {
                            Counter = 0;

                            SharkCount++;
                            if (SharkCount >= 5)
                            {
                                npc.life = 0;
                                Main.PlaySound(npc.DeathSound, npc.Center);
                                npc.active = false;

                                if (Main.netMode != 1)
                                {
                                    for (int i = 0; i < 100; i++)
                                    {
                                        int type = ProjectileID.Dynamite;
                                        int damage = 250;
                                        float knockback = 8f;
                                        switch (Main.rand.Next(10))
                                        {
                                            case 0: break;
                                            case 1:
                                            case 2:
                                            case 3: type = ProjectileID.BouncyDynamite; knockback = 10f; break;
                                            case 4: type = ProjectileID.StickyDynamite; knockback = 10f; break;
                                            case 5: type = ProjectileID.Bomb; damage = 100; break;
                                            case 6:
                                            case 7:
                                            case 8: type = ProjectileID.BouncyBomb; damage = 100; break;
                                            case 9: type = ProjectileID.StickyBomb; damage = 100; break;
                                            case 10: type = ProjectileID.Grenade; damage = 60; break;
                                            case 11:
                                            case 12:
                                            case 13: type = ProjectileID.BouncyGrenade; damage = 60; break;
                                            case 14: type = ProjectileID.StickyGrenade; damage = 60; break;
                                        }

                                        int p = Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height), Main.rand.Next(-1000, 1001) / 100f, Main.rand.Next(-2000, 101) / 100f, type, damage, knockback, Main.myPlayer);
                                        Main.projectile[p].timeLeft += Main.rand.Next(180);
                                    }

                                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType<NukeProj>(), 250, 20f, Main.myPlayer);
                                }

                                if (Main.netMode == 0)
                                    Main.NewText("A Clown has exploded!", 175, 75, byte.MaxValue);
                            }
                        }
                        break;

                    case 75: //paladin
                        foreach (NPC n in Main.npc.Where(n => n.active && !n.friendly && n.type != NPCID.Paladin && n.Distance(npc.Center) < 800f))
                        {
                            n.GetGlobalNPC<FargoGlobalNPC>().PaladinsShield = true;

                            if (Main.rand.Next(2) == 0)
                            {
                                int d = Dust.NewDust(n.position, n.width, n.height, 246, 0f, -1.5f, 0, new Color());
                                Main.dust[d].velocity *= 0.5f;
                                Main.dust[d].noLight = true;
                            }
                        }
                        break;

                    case 76: //mimic (normal)
                        npc.dontTakeDamage = false;

                        if (npc.justHit)
                            Counter = 20;

                        if (Counter != 0)
                        {
                            Counter--;
                            npc.dontTakeDamage = true;
                        }
                        break;

                    case 77: //all prime limbs
                        if (!masoBool[0])
                        {
                            if (BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && Main.npc[primeBoss].GetGlobalNPC<FargoGlobalNPC>().masoBool[0])
                            {
                                masoBool[0] = true;
                                npc.defDefense = 9999;
                                npc.defense = 9999;
                                int heal = npc.lifeMax - npc.life;
                                npc.life = npc.lifeMax;
                                npc.damage = npc.damage * 3 / 2;
                                if (heal > 0)
                                    CombatText.NewText(new Rectangle((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height), CombatText.HealLife, heal, false, false);
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            npc.position += npc.velocity;
                        }
                        break;

                    /* pseudo memes

                    case unicorn
                    if counter == blah
                    NewProjectile(Rainbow gun, 0 velocity) //trail of rainbow, possibly adjust rotation if it jumps or whatever?

                     case moth:
                    Shoot(Spores)

                    case flying fish:
                    swarm spawn

                    case umbrealla slime:
                    fall slow and explodes water EVERYWHERE instantly flooding a world

                    case nimbus:
                    Shoot(Lightning)

                    case bats: swarm spawn

                    case vulture: 
                    swarm if player.life < 25%

                    case spike ball: 
                    some AI = faster speed ?


                    case harpy:
                    summon tornado, cthulunado recolor without shark spawn

                    case jellyfish:
                    if ai == electric thing && player.same water
                    player.AddBuff(BuffID.Electrocute);

                    case demon eye
                    if counter == blah
                    pre AI return false for a few frames 
                    Shoot(petrify beam) new projectile or could use an existing one with special property or could steal medusa code and see wtf happens there
                    npc.velocity go backward a bit (beam knockback )

                    case fire imp fire ball:
                    if fire imp is within like 2 pixels 

                    newNPC(fire ball slight rotate velocity)
                    newNPC(fire ball, slight rotate velocity)
                    transform = false

                    case angry bones:
                    count(NPCID.AngryBones) something something 

                    case turtles: 
                    if frame = in shell 
                    reflect the proj

                    */

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
            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
            if (t == -1)
                return;

            Player player = Main.player[t];

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
                switch (SharkCount)
                {
                    case 253:
                        drawColor.R = 255;
                        drawColor.G /= 2;
                        drawColor.B /= 2;
                        break;

                    case 254:
                        drawColor.R /= 2;
                        drawColor.G = 255;
                        drawColor.B /= 2;
                        break;

                    default:
                        drawColor.R = (byte)(SharkCount * 20 + 155);
                        drawColor.G /= (byte)(SharkCount + 1);
                        drawColor.B /= (byte)(SharkCount + 1);
                        break;
                }

                return drawColor;
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
                    switch (npc.type)
                    {
                        case NPCID.SlimeSpiked:
                            if (BossIsAlive(ref slimeBoss, NPCID.KingSlime))
                            {
                                npc.Transform(NPCID.KingSlime);
                                npc.velocity.Y = -20f;
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                            break;

                        case NPCID.WanderingEye:
                            if (!BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu))
                            {
                                npc.Transform(NPCID.EyeofCthulhu);
                                npc.velocity.Y = -5f;
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                            break;

                        case NPCID.PigronCorruption:
                        case NPCID.PigronCrimson:
                        case NPCID.PigronHallow:
                            if (!BossIsAlive(ref fishBoss, NPCID.DukeFishron))
                            {
                                npc.velocity = npc.Center - target.Center;
                                npc.velocity.Normalize();
                                npc.velocity *= 2.5f;
                                npc.Transform(NPCID.DukeFishron);
                            }
                            break;

                        default:
                            break;
                    }
                }

                if (npc.netID == NPCID.Pinky && !target.noKnockback)
                {
                    Vector2 velocity = Vector2.Normalize(target.Center - npc.Center) * 30;
                    target.velocity = velocity;
                }

                if (npc.type == NPCID.BoneLee)
                {
                    target.velocity.X = npc.velocity.Length() * npc.direction;
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

                /*if(npc.type == NPCID.Golem || npc.type == NPCID.GolemFistLeft || npc.type == NPCID.GolemFistRight || npc.type == NPCID.GolemHead)
                {
                    npc.lifeRegen += npc.lifeMax / 15;
                }*/
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
					
					int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 40, 0f + Main.rand.Next(-5, 5), -5f, mod.ProjectileType<Projectiles.Souls.SuperBlood>(), dmg, 0f, Main.myPlayer);

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
            if (FargoWorld.MasochistMode)
			{
                //layers
                int y = spawnInfo.spawnTileY;
                bool cavern = y >= Main.maxTilesY * 0.4f && y <= Main.maxTilesY * 0.8f;
                bool underground = y > Main.worldSurface && y <= Main.maxTilesY * 0.4f;
                bool surface = y < Main.worldSurface && !spawnInfo.sky;
                bool wideUnderground = cavern || underground;
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
                bool noInvasion = Fargowiltas.NoInvasion(spawnInfo);
                bool normalSpawn = !spawnInfo.playerInTown && noInvasion;



                //all the pre hardmode
                if (!Main.hardMode)
				{
                    //mutually exclusive world layers
                    if (surface)
                    {
                        if (day)
                        {
                            if (Main.slimeRain && NPC.downedSlimeKing)
                            {
                                pool[NPCID.KingSlime] = BossIsAlive(ref slimeBoss, NPCID.KingSlime) ? .01f : 0.04f;
                            }
                        }
                        else //night
                        {
                            if (noBiome)
                            {
                                pool[NPCID.CorruptBunny] = .1f;
                                pool[NPCID.CrimsonBunny] = .1f;
                            }

                            if (snow)
                            {
                                pool[NPCID.CorruptPenguin] = .1f;
                                pool[NPCID.CrimsonPenguin] = .1f;
                            }

                            if (ocean)
                            {
                                pool[NPCID.CorruptGoldfish] = .1f;
                                pool[NPCID.CrimsonGoldfish] = .1f;
                            }

                            if (normalSpawn && NPC.downedBoss1)
                            {
                                if (jungle)
                                {
                                    pool[NPCID.DoctorBones] = .05f;
                                }

                                if (NPC.downedBoss3)
                                {
                                    if (Main.bloodMoon)
                                    {
                                        pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .0025f : .01f;
                                    }
                                    else
                                    {
                                        pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .001f : .004f;
                                    }
                                }
                            }
                        }
                    }
                    else if (wideUnderground)
                    {
                        if (nearLava)
                        {
                            pool[NPCID.FireImp] = .1f;
                            pool[NPCID.LavaSlime] = .1f;
                        }

                        if (marble && NPC.downedBoss2)
                        {
                            pool[NPCID.Medusa] = .1f;
                        }

                        if (granite)
                        {
                            pool[NPCID.GraniteFlyer] = .1f;
                            pool[NPCID.GraniteGolem] = .1f;
                        }

                        if (cavern)
                        {
                            if (noBiome && NPC.downedBoss3)
                            {
                                pool[NPCID.DarkCaster] = .1f;
                            }
                        }
                    }
                    else if (underworld)
                    {
                        pool[NPCID.LeechHead] = .05f;
                    }
                    else if (sky)
                    {
                        pool[NPCID.AngryNimbus] = .05f;
                    }



                    //height-independent biomes
                    if (corruption)
                    {
                        if (NPC.downedBoss2)
                        {
                            pool[NPCID.SeekerHead] = .01f;

                            if (normalSpawn && NPC.downedBoss3 && NPC.downedQueenBee && !underworld)
                            {
                                pool[NPCID.EaterofWorldsHead] = BossIsAlive(ref eaterBoss, NPCID.EaterofWorldsHead) ? .00125f : .005f;
                            }
                        }
                    }

                    if (crimson)
                    {
                        if (NPC.downedBoss2)
                        {
                            pool[NPCID.IchorSticker] = .01f;

                            if (normalSpawn && NPC.downedBoss3 && NPC.downedQueenBee && !underworld)
                            {
                                pool[NPCID.BrainofCthulhu] = BossIsAlive(ref brainBoss, NPCID.BrainofCthulhu) ? .00125f : .005f;
                            }
                        }
                    }

                    if (mushroom)
                    {
                        pool[NPCID.FungiBulb] = .1f;
                        pool[NPCID.MushiLadybug] = .1f;
                        pool[NPCID.ZombieMushroom] = .1f;
                        pool[NPCID.ZombieMushroomHat] = .1f;
                        pool[NPCID.AnomuraFungus] = .1f;
                    }

                    /*if(Fargowiltas.NormalSpawn(spawnInfo) && NPC.downedMechBoss2 && !surface)
                    {
                        pool[NPCID.Mimic] = .01f;
                    }*/

                }
                else //all the hardmode
                {
                    //mutually exclusive world layers
                    if (surface)
                    {
                        if (day)
                        {
                            if (NPC.downedMechBossAny)
                            {
                                pool[NPCID.CultistArcherWhite] = .05f;
                            }

                            if (noBiome)
                            {
                                pool[NPCID.KingSlime] = BossIsAlive(ref slimeBoss, NPCID.KingSlime) ? .01f : .04f;
                            }

                            if (Main.slimeRain && normalSpawn)
                            {
                                pool[NPCID.KingSlime] = BossIsAlive(ref slimeBoss, NPCID.KingSlime) ? .025f : .1f;
                            }
                        }
                        else //night
                        {
                            if (Main.bloodMoon)
                            {
                                pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .0125f : .05f;

                                if (NPC.downedPlantBoss)
                                {
                                    if (!BossIsAlive(ref retiBoss, NPCID.Retinazer) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref destroyBoss, NPCID.TheDestroyer))
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
                                else if (normalSpawn && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                                {
                                    if (!BossIsAlive(ref retiBoss, NPCID.Retinazer) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref destroyBoss, NPCID.TheDestroyer))
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
                            }
                            else //not blood moon
                            {
                                pool[NPCID.Clown] = 0.01f;

                                if (normalSpawn)
                                {
                                    pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .005f : .02f;

                                    if (NPC.downedPlantBoss) //GODLUL
                                    {
                                        if (!BossIsAlive(ref retiBoss, NPCID.Retinazer) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref destroyBoss, NPCID.TheDestroyer))
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
                                }

                                if (NPC.downedPlantBoss)
                                {
                                    pool[NPCID.SkeletonSniper] = .05f;
                                    pool[NPCID.SkeletonCommando] = .05f;
                                    pool[NPCID.TacticalSkeleton] = .05f;
                                }
                            }

                            if (NPC.downedMechBossAny)
                            {
                                #region night pumpkin moon, frost moon
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

                                    if (NPC.downedHalloweenKing)
                                    {
                                        //pool[NPCID.HeadlessHorseman] = .01f;
                                        pool[NPCID.Pumpking] = .005f;
                                    }
                                }
                                else //in some biome
                                {
                                    if (hallow)
                                    {
                                        pool[NPCID.PresentMimic] = .05f;
                                        pool[NPCID.GingerbreadMan] = .05f;
                                    }
                                    else if (crimson || corruption)
                                    {
                                        pool[NPCID.Splinterling] = .05f;

                                        if (NPC.downedHalloweenTree)
                                        {
                                            pool[NPCID.MourningWood] = .005f;
                                        }
                                    }

                                    if (snow)
                                    {
                                        pool[NPCID.ZombieElf] = .02f;
                                        pool[NPCID.ZombieElfBeard] = .02f;
                                        pool[NPCID.ZombieElfGirl] = .02f;
                                        pool[NPCID.Yeti] = .01f;

                                        pool[NPCID.ElfArcher] = .05f;
                                        pool[NPCID.ElfCopter] = .01f;

                                        if (NPC.downedChristmasTree)
                                        {
                                            pool[NPCID.Everscream] = .005f;
                                        }

                                        if (NPC.downedChristmasSantank)
                                        {
                                            pool[NPCID.SantaNK1] = .005f;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        if (hallow)
                        {
                            pool[NPCID.RainbowSlime] = .01f;
                        }

                        if (snow)
                        {
                            pool[NPCID.IceGolem] = .01f;
                        }

                        if (ocean)
                        {
                            if (NPC.downedFishron)
                            {
                                pool[NPCID.DukeFishron] = .005f;
                            }
                        }
                        else if (desert)
                        {
                            pool[NPCID.DesertBeast] = .05f;
                        }
                    }
                    else if (wideUnderground)
                    {
                        if (NPC.downedMechBossAny)
                        {
                            if (spawnInfo.player.ZoneDungeon && night && normalSpawn)
                            {
                                pool[NPCID.SkeletronHead] = BossIsAlive(ref skeleBoss, NPCID.SkeletronHead) ? .00125f : .005f;
                            }

                            if (snow && !Main.dayTime) //frost moon underground
                            {
                                if (underground)
                                {
                                    pool[NPCID.Nutcracker] = .05f;
                                }

                                if (cavern)
                                {
                                    pool[NPCID.Krampus] = .05f;

                                    if (NPC.downedChristmasIceQueen)
                                    {
                                        pool[NPCID.IceQueen] = .005f;
                                    }
                                }
                            }
                        }

                        if (NPC.downedPlantBoss)
                        {
                            pool[NPCID.DiabolistRed] = .004f;
                            pool[NPCID.DiabolistWhite] = .004f;
                            pool[NPCID.Necromancer] = .004f;
                            pool[NPCID.NecromancerArmored] = .004f;
                            pool[NPCID.RaggedCaster] = .004f;
                            pool[NPCID.RaggedCasterOpenCoat] = .004f;
                        }
                    }
                    else if (underworld)
                    {
                        if (NPC.downedPlantBoss)
                        {
                            pool[NPCID.DiabolistRed] = .004f;
                            pool[NPCID.DiabolistWhite] = .004f;
                            pool[NPCID.Necromancer] = .004f;
                            pool[NPCID.NecromancerArmored] = .004f;
                            pool[NPCID.RaggedCaster] = .004f;
                            pool[NPCID.RaggedCasterOpenCoat] = .004f;
                        }

                        if (NPC.downedGolemBoss)
                        {
                            pool[NPCID.DD2Betsy] = .005f;
                        }
                    }
                    else if (sky)
                    {
                        if (normalSpawn)
                        {
                            if (NPC.downedGolemBoss)
                            {
                                pool[NPCID.SolarCrawltipedeHead] = .03f;
                                pool[NPCID.VortexHornetQueen] = .03f;
                                pool[NPCID.NebulaBrain] = .03f;
                                pool[NPCID.StardustJellyfishBig] = .03f;
                                pool[NPCID.AncientCultistSquidhead] = .03f;
                                pool[NPCID.CultistDragonHead] = .03f;
                            }
                            else
                            {
                                pool[NPCID.SolarCrawltipedeHead] = .01f;
                                pool[NPCID.VortexHornetQueen] = .01f;
                                pool[NPCID.NebulaBrain] = .01f;
                                pool[NPCID.StardustJellyfishBig] = .01f;
                            }
                        }
                    }


                    //height-independent biomes
                    if (corruption)
                    {
                        if (normalSpawn)
                        {
                            pool[NPCID.EaterofWorldsHead] = BossIsAlive(ref eaterBoss, NPCID.EaterofWorldsHead) ? .0025f : .01f;
                        }
                    }

                    if (crimson)
                    {
                        if (normalSpawn)
                        {
                            pool[NPCID.BrainofCthulhu] = BossIsAlive(ref brainBoss, NPCID.BrainofCthulhu) ? .0025f : .01f;
                        }
                    }

                    if (jungle)
                    {
                        if (day)
                        {
                            if (normalSpawn && NPC.downedMechBossAny)
                            {
                                pool[NPCID.QueenBee] = BossIsAlive(ref beeBoss, NPCID.QueenBee) ? .00125f : .005f;
                            }
                        }

                        if (!surface)
                        {
                            pool[NPCID.BigMimicJungle] = .01f;

                            if (NPC.downedGolemBoss)
                            {
                                pool[NPCID.Plantera] = BossIsAlive(ref NPC.plantBoss, NPCID.Plantera) ? 0.00125f : .005f;
                            }
                        }
                    }

                    if (meteor && NPC.downedGolemBoss)
                    {
                        pool[NPCID.SolarCorite] = .025f;
                    }
                }

                //maybe make moon lord core masoAI handle these spawns...?
                if (BossIsAlive(ref moonBoss, NPCID.MoonLordCore))
                {
                    FargoGlobalNPC modNPC = Main.npc[moonBoss].GetGlobalNPC<FargoGlobalNPC>();
                    if (modNPC.masoBool[0])
                    {
                        pool[NPCID.SolarCrawltipedeHead] = 1f;
                        pool[NPCID.StardustJellyfishBig] = 3f;
                        pool[NPCID.VortexRifleman] = 3f;
                        pool[NPCID.NebulaBrain] = 2f;

                        //pool[NPCID.AncientCultistSquidhead] = 3f;
                        //pool[NPCID.CultistDragonHead] = .5f;
                    }
                }
            }
		
			
		
		}
		
		public override bool PreNPCLoot (NPC npc)
		{
            return dropLoot;
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
                            if (eyes[i] != 200)
                            {
                                Main.npc[eyes[i]].velocity = new Vector2(Main.rand.Next(-3, 3), Main.rand.Next(-3, 3));
                            }
                        }
                        break;

                    case 2: //goblins
                        Projectile ball = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-5, 6), -5), ProjectileID.SpikyBall, 15, 0, Main.myPlayer);
                        ball.hostile = true;
                        ball.friendly = false;
                        ball.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                        break;

                    case 3: //angry bones
                        if (Main.rand.Next(5) == 0)
                        {
                            NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.CursedSkull);
                        }
                        break;

                    case 4: //dungeon slime
                        if (NPC.downedPlantBoss)
                        {
                            int paladin = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.Paladin);

                            if (paladin != 200)
                            {
                                Vector2 center = Main.npc[paladin].Center;
                                Main.npc[paladin].width = (int)(Main.npc[paladin].width * .65f);
                                Main.npc[paladin].height = (int)(Main.npc[paladin].height * .65f);
                                Main.npc[paladin].scale = .65f;
                                Main.npc[paladin].Center = center;
                                Main.npc[paladin].defense /= 2;
                            }
                        }
                        break;

                    case 5: //yellow slime
                        for (int i = 0; i < 2; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);

                            if (spawn != 200)
                            {
                                Main.npc[spawn].SetDefaults(NPCID.PurpleSlime);
                                Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                                Main.npc[spawn].velocity.Y = npc.velocity.Y;

                                NPC spawn2 = Main.npc[spawn];
                                spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                                NPC spawn3 = Main.npc[spawn];
                                spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                                Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                            }
                        }
                        break;

                    case 6: //purple slime
                        for (int i = 0; i < 2; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);

                            if (spawn != 200)
                            {
                                Main.npc[spawn].SetDefaults(NPCID.RedSlime);
                                Main.npc[spawn].velocity.X = npc.velocity.X * 2f;
                                Main.npc[spawn].velocity.Y = npc.velocity.Y;

                                NPC spawn2 = Main.npc[spawn];
                                spawn2.velocity.X = spawn2.velocity.X + (Main.rand.Next(-20, 20) * 0.1f + i * npc.direction * 0.3f);
                                NPC spawn3 = Main.npc[spawn];
                                spawn3.velocity.Y = spawn3.velocity.Y - (Main.rand.Next(0, 10) * 0.1f + i);
                                Main.npc[spawn].ai[0] = -1000 * Main.rand.Next(3);
                            }
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
                            npc.life = npc.lifeMax / 1000;
                            if (npc.life < 100)
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
                        if (npc.ai[0] <= 9)
                        {
                            masoBool[1] = true;
                            npc.life = 1;
                            npc.dontTakeDamage = true;

                            for (int index1 = 0; index1 < 100; ++index1) //gross vanilla dodge dust
                            {
                                int index2 = Dust.NewDust(npc.position, npc.width, npc.height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
                                Main.dust[index2].position.X += Main.rand.Next(-20, 21);
                                Main.dust[index2].position.Y += Main.rand.Next(-20, 21);
                                Dust dust = Main.dust[index2];
                                dust.velocity *= 0.5f;
                                Main.dust[index2].scale *= 1f + Main.rand.Next(50) * 0.01f;
                                //Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(npc.cWaist, npc);
                                if (Main.rand.Next(2) == 0)
                                {
                                    Main.dust[index2].scale *= 1f + Main.rand.Next(50) * 0.01f;
                                    Main.dust[index2].noGravity = true;
                                }
                            }

                            for (int i = 0; i < 5; i++) //gross vanilla dodge dust
                            {
                                int index3 = Gore.NewGore(npc.position + new Vector2(Main.rand.Next(npc.width), Main.rand.Next(npc.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                                Main.gore[index3].scale = 2f;
                                Main.gore[index3].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index3].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index3].velocity *= 0.5f;

                                int index4 = Gore.NewGore(npc.position + new Vector2(Main.rand.Next(npc.width), Main.rand.Next(npc.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                                Main.gore[index4].scale = 2f;
                                Main.gore[index4].velocity.X = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index4].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index4].velocity *= 0.5f;

                                int index5 = Gore.NewGore(npc.position + new Vector2(Main.rand.Next(npc.width), Main.rand.Next(npc.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                                Main.gore[index5].scale = 2f;
                                Main.gore[index5].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index5].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index5].velocity *= 0.5f;

                                int index6 = Gore.NewGore(npc.position + new Vector2(Main.rand.Next(npc.width), Main.rand.Next(npc.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                                Main.gore[index6].scale = 2f;
                                Main.gore[index6].velocity.X = 1.5f - Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index6].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index6].velocity *= 0.5f;

                                int index7 = Gore.NewGore(npc.position + new Vector2(Main.rand.Next(npc.width), Main.rand.Next(npc.height)), new Vector2(), Main.rand.Next(61, 64), 1f);
                                Main.gore[index7].scale = 2f;
                                Main.gore[index7].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index7].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
                                Main.gore[index7].velocity *= 0.5f;
                            }

                            return false;
                        }
                        else if (FargoWorld.FishronCount < 120)
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
                            slime.width = (int)(slime.width / slime.scale);
                            slime.height = (int)(slime.height / slime.scale);
                            slime.scale = 1f;
                            slime.Center = center;

                            slime.lifeMax /= 5;
                            slime.life = slime.lifeMax;

                            slime.GetGlobalNPC<FargoGlobalNPC>().masoDeathAI = 0;
                            slime.velocity = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 1));
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

                    case 26: //flying snake
                        masoDeathAI = 0;
                        SharkCount = 1;
                        npc.life = npc.lifeMax;
                        npc.damage = npc.damage * 3 / 2;
                        return false;

                    case 27: //lihzahrd
                        for (int i = 0; i < 3; i++)
                        {
                            int p = Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(21)), ProjectileID.SpikyBallTrap, 30, 0f, Main.myPlayer);
                            Main.projectile[p].friendly = false;
                        }
                        break;

                    case 28: //stardust enemies
                        if (NPC.TowerActiveStardust)
                        {
                            if (NPC.CountNPCS(NPCID.StardustCellSmall) < 10)
                            {
                                for (int i = 0; i < 3; i++)
                                {
                                    int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.StardustCellSmall);
                                    Main.npc[n].velocity.X = Main.rand.Next(-10, 11);
                                    Main.npc[n].velocity.Y = Main.rand.Next(-10, 11);
                                }
                            }

                            if (Main.rand.Next(3) == 0) //die without contributing to pillar shield
                            {
                                Main.PlaySound(npc.DeathSound, npc.Center);
                                npc.active = false;
                                return false;
                            }
                        }
                        break;

                    case 29: //drakanian
                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                        if (t != -1 && Main.player[t].active && !Main.player[t].dead)
                        {
                            Vector2 velocity = Main.player[t].Center - npc.Center;
                            velocity.Normalize();
                            velocity *= 12f;

                            int p = Projectile.NewProjectile(npc.Center, velocity, ProjectileID.Daybreak, (int)(npc.damage * 0.4), 1f, Main.myPlayer);
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].melee = false;
                            Main.projectile[p].hostile = true;

                            Main.PlaySound(SoundID.Item1, npc.Center);
                        }

                        npc.Transform(NPCID.SolarSolenian);
                        return false;

                    case 30: //nebula floater
                        int t2 = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                        if (t2 != -1 && Main.player[t2].active && !Main.player[t2].dead)
                        {
                            Player target = Main.player[npc.target];
                            Vector2 boltVel = target.Center - npc.Center;
                            boltVel.Normalize();
                            boltVel *= 9;

                            for (int i = 0; i < (int)npc.localAI[2] / 60; i++)
                            {
                                Vector2 spawnPos = npc.position;
                                spawnPos.X += Main.rand.Next(npc.width);
                                spawnPos.Y += Main.rand.Next(npc.height);

                                Vector2 boltVel2 = boltVel.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-30, 31)));
                                boltVel2 *= Main.rand.NextFloat(0.8f, 1.2f);

                                Projectile.NewProjectile(spawnPos, boltVel2, ProjectileID.NebulaLaser, 48, 0f, Main.myPlayer);
                            }
                        }
                        break;

                    case 31: //alien hornet
                        Main.PlaySound(npc.DeathSound, npc.Center); //die without contributing to pillar shield
                        npc.active = false;
                        return false;

                    case 32: //brain suckler
                        if (npc.ai[0] == 5f) //latched on player
                        {
                            npc.Transform(NPCID.NebulaBrain);
                        }
                        else //die without contributing to pillar shield
                        {
                            Main.PlaySound(npc.DeathSound, npc.Center);
                            npc.active = false;
                        }
                        return false;

                    case 33: //skeletron hand
                        NPC head = Main.npc[(int)npc.ai[1]];
                        if (head.active && head.type == NPCID.SkeletronHead && head.GetGlobalNPC<FargoGlobalNPC>().Counter == 0)
                        {
                            if (npc.ai[0] == 1f)
                                head.GetGlobalNPC<FargoGlobalNPC>().Counter = 1;
                            else
                                head.GetGlobalNPC<FargoGlobalNPC>().Counter = 2;

                            //Main.NewText("hand dead");
                        }
                        break;

                    case 34: //eater of worlds body
                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.EaterofSouls);
                        break;

                    case 35: //goblin summoner
                        for (int i = 0; i < 50; i++)
                        {
                            Projectile balls = Projectile.NewProjectileDirect(npc.Center, new Vector2(Main.rand.Next(-500, 501) / 100f, Main.rand.Next(-1000, 1) / 100f), ProjectileID.SpikyBall, npc.damage / 8, 0, Main.myPlayer);
                            balls.hostile = true;
                            balls.friendly = false;
                            balls.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                        }
                        break;

                    case 36: //clown
                        for (int i = 0; i < 30; i++)
                        {
                            int p = Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height), Main.rand.Next(-500, 501) / 100f, Main.rand.Next(-1000, 101) / 100f, ProjectileID.BouncyGrenade, 200, 8f, Main.myPlayer);
                            Main.projectile[p].timeLeft -= Main.rand.Next(120);
                        }
                        break;
			
			/* pseudo memes
			case: slime zombie
			NewNPC(Slime)
			
			case skeletons:
			for()
			{
			NewProjectile(Bones)
			}
			
			case digger:
			NewNPC(GiantWorm)
			
			case ichor sticker:
			for()
			{
			NewProjectile(Ichor)
			}
			
			*/

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

                    case 1: //salamander
                        masoHurtAI = 0;
                        npc.Opacity *= 25;
                        break;

                    case 2: //giant shelly
                        player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was impaled by a Giant Shelly."), damage / 4, 0);
                        break;

                    case 3: //bone lee
                        if (Main.rand.Next(10) == 0)
                        {
                            Vector2 teleportTarget = player.Center;
                            float offset = 100f * -player.direction;
                            teleportTarget.X += offset;
                            teleportTarget.Y -= 50f;

                            if (!Collision.CanHit(teleportTarget, 1, 1, player.position, player.width, player.height))
                            {
                                teleportTarget.X -= offset * 2f;

                                if (!Collision.CanHit(teleportTarget, 1, 1, player.position, player.width, player.height))
                                    break;
                            }

                            GrossVanillaDodgeDust(npc);

                            npc.Center = teleportTarget;

                            GrossVanillaDodgeDust(npc);
                        }
                        break;

                    case 4: //martian officer shield
                        if (Main.rand.Next(2) == 0)
                        {
                            Vector2 velocity = player.Center - npc.Center;
                            velocity.Normalize();
                            velocity *= 14f;
                            int Damage = Main.expertMode ? 28 : 35;
                            Projectile.NewProjectile(npc.Center, velocity, ProjectileID.MartianTurretBolt, Damage, 0f, Main.myPlayer);
                        }
                        break;

                    case 5: //selenian
                        if (npc.ai[2] <= -6f)
                        {
                            damage = 0;
                            npc.ai[2] = -6f;
                        }
                        break;

                    case 6: //desu body/tail
                        damage /= 2;
                        break;

                    case 7: //golem fists
                        break;

                    case 8: //skelly prime DR
                        int armCount = 0;
                        bool[] arms = { NPC.AnyNPCs(NPCID.PrimeCannon), NPC.AnyNPCs(NPCID.PrimeLaser), NPC.AnyNPCs(NPCID.PrimeSaw), NPC.AnyNPCs(NPCID.PrimeVice) };

                        for (int i = 0; i < 4; i++)
                        {
                            if (arms[i])
                            {
                                armCount++;
                            }
                        }

                        switch (armCount)
                        {
                            case 4:
                                damage /= 20;
                                break;
                            case 3:
                                damage /= 10;
                                break;
                            case 2:
                                damage /= 4;
                                break;
                            case 1:
                                damage /= 2;
                                break;
                            default:
                                break;
                        }
                        break;

                    case 9: //brain of cthulhu
                        if (!player.HasBuff(BuffID.Confused) && Main.rand.Next(10) == 0)
                            player.AddBuff(BuffID.Confused, Main.rand.Next(30));
                        break;

                    case 10: //ice tortoise
                        float reduction = (float)npc.life / npc.lifeMax;
                        if (reduction < 0.25f)
                            reduction = 0.25f;
                        damage = (int)(damage * reduction);
                        break;

                    default:
                        break;
                }
            }
        }

        private void GrossVanillaDodgeDust(NPC npc)
        {
            for (int index1 = 0; index1 < 100; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 31, 0.0f, 0.0f, 100, new Color(), 2f);
                Main.dust[index2].position.X += Main.rand.Next(-20, 21);
                Main.dust[index2].position.Y += Main.rand.Next(-20, 21);
                Dust dust = Main.dust[index2];
                dust.velocity *= 0.4f;
                Main.dust[index2].scale *= 1f + Main.rand.Next(40) * 0.01f;
                //Main.dust[index2].shader = GameShaders.Armor.GetSecondaryShader(npc.cWaist, npc);
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[index2].scale *= 1f + Main.rand.Next(40) * 0.01f;
                    Main.dust[index2].noGravity = true;
                }
            }

            int index3 = Gore.NewGore(new Vector2(npc.Center.X - 24, npc.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index3].scale = 1.5f;
            Main.gore[index3].velocity.X = Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index3].velocity.Y = Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index3].velocity *= 0.4f;

            int index4 = Gore.NewGore(new Vector2(npc.Center.X - 24, npc.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index4].scale = 1.5f;
            Main.gore[index4].velocity.X = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index4].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index4].velocity *= 0.4f;

            int index5 = Gore.NewGore(new Vector2(npc.Center.X - 24, npc.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index5].scale = 1.5f;
            Main.gore[index5].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index5].velocity.Y = 1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index5].velocity *= 0.4f;

            int index6 = Gore.NewGore(new Vector2(npc.Center.X - 24, npc.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index6].scale = 1.5f;
            Main.gore[index6].velocity.X = 1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index6].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index6].velocity *= 0.4f;

            int index7 = Gore.NewGore(new Vector2(npc.Center.X - 24, npc.Center.Y - 24), new Vector2(), Main.rand.Next(61, 64), 1f);
            Main.gore[index7].scale = 1.5f;
            Main.gore[index7].velocity.X = -1.5f - Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index7].velocity.Y = -1.5f + Main.rand.Next(-50, 51) * 0.01f;
            Main.gore[index7].velocity *= 0.4f;
        }

        public override void ModifyHitByProjectile (NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (FargoWorld.MasochistMode)
            {
                switch (masoHurtAI)
                {
                    case 0:
                        break;

                    case 1: //salamander
                        masoHurtAI = 0;
                        npc.Opacity *= 25;
                        break;

                    case 2: //giant shelly
                        break;

                    case 3: //bone lee
                        if (Main.rand.Next(10) == 0 && npc.HasPlayerTarget)
                        {
                            Player target = Main.player[npc.target];
                            if (target.active && !target.dead)
                            {
                                Vector2 teleportTarget = target.Center;
                                float offset = 100f * -target.direction;
                                teleportTarget.X += offset;
                                teleportTarget.Y -= 50f;

                                if (!Collision.CanHit(teleportTarget, 1, 1, target.position, target.width, target.height))
                                {
                                    teleportTarget.X -= offset * 2f;

                                    if (!Collision.CanHit(teleportTarget, 1, 1, target.position, target.width, target.height))
                                        break;
                                }
                                
                                GrossVanillaDodgeDust(npc);

                                npc.Center = teleportTarget;

                                GrossVanillaDodgeDust(npc);
                            }   
                        }
                        break;

                    case 4: //martian officer shield
                        if (Main.rand.Next(2) == 0)
                        {
                            int closest = npc.FindClosestPlayer();
                            if (closest != -1)
                            {
                                Player target = Main.player[closest];
                                Vector2 velocity = target.Center - npc.Center;
                                velocity.Normalize();
                                velocity *= 14f;
                                int Damage = Main.expertMode ? 28 : 35;
                                Projectile.NewProjectile(npc.Center, velocity, ProjectileID.MartianTurretBolt, Damage, 0f, Main.myPlayer);
                            }
                        }
                        break;

                    case 5: //selenian
                        if (npc.ai[2] <= -6f)
                        {
                            damage = 0;
                            npc.ai[2] = -6f;
                        }
                        break;

                    case 6: //desu body/tail & eow
                        if (projectile.type == ProjectileID.HallowStar || projectile.type == ProjectileID.CrystalShard)
                        {
                            damage = 1;
                        }
                        else if (projectile.penetrate < 0)
                        {
                            damage /= 5;
                        }
                        else if (projectile.maxPenetrate > 0)
                        {
                            damage /= projectile.maxPenetrate;
                        }
                        break;

                    case 7: //golem fists
                        if (projectile.maxPenetrate != 1)
                            projectile.active = false;
                        break;

                    case 8: //skelly prime DR
                        int armCount = 0;
                        bool[] arms = { NPC.AnyNPCs(NPCID.PrimeCannon), NPC.AnyNPCs(NPCID.PrimeLaser), NPC.AnyNPCs(NPCID.PrimeSaw), NPC.AnyNPCs(NPCID.PrimeVice) };

                        for (int i = 0; i < 4; i++)
                        {
                            if (arms[i])
                            {
                                armCount++;
                            }
                        }

                        switch (armCount)
                        {
                            case 4:
                                damage /= 20;
                                break;
                            case 3:
                                damage /= 10;
                                break;
                            case 2:
                                damage /= 4;
                                break;
                            case 1:
                                damage /= 2;
                                break;
                            default:
                                break;
                        }
                        break;

                    case 9: //brain of cthulhu
                        if (!player.HasBuff(BuffID.Confused) && Main.rand.Next(10) == 0)
                            player.AddBuff(BuffID.Confused, Main.rand.Next(30));
                        break;

                    case 10: //ice tortoise
                        float reduction = (float)npc.life / npc.lifeMax;
                        if (reduction < 0.25f)
                            reduction = 0.25f;
                        damage = (int)(damage * reduction);
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
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 300));
                        break;

                    case NPCID.SpikedJungleSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Venom, Main.rand.Next(60, 300));
                        break;

                    case NPCID.MotherSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(mod.BuffType<Antisocial>(), Main.rand.Next(180, 1800));
                        break;

                    case NPCID.LavaSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(120, 600));
                        break;

                    case NPCID.DungeonSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Blackout, Main.rand.Next(120, 1200));
                        break;

                    case NPCID.KingSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(300, 600));

                        if (Main.rand.Next(5) == 0 && !target.HasBuff(mod.BuffType<Stunned>()))
                            target.AddBuff(mod.BuffType<Stunned>(), Main.rand.Next(30, 120));
                        break;

                    case NPCID.ToxicSludge:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(300, 600));
                        break;

                    case NPCID.CorruptSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(300, 1200));
                        break;

                    case NPCID.Crimslime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<Bloodthirsty>(), Main.rand.Next(30, 300));
                        break;

                    case NPCID.Gastropod:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<Fused>(), 1800);
                        break;

                    case NPCID.IlluminantSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<Purified>(), Main.rand.Next(30, 300));
                        break;

                    case NPCID.RainbowSlime:
                        target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                        target.AddBuff(mod.BuffType<FlamesoftheUniverse>(), Main.rand.Next(120, 600));
                        break;

                    case NPCID.DemonEye:
                    case NPCID.DemonEyeOwl:
                    case NPCID.DemonEyeSpaceship:
                        if (NPC.downedBoss2 && !target.HasBuff(BuffID.Stoned))
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(30, 120));
                        break;

                    case NPCID.EaterofSouls:
                    case NPCID.Crimera:
                        target.AddBuff(BuffID.Weak, Main.rand.Next(300, 1800));
                        break;

                    case NPCID.EyeofCthulhu:
                        target.AddBuff(mod.BuffType<Berserked>(), Main.rand.Next(60, 600));
                        break;

                    case NPCID.ServantofCthulhu:
                        target.AddBuff(mod.BuffType<Hexed>(), Main.rand.Next(300));
                        break;

                    case NPCID.QueenBee:
                        target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(300, 600));
                        target.AddBuff(mod.BuffType<Crippled>(), Main.rand.Next(300, 600));
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(300, 600));
                        break;

                    case NPCID.WallofFlesh:
                    case NPCID.WallofFleshEye:
                        if (!target.HasBuff(mod.BuffType<Unstable>()))
                            target.AddBuff(mod.BuffType<Unstable>(), Main.rand.Next(60, 240));
                        break;

                    case NPCID.TheHungry:
                    case NPCID.TheHungryII:
                        target.AddBuff(mod.BuffType<Crippled>(), Main.rand.Next(180, 300));
                        break;

                    case NPCID.EaterofWorldsHead:
                    case NPCID.EaterofWorldsBody:
                    case NPCID.EaterofWorldsTail:
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(300, 1800));
                        break;

                    case NPCID.CursedSkull:
                        if (Main.rand.Next(4) == 0)
                            target.AddBuff(BuffID.Cursed, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Snatcher:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 1800));
                        break;

                    case NPCID.ManEater:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 1800));
                        if (target.statLife < 100)
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Man Eater."), 999, 0);
                        break;

                    case NPCID.TombCrawlerHead:
                        if (target.statLife < 60)
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Tomb Crawler."), 999, 0);
                        break;

                    case NPCID.DevourerHead:
                        target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(90, 900));
                        if (target.statLife < 50)
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Devourer."), 999, 0);
                        break;

                    case NPCID.AngryTrapper:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 1800));
                        if (target.statLife < 180)
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by an Angry Trapper."), 999, 0);
                        break;

                    case NPCID.SkeletronHead:
                        target.AddBuff(mod.BuffType<Lethargic>(), Main.rand.Next(150, 300));
                        break;

                    case NPCID.SkeletronHand:
                        if (Main.rand.Next(2) == 0)
                            target.AddBuff(mod.BuffType<Stunned>(), Main.rand.Next(90));
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
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));

                        if (target.HasBuff(BuffID.Chilled) && !target.HasBuff(BuffID.Frozen))
                            target.AddBuff(BuffID.Frozen, Main.rand.Next(120));
                        break;

                    case NPCID.Lavabat:
                        int duration = Main.rand.Next(120, 600);
                        target.AddBuff(BuffID.OnFire, duration);
                        target.AddBuff(BuffID.Burning, duration);

                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.GiantBat:
                        target.AddBuff(BuffID.Confused, Main.rand.Next(300));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.IlluminantBat:
                        target.AddBuff(mod.BuffType<Flipped>(), Main.rand.Next(180, 1800));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.GiantFlyingFox:
                        target.AddBuff(mod.BuffType<Bloodthirsty>(), Main.rand.Next(30, 300));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                        break;

                    case NPCID.VampireBat:
                    case NPCID.Vampire:
                        target.AddBuff(BuffID.Darkness, Main.rand.Next(900, 1800));
                        target.AddBuff(BuffID.Weak, Main.rand.Next(900, 1800));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(900, 1800));
                        target.AddBuff(mod.BuffType<LivingWasteland>(), Main.rand.Next(300, 900));
                        break;

                    case NPCID.SnowFlinx:
                        target.AddBuff(mod.BuffType<Purified>(), Main.rand.Next(180, 1800));
                        break;

                    case NPCID.Piranha:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Medusa:
                        target.AddBuff(mod.BuffType<Flipped>(), Main.rand.Next(90, 180));

                        if (!target.HasBuff(BuffID.Stoned))
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(60, 120));
                        break;

                    case NPCID.SpikeBall:
                        target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(300, 1200));
                        break;

                    case NPCID.BlazingWheel:
                        target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Shark:
                    case NPCID.SandShark:
                    case NPCID.SandsharkCorrupt:
                    case NPCID.SandsharkCrimson:
                    case NPCID.SandsharkHallow:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(60, 300));
                        break;

                    case NPCID.BlueJellyfish:
                    case NPCID.PinkJellyfish:
                    case NPCID.GreenJellyfish:
                    case NPCID.BloodJelly:
                        if (target.wet)
                            target.AddBuff(BuffID.Electrified, Main.rand.Next(120, 240));
                        break;

                    case NPCID.GraniteFlyer:
                    case NPCID.GraniteGolem:
                        if (!target.HasBuff(BuffID.Stoned))
                            target.AddBuff(BuffID.Stoned, Main.rand.Next(120));
                        break;

                    case NPCID.LeechHead:
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 900));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(300, 900));
                        break;

                    case NPCID.AnomuraFungus:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(300, 600));
                        break;

                    case NPCID.WaterSphere:
                        target.AddBuff(mod.BuffType<Flipped>(), Main.rand.Next(600));
                        break;

                    case NPCID.GiantShelly:
                    case NPCID.GiantShelly2:
                        target.AddBuff(BuffID.Slow, Main.rand.Next(30, 300));
                        break;

                    case NPCID.Squid:
                        target.AddBuff(BuffID.Obstructed, Main.rand.Next(30, 300));
                        break;

                    case NPCID.BloodZombie:
                        target.AddBuff(mod.BuffType<Bloodthirsty>(), Main.rand.Next(30, 120));
                        break;

                    case NPCID.Drippler:
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(300, 600));
                        break;

                    case NPCID.ChaosBall:
                        target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(60, 600));
                        break;

                    case NPCID.Tumbleweed:
                        target.AddBuff(mod.BuffType<Crippled>(), Main.rand.Next(60, 300));
                        break;

                    case NPCID.CorruptBunny:
                    case NPCID.CrimsonBunny:
                    case NPCID.CorruptGoldfish:
                    case NPCID.CrimsonGoldfish:
                    case NPCID.CorruptPenguin:
                    case NPCID.CrimsonPenguin:
                    case NPCID.MothronSpawn:
                    case NPCID.PigronCorruption:
                    case NPCID.PigronCrimson:
                    case NPCID.PigronHallow:
                    case NPCID.Scutlix:
                    case NPCID.Parrot:
                    case NPCID.GingerbreadMan:
                        target.AddBuff(mod.BuffType<SqueakyToy>(), Main.rand.Next(300));
                        break;

                    case NPCID.FaceMonster:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(1800));
                        break;

                    case NPCID.Harpy:
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(60, 600));
                        break;

                    case NPCID.SeaSnail:
                        target.AddBuff(BuffID.OgreSpit, Main.rand.Next(300));
                        break;

                    case NPCID.BrainofCthulhu:
                        target.AddBuff(mod.BuffType<Hexed>(), Main.rand.Next(90));
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(300));
                        target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(300));
                        target.AddBuff(mod.BuffType<Flipped>(), Main.rand.Next(180));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(900, 1800));
                        break;

                    case NPCID.Creeper:
                        switch (Main.rand.Next(6))
                        {
                            case 0:
                                target.AddBuff(mod.BuffType<Berserked>(), Main.rand.Next(240));
                                break;

                            case 1:
                                target.AddBuff(mod.BuffType<Lethargic>(), Main.rand.Next(240));
                                break;

                            case 2:
                                target.AddBuff(mod.BuffType<Flipped>(), Main.rand.Next(240));
                                break;

                            case 3:
                                target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(240));
                                break;

                            case 4:
                                target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(240));
                                break;

                            case 5:
                                target.AddBuff(BuffID.Rabies, Main.rand.Next(900));
                                break;
                        }
                        break;

                    case NPCID.SwampThing:
                        target.AddBuff(BuffID.OgreSpit, Main.rand.Next(30, 300));
                        break;

                    case NPCID.Frankenstein:
                        target.AddBuff(mod.BuffType<LightningRod>(), Main.rand.Next(60, 600));
                        break;

                    case NPCID.Reaper:
                        if (Main.rand.Next(4) == 0 && !target.HasBuff(mod.BuffType<MarkedforDeath>()))
                        {
                            target.AddBuff(mod.BuffType<MarkedforDeath>(), 1800);
                            target.AddBuff(mod.BuffType<LivingWasteland>(), 1800);
                        }

                        target.AddBuff(mod.BuffType<LivingWasteland>(), Main.rand.Next(300, 900));
                        break;

                    case NPCID.Butcher:
                        target.AddBuff(mod.BuffType<Berserked>(), Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.Bleeding, Main.rand.Next(900, 1800));
                        break;

                    case NPCID.ThePossessed:
                        target.AddBuff(mod.BuffType<Hexed>(), Main.rand.Next(240));
                        break;

                    case NPCID.Wolf:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(1800));
                        break;

                    case NPCID.Werewolf:
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(1800));
                        break;

                    //all armored bones
                    case 269:
                    case 270:
                    case 271:
                    case 272:
                    case 273:
                    case 274:
                    case 275:
                    case 276:
                    case 277:
                    case 278:
                    case 279:
                    case 280: target.AddBuff(mod.BuffType<Bloodthirsty>(), Main.rand.Next(90)); break;

                    case NPCID.GiantTortoise:
                        target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(60, 300));
                        break;

                    case NPCID.IceTortoise:
                        target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(60, 300));

                        if (Main.rand.Next(3) == 0)
                            target.AddBuff(BuffID.Frozen, Main.rand.Next(120));
                        break;

                    //CULTIST OP
                    case NPCID.CultistBoss:
                        target.AddBuff(mod.BuffType<MarkedforDeath>(), 600);
                        break;
                    case NPCID.AncientDoom:
                        if (Main.rand.Next(3) == 0 && !target.HasBuff(mod.BuffType<MarkedforDeath>()))
                            target.AddBuff(mod.BuffType<MarkedforDeath>(), 1200);
                        break;
                    case NPCID.AncientLight:
                        target.AddBuff(mod.BuffType<Purified>(), Main.rand.Next(60, 180));
                        break;
                    case NPCID.CultistBossClone:
                        target.AddBuff(mod.BuffType<Hexed>(), Main.rand.Next(60, 180));
                        break;

                    case NPCID.MossHornet:
                        target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(30, 300));
                        break;

                    case NPCID.Paladin:
                        target.AddBuff(mod.BuffType<Lethargic>(), Main.rand.Next(480, 720));
                        break;

                    case NPCID.DukeFishron:
                        target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(300, 600));
                        target.AddBuff(mod.BuffType<MutantNibble>(), Main.rand.Next(600, 1200));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(3600, 7200));
                        break;

                    case NPCID.Sharkron:
                    case NPCID.Sharkron2:
                        target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(300, 600));
                        target.AddBuff(mod.BuffType<MutantNibble>(), Main.rand.Next(300, 600));
                        target.AddBuff(BuffID.Rabies, Main.rand.Next(3600, 7200));
                        break;

                    case NPCID.Hellhound:
                        target.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(900));
                        target.AddBuff(BuffID.Obstructed, Main.rand.Next(180));
                        break;

                    case NPCID.Mimic:
                    case NPCID.PresentMimic:
                    case NPCID.BigMimicCorruption:
                    case NPCID.BigMimicCrimson:
                    case NPCID.BigMimicHallow:
                    case NPCID.BigMimicJungle:
                        target.AddBuff(mod.BuffType<Purified>(), Main.rand.Next(120));
                        break;

                    case NPCID.RuneWizard:
                        target.AddBuff(mod.BuffType<MarkedforDeath>(), 1800);
                        target.AddBuff(mod.BuffType<Unstable>(), 1800);
                        break;

                    case NPCID.Nutcracker:
                    case NPCID.NutcrackerSpinning:
                        if (target.Male)
                        {
                            damage = 9999;
                            target.statDefense = 0;
                            target.endurance = 0;
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " got his nuts cracked."), 9999, 0);
                        }
                        else
                        {
                            target.AddBuff(BuffID.Bleeding, Main.rand.Next(900, 1800));
                        }
                        break;

                    case NPCID.Wraith:
                        target.AddBuff(mod.BuffType<LivingWasteland>(), Main.rand.Next(300, 900));
                        break;

                    case NPCID.Plantera:
                        target.AddBuff(mod.BuffType<MutantNibble>(), Main.rand.Next(600, 900));
                        goto case NPCID.PlanterasHook;

                    case NPCID.PlanterasHook:
                    case NPCID.PlanterasTentacle:
                    case NPCID.Spore:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(60, 300));
                        target.AddBuff(BuffID.Venom, Main.rand.Next(60, 300));

                        if (target.HasBuff(mod.BuffType<Infested>()))
                            target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(900, 1260));
                        else
                            target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(180, 360));
                        break;

                    case NPCID.ChaosElemental:
                        target.AddBuff(mod.BuffType<Unstable>(), Main.rand.Next(600));
                        break;

                    case NPCID.Flocko:
                        target.AddBuff(BuffID.Chilled, Main.rand.Next(900, 1800));
                        target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 600));
                        break;

                    case NPCID.GoblinThief:
                        if (target.whoAmI == Main.myPlayer)
                        {
                            //try stealing mouse item, then selected item
                            if (!StealFromInventory(target, ref Main.mouseItem))
                                StealFromInventory(target, ref target.inventory[target.selectedItem]);

                            byte extraTries = 30;
                            for (int i = 0; i < 3; i++)
                            {
                                bool successfulSteal = StealFromInventory(target, ref target.inventory[Main.rand.Next(target.inventory.Length)]);

                                if (!successfulSteal && extraTries > 0)
                                {
                                    extraTries--;
                                    i--;
                                }
                            }
                        }
                        goto case NPCID.PirateCorsair;

                    case NPCID.PirateCaptain:
                    case NPCID.PirateCorsair:
                    case NPCID.PirateCrossbower:
                    case NPCID.PirateDeadeye:
                    case NPCID.PirateShipCannon:
                        target.DropCoins();
                        break;

                    case NPCID.PirateDeckhand:
                        npc.Transform(NPCID.PirateCaptain);
                        npc.GetGlobalNPC<FargoGlobalNPC>().dropLoot = false;
                        target.DropCoins();
                        goto case NPCID.GrayGrunt;

                    case NPCID.GoblinPeon:
                    case NPCID.GrayGrunt:
                        if (Main.hardMode)
                            target.AddBuff(mod.BuffType<LivingWasteland>(), Main.rand.Next(300, 900));
                        break;

                    case NPCID.Zombie:
                    case NPCID.ArmedZombie:
                    case NPCID.ArmedZombieCenx:
                    case NPCID.ArmedZombieEskimo:
                    case NPCID.ArmedZombiePincussion:
                    case NPCID.ArmedZombieSlimed:
                    case NPCID.ArmedZombieSwamp:
                    case NPCID.ArmedZombieTwiggy:
                    case NPCID.BaldZombie:
                    case NPCID.FemaleZombie:
                    case NPCID.PincushionZombie:
                    case NPCID.SlimedZombie:
                    case NPCID.TwiggyZombie:
                    case NPCID.ZombieEskimo:
                    case NPCID.ZombiePixie:
                    case NPCID.ZombieRaincoat:
                    case NPCID.ZombieSuperman:
                    case NPCID.ZombieSweater:
                    case NPCID.ZombieXmas:
                    case NPCID.ZombieMushroom:
                    case NPCID.ZombieMushroomHat:
                    case NPCID.SwampZombie:
                    case NPCID.SmallSwampZombie:
                    case NPCID.BigSwampZombie:
                    case NPCID.ZombieDoctor:
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(60, 600));
                        break;

                    case NPCID.Corruptor:
                        target.AddBuff(BuffID.Weak, Main.rand.Next(60, 7200));
                        break;

                    case NPCID.Mummy:
                    case NPCID.LightMummy:
                    case NPCID.DarkMummy:
                        if (!target.HasBuff(BuffID.Webbed))
                            target.AddBuff(BuffID.Webbed, Main.rand.Next(30, 300));
                        break;

                    case NPCID.Derpling:
                        target.AddBuff(mod.BuffType<Lethargic>(), Main.rand.Next(600, 1200));
                        break;

                    case NPCID.Spazmatism:
                        if (masoBool[1] && Main.rand.Next(3) == 0)
                            target.AddBuff(BuffID.CursedInferno, 480);
                        goto case NPCID.Retinazer;
                    case NPCID.Retinazer:
                        target.AddBuff(mod.BuffType<Crippled>(), Main.rand.Next(120, 240));
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(120, 240));
                        break;

                    case NPCID.TheDestroyer:
                        target.AddBuff(mod.BuffType<Crippled>(), Main.rand.Next(300, 1200));

                        if (target.statLife <= 400)
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by the Destroyer."), 9999, 0);
                        goto case NPCID.TheDestroyerTail;

                    case NPCID.TheDestroyerBody:
                    case NPCID.TheDestroyerTail:
                        target.AddBuff(mod.BuffType<LightningRod>(), Main.rand.Next(300, 1200));
                        goto case NPCID.PrimeSaw;

                    case NPCID.SkeletronPrime:
                        target.AddBuff(mod.BuffType<Defenseless>(), Main.rand.Next(180, 300));
                        goto case NPCID.PrimeCannon;

                    case NPCID.PrimeVice:
                        if (target.mount.Active)
                            target.mount.Dismount(target);
                        target.AddBuff(mod.BuffType<Stunned>(), Main.rand.Next(30, 90));
                        goto case NPCID.PrimeCannon;

                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeSaw:
                    case NPCID.Probe:
                        target.AddBuff(mod.BuffType<ClippedWings>(), 15); //all mech cases come here
                        break;

                    case NPCID.BlackRecluse:
                        target.AddBuff(BuffID.Poisoned, Main.rand.Next(30, 300));
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(60, 1800));
                        break;

                    case NPCID.DesertBeast:
                        target.AddBuff(mod.BuffType<Crippled>(), Main.rand.Next(300, 900));
                        break;

                    case NPCID.FlyingSnake:
                        target.AddBuff(mod.BuffType<Infested>(), Main.rand.Next(300, 600));
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(600, 1200));
                        break;

                    case NPCID.Lihzahrd:
                    case NPCID.LihzahrdCrawler:
                        target.AddBuff(mod.BuffType<Bloodthirsty>(), Main.rand.Next(120));
                        break;

                    case NPCID.CultistDragonHead:
                        target.AddBuff(mod.BuffType<FlamesoftheUniverse>(), 300);
                        goto case NPCID.CultistDragonTail;

                    case NPCID.CultistDragonBody1:
                    case NPCID.CultistDragonBody2:
                    case NPCID.CultistDragonBody3:
                    case NPCID.CultistDragonBody4:
                    case NPCID.CultistDragonTail:
                        target.AddBuff(mod.BuffType<ClippedWings>(), 15);
                        break;

                    case NPCID.AncientCultistSquidhead:
                        target.AddBuff(mod.BuffType<Flipped>(), Main.rand.Next(60, 120));
                        target.AddBuff(mod.BuffType<Unstable>(), Main.rand.Next(120, 180));
                        break;

                    case NPCID.SolarCrawltipedeHead:
                        if (target.statLife < 200)
                            target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Crawltipede."), 999, 0);
                        break;

                    case NPCID.BoneLee:
                        target.AddBuff(BuffID.Obstructed, Main.rand.Next(60));
                        break;

                    case NPCID.Pumpking:
                    case NPCID.PumpkingBlade:
                        target.AddBuff(BuffID.Weak, Main.rand.Next(900, 1800));
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(900, 1800));
                        break;

                    case NPCID.MourningWood:
                        int duration1 = Main.rand.Next(120, 240);
                        target.AddBuff(BuffID.OnFire, duration1);
                        target.AddBuff(BuffID.CursedInferno, duration1);
                        target.AddBuff(BuffID.ShadowFlame, duration1);
                        break;

                    case NPCID.VortexHornet:
                    case NPCID.VortexHornetQueen:
                    case NPCID.VortexSoldier:
                    case NPCID.VortexRifleman:
                        target.AddBuff(mod.BuffType<LightningRod>(), Main.rand.Next(60, 600));
                        target.AddBuff(mod.BuffType<ClippedWings>(), Main.rand.Next(30, 300));
                        break;

                    case NPCID.ZombieElf:
                    case NPCID.ZombieElfBeard:
                    case NPCID.ZombieElfGirl:
                        target.AddBuff(mod.BuffType<Rotting>(), Main.rand.Next(300, 1800));
                        break;

                    case NPCID.Krampus:
                        if (target.whoAmI == Main.myPlayer)
                        {
                            //try stealing mouse item, then selected item
                            if (!StealFromInventory(target, ref Main.mouseItem))
                                StealFromInventory(target, ref target.inventory[target.selectedItem]);

                            StealFromInventory(target, ref target.armor[Main.rand.Next(3)]);

                            byte maxAttempts = 15;
                            for (int i = 0; i < 3; i++)
                            {
                                int toss = Main.rand.Next(3, 8 + target.extraAccessorySlots); //pick random accessory slot
                                if (Main.rand.Next(3) == 0 && target.armor[toss + 10].stack > 0) //chance to pick vanity slot if accessory is there
                                {
                                    toss += 10;
                                }

                                bool successfulSteal = StealFromInventory(target, ref target.armor[toss]);

                                if (!successfulSteal && maxAttempts > 0)
                                {
                                    maxAttempts--;
                                    i--;
                                }
                            }
                        }
                        break;

                    case NPCID.GigaZapper:
                        target.AddBuff(mod.BuffType<LightningRod>(), Main.rand.Next(300, 600));
                        break;

                    case NPCID.Clown:
                        target.AddBuff(mod.BuffType<Fused>(), 180);
                        target.AddBuff(mod.BuffType<Hexed>(), 1200);
                        break;

                    case NPCID.UndeadMiner:
                        int length = Main.rand.Next(3600, 7200);
                        target.AddBuff(mod.BuffType<Lethargic>(), length * 2);
                        target.AddBuff(BuffID.Darkness, length);
                        target.AddBuff(BuffID.Blackout, length);

                        for (int i = 0; i < 59; i++)
                        {
                            if (target.inventory[i].pick != 0 || target.inventory[i].hammer != 0 || target.inventory[i].axe != 0)
                                StealFromInventory(target, ref target.inventory[i]);
                        }
                        break;

                    default:
                        break;
                }
            }
           
            if(target.HasBuff(mod.BuffType<Buffs.Souls.ShellHide>()))
            {
                damage *= 2;
            }

            if(SqueakyToy)
            {
                damage = 1;
                modPlayer.Squeak(target.Center);
            }
        }

        private bool StealFromInventory(Player target, ref Item item)
        {
            if (!item.IsAir)
            {
                int i = Item.NewItem((int)target.position.X, (int)target.position.Y, target.width, target.height, item.type, 1, false, 0, false, false);
                Main.item[i].netDefaults(item.netID);
                Main.item[i].Prefix(item.prefix);
                Main.item[i].stack = item.stack;
                Main.item[i].velocity.X = Main.rand.Next(-20, 21) * 0.2f;
                Main.item[i].velocity.Y = Main.rand.Next(-20, 1) * 0.2f;
                Main.item[i].noGrabDelay = 100;
                Main.item[i].newAndShiny = false;

                if (Main.netMode == 1)
                    NetMessage.SendData(21, -1, -1, null, i, 0.0f, 0.0f, 0.0f, 0, 0, 0);

                item = new Item();

                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool StrikeNPC (NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
            bool retValue = true;

			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
			if (FargoWorld.MasochistMode)
            {
                if (PaladinsShield)
                    damage /= 2;
                
                //basically, is this a segment?
                if (npc.realLife == -1)
                    ResetRegenTimer(npc);
                else
                    ResetRegenTimer(Main.npc[npc.realLife]);
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

        public static bool BossIsAlive(ref int bossID, int bossType)
        {
            if (bossID != -1)
            {
                if (Main.npc[bossID].active && Main.npc[bossID].type == bossType)
                {
                    return true;
                }
                else
                {
                    bossID = -1;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
