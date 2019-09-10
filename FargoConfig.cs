
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
namespace FargowiltasSouls
{
    class SoulConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static SoulConfig Instance;
        [JsonIgnore]
        public Dictionary<string, bool> enchantToggles = new Dictionary<string, bool>();
        [JsonIgnore]
        public Dictionary<string, bool> masoTogDict = new Dictionary<string, bool>();
        [JsonIgnore]
        public Dictionary<string, bool> petToggles = new Dictionary<string, bool>();
        [JsonIgnore]
        public Dictionary<string, bool> walletToggles = new Dictionary<string, bool>();
        [JsonIgnore]
        public Dictionary<string, bool> soulToggles = new Dictionary<string, bool>();
        [Label("Terraria")]
        public TerraMenu terrmenu = new TerraMenu();
        [Label("Calamity")]
        public CalamMenu calamenu = new CalamMenu();
        [Label("Thorium")]
        public ThorMenu thoriummenu = new ThorMenu();
        [Label("Masochist Mode")]
        public MasoMenu masomenu = new MasoMenu();
        [Label("Pets")]
        public PetMenu petmenu = new PetMenu();
        [SeparatePage]
        public class TerraMenu
        {
            public bool loltesta = true;
            [Label("Enchants")]
            public TerEncMenu terenchmenu = new TerEncMenu();
            [Label("Souls")]
            public SoulMenu soulmenu = new SoulMenu();
            [SeparatePage]
            public class TerEncMenu
            {
                //public s string get = SoulConfig.Instance.mod.ItemType("WoodForce").ToString;
                [Header("$Mods.FargowiltasSouls.WoodHeader")]
                [Label("$Mods.FargowiltasSouls.BorealConfig")]
                public bool borealSnow = true;
                [Label("$Mods.FargowiltasSouls.EbonConfig")]
                public bool ebonFlame = true;
                [Label("$Mods.FargowiltasSouls.MahoganyConfig")]
                public bool mahoganyHook = true;
                [Label("$Mods.FargowiltasSouls.PalmConfig")]
                public bool palmSentry = true;
                [Label("$Mods.FargowiltasSouls.PearlConfig")]
                public bool pearlTrail = true;
                [Header("$Mods.FargowiltasSouls.EarthHeader")]
                [Label("$Mods.FargowiltasSouls.AdamantiteConfig")]
                public bool adamSplit = true;
                [Label("$Mods.FargowiltasSouls.CobaltConfig")]
                public bool cobaltShards = true;
                [Label("$Mods.FargowiltasSouls.MythrilConfig")]
                public bool mythSpeed = true;
                [Label("$Mods.FargowiltasSouls.OrichalcumConfig")]
                public bool oriFire = true;
                [Label("$Mods.FargowiltasSouls.PalladiumConfig")]
                public bool palHeal = true;
                [Label("$Mods.FargowiltasSouls.TitaniumConfig")]
                public bool titDodge = true;
                [Header("$Mods.FargowiltasSouls.TerraHeader")]
                [Label("$Mods.FargowiltasSouls.CopperConfig")]
                public bool copLight = true;
                [Label("$Mods.FargowiltasSouls.IronMConfig")]
                public bool ironMag = true;
                [Label("$Mods.FargowiltasSouls.IronSConfig")]
                public bool ironShield = true;
                [Label("$Mods.FargowiltasSouls.TinConfig")]
                public bool tinCrit = true;
                [Label("$Mods.FargowiltasSouls.TungstenConfig")]
                public bool tung = true;
                [Header("$Mods.FargowiltasSouls.WillHeader")]
                [Label("$Mods.FargowiltasSouls.GladiatorConfig")]
                public bool gladRain = true;
                [Label("$Mods.FargowiltasSouls.GoldConfig")]
                public bool goldCoin = true;
                [Label("$Mods.FargowiltasSouls.RedRidingConfig")]
                public bool redBleed = true;
                [Label("$Mods.FargowiltasSouls.ValhallaConfig")]
                public bool valhalKnock = true;
                [Header("$Mods.FargowiltasSouls.LifeHeader")]
                [Label("$Mods.FargowiltasSouls.BeetleConfig")]
                public bool beetBeetles = true;
                [Label("$Mods.FargowiltasSouls.CactusConfig")]
                public bool cacNeedle = true;
                [Label("$Mods.FargowiltasSouls.PumpkinConfig")]
                public bool pumpFire = true;
                [Label("$Mods.FargowiltasSouls.SpiderConfig")]
                public bool spidSwarm = true;
                [Label("$Mods.FargowiltasSouls.TurtleConfig")]
                public bool turtShell = true;
                //force of nature
                [Header("$Mods.FargowiltasSouls.NatureHeader")]
                [Label("$Mods.FargowiltasSouls.ChlorophyteConfig")]
                public bool chloroCrystal = true;
                [Label("$Mods.FargowiltasSouls.FrostConfig")]
                public bool frostIce = true;
                [Label("$Mods.FargowiltasSouls.JungleConfig")]
                public bool jungleSpores = true;
                [Label("$Mods.FargowiltasSouls.MoltenConfig")]
                public bool moltInfern = true;
                [Label("$Mods.FargowiltasSouls.ShroomiteConfig")]
                public bool shroomStealth = true;
                //shadow force
                [Header("$Mods.FargowiltasSouls.ShadowHeader")]
                [Label("$Mods.FargowiltasSouls.DarkArtConfig")]
                public bool dArtEffect = true;
                [Label("$Mods.FargowiltasSouls.NecroConfig")]
                public bool necroGuard = true;
                [Label("$Mods.FargowiltasSouls.ShadowConfig")]
                public bool shadowDark = true;
                [Label("$Mods.FargowiltasSouls.ShinobiConfig")]
                public bool shinWalls = true;
                [Label("$Mods.FargowiltasSouls.SpookyConfig")]
                public bool spookScythe = true;
                //force of spirit
                [Header("$Mods.FargowiltasSouls.SpiritHeader")]
                [Label("$Mods.FargowiltasSouls.ForbiddenConfig")]
                public bool forbidStorm = true;
                [Label("$Mods.FargowiltasSouls.HallowedConfig")]
                public bool hallowSword = true;
                [Label("$Mods.FargowiltasSouls.HalllowSConfig")]
                public bool hallowShield = true;
                [Label("$Mods.FargowiltasSouls.SilverConfig")]
                public bool silverSword = true;
                [Label("$Mods.FargowiltasSouls.SpectreConfig")]
                public bool spectreOrb = true;
                //force of cosmos
                [Header("$Mods.FargowiltasSouls.CosmoHeader")]
                [Label("$Mods.FargowiltasSouls.MeteorConfig")]
                public bool meteorShow = true;
                [Label("$Mods.FargowiltasSouls.NebulaConfig")]
                public bool nebulaBoost = true;
                [Label("$Mods.FargowiltasSouls.SolarConfig")]
                public bool solarShield = true;
                [Label("$Mods.FargowiltasSouls.StardustConfig")]
                public bool stardGuard = true;
                [Label("$Mods.FargowiltasSouls.VortexSConfig")]
                public bool vortSneak = true;
                [Label("$Mods.FargowiltasSouls.VortexVConfig")]
                public bool vortVoid = true;
                public void Change()
                {
                    SoulConfig.Instance.enchantToggles["Boreal Snowballs"] = borealSnow;
                    SoulConfig.Instance.enchantToggles["Ebonwood Shadowflame"] = ebonFlame;
                    SoulConfig.Instance.enchantToggles["Mahogany Hook Speed"] = mahoganyHook;
                    SoulConfig.Instance.enchantToggles["Palmwood Sentry"] = palmSentry;
                    SoulConfig.Instance.enchantToggles["Pearlwood Rainbow"] = pearlTrail;
                    SoulConfig.Instance.enchantToggles["Adamantite Projectile Splitting"] = adamSplit;
                    SoulConfig.Instance.enchantToggles["Cobalt Shards"] = cobaltShards;
                    SoulConfig.Instance.enchantToggles["Mythril Weapon Speed"] = mythSpeed;
                    SoulConfig.Instance.enchantToggles["Orichalcum Fireballs"] = oriFire;
                    SoulConfig.Instance.enchantToggles["Palladium Healing"] = palHeal;
                    SoulConfig.Instance.enchantToggles["Titanium Shadow Dodge"] = titDodge;
                    SoulConfig.Instance.enchantToggles["Copper Lightning"] = copLight;
                    SoulConfig.Instance.enchantToggles["Iron Magnet"] = ironMag;
                    SoulConfig.Instance.enchantToggles["Iron Shield"] = ironShield;
                    SoulConfig.Instance.enchantToggles["Tin Crits"] = tinCrit;
                    SoulConfig.Instance.enchantToggles["Tungsten Effect"] = tung;
                    SoulConfig.Instance.enchantToggles["Gladiator Rain"] = gladRain;
                    SoulConfig.Instance.enchantToggles["Gold Lucky Coin"] = goldCoin;
                    SoulConfig.Instance.enchantToggles["Red Riding Super Bleed"] = redBleed;
                    SoulConfig.Instance.enchantToggles["Valhalla Knockback"] = valhalKnock;
                    SoulConfig.Instance.enchantToggles["Beetles"] = beetBeetles;
                    SoulConfig.Instance.enchantToggles["Cactus Needles"] = cacNeedle;
                    SoulConfig.Instance.enchantToggles["Pumpkin Fire"] = pumpFire;
                    SoulConfig.Instance.enchantToggles["Spider Swarm"] = spidSwarm;
                    SoulConfig.Instance.enchantToggles["Turtle Shell Buff"] = turtShell;
                    //force of nature
                    SoulConfig.Instance.enchantToggles["Chlorophyte Leaf Crystal"] = chloroCrystal;
                    SoulConfig.Instance.enchantToggles["Frost Icicles"] = frostIce;
                    SoulConfig.Instance.enchantToggles["Jungle Spores"] = jungleSpores;
                    SoulConfig.Instance.enchantToggles["Molten Inferno Buff"] = moltInfern;
                    SoulConfig.Instance.enchantToggles["Shroomite Stealth"] = shroomStealth;
                    //shadow force
                    SoulConfig.Instance.enchantToggles["Dark Artist Effect"] = dArtEffect;
                    SoulConfig.Instance.enchantToggles["Necro Guardian"] = necroGuard;
                    SoulConfig.Instance.enchantToggles["Shadow Darkness"] = shadowDark;
                    SoulConfig.Instance.enchantToggles["Shinobi Through Walls"] = shinWalls;
                    SoulConfig.Instance.enchantToggles["Spooky Scythes"] = spookScythe;
                    //force of spirit
                    SoulConfig.Instance.enchantToggles["Forbidden Storm"] = forbidStorm;
                    SoulConfig.Instance.enchantToggles["Hallowed Enchanted Sword Familiar"] = hallowSword;
                    SoulConfig.Instance.enchantToggles["Hallowed Shield"] = hallowShield;
                    SoulConfig.Instance.enchantToggles["Silver Sword Familiar"] = silverSword;
                    SoulConfig.Instance.enchantToggles["Spectre Orbs"] = spectreOrb;
                    //force of cosmos
                    SoulConfig.Instance.enchantToggles["Meteor Shower"] = meteorShow;
                    SoulConfig.Instance.enchantToggles["Nebula Boosters"] = nebulaBoost;
                    SoulConfig.Instance.enchantToggles["Solar Shield"] = solarShield;
                    SoulConfig.Instance.enchantToggles["Stardust Guardian"] = stardGuard;
                    SoulConfig.Instance.enchantToggles["Vortex Stealth"] = vortSneak;
                    SoulConfig.Instance.enchantToggles["Vortex Voids"] = vortVoid;
                }
                //[Label("[i:][c/: ]")]
                //public bool ech = true;

            }
            [SeparatePage]
            public class SoulMenu
            {
                [Label("$Mods.FargowiltasSouls.MeleeConfig")]
                public bool gladSpeed = true;
                [Label("$Mods.FargowiltasSouls.SniperConfig")]
                public bool sharpSniper = true;
                [Label("$Mods.FargowiltasSouls.UniverseConfig")]
                public bool universeSpeed = true;
                //dimensions
                [Label("$Mods.FargowiltasSouls.MiningHuntConfig")]
                public bool mineHunt = true;
                [Label("$Mods.FargowiltasSouls.MiningDangerConfig")]
                public bool mineDanger = true;
                [Label("$Mods.FargowiltasSouls.MiningSpelunkConfig")]
                public bool mineSpelunk = true;
                [Label("$Mods.FargowiltasSouls.MiningShineConfig")]
                public bool mineShine = true;
                [Label("$Mods.FargowiltasSouls.BuilderConfig")]
                public bool worldBuild = true;
                [Label("$Mods.FargowiltasSouls.DefenseSporeConfig")]
                public bool colSpore = true;
                [Label("$Mods.FargowiltasSouls.DefenseStarConfig")]
                public bool colStar = true;
                [Label("$Mods.FargowiltasSouls.DefenseBeeConfig")]
                public bool colBee = true;
                [Label("$Mods.FargowiltasSouls.SupersonicConfig")]
                public bool supersonicSpeed = true;
                //idk 
                [Label("$Mods.FargowiltasSouls.EternityConfig")]
                public bool eternityStack = true;
                public void Change()
                {
                    //Universe
                    SoulConfig.Instance.soulToggles["Melee Speed"] = gladSpeed;
                    SoulConfig.Instance.soulToggles["Sniper Scope"] = sharpSniper;
                    SoulConfig.Instance.soulToggles["Universe Attack Speed"] = universeSpeed;
                    //dimensions
                    SoulConfig.Instance.soulToggles["Mining Hunter Buff"] = mineHunt;
                    SoulConfig.Instance.soulToggles["Mining Dangersense Buff"] = mineDanger;
                    SoulConfig.Instance.soulToggles["Mining Spelunker Buff"] = mineSpelunk;
                    SoulConfig.Instance.soulToggles["Mining Shine Buff"] = mineShine;
                    SoulConfig.Instance.soulToggles["Builder Mode"] = worldBuild;
                    SoulConfig.Instance.soulToggles["Spore Sac"] = colSpore;
                    SoulConfig.Instance.soulToggles["Stars On Hit"] = colStar;
                    SoulConfig.Instance.soulToggles["Bees On Hit"] = colBee;
                    SoulConfig.Instance.soulToggles["Supersonic Speed Boosts"] = supersonicSpeed;
                    //idk 
                    SoulConfig.Instance.soulToggles["Eternity Stacking"] = eternityStack;
                }
            }

        }
        [SeparatePage]
        public class CalamMenu
        {
            public bool loltest = true;
        }
        [SeparatePage]
        public class ThorMenu
        {
            public bool loltesthor = true;
        }
        [SeparatePage]
        public class MasoMenu
        {
            [Label("$Mods.FargowiltasSouls.MasoSlimeConfig")]
            public bool slimeShield = true;
            [Label("$Mods.FargowiltasSouls.MasoEyeConfig")]
            public bool eyeScythes = true;
            [Label("$Mods.FargowiltasSouls.MasoSkeleConfig")]
            public bool skeleArms = true;
            //pure heart
            [Label("$Mods.FargowiltasSouls.MasoEaterConfig")]
            public bool tinyEaters = true;
            [Label("$Mods.FargowiltasSouls.MasoBrainConfig")]
            public bool awwMan = true;
            //bionomic cluster
            [Label("$Mods.FargowiltasSouls.MasoRainbowConfig")]
            public bool rainSlime = true;
            [Label("$Mods.FargowiltasSouls.MasoFrigidConfig")]
            public bool frostFire = true;
            [Label("$Mods.FargowiltasSouls.MasoNymphConfig")]
            public bool heartAttacks = true;
            [Label("$Mods.FargowiltasSouls.MasoSqueakConfig")]
            public bool squeakToy = true;
            [Label("$Mods.FargowiltasSouls.MasoPouchConfig")]
            public bool pouchTentacles = true;
            [Label("$Mods.FargowiltasSouls.MasoClippedConfig")]
            public bool clipAttack = true;
            [Label("Security wallet")]
            public WalletMenu wallet = new WalletMenu();

            //dubious circutry
            [Label("$Mods.FargowiltasSouls.MasoLightningConfig")]
            public bool lightRod = true;
            [Label("$Mods.FargowiltasSouls.MasoProbeConfig")]
            public bool destroyProbe = true;
            //heart of the masochist
            [Label("$Mods.FargowiltasSouls.MasoGravConfig")]
            public bool gravGlobe = true;
            [Label("$Mods.FargowiltasSouls.MasoPump")]
            public bool pumpCape = true;
            [Label("$Mods.FargowiltasSouls.MasoFlockoConfig")]
            public bool flockoMinion = true;
            [Label("$Mods.FargowiltasSouls.MasoUfoConfig")]
            public bool ufoMinion = true;
            [Label("$Mods.FargowiltasSouls.MasoTrueEyeConfig")]
            public bool trueEoc = true;
            //chalice of the moon
            [Label("$Mods.FargowiltasSouls.MasoCelestConfig")]
            public bool celestRune = true;
            [Label("$Mods.FargowiltasSouls.MasoPlantConfig")]
            public bool plantMinion = true;
            [Label("$Mods.FargowiltasSouls.MasoGolemConfig")]
            public bool golemGround = true;
            [Label("$Mods.FargowiltasSouls.MasoVisionConfig")]
            public bool ancientVision = true;
            [Label("$Mods.FargowiltasSouls.MasoCultistConfig")]
            public bool cultMinion = true;
            [Label("$Mods.FargowiltasSouls.MasoFishronConfig")]
            public bool fishMinion = true;
            //lump of flesh
            [Label("$Mods.FargowiltasSouls.MasoPugentConfig")]
            public bool pungentEye = true;
            //mutant armor
            [Label("$Mods.FargowiltasSouls.MasoAbomConfig")]
            public bool abomMinion = true;
            [Label("$Mods.FargowiltasSouls.MasoRingConfig")]
            public bool ringMinion = true;
            //other
            [Label("$Mods.FargowiltasSouls.MasoSpikeConfig")]
            public bool spikeHit = true;
            [Label("$Mods.FargowiltasSouls.MasoIconConfig")]
            public bool sinIcon = true;
            public class WalletMenu
            {
                [Label("$Mods.FargowiltasSouls.WalletWardingConfig")]
                public bool warding = true;
                [Label("$Mods.FargowiltasSouls.WalletViolentConfig")]
                public bool violent = true;
                [Label("$Mods.FargowiltasSouls.WalletQuickConfig")]
                public bool quick = true;
                [Label("$Mods.FargowiltasSouls.WalletLuckyConfig")]
                public bool lucky = true;
                [Label("$Mods.FargowiltasSouls.WalletMenacingConfig")]
                public bool menacing = true;
                [Label("$Mods.FargowiltasSouls.WalletLegendaryConfig")]
                public bool legendary = true;
                [Label("$Mods.FargowiltasSouls.WalletUnrealConfig")]
                public bool unreal = true;
                [Label("$Mods.FargowiltasSouls.WalletMythicalConfig")]
                public bool mythical = true;
                [Label("$Mods.FargowiltasSouls.WalletGodlyConfig")]
                public bool godly = true;
                [Label("$Mods.FargowiltasSouls.WalletDemonicConfig")]
                public bool demonic = true;
                [Label("$Mods.FargowiltasSouls.WalletRuthlessConfig")]
                public bool ruthless = true;
                [Label("$Mods.FargowiltasSouls.WalletLightConfig")]
                public bool light = true;
                [Label("$Mods.FargowiltasSouls.WalletDeadlyConfig")]
                public bool deadly = true;
                [Label("$Mods.FargowiltasSouls.WalletRapidConfig")]
                public bool rapid = true;
                public void Change()
                {
                    SoulConfig.Instance.walletToggles["Warding"] = warding;
                    SoulConfig.Instance.walletToggles["Violent"] = violent;
                    SoulConfig.Instance.walletToggles["Quick"] = quick;
                    SoulConfig.Instance.walletToggles["Lucky"] = lucky;
                    SoulConfig.Instance.walletToggles["Menacing"] = menacing;
                    SoulConfig.Instance.walletToggles["Legendary"] = legendary;
                    SoulConfig.Instance.walletToggles["Unreal"] = unreal;
                    SoulConfig.Instance.walletToggles["Mythical"] = mythical;
                    SoulConfig.Instance.walletToggles["Godly"] = godly;
                    SoulConfig.Instance.walletToggles["Demonic"] = demonic;
                    SoulConfig.Instance.walletToggles["Ruthless"] = ruthless;
                    SoulConfig.Instance.walletToggles["Light"] = light;
                    SoulConfig.Instance.walletToggles["Deadly"] = deadly;
                    SoulConfig.Instance.walletToggles["Rapid"] = rapid;
                }
            }

            public void Change()
            {
                SoulConfig.Instance.masoTogDict["Slimy Shield Effects"] = slimeShield;
                SoulConfig.Instance.masoTogDict["Scythes When Dashing"] = eyeScythes;
                SoulConfig.Instance.masoTogDict["Skeletron Arms Minion"] = skeleArms;
                //pure heart
                SoulConfig.Instance.masoTogDict["Tiny Eaters"] = tinyEaters;
                SoulConfig.Instance.masoTogDict["Creeper Shield"] = awwMan;
                //bionomic cluster
                SoulConfig.Instance.masoTogDict["Rainbow Slime Minion"] = rainSlime;
                SoulConfig.Instance.masoTogDict["Frostfireballs"] = frostFire;
                SoulConfig.Instance.masoTogDict["Attacks Spawn Hearts"] = heartAttacks;
                SoulConfig.Instance.masoTogDict["Squeaky Toy On Hit"] = squeakToy;
                SoulConfig.Instance.masoTogDict["Tentacles On Hit"] = pouchTentacles;
                SoulConfig.Instance.masoTogDict["Inflict Clipped Wings"] = clipAttack;
                //dubious circutry
                SoulConfig.Instance.masoTogDict["Inflict Lightning Rod"] = lightRod;
                SoulConfig.Instance.masoTogDict["Probes Minion"] = destroyProbe;
                //heart of the masochist
                SoulConfig.Instance.masoTogDict["Gravity Control"] = gravGlobe;
                SoulConfig.Instance.masoTogDict["Pumpking's Cape Support"] = pumpCape;
                SoulConfig.Instance.masoTogDict["Flocko Minion"] = flockoMinion;
                SoulConfig.Instance.masoTogDict["Saucer Minion"] = ufoMinion;
                SoulConfig.Instance.masoTogDict["True Eyes Minion"] = trueEoc;
                //chalice of the moon
                SoulConfig.Instance.masoTogDict["Celestial Rune Support"] = celestRune;
                SoulConfig.Instance.masoTogDict["Plantera Minion"] = plantMinion;
                SoulConfig.Instance.masoTogDict["Lihzahrd Ground Pound"] = golemGround;
                SoulConfig.Instance.masoTogDict["Ancient Visions On Hit"] = ancientVision;
                SoulConfig.Instance.masoTogDict["Cultist Minion"] = cultMinion;
                SoulConfig.Instance.masoTogDict["Spectral Fishron"] = fishMinion;
                //lump of flesh
                SoulConfig.Instance.masoTogDict["Pungent Eye Minion"] = pungentEye;
                //mutant armor
                SoulConfig.Instance.masoTogDict["Abominationn Minion"] = abomMinion;
                SoulConfig.Instance.masoTogDict["Phantasmal Ring Minion"] = ringMinion;
                //other
                SoulConfig.Instance.masoTogDict["Spiky Balls On Hit"] = spikeHit;
                SoulConfig.Instance.masoTogDict["Sinister Icon"] = sinIcon;


            }
        }
        [SeparatePage]
        public class PetMenu
        {
            [Label("$Mods.FargowiltasSouls.PetCatConfig")]
            public bool bCat = true;
            [Label("$Mods.FargowiltasSouls.PetCubeConfig")]
            public bool cCube = true;
            [Label("$Mods.FargowiltasSouls.PetCurseSapConfig")]
            public bool cSapling = true;
            [Label("$Mods.FargowiltasSouls.PetDinoConfig")]
            public bool dino = true;
            [Label("$Mods.FargowiltasSouls.PetDragonConfig")]
            public bool dragon = true;
            [Label("$Mods.FargowiltasSouls.PetEaterConfig")]
            public bool eater = true;
            [Label("$Mods.FargowiltasSouls.PetEyeSpringConfig")]
            public bool eSpring = true;
            [Label("$Mods.FargowiltasSouls.PetFaceMonsterConfig")]
            public bool fMonster = true;
            [Label("$Mods.FargowiltasSouls.PetGatoConfig")]
            public bool gato = true;
            [Label("$Mods.FargowiltasSouls.PetHornetConfig")]
            public bool hornet = true;
            [Label("$Mods.FargowiltasSouls.PetLizardConfig")]
            public bool lizard = true;
            [Label("$Mods.FargowiltasSouls.PetMinitaurConfig")]
            public bool mMinitaur = true;
            [Label("$Mods.FargowiltasSouls.PetParrotConfig")]
            public bool parrot = true;
            [Label("$Mods.FargowiltasSouls.PetPenguinConfig")]
            public bool penguin = true;
            [Label("$Mods.FargowiltasSouls.PetPupConfig")]
            public bool puppy = true;
            [Label("$Mods.FargowiltasSouls.PetSeedConfig")]
            public bool seedling = true;
            [Label("$Mods.FargowiltasSouls.PetDGConfig")]
            public bool dGuard = true;
            [Label("$Mods.FargowiltasSouls.PetSnowmanConfig")]
            public bool snowman = true;
            [Label("$Mods.FargowiltasSouls.PetSpiderConfig")]
            public bool spider = true;
            [Label("$Mods.FargowiltasSouls.PetSquashConfig")]
            public bool squash = true;
            [Label("$Mods.FargowiltasSouls.PetTikiConfig")]
            public bool tiki = true;
            [Label("$Mods.FargowiltasSouls.PetShroomConfig")]
            public bool truffle = true;
            [Label("$Mods.FargowiltasSouls.PetTurtleConfig")]
            public bool turtle = true;
            [Label("$Mods.FargowiltasSouls.PetZephyrConfig")]
            public bool zFish = true;
            //LIGHT PETS
            [Label("$Mods.FargowiltasSouls.PetHeartConfig")]
            public bool cHeart = true;
            [Label("$Mods.FargowiltasSouls.PetNaviConfig")]
            public bool fairy = true;
            [Label("$Mods.FargowiltasSouls.PetFlickerConfig")]
            public bool flick = true;
            [Label("$Mods.FargowiltasSouls.PetLanturnConfig")]
            public bool mLanturn = true;
            [Label("$Mods.FargowiltasSouls.PetOrbConfig")]
            public bool sOrb = true;
            [Label("$Mods.FargowiltasSouls.PetSuspEyeConfig")]
            public bool sEye = true;
            [Label("$Mods.FargowiltasSouls.PetWispConfig")]
            public bool wisp = true;
            public void Change()
            {
                SoulConfig.Instance.petToggles["Black Cat Pet"] = bCat;
                SoulConfig.Instance.petToggles["Companion Cube Pet"] = cCube;
                SoulConfig.Instance.petToggles["Cursed Sapling Pet"] = cSapling;
                SoulConfig.Instance.petToggles["Dino Pet"] = dino;
                SoulConfig.Instance.petToggles["Dragon Pet"] = dragon;
                SoulConfig.Instance.petToggles["Eater Pet"] = eater;
                SoulConfig.Instance.petToggles["Eye Spring Pet"] = eSpring;
                SoulConfig.Instance.petToggles["Face Monster Pet"] = fMonster;
                SoulConfig.Instance.petToggles["Gato Pet"] = gato;
                SoulConfig.Instance.petToggles["Hornet Pet"] = hornet;
                SoulConfig.Instance.petToggles["Lizard Pet"] = lizard;
                SoulConfig.Instance.petToggles["Mini Minotaur Pet"] = mMinitaur;
                SoulConfig.Instance.petToggles["Parrot Pet"] = parrot;
                SoulConfig.Instance.petToggles["Penguin Pet"] = penguin;
                SoulConfig.Instance.petToggles["Puppy Pet"] = puppy;
                SoulConfig.Instance.petToggles["Seedling Pet"] = seedling;
                SoulConfig.Instance.petToggles["Skeletron Pet"] = dGuard;
                SoulConfig.Instance.petToggles["Snowman Pet"] = snowman;
                SoulConfig.Instance.petToggles["Spider Pet"] = spider;
                SoulConfig.Instance.petToggles["Squashling Pet"] = squash;
                SoulConfig.Instance.petToggles["Tiki Pet"] = tiki;
                SoulConfig.Instance.petToggles["Truffle Pet"] = truffle;
                SoulConfig.Instance.petToggles["Turtle Pet"] = turtle;
                SoulConfig.Instance.petToggles["Zephyr Fish Pet"] = zFish;
                //LIGHT PETS
                SoulConfig.Instance.petToggles["Crimson Heart Pet"] = cHeart;
                SoulConfig.Instance.petToggles["Fairy Pet"] = fairy;
                SoulConfig.Instance.petToggles["Flickerwick Pet"] = flick;
                SoulConfig.Instance.petToggles["Magic Lantern Pet"] = mLanturn;
                SoulConfig.Instance.petToggles["Shadow Orb Pet"] = sOrb;
                SoulConfig.Instance.petToggles["Suspicious Eye Pet"] = sEye;
                SoulConfig.Instance.petToggles["Wisp Pet"] = wisp;
            }
        }
        public override void OnChanged()
        {

            terrmenu.terenchmenu.Change();
        }
        public override void OnLoaded()
        {
            SoulConfig.Instance = this;
            enchantToggles.Add("Boreal Snowballs", terrmenu.terenchmenu.borealSnow);
            enchantToggles.Add("Ebonwood Shadowflame", terrmenu.terenchmenu.ebonFlame);
            enchantToggles.Add("Mahogany Hook Speed", terrmenu.terenchmenu.mahoganyHook);
            enchantToggles.Add("Palmwood Sentry", terrmenu.terenchmenu.palmSentry);
            enchantToggles.Add("Pearlwood Rainbow", terrmenu.terenchmenu.pearlTrail);
            enchantToggles.Add("Adamantite Projectile Splitting", terrmenu.terenchmenu.adamSplit);
            enchantToggles.Add("Cobalt Shards", terrmenu.terenchmenu.cobaltShards);
            enchantToggles.Add("Mythril Weapon Speed", terrmenu.terenchmenu.mythSpeed);
            enchantToggles.Add("Orichalcum Fireballs", terrmenu.terenchmenu.oriFire);
            enchantToggles.Add("Palladium Healing", terrmenu.terenchmenu.palHeal);
            enchantToggles.Add("Titanium Shadow Dodge", terrmenu.terenchmenu.titDodge);
            enchantToggles.Add("Copper Lightning", terrmenu.terenchmenu.copLight);
            enchantToggles.Add("Iron Magnet", terrmenu.terenchmenu.ironMag);
            enchantToggles.Add("Iron Shield", terrmenu.terenchmenu.ironShield);
            enchantToggles.Add("Tin Crits", terrmenu.terenchmenu.tinCrit);
            enchantToggles.Add("Tungsten Effect", terrmenu.terenchmenu.tung);
            enchantToggles.Add("Gladiator Rain", terrmenu.terenchmenu.gladRain);
            enchantToggles.Add("Gold Lucky Coin", terrmenu.terenchmenu.goldCoin);
            enchantToggles.Add("Red Riding Super Bleed", terrmenu.terenchmenu.redBleed);
            enchantToggles.Add("Valhalla Knockback", terrmenu.terenchmenu.valhalKnock);

            //force of life
            enchantToggles.Add("Beetles", terrmenu.terenchmenu.beetBeetles);
            enchantToggles.Add("Cactus Needles", terrmenu.terenchmenu.cacNeedle);
            enchantToggles.Add("Pumpkin Fire", terrmenu.terenchmenu.pumpFire);
            enchantToggles.Add("Spider Swarm", terrmenu.terenchmenu.spidSwarm);
            enchantToggles.Add("Turtle Shell Buff", terrmenu.terenchmenu.turtShell);
            //force of nature
            enchantToggles.Add("Chlorophyte Leaf Crystal", terrmenu.terenchmenu.chloroCrystal);
            enchantToggles.Add("Frost Icicles", terrmenu.terenchmenu.frostIce);
            enchantToggles.Add("Jungle Spores", terrmenu.terenchmenu.jungleSpores);
            enchantToggles.Add("Molten Inferno Buff", terrmenu.terenchmenu.moltInfern);
            enchantToggles.Add("Shroomite Stealth", terrmenu.terenchmenu.shroomStealth);
            //shadow force
            enchantToggles.Add("Dark Artist Effect", terrmenu.terenchmenu.dArtEffect);
            enchantToggles.Add("Necro Guardian", terrmenu.terenchmenu.necroGuard);
            enchantToggles.Add("Shadow Darkness", terrmenu.terenchmenu.shadowDark);
            enchantToggles.Add("Shinobi Through Walls", terrmenu.terenchmenu.shinWalls);
            enchantToggles.Add("Spooky Scythes", terrmenu.terenchmenu.spookScythe);
            //force of spirit
            enchantToggles.Add("Forbidden Storm", terrmenu.terenchmenu.forbidStorm);
            enchantToggles.Add("Hallowed Enchanted Sword Familiar", terrmenu.terenchmenu.hallowSword);
            enchantToggles.Add("Hallowed Shield", terrmenu.terenchmenu.hallowShield);
            enchantToggles.Add("Silver Sword Familiar", terrmenu.terenchmenu.silverSword);
            enchantToggles.Add("Spectre Orbs", terrmenu.terenchmenu.spectreOrb);
            //force of cosmos
            enchantToggles.Add("Meteor Shower", terrmenu.terenchmenu.meteorShow);
            enchantToggles.Add("Nebula Boosters", terrmenu.terenchmenu.nebulaBoost);
            enchantToggles.Add("Solar Shield", terrmenu.terenchmenu.solarShield);
            enchantToggles.Add("Stardust Guardian", terrmenu.terenchmenu.stardGuard);
            enchantToggles.Add("Vortex Stealth", terrmenu.terenchmenu.vortSneak);
            enchantToggles.Add("Vortex Voids", terrmenu.terenchmenu.vortVoid);

            masoTogDict.Add("Slimy Shield Effects", masomenu.slimeShield);
            masoTogDict.Add("Scythes When Dashing", masomenu.eyeScythes);
            masoTogDict.Add("Skeletron Arms Minion", masomenu.skeleArms);
            //pure heart
            masoTogDict.Add("Tiny Eaters", masomenu.tinyEaters);
            masoTogDict.Add("Creeper Shield", masomenu.awwMan);
            //bionomic cluster
            masoTogDict.Add("Rainbow Slime Minion", masomenu.rainSlime);
            masoTogDict.Add("Frostfireballs", masomenu.frostFire);
            masoTogDict.Add("Attacks Spawn Hearts", masomenu.heartAttacks);
            masoTogDict.Add("Squeaky Toy On Hit", masomenu.squeakToy);
            masoTogDict.Add("Tentacles On Hit", masomenu.pouchTentacles);
            masoTogDict.Add("Inflict Clipped Wings", masomenu.clipAttack);
            //dubious circutry
            masoTogDict.Add("Inflict Lightning Rod", masomenu.lightRod);
            masoTogDict.Add("Probes Minion", masomenu.destroyProbe);
            //heart of the masochist
            masoTogDict.Add("Gravity Control", masomenu.gravGlobe);
            masoTogDict.Add("Pumpking's Cape Support", masomenu.pumpCape);
            masoTogDict.Add("Flocko Minion", masomenu.flockoMinion);
            masoTogDict.Add("Saucer Minion", masomenu.ufoMinion);
            masoTogDict.Add("True Eyes Minion", masomenu.trueEoc);
            //chalice of the moon
            masoTogDict.Add("Celestial Rune Support", masomenu.celestRune);
            masoTogDict.Add("Plantera Minion", masomenu.plantMinion);
            masoTogDict.Add("Lihzahrd Ground Pound", masomenu.golemGround);
            masoTogDict.Add("Ancient Visions On Hit", masomenu.ancientVision);
            masoTogDict.Add("Cultist Minion", masomenu.cultMinion);
            masoTogDict.Add("Spectral Fishron", masomenu.fishMinion);
            //lump of flesh
            masoTogDict.Add("Pungent Eye Minion", masomenu.pungentEye);
            //mutant armor
            masoTogDict.Add("Abominationn Minion", masomenu.abomMinion);
            masoTogDict.Add("Phantasmal Ring Minion", masomenu.ringMinion);
            //other
            masoTogDict.Add("Spiky Balls On Hit", masomenu.spikeHit);
            masoTogDict.Add("Sinister Icon", masomenu.sinIcon);
            petToggles.Add("Black Cat Pet", petmenu.bCat);
            petToggles.Add("Companion Cube Pet", petmenu.cCube);
            petToggles.Add("Cursed Sapling Pet", petmenu.cSapling);
            petToggles.Add("Dino Pet", petmenu.dino);
            petToggles.Add("Dragon Pet", petmenu.dragon);
            petToggles.Add("Eater Pet", petmenu.eater);
            petToggles.Add("Eye Spring Pet", petmenu.eSpring);
            petToggles.Add("Face Monster Pet", petmenu.fMonster);
            petToggles.Add("Gato Pet", petmenu.gato);
            petToggles.Add("Hornet Pet", petmenu.hornet);
            petToggles.Add("Lizard Pet", petmenu.lizard);
            petToggles.Add("Mini Minotaur Pet", petmenu.mMinitaur);
            petToggles.Add("Parrot Pet", petmenu.parrot);
            petToggles.Add("Penguin Pet", petmenu.penguin);
            petToggles.Add("Puppy Pet", petmenu.puppy);
            petToggles.Add("Seedling Pet", petmenu.seedling);
            petToggles.Add("Skeletron Pet", petmenu.dGuard);
            petToggles.Add("Snowman Pet", petmenu.snowman);
            petToggles.Add("Spider Pet", petmenu.spider);
            petToggles.Add("Squashling Pet", petmenu.squash);
            petToggles.Add("Tiki Pet", petmenu.tiki);
            petToggles.Add("Truffle Pet", petmenu.truffle);
            petToggles.Add("Turtle Pet", petmenu.turtle);
            petToggles.Add("Zephyr Fish Pet", petmenu.zFish);
            //LIGHT PETS
            petToggles.Add("Crimson Heart Pet", petmenu.cHeart);
            petToggles.Add("Fairy Pet", petmenu.fairy);
            petToggles.Add("Flickerwick Pet", petmenu.flick);
            petToggles.Add("Magic Lantern Pet", petmenu.mLanturn);
            petToggles.Add("Shadow Orb Pet", petmenu.sOrb);
            petToggles.Add("Suspicious Eye Pet", petmenu.sEye);
            petToggles.Add("Wisp Pet", petmenu.wisp);
            walletToggles.Add("Warding", masomenu.wallet.warding);
            walletToggles.Add("Violent", masomenu.wallet.violent);
            walletToggles.Add("Quick", masomenu.wallet.quick);
            walletToggles.Add("Lucky", masomenu.wallet.lucky);
            walletToggles.Add("Menacing", masomenu.wallet.menacing);
            walletToggles.Add("Legendary", masomenu.wallet.legendary);
            walletToggles.Add("Unreal", masomenu.wallet.unreal);
            walletToggles.Add("Mythical", masomenu.wallet.mythical);
            walletToggles.Add("Godly", masomenu.wallet.godly);
            walletToggles.Add("Demonic", masomenu.wallet.demonic);
            walletToggles.Add("Ruthless", masomenu.wallet.ruthless);
            walletToggles.Add("Light", masomenu.wallet.light);
            walletToggles.Add("Deadly", masomenu.wallet.deadly);
            walletToggles.Add("Rapid", masomenu.wallet.rapid);

            //Universe
            soulToggles.Add("Melee Speed", terrmenu.soulmenu.gladSpeed);
            soulToggles.Add("Sniper Scope", terrmenu.soulmenu.sharpSniper);
            soulToggles.Add("Universe Attack Speed", terrmenu.soulmenu.universeSpeed);
            //dimensions
            soulToggles.Add("Mining Hunter Buff", terrmenu.soulmenu.mineHunt);
            soulToggles.Add("Mining Dangersense Buff", terrmenu.soulmenu.mineDanger);
            soulToggles.Add("Mining Spelunker Buff", terrmenu.soulmenu.mineSpelunk);
            soulToggles.Add("Mining Shine Buff", terrmenu.soulmenu.mineShine);
            soulToggles.Add("Builder Mode", terrmenu.soulmenu.worldBuild);
            soulToggles.Add("Spore Sac", terrmenu.soulmenu.colSpore);
            soulToggles.Add("Stars On Hit", terrmenu.soulmenu.colStar);
            soulToggles.Add("Bees On Hit", terrmenu.soulmenu.colBee);
            soulToggles.Add("Supersonic Speed Boosts", terrmenu.soulmenu.supersonicSpeed);
            //idk 
            soulToggles.Add("Eternity Stacking", terrmenu.soulmenu.eternityStack);




            //enchantToggles.Add("Boreal Snowballs", terrmenu.terenchmenu.borealsnow);

        }
    public bool GetValue(string input)
        {
            if(Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().MutantPresence)
                {
                return false;
            }
            bool tryget;
            bool oooout;
            if(enchantToggles.TryGetValue(input, out oooout))
            {
                return oooout;
            }
            if (petToggles.TryGetValue(input, out oooout))
            {
                return oooout;
            }
            if (soulToggles.TryGetValue(input, out oooout))
            {
                return oooout;
            }
            if (walletToggles.TryGetValue(input, out oooout))
            {
                return oooout;
            }
            if (masoTogDict.TryGetValue(input, out oooout))
            {
                return oooout;
            }
            return false;
        }
    }
}
