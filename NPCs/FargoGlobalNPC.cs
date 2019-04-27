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
        public bool HellFire;
        public bool Infested;
        public int MaxInfestTime;
        public float InfestedDust;
        public bool Needles;
        public bool Electrified;
        public bool CurseoftheMoon;
        public int MaxLifeReduction;
        public int lightningRodTimer;
        public bool Sadism;
        public bool gotSadism;
        public bool OceanicMaul;
        public bool MutantNibble;
        public int LifePrevious = -1;

        public bool PillarSpawn = true;
        public bool ValhallaImmune;

        //masochist doom
        public byte masoAI = 0;
        public byte masoDeathAI = 0;
        public byte masoHurtAI = 0;
        public byte masoState = 0;
        public bool[] masoBool = new bool[4];
        public bool Transform = false;
        private int Stop = 0;
        public bool dropLoot = true;
        public bool PaladinsShield = false;
        public int RegenTimer = 0;
        public int Counter = 0;
        public int Counter2 = 0;
        public int Timer = 600;
        public byte SharkCount = 0;

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
        public static int fishBossEX = -1;
        public static bool spawnFishronEX;

        public override void ResetEffects(NPC npc)
        {
            TimeFrozen = false;
            SBleed = false;
            Shock = false;
            Rotting = false;
            LeadPoison = false;
            SqueakyToy = false;
            SolarFlare = false;
            HellFire = false;
            PaladinsShield = false;
            Infested = false;
            Needles = false;
            Electrified = false;
            CurseoftheMoon = false;
            Sadism = false;
            OceanicMaul = false;
            MutantNibble = false;
        }

        public override void SetDefaults(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                ResetRegenTimer(npc);

                if (npc.boss)
                    npc.npcSlots += 10;

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

                    //case NPCID.TheDestroyerBody:
                    //case NPCID.TheDestroyerTail:
                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeSaw:
                    case NPCID.PrimeVice: masoHurtAI = 6; npc.trapImmune = true; break;
                    case NPCID.Retinazer:
                    case NPCID.Spazmatism: masoHurtAI = 6; break;

                    case NPCID.GolemFistLeft:
                    case NPCID.GolemFistRight: masoHurtAI = 7; break;

                    case NPCID.SkeletronPrime:
                        masoHurtAI = 8;
                        npc.trapImmune = true;
                        break;

                    case NPCID.BrainofCthulhu: masoHurtAI = 9; break;

                    case NPCID.IceTortoise: masoHurtAI = 10; break;

                    case NPCID.WallofFlesh:
                    case NPCID.WallofFleshEye: masoHurtAI = 11; break;

                    case NPCID.MoonLordCore:
                    case NPCID.MoonLordHand:
                    case NPCID.MoonLordHead: masoHurtAI = 12; ValhallaImmune = true; break;

                    case NPCID.CultistDragonBody1:
                    case NPCID.CultistDragonBody2:
                    case NPCID.CultistDragonBody3:
                    case NPCID.CultistDragonBody4:
                    case NPCID.CultistDragonTail: masoHurtAI = 13; break;

                    case NPCID.Psycho: masoHurtAI = 14; break;

                    case NPCID.DukeFishron: masoHurtAI = 15; break;

                    case NPCID.TargetDummy: ValhallaImmune = true; break;

                    case NPCID.RainbowSlime:
                        npc.scale = 3f;
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.Hellhound:
                        npc.lavaImmune = true;
                        break;

                    case NPCID.WalkingAntlion:
                        npc.knockBackResist = .4f;
                        break;

                    case NPCID.Tumbleweed:
                        npc.knockBackResist = .1f;
                        break;

                    case NPCID.DesertBeast:
                        npc.knockBackResist = 0f;
                        break;

                    case NPCID.DetonatingBubble:
                        npc.lavaImmune = true;
                        npc.buffImmune[BuffID.OnFire] = true;
                        if (!NPC.downedBoss3)
                            npc.noTileCollide = false;
                        break;

                    case NPCID.MoonLordLeechBlob:
                        npc.lifeMax *= 10;
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
                        npc.lifeMax *= 2;
                        npc.scale += 0.5f;
                        break;

                    case NPCID.Probe:
                        dropLoot = false;
                        break;

                    case NPCID.Sharkron:
                    case NPCID.Sharkron2:
                        npc.lifeMax *= 5;
                        if (BossIsAlive(ref fishBossEX, NPCID.DukeFishron))
                        {
                            npc.lifeMax *= 4;
                            npc.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
                            ValhallaImmune = true;
                            masoHurtAI = 15;
                        }
                        //npc.damage = npc.damage * 3 / 2;
                        npc.buffImmune[BuffID.OnFire] = true;
                        npc.lavaImmune = true;
                        break;

                    case NPCID.ServantofCthulhu:
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.VileSpit:
                        npc.dontTakeDamage = true;
                        break;

                    case NPCID.TheHungry:
                        npc.lifeMax *= 4;
                        npc.knockBackResist = 0f;
                        break;

                    case NPCID.DoctorBones:
                    case NPCID.Lihzahrd:
                    case NPCID.FlyingSnake:
                        npc.trapImmune = true;
                        break;

                    case NPCID.CultistBossClone:
                        npc.damage = 75;
                        break;

                    case NPCID.SandElemental:
                        npc.lifeMax *= 2;
                        break;

                    case NPCID.AncientDoom:
                        npc.damage *= 3;
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

                    case NPCID.IlluminantBat:
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
                        //npc.buffImmune[BuffID.OnFire] = false;
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
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.LunarTowerSolar:
                        masoAI = 27;
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.LunarTowerStardust:
                        masoAI = 28;
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.LunarTowerVortex:
                        masoAI = 29;
                        npc.lifeMax *= 5;
                        break;

                    case NPCID.CultistBoss:
                        masoAI = 30;
                        npc.lifeMax = (int)(npc.lifeMax * 1.5);
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
                        npc.defense *= 5;
                        npc.defDefense = npc.defense;
                        ValhallaImmune = true;
                        break;

                    case NPCID.TheDestroyer:
                        masoAI = 36;
                        break;

                    case NPCID.DukeFishron:
                        masoAI = 37;
                        ValhallaImmune = true;
                        if (spawnFishronEX)
                        {
                            masoAI = 89;
			                npc.GivenName = "Duke Fishron EX";
                            npc.defDamage = (int)(npc.defDamage * 1.5);
                            npc.defDefense *= 2;
                            npc.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
                        }
                        break;

                    case NPCID.MoonLordCore:
                        masoAI = 38;
                        break;

                    case NPCID.Splinterling:
                        masoAI = 39;
                        break;

                    case NPCID.Golem:
                        masoAI = 40;
                        npc.lifeMax *= 2;
                        npc.trapImmune = true;
                        break;

                    case NPCID.GolemHeadFree:
                        masoAI = 41;
                        npc.trapImmune = true;
                        break;

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

                    /*case NPCID.CultistDragonBody1:
                    case NPCID.CultistDragonBody2:
                    case NPCID.CultistDragonBody3:
                    case NPCID.CultistDragonBody4:
                    case NPCID.CultistDragonTail:
                        masoAI = 46;
                        break;*/

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
                        npc.dontTakeDamage = true;
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
                        npc.lavaImmune = true;
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
                        npc.lifeMax *= 2;
                        break;

                    case NPCID.Paladin:
                        masoAI = 75;
                        break;

                    case NPCID.Mimic:
                    case NPCID.PresentMimic:
                        masoAI = 76;
                        break;

                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeVice:
                        masoAI = 77;
                        break;

                    case NPCID.WallofFleshEye:
                        masoAI = 78;
                        ValhallaImmune = true;
                        break;

                    case NPCID.GolemFistLeft:
                    case NPCID.GolemFistRight:
                        masoAI = 79;
                        npc.scale += 0.5f;
                        npc.trapImmune = true;
                        ValhallaImmune = true;
                        break;

                    case NPCID.MoonLordHand:
                    case NPCID.MoonLordHead:
                        masoAI = 80;
                        break;

                    case NPCID.AncientLight:
                        npc.buffImmune[BuffID.OnFire] = true;
                        npc.lavaImmune = true;
                        if (BossIsAlive(ref moonBoss, NPCID.MoonLordCore))
                            masoAI = 81;
                        else
                            masoAI = 82;
                        break;

                    case NPCID.DemonEye:
                    case NPCID.DemonEyeOwl:
                    case NPCID.DemonEyeSpaceship:
                        masoAI = 83;
                        break;

                    case NPCID.HoppinJack:
                        masoAI = 84;
                        break;

                    case NPCID.Antlion:
                        masoAI = 85;
                        break;

                    case NPCID.AngryNimbus:
                        masoAI = 86;
                        break;

                    case NPCID.Unicorn:
                        masoAI = 87;
                        break;

                    case NPCID.Psycho:
                        masoAI = 88;
                        break;

                    case NPCID.Reaper:
                        masoAI = 90;
                        break;

                    case NPCID.Werewolf:
                        masoAI = 91;
                        break; 

                    case NPCID.BloodZombie:
                        masoAI = 92;
                        break;

                    case NPCID.PossessedArmor:
                        masoAI = 93;
                        break;

                    case NPCID.ShadowFlameApparition:
                        masoAI = 94;
                        break;

                    case NPCID.BlueJellyfish:
                    case NPCID.PinkJellyfish:
                    case NPCID.GreenJellyfish:
                    case NPCID.BloodJelly:
                        masoAI = 95;
                        break;

                    case NPCID.Wraith:
                        masoAI = 96;
                        break;

                    case NPCID.MartianSaucer:
                        masoAI = 97;
                        break;

                    case NPCID.ToxicSludge:
                        masoAI = 98;
                        break;

                    case NPCID.GiantTortoise:
                    case NPCID.IceTortoise:
                        masoAI = 99;
                        break;

                    case NPCID.SpikeBall:
                    case NPCID.BlazingWheel:
                        masoAI = 100;
                        break;

                    case NPCID.FloatyGross:
                        masoAI = 101;
                        break;

                    case NPCID.GraniteGolem:
                        masoAI = 102;
                        break;

                    case NPCID.BigMimicCorruption:
                    case NPCID.BigMimicCrimson:
                    case NPCID.BigMimicHallow:
                    case NPCID.BigMimicJungle:
                        masoAI = 103;
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
                        masoDeathAI = 40;
                        break;
			
		            case NPCID.Pinky:
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

                    case NPCID.Shark:
                        masoDeathAI = 37;
                        break;

                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeVice:
                    case NPCID.PrimeSaw:
                        masoDeathAI = 38;
                        break;

                    case NPCID.ZombieMushroom:
                    case NPCID.ZombieMushroomHat:
                    case NPCID.AnomuraFungus:
                        masoDeathAI = 39;
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
                    case NPCID.ServantofCthulhu:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;

                    case NPCID.KingSlime:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SlimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.SlimeCount * .0125));
                        break;

                    case NPCID.EaterofWorldsHead:
                    case NPCID.EaterofWorldsBody:
                    case NPCID.EaterofWorldsTail:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.EyeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.EyeCount * .0125));
                        break;

                    case NPCID.BrainofCthulhu:
                    case NPCID.Creeper:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BrainCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.BrainCount * .0125));
                        break;

                    case NPCID.QueenBee:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.BeeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.BeeCount * .0125));
                        break;

                    case NPCID.SkeletronHead:
                    case NPCID.SkeletronHand:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.SkeletronCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.SkeletronCount * .0125));
                        break;

                    case NPCID.WallofFlesh:
                    case NPCID.WallofFleshEye:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.WallCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.WallCount * .0125));
                        break;

                    case NPCID.TheDestroyer:
                    case NPCID.TheDestroyerBody:
                    case NPCID.TheDestroyerTail:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.DestroyerCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.DestroyerCount * .0125));
                        break;

                    case NPCID.SkeletronPrime:
                    case NPCID.PrimeCannon:
                    case NPCID.PrimeLaser:
                    case NPCID.PrimeSaw:
                    case NPCID.PrimeVice:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PrimeCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PrimeCount * .0125));
                        break;

                    case NPCID.Retinazer:
                    case NPCID.Spazmatism:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.TwinsCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.TwinsCount * .0125));
                        break;

                    case NPCID.Plantera:
                    case NPCID.PlanterasHook:
                    case NPCID.PlanterasTentacle:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.PlanteraCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.PlanteraCount * .0125));
                        break;

                    case NPCID.Golem:
                    case NPCID.GolemFistLeft:
                    case NPCID.GolemFistRight:
                    case NPCID.GolemHead:
                    case NPCID.GolemHeadFree:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.GolemCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.GolemCount * .0125));
                        break;

                    case NPCID.CultistBoss:
                    case NPCID.CultistBossClone:
                    case NPCID.AncientCultistSquidhead:
                    case NPCID.AncientDoom:
                    case NPCID.CultistDragonHead:
                    case NPCID.CultistDragonBody1:
                    case NPCID.CultistDragonBody2:
                    case NPCID.CultistDragonBody3:
                    case NPCID.CultistDragonBody4:
                    case NPCID.CultistDragonTail:
                        npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.CultistCount * .025));
                        npc.damage = (int)(npc.damage * (1 + FargoWorld.CultistCount * .0125));
                        break;

                    case NPCID.AncientLight:
                    case NPCID.SolarGoop:
                        if (BossIsAlive(ref moonBoss, NPCID.MoonLordCore))
                            npc.damage = (int)(npc.damage * (1 + FargoWorld.MoonlordCount * .0125));
                        else if (BossIsAlive(ref cultBoss, NPCID.CultistBoss))
                            npc.damage = (int)(npc.damage * (1 + FargoWorld.CultistCount * .0125));
                        break;

                    case NPCID.DukeFishron:
                        if (FargoWorld.downedFishronEX || !spawnFishronEX)
                        {
                            npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.FishronCount * .025));
                            npc.damage = (int)(npc.damage * (1 + FargoWorld.FishronCount * .0125));
                        }
                        break;
                    case NPCID.Sharkron:
                    case NPCID.Sharkron2:
                        if (FargoWorld.downedFishronEX || !BossIsAlive(ref fishBossEX, NPCID.DukeFishron))
                        {
                            npc.lifeMax = (int)(npc.lifeMax * (1 + FargoWorld.FishronCount * .025));
                            npc.damage = (int)(npc.damage * (1 + FargoWorld.FishronCount * .0125));
                        }
                        break;
                    case NPCID.DetonatingBubble:
                        if (FargoWorld.downedFishronEX || !BossIsAlive(ref fishBossEX, NPCID.DukeFishron))
                            npc.damage = (int)(npc.damage * (1 + FargoWorld.FishronCount * .0125));
                        break;

                    case NPCID.MoonLordCore:
                    case NPCID.MoonLordHand:
                    case NPCID.MoonLordHead:
                    case NPCID.MoonLordFreeEye:
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
                npc.frameCounter = 0;
                return false;
            }

            if (Stop > 0)
            {
                Stop--;
                npc.position = npc.oldPosition;
                npc.frameCounter = 0;
            }

            return true;
        }

        public override void AI(NPC npc)
        {
            if (FargoWorld.MasochistMode)
            {
                if (RegenTimer > 0)
                    RegenTimer--;
                
                //transformations
                if(!Transform)
                {
                    int npcType = 0;
                    int[] transforms = { NPCID.Zombie, NPCID.ArmedZombie, NPCID.ZombieEskimo, NPCID.ArmedZombieEskimo, NPCID.PincushionZombie, NPCID.ArmedZombiePincussion, NPCID.FemaleZombie, NPCID.ArmedZombieCenx, NPCID.SlimedZombie, NPCID.ArmedZombieSlimed, NPCID.TwiggyZombie, NPCID.ArmedZombieTwiggy, NPCID.SwampZombie, NPCID.ArmedZombieSwamp, NPCID.Skeleton, NPCID.BoneThrowingSkeleton, NPCID.HeadacheSkeleton, NPCID.BoneThrowingSkeleton2, NPCID.MisassembledSkeleton, NPCID.BoneThrowingSkeleton3, NPCID.PantlessSkeleton, NPCID.BoneThrowingSkeleton4, NPCID.JungleSlime, NPCID.SpikedJungleSlime, NPCID.IceSlime, NPCID.SpikedIceSlime };
                    if (Array.IndexOf(transforms, npc.type) % 2 == 0 && Main.rand.Next(5) == 0)
                        npcType = transforms[Array.IndexOf(transforms, npc.type) + 1];

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
                            if (Main.rand.Next(8) == 0)
                                Horde(npc, 6);
                            break;

                        case NPCID.EaterofSouls:
			            case NPCID.Crimera:
                            if (NPC.downedBoss2 && Main.rand.Next(5) == 0)
                                Horde(npc, 5);
                            break;

                        case NPCID.CaveBat:
                            if (Main.rand.Next(5) == 0)
                                Horde(npc, 4);
                            break;

                        case NPCID.Shark:
                            if (Main.rand.Next(4) == 0)
                                Horde(npc, 2);
                            break;

                        case NPCID.WyvernHead:
                            if (Main.rand.Next(4) == 0)
                                Horde(npc, 2);
                            break;

                        case NPCID.Wolf:
                            if (Main.rand.Next(3) == 0)
                                Horde(npc, 5);
                            break;

                        case NPCID.FlyingFish:
                            if (Main.rand.Next(4) == 0)
                                Horde(npc, 3);
                            break;

                        case NPCID.Ghost:
                            if (Main.rand.Next(5) == 0)
                                Horde(npc, 3);
                            break;

                        case NPCID.GreekSkeleton:
                            if (Main.rand.Next(3) == 0)
                                Horde(npc, 3);
                            break;

                        case NPCID.RedDevil:
                            if (Main.rand.Next(5) == 0)
                                Horde(npc, 5);
                            break;
			    
			            case NPCID.MossHornet:
                            if (Main.rand.Next(4) == 0)
                                Horde(npc, 5);
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
			                if (Main.rand.Next(2) == 0)
                            	npcType = NPCID.VortexHornet;
                            break;

                        case NPCID.Bee:
                        case NPCID.BeeSmall:
                            if (Main.rand.Next(2) == 0)
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
                        npc.Transform(npcType);
                }

                /*if (npc.townNPC && Main.hardMode && !Main.dayTime && Main.moonPhase == 0 && werewolfTime)
                {
                    werewolfTime = false;
                    Main.PlaySound(SoundID.Roar);
                    Main.NewText(npc.GivenName + " has succumbed to the curse..", new Color(255, 0, 0));
                    npc.Transform(NPCID.Werewolf);
                }
                else if (Main.moonPhase != 0)
                {
                    werewolfTime = true;
                }*/

                switch (masoAI)
                {
                    case 0: //default
                        break;

                    case 1: //tim
                        Aura(npc, 400, BuffID.Silenced, false, 15);
                        break;

                    case 2: //runewizard
                        Aura(npc, 300, mod.BuffType("Hexed"), true);
                        break;

                    case 3: //beetles
                        Aura(npc, 400, mod.BuffType("Lethargic"), false, 60);
                        break;

                    case 4: //enchanted sword family
                        Aura(npc, 400, BuffID.WitheredWeapon, false, 14);
                        break;

                    case 5: //ghost
                        Aura(npc, 100, BuffID.Cursed, false, 20);
                        break;

                    case 6: //mummies
                        Aura(npc, 500, BuffID.Slow, false, 0);
                        break;

                    case 7: //derpling
                        Aura(npc, 600, BuffID.Confused, false, 15);
                        break;

                    case 8: //illum bat
                        npc.TargetClosest();
                        if (Main.player[npc.target] != null && npc.Distance(Main.player[npc.target].Center) < 1000)
                        {
                            if (++Counter >= 600)
                            {
                                if (Main.netMode != 1 && NPC.CountNPCS(NPCID.IlluminantBat) < 20)
                                {
                                    int bat = NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, NPCID.IlluminantBat);
                                    if (bat < 200)
                                    {
                                        Main.npc[bat].velocity.X = Main.rand.Next(-5, 6);
                                        Main.npc[bat].velocity.Y = Main.rand.Next(-5, 6);
                                        Main.npc[bat].netUpdate = true;
                                        if (Main.netMode == 2)
                                            NetMessage.SendData(23, -1, -1, null, bat);
                                    }
                                }
                                Counter = 0;
                            }
                        }
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

                        Aura(npc, 100, BuffID.Burning, false, DustID.Fire);
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
                        //in the air
                        if (npc.ai[0] != 0f)
                        {
                            Counter++;
                            if (Counter >= 300)
                            {
                                Shoot(npc, 30, 500, 10, ProjectileID.HarpyFeather, npc.damage / 2, 1, true);
                            }
                        }
                        break;

                    case 12: //dr bones
                        Counter++;
                        if (Counter >= 600)
                        {
                            Shoot(npc, 120, 1000, 14, ProjectileID.Boulder, npc.damage * 4, 2);
                        }
                        
                        break;

                    case 13: //crab
                        Counter++;
                        if (Counter >= 300)
                        {
                            Shoot(npc, 30, 800, 14, ProjectileID.Bubble, npc.damage / 2, 1, false, true);
                        }
                        break;

                    case 14: //armored viking
                        Counter++;
                        if (Counter >= 10)
                        {
                            Shoot(npc, 0, 200, 10, ProjectileID.IceSickle, npc.damage / 2, 1, false, true);
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
                            Shoot(npc, 60, 400, 14, ProjectileID.WebSpit, npc.damage / 4, 0);
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
                                FargoGlobalProjectile.XWay(8, npc.Center, ProjectileID.DemonSickle, 1, npc.damage / 2, .5f);
                        }
                        break;

                    case 19: //voodoo demon
                        if (npc.lavaWet && !npc.HasBuff(BuffID.OnFire))
                        {
                            npc.buffImmune[BuffID.OnFire] = false;
                            npc.AddBuff(BuffID.OnFire, 600);
                        }
                        else if (npc.HasBuff(BuffID.OnFire))
                        {
                            Main.PlaySound(4, (int)npc.position.X, (int)npc.position.Y, 10, 1f, 0.5f);
                            if (npc.buffTime[npc.FindBuffIndex(BuffID.OnFire)] < 60 && !BossIsAlive(ref wallBoss, NPCID.WallofFlesh))
                            {
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.WallofFlesh);
                                Main.NewText("Wall of Flesh has awoken!", 175, 75);
                                npc.Transform(NPCID.Demon);
                            }
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
                                    player.AddBuff(mod.BuffType("Defenseless"), player.buffTime[b]);
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
                        if (Counter >= 300)
                        {
                            Counter = 0;

                            if (npc.life <= npc.lifeMax * 0.65 && NPC.CountNPCS(NPCID.ServantofCthulhu) < 12)
                            {
                                int[] eyes = new int[4];

                                for (int i = 0; i < 4; i++)
                                {
                                    eyes[i] = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), NPCID.ServantofCthulhu);
                                }

                                Main.npc[eyes[0]].velocity = new Vector2(2, 2);
                                Main.npc[eyes[1]].velocity = new Vector2(-2, 2);
                                Main.npc[eyes[2]].velocity = new Vector2(-2, -2);
                                Main.npc[eyes[3]].velocity = new Vector2(2, -2);
                            }
                        }

                        //during dashes in phase 2
                        if (npc.ai[1] == 3f && npc.life < npc.lifeMax * .4f)
                        {
                            Counter2 = 30;

                            Projectile[] projs = new Projectile[8];
                            projs = FargoGlobalProjectile.XWay(8, npc.Center, ProjectileID.DemonSickle, 2, npc.damage / 4, 1f);

                            for (int i = 0; i < 8; i++)
                            {
                                projs[i].hostile = true;
                                projs[i].friendly = false;
                                projs[i].GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                            }
                        }

                        if (Counter2 > 0 && Counter2 % 5 == 0)
                        {
                            int p = Projectile.NewProjectile(new Vector2(npc.Center.X + Main.rand.Next(-15, 15), npc.Center.Y), npc.velocity / 10, ProjectileID.DemonSickle, npc.damage / 4, 1f, Main.myPlayer);
                            Main.projectile[p].hostile = true;
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                            //Main.projectile[p].timeLeft = 120;
                        }
                        Counter2--;
                        break;

                    case 24: //retinazer
                        retiBoss = npc.whoAmI;
                        bool spazAlive = BossIsAlive(ref spazBoss, NPCID.Spazmatism);
                        bool targetAlive = npc.HasPlayerTarget && Main.player[npc.target].active;

                        if (!masoBool[0]) //start phase 2
                        {
                            masoBool[0] = true;
                            npc.ai[0] = 1f;
                            npc.ai[1] = 0.0f;
                            npc.ai[2] = 0.0f;
                            npc.ai[3] = 0.0f;
                            npc.netUpdate = true;
                        }

                        if (npc.ai[0] < 4f) //going to phase 3
                        {
                            if (npc.life <= npc.lifeMax / 2)
                            {
                                npc.ai[0] = 4f;
                                npc.netUpdate = true;
                                Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);

                                /*int heal = npc.lifeMax - npc.life;
                                npc.life += heal;
                                CombatText.NewText(npc.Hitbox, CombatText.HealLife, heal);*/
                            }
                        }
                        else
                        {
                            //2*pi * (# of full circles) / (seconds to finish rotation) / (ticks per sec)
                            const float rotationInterval = 2f * (float)Math.PI * 1f / 4f / 60f;

                            npc.ai[0]++; //base value is 4
                            switch ((int)npc.ai[3]) //laser code idfk
                            {
                                case 0:
                                    if (!targetAlive)
                                        npc.ai[0]--; //stop counting up if player is dead
                                    if (npc.ai[0] > 604f)
                                    {
                                        npc.ai[0] = 4f;
                                        if (npc.HasPlayerTarget)
                                        {
                                            npc.ai[3]++;
                                            npc.ai[2] = -npc.rotation;
                                            masoBool[2] = (Main.player[npc.target].Center.X - npc.Center.X < 0);
                                        }
                                        npc.netUpdate = true;
                                    }
                                    break;

                                case 1: //slowing down, beginning rotation
                                    npc.velocity *= 1f - (npc.ai[0] - 4f) / 120f;
                                    npc.localAI[1] = 0f;
                                    npc.ai[2]--; //negate vanilla counting up
                                    npc.ai[2] -= (npc.ai[0] - 4f) / 120f * rotationInterval * (masoBool[2] ? 1f : -1f);
                                    npc.rotation = -npc.ai[2];

                                    if (npc.ai[0] >= 124f) //FIRE LASER
                                    {
                                        if (Main.netMode != 1)
                                        {
                                            Vector2 speed = Vector2.UnitX.RotatedBy(npc.rotation);
                                            Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("PhantasmalDeathray"), npc.damage / 2, 0f, Main.myPlayer, 0f, npc.whoAmI);
                                        }

                                        npc.ai[3]++;
                                        npc.ai[0] = 4f;
                                        npc.netUpdate = true;
                                    }
                                    break;

                                case 2: //spinning full speed
                                    npc.velocity = Vector2.Zero;
                                    npc.localAI[1] = 0f;
                                    npc.ai[2]--;
                                    npc.ai[2] -= rotationInterval * (masoBool[2] ? 1f : -1f);
                                    npc.rotation = -npc.ai[2];

                                    if (npc.ai[0] >= 244f)
                                    {
                                        npc.ai[3]++;
                                        npc.ai[0] = 4f;
                                        npc.netUpdate = true;
                                    }
                                    break;

                                case 3: //laser done, slowing down spin, moving again
                                    npc.velocity *= (npc.ai[0] - 4f) / 60f;
                                    npc.localAI[1] = 0f;
                                    npc.ai[2]--;
                                    npc.ai[2] -= (1f - (npc.ai[0] - 4f) / 60f) * rotationInterval * (masoBool[2] ? 1f : -1f);
                                    npc.rotation = -npc.ai[2];

                                    if (npc.ai[0] >= 64f)
                                    {
                                        npc.ai[3] = 0f;
                                        npc.ai[0] = 4f;
                                        npc.netUpdate = true;
                                    }
                                    break;

                                default:
                                    npc.ai[3] = 0f;
                                    npc.ai[0] = 4f;
                                    npc.netUpdate = true;
                                    break;
                            }

                            npc.position += npc.velocity / 4f;

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
                            //    Projectile.NewProjectile(npc.Center, vector200, mod.ProjectileType("PhantasmalDeathray"), npc.damage / 2, 0f, Main.myPlayer, num1225 * 0.0104719755f, npc.whoAmI);
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

                        if (!BossIsAlive(ref spazBoss, NPCID.Spazmatism) && targetAlive)
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

                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
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

                        if (npc.ai[0] < 4f)
                        {
                            if (npc.life <= npc.lifeMax / 2) //going to phase 3
                            {
                                npc.ai[0] = 4f;
                                npc.netUpdate = true;
                                Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);

                                /*int heal = npc.lifeMax - npc.life;
                                npc.life += heal;
                                CombatText.NewText(npc.Hitbox, CombatText.HealLife, heal);*/

                                int index = npc.FindBuffIndex(BuffID.CursedInferno);
                                if (index != -1)
                                    npc.DelBuff(index); //remove cursed inferno debuff if i have it

                                npc.buffImmune[BuffID.CursedInferno] = true;
                                npc.buffImmune[BuffID.OnFire] = true;
                                npc.buffImmune[BuffID.ShadowFlame] = true;
                                npc.buffImmune[BuffID.Frostburn] = true;
                            }
                        }
                        else
                        {
                            npc.position += npc.velocity / 4f;

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
                                            Damage = (int)(Damage * (1 + FargoWorld.TwinsCount * .0125));
                                            Speed.X += Main.rand.Next(-40, 41) * 0.05f;
                                            Speed.Y += Main.rand.Next(-40, 41) * 0.05f;
                                            Projectile.NewProjectile(npc.Center + Speed * 4f, Speed, ProjectileID.CursedFlameHostile, Damage, 0f, Main.myPlayer);
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                            else if (npc.HasPlayerTarget && Main.player[npc.target].active) //cursed flamethrower when dashing
                            {
                                Counter++;
                                if (Counter == 4)
                                {
                                    Counter = 0;
                                    Projectile.NewProjectile(npc.Center, npc.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-6f, 6f))) * 0.66f, ProjectileID.EyeFire, npc.damage / 5, 0f, Main.myPlayer);
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

                        if (!retiAlive && npc.HasPlayerTarget && Main.player[npc.target].active)
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
                        Aura(npc, 5000, mod.BuffType("Atrophied"), false, 58);
                        Aura(npc, 5000, mod.BuffType("Jammed"));
                        Aura(npc, 5000, mod.BuffType("Antisocial"));
                        if (!npc.dontTakeDamage)
                        {
                            if (++Counter > 240)
                            {
                                Counter = 0;
                                npc.TargetClosest(false);
                                for (int i = 0; i < 40; ++i)
                                {
                                    int d = Dust.NewDust(npc.position, npc.width, npc.height, 242, 0.0f, 0.0f, 0, new Color(), 1f);
                                    Dust dust = Main.dust[d];
                                    dust.velocity *= 4f;
                                    Main.dust[d].noGravity = true;
                                    Main.dust[d].scale += 1.5f;
                                }
                                if (npc.HasPlayerTarget && Main.netMode != 1)
                                {
                                    int x = (int)Main.player[npc.target].Center.X / 16;
                                    int y = (int)Main.player[npc.target].Center.Y / 16;
                                    for (int i = 0; i < 100; i++)
                                    {
                                        int newX = x + Main.rand.Next(10, 31) * (Main.rand.Next(2) == 0 ? 1 : -1);
                                        int newY = y + Main.rand.Next(-15, 16);
                                        Vector2 newPos = new Vector2(newX * 16, newY * 16);
                                        if (!Collision.SolidCollision(newPos, npc.width, npc.height))
                                        {
                                            npc.Center = newPos;
                                            break;
                                        }
                                    }
                                }
                                for (int i = 0; i < 40; ++i)
                                {
                                    int d = Dust.NewDust(npc.position, npc.width, npc.height, 242, 0.0f, 0.0f, 0, new Color(), 1f);
                                    Dust dust = Main.dust[d];
                                    dust.velocity *= 4f;
                                    Main.dust[d].noGravity = true;
                                    Main.dust[d].scale += 1.5f;
                                }
                                Main.PlaySound(SoundID.Item8, npc.Center);
                                npc.netUpdate = true;
                            }
                            
                            if (++Timer > 60)
                            {
                                Timer = 0;
                                npc.TargetClosest(false);
                                if (npc.HasPlayerTarget && Main.netMode != 1)
                                {
                                    for (int i = 0; i < 3; i++)
                                    {
                                        Vector2 position = Main.player[npc.target].Center;
                                        position.X += Main.rand.Next(-300, 301);
                                        position.Y -= Main.rand.Next(500, 801);
                                        Vector2 speed = Main.player[npc.target].Center - position;
                                        speed.Normalize();
                                        speed *= 10f;
                                        Projectile.NewProjectile(position, speed, ProjectileID.NebulaLaser, 40, 0f, Main.myPlayer);
                                    }
                                }
                            }
                        }
                        break;

                    case 27: //solar pill
                        Aura(npc, 5000, mod.BuffType("ReverseManaFlow"), false, DustID.SolarFlare);
                        Aura(npc, 5000, mod.BuffType("Jammed"));
                        Aura(npc, 5000, mod.BuffType("Antisocial"));
                        if (!npc.dontTakeDamage)
                        {
                            if (++Timer > 240)
                            {
                                Timer = 0;
                                npc.TargetClosest(false);
                                if (npc.HasPlayerTarget && Main.netMode != 1)
                                {
                                    const float rotate = (float)Math.PI / 4f;
                                    Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                    speed.Normalize();
                                    speed *= 5f;
                                    for (int i = -2; i <= 2; i++)
                                        Projectile.NewProjectile(npc.Center, speed.RotatedBy(i * rotate), ProjectileID.CultistBossFireBall, 40, 0f, Main.myPlayer);
                                }
                            }
                        }
                        break;

                    case 28: //stardust pill
                        Aura(npc, 5000, mod.BuffType("Atrophied"), false, 20);
                        Aura(npc, 5000, mod.BuffType("Jammed"));
                        Aura(npc, 5000, mod.BuffType("ReverseManaFlow"));
                        if (!npc.dontTakeDamage)
                        {
                            if (++Timer > 420)
                            {
                                Timer = 0;
                                npc.TargetClosest(false);
                                if (npc.HasPlayerTarget && Main.netMode != 1)
                                {
                                    const float rotate = (float)Math.PI / 12f;
                                    Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                    speed.Normalize();
                                    speed *= 8f;
                                    for (int i = 0; i < 24; i++)
                                    {
                                        Vector2 vel = speed.RotatedBy(rotate * i);
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.AncientLight, 0,
                                            0f, (Main.rand.NextFloat() - 0.5f) * 0.3f * 6.28318548202515f / 60f, vel.X, vel.Y);
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = vel;
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 29: //vortex pill
                        Aura(npc, 5000, mod.BuffType("Atrophied"), false, DustID.Vortex);
                        Aura(npc, 5000, mod.BuffType("ReverseManaFlow"));
                        Aura(npc, 5000, mod.BuffType("Antisocial"));
                        if (!npc.dontTakeDamage)
                        {
                            if (++Counter > 360) //triggers "shield going down" animation
                            {
                                Counter = 0;
                                npc.ai[3] = 1f;
                                npc.netUpdate = true;
                            }

                            npc.reflectingProjectiles = npc.ai[3] != 0f;
                            if (npc.reflectingProjectiles) //dust
                            {
                                for (int i = 0; i < 20; i++)
                                {
                                    Vector2 offset = new Vector2();
                                    double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                    offset.X += (float)(Math.Sin(angle) * npc.height / 2);
                                    offset.Y += (float)(Math.Cos(angle) * npc.height / 2);
                                    Dust dust = Main.dust[Dust.NewDust(
                                        npc.Center + offset - new Vector2(4, 4), 0, 0,
                                        DustID.Vortex, 0, 0, 100, Color.White, 1f
                                        )];
                                    dust.noGravity = true;
                                }
                            }

                            if (++Timer > 240)
                            {
                                Timer = 0;
                                npc.TargetClosest(false);
                                if (npc.HasPlayerTarget && Main.netMode != 1)
                                {
                                    Vector2 speed = Main.player[npc.target].Center + Main.player[npc.target].velocity * 15f - npc.Center;
                                    speed.Normalize();
                                    speed *= 4f;
                                    Projectile.NewProjectile(npc.Center, speed, ProjectileID.CultistBossLightningOrb, 30, 0f, Main.myPlayer);
                                }
                            }
                        }
                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                        break;

                    case 30: //lunatic cultist
                        cultBoss = npc.whoAmI;

                        Timer++;
                        if (Timer >= 1200)
                        {
                            Timer = 0;

                            if (NPC.CountNPCS(NPCID.AncientCultistSquidhead) < 4)
                                NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.AncientCultistSquidhead, 0, 0f, 0f, 0f, 0f, npc.target);
                        }

                        if (npc.ai[3] == -1f)
                        {
                            if (npc.ai[1] >= 120f && npc.ai[1] < 419f) //skip summoning ritual LMAO
                            {
                                npc.ai[1] = 419f;
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            switch((int)npc.ai[0])
                            {
                                case -1:
                                    if (npc.ai[1] == 419f)
                                    {
                                        npc.ai[0] = 0f;
                                        npc.ai[1] = 0f;
                                        npc.ai[3] = 11f;
                                        npc.netUpdate = true;
                                    }
                                    break;

                                case 2:
                                    if (npc.ai[1] == 3f && Main.netMode != 1) //ice mist, frost wave support
                                    {
                                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                                        if (t != -1 && Main.player[t].active)
                                        {
                                            for (int i = 0; i < 200; i++)
                                            {
                                                if (Main.npc[i].active && Main.npc[i].type == NPCID.CultistBossClone)
                                                {
                                                    Vector2 distance = Main.player[t].Center - Main.npc[i].Center;
                                                    distance.Normalize();
                                                    distance = distance.RotatedByRandom(Math.PI / 12);
                                                    distance *= Main.rand.NextFloat(6f, 9f);
                                                    Projectile.NewProjectile(Main.npc[i].Center, distance,
                                                        ProjectileID.FrostWave, npc.damage / 3, 0f, Main.myPlayer);
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case 3:
                                    if (npc.ai[1] == 3f && Main.netMode != 1) //fireballs, solar goop support
                                    {
                                        for (int i = 0; i < 200; i++)
                                        {
                                            if (Main.npc[i].active && Main.npc[i].type == NPCID.CultistBossClone)
                                            {
                                                int n = NPC.NewNPC((int)Main.npc[i].Center.X, (int)Main.npc[i].Center.Y, NPCID.SolarGoop);
                                                if (n < 200)
                                                {
                                                    Main.npc[n].velocity.X = Main.rand.Next(-10, 11);
                                                    Main.npc[n].velocity.Y = Main.rand.Next(-15, -4);
                                                    if (Main.netMode == 2)
                                                        NetMessage.SendData(23, -1, -1, null, n);
                                                }
                                            }
                                        }
                                    }
                                    break;

                                case 4:
                                    if (npc.ai[1] == 19f && npc.HasPlayerTarget && Main.netMode != 1) //lightning orb, lightning support
                                    {
                                        for (int i = 0; i < 200; i++)
                                        {
                                            if (Main.npc[i].active && Main.npc[i].type == NPCID.CultistBossClone)
                                            {
                                                Vector2 dir = Main.player[npc.target].Center - npc.Center;
                                                float ai1New = Main.rand.Next(100);
                                                Vector2 vel = Vector2.Normalize(dir.RotatedByRandom(Math.PI / 4)) * 7f;
                                                Projectile.NewProjectile(Main.npc[i].Center, vel, ProjectileID.CultistBossLightningOrbArc,
                                                    npc.damage / 15 * 6, 0, Main.myPlayer, dir.ToRotation(), ai1New);
                                            }
                                        }
                                    }
                                    break;

                                case 7:
                                    if (npc.ai[1] == 3f && Main.netMode != 1) //ancient light, phantasmal eye support
                                    {
                                        for (int i = 0; i < 200; i++)
                                        {
                                            if (Main.npc[i].active && Main.npc[i].type == NPCID.CultistBossClone)
                                            {
                                                Vector2 speed = Vector2.UnitX.RotatedByRandom(Math.PI);
                                                speed *= 6f;
                                                Projectile.NewProjectile(Main.npc[i].Center, speed,
                                                    ProjectileID.PhantasmalEye, npc.damage / 3, 0f, Main.myPlayer);
                                                Projectile.NewProjectile(Main.npc[i].Center, speed.RotatedBy(Math.PI * 2 / 3),
                                                    ProjectileID.PhantasmalEye, npc.damage / 3, 0f, Main.myPlayer);
                                                Projectile.NewProjectile(Main.npc[i].Center, speed.RotatedBy(-Math.PI * 2 / 3),
                                                    ProjectileID.PhantasmalEye, npc.damage / 3, 0f, Main.myPlayer);
                                            }
                                        }
                                    }
                                    break;

                                case 8:
                                    if (npc.ai[1] == 3f) //ancient doom, nebula sphere support
                                    {
                                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                                        if (t != -1 && Main.player[t].active)
                                        {
                                            for (int i = 0; i < 200; i++)
                                            {
                                                if (Main.npc[i].active && Main.npc[i].type == NPCID.CultistBossClone)
                                                    Projectile.NewProjectile(Main.npc[i].Center, Vector2.Zero,
                                                        ProjectileID.NebulaSphere, npc.damage / 15 * 6, 0f, Main.myPlayer);
                                            }
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                        }

                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                        break;

                    case 31: //king slime
                        slimeBoss = npc.whoAmI;

                        if (masoBool[1])
                        {
                            if (npc.velocity.Y == 0f) //start attack
                            {
                                masoBool[1] = false;

                                for (int i = 0; i < 30; i++)
                                {
                                    int p = Projectile.NewProjectile(new Vector2(npc.Center.X + Main.rand.Next(-5, 5), npc.Center.Y - 15), new Vector2(Main.rand.NextFloat(-6, 6), Main.rand.NextFloat(-8, -5)), ProjectileID.SpikedSlimeSpike, npc.damage / 5, 0f, Main.myPlayer);
                                    Main.projectile[p].friendly = false;
                                    Main.projectile[p].hostile = true;
                                }
                            }
                        }
                        else if (npc.velocity.Y > 0)
                        {
                            masoBool[1] = true;
                        }

                        if (npc.life < npc.lifeMax * .5f && npc.HasPlayerTarget)
                        {
                            Player p = Main.player[npc.target];

                            Counter++;
                            if (Counter >= 90) //slime rain
                            {
                                Counter = 0;
                                Main.PlaySound(SoundID.Item21, p.Center);
                                if (Main.netMode != 1)
                                {
                                    for (int i = 0; i < 5; i++)
                                    {
                                        Vector2 spawn = p.Center;
                                        spawn.X += Main.rand.Next(-200, 201);
                                        spawn.Y -= Main.rand.Next(600, 901);
                                        Vector2 speed = p.Center - spawn;
                                        speed.Normalize();
                                        speed *= 5f;
                                        speed = speed.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-5, 5)));
                                        Projectile.NewProjectile(spawn, speed, mod.ProjectileType("SlimeBallHostile"), npc.damage / 5, 0f, Main.myPlayer);
                                    }
                                }
                            }
                        }

                        if (!masoBool[0])
                        {
                            if (npc.HasPlayerTarget)
                            {
                                Player player = Main.player[npc.target];
                                if (player.active && !player.dead && player.Center.Y < npc.position.Y && npc.Distance(player.Center) < 1000f)
                                {
                                    Counter2++; //timer runs if player is above me and nearby

                                    if (Counter2 >= 600) //go berserk
                                    {
                                        masoBool[0] = true;
                                        SharkCount = 1;
                                        Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                        npc.defDamage *= 20;
                                        npc.defDefense *= 999;
                                        npc.netUpdate = true;
                                        Main.NewText("King Slime has enraged!", 175, 75, 255);
                                    }
                                }
                                else
                                {
                                    Counter2 = 0;
                                }
                            }
                        }
                        else //is berserk
                        {
                            npc.damage = npc.defDamage;
                            npc.defense = npc.defDefense;

                            if (npc.HasPlayerTarget)
                            {
                                Player p = Main.player[npc.target];

                                Counter2++;
                                if (Counter2 >= 3) //spray random slime spikes
                                {
                                    Counter2 = 0;

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
                                    p.AddBuff(mod.BuffType("Crippled"), 2);
                                    p.AddBuff(mod.BuffType("ClippedWings"), 2);
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

                        if (!masoBool[0] && npc.life < npc.lifeMax / 2)
                        {
                            masoBool[0] = true;
                            if (Main.netMode != 1)
                            {
                                int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RoyalSubject"));
                                if (n < 200 && Main.netMode == 2)
                                    NetMessage.SendData(23, -1, -1, null, n);
                            }
                        }

                        if (!masoBool[1] && npc.life < npc.lifeMax / 4)
                        {
                            masoBool[1] = true;
                            if (Main.netMode != 1)
                            {
                                int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("RoyalSubject"));
                                if (n < 200 && Main.netMode == 2)
                                    NetMessage.SendData(23, -1, -1, null, n);
                            }
                        }

                        //only while stationary mode
                        if (npc.ai[0] == 3f || npc.ai[0] == 1f)
                        {
                            Counter++;
                            if (Counter >= 90)
                            {
                                Counter = 0;
                                Counter2++;
                                if (Counter2 > 3)
                                {
                                    if (Main.netMode != 1)
                                        FargoGlobalProjectile.XWay(16, npc.Center, ProjectileID.Stinger, 6, npc.damage / 4, 1);
                                    Counter2 = 0;
                                }
                                else
                                {
                                    if (Main.netMode != 1)
                                        FargoGlobalProjectile.XWay(8, npc.Center, ProjectileID.Stinger, 6, npc.damage / 4, 1);
                                }
                            }
                        }
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

                        if (npc.ai[3] == 0f) //when spawned in, make one eye invul
                        {
                            for (int i = 0; i < 200; i++)
                            {
                                if (Main.npc[i].active && Main.npc[i].type == NPCID.WallofFleshEye && Main.npc[i].realLife == npc.whoAmI)
                                {
                                    Main.npc[i].ai[2] = -1f;
                                    Main.npc[i].netUpdate = true;
                                    npc.ai[3] = 1f;
                                    npc.netUpdate = true;
                                    break;
                                }
                            }
                        }
                        else if (npc.ai[3] == 1f)
                        {
                            if (npc.life < npc.lifeMax / 2) //enter phase 2
                            {
                                npc.ai[3] = 2f;
                                npc.netUpdate = true;
                                for (int i = 0; i < 200; i++)
                                {
                                    if (Main.npc[i].active && Main.npc[i].type == NPCID.WallofFleshEye && Main.npc[i].realLife == npc.whoAmI)
                                    {
                                        if (Main.npc[i].ai[0] <= 0f)
                                            Main.npc[i].ai[0] = -2f;
                                        else
                                            Main.npc[i].ai[0] = 2f;
                                        Main.npc[i].netUpdate = true;
                                    }
                                }
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                        }

                        Counter++;
                        if (Counter >= 180)
                        {
                            Counter = Main.rand.Next(120);
                            if (Main.netMode != 1 && npc.HasPlayerTarget && Main.player[npc.target].active) //vanilla spaz p1 shoot fireball code
                            {
                                Vector2 Speed = Main.player[npc.target].Center - npc.Center;
                                double angle = Speed.ToRotation();
                                if (Speed.X * npc.velocity.X > 0) //don't shoot fireballs behind myself
                                {
                                    Speed.Normalize();
                                    int Damage;
                                    Speed *= 12f;
                                    Damage = npc.damage / 12;
                                    Speed.X += Main.rand.Next(-40, 41) * 0.05f;
                                    Speed.Y += Main.rand.Next(-40, 41) * 0.05f;
                                    Projectile.NewProjectile(npc.Center + Speed * 4f, Speed, ProjectileID.CursedFlameHostile, Damage, 0f, Main.myPlayer);
                                }
                            }
                            npc.netUpdate = true;

                            //tongue the player if they're in hell, too far away, and not debuffed already
                            if (npc.HasPlayerTarget && Main.player[npc.target].active && Main.player[npc.target].ZoneUnderworldHeight
                            && Math.Abs(Main.player[npc.target].Center.X - npc.Center.X) > 3000
                            && !Main.player[npc.target].HasBuff(BuffID.TheTongue))
                            {
                                Main.player[npc.target].AddBuff(BuffID.TheTongue, 10);
                                Main.PlaySound(15, (int)Main.player[npc.target].position.X, (int)Main.player[npc.target].position.Y, 0);
                            }
                        }

                        Timer++;
                        if (Timer >= 600) //ichor vomit
                        {
                            Timer = Main.rand.Next(300);
                            if (npc.HasPlayerTarget && Main.netMode != 1 && Main.player[npc.target].active)
                            {
                                for (int i = 0; i < 10; i++)
                                {
                                    Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                    speed.Y -= Math.Abs(speed.X) * 0.3f; //account for gravity
                                    speed.Normalize();
                                    speed *= 9f;
                                    speed.X += Main.rand.Next(-20, 21) * 0.08f;
                                    speed.Y += Main.rand.Next(-20, 21) * 0.08f;
                                    Projectile.NewProjectile(npc.Center, speed, ProjectileID.GoldenShowerHostile, npc.damage / 25, 0f, Main.myPlayer);
                                }
                            }
                            npc.netUpdate = true;
                        }

                        if (npc.HasPlayerTarget && Main.player[npc.target].dead)// Vector2.Distance(npc.Center, Main.player[npc.target].Center) > 2000)
                        {
                            npc.velocity *= 20;
                        }

                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                        break;

                    case 36: //destroyer head
                        destroyBoss = npc.whoAmI;

                        if (!masoBool[0])
                        {
                            if (npc.life < npc.lifeMax / 2)
                            {
                                masoBool[0] = true;
                                npc.netUpdate = true;
                                for (int i = 0; i < 200; i++) //upgrade all segments
                                {
                                    if (Main.npc[i].realLife == npc.whoAmI)
                                    {
                                        Main.npc[i].GetGlobalNPC<FargoGlobalNPC>().masoHurtAI = 6;
                                        Main.npc[i].netUpdate = true;
                                    }
                                }
                                if (npc.HasPlayerTarget)
                                    Main.PlaySound(15, (int)Main.player[npc.target].position.X, (int)Main.player[npc.target].position.Y, 0);
                            }

                            RegenTimer = 2;
                        }
                        else
                        {
                            if (npc.HasPlayerTarget && !Main.dayTime && !Main.player[npc.target].dead)
                            {
                                npc.position -= npc.velocity;

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

                                const float num14 = 16f;    //max speed?
                                const float num15 = 0.1f;   //turn speed?
                                const float num16 = 0.15f;   //acceleration?
                                float num17 = Main.player[npc.target].Center.X;
                                float num18 = Main.player[npc.target].Center.Y;

                                float num21 = num17 - npc.Center.X;
                                float num22 = num18 - npc.Center.Y;
                                float num23 = (float)Math.Sqrt((double)num21 * (double)num21 + (double)num22 * (double)num22);

                                if (!isOnSolidTile)
                                {
                                    //negating default air behaviour
                                    npc.velocity.Y -= 0.15f;

                                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num14 * 0.4f)
                                    {
                                        if (npc.velocity.X < 0f)
                                            npc.velocity.X += num15 * 1.1f;
                                        else
                                            npc.velocity.X -= num15 * 1.1f;
                                    }
                                    else if (npc.velocity.Y == num14)
                                    {
                                        if (npc.velocity.X < num21)
                                            npc.velocity.X -= num15;
                                        else if (npc.velocity.X > num21)
                                            npc.velocity.X += num15;
                                    }
                                    else if (npc.velocity.Y > 4f)
                                    {
                                        if (npc.velocity.X < 0f)
                                            npc.velocity.X -= num15 * 0.9f;
                                        else
                                            npc.velocity.X += num15 * 0.9f;
                                    }
                                }

                                //ground movement code but it always runs
                                float num2 = (float)Math.Sqrt(num21 * num21 + num22 * num22);
                                float num3 = Math.Abs(num21);
                                float num4 = Math.Abs(num22);
                                float num5 = num14 / num2;
                                float num6 = num21 * num5;
                                float num7 = num22 * num5;
                                if ((npc.velocity.X > 0f && num6 > 0f || npc.velocity.X < 0f && num6 < 0f) && (npc.velocity.Y > 0f && num7 > 0f || npc.velocity.Y < 0f && num7 < 0f))
                                {
                                    if (npc.velocity.X < num6)
                                        npc.velocity.X += num16;
                                    else if (npc.velocity.X > num6)
                                        npc.velocity.X -= num16;
                                    if (npc.velocity.Y < num7)
                                        npc.velocity.Y += num16;
                                    else if (npc.velocity.Y > num7)
                                        npc.velocity.Y -= num16;
                                }
                                if (npc.velocity.X > 0f && num6 > 0f || npc.velocity.X < 0f && num6 < 0f || npc.velocity.Y > 0f && num7 > 0f || npc.velocity.Y < 0f && num7 < 0f)
                                {
                                    if (npc.velocity.X < num6)
                                        npc.velocity.X += num15;
                                    else if (npc.velocity.X > num6)
                                        npc.velocity.X -= num15;
                                    if (npc.velocity.Y < num7)
                                        npc.velocity.Y += num15;
                                    else if (npc.velocity.Y > num7)
                                        npc.velocity.Y -= num15;

                                    if (Math.Abs(num7) < num14 * 0.2f && (npc.velocity.X > 0f && num6 < 0f || npc.velocity.X < 0f && num6 > 0f))
                                    {
                                        if (npc.velocity.Y > 0f)
                                            npc.velocity.Y += num15 * 2f;
                                        else
                                            npc.velocity.Y -= num15 * 2f;
                                    }
                                    if (Math.Abs(num6) < num14 * 0.2f && (npc.velocity.Y > 0f && num7 < 0f || npc.velocity.Y < 0f && num7 > 0f))
                                    {
                                        if (npc.velocity.X > 0f)
                                            npc.velocity.X += num15 * 2f;
                                        else
                                            npc.velocity.X -= num15 * 2f;
                                    }
                                }
                                else if (num3 > num4)
                                {
                                    if (npc.velocity.X < num6)
                                        npc.velocity.X += num15 * 1.1f;
                                    else if (npc.velocity.X > num6)
                                        npc.velocity.X -= num15 * 1.1f;

                                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num14 * 0.5f)
                                    {
                                        if (npc.velocity.Y > 0f)
                                            npc.velocity.Y += num15;
                                        else
                                            npc.velocity.Y -= num15;
                                    }
                                }
                                else
                                {
                                    if (npc.velocity.Y < num7)
                                        npc.velocity.Y += num15 * 1.1f;
                                    else if (npc.velocity.Y > num7)
                                        npc.velocity.Y -= num15 * 1.1f;

                                    if (Math.Abs(npc.velocity.X) + Math.Abs(npc.velocity.Y) < num14 * 0.5f)
                                    {
                                        if (npc.velocity.X > 0f)
                                            npc.velocity.X += num15;
                                        else
                                            npc.velocity.X -= num15;
                                    }
                                }
                                npc.rotation = (float)Math.Atan2(npc.velocity.Y, npc.velocity.X) + 1.57f;
                                npc.netUpdate = true;
                                npc.localAI[0] = 1f;

                                float ratio = (float)npc.life / npc.lifeMax;
                                if (ratio > 0.5f)
                                    ratio = 0.5f;
                                npc.position += npc.velocity * (1.5f - ratio);
                            }
                        }
                        break;

                    case 37: //fishron (regular)
                        fishBoss = npc.whoAmI;
                        npc.position += npc.velocity * 0.25f;
                        switch ((int)npc.ai[0])
                        {
                            case -1: //just spawned
                                npc.dontTakeDamage = true;
                                break;

                            case 0: //phase 1
                                if (!masoBool[1])
                                    npc.dontTakeDamage = false;
                                break;

                            case 1: //p1 dash
                                Counter++;
                                if (Counter > 5)
                                {
                                    Counter = 0;

                                    if (Main.netMode != 1)
                                        NPC.NewNPC((int)npc.position.X + Main.rand.Next(npc.width), (int)npc.position.Y + Main.rand.Next(npc.height), NPCID.DetonatingBubble);
                                }
                                break;

                            case 2: //p1 bubbles
                                break;

                            case 3: //p1 drop nados
                                if (npc.ai[2] == 60f && Main.netMode != 1)
                                {
                                    SpawnRazorbladeRing(npc, 12, 10f, npc.damage / 4, 1f);
                                }
                                break;

                            case 4: //phase 2 transition
                                npc.dontTakeDamage = true;
                                masoBool[1] = false;
                                if (npc.ai[2] == 120)
                                {
                                    int heal = npc.lifeMax - npc.life;
                                    npc.life = npc.lifeMax;
                                    CombatText.NewText(npc.Hitbox, CombatText.HealLife, heal);
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
                                if (Counter > 1)
                                {
                                    Counter = 0;
                                    if (Main.netMode != 1)
                                    {
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubble"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(Math.PI / 2);
                                            Main.npc[n].velocity *= -npc.spriteDirection;
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
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
                                    Projectile.NewProjectile(spawnPos.X, spawnPos.Y, 0f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);

                                    SpawnRazorbladeRing(npc, 12, 10f, npc.damage / 4, 2f);
                                }
                                break;

                            case 9: //phase 3 transition
                                goto case 4;

                            case 10: //phase 3
                                npc.position += npc.velocity * 0.25f;
                                Timer++;
                                if (Timer >= 600) //spawn cthulhunado
                                {
                                    Timer = 0;
                                    if (Main.netMode != 1)
                                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, npc.target + 1);
                                }
                                break;

                            case 11: //p3 dash
                                Counter++;
                                if (Counter > 2)
                                {
                                    Counter = 0;
                                    if (Main.netMode != 1)
                                    {
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubble"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(Math.PI / 2);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                        n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubble"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(-Math.PI / 2);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                    }
                                }
                                goto case 10;

                            case 12: //p3 *teleports behind you*
                                if (npc.ai[2] == 15f)
                                {
                                    SpawnRazorbladeRing(npc, 3, 9f, npc.damage / 4, -0.75f);
                                }
                                else if (npc.ai[2] == 16f)
                                {
                                    if (Main.netMode != 1)
                                    {
                                        Vector2 spawnPos = Vector2.UnitX * npc.direction;
                                        spawnPos = spawnPos.RotatedBy(npc.rotation);
                                        spawnPos *= npc.width + 20f;
                                        spawnPos /= 2f;
                                        spawnPos += npc.Center;
                                        Projectile.NewProjectile(spawnPos.X, spawnPos.Y, 0f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                                    }
                                }
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
                            if (Main.player[Main.myPlayer].active && !Main.player[Main.myPlayer].dead)
                                Main.player[Main.myPlayer].AddBuff(mod.BuffType("NullificationCurse"), 2);
                        }
                        else
                        {
                            Counter++; //phases transition twice as fast when core is exposed

                            if (Main.player[Main.myPlayer].active && !Main.player[Main.myPlayer].dead)
                            {
                                Player player = Main.player[Main.myPlayer];
                                if (player.moonLeech && !player.buffImmune[mod.BuffType("MutantNibble")]) //replace moon bite with mutant nibble
                                {
                                    int buffIndex = player.FindBuffIndex(BuffID.MoonLeech);
                                    if (buffIndex != -1)
                                    {
                                        player.AddBuff(mod.BuffType("MutantNibble"), player.buffTime[buffIndex]);
                                        player.DelBuff(buffIndex);
                                    }
                                }
                                player.AddBuff(BuffID.WaterCandle, 2);
                                player.AddBuff(BuffID.Battle, 2);
                                player.AddBuff(mod.BuffType("NullificationCurse"), 2);
                            }

                            Timer++;
                            if (Timer >= 360)
                            {
                                Timer = Main.rand.Next(180);

                                switch (masoState)
                                {
                                    case 0: //melee
                                        for (int i = 0; i < 3; i++)
                                        {
                                            NPC bodyPart = Main.npc[(int)npc.localAI[i]];

                                            if (bodyPart.active)
                                            {
                                                if (i == 2 && bodyPart.type == NPCID.MoonLordHead)
                                                {
                                                    Vector2 speed = new Vector2(0f, -12f).RotatedBy(MathHelper.ToRadians(-60));
                                                    for (int j = 0; j < 6; j++)
                                                    {
                                                        if (Main.netMode != 1)
                                                        {
                                                            int n = NPC.NewNPC((int)bodyPart.Center.X, (int)bodyPart.Center.Y, NPCID.SolarGoop);
                                                            if (n < 200)
                                                            {
                                                                Main.npc[n].velocity = speed;
                                                                if (Main.netMode == 2)
                                                                    NetMessage.SendData(23, -1, -1, null, n);
                                                            }
                                                        }
                                                        speed = speed.RotatedBy(MathHelper.ToRadians(20));
                                                    }
                                                }
                                                else if (bodyPart.type == NPCID.MoonLordHand)
                                                {
                                                    Vector2 speed = Main.player[npc.target].Center - bodyPart.Center;
                                                    speed.Normalize();
                                                    speed *= 6f;
                                                    int damage = (int)(25 * (1 + FargoWorld.MoonlordCount * .0125));
                                                    for (int j = 0; j < 4; j++)
                                                    {
                                                        if (Main.netMode != 1)
                                                            Projectile.NewProjectile(bodyPart.Center, speed,
                                                                ProjectileID.CultistBossFireBall, damage, 0f, Main.myPlayer);
                                                        speed = speed.RotatedBy(Math.PI / 2);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 1: //ranged
                                        for (int i = 0; i < 12; i++) //spawn lightning
                                        {
                                            Point tileCoordinates = Main.player[npc.target].Top.ToTileCoordinates();

                                            if (Main.rand.Next(2) == 0)
                                            {
                                                tileCoordinates.X += Main.rand.Next(-40, 41);
                                                tileCoordinates.Y += Main.rand.Next(30, 41) * (Main.rand.Next(2) == 0 ? 1 : -1);
                                            }
                                            else
                                            {
                                                tileCoordinates.X += Main.rand.Next(30, 41) * (Main.rand.Next(2) == 0 ? 1 : -1);
                                                tileCoordinates.Y += Main.rand.Next(-40, 41);
                                            }

                                            for (int index = 0; index < 10 && !WorldGen.SolidTile(tileCoordinates.X, tileCoordinates.Y) && tileCoordinates.Y > 10; ++index)
                                                tileCoordinates.Y -= 1;

                                            Projectile.NewProjectile(tileCoordinates.X * 16 + 8, tileCoordinates.Y * 16 + 17, 0f, 0f, 578, 0, 1f, Main.myPlayer);
                                        }

                                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.CultistBossLightningOrb,
                                            (int)(30 * (1 + FargoWorld.MoonlordCount * .0125)), 0f, Main.myPlayer);
                                        break;
                                    case 2: //magic
                                        for (int i = 0; i < 3; i++)
                                        {
                                            NPC bodyPart = Main.npc[(int)npc.localAI[i]];

                                            if (bodyPart.active &&
                                                ((i == 2 && bodyPart.type == NPCID.MoonLordHead) ||
                                                bodyPart.type == NPCID.MoonLordHand))
                                            {
                                                Vector2 distance = Main.player[npc.target].Center - bodyPart.Center;
                                                distance.Normalize();
                                                distance *= 6f;
                                                int damage = (int)(35 * (1 + FargoWorld.MoonlordCount * .0125));
                                                for (int j = -1; j <= 1; j += 2) //aim above and below player
                                                {
                                                    Vector2 speed = distance.RotatedBy(Math.PI / 24 * j);
                                                    for (int k = -2; k <= 2; k++) //fire a 5-spread each
                                                    {
                                                        Projectile.NewProjectile(bodyPart.Center, speed.RotatedBy(Math.PI / 96 * k),
                                                            ProjectileID.NebulaLaser, damage, 0f, Main.myPlayer);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 3: //summoner
                                        for (int i = 0; i < 3; i++)
                                        {
                                            NPC bodyPart = Main.npc[(int)npc.localAI[i]];

                                            if (bodyPart.active &&
                                                ((i == 2 && bodyPart.type == NPCID.MoonLordHead) ||
                                                bodyPart.type == NPCID.MoonLordHand))
                                            {
                                                Vector2 speed = Main.player[npc.target].Center - bodyPart.Center;
                                                speed.Normalize();
                                                speed *= 9f;
                                                for (int j = -3; j <= 3; j++)
                                                {
                                                    Vector2 vel = speed.RotatedBy(Math.PI / 6 * j);
                                                    int n = NPC.NewNPC((int)bodyPart.Center.X, (int)bodyPart.Center.Y, NPCID.AncientLight, 0, 0f, (Main.rand.NextFloat() - 0.5f) * 0.3f * 6.28318548202515f / 60f, vel.X, vel.Y);
                                                    if (n < 200)
                                                    {
                                                        Main.npc[n].velocity = vel;
                                                        Main.npc[n].netUpdate = true;
                                                        if (Main.netMode == 2)
                                                            NetMessage.SendData(23, -1, -1, null, n);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                    case 4: //throwing
                                        /*Timer = 0;
                                        bool makeDragon = !NPC.AnyNPCs(NPCID.CultistDragonHead);
                                        bool makeVisions = !NPC.AnyNPCs(NPCID.AncientCultistSquidhead);
                                        for (int i = 0; i < 3; i++)
                                        {
                                            NPC bodyPart = Main.npc[(int)npc.localAI[i]];

                                            if (bodyPart.active)
                                            {
                                                if (i == 2 && bodyPart.type == NPCID.MoonLordHead)
                                                {
                                                    if (makeDragon && Main.netMode != 1)
                                                        NPC.NewNPC((int)bodyPart.Center.X, (int)bodyPart.Center.Y, NPCID.CultistDragonHead, 0, 0f, 0f, 0f, 0f, npc.target);
                                                }
                                                else if (bodyPart.type == NPCID.MoonLordHand)
                                                {
                                                    if (makeVisions && Main.netMode != 1)
                                                        NPC.NewNPC((int)bodyPart.Center.X, (int)bodyPart.Center.Y, NPCID.AncientCultistSquidhead, 0, 0f, 0f, 0f, 0f, npc.target);
                                                }
                                            }
                                        }*/
                                        break;
                                }
                            }
                        }

                        if (npc.ai[0] == 2f)
                        {
                            Counter = 0;
                            masoState = 4;
                        }

                        int dustType = 87;
                        switch (masoState)
                        {
                            case 0: Main.monolithType = 3; break;
                            case 1: Main.monolithType = 0; dustType = 89; break;
                            case 2: Main.monolithType = 1; dustType = 86; break;
                            case 3: Main.monolithType = 2; dustType = 88; break;
                            case 4: dustType = 91; break;
                        }

                        double MLoffset = MathHelper.ToRadians(360 * Counter / 600);
                        for (int i = 0; i < 5; i++)
                        {
                            int MLdust = Dust.NewDust(npc.Center + new Vector2(120f, 0f).RotatedBy(Math.PI * 2 / 5 * i + MLoffset), 0, 0, dustType, npc.velocity.X * 0.5f, npc.velocity.Y * 0.5f, 0, default(Color), 2f);
                            Main.dust[MLdust].noGravity = true;
                            Main.dust[MLdust].velocity.Y -= 3.5f;
                        }
                        goto case 80;

                    case 39: //splinterling
                        Counter++;
                        if (Counter >= 60)
                        {
                            Counter = 0;

                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, Main.rand.Next(-3, 4), Main.rand.Next(-5, 0), Main.rand.Next(326, 329), npc.damage / 4, 0f, Main.myPlayer);
                        }
                        break;

                    case 40: //golem body
                        if (npc.ai[0] == 0f && npc.velocity.Y == 0f) //manipulating golem jump ai
                        {
                            if (npc.ai[1] > 0f)
                            {
                                npc.ai[1] += 5f; //count up to initiate jump faster
                            }
                            else
                            {
                                float threshold = -2f - (float)Math.Round(18f * npc.life / npc.lifeMax);
                                
                                if (npc.ai[1] < threshold) //jump activates at npc.ai[1] == -1
                                    npc.ai[1] = threshold;
                            }
                        }

                        if (masoBool[0])
                        {
                            if (npc.velocity.Y == 0f) //spawning geysers from floor
                            {
                                masoBool[0] = false;

                                Vector2 spawnPos = new Vector2(npc.position.X, npc.Center.Y);

                                if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16] != null && //in temple
                                    Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].wall == WallID.LihzahrdBrickUnsafe)
                                {
                                    spawnPos.X -= npc.width * 2;
                                    for (int i = 0; i < 2; i++)
                                    {
                                        int tilePosX = (int)spawnPos.X / 16 + npc.width * i * 5 / 16;
                                        int tilePosY = (int)spawnPos.Y / 16;// + 1;

                                        if (Main.tile[tilePosX, tilePosY] == null)
                                            Main.tile[tilePosX, tilePosY] = new Tile();

                                        while (!(Main.tile[tilePosX, tilePosY].nactive() && Main.tileSolid[(int)Main.tile[tilePosX, tilePosY].type]))
                                        {
                                            tilePosY++;
                                            if (Main.tile[tilePosX, tilePosY] == null)
                                                Main.tile[tilePosX, tilePosY] = new Tile();
                                        }

                                        Projectile.NewProjectile(tilePosX * 16 + 8, tilePosY * 16 + 8, 0f, -8f,
                                            ProjectileID.GeyserTrap, npc.damage / 5, 0f, Main.myPlayer);
                                    }
                                }
                                else //outside temple
                                {
                                    spawnPos.X -= npc.width * 7;
                                    for (int i = 0; i < 6; i++)
                                    {
                                        int tilePosX = (int)spawnPos.X / 16 + npc.width * i * 3 / 16;
                                        int tilePosY = (int)spawnPos.Y / 16;// + 1;

                                        if (Main.tile[tilePosX, tilePosY] == null)
                                            Main.tile[tilePosX, tilePosY] = new Tile();

                                        while (!(Main.tile[tilePosX, tilePosY].nactive() && Main.tileSolid[(int)Main.tile[tilePosX, tilePosY].type]))
                                        {
                                            tilePosY++;
                                            if (Main.tile[tilePosX, tilePosY] == null)
                                                Main.tile[tilePosX, tilePosY] = new Tile();
                                        }

                                        Projectile.NewProjectile(tilePosX * 16 + 8, tilePosY * 16 + 8, 0f, -8f,
                                            ProjectileID.GeyserTrap, npc.damage / 5, 0f, Main.myPlayer);
                                    }
                                }
                            }
                        }
                        else if (npc.velocity.Y > 0)
                        {
                            masoBool[0] = true;
                        }

                        Counter++;
                        if (npc.HasPlayerTarget)
                        {
                            Player player = Main.player[npc.target];
                            if (!(player.active && !player.dead && (player.Center.Y < npc.position.Y || player.Center.Y > npc.position.Y + npc.height) && npc.Distance(player.Center) < 2000f))
                            {
                                masoBool[1] = true; //true means player is okay, don't enrage
                            }
                        }
                        if (Counter >= 540)
                        {
                            if (!masoBool[1] && !masoBool[2]) //player never went back to safe zone, enrage
                            {
                                masoBool[2] = true;
                                SharkCount = 2;
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                npc.defDamage *= 20;
                                npc.defDefense *= 999;
                                npc.netUpdate = true;
                                Main.NewText("Golem has enraged!", 175, 75, 255);
                            }

                            Counter = 0;
                            masoBool[1] = false;

                            //spray spiky balls
                            if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16] != null && //in temple
                                Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].wall == WallID.LihzahrdBrickUnsafe)
                            {
                                for (int i = 0; i < 8; i++)
                                    Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height),
                                        0, Main.rand.Next(-15, -4), ProjectileID.SpikyBallTrap, npc.damage / 5, 0f, Main.myPlayer);
                            }
                            else //outside temple
                            {
                                for (int i = 0; i < 16; i++)
                                    Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height),
                                        Main.rand.NextFloat(-1f, 1f), Main.rand.Next(-20, -9), ProjectileID.SpikyBallTrap, npc.damage / 4, 0f, Main.myPlayer);
                            }
                        }

                        if (masoBool[2]) //enraged
                        {
                            npc.damage = npc.defDamage;
                            npc.defense = npc.defDefense;

                            if (npc.HasPlayerTarget)
                            {
                                Player p = Main.player[npc.target];

                                masoState++;
                                if (masoState >= 3 && p.active && !p.dead) //spray random spiky balls
                                {
                                    masoState = 0;
                                    Vector2 speed = p.Center - npc.Center;
                                    speed.Y -= Math.Abs(speed.X) * 0.1f;
                                    speed.Normalize();
                                    speed *= 20f + Main.rand.Next(-50, 51) * 0.025f;
                                    speed = speed.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-10, 11)));
                                    Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height),
                                        speed.X, speed.Y, ProjectileID.SpikyBallTrap, npc.damage / 4, 0f, Main.myPlayer);

                                    Vector2 velocity = p.Center - npc.Center;
                                    velocity.Normalize();
                                    velocity *= 11f;
                                    int max;
                                    masoBool[3] = !masoBool[3];
                                    if (masoBool[3])
                                    {
                                        velocity = velocity.RotatedBy(MathHelper.ToRadians(-25));
                                        max = 6;
                                    }
                                    else
                                    {
                                        velocity = velocity.RotatedBy(MathHelper.ToRadians(-30));
                                        max = 7;
                                    }
                                    for (int i = 0; i < max; i++)
                                    {
                                        int p1 = Projectile.NewProjectile(npc.Center, velocity, ProjectileID.EyeBeam, npc.damage / 4, 0f, Main.myPlayer);
                                        Main.projectile[p1].timeLeft = 300;
                                        velocity = velocity.RotatedBy(MathHelper.ToRadians(10));
                                    }
                                }

                                if (p.mount.Active)
                                    p.mount.Dismount(p);

                                p.AddBuff(BuffID.Dazed, 2);
                                p.AddBuff(mod.BuffType("Crippled"), 2);
                                p.AddBuff(mod.BuffType("ClippedWings"), 2);
                            }
                        }

                        if (!npc.dontTakeDamage)
                        {
                            npc.life += masoBool[2] ? 167 : 9;
                            if (npc.life > npc.lifeMax)
                                npc.life = npc.lifeMax;

                            Timer++;
                            if (Timer >= 75)
                            {
                                Timer = Main.rand.Next(30);
                                CombatText.NewText(npc.Hitbox, CombatText.HealLife, masoBool[2] ? 9999 : 500);
                            }
                        }
                        break;

                    case 41: //golem head flying
                        npc.position += npc.velocity * 0.25f;
                        npc.position.Y += npc.velocity.Y * 0.25f;

                        Timer++;
                        if (Timer > 2 && masoBool[0]) //flamethrower if player roughly below me and outside temple
                        {
                            Timer = 0;

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                            if (t != -1)
                            {
                                Player player = Main.player[t];
                                Vector2 distance = player.Center - npc.Center;
                                if (Math.Abs(distance.X) < npc.width * 3)
                                {
                                    double angle = distance.ToRotation();
                                    if (angle > Math.PI * .25 && angle < Math.PI * .75)
                                    {
                                        Projectile.NewProjectile(npc.Center.X + npc.velocity.X * 5, npc.position.Y + npc.velocity.Y * 5,
                                            Main.rand.NextFloat(-2f, 2f), 9f,
                                            ProjectileID.FlamesTrap, npc.damage / 4, 0f, Main.myPlayer);
                                        Main.PlaySound(SoundID.Item34, npc.Center);
                                    }
                                }
                            }
                        }

                        if (Counter-- <= 0)
                        {
                            //masobool[0] = is NOT in temple, used for flamethrower (put here to check less often)
                            masoBool[0] = (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16] != null
                                && Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].wall != WallID.LihzahrdBrickUnsafe);

                            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer(); //shoot lasers
                            if (t != -1 && NPC.golemBoss != -1 && Main.npc[NPC.golemBoss].active && Main.npc[NPC.golemBoss].type == NPCID.Golem)
                            {
                                Counter = (int)(300f * Main.npc[NPC.golemBoss].life / Main.npc[NPC.golemBoss].lifeMax);

                                Vector2 velocity = Main.player[t].Center - npc.Center;
                                velocity.Normalize();
                                velocity *= 11f;
                                int max;
                                masoBool[1] = !masoBool[1];
                                if (masoBool[1])
                                {
                                    velocity = velocity.RotatedBy(MathHelper.ToRadians(-15));
                                    max = 4;
                                }
                                else
                                {
                                    velocity = velocity.RotatedBy(MathHelper.ToRadians(-10));
                                    max = 3;
                                }
                                for (int i = 0; i < max; i++)
                                {
                                    int p = Projectile.NewProjectile(npc.Center, velocity,
                                        ProjectileID.EyeBeam, npc.damage / 32 * 7, 0f, Main.myPlayer);
                                    Main.projectile[p].timeLeft = 300;
                                    velocity = velocity.RotatedBy(MathHelper.ToRadians(10));
                                }
                            }
                            else
                            {
                                Counter = 300; //failsafe
                            }
                        }
                        break;

                    case 42: //golem head
                        if (!npc.dontTakeDamage)
                        {
                            npc.life += 8;
                            if (npc.life > npc.lifeMax)
                                npc.life = npc.lifeMax;

                            Timer++;
                            if (Timer >= 75)
                            {
                                Timer = Main.rand.Next(30);
                                CombatText.NewText(npc.Hitbox, CombatText.HealLife, 500);
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
                                if (player.active && Main.netMode != 1)
                                {
                                    Vector2 velocity = player.Center - npc.Center;
                                    velocity.Normalize();
                                    velocity *= 12f;

                                    Projectile.NewProjectile(npc.Center, velocity, ProjectileID.PoisonDartTrap, 30, 0f, Main.myPlayer);
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

                    case 46: //DEPRECATED - phantasm dragon non-head segments
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
                                    Projectile.NewProjectile(npc.Center.X, npc.position.Y, Main.rand.Next(-3, 4), Main.rand.Next(-4, 0), Main.rand.Next(326, 329), npc.damage * 2 / 5, 0f, Main.myPlayer);
                                }
                            }
                        }
                        break;

                    case 49: //corite
                        Aura(npc, 250, BuffID.Burning, false, DustID.Fire);
                        break;

                    case 50: //brain suckler
                        Counter++;
                        if (Counter >= 300)
                        {
                            if (npc.ai[0] != 5f) //if not latched on player
                            {
                                Shoot(npc, 60, 1000, 9, ProjectileID.NebulaLaser, (int)(npc.damage * 0.4f), 0);
                            }

                            Counter = (short)Main.rand.Next(120);
                        }
                        break;

                    case 51: //plantera
                        if (!masoBool[0]) //spawn protective crystal ring once
                        {
                            masoBool[0] = true;
                            if (Main.netMode != 1)
                            {
                                const int max = 5;
                                const float distance = 130f;
                                float rotation = 2f * (float)Math.PI / max;
                                for (int i = 0; i < max; i++)
                                {
                                    Vector2 spawnPos = npc.Center + new Vector2(distance, 0f).RotatedBy(rotation * i);
                                    int n = NPC.NewNPC((int)spawnPos.X, (int)spawnPos.Y, mod.NPCType("CrystalLeaf"), 0, npc.whoAmI, distance, 300, rotation * i);
                                    if (Main.netMode == 2 && n < 200)
                                        NetMessage.SendData(23, -1, -1, null, n);
                                }
                            }
                        }

                        if (npc.life <= npc.lifeMax / 2) //phase 2
                        {
                            masoBool[1] = true;
                            npc.defense += 21;

                            Counter++;
                            if (Counter >= 30)
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
                                        damage = damage * 2;
                                    else if (Main.expertMode)
                                        damage = damage * 9 / 10;
                                    damage = (int)(damage * (1 + FargoWorld.PlanteraCount * .0125));

                                    Vector2 velocity = Main.player[t].Center - npc.Center;
                                    velocity.Normalize();
                                    velocity *= Main.expertMode ? 17f : 15f;

                                    int p = Projectile.NewProjectile(npc.Center + velocity * 3f, velocity, type, damage, 0f, Main.myPlayer);
                                    if (type != ProjectileID.ThornBall)
                                        Main.projectile[p].timeLeft = 300;
                                }
                            }

                            /*Timer++;
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
                            }*/

                            SharkCount = 0;

                            if (npc.HasPlayerTarget)
                            {
                                if (Main.player[npc.target].venom)
                                {
                                    npc.position += npc.velocity / 3f;
                                    npc.defense *= 2;
                                    Counter++;
                                    SharkCount = 1;

                                    if (RegenTimer > 120)
                                        RegenTimer = 120;
                                }
                            }
                            
                            if (RegenTimer <= 2 && npc.life + 1 + npc.lifeMax / 25 >= npc.lifeMax / 2)
                            {
                                npc.life = npc.lifeMax / 2;
                                npc.lifeRegen = 0;
                                RegenTimer = 2;
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

                        if (npc.ai[0] != 2f) //in phase 1
                        {
                            if (npc.life < npc.lifeMax / 2) //enter phase 2
                            {
                                npc.ai[0] = 2f;
                                npc.ai[3] = 0f;
                                npc.netUpdate = true;

                                if (!NPC.AnyNPCs(NPCID.PrimeLaser)) //revive all dead limbs
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeLaser, npc.whoAmI, 1f, npc.whoAmI, 0f, 150f, npc.target);
                                if (!NPC.AnyNPCs(NPCID.PrimeSaw))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeSaw, npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, npc.target);
                                if (!NPC.AnyNPCs(NPCID.PrimeCannon))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeCannon, npc.whoAmI, -1f, npc.whoAmI, 0f, 150f, npc.target);
                                if (!NPC.AnyNPCs(NPCID.PrimeVice))
                                    NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeVice, npc.whoAmI, -1f, npc.whoAmI, 0f, 0f, npc.target);

                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                                return;
                            }

                            if (npc.ai[0] != 1f) //limb is dead and needs reviving
                            {
                                npc.ai[3]++;
                                if (npc.ai[3] > 1200f) //revive a dead limb
                                {
                                    npc.ai[3] = 0;
                                    npc.netUpdate = true;
                                    if (Main.netMode != 1)
                                    {
                                        int n = 200;
                                        switch ((int)npc.ai[0])
                                        {
                                            case 3: //laser
                                                n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeLaser, npc.whoAmI, 1f, npc.whoAmI, 0f, 150f, npc.target);
                                                break;
                                            case 4: //cannon
                                                n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeCannon, npc.whoAmI, -1f, npc.whoAmI, 0f, 0f, npc.target);
                                                break;
                                            case 5: //saw
                                                n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeSaw, npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, npc.target);
                                                break;
                                            case 6: //vice
                                                n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeVice, npc.whoAmI, -1f, npc.whoAmI, 0f, 150f, npc.target);
                                                break;
                                            default:
                                                break;
                                        }
                                        if (n < 200)
                                        {
                                            Main.npc[n].life = Main.npc[n].lifeMax / 5;
                                            Main.npc[n].netUpdate = true;
                                        }
                                    }

                                    if (!NPC.AnyNPCs(NPCID.PrimeLaser)) //look for any other dead limbs
                                        npc.ai[0] = 3f;
                                    else if (!NPC.AnyNPCs(NPCID.PrimeCannon))
                                        npc.ai[0] = 4f;
                                    else if (!NPC.AnyNPCs(NPCID.PrimeSaw))
                                        npc.ai[0] = 5f;
                                    else if (!NPC.AnyNPCs(NPCID.PrimeVice))
                                        npc.ai[0] = 6f;
                                    else
                                        npc.ai[0] = 1f;
                                }
                            }

                            Timer++;
                            if (Timer >= 300)
                            {
                                Timer = 0;

                                if (npc.HasPlayerTarget) //skeleton commando rockets LUL
                                {
                                    Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                    speed.X += Main.rand.Next(-20, 21);
                                    speed.Y += Main.rand.Next(-20, 21);
                                    speed.Normalize();

                                    int damage = npc.damage / 4;

                                    if (Main.netMode != 1)
                                    {
                                        Projectile.NewProjectile(npc.Center, 4f * speed, ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                        Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(5f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                        Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(-5f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                    }

                                    Main.PlaySound(SoundID.Item11, npc.Center);
                                }
                            }
                        }
                        else
                        {
                            masoHurtAI = 6;
                            if (npc.ai[1] == 1f && npc.ai[2] > 2f)
                            {
                                if (npc.velocity.Length() < 10f)
                                {
                                    npc.velocity.Normalize();
                                    npc.velocity *= 10f;
                                }
                            }
                            else if (npc.ai[1] != 2f)
                            {
                                npc.position += npc.velocity / 3f;
                            }

                            if (npc.ai[3] >= 0f) //spawn 4 more limbs
                            {
                                npc.ai[3]++;
                                if (npc.ai[3] >= 180f)
                                {
                                    npc.ai[3] = -1f;
                                    npc.netUpdate = true;
                                    if (Main.netMode != 1)
                                    {
                                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeLaser, npc.whoAmI, -1f, npc.whoAmI, 0f, 150f, npc.target);
                                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeSaw, npc.whoAmI, -1f, npc.whoAmI, 0f, 0f, npc.target);
                                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeCannon, npc.whoAmI, 1f, npc.whoAmI, 0f, 150f, npc.target);
                                        NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.PrimeVice, npc.whoAmI, 1f, npc.whoAmI, 0f, 0f, npc.target);
                                    }
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

                                    if (Main.netMode != 1)
                                    {
                                        Projectile.NewProjectile(npc.Center, 4f * speed, ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                        Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(5f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                        Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(-5f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                    }

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

                    case 59: //storm diver - default: if (npc.localAI[2] >= 360f + Main.rand.Next(360) && etc)
                        if (npc.localAI[2] >= 180f + Main.rand.Next(180) && npc.Distance(Main.player[npc.target].Center) < 400f && Math.Abs(npc.DirectionTo(Main.player[npc.target].Center).Y) < 0.5f && Collision.CanHitLine(npc.Center, 0, 0, Main.player[npc.target].Center, 0, 0))
                        {
                            npc.localAI[2] = 0f;
                            Vector2 vector2_1 = npc.Center;
                            vector2_1.X += npc.direction * 30f;
                            vector2_1.Y += 2f;

                            Vector2 vec = npc.DirectionTo(Main.player[npc.target].Center) * 7f;
                            if (vec.HasNaNs())
                                vec = new Vector2(npc.direction * 8f, 0f);

                            if (Main.netMode != 1)
                            {
                                int Damage = Main.expertMode ? 50 : 75;
                                for (int index = 0; index < 4; ++index)
                                {
                                    Vector2 vector2_2 = vec + Utils.RandomVector2(Main.rand, -0.8f, 0.8f);
                                    Projectile.NewProjectile(vector2_1.X, vector2_1.Y, vector2_2.X, vector2_2.Y, mod.ProjectileType("StormDiverBullet"), Damage, 1f, Main.myPlayer);
                                }
                            }

                            Main.PlaySound(SoundID.Item36, npc.Center);
                        }
                        break;

                    case 60: //elf copter
                        if (npc.localAI[0] >= 14f)
                        {
                            npc.localAI[0] = 0f;

                            if (Main.netMode != 1)
                            {
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
                                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, SpeedX, SpeedY, mod.ProjectileType("ElfCopterBullet"), 32, 0f, Main.myPlayer);
                            }
                        }
                        break;

                    case 61: //destroyer body/tail
                        if (npc.realLife >= 0 && npc.realLife < 200)
                        {
                            if (Main.npc[npc.realLife].life > 0)
                            {
                                int cap = Main.npc[npc.realLife].lifeMax / Main.npc[npc.realLife].life;
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
                        if (npc.ai[2] > 0f && npc.ai[1] <= 65f)
                        {
                            if (Main.netMode != 1)
                            {
                                for (int index = 0; index < 6; ++index)
                                {
                                    float num6 = Main.player[npc.target].Center.X - npc.Center.X;
                                    float num10 = Main.player[npc.target].Center.Y - npc.Center.Y;
                                    float num11 = 11f / (float)Math.Sqrt(num6 * num6 + num10 * num10);
                                    float num12;
                                    float num18 = num12 = num6 + Main.rand.Next(-40, 41);
                                    float num19;
                                    float num20 = num19 = num10 + Main.rand.Next(-40, 41);
                                    float SpeedX = num18 * num11;
                                    float SpeedY = num20 * num11;
                                    int damage = Main.expertMode ? 40 : 50;
                                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, SpeedX, SpeedY, mod.ProjectileType("TacticalSkeletonBullet"), damage, 0f, Main.myPlayer);
                                }
                            }
                            Main.PlaySound(SoundID.Item38, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.ai[3] = 0f; //specific to me
                            npc.netUpdate = true;
                        }
                        break;

                    case 63: //skeleton sniper, num3 = 200, num8 = 0
                        if (npc.ai[2] > 0f && npc.ai[1] <= 105f)
                        {
                            if (Main.netMode != 1)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-40, 41) * 0.2f;
                                speed.Y += Main.rand.Next(-40, 41) * 0.2f;
                                speed.Normalize();
                                speed *= 11f;

                                int damage = Main.expertMode ? 80 : 100;
                                Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("SniperBullet"), damage, 0f, Main.myPlayer);
                            }
                            Main.PlaySound(SoundID.Item40, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        break;

                    case 64: //skeleton archer, damage = 28/35, ID.VenomArrow
                        if (npc.ai[2] > 0f && npc.ai[1] <= 40f)
                        {
                            if (Main.netMode != 1)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.Y -= Math.Abs(speed.X) * 0.075f; //account for gravity (default *0.1f)
                                speed.X += Main.rand.Next(-24, 25);
                                speed.Y += Main.rand.Next(-24, 25);
                                speed.Normalize();
                                speed *= 11f;

                                int damage = Main.expertMode ? 28 : 35;
                                Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("SkeletonArcherArrow"), damage, 0f, Main.myPlayer);
                            }
                            Main.PlaySound(SoundID.Item5, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        break;

                    case 65: //skeleton commando, num3 = 90, num5 = 4f, damage = 48/60, ID.RocketSkeleton
                        if (npc.ai[2] > 0f && npc.ai[1] <= 50f)
                        {
                            if (Main.netMode != 1)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();

                                int damage = Main.expertMode ? 48 : 60;
                                Projectile.NewProjectile(npc.Center, 4f * speed, ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(10f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, 3f * speed.RotatedBy(MathHelper.ToRadians(-10f)), ProjectileID.RocketSkeleton, damage, 0f, Main.myPlayer);
                            }
                            Main.PlaySound(SoundID.Item11, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        break;

                    case 66: //elf archer, num3 = 110, damage = 36/45, tsunami
                        if (npc.ai[2] > 0f && npc.ai[1] <= 60f)
                        {
                            if (Main.netMode != 1)
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
                                    Vector2 vector2_5 = spinningpoint.RotatedBy(num3 * num8);
                                    if (!flag4)
                                        vector2_5 -= spinningpoint;
                                    int p = Projectile.NewProjectile(npc.Center + vector2_5, speed, mod.ProjectileType("ElfArcherArrow"), damage, 0f, Main.myPlayer);
                                    Main.projectile[p].noDropItem = true;
                                }
                            }
                            Main.PlaySound(SoundID.Item5, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        break;

                    case 67: //pirate crossbower, num3 = 80, num5 = 16f, num8 = Math.Abs(num7) * .08f, damage = 32/40, num12 = 800f?
                        if (npc.ai[2] > 0f && npc.ai[1] <= 45f)
                        {
                            if (Main.netMode != 1)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();
                                speed *= 11f;

                                int damage = Main.expertMode ? 32 : 40;
                                Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("PirateCrossbowerArrow"), damage, 0f, Main.myPlayer);
                            }
                            Main.PlaySound(SoundID.Item5, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        break;

                    case 68: //pirate deadeye, num3 = 40, num5 = 14f, num8 = 0f, damage = 20/25, num12 = 550f?
                        if (npc.ai[2] > 0f && npc.ai[1] <= 25f)
                        {
                            if (Main.netMode != 1)
                            {
                                Vector2 speed = Main.player[npc.target].Center - npc.Center;
                                speed.X += Main.rand.Next(-20, 21);
                                speed.Y += Main.rand.Next(-20, 21);
                                speed.Normalize();
                                speed *= 14f;

                                int damage = Main.expertMode ? 20 : 25;
                                Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("PirateDeadeyeBullet"), damage, 0f, Main.myPlayer);
                            }
                            Main.PlaySound(SoundID.Item11, npc.Center);
                            npc.ai[2] = 0f;
                            npc.ai[1] = 0f;
                            npc.netUpdate = true;
                        }
                        break;

                    case 69: //pirate captain, 60 delay for cannonball, 8 for bullets
                        if (npc.ai[2] > 0f && npc.localAI[2] >= 20f && npc.ai[1] <= 30)
                        {
                            //npc.localAI[2]++;
                            if (Main.netMode != 1)
                            {
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
                            }
                            //npc.ai[2] = 0f;
                            //npc.ai[1] = 0f;
                            npc.netUpdate = true;
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

                        npc.dontTakeDamage = true;
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

                        Aura(npc, 100, mod.BuffType("SqueakyToy"));
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

                                bool bossAlive = false;
                                for (int i = 0; i < 200; i++)
                                {
                                    if (Main.npc[i].boss)
                                    {
                                        bossAlive = true;
                                        break;
                                    }
                                }

                                if (Main.netMode != 1)
                                {
                                    if (bossAlive)
                                    {
                                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.BouncyGrenade, 60, 8f, Main.myPlayer);
                                    }
                                    else
                                    {
                                        for (int i = 0; i < 100; i++)
                                        {
                                            int type = ProjectileID.Grenade;
                                            int damage = 250;
                                            float knockback = 8f;
                                            switch (Main.rand.Next(10))
                                            {
                                                case 0: 
                                                case 1:
                                                case 2: type = ProjectileID.HappyBomb; damage = 100; break;
                                                case 3: 
                                                case 4: 
                                                case 5: 
                                                case 6: type = ProjectileID.BouncyGrenade; damage = 60; break;
                                                case 7: 
                                                case 8: 
                                                case 9: type = ProjectileID.StickyGrenade; damage = 60; break;
                                            }

                                            int p = Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height), Main.rand.Next(-1000, 1001) / 100f, Main.rand.Next(-2000, 101) / 100f, type, damage, knockback, Main.myPlayer);
                                            Main.projectile[p].timeLeft += Main.rand.Next(180);
                                        }

                                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.BouncyDynamite, 250, 20f, Main.myPlayer);
                                    }
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
                            RegenTimer = 2;
                            if (Main.npc[(int)npc.ai[1]].type == NPCID.SkeletronPrime && Main.npc[(int)npc.ai[1]].ai[0] == 2f)
                            {
                                masoBool[0] = true;
                                npc.defDefense = 9999;
                                npc.defense = 9999;
                                npc.defDamage = npc.defDamage * 4 / 3;
                                npc.damage = npc.defDamage;
                                int heal = npc.lifeMax - npc.life;
                                npc.life = npc.lifeMax;
                                if (heal > 0)
                                    CombatText.NewText(npc.Hitbox, CombatText.HealLife, heal);
                                npc.netUpdate = true;
                            }
                        }
                        else
                        {
                            npc.position += npc.velocity / 3f;
                            npc.dontTakeDamage = true;
                        }
                        break;

                    case 78: //wall of flesh eye
                        npc.ai[1]++;
                        const float maxTime = 540f;
                        if (npc.ai[1] >= maxTime)
                        {
                            npc.ai[1] = 0f;
                            if (npc.ai[2] == 0f)
                                npc.ai[2] = 1f;
                            else
                                npc.ai[2] *= -1f;
                            if (npc.ai[2] == 2f) //FIRE LASER
                            {
                                if (Main.netMode != 1)
                                {
                                    Vector2 speed = Vector2.UnitX.RotatedBy(npc.ai[3]);
                                    float ai0 = (npc.realLife != -1 && Main.npc[npc.realLife].velocity.X > 0) ? 1f : 0f;
                                    if (Main.netMode != 1)
                                        Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("PhantasmalDeathrayWOF"), npc.damage / 4, 0f, Main.myPlayer, ai0, npc.whoAmI);
                                }
                            }
                            npc.netUpdate = true;
                        }

                        if (npc.ai[2] >= 0f)
                        {
                            SharkCount = 4;
                            npc.dontTakeDamage = true;
                            if (npc.ai[1] <= 90 && npc.ai[2] == 2f)
                            {
                                npc.localAI[1] = 0f;
                                npc.rotation = npc.ai[3];
                            }
                            else
                            {
                                npc.ai[2] = 1f;
                            }
                        }
                        else
                        {
                            SharkCount = 0;
                            npc.dontTakeDamage = false;
                            if (npc.ai[1] > maxTime - 180f)
                            {
                                if (Main.rand.Next(4) < 3) //dust telegraphs switch
                                {
                                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, 90, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 114, default(Color), 3.5f);
                                    Main.dust[dust].noGravity = true;
                                    Main.dust[dust].velocity *= 1.8f;
                                    Main.dust[dust].velocity.Y -= 0.5f;
                                    if (Main.rand.Next(4) == 0)
                                    {
                                        Main.dust[dust].noGravity = false;
                                        Main.dust[dust].scale *= 0.5f;
                                    }
                                }

                                const float stopTime = maxTime - 90f;
                                if (npc.ai[1] == stopTime) //shoot warning dust in phase 2
                                {
                                    if (npc.ai[0] == 2f || npc.ai[0] == -2f)
                                    {
                                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                                        if (t != -1)
                                        {
                                            Main.PlaySound(15, (int)Main.player[t].position.X, (int)Main.player[t].position.Y, 0);
                                            //Main.NewText("found target OK");
                                            npc.ai[2] = -2f;
                                            npc.ai[3] = (npc.Center - Main.player[t].Center).ToRotation();
                                            //Main.NewText("stored rotation, warning dust");
                                            Vector2 offset = Vector2.UnitX.RotatedBy(npc.ai[3] + Math.PI) * 10f;
                                            for (int i = 0; i < 240; i++) //dust warning line for laser
                                            {
                                                int d = Dust.NewDust(npc.Center + offset * i, 1, 1, 112, 0f, 0f, 0, default(Color), 1.5f);
                                                Main.dust[d].noGravity = true;
                                                Main.dust[d].velocity *= 0.5f;
                                            }
                                            if (npc.realLife != -1 && Main.npc[npc.realLife].velocity.X > 0)
                                                npc.ai[3] += (float)Math.PI;
                                        }
                                    }
                                    npc.netUpdate = true;
                                }
                                else if (npc.ai[1] > stopTime && npc.ai[2] == -2f)
                                {
                                    npc.localAI[1] = 0f;
                                    npc.rotation = npc.ai[3];
                                }
                            }
                        }
                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                        break;

                    case 79: //golem fists
                        if (npc.ai[0] == 0f && masoBool[0])
                        {
                            masoBool[0] = false;
                            if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16] != null //NOT in temple
                                && Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].wall != WallID.LihzahrdBrickUnsafe
                                && Main.netMode != 1)
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("FuseBomb"), npc.damage / 4, 0f, Main.myPlayer);
                        }
                        masoBool[0] = npc.ai[0] != 0f;
                        npc.life += 167;
                        if (npc.life > npc.lifeMax)
                            npc.life = npc.lifeMax;
                        Timer++;
                        if (Timer >= 75)
                        {
                            Timer = Main.rand.Next(30);
                            CombatText.NewText(npc.Hitbox, CombatText.HealLife, 9999);
                        }
                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                        break;

                    case 80: //moon lord all parts
                        Counter++;
                        if (Counter > 1800)
                        {
                            Counter = 0;
                            masoState++;
                            if (masoState > 3)
                                masoState = 0;
                        }
                        if (npc.type != NPCID.MoonLordCore)
                        {
                            RegenTimer = 2;
                            if (npc.ai[0] == -2f) //eye socket is empty
                            {
                                if (npc.ai[1] == 0f //happens every 32 ticks
                                    && Main.npc[(int)npc.ai[3]].ai[0] != 2f) //will stop when ML dies
                                {
                                    Timer++;
                                    if (Timer >= 29) //warning dust, reset timer
                                    {
                                        Timer = 0;
                                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                                        if (t != -1)
                                        {
                                            npc.localAI[0] = (Main.player[t].Center - npc.Center).ToRotation();
                                            Vector2 offset = Vector2.UnitX.RotatedBy(npc.localAI[0]) * 10f;
                                            for (int i = 0; i < 300; i++) //dust warning line for laser
                                            {
                                                int d = Dust.NewDust(npc.Center + offset * i, 1, 1, 111, 0f, 0f, 0, default(Color), 1.5f);
                                                Main.dust[d].noGravity = true;
                                                Main.dust[d].velocity *= 0.5f;
                                            }
                                        }
                                    }
                                    if (Timer == 2) //FIRE LASER
                                    {
                                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                                        if (t != -1)
                                        {
                                            if (Main.netMode != 1)
                                            {
                                                float newRotation = (Main.player[t].Center - npc.Center).ToRotation();
                                                float difference = newRotation - npc.localAI[0];
                                                //Main.NewText(newRotation.ToString() + " - " + npc.localAI[0].ToString() + " = " + difference.ToString());
                                                const float PI = (float)Math.PI;
                                                float rotationDirection = PI / 3f / 120f; //positive is CW, negative is CCW
                                                if (difference < -PI)
                                                    difference += 2f * PI;
                                                if (difference > PI)
                                                    difference -= 2f * PI;
                                                if (difference < 0f)
                                                    rotationDirection *= -1f;
                                                Vector2 speed = Vector2.UnitX.RotatedBy(npc.localAI[0]);
                                                int damage = (int)(75 * (1 + FargoWorld.MoonlordCount * .0125));
                                                if (Main.netMode != 1)
                                                    Projectile.NewProjectile(npc.Center, speed, mod.ProjectileType("PhantasmalDeathrayML"),
                                                        damage, 0f, Main.myPlayer, rotationDirection, npc.whoAmI);
                                            }
                                        }
                                    }
                                    npc.netUpdate = true;
                                }
                                //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                                //Main.NewText("L0 " + npc.localAI[0].ToString() + ", L1 " + npc.localAI[1].ToString() + ", L2 " + npc.localAI[2].ToString() + ", L3 " + npc.localAI[3].ToString());
                            }
                        }
                        break;

                    case 81: //ancient light when moon lord is alive
                        if (npc.HasPlayerTarget)
                        {
                            Vector2 speed = Main.player[npc.target].Center - npc.Center;
                            speed.Normalize();
                            speed *= 9f;

                            npc.ai[2] += speed.X / 100f;
                            if (npc.ai[2] > 9f)
                                npc.ai[2] = 9f;
                            if (npc.ai[2] < -9f)
                                npc.ai[2] = -9f;
                            npc.ai[3] += speed.Y / 100f;
                            if (npc.ai[3] > 9f)
                                npc.ai[3] = 9f;
                            if (npc.ai[3] < -9f)
                                npc.ai[3] = -9f;
                        }
                        else
                        {
                            npc.TargetClosest(false);
                        }

                        Counter++;
                        if (Counter > 240)
                        {
                            npc.HitEffect(0, 9999);
                            npc.active = false;
                        }

                        npc.velocity.X = npc.ai[2];
                        npc.velocity.Y = npc.ai[3];
                        npc.dontTakeDamage = true;
                        break;

                    case 82:
                        npc.dontTakeDamage = true;
                        break;

                    //ok lets make them dash instead
                    case 83: //demon eye
                        Counter++;
                        if (Counter >= 420 && Main.rand.Next(60) == 0)
                        {
                            npc.TargetClosest();

                            Vector2 velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 12;
                            npc.velocity = velocity;
                            Counter = 0;
                        }

                        if (Math.Abs(npc.velocity.Y) > 5 || Math.Abs(npc.velocity.X) > 5)
                        {
                            //dust!
                            int dustId = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width, npc.height + 5, DustID.Stone, npc.velocity.X * 0.2f,
                                npc.velocity.Y * 0.2f, 100, default(Color), 1f);
                            Main.dust[dustId].noGravity = true;
                            int dustId3 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + 2f), npc.width, npc.height + 5, DustID.Stone, npc.velocity.X * 0.2f,
                                npc.velocity.Y * 0.2f, 100, default(Color), 1f);
                            Main.dust[dustId3].noGravity = true;
                        }

                        break;
                    
                    //hoppin jack
                    case 84:
                        Counter++;

                        if (Counter >= 20 && npc.velocity.X != 0)
                        {
                            int p = Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.MolotovFire, (int)(npc.damage / 2), 1f, Main.myPlayer);
                            Main.projectile[p].hostile = true;
                            Main.projectile[p].friendly = false;
                            Counter = 0;
                        }
                         
                        break;
                    
                    //antlion
                    case 85:
                        Counter++;
                        if (Counter >= 30)
                        {
                            foreach (Player p in Main.player.Where(x => x.active && !x.dead))
                            {
                                if (npc.Distance(p.Center) < 250)
                                {
                                    Vector2 velocity = Vector2.Normalize(npc.Center - p.Center) * 5f;
                                    p.velocity += velocity;
                                }
                            }
                            Counter = 0;
                        }

                        break;

                    //nimbus
                    case 86:
                        Counter++;
                        if (Counter >= 360)
                        {
                            Projectile.NewProjectile(new Vector2(npc.Center.X + 100, npc.Center.Y), Vector2.Zero, ProjectileID.VortexVortexLightning, 0, 1, Main.myPlayer, 0, 1);
                            Projectile.NewProjectile(new Vector2(npc.Center.X - 100, npc.Center.Y), Vector2.Zero, ProjectileID.VortexVortexLightning, 0, 1, Main.myPlayer, 0, 1);
                            Counter = 0;
                        }
                        break;

                    //unicorn
                    case 87:
                        Counter++;
                        if (Math.Abs(npc.velocity.X) > 1 && Counter >= 3)
                        {
                            int direction = npc.velocity.X > 0 ? 1 : -1;
                            int p = Projectile.NewProjectile(new Vector2(npc.Center.X - direction * ( npc.width / 2), npc.Center.Y), npc.velocity, ProjectileID.RainbowBack, npc.damage / 3, 1);
                            if (p < 1000)
                            {
                                Main.projectile[p].friendly = false;
                                Main.projectile[p].hostile = true;
                            }
                            Counter = 0;
                        }
                        break;

                    case 89: //fishron EX
                        fishBoss = fishBossEX = npc.whoAmI;
                        npc.position += npc.velocity * 0.5f;
                        //Main.NewText("ai0 " + npc.ai[0].ToString() + ", ai1 " + npc.ai[1].ToString() + ", ai2 " + npc.ai[2].ToString() + ", ai3 " + npc.ai[3].ToString());
                        switch ((int)npc.ai[0])
                        {
                            case -1: //just spawned
                                if (npc.ai[2] == 1 && Main.netMode != 1)
                                {
                                    int p = Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("FishronRitual"), 0, 0f, Main.myPlayer, npc.lifeMax, npc.whoAmI);
                                    if (p < 0 || p >= 1000) //failed to spawn projectile, abort spawn
                                    {
                                        npc.active = false;
                                        //Main.NewText("abort");
                                    }
                                }
                                masoBool[2] = true;
                                break;

                            case 0: //phase 1
                                if (!masoBool[1])
                                    npc.dontTakeDamage = false;
                                masoBool[2] = false;
                                npc.ai[2]++;
                                break;

                            case 1: //p1 dash
                                npc.position += npc.velocity * 0.25f;
                                Counter++;
                                if (Counter > 5)
                                {
                                    Counter = 0;
                                    if (Main.netMode != 1)
                                        NPC.NewNPC((int)npc.position.X + Main.rand.Next(npc.width), (int)npc.position.Y + Main.rand.Next(npc.height), NPCID.DetonatingBubble);
                                }
                                break;

                            case 2: //p1 bubbles
                                if (npc.ai[2] == 0f && Main.netMode != 1)
                                    Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, npc.target + 1);
                                break;

                            case 3: //p1 drop nados
                                if (npc.ai[2] == 60f && Main.netMode != 1)
                                {
                                    const int max = 32;
                                    float rotation = 2f * (float)Math.PI / max;
                                    for (int i = 0; i < max; i++)
                                    {
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubbleEX"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = Vector2.UnitY.RotatedBy(rotation * i);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                    }

                                    SpawnRazorbladeRing(npc, 18, 10f, npc.damage / 6, 1f);
                                }
                                break;

                            case 4: //phase 2 transition
                                masoBool[1] = false;
                                masoBool[2] = true;
                                if (npc.ai[2] == 1 && Main.netMode != 1)
                                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("FishronRitual"), 0, 0f, Main.myPlayer, npc.lifeMax / 4, npc.whoAmI);
                                if (npc.ai[2] >= 114)
                                {
                                    Counter++;
                                    if (Counter > 6) //display healing effect
                                    {
                                        Counter = 0;
                                        int heal = (int)(npc.lifeMax * Main.rand.NextFloat(0.1f, 0.12f));
                                        npc.life += heal;
                                        if (npc.life > npc.lifeMax)
                                            npc.life = npc.lifeMax;
                                        CombatText.NewText(npc.Hitbox, CombatText.HealLife, heal);
                                    }
                                }
                                break;

                            case 5: //phase 2
                                if (!masoBool[1])
                                    npc.dontTakeDamage = false;
                                masoBool[2] = false;
                                npc.ai[2]++;
                                break;

                            case 6: //p2 dash
                                goto case 1;

                            case 7: //p2 spin & bubbles
                                Counter++;
                                if (Counter > 1)
                                {
                                    Counter = 0;
                                    if (Main.netMode != 1)
                                    {
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubbleEX"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(Math.PI / 2);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                        n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubbleEX"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(-Math.PI / 2);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
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
                                    Projectile.NewProjectile(spawnPos.X, spawnPos.Y, 0f, 2f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);

                                    SpawnRazorbladeRing(npc, 12, 12.5f, npc.damage / 6, 0.75f);
                                    SpawnRazorbladeRing(npc, 12, 10f, npc.damage / 6, -2f);
                                }
                                break;

                            case 9: //phase 3 transition
                                if (npc.ai[2] == 1f)
                                {
                                    for (int i = 0; i < npc.buffImmune.Length; i++)
                                        npc.buffImmune[i] = true;
                                    while (npc.buffTime[0] != 0)
                                        npc.DelBuff(0);
                                }
                                goto case 4;

                            case 10: //phase 3
                                //vanilla fishron has x1.1 damage in p3. p2 has x1.2 damage...
                                npc.damage = (int)(npc.defDamage * 1.2f * (Main.expertMode ? 0.6f * Main.damageMultiplier : 1f));
                                masoBool[2] = false;
                                Timer++;
                                if (Timer >= 60 + (int)(540.0 * npc.life / npc.lifeMax)) //yes that needs to be a double
                                {
                                    Timer = 0;
                                    if (Main.netMode != 1) //spawn cthulhunado
                                        Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer, 1f, npc.target + 1);
                                }
                                break;

                            case 11: //p3 dash
                                Counter++;
                                if (Counter > 1)
                                {
                                    Counter = 0;
                                    if (Main.netMode != 1)
                                    {
                                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubbleEX"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(Math.PI / 2);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                        n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubbleEX"));
                                        if (n < 200)
                                        {
                                            Main.npc[n].velocity = npc.velocity.RotatedBy(-Math.PI / 2);
                                            Main.npc[n].velocity.Normalize();
                                            Main.npc[n].netUpdate = true;
                                            if (Main.netMode == 2)
                                                NetMessage.SendData(23, -1, -1, null, n);
                                        }
                                    }
                                }
                                goto case 10;

                            case 12: //p3 *teleports behind you*
                                if (npc.ai[2] == 15f)
                                {
                                    if (Main.netMode != 1)
                                    {
                                        const int max = 24;
                                        float rotation = 2f * (float)Math.PI / max;
                                        for (int i = 0; i < max; i++)
                                        {
                                            int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("DetonatingBubbleEX"));
                                            if (n < 200)
                                            {
                                                Main.npc[n].velocity = npc.velocity.RotatedBy(rotation * i);
                                                Main.npc[n].velocity.Normalize();
                                                Main.npc[n].netUpdate = true;
                                                if (Main.netMode == 2)
                                                    NetMessage.SendData(23, -1, -1, null, n);
                                            }
                                        }
                                    }
                                }
                                else if (npc.ai[2] == 16f)
                                {
                                    if (Main.netMode != 1)
                                    {
                                        Vector2 spawnPos = Vector2.UnitX * npc.direction;
                                        spawnPos = spawnPos.RotatedBy(npc.rotation);
                                        spawnPos *= npc.width + 20f;
                                        spawnPos /= 2f;
                                        spawnPos += npc.Center;
                                        Projectile.NewProjectile(spawnPos.X, spawnPos.Y, npc.direction * 2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);
                                        Projectile.NewProjectile(spawnPos.X, spawnPos.Y, npc.direction * -2f, 8f, ProjectileID.SharknadoBolt, 0, 0f, Main.myPlayer);

                                        SpawnRazorbladeRing(npc, 3, 9f, npc.damage / 6, 1f);
                                        SpawnRazorbladeRing(npc, 3, 9f, npc.damage / 6, -0.5f);
                                    }
                                }
                                goto case 10;

                            default:
                                break;
                        }
                        break;

                    case 90: //reaper
                        Aura(npc, 40, mod.BuffType("MarkedforDeath"), false, 199);

                        Counter++;
                        if (Counter >= 420)
                        {
                            npc.TargetClosest();

                            Vector2 velocity = Vector2.Normalize(Main.player[npc.target].Center - npc.Center) * 10;
                            npc.velocity = velocity;
                            Counter = 0;
                            masoBool[0] = true;
                            masoState = 5;
                        }

                        if (masoBool[0])
                        {
                            Counter2++;

                            if (Counter2 >= 10)
                            {
                                int p = Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.DeathSickle, (int)(npc.damage / 2), 1f, Main.myPlayer);
                                Main.projectile[p].hostile = true;
                                Main.projectile[p].friendly = false;

                                Counter2 = 0;
                                masoState--;

                                if (masoState <= 0)
                                {
                                    masoBool[0] = false;
                                }
                            }
                        }
                        break;

                    case 91: //werewolf
                        Aura(npc, 200, mod.BuffType("Berserked"), false, 60);
                        break;

                    case 92: //blood zombie
                        Aura(npc, 80, mod.BuffType("Bloodthirsty"), false, 5);
                        break;

                    case 93: //possessed armor
                        Aura(npc, 400, BuffID.BrokenArmor, false, 37);
                        break;

                    case 94: //shadowflame apparation
                        Aura(npc, 60, BuffID.ShadowFlame, false, DustID.Shadowflame);
                        break;

                    case 95: //jellyfish
                        //when they be electrocuting
                        if (npc.wet && npc.ai[1] == 1f)
                        {
                            foreach (Player p in Main.player.Where(x => x.active && !x.dead))
                            {
                                if (npc.Distance(p.Center) < 200 && p.wet && Collision.CanHitLine(p.Center, 2, 2, npc.Center, 2, 2))
                                {
                                    p.AddBuff(BuffID.Electrified, 2);
                                }
                            }

                            for (int i = 0; i < 10; i++)
                            {
                                Vector2 offset = new Vector2();
                                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                                offset.X += (float)(Math.Sin(angle) * 200);
                                offset.Y += (float)(Math.Cos(angle) * 200);
                                Dust dust = Main.dust[Dust.NewDust(
                                    npc.Center + offset - new Vector2(4, 4), 0, 0,
                                    DustID.Electric, 0, 0, 100, Color.White, 1f
                                    )];
                                dust.velocity = npc.velocity;
                                dust.noGravity = true;
                            }
                        }
                        break;

                    case 96: //wraith
                        Aura(npc, 80, BuffID.Obstructed, false, 199);
                        break;

                    case 97: //saucer
                        Aura(npc, 250, BuffID.VortexDebuff, false, DustID.Vortex);
                        break;

                    case 98: //toxic sludge
                        Aura(npc, 200, BuffID.Poisoned, false, 188);
                        break;

                    case 99: //giant and ice tortoise
                        //while shell spinning
                        if (npc.ai[0] == 6f)
                        {
                            CustomReflect(npc, DustID.Sandstorm);
                        }
                        break;

                    case 100: //spike ball, blazing wheel
                        //in temple
                        if (Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16] != null && Main.tile[(int)npc.Center.X / 16, (int)npc.Center.Y / 16].wall == WallID.LihzahrdBrickUnsafe)
                        {
                            npc.damage = 150;
                            masoBool[0] = true;
                        }
                        break;

                    //
                    case 101: //????
                        

                        break;

                    case 102: //granite golem
                        //while shielding, reflect
                        if (npc.ai[2] < 0f)
                        {
                            CustomReflect(npc, DustID.Granite, 2);
                        }
                        break;

                    case 103: // biome mimics
                        if (masoBool[0])
                        {
                            if (npc.velocity.Y == 0f) //spawn smash
                            {
                                masoBool[0] = false;

                                Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.DD2OgreSmash, npc.damage / 2, 4, Main.myPlayer);
                            }
                        }
                        else if (npc.velocity.Y > 0 && npc.noTileCollide) //mega jump
                        {
                            masoBool[0] = true;
                        }
                        break;


                    //drakin possible meme tm
                    /*if (!DD2Event.Ongoing)
                    {
                        npc.ai[0] = 1;

                        if (npc.lavaWet)
                        {
                            npc.velocity.Y -= 2;
                            npc.velocity.X *= 2;
                        }
                    }*/

                    //psycho wtf why wont this work ech
                    /*case 88:
                        Counter++;
                        if (Counter >= 120)
                        {
                            npc.Opacity -= .01f;
                        }
                        break;*/

                    default:
                        break;
                }
            }

            if(!Transform && (npc.type == NPCID.Squirrel || npc.type == NPCID.SquirrelRed) && Main.rand.Next(8) == 0)
            {
                npc.Transform(mod.NPCType("TophatSquirrel"));
            }

            Transform = true;

        }

        private void Horde(NPC npc, int size)
        {
            for (int i = 0; i < size; i++)
            {
                Vector2 pos = new Vector2(npc.Center.X + Main.rand.NextFloat(-2f, 2f) * npc.width, npc.Center.Y);
                if (!Collision.SolidCollision(pos, npc.width, npc.height) && Main.netMode != 1)
                {
                    int j = NPC.NewNPC((int)pos.X + npc.width / 2, (int)pos.Y + npc.height / 2, npc.type);
                    if (j != 200)
                    {
                        NPC newNPC = Main.npc[j];
                        newNPC.velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 5f;
                        newNPC.GetGlobalNPC<FargoGlobalNPC>().Transform = true;
                        if (Main.netMode == 2)
                            NetMessage.SendData(23, -1, -1, null, j);
                    }
                }
            }
        }

        private void Aura(NPC npc, float distance, int buff, bool reverse = false, int dustid = DustID.GoldFlame)
        {
            //works because buffs are client side anyway :ech:
            Player p = Main.player[Main.myPlayer];
            if ((reverse && npc.Distance(p.Center) > distance) || (!reverse && npc.Distance(p.Center) < distance))
                p.AddBuff(buff, 2);

            for (int i = 0; i < 20; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * distance);
                offset.Y += (float)(Math.Cos(angle) * distance);
                Dust dust = Main.dust[Dust.NewDust(
                    npc.Center + offset - new Vector2(4, 4), 0, 0,
                    dustid, 0, 0, 100, Color.White, 1f
                    )];
                dust.velocity = npc.velocity;
                dust.noGravity = true;
            }
        }

        private void Shoot(NPC npc, int delay, float distance, int speed, int proj, int dmg, float kb, bool recolor = false, bool hostile = false)
        {
            int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
            if (t == -1)
                return;

            Player player = Main.player[t];
            //npc facing player target or if already started attack
            if (npc.direction == (Math.Sign(player.position.X - npc.position.X)) || Stop > 0)
            {
                //start the pause
                if (delay != 0 && Stop == 0)
                {
                    Stop = delay;
                }
                //half way through start attack
                else if (delay == 0 || Stop == delay / 2)
                {
                    Vector2 velocity = Vector2.Normalize(player.Center - npc.Center) * speed;
                    if (npc.Distance(player.Center) < distance)
                        velocity = Vector2.Normalize(player.Center - npc.Center) * speed;
                    else //player too far away now, just shoot straight ahead
                        velocity = new Vector2(npc.direction * speed, 0);

                    int p = Projectile.NewProjectile(npc.Center, velocity, proj, dmg, kb, Main.myPlayer);
                    if (p < 1000)
                    {
                        if (recolor)
                            Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                        if (hostile)
                        {
                            Main.projectile[p].friendly = false;
                            Main.projectile[p].hostile = true;
                        }
                    }
                    Counter = 0;
                }
            }
            
        }

        private void CustomReflect(NPC npc, int dustID, int ratio = 1)
        {
            float distance = 2f * 16;

            Main.projectile.Where(x => x.active && x.friendly && x.minionSlots <= 0).ToList().ForEach(x =>
            {
                if (Vector2.Distance(x.Center, npc.Center) <= distance)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        int dustId = Dust.NewDust(new Vector2(x.position.X, x.position.Y + 2f), x.width, x.height + 5, dustID, x.velocity.X * 0.2f, x.velocity.Y * 0.2f, 100, default(Color), 1.5f);
                        Main.dust[dustId].noGravity = true;
                    }

                    // Set ownership
                    x.hostile = true;
                    x.friendly = false;
                    x.owner = Main.myPlayer;
                    x.damage /= ratio;

                    // Turn around
                    x.velocity *= -1f;

                    // Flip sprite
                    if (x.Center.X > npc.Center.X * 0.5f)
                    {
                        x.direction = 1;
                        x.spriteDirection = 1;
                    }
                    else
                    {
                        x.direction = -1;
                        x.spriteDirection = -1;
                    }

                    //x.netUpdate = true;
                }
            });
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

            if (Infested)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, 44, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, Color.LimeGreen, InfestedDust);
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

            if (Electrified)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(new Vector2(npc.position.X - 2f, npc.position.Y - 2f), npc.width + 4, npc.height + 4, 229, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    if (Main.rand.Next(3) == 0)
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }

                Lighting.AddLight((int)npc.Center.X / 16, (int)npc.Center.Y / 16, 0.3f, 0.8f, 1.1f);
            }

            if (CurseoftheMoon)
            {
                int d = Dust.NewDust(npc.Center, 0, 0, 229, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 3f;
                Main.dust[d].scale += 0.5f;

                if (Main.rand.Next(4) < 3)
                {
                    d = Dust.NewDust(npc.position, npc.width, npc.height, 229, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Y -= 1f;
                    Main.dust[d].velocity *= 2f;
                }
            }

            if (Sadism)
            {
                int d = Dust.NewDust(npc.Center, 0, 0, 86, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 3f;
                Main.dust[d].scale += 1f;

                if (Main.rand.Next(4) < 3)
                {
                    d = Dust.NewDust(npc.position, npc.width, npc.height, 86, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f);
                    Main.dust[d].noGravity = true;
                    Main.dust[d].velocity.Y -= 1f;
                    Main.dust[d].velocity *= 2f;
                    Main.dust[d].scale += 0.5f;
                }
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
                            if (!BossIsAlive(ref slimeBoss, NPCID.KingSlime))
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

                        case NPCID.Probe:
                            if (!BossIsAlive(ref destroyBoss, NPCID.TheDestroyer) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref retiBoss, NPCID.Retinazer))
                            {
                                int[] mechs = new int[] { NPCID.TheDestroyer, NPCID.SkeletronPrime, NPCID.Spazmatism, NPCID.Retinazer};
                                int spawn = mechs[Main.rand.Next(mechs.Length)];

                                npc.Transform(spawn);
                                npc.velocity.Y = -5f;
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);

                                if (spawn == NPCID.Spazmatism)
                                {
                                    NPC.SpawnOnPlayer(target.whoAmI, NPCID.Retinazer);
                                }
                                else if (spawn == NPCID.Retinazer)
                                {
                                    NPC.SpawnOnPlayer(target.whoAmI, NPCID.Spazmatism);
                                }
                            }
                            break;

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
                                    target.AddBuff(BuffID.Slimed, Main.rand.Next(300, 600));
                                    target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(120));
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
                            target.AddBuff(mod.BuffType("Antisocial"), Main.rand.Next(180, 1800));
                            break;

                        case NPCID.LavaSlime:
                            target.AddBuff(BuffID.Oiled, Main.rand.Next(900, 1800));
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(120, 600));
                            break;

                        case NPCID.DungeonSlime:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                            target.AddBuff(BuffID.Blackout, Main.rand.Next(120, 1200));
                            break;

                        case NPCID.KingSlime:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300, 600));

                            if (Main.rand.Next(5) == 0 && !target.HasBuff(mod.BuffType("Stunned")))
                                target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(30, 120));
                            break;

                        case NPCID.ToxicSludge:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300, 600));
                            break;

                        case NPCID.CorruptSlime:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 1200));
                            break;

                        case NPCID.Crimslime:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(30, 240));
                            break;

                        case NPCID.Gastropod:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("Fused"), 1800);
                            break;

                        case NPCID.IlluminantSlime:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(30, 300));
                            break;

                        case NPCID.RainbowSlime:
                            target.AddBuff(BuffID.Slimed, Main.rand.Next(30, 300));
                            target.AddBuff(mod.BuffType("FlamesoftheUniverse"), Main.rand.Next(30, 120));
                            break;

                        case NPCID.DemonEye:
                        case NPCID.DemonEyeOwl:
                        case NPCID.DemonEyeSpaceship:
                            if ((Math.Abs(npc.velocity.Y) > 5 || Math.Abs(npc.velocity.X) > 5) && !target.HasBuff(BuffID.Stoned))
                                target.AddBuff(BuffID.Stoned, Main.rand.Next(30, 120));
                            break;

                        case NPCID.EaterofSouls:
                        case NPCID.Crimera:
                            target.AddBuff(BuffID.Weak, Main.rand.Next(300, 1800));
                            break;

                        case NPCID.EyeofCthulhu:
                            target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(60, 600));
                            break;

                        case NPCID.ServantofCthulhu:
                            target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(300));
                            break;

                        case NPCID.QueenBee:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(300, 600));
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(300, 600));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300, 600));
                            break;

                        case NPCID.WallofFlesh:
                        case NPCID.WallofFleshEye:
                            if (!target.HasBuff(mod.BuffType("Unstable")))
                                target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(60, 240));
                            break;

                        case NPCID.TheHungry:
                        case NPCID.TheHungryII:
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(300, 600));
                            target.velocity = target.velocity / 4;
                            if (!BossIsAlive(ref wallBoss, NPCID.WallofFlesh))
                            {
                                NPC.SpawnWOF(target.Center);
                                Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            }
                            break;

                        case NPCID.EaterofWorldsHead:
                        case NPCID.EaterofWorldsBody:
                        case NPCID.EaterofWorldsTail:
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 1800));
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
                            target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(150, 300));
                            break;

                        case NPCID.SkeletronHand:
                            if (Main.rand.Next(2) == 0)
                                target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(90));
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
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(1800, 3600));
                            break;

                        case NPCID.GiantFlyingFox:
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(600, 1200));
                            target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(30, 300));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                            break;

                        case NPCID.VampireBat:
                        case NPCID.Vampire:
                            target.AddBuff(BuffID.Darkness, Main.rand.Next(900, 1800));
                            target.AddBuff(BuffID.Weak, Main.rand.Next(900, 1800));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(900, 1800));
                            npc.life += damage / 2;
                            CombatText.NewText(npc.Hitbox, CombatText.HealLife, damage / 2);
                            npc.damage = (int)(npc.damage * 1.1f);
                            break;

                        case NPCID.SnowFlinx:
                            target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(180, 1800));
                            break;

                        case NPCID.Piranha:
                            target.AddBuff(BuffID.Bleeding, Main.rand.Next(60, 600));
                            break;

                        case NPCID.Medusa:
                            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(90, 180));
                            if (!target.HasBuff(BuffID.Stoned))
                                target.AddBuff(BuffID.Stoned, Main.rand.Next(60, 120));
                            break;

                        case NPCID.SpikeBall:
                            target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(300, 1200));
                            if (masoBool[0])
                                target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(300, 1200));
                            break;

                        case NPCID.BlazingWheel:
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 600));
                            if (masoBool[0])
                                target.AddBuff(mod.BuffType("FlamesoftheUniverse"), Main.rand.Next(300));
                            break;

                        case NPCID.Shark:
                        case NPCID.SandShark:
                        case NPCID.SandsharkCorrupt:
                        case NPCID.SandsharkCrimson:
                        case NPCID.SandsharkHallow:
                            target.AddBuff(BuffID.Bleeding, Main.rand.Next(60, 300));
                            break;

                        case NPCID.GraniteFlyer:
                        case NPCID.GraniteGolem:
                            if (!target.HasBuff(BuffID.Stoned) && Main.rand.Next(2) == 0)
                                target.AddBuff(BuffID.Stoned, Main.rand.Next(120));
                            break;

                        case NPCID.LeechHead:
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(300, 900));
                            target.AddBuff(BuffID.Bleeding, Main.rand.Next(300, 900));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(300, 900));
                            break;

                        case NPCID.AnomuraFungus:
                            target.AddBuff(BuffID.Poisoned, Main.rand.Next(300, 600));
                            break;

                        case NPCID.WaterSphere:
                            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(600));
                            target.AddBuff(BuffID.Wet, Main.rand.Next(1200));
                            break;

                        case NPCID.GiantShelly:
                        case NPCID.GiantShelly2:
                            target.AddBuff(BuffID.Slow, Main.rand.Next(30, 300));
                            break;

                        case NPCID.Squid:
                            target.AddBuff(BuffID.Obstructed, Main.rand.Next(30, 300));
                            break;

                        case NPCID.BloodZombie:
                            target.AddBuff(BuffID.Cursed, Main.rand.Next(240, 480));
                            break;

                        case NPCID.Drippler:
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 600));
                            break;

                        case NPCID.ChaosBall:
                            target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(60, 600));
                            break;

                        case NPCID.Tumbleweed:
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(60, 300));
                            break;

                        case NPCID.PigronCorruption:
                        case NPCID.PigronCrimson:
                        case NPCID.PigronHallow:
                            target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(300));
                            if (!BossIsAlive(ref fishBoss, NPCID.DukeFishron))
                            {
                                npc.velocity = npc.Center - target.Center;
                                npc.velocity.Normalize();
                                npc.velocity *= 2.5f;
                                npc.Transform(NPCID.DukeFishron);
                            }
                            break;

                        case NPCID.CorruptBunny:
                        case NPCID.CrimsonBunny:
                        case NPCID.CorruptGoldfish:
                        case NPCID.CrimsonGoldfish:
                        case NPCID.CorruptPenguin:
                        case NPCID.CrimsonPenguin:
                        case NPCID.MothronSpawn:
                        case NPCID.Scutlix:
                        case NPCID.Parrot:
                        case NPCID.GingerbreadMan:
                            target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(300));
                            break;

                        case NPCID.FaceMonster:
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(1800));
                            break;

                        case NPCID.Harpy:
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(60, 600));
                            break;

                        case NPCID.SeaSnail:
                            target.AddBuff(BuffID.OgreSpit, Main.rand.Next(300));
                            break;

                        case NPCID.BrainofCthulhu:
                            target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(90));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300));
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300));
                            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(180));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(900, 1800));
                            break;

                        case NPCID.Creeper:
                            switch (Main.rand.Next(6))
                            {
                                case 0:
                                    target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(240));
                                    break;

                                case 1:
                                    target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(240));
                                    break;

                                case 2:
                                    target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(240));
                                    break;

                                case 3:
                                    target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(240));
                                    break;

                                case 4:
                                    target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(240));
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
                            target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(60, 600));
                            break;

                        case NPCID.Butcher:
                            target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Bleeding, Main.rand.Next(900, 1800));
                            break;

                        case NPCID.ThePossessed:
                            target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(240));
                            break;

                        case NPCID.Wolf:
                            target.AddBuff(mod.BuffType("Crippled"), 300);
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
                        case 280: target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(90)); break;

                        case NPCID.GiantTortoise:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(60, 300));
                            break;

                        case NPCID.IceTortoise:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(60, 300));
                            if (Main.rand.Next(3) == 0)
                                target.AddBuff(BuffID.Frozen, Main.rand.Next(120));
                            break;

                        //CULTIST OP
                        case NPCID.AncientDoom:
                            target.AddBuff(mod.BuffType("MarkedforDeath"), 120);
                            break;
                        case NPCID.AncientLight:
                            target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(60, 180));
                            break;
                        case NPCID.CultistBossClone:
                            target.AddBuff(mod.BuffType("Hexed"), Main.rand.Next(60, 120));
                            target.AddBuff(mod.BuffType("CurseoftheMoon"), Main.rand.Next(600, 900));
                            break;

                        case NPCID.MossHornet:
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(30, 300));
                            break;

                        case NPCID.Paladin:
                            target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(480, 720));
                            break;

                        case NPCID.DukeFishron:
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(600, 900));
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(600, 900));
                            target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(600, 900));
                            target.AddBuff(BuffID.WitheredArmor, Main.rand.Next(600, 900));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(3600, 7200));
                            target.GetModPlayer<FargoPlayer>(mod).MaxLifeReduction += (npc.whoAmI == fishBossEX) ? 150 : 100;
                            target.AddBuff(mod.BuffType("OceanicMaul"), Main.rand.Next(3600, 7200));
                            break;

                        case NPCID.Sharkron:
                        case NPCID.Sharkron2:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(600, 900));
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(3600, 7200));
                            if (BossIsAlive(ref fishBossEX, NPCID.DukeFishron))
                            {
                                target.GetModPlayer<FargoPlayer>(mod).MaxLifeReduction += 100;
                                target.AddBuff(mod.BuffType("OceanicMaul"), Main.rand.Next(1800, 3600));
                            }
                            break;

                        case NPCID.DetonatingBubble:
                            target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(60, 180));
                            if (BossIsAlive(ref fishBossEX, NPCID.DukeFishron))
                            {
                                target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(600, 900));
                                target.GetModPlayer<FargoPlayer>(mod).MaxLifeReduction += 50;
                                target.AddBuff(mod.BuffType("OceanicMaul"), Main.rand.Next(1800, 3600));
                            }
                            break;

                        case NPCID.Hellhound:
                            target.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(900));
                            target.AddBuff(BuffID.Obstructed, Main.rand.Next(180));
                            break;

                        case NPCID.Mimic:
                        case NPCID.PresentMimic:
                            target.AddBuff(mod.BuffType("Purified"), Main.rand.Next(120));
                            break;

                        case NPCID.BigMimicCorruption:
                        case NPCID.BigMimicCrimson:
                        case NPCID.BigMimicHallow:
                        case NPCID.BigMimicJungle:
                            target.AddBuff(mod.BuffType("Berserked"), 300);
                            goto case NPCID.Mimic;

                        case NPCID.RuneWizard:
                            target.AddBuff(mod.BuffType("MarkedforDeath"), 120);
                            target.AddBuff(mod.BuffType("Unstable"), 30);
                            break;

                        case NPCID.Nutcracker:
                        case NPCID.NutcrackerSpinning:
                            if (target.Male)
                            {
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
                            target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(300, 900));
                            break;

                        case NPCID.Plantera:
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(600, 900));
                            goto case NPCID.PlanterasHook;

                        case NPCID.PlanterasHook:
                        case NPCID.PlanterasTentacle:
                        case NPCID.Spore:
                            target.AddBuff(BuffID.Poisoned, Main.rand.Next(120, 600));
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 300));
                            bool isVenomed = false;
                            for (int i = 0; i < 22; i++)
                            {
                                if (target.buffType[i] == BuffID.Venom && target.buffTime[i] > 1)
                                {
                                    isVenomed = true;
                                    target.buffTime[i] += Main.rand.Next(60, 180);
                                    if (target.buffTime[i] > 1200)
                                    {
                                        target.AddBuff(mod.BuffType("Infested"), target.buffTime[i]);
                                        Main.PlaySound(15, (int)target.Center.X, (int)target.Center.Y, 0);
                                    }
                                    break;
                                }
                            }
                            if (!isVenomed)
                            {
                                target.AddBuff(BuffID.Venom, Main.rand.Next(60, 300));
                            }
                            break;

                        case NPCID.ChaosElemental:
                            target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(600));
                            break;

                        case NPCID.Flocko:
                            target.AddBuff(BuffID.Chilled, Main.rand.Next(900, 1800));
                            target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 600));
                            break;

                        case NPCID.GoblinThief:
                            if (target.whoAmI == Main.myPlayer && Main.rand.Next(2) == 0)
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
                                target.AddBuff(mod.BuffType("LivingWasteland"), Main.rand.Next(300, 900));
                            break;

                        case NPCID.Zombie:
                        case NPCID.ArmedZombie:
                        case NPCID.ArmedZombieCenx:
                        case NPCID.ArmedZombiePincussion:
                        case NPCID.ArmedZombieSlimed:
                        case NPCID.ArmedZombieSwamp:
                        case NPCID.ArmedZombieTwiggy:
                        case NPCID.BaldZombie:
                        case NPCID.FemaleZombie:
                        case NPCID.PincushionZombie:
                        case NPCID.SlimedZombie:
                        case NPCID.TwiggyZombie:
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
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(60, 600));
                            break;

                        case NPCID.ZombieEskimo:
                        case NPCID.ArmedZombieEskimo:
                            target.AddBuff(BuffID.Chilled, Main.rand.Next(300));
                            break;

                        case NPCID.Corruptor:
                            target.AddBuff(BuffID.Weak, Main.rand.Next(60, 7200));
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 3600));
                            break;

                        case NPCID.Mummy:
                        case NPCID.LightMummy:
                        case NPCID.DarkMummy:
                            if (!target.HasBuff(BuffID.Webbed))
                                target.AddBuff(BuffID.Webbed, Main.rand.Next(30, 300));
                            break;

                        case NPCID.Derpling:
                            target.AddBuff(mod.BuffType("Lethargic"), Main.rand.Next(600, 1200));
                            break;

                        case NPCID.Spazmatism:
                            if (masoBool[1] && Main.rand.Next(3) == 0)
                                target.AddBuff(BuffID.CursedInferno, 480);
                            break;
                        /*case NPCID.Retinazer:
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(120, 240));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(120, 240));
                            break;*/

                        case NPCID.TheDestroyer:
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(300, 1200));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300, 1200));
                            if (target.statLife < 300)
                                target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by the Destroyer."), 9999, 0);
                            goto case NPCID.TheDestroyerTail;

                        case NPCID.TheDestroyerBody:
                        case NPCID.TheDestroyerTail:
                            target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(300, 1200));
                            break;

                        case NPCID.SkeletronPrime:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(180, 300));
                            break;

                        case NPCID.PrimeVice:
                            if (target.mount.Active)
                                target.mount.Dismount(target);
                            target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(30, 90));
                            break;

                        /*case NPCID.PrimeCannon:
                        case NPCID.PrimeLaser:
                        case NPCID.PrimeSaw:
                        case NPCID.Probe:
                            target.AddBuff(mod.BuffType("ClippedWings"), 15); //all mech cases come here
                            break;*/

                        case NPCID.BlackRecluse:
                            target.AddBuff(BuffID.Poisoned, Main.rand.Next(30, 300));
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(60, 1800));
                            break;

                        case NPCID.DesertBeast:
                            target.AddBuff(mod.BuffType("Crippled"), Main.rand.Next(300, 900));
                            break;

                        case NPCID.FlyingSnake:
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300, 600));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(600, 1200));
                            break;

                        case NPCID.Lihzahrd:
                        case NPCID.LihzahrdCrawler:
                            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 300));
                            target.AddBuff(mod.BuffType("Bloodthirsty"), Main.rand.Next(120));
                            break;

                        case NPCID.CultistDragonHead:
                            target.AddBuff(mod.BuffType("CurseoftheMoon"), Main.rand.Next(300, 600));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(60, 300));
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(300, 600));
                            break;
                        //goto case NPCID.CultistDragonTail;

                        /*case NPCID.CultistDragonBody1:
                        case NPCID.CultistDragonBody2:
                        case NPCID.CultistDragonBody3:
                        case NPCID.CultistDragonBody4:
                        case NPCID.CultistDragonTail:
                            target.AddBuff(mod.BuffType("ClippedWings"), 15);
                            break;*/

                        case NPCID.AncientCultistSquidhead:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(120, 480));
                            if (Main.rand.Next(2) == 0)
                                target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(30, 60));
                            else
                                target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(30, 60));
                            break;

                        case NPCID.SolarCrawltipedeHead:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(600, 1200));
                            if (target.statLife < 200)
                                target.KillMe(PlayerDeathReason.ByCustomReason(target.name + " was eaten alive by a Crawltipede."), 999, 0);
                            break;

                        case NPCID.BoneLee:
                            target.AddBuff(BuffID.Obstructed, Main.rand.Next(60));
                            break;

                        case NPCID.Pumpking:
                        case NPCID.PumpkingBlade:
                            target.AddBuff(BuffID.Weak, Main.rand.Next(900, 1800));
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(900, 1800));
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
                            target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(60, 600));
                            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(30, 300));
                            break;

                        case NPCID.ZombieElf:
                        case NPCID.ZombieElfBeard:
                        case NPCID.ZombieElfGirl:
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(300, 1800));
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
                            target.AddBuff(mod.BuffType("LightningRod"), Main.rand.Next(300, 600));
                            break;

                        case NPCID.Clown:
                            target.AddBuff(mod.BuffType("Fused"), 180);
                            target.AddBuff(mod.BuffType("Hexed"), 1200);
                            break;

                        case NPCID.UndeadMiner:
                            int length = Main.rand.Next(3600, 7200);
                            target.AddBuff(mod.BuffType("Lethargic"), length * 2);
                            target.AddBuff(BuffID.Blackout, length);
                            target.AddBuff(BuffID.NoBuilding, length);
                            for (int i = 0; i < 59; i++)
                            {
                                if (target.inventory[i].pick != 0 || target.inventory[i].hammer != 0 || target.inventory[i].axe != 0)
                                    StealFromInventory(target, ref target.inventory[i]);
                            }
                            break;

                        case NPCID.Golem:
                        case NPCID.GolemFistLeft:
                        case NPCID.GolemFistRight:
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(60, 300));
                            break;

                        case NPCID.DD2Betsy:
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(3600, 7200));
                            target.AddBuff(BuffID.WitheredArmor, Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Ichor, Main.rand.Next(600, 900));
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(900, 1800));
                            break;

                        case NPCID.DD2WyvernT1:
                        case NPCID.DD2WyvernT2:
                        case NPCID.DD2WyvernT3:
                            target.AddBuff(mod.BuffType("MutantNibble"), Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(3600, 7200));
                            break;

                        case NPCID.DD2KoboldFlyerT2:
                        case NPCID.DD2KoboldFlyerT3:
                        case NPCID.DD2KoboldWalkerT2:
                        case NPCID.DD2KoboldWalkerT3:
                            target.AddBuff(mod.BuffType("Fused"), 1800);
                            break;

                        case NPCID.DD2OgreT2:
                        case NPCID.DD2OgreT3:
                            target.AddBuff(mod.BuffType("Stunned"), Main.rand.Next(30, 60));
                            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(180, 300));
                            target.AddBuff(BuffID.BrokenArmor, Main.rand.Next(300, 600));
                            break;

                        case NPCID.DD2LightningBugT3:
                            target.AddBuff(BuffID.Electrified, Main.rand.Next(300, 600));
                            break;

                        case NPCID.DD2SkeletonT1:
                        case NPCID.DD2SkeletonT3:
                            target.AddBuff(BuffID.ShadowFlame, Main.rand.Next(300, 600));
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(1200, 2400));
                            break;

                        case NPCID.SolarSpearman:
                        case NPCID.SolarSroller:
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Burning, Main.rand.Next(60, 300));
                            break;

                        case NPCID.SolarSolenian:
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Ichor, Main.rand.Next(600, 1200));
                            break;

                        case NPCID.SolarDrakomire:
                        case NPCID.SolarDrakomireRider:
                            target.AddBuff(BuffID.OnFire, Main.rand.Next(300, 600));
                            target.AddBuff(BuffID.Rabies, Main.rand.Next(7200));
                            break;

                        case NPCID.DesertScorpionWalk:
                        case NPCID.DesertScorpionWall:
                        case NPCID.MisterStabby:
                            target.AddBuff(mod.BuffType("MarkedforDeath"), Main.rand.Next(60, 180));
                            break;

                        case NPCID.NebulaHeadcrab:
                            target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(60, 180));
                            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(60, 180));
                            break;

                        case NPCID.StardustCellBig:
                        case NPCID.StardustCellSmall:
                            target.AddBuff(mod.BuffType("SqueakyToy"), Main.rand.Next(60, 180));
                            break;

                        case NPCID.StardustWormHead:
                        case NPCID.StardustWormBody:
                        case NPCID.StardustWormTail:
                            target.AddBuff(mod.BuffType("Berserked"), Main.rand.Next(60, 300));
                            break;

                        case NPCID.StardustSpiderBig:
                        case NPCID.StardustSpiderSmall:
                            target.AddBuff(BuffID.Frostburn, Main.rand.Next(300, 600));
                            break;

                        case NPCID.MoonLordFreeEye:
                        case NPCID.MoonLordHand:
                        case NPCID.MoonLordHead:
                            target.AddBuff(mod.BuffType("CurseoftheMoon"), 600);
                            break;

                        case NPCID.BoneSerpentHead:
                            target.AddBuff(BuffID.Burning, Main.rand.Next(300, 600));
                            break;

                        case NPCID.Salamander:
                        case NPCID.Salamander2:
                        case NPCID.Salamander3:
                        case NPCID.Salamander4:
                        case NPCID.Salamander5:
                        case NPCID.Salamander6:
                        case NPCID.Salamander7:
                        case NPCID.Salamander8:
                        case NPCID.Salamander9:
                            target.AddBuff(BuffID.Poisoned, Main.rand.Next(600, 900));
                            break;

                        case NPCID.VileSpit:
                            target.AddBuff(mod.BuffType("Rotting"), Main.rand.Next(240, 360));
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

        private void SpawnRazorbladeRing(NPC npc, int max, float speed, int damage, float rotationModifier)
        {
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = Main.player[npc.target].Center - npc.Center;
            vel.Normalize();
            vel *= speed;
            int type = mod.ProjectileType("RazorbladeTyphoon");
            for (int i = 0; i < max; i++)
            {
                vel = vel.RotatedBy(rotation);
                Projectile.NewProjectile(npc.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * npc.spriteDirection, speed);
            }
        }

        public void ResetRegenTimer(NPC npc)
        {
            //8 sec
            npc.GetGlobalNPC<FargoGlobalNPC>().RegenTimer = 480;
        }
		
		public override void UpdateLifeRegen(NPC npc, ref int damage)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			int dmg;

            if (FargoWorld.MasochistMode)
            {
                if(!npc.dontTakeDamage && !npc.friendly && RegenTimer <= 0)
                {
                    npc.lifeRegen += 1 + npc.lifeMax / 25;
                }
            }

            //20 dps
            if (SBleed)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= 40;
				if (damage < 20)
				{
					damage = 20;
				}
				
				if(Main.rand.Next(4) == 0)
				{
                    dmg = 20;
					int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y - 40, 0f + Main.rand.Next(-5, 5), -5f, mod.ProjectileType("SuperBlood"), dmg, 0f, Main.myPlayer);
                    if (p < 1000)
                        Main.projectile[p].GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
			}

            if (Rotting)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= 100;

                if (damage < 5)
                    damage = 5;
            }

            if(LeadPoison)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 10;

                for (int i = 0; i < 200; i++)
                {
                    NPC spread = Main.npc[i];

                    if (spread.active && !spread.townNPC && !spread.friendly && spread.lifeMax > 1 && !spread.HasBuff(mod.BuffType("LeadPoison")) && Vector2.Distance(npc.Center, spread.Center) < 50)
                    {
                        spread.AddBuff(mod.BuffType("LeadPoison"), 120);
                    }
                }
            }

            //50 dps
            if(SolarFlare)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }

                npc.lifeRegen -= 100;

                if (damage < 10)
                {
                    damage = 10;
                }
            }

            //.5% dps
            if(HellFire)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= npc.lifeMax / 100;

                if (damage < npc.lifeMax / 1000)
                    damage = npc.lifeMax / 1000;
            }

            if(Infested)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= InfestedExtraDot(npc);

                if (damage < 8)
                    damage = 8;
            }
            else
            {
                MaxInfestTime = 0;
            }

            if (Electrified)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= 4;
                if (npc.velocity != Vector2.Zero)
                    npc.lifeRegen -= 16;

                if (damage < 4)
                    damage = 4;
            }

            if (CurseoftheMoon)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= 24;

                if (damage < 6)
                    damage = 6;
            }

            if (OceanicMaul)
            {
                if (RegenTimer < 2)
                    RegenTimer = 2;

                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= 48;

                if (damage < 12)
                    damage = 12;
            }

            if (Sadism)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;

                npc.lifeRegen -= 170;

                if (damage < 70)
                    damage = 70;
            }

            if (MutantNibble)
            {
                if (npc.lifeRegen > 0)
                    npc.lifeRegen = 0;
                if (npc.lifeRegenCount > 0)
                    npc.lifeRegenCount = 0;

                if (npc.life > LifePrevious)
                    npc.life = LifePrevious;
                else
                    LifePrevious = npc.life;
            }
            else
            {
                LifePrevious = npc.life;
            }
		}

        private int InfestedExtraDot(NPC npc)
        {
            int buffIndex = npc.FindBuffIndex(mod.BuffType("Infested"));
            if (buffIndex == -1)
                return 0;

            int timeLeft = npc.buffTime[buffIndex];
            if (MaxInfestTime <= 0)
                MaxInfestTime = timeLeft;
            float baseVal = (MaxInfestTime - timeLeft) / 30f; //change the denominator to adjust max power of DOT
            int dmg = (int)(baseVal * baseVal + 8);

            InfestedDust = baseVal / 15 + .5f;
            if (InfestedDust > 5f)
                InfestedDust = 5f;
            
            return dmg;
        }

        public override void EditSpawnRate (Player player, ref int spawnRate, ref int maxSpawns)
		{
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			
            if(FargoWorld.MasochistMode)
            {
                if(!Main.hardMode)
                {
                    //1.3x spawn rate
                    spawnRate = (int)(spawnRate * 0.8);
                    //2x max spawn
                    maxSpawns = (int)(maxSpawns * 1.5f);
                }
                else
                {
                    //2x spawn rate
                    spawnRate = (int)(spawnRate * 0.6);
                    //3x max spawn
                    maxSpawns = (int)(maxSpawns * 2f);
                }
            }

            if (FargoWorld.Bloodthirsty)
            {
                //20x spawn rate
                spawnRate = (int)(spawnRate * 0.05);
                //20x max spawn
                maxSpawns = (int)(maxSpawns * 20f);
            }

            if (FargoWorld.BuilderMode)
			{
				maxSpawns = 0;
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
                        if(night)
                        {
                            if (noBiome)
                            {
                                pool[NPCID.CorruptBunny] = .05f;
                                pool[NPCID.CrimsonBunny] = .05f;
                            }

                            if (snow)
                            {
                                pool[NPCID.CorruptPenguin] = .1f;
                                pool[NPCID.CrimsonPenguin] = .1f;
                            }

                            if (ocean || Main.raining)
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
                                        pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .0025f : .005f;
                                    }
                                    else
                                    {
                                        pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .001f : .002f;
                                    }
                                }
                            }
                        }
			
			            if (Main.slimeRain && NPC.downedBoss2)
                        {
                             pool[NPCID.KingSlime] = BossIsAlive(ref slimeBoss, NPCID.KingSlime) ? .01f : 0.02f;
                        }

                        if (goblinArmy)
                        {
                            pool[NPCID.DD2GoblinT1] = .1f;
                            pool[NPCID.DD2GoblinBomberT1] = .1f;
                            pool[NPCID.DD2JavelinstT1] = .1f;
                        }
                    }
                    else if (wideUnderground)
                    {
                        if (nearLava)
                        {
                            pool[NPCID.FireImp] = .05f;
                            pool[NPCID.LavaSlime] = .05f;
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
                        pool[NPCID.DD2WyvernT1] = .05f;
                        pool[NPCID.BlazingWheel] = .1f;
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

                            if (normalSpawn && NPC.downedBoss3 && !underworld)
                            {
                                pool[NPCID.EaterofWorldsHead] = BossIsAlive(ref eaterBoss, NPCID.EaterofWorldsHead) ? .00125f : .0025f;
                            }
                        }
                    }

                    if (crimson)
                    {
                        if (NPC.downedBoss2)
                        {
                            pool[NPCID.IchorSticker] = .01f;

                            if (normalSpawn && NPC.downedBoss3 && !underworld)
                            {
                                pool[NPCID.BrainofCthulhu] = BossIsAlive(ref brainBoss, NPCID.BrainofCthulhu) ? .00125f : .0025f;
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

                    if(Fargowiltas.NormalSpawn(spawnInfo) && !surface)
                    {
                        pool[NPCID.Mimic] = .01f;
                    }
                }
                else //all the hardmode
                {
                    //mutually exclusive world layers
                    if (surface)
                    {
                        if (NPC.LunarApocalypseIsUp && (nebulaTower || vortexTower || stardustTower || solarTower))
                            return;

                        if (day)
                        {
                            if (normalSpawn)
                            {
                                if (noBiome)
                                {
                                    pool[NPCID.KingSlime] = BossIsAlive(ref slimeBoss, NPCID.KingSlime) ? .01f : .02f;
                                }
				
				if (NPC.downedMechBossAny && (noBiome || dungeon))
                                    {
                                        pool[NPCID.CultistArcherWhite] = .05f;
                                    }

                                if (Main.slimeRain)
                                {
                                    pool[NPCID.KingSlime] = BossIsAlive(ref slimeBoss, NPCID.KingSlime) ? .025f : .05f;
                                }
                            }
                        }
                        else //night
                        {
                            if (Main.bloodMoon)
                            {
                                if (NPC.downedGolemBoss)
                                {
                                    pool[NPCID.DD2DarkMageT3] = .05f;
                                }
                                else
                                {
                                    pool[NPCID.DD2DarkMageT1] = .05f;
                                }

                                pool[NPCID.ChatteringTeethBomb] = .1f;
                                pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .0125f : .025f;

                                if (NPC.downedPlantBoss)
                                {
                                    if (!BossIsAlive(ref retiBoss, NPCID.Retinazer) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref destroyBoss, NPCID.TheDestroyer))
                                    {
                                        pool[NPCID.Retinazer] = .025f;
                                        pool[NPCID.Spazmatism] = .025f;
                                        pool[NPCID.TheDestroyer] = .025f;
                                        pool[NPCID.SkeletronPrime] = .025f;
                                    }
                                    else
                                    {
                                        pool[NPCID.Retinazer] = .0125f;
                                        pool[NPCID.Spazmatism] = .0125f;
                                        pool[NPCID.TheDestroyer] = .0125f;
                                        pool[NPCID.SkeletronPrime] = .0125f;
                                    }
                                }
                                else if (normalSpawn && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                                {
                                    if (!BossIsAlive(ref retiBoss, NPCID.Retinazer) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref destroyBoss, NPCID.TheDestroyer))
                                    {
                                        pool[NPCID.Retinazer] = .0025f;
                                        pool[NPCID.Spazmatism] = .0025f;
                                        pool[NPCID.TheDestroyer] = .0025f;
                                        pool[NPCID.SkeletronPrime] = .0025f;
                                    }
                                    else
                                    {
                                        pool[NPCID.Retinazer] = .00125f;
                                        pool[NPCID.Spazmatism] = .00125f;
                                        pool[NPCID.TheDestroyer] = .00125f;
                                        pool[NPCID.SkeletronPrime] = .00125f;
                                    }
                                }
                            }
                            else //not blood moon
                            {
                                pool[NPCID.Clown] = 0.01f;
				
				            if (noBiome)
                            {
                                pool[NPCID.CorruptBunny] = .05f;
                                pool[NPCID.CrimsonBunny] = .05f;
                            }

                            if (snow)
                            {
                                pool[NPCID.CorruptPenguin] = .05f;
                                pool[NPCID.CrimsonPenguin] = .05f;
                            }

                            if (ocean || Main.raining)
                            {
                                pool[NPCID.CorruptGoldfish] = .05f;
                                pool[NPCID.CrimsonGoldfish] = .05f;
                            }

                                if (normalSpawn)
                                {
                                    pool[NPCID.EyeofCthulhu] = BossIsAlive(ref eyeBoss, NPCID.EyeofCthulhu) ? .005f : .02f;

                                    if (NPC.downedMechBossAny)
                                    {
                                        pool[NPCID.Probe] = 0.2f;
                                    }

                                    if (NPC.downedPlantBoss) //GODLUL
                                    {
                                        if (!BossIsAlive(ref retiBoss, NPCID.Retinazer) && !BossIsAlive(ref spazBoss, NPCID.Spazmatism) && !BossIsAlive(ref primeBoss, NPCID.SkeletronPrime) && !BossIsAlive(ref destroyBoss, NPCID.TheDestroyer))
                                        {
                                            pool[NPCID.Retinazer] = .0025f;
                                            pool[NPCID.Spazmatism] = .0025f;
                                            pool[NPCID.TheDestroyer] = .0025f;
                                            pool[NPCID.SkeletronPrime] = .0025f;
                                        }
                                        else
                                        {
                                            pool[NPCID.Retinazer] = .00125f;
                                            pool[NPCID.Spazmatism] = .00125f;
                                            pool[NPCID.TheDestroyer] = .00125f;
                                            pool[NPCID.SkeletronPrime] = .00125f;
                                        }

                                        if (!Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().SkullCharm)
                                        {
                                            pool[NPCID.SkeletonSniper] = .03f;
                                            pool[NPCID.SkeletonCommando] = .03f;
                                            pool[NPCID.TacticalSkeleton] = .03f;
                                        }
                                    }
                                }
                            }

                            if (NPC.downedMechBossAny && noInvasion)
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
                            pool[NPCID.RainbowSlime] = .001f;
			                pool[NPCID.GingerbreadMan] = .05f;
                        }

                        if (snow && noInvasion)
                        {
                            pool[NPCID.IceGolem] = .01f;
                            pool[NPCID.SnowBalla] = .04f;
                            pool[NPCID.MisterStabby] = .04f;
                            pool[NPCID.SnowmanGangsta] = .04f;
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
                            pool[NPCID.MartianDrone] = .05f;
                        }

                        if (goblinArmy)
                        {
                            if (NPC.downedMechBossAny)
                            {
                                pool[NPCID.DD2GoblinT3] = .1f;
                                pool[NPCID.DD2GoblinBomberT3] = .1f;
                                pool[NPCID.DD2JavelinstT3] = .1f;
                            }
                            else
                            {
                                pool[NPCID.DD2GoblinT2] = .1f;
                                pool[NPCID.DD2GoblinBomberT2] = .1f;
                                pool[NPCID.DD2JavelinstT2] = .1f;
                            }
                        }

                        if (NPC.downedMechBossAny && Main.raining)
                        {
                            pool[NPCID.LightningBug] = .1f;
                        }
                    }
                    else if (wideUnderground)
                    {
		                if (nearLava)
                        {
                            pool[NPCID.FireImp] = .02f;
                            pool[NPCID.LavaSlime] = .02f;
                        }

                        if (cavern)
                        {
                            if (noBiome && NPC.downedBoss3)
                            {
                                pool[NPCID.DarkCaster] = .05f;
                            }
                        }

                        if (dungeon && night && normalSpawn)
                        {
                            pool[NPCID.SkeletronHead] = BossIsAlive(ref skeleBoss, NPCID.SkeletronHead) ? .00125f : .0025f;
                        }

                        if (NPC.downedMechBossAny)
                        {
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

                            if (noBiome)
                            {
                                pool[NPCID.DD2KoboldWalkerT2] = .1f;
                            }
                        }

                        if (NPC.downedPlantBoss && !Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().SkullCharm)
                        {
                            pool[NPCID.DiabolistRed] = .002f;
                            pool[NPCID.DiabolistWhite] = .002f;
                            pool[NPCID.Necromancer] = .002f;
                            pool[NPCID.NecromancerArmored] = .002f;
                            pool[NPCID.RaggedCaster] = .002f;
                            pool[NPCID.RaggedCasterOpenCoat] = .002f;
                        }

                        if (NPC.downedGolemBoss && noBiome)
                        {
                            pool[NPCID.DD2KoboldWalkerT3] = .1f;
                        }

                        if (NPC.downedAncientCultist && dungeon && !BossIsAlive(ref cultBoss, NPCID.CultistBoss))
                        {
                            pool[NPCID.CultistBoss] = 0.001f;
                        }

                        if (hallow)
                        {
                            if (NPC.downedGolemBoss)
                            {
                                pool[NPCID.DD2WitherBeastT3] = .1f;
                            }
                            else
                            {
                                pool[NPCID.DD2WitherBeastT2] = .1f;
                            }
                        }
                    }
                    else if (underworld)
                    {
		    pool[NPCID.LeechHead] = .025f;
                        pool[NPCID.BlazingWheel] = .05f;
		    
                        if (!BossIsAlive(ref wallBoss, NPCID.WallofFlesh))
                        {
                            pool[NPCID.TheHungryII] = .03f;
                        }

                        if (NPC.downedPlantBoss && !Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().SkullCharm)
                        {
                            pool[NPCID.DiabolistRed] = .002f;
                            pool[NPCID.DiabolistWhite] = .002f;
                            pool[NPCID.Necromancer] = .002f;
                            pool[NPCID.NecromancerArmored] = .002f;
                            pool[NPCID.RaggedCaster] = .002f;
                            pool[NPCID.RaggedCasterOpenCoat] = .002f;
                        }

                        if (NPC.downedGolemBoss)
                        {
                            pool[NPCID.DD2Betsy] = .005f;
                            pool[NPCID.DD2WyvernT3] = .05f;
                            pool[NPCID.DD2DrakinT3] = .05f;
                        }
                        else
                        {
                            pool[NPCID.DD2WyvernT2] = .05f;
                            pool[NPCID.DD2DrakinT2] = .05f;
                        }
                    }
                    else if (sky)
                    {
                        if (normalSpawn)
                        {
			pool[NPCID.AngryNimbus] = .05f;
			
                            if (NPC.downedGolemBoss)
                            {
                                pool[NPCID.SolarCrawltipedeHead] = .03f;
                                pool[NPCID.VortexHornetQueen] = .03f;
                                pool[NPCID.NebulaBrain] = .03f;
                                pool[NPCID.StardustJellyfishBig] = .03f;
                                pool[NPCID.AncientCultistSquidhead] = .03f;
                                pool[NPCID.CultistDragonHead] = .03f;
                            }
                            else if(NPC.downedMechBossAny)
                            {
                                pool[NPCID.SolarCrawltipedeHead] = .01f;
                                pool[NPCID.VortexHornetQueen] = .01f;
                                pool[NPCID.NebulaBrain] = .01f;
                                pool[NPCID.StardustJellyfishBig] = .01f;
                            }
                            if (NPC.downedMoonlord && !BossIsAlive(ref moonBoss, NPCID.MoonLordCore))
                            {
                                pool[NPCID.MoonLordCore] = 0.001f;
                            }
                        }
                    }

                    //height-independent biomes
                    if (corruption)
                    {
                        if (normalSpawn)
                        {
                            pool[NPCID.EaterofWorldsHead] = BossIsAlive(ref eaterBoss, NPCID.EaterofWorldsHead) ? .0025f : .005f;
                        }
                    }

                    if (crimson)
                    {
                        if (normalSpawn)
                        {
                            pool[NPCID.BrainofCthulhu] = BossIsAlive(ref brainBoss, NPCID.BrainofCthulhu) ? .0025f : .005f;
                        }
                    }

                    if (jungle)
                    {
                        if (day && normalSpawn)
                        {
                            pool[NPCID.QueenBee] = BossIsAlive(ref beeBoss, NPCID.QueenBee) ? .00125f : .0025f;
                        }

                        if (!surface)
                        {
                            pool[NPCID.BigMimicJungle] = .01f;

                            if (NPC.downedGolemBoss && !BossIsAlive(ref NPC.plantBoss, NPCID.Plantera))
                            {
                                pool[NPCID.Plantera] = .00125f;
                            }
                        }
                    }

                    if (meteor && NPC.downedGolemBoss)
                    {
                        pool[NPCID.SolarCorite] = .025f;
                    }

                    if (spawnInfo.lihzahrd)
                    {
                        pool[NPCID.BlazingWheel] = .1f;
                        pool[NPCID.SpikeBall] = .1f;
                    }
                }

                //maybe make moon lord core masoAI handle these spawns...?
                if (BossIsAlive(ref moonBoss, NPCID.MoonLordCore))
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
		
		public override bool PreNPCLoot (NPC npc)
		{
            return dropLoot;
		}

        private bool firstLoot = true;

		public override void NPCLoot(NPC npc)
		{
			Player player = Main.player[Main.myPlayer];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if(modPlayer.PlatinumEnchant && !npc.boss && firstLoot)
            {
                bool midas = npc.HasBuff(BuffID.Midas);
                int chance = 10;
                int bonus = 4;

                if(midas)
                {
                    chance/= 2;
                    bonus *= 2;
                }

                if(Main.rand.Next(chance) == 0)
                {
                    firstLoot = false;
                    for (int i = 1; i < bonus; i++)
                    {
                        npc.NPCLoot();
                        NPC.killCount[Item.NPCtoBanner(npc.BannerID())]--;
                    }
                }
            }

            firstLoot = false;

            if (FargoWorld.MasochistMode)
            {
                switch (npc.type)
                {
                    case NPCID.CaveBat:
                    case NPCID.GiantBat:
                    case NPCID.IceBat:
                    case NPCID.JungleBat:
                    case NPCID.Vampire:
                    case NPCID.VampireBat:
                    case NPCID.GiantFlyingFox:
                    case NPCID.Hellbat:
                    case NPCID.Lavabat:
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RabiesShot"));
                        break;

                    case NPCID.IlluminantBat:
                        if (Main.rand.Next(20) == 0)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RabiesShot"));
                        goto case NPCID.IlluminantSlime;

                    case NPCID.IlluminantSlime:
                    case NPCID.EnchantedSword:
                        if (Main.rand.Next(10) == 0)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VolatileEnergy"));
                        break;

                    case NPCID.ChaosElemental:
                        if (Main.rand.Next(5) == 0)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VolatileEnergy"));
                        break;

                    case NPCID.SkeletonSniper:
                    case NPCID.TacticalSkeleton:
                    case NPCID.SkeletonCommando:
                    case NPCID.DiabolistRed:
                    case NPCID.DiabolistWhite:
                    case NPCID.Necromancer:
                    case NPCID.NecromancerArmored:
                    case NPCID.RaggedCaster:
                    case NPCID.RaggedCasterOpenCoat:
                        if (Main.rand.Next(100) == 0)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SkullCharm"));
                        break;

                    case NPCID.BigMimicJungle:
                        if (Main.rand.Next(5) == 0)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TribalCharm"));
                        break;

                    case NPCID.KingSlime:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.HerbBag);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SlimyShield"));
                        break;

                    case NPCID.EyeofCthulhu:
                        int maxEOC = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxEOC; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.ThornsPotion);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AgitatingLens"));
                        break;

                    case NPCID.EaterofWorldsHead:
                    case NPCID.EaterofWorldsBody:
                    case NPCID.EaterofWorldsTail:
                        bool dropPotions = true;
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && i != npc.whoAmI && (Main.npc[i].type == 13 || Main.npc[i].type == 14 || Main.npc[i].type == 15))
                            {
                                dropPotions = false;
                                break;
                            }
                        }
                        if (dropPotions)
                        {
                            int max = Main.rand.Next(10) + 1;
                            for (int i = 0; i < max; i++)
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.RagePotion);
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CorruptHeart"));
                        }
                        break;

                    case NPCID.BrainofCthulhu:
                        int maxBOC = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxBOC; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.WrathPotion);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GuttedHeart"));
                        break;

                    case NPCID.SkeletronHead:
                        int maxSkel = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxSkel; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.TitanPotion);
                        break;

                    case NPCID.QueenBee:
                        int maxQB = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxQB; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.SummoningPotion);
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("QueenStinger"));
                        break;

                    case NPCID.WallofFlesh:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PungentEyeball"));
                        int maxWOF = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxWOF; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.InfernoPotion);
                        break;

                    case NPCID.Retinazer:
                        if (!BossIsAlive(ref spazBoss, NPCID.Spazmatism))
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FusedLens"));
                            int max = Main.rand.Next(10) + 1;
                            for (int i = 0; i < max; i++)
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MagicPowerPotion);
                        }
                        break;

                    case NPCID.Spazmatism:
                        if (!BossIsAlive(ref retiBoss, NPCID.Retinazer))
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FusedLens"));
                            int max = Main.rand.Next(10) + 1;
                            for (int i = 0; i < max; i++)
                                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.MagicPowerPotion);
                        }
                        break;

                    case NPCID.TheDestroyer:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GroundStick"));
                        int maxDes = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxDes; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.GravitationPotion);
                        break;

                    case NPCID.SkeletronPrime:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ReinforcedPlating"));
                        int maxSP = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxSP; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.EndurancePotion);
                        break;

                    case NPCID.Plantera:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MagicalBulb"));
                        int maxPlant = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxPlant; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.CalmingPotion);
                        break;

                    case NPCID.Golem:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LihzahrdTreasureBox"));
                        int maxGolem = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxGolem; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ItemID.LifeforcePotion);
                        break;

                    case NPCID.DD2Betsy:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BetsysHeart"));
                        break;

                    case NPCID.DukeFishron:
                        int type;
                        if (npc.whoAmI == fishBossEX)
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CyclonicFin"));
                            type = mod.ItemType("Sadism");
                        }
                        else
                        {
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MutantAntibodies"));
                            type = ItemID.Bacon;
                        }
                        int maxDF = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxDF; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, type);
                        break;

                    case NPCID.CultistBoss:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialRune"));
                        break;

                    case NPCID.MoonLordCore:
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GalacticGlobe"));
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CelestialSeal"));
                        int maxML = Main.rand.Next(10) + 1;
                        for (int i = 0; i < maxML; i++)
                            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LunarCrystal"));
                        break;

                    default:
                        break;
                }
            }
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

            if (Needles && npc.lifeMax > 1 && Main.rand.Next(2) == 0)
            {
                int dmg = 15;
                int numNeedles = 8;

                if (modPlayer.LifeForce)
                {
                    dmg = 50;
                    numNeedles = 16;
                }

                Projectile[] projs = FargoGlobalProjectile.XWay(numNeedles, npc.Center, ProjectileID.PineNeedleFriendly, 5, (int)(dmg * player.meleeDamage), 5f);

                for (int i = 0; i < projs.Length; i++)
                {
                    if (projs[i] == null) continue;
                    Projectile p = projs[i];
                    p.GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                    p.magic = false;
                    p.melee = true;
                    p.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
                }
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
                        int ball = Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-5, 6), -5), ProjectileID.SpikyBall, 15, 0, Main.myPlayer);
                        if (ball < 1000)
                        {
                            Main.projectile[ball].hostile = true;
                            Main.projectile[ball].friendly = false;
                            Main.projectile[ball].GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                            Main.projectile[ball].penetrate = 1;
                        }
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
                            Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11)), ProjectileID.DrManFlyFlask, npc.damage / 2, 1f, Main.myPlayer);
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
                        if (npc.ai[1] != 2f)
                        {
                            Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            for (int k = 0; k < npc.buffImmune.Length; k++)
                                npc.buffImmune[k] = true;
                            while (npc.buffTime[0] != 0)
                                npc.DelBuff(0);

                            npc.life = npc.lifeMax / 176;
                            if (npc.life < 50)
                                npc.life = 50;
                            npc.defDefense = 9999;
                            npc.defense = 9999;
                            npc.defDamage *= 15;
                            npc.damage *= 15;
                            npc.ai[1] = 2f;
                            npc.netUpdate = true;
                            return false;
                        }
                        else if (FargoWorld.SkeletronCount < 280)
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
                            Main.PlaySound(15, (int)npc.position.X, (int)npc.position.Y, 0);
                            for (int k = 0; k < npc.buffImmune.Length; k++)
                                npc.buffImmune[k] = true;
                            while (npc.buffTime[0] != 0)
                                npc.DelBuff(0);

                            npc.life = npc.lifeMax / 420;
                            if (npc.life < 100)
                                npc.life = 100;
                            npc.defDefense = 9999;
                            npc.defense = 9999;
                            npc.defDamage *= 13;
                            npc.damage *= 13;
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
                        else
                        {
                            if (FargoWorld.FishronCount < 120)
                            {
                                FargoWorld.FishronCount++;
                            }

                            if (fishBossEX == npc.whoAmI)
                            {
                                if (!FargoWorld.downedFishronEX)
                                    Main.NewText("The ocean stirs...", 0, 100, 255);
                                FargoWorld.downedFishronEX = true;
                            }
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

                    case 25: //pinky
                        for (int i = 0; i < 3; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);

                            if (spawn != 200)
                            {
                                Main.npc[spawn].SetDefaults(i < 2 ? NPCID.YellowSlime : NPCID.MotherSlime);
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

                    case 26: //flying snake
                        masoDeathAI = 0;
                        SharkCount = 1;
                        npc.life = npc.lifeMax;
                        npc.damage = npc.damage * 3 / 2;
                        return false;

                    case 27: //lihzahrd
                        if (Main.netMode != 1)
                        {
                            for (int i = 0; i < 3; i++)
                            {
                                Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(21)), ProjectileID.SpikyBallTrap, 30, 0f, Main.myPlayer);
                            }
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
                        }
                        break;

                    case 29: //drakanian
                        int t = npc.HasPlayerTarget ? npc.target : npc.FindClosestPlayer();
                        if (t != -1 && Main.player[t].active && !Main.player[t].dead && Main.netMode != 1)
                        {
                            Vector2 velocity = Main.player[t].Center - npc.Center;
                            velocity.Normalize();
                            velocity *= 14f;

                            Projectile.NewProjectile(npc.Center, velocity, mod.ProjectileType("DrakanianDaybreak"), npc.damage / 4, 1f, Main.myPlayer);
                        }
                        Main.PlaySound(SoundID.Item1, npc.Center);
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
                        if (Main.rand.Next(3) != 0)
                        {
                            Main.PlaySound(npc.DeathSound, npc.Center); //die without contributing to pillar shield
                            npc.active = false;
                            return false;
                        }
                        break;

                    case 32: //brain suckler
                        if (npc.ai[0] == 5f) //latched on player
                        {
                            npc.Transform(NPCID.NebulaBrain);
                            return false;
                        }
                        else if (Main.rand.Next(3) != 0) //die without contributing to pillar shield
                        {
                            Main.PlaySound(npc.DeathSound, npc.Center);
                            npc.active = false;
                            return false;
                        }
                        return true;

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
                            int balls = Projectile.NewProjectile(npc.Center, new Vector2(Main.rand.Next(-500, 501) / 100f, Main.rand.Next(-1000, 1) / 100f), ProjectileID.SpikyBall, npc.damage / 8, 0, Main.myPlayer);
                            if (balls < 1000)
                            {
                                Main.projectile[balls].hostile = true;
                                Main.projectile[balls].friendly = false;
                                Main.projectile[balls].GetGlobalProjectile<FargoGlobalProjectile>().IsRecolor = true;
                            }
                        }
                        break;

                    case 36: //clown
                        for (int i = 0; i < 30; i++)
                        {
                            int p = Projectile.NewProjectile(npc.position.X + Main.rand.Next(npc.width), npc.position.Y + Main.rand.Next(npc.height), Main.rand.Next(-500, 501) / 100f, Main.rand.Next(-1000, 101) / 100f, ProjectileID.BouncyGrenade, 200, 8f, Main.myPlayer);
                            Main.projectile[p].timeLeft -= Main.rand.Next(120);
                        }
                        break;

                    case 37: //shark
                        if(Main.hardMode && Main.rand.Next(4) == 0)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, ProjectileID.Cthulunado, npc.damage / 2, 0f, Main.myPlayer, 16, 11);
                        }
                        break;

                    case 38: //prime limbs
                        if (npc.ai[1] >= 0f && npc.ai[1] < 200f && Main.npc[(int)npc.ai[1]].type == NPCID.SkeletronPrime)
                        {
                            if (Main.npc[(int)npc.ai[1]].dontTakeDamage)
                            {
                                Main.npc[(int)npc.ai[1]].dontTakeDamage = false;
                                if (Main.netMode == 2)
                                {
                                    var netMessage = mod.GetPacket();
                                    netMessage.Write((byte)2);
                                    netMessage.Write((byte)npc.ai[1]);
                                    netMessage.Send();
                                }
                            }
                            if (Main.npc[(int)npc.ai[1]].ai[0] == 1f)
                            {
                                switch (npc.type)
                                {
                                    case NPCID.PrimeLaser: Main.npc[(int)npc.ai[1]].ai[0] = 3f; break;
                                    case NPCID.PrimeCannon: Main.npc[(int)npc.ai[1]].ai[0] = 4f; break;
                                    case NPCID.PrimeSaw: Main.npc[(int)npc.ai[1]].ai[0] = 5f; break;
                                    case NPCID.PrimeVice: Main.npc[(int)npc.ai[1]].ai[0] = 6f; break;
                                    default: break;
                                }
                            }
                            Main.npc[(int)npc.ai[1]].netUpdate = true;
                        }
                        break;

                    case 39: //shroom zombies, anomura
                        for (int i = 0; i < 10; i++)
                        {
                            int spore = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, NPCID.FungiSpore);
                            Main.npc[spore].velocity = new Vector2(Main.rand.Next(-5, 5), Main.rand.Next(-5, 5));
                        }
                        break;
			
		case 40: //rainbow slime 1
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

                            slime.GetGlobalNPC<FargoGlobalNPC>().masoDeathAI = 41;
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
			
		case 41: //rainbow slime 2
			for (int i = 0; i < 10; i++)
                        {
                            int spawn = NPC.NewNPC((int)(npc.position.X + npc.width / 2), (int)(npc.position.Y + npc.height), 1);

                            if (spawn != 200)
                            {
                                Main.npc[spawn].SetDefaults(i < 5 ? NPCID.Pinky : NPCID.Gastropod);
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

        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();

            if (modPlayer.CactusEnchant)
            {
                Needles = true;
            }

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
                            damage /= 3;
                            npc.ai[2] = -6f;
                        }
                        break;

                    case 6: //all mechs
                        break;

                    case 7: //golem fists
                        break;

                    case 8: //skelly prime DR
                        int armCount = 0;
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type >= 128 && Main.npc[i].type <= 131 && Main.npc[i].ai[1] == npc.whoAmI)
                                armCount++;
                        }
                        switch (armCount)
                        {
                            case 4:
                                damage = 0;
                                break;
                            case 3:
                                damage = damage / 2;
                                break;
                            case 2:
                                damage = (int)(damage * 0.75);
                                break;
                            case 1:
                                damage = (int)(damage * 0.9);
                                break;
                            default:
                                break;
                        }
                        goto case 6;

                    case 9: //brain of cthulhu
                        if (!player.HasBuff(BuffID.Confused) && Main.rand.Next(10) == 0)
                        {
                            player.AddBuff(BuffID.Confused, Main.rand.Next(300));
                            Projectile.NewProjectile(npc.Center, new Vector2(-5, 0), ProjectileID.BrainOfConfusion, 0, 0, Main.myPlayer);
                        }

                        break;

                    case 10: //ice tortoise
                        float reduction = (float)npc.life / npc.lifeMax;
                        if (reduction < 0.25f)
                            reduction = 0.25f;
                        damage = (int)(damage * reduction);
                        break;

                    case 11: //wof
                        /*if (npc.ai[0] == 2f || npc.ai[0] == -2f)
                            damage = (int)(damage * 0.75);*/
                        break;

                    case 12: //moon lord
                        if (npc.type == NPCID.MoonLordCore)
                            damage = damage * 2 / 3;
                        else if (npc.type == NPCID.MoonLordHead)
                            damage = damage * 2;
                        /*switch (masoState)
                        {
                            case 0: if (!item.melee) damage = 0; break;
                            case 1: if (!item.ranged) damage = 0; break;
                            case 2: if (!item.magic) damage = 0; break;
                            case 3: if (!item.summon) damage = 0; break;
                            case 4: if (!item.thrown) damage = 0; break;
                        }*/
                        break;

                    case 13: //tortoise
                        player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was impaled by a Giant Tortoise."), damage / 2, 0);
                        break;

                    /*case 14: //psycho
                        npc.Opacity = 1;
                        Counter = 0;
                        break;*/

                    case 15: //fishron
                        if (modPlayer.UniverseEffect && crit)
                            damage /= 5;
                        if (masoBool[2])
                            damage = 0;
                        break;

                    default:
                        break;
                }
            }
        }

        public override void ModifyHitByProjectile (NPC npc, Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
		{
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (modPlayer.CactusEnchant)
                Needles = true;

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
                            damage /= 3;
                            npc.ai[2] = -6f;
                        }
                        break;

                    case 6: //all mechs
                        if (projectile.type == ProjectileID.HallowStar)
                            damage /= 4;
                        break;

                    case 7: //golem fists
                        if (projectile.maxPenetrate != 1 && !projectile.minion)
                            projectile.active = false;
                        break;

                    case 8: //skelly prime DR
                        int armCount = 0;
                        for (int i = 0; i < 200; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type >= 128 && Main.npc[i].type <= 131 && Main.npc[i].ai[1] == npc.whoAmI)
                                armCount++;
                        }
                        switch (armCount)
                        {
                            case 4:
                                damage = 0;
                                break;
                            case 3:
                                damage = damage / 2;
                                break;
                            case 2:
                                damage = (int)(damage * 0.75);
                                break;
                            case 1:
                                damage = (int)(damage * 0.9);
                                break;
                            default:
                                break;
                        }
                        goto case 6;

                    case 9: //brain of cthulhu
                        if (!player.HasBuff(BuffID.Confused) && Main.rand.Next(10) == 0)
                        {
                            player.AddBuff(BuffID.Confused, Main.rand.Next(300));
                            Projectile.NewProjectile(npc.Center, new Vector2(-5, 0), ProjectileID.BrainOfConfusion, 0, 0, Main.myPlayer);
                        }
                        break;

                    case 10: //ice tortoise
                        float reduction = (float)npc.life / npc.lifeMax;
                        if (reduction < 0.25f)
                            reduction = 0.25f;
                        damage = (int)(damage * reduction);
                        break;

                    case 11: //wof
                        /*if (npc.ai[0] == 2f || npc.ai[0] == -2f)
                            damage = (int)(damage * 0.75);*/
                        if (projectile.type == ProjectileID.Bee || projectile.type == ProjectileID.GiantBee)
                            damage /= 5;
                        break;

                    case 12: //moon lord
                        if (projectile.type == ProjectileID.DD2BetsyArrow || projectile.type == ProjectileID.PhantasmArrow)
                            damage = damage * 3 / 4;
                        else if (npc.type == NPCID.MoonLordCore)
                            damage = damage * 2 / 3;
                        else if (npc.type == NPCID.MoonLordHead)
                            damage = damage * 2;
                        /*switch (masoState)
                        {
                            case 0: if (!projectile.melee) damage = 0; break;
                            case 1: if (!projectile.ranged) damage = 0; break;
                            case 2: if (!projectile.magic) damage = 0; break;
                            case 3: if (!projectile.minion) damage = 0; break;
                            case 4: if (!projectile.thrown) damage = 0; break;
                        }*/
                        break;

                    case 13: //phantasmal dragon
                        if (projectile.maxPenetrate > 1)
                            damage /= projectile.maxPenetrate;
                        else if (projectile.maxPenetrate < 0 || projectile.type == ProjectileID.DD2BetsyArrow)
                            damage /= 4;
                        break;
                        
                    case 15: //fishron
                        if (modPlayer.UniverseEffect && crit)
                            damage /= 5;
                        if (projectile.ranged)
                        {
                            if (projectile.arrow)
                            {
                                if (projectile.type == ProjectileID.PhantasmArrow || projectile.type == ProjectileID.DD2BetsyArrow)
                                    damage /= 4;
                                if (projectile.type == mod.ProjectileType("FargoArrowProj"))
                                {
                                    projectile.active = false;
                                    damage = 0;
                                }
                            }
                            else if (projectile.type == mod.ProjectileType("FargoBulletProj"))
                            {
                                projectile.active = false;
                                damage = 0;
                            }
                        }
                        if (masoBool[2])
                            damage = 0;
                        break;

                    default:
                        break;
                }
            }

            //bees ignore defense
            if (modPlayer.BeeEnchant && !modPlayer.TerrariaSoul && projectile.type == ProjectileID.GiantBee)
                damage = (int)(damage + npc.defense * .5);

            if (modPlayer.SpiderEnchant && projectile.minion && Main.rand.Next(10) == 0)
            {
                crit = true;
                damage *= 2;
            }

            /*if (projectile.type == ProjectileID.EyeFire) //why is there a type check in here, MAKE A MODPROJ
                npc.AddBuff(BuffID.CursedInferno, 300);*/
		}

        public override void ModifyHitPlayer(NPC npc, Player target, ref int damage, ref bool crit)
        {
            FargoPlayer modPlayer = target.GetModPlayer<FargoPlayer>(mod);

            /*if (FargoWorld.MasochistMode)
            {
                switch (npc.type)
                {
                    

                    default:
                        break;
                }
            }*/
           
            if(target.HasBuff(mod.BuffType("ShellHide")))
                damage *= 2;

            if(SqueakyToy)
            {
                damage = 1;
                modPlayer.Squeak(target.Center);
            }

            if (modPlayer.DeathMarked)
            {
                damage *= 10;
                crit = true;
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

                if (npc.realLife == -1)
                    ResetRegenTimer(npc);
                else
                    ResetRegenTimer(Main.npc[npc.realLife]);
            }

            if (OceanicMaul)
                damage += 30;

            if (CurseoftheMoon)
            {
                damage += 5;
                damage *= 1.1;
            }

            if (modPlayer.Eternity)
            {
                if (crit)
                {
                    damage *= 10;
                    retValue = false;
                }
            }
			else if(modPlayer.UniverseEffect)
			{
				if(crit)
				{
					damage *= 5;
                    retValue = false;
				}
			}

            if(modPlayer.RedEnchant && !modPlayer.WillForce)
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
			
			if(crit && modPlayer.ShroomEnchant && !modPlayer.TerrariaSoul && player.stealth == 0)
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
			
            if(modPlayer.ValhallaEnchant && Soulcheck.GetValue("Valhalla Knockback") &&
                npc.type != NPCID.WallofFlesh && npc.type != NPCID.WallofFleshEye && npc.type != NPCID.TargetDummy &&
                !npc.GetGlobalNPC<FargoGlobalNPC>().ValhallaImmune && npc.knockBackResist < 1)
            {
                npc.knockBackResist += .02f;
                if(npc.knockBackResist > .5f)
                    npc.knockBackResist = .5f;
            }
		}

        public override void OnHitByProjectile(NPC npc, Projectile projectile, int damage, float knockback, bool crit)
        {
            FargoPlayer modPlayer = Main.player[projectile.owner].GetModPlayer<FargoPlayer>(mod);

            //spears
            if(modPlayer.ValhallaEnchant && Soulcheck.GetValue("Valhalla Knockback") && (projectile.aiStyle == 19 || modPlayer.WillForce) &&
                npc.type != NPCID.WallofFlesh && npc.type != NPCID.WallofFleshEye && npc.type != NPCID.TargetDummy &&
                !npc.GetGlobalNPC<FargoGlobalNPC>().ValhallaImmune && npc.knockBackResist < 1)
            {
                npc.knockBackResist += .02f;
                if (npc.knockBackResist > .5f)
                    npc.knockBackResist = .5f;
            }
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            if (FargoWorld.MasochistMode && masoHurtAI == 12)
            {
                return masoState == 0;
            }

            return null;
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            if (FargoWorld.MasochistMode && masoHurtAI == 12)
            {
                switch (masoState)
                {
                    case 0: if (!projectile.melee) return false; break;
                    case 1: if (!projectile.ranged) return false; break;
                    case 2: if (!projectile.magic) return false; break;
                    case 3: if (!projectile.minion) return false; break;
                    //case 4: if (!projectile.thrown) return false; break;
                    default: break;
                }
            }

            return null;
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
