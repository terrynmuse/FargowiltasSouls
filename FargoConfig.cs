
using Newtonsoft.Json;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace FargowiltasSouls
{
    class SoulConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;
        public static SoulConfig Instance;

        [Label("Terraria")]
        public TerraMenu terrmenu = new TerraMenu();
        [JsonIgnore]
        public Dictionary<string, bool> enchantToggles = new Dictionary<string, bool>();
        [JsonIgnore]
        public Dictionary<string, bool> soulToggles = new Dictionary<string, bool>();

        [Label("Masochist Mode")]
        public MasoMenu masomenu = new MasoMenu();
        [JsonIgnore]
        public Dictionary<string, bool> masoTogDict = new Dictionary<string, bool>();
        [JsonIgnore]
        public Dictionary<string, bool> walletToggles = new Dictionary<string, bool>();

        [Label("Pets")]
        public PetMenu petmenu = new PetMenu();
        [JsonIgnore]
        public Dictionary<string, bool> petToggles = new Dictionary<string, bool>();

        [Label("Thorium")]
        public ThorMenu thoriummenu = new ThorMenu();
        [JsonIgnore]
        public Dictionary<string, bool> thoriumToggles = new Dictionary<string, bool>();

        [Label("Calamity")]
        public CalamMenu calamenu = new CalamMenu();
        [JsonIgnore]
        public Dictionary<string, bool> calamityToggles = new Dictionary<string, bool>();

        [SeparatePage]
        public class TerraMenu
        {
            [Label("Enchants")]
            public TerEncMenu terenchmenu = new TerEncMenu();
            [Label("Souls")]
            public SoulMenu soulmenu = new SoulMenu();

            [SeparatePage]
            public class TerEncMenu
            {
                [Header("$Mods.FargowiltasSouls.WoodHeader")]
                [Label("$Mods.FargowiltasSouls.BorealConfig")]
                public bool borealSnow = true;
                [Label("$Mods.FargowiltasSouls.EbonConfig")]
                public bool ebonFlame = true;
                [Label("$Mods.FargowiltasSouls.ShadeConfig")]
                public bool shadeBlood = true;
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
                [Label("$Mods.FargowiltasSouls.CthulhuShield")]
                public bool cthulhuShield = true;
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

                [Header("$Mods.FargowiltasSouls.NatureHeader")]
                [Label("$Mods.FargowiltasSouls.ChlorophyteConfig")]
                public bool chloroCrystal = true;
                [Label("$Mods.FargowiltasSouls.CrimsonConfig")]
                public bool crimsonRegen = true;
                [Label("$Mods.FargowiltasSouls.FrostConfig")]
                public bool frostIce = true;
                [Label("$Mods.FargowiltasSouls.JungleConfig")]
                public bool jungleSpores = true;
                [Label("$Mods.FargowiltasSouls.MoltenConfig")]
                public bool moltInfern = true;
                [Label("$Mods.FargowiltasSouls.ShroomiteConfig")]
                public bool shroomStealth = true;

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
                [Label("$Mods.FargowiltasSouls.TikiConfig")]
                public bool tikiMinion = true;

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
                    SoulConfig.Instance.enchantToggles["Blood Geyser On Hit"] = shadeBlood;
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
                    SoulConfig.Instance.enchantToggles["Shield of Cthulhu"] = cthulhuShield;
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
                    SoulConfig.Instance.enchantToggles["Crimson Regen"] = crimsonRegen;
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
                    SoulConfig.Instance.enchantToggles["Tiki Minions"] = tikiMinion;
                    SoulConfig.Instance.enchantToggles["Spectre Orbs"] = spectreOrb;
                    //force of cosmos
                    SoulConfig.Instance.enchantToggles["Meteor Shower"] = meteorShow;
                    SoulConfig.Instance.enchantToggles["Nebula Boosters"] = nebulaBoost;
                    SoulConfig.Instance.enchantToggles["Solar Shield"] = solarShield;
                    SoulConfig.Instance.enchantToggles["Stardust Guardian"] = stardGuard;
                    SoulConfig.Instance.enchantToggles["Vortex Stealth"] = vortSneak;
                    SoulConfig.Instance.enchantToggles["Vortex Voids"] = vortVoid;
                }
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
                [Label("$Mods.FargowiltasSouls.EternityConfig")]
                public bool eternityStack = true;

                public void Change()
                {
                    SoulConfig.Instance.soulToggles["Melee Speed"] = gladSpeed;
                    SoulConfig.Instance.soulToggles["Sniper Scope"] = sharpSniper;
                    SoulConfig.Instance.soulToggles["Universe Attack Speed"] = universeSpeed;
                    SoulConfig.Instance.soulToggles["Mining Hunter Buff"] = mineHunt;
                    SoulConfig.Instance.soulToggles["Mining Dangersense Buff"] = mineDanger;
                    SoulConfig.Instance.soulToggles["Mining Spelunker Buff"] = mineSpelunk;
                    SoulConfig.Instance.soulToggles["Mining Shine Buff"] = mineShine;
                    SoulConfig.Instance.soulToggles["Builder Mode"] = worldBuild;
                    SoulConfig.Instance.soulToggles["Spore Sac"] = colSpore;
                    SoulConfig.Instance.soulToggles["Stars On Hit"] = colStar;
                    SoulConfig.Instance.soulToggles["Bees On Hit"] = colBee;
                    SoulConfig.Instance.soulToggles["Supersonic Speed Boosts"] = supersonicSpeed;
                    SoulConfig.Instance.soulToggles["Eternity Stacking"] = eternityStack;
                }
            }
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
            //dubious circutry
            [Label("$Mods.FargowiltasSouls.MasoLightningConfig")]
            public bool lightRod = true;
            [Label("$Mods.FargowiltasSouls.MasoProbeConfig")]
            public bool destroyProbe = true;
            //heart of the masochist
            [Label("$Mods.FargowiltasSouls.MasoGravConfig")]
            public bool gravGlobe = true;
            [Label("$Mods.FargowiltasSouls.MasoGrav2Config")]
            public bool gravGlobe2 = true;
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
            [Label("$Mods.FargowiltasSouls.MasoBossRecolors")]
            public bool bossRecolors = true;

            [Label("Security wallet")]
            public WalletMenu wallet = new WalletMenu();

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
                SoulConfig.Instance.masoTogDict["Stabilized Gravity"] = gravGlobe2;
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
                SoulConfig.Instance.masoTogDict["Boss Recolors (Restart to use)"] = bossRecolors;
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

        [SeparatePage]
        public class ThorMenu
        {
            [Label("$Mods.FargowiltasSouls.ThoriumAirWalkersConfig")]
            public bool airWalkers = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCrystalScorpionConfig")]
            public bool crystalScorpion = true;
            [Label("$Mods.FargowiltasSouls.ThoriumYumasPendantConfig")]
            public bool yumasPendant = true;
            [Label("$Mods.FargowiltasSouls.ThoriumHeadMirrorConfig")]
            public bool headMirror = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCelestialAuraConfig")]
            public bool celestialAura = true;
            [Label("$Mods.FargowiltasSouls.ThoriumAscensionStatueConfig")]
            public bool ascensionStatue = true;
            [Label("$Mods.FargowiltasSouls.ThoriumManaBootsConfig")]
            public bool manaBoots = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBronzeLightningConfig")]
            public bool bronzeLightning = true;
            [Label("$Mods.FargowiltasSouls.ThoriumIllumiteMissileConfig")]
            public bool illumiteMissile = true;
            [Label("$Mods.FargowiltasSouls.ThoriumJesterBellConfig")]
            public bool jesterBell = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBeholderEyeConfig")]
            public bool beholderEye = true;
            [Label("$Mods.FargowiltasSouls.ThoriumTerrariumSpiritsConfig")]
            public bool terrariumSpirits = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCrietzConfig")]
            public bool crietz = true;
            [Label("$Mods.FargowiltasSouls.ThoriumYewCritsConfig")]
            public bool yewCrits = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCryoDamageConfig")]
            public bool cryoDamage = true;
            [Label("$Mods.FargowiltasSouls.ThoriumWhiteDwarfConfig")]
            public bool whiteDwarf = true;
            [Label("$Mods.FargowiltasSouls.ThoriumTideFoamConfig")]
            public bool tideFoam = true;
            [Label("$Mods.FargowiltasSouls.ThoriumWhisperingTentaclesConfig")]
            public bool whisperingTentacles = true;
            [Label("$Mods.FargowiltasSouls.ThoriumIcyBarrierConfig")]
            public bool icyBarrier = true;
            [Label("$Mods.FargowiltasSouls.ThoriumPlagueFlaskConfig")]
            public bool plagueFlask = true;
            [Label("$Mods.FargowiltasSouls.ThoriumTideGlobulesConfig")]
            public bool tideGlobules = true;
            [Label("$Mods.FargowiltasSouls.ThoriumTideDaggersConfig")]
            public bool tideDaggers = true;
            [Label("$Mods.FargowiltasSouls.ThoriumFolvAuraConfig")]
            public bool folvAura = true;
            [Label("$Mods.FargowiltasSouls.ThoriumFolvBoltsConfig")]
            public bool folvBolts = true;
            [Label("$Mods.FargowiltasSouls.ThoriumVampireGlandConfig")]
            public bool vampireGland = true;
            [Label("$Mods.FargowiltasSouls.ThoriumFleshDropsConfig")]
            public bool fleshDrops = true;
            [Label("$Mods.FargowiltasSouls.ThoriumDragonFlamesConfig")]
            public bool dragonFlames = true;
            [Label("$Mods.FargowiltasSouls.ThoriumHarbingerOverchargeConfig")]
            public bool harbingerOvercharge = true;
            [Label("$Mods.FargowiltasSouls.ThoriumAssassinDamageConfig")]
            public bool assassinDamage = true;
            [Label("$Mods.FargowiltasSouls.ThoriumpyromancerBurstsConfig")]
            public bool pyromancerBursts = true;
            [Label("$Mods.FargowiltasSouls.ThoriumConduitShieldConfig")]
            public bool conduitShield = true;
            [Label("$Mods.FargowiltasSouls.ThoriumIncandescentSparkConfig")]
            public bool incandescentSpark = true;
            [Label("$Mods.FargowiltasSouls.ThoriumGreedyMagnetConfig")]
            public bool greedyMagnet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCyberStatesConfig")]
            public bool cyberStates = true;
            [Label("$Mods.FargowiltasSouls.ThoriumMetronomeConfig")]
            public bool metronome = true;
            [Label("$Mods.FargowiltasSouls.ThoriumMixTapeConfig")]
            public bool mixTape = true;
            [Label("$Mods.FargowiltasSouls.ThoriumLodestoneConfig")]
            public bool lodestoneResist = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBiotechProbeConfig")]
            public bool biotechProbe = true;
            [Label("$Mods.FargowiltasSouls.ThoriumProofAvariceConfig")]
            public bool proofAvarice = true;
            [Label("$Mods.FargowiltasSouls.ThoriumSlagStompersConfig")]
            public bool slagStompers = true;
            [Label("$Mods.FargowiltasSouls.ThoriumSpringStepsConfig")]
            public bool springSteps = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBerserkerConfig")]
            public bool berserker = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBeeBootiesConfig")]
            public bool beeBooties = true;
            [Label("$Mods.FargowiltasSouls.ThoriumGhastlyCarapaceConfig")]
            public bool ghastlyCarapace = true;
            [Label("$Mods.FargowiltasSouls.ThoriumSpiritWispsConfig")]
            public bool spiritWisps = true;
            [Label("$Mods.FargowiltasSouls.ThoriumWarlockWispsConfig")]
            public bool warlockWisps = true;
            [Label("$Mods.FargowiltasSouls.ThoriumDreadConfig")]
            public bool dreadSpeed = true;
            [Label("$Mods.FargowiltasSouls.ThoriumDiverConfig")]
            public bool divers = true;
            [Label("$Mods.FargowiltasSouls.ThoriumDemonBloodConfig")]
            public bool demonBlood = true;
            [Label("$Mods.FargowiltasSouls.ThoriumDevilMinionConfig")]
            public bool devilMinion = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCherubMinionConfig")]
            public bool cherubMinion = true;
            [Label("$Mods.FargowiltasSouls.ThoriumSaplingMinionConfig")]
            public bool saplingMinion = true;

            [Label("$Mods.FargowiltasSouls.ThoriumOmegaPetConfig")]
            public bool omegaPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumIFOPetConfig")]
            public bool ifoPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBioFeederPetConfig")]
            public bool bioFeederPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBlisterPetConfig")]
            public bool blisterPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumWyvernPetConfig")]
            public bool wyvernPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumLanternPetConfig")]
            public bool lanternPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumBoxPetConfig")]
            public bool boxPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumSpiritPetConfig")]
            public bool spiritPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumGoatPetConfig")]
            public bool goatPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumOwlPetConfig")]
            public bool owlPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumJellyfishPetConfig")]
            public bool jellyfishPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumMooglePetConfig")]
            public bool mooglePet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumMaidPetConfig")]
            public bool maidPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumSlimePetConfig")]
            public bool slimePet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumGlitterPetConfig")]
            public bool glitterPet = true;
            [Label("$Mods.FargowiltasSouls.ThoriumCoinPetConfig")]
            public bool coinPet = true;

            public void Change()
            {
                SoulConfig.Instance.thoriumToggles["Air Walkers"] = airWalkers;
                SoulConfig.Instance.thoriumToggles["Crystal Scorpion"] = crystalScorpion;
                SoulConfig.Instance.thoriumToggles["Yuma's Pendant"] = yumasPendant;
                SoulConfig.Instance.thoriumToggles["Head Mirror"] = headMirror;
                SoulConfig.Instance.thoriumToggles["Celestial Aura"] = celestialAura;
                SoulConfig.Instance.thoriumToggles["Ascension Statuette"] = ascensionStatue;
                SoulConfig.Instance.thoriumToggles["Mana-Charged Rocketeers"] = manaBoots;
                SoulConfig.Instance.thoriumToggles["Bronze Lightning"] = bronzeLightning;
                SoulConfig.Instance.thoriumToggles["Illumite Missile"] = illumiteMissile;
                SoulConfig.Instance.thoriumToggles["Jester Bell"] = jesterBell;
                SoulConfig.Instance.thoriumToggles["Eye of the Beholder"] = beholderEye;
                SoulConfig.Instance.thoriumToggles["Terrarium Spirits"] = terrariumSpirits;
                SoulConfig.Instance.thoriumToggles["Crietz"] = crietz;
                SoulConfig.Instance.thoriumToggles["Yew Wood Crits"] = yewCrits;
                SoulConfig.Instance.thoriumToggles["Cryo-Magus Damage"] = cryoDamage;
                SoulConfig.Instance.thoriumToggles["White Dwarf Flares"] = whiteDwarf;
                SoulConfig.Instance.thoriumToggles["Tide Hunter Foam"] = tideFoam;
                SoulConfig.Instance.thoriumToggles["Whispering Tentacles"] = whisperingTentacles;
                SoulConfig.Instance.thoriumToggles["Icy Barrier"] = icyBarrier;
                SoulConfig.Instance.thoriumToggles["Plague Lord's Flask"] = plagueFlask;
                SoulConfig.Instance.thoriumToggles["Tide Turner Globules"] = tideGlobules;
                SoulConfig.Instance.thoriumToggles["Tide Turner Daggers"] = tideDaggers;
                SoulConfig.Instance.thoriumToggles["Folv's Aura"] = folvAura;
                SoulConfig.Instance.thoriumToggles["Folv's Bolts"] = folvBolts;
                SoulConfig.Instance.thoriumToggles["Vampire Gland"] = vampireGland;
                SoulConfig.Instance.thoriumToggles["Flesh Drops"] = fleshDrops;
                SoulConfig.Instance.thoriumToggles["Dragon Flames"] = dragonFlames;
                SoulConfig.Instance.thoriumToggles["Harbinger Overcharge"] = harbingerOvercharge;
                SoulConfig.Instance.thoriumToggles["Assassin Damage"] = assassinDamage;
                SoulConfig.Instance.thoriumToggles["Pyromancer Bursts"] = pyromancerBursts;
                SoulConfig.Instance.thoriumToggles["Conduit Shield"] = conduitShield;
                SoulConfig.Instance.thoriumToggles["Incandescent Spark"] = incandescentSpark;
                SoulConfig.Instance.thoriumToggles["Greedy Magnet"] = greedyMagnet;
                SoulConfig.Instance.thoriumToggles["Cyber Punk States"] = cyberStates;
                SoulConfig.Instance.thoriumToggles["Metronome"] = metronome;
                SoulConfig.Instance.thoriumToggles["Mix Tape"] = mixTape;
                SoulConfig.Instance.thoriumToggles["Lodestone Resistance"] = lodestoneResist;
                SoulConfig.Instance.thoriumToggles["Biotech Probe"] = biotechProbe;
                SoulConfig.Instance.thoriumToggles["Proof of Avarice"] = proofAvarice;
                SoulConfig.Instance.thoriumToggles["Slag Stompers"] = slagStompers;
                SoulConfig.Instance.thoriumToggles["Spring Steps"] = springSteps;
                SoulConfig.Instance.thoriumToggles["Berserker Effect"] = berserker;
                SoulConfig.Instance.thoriumToggles["Bee Booties"] = beeBooties;
                SoulConfig.Instance.thoriumToggles["Ghastly Carapace"] = ghastlyCarapace;
                SoulConfig.Instance.thoriumToggles["Spirit Trapper Wisps"] = spiritWisps;
                SoulConfig.Instance.thoriumToggles["Warlock Wisps"] = warlockWisps;
                SoulConfig.Instance.thoriumToggles["Dread Speed"] = dreadSpeed;
                SoulConfig.Instance.thoriumToggles["Spawn Divers"] = divers;
                SoulConfig.Instance.thoriumToggles["Demon Blood Effect"] = demonBlood;

                SoulConfig.Instance.thoriumToggles["Li'l Devil Minion"] = devilMinion;
                SoulConfig.Instance.thoriumToggles["Li'l Cherub Minion"] = cherubMinion;
                SoulConfig.Instance.thoriumToggles["Sapling Minion"] = saplingMinion;

                SoulConfig.Instance.thoriumToggles["Omega Pet"] = omegaPet;
                SoulConfig.Instance.thoriumToggles["I.F.O. Pet"] = ifoPet;
                SoulConfig.Instance.thoriumToggles["Bio-Feeder Pet"] = bioFeederPet;
                SoulConfig.Instance.thoriumToggles["Blister Pet"] = blisterPet;
                SoulConfig.Instance.thoriumToggles["Wyvern Pet"] = wyvernPet;
                SoulConfig.Instance.thoriumToggles["Inspiring Lantern Pet"] = lanternPet;
                SoulConfig.Instance.thoriumToggles["Lock Box Pet"] = boxPet;
                SoulConfig.Instance.thoriumToggles["Life Spirit Pet"] = spiritPet;
                SoulConfig.Instance.thoriumToggles["Holy Goat Pet"] = goatPet;
                SoulConfig.Instance.thoriumToggles["Owl Pet"] = owlPet;
                SoulConfig.Instance.thoriumToggles["Jellyfish Pet"] = jellyfishPet;
                SoulConfig.Instance.thoriumToggles["Moogle Pet"] = mooglePet;
                SoulConfig.Instance.thoriumToggles["Maid Pet"] = maidPet;
                SoulConfig.Instance.thoriumToggles["Pink Slime Pet"] = slimePet;
                SoulConfig.Instance.thoriumToggles["Glitter Pet"] = glitterPet;
                SoulConfig.Instance.thoriumToggles["Coin Bag Pet"] = coinPet;
            }
        }

        [SeparatePage]
        public class CalamMenu
        {
            [Label("$Mods.FargowiltasSouls.CalamityUrchinConfig")]
            public bool urchin = true;
            [Label("$Mods.FargowiltasSouls.CalamityProfanedArtifactConfig")]
            public bool profanedSoulArtifact = true;
            [Label("$Mods.FargowiltasSouls.CalamitySlimeMinionConfig")]
            public bool slimeMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityReaverMinionConfig")]
            public bool reaverMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityOmegaTentaclesConfig")]
            public bool omegaTentacles = true;
            [Label("$Mods.FargowiltasSouls.CalamitySilvaMinionConfig")]
            public bool silvaMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityGodlyArtifactConfig")]
            public bool godlySoulArtifact = true;
            [Label("$Mods.FargowiltasSouls.CalamityMechwormMinionConfig")]
            public bool mechwormMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityNebulousCoreConfig")]
            public bool nebulousCore = true;
            [Label("$Mods.FargowiltasSouls.CalamityDevilMinionConfig")]
            public bool devilMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityPermafrostPotionConfig")]
            public bool permafrostPotion = true;
            [Label("$Mods.FargowiltasSouls.CalamityDaedalusMinionConfig")]
            public bool daedalusMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityPolterMinesConfig")]
            public bool polterMines = true;
            [Label("$Mods.FargowiltasSouls.CalamityPlagueHiveConfig")]
            public bool plagueHive = true;
            [Label("$Mods.FargowiltasSouls.CalamityChaosMinionConfig")]
            public bool chaosMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityValkyrieMinionConfig")]
            public bool valkyrieMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityYharimGiftConfig")]
            public bool yharimGift = true;
            [Label("$Mods.FargowiltasSouls.CalamityFungalMinionConfig")]
            public bool fungalMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityWaifuMinionsConfig")]
            public bool waifuMinions = true;
            [Label("$Mods.FargowiltasSouls.CalamityShellfishMinionConfig")]
            public bool shellfishMinion = true;
            [Label("$Mods.FargowiltasSouls.CalamityAmidiasPendantConfig")]
            public bool amidiasPendant = true;
            [Label("$Mods.FargowiltasSouls.CalamityGiantPearlConfig")]
            public bool giantPearl = true;
            [Label("$Mods.FargowiltasSouls.CalamityPoisonSeawaterConfig")]
            public bool poisonSeawater = true;
            [Label("$Mods.FargowiltasSouls.CalamityDaedalusEffectsConfig")]
            public bool daedalusEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityReaverEffectsConfig")]
            public bool reaverEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityAstralStarsConfig")]
            public bool astralStars = true;
            [Label("$Mods.FargowiltasSouls.CalamityAtaxiaEffectsConfig")]
            public bool ataxiaEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityXerocEffectsConfig")]
            public bool xerocEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityTarragonEffectsConfig")]
            public bool tarragonEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityBloodflareEffectsConfig")]
            public bool bloodflareEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityGodSlayerEffectsConfig")]
            public bool godSlayerEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamitySilvaEffectsConfig")]
            public bool silvaEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityAuricEffectsConfig")]
            public bool auricEffects = true;
            [Label("$Mods.FargowiltasSouls.CalamityElementalQuiverConfig")]
            public bool elementalQuiver = true;
            [Label("$Mods.FargowiltasSouls.CalamityLuxorGiftConfig")]
            public bool luxorGift = true;
            [Label("$Mods.FargowiltasSouls.CalamityGladiatorLocketConfig")]
            public bool gladiatorLocket = true;
            [Label("$Mods.FargowiltasSouls.CalamityUnstablePrismConfig")]
            public bool unstablePrism = true;
            [Label("$Mods.FargowiltasSouls.CalamityRegeneratorConfig")]
            public bool regenerator = true;
            [Label("$Mods.FargowiltasSouls.CalamityDivingSuitConfig")]
            public bool divingSuit = true;

            [Label("$Mods.FargowiltasSouls.CalamityKendraConfig")]
            public bool kendraPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityPerforatorConfig")]
            public bool perforatorPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityBearConfig")]
            public bool bearPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityThirdSageConfig")]
            public bool thirdSagePet = true;
            [Label("$Mods.FargowiltasSouls.CalamityBrimlingConfig")]
            public bool brimlingPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityDannyConfig")]
            public bool dannyPet = true;
            [Label("$Mods.FargowiltasSouls.CalamitySirenConfig")]
            public bool sirenPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityChibiiConfig")]
            public bool chibiiPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityAkatoConfig")]
            public bool akatoPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityFoxConfig")]
            public bool foxPet = true;
            [Label("$Mods.FargowiltasSouls.CalamityLeviConfig")]
            public bool leviPet = true;

            public void Change()
            {
                SoulConfig.Instance.calamityToggles["Victide Sea Urchin"] = urchin;
                SoulConfig.Instance.calamityToggles["Profaned Soul Artifact"] = profanedSoulArtifact;
                SoulConfig.Instance.calamityToggles["Slime God Minion"] = slimeMinion;
                SoulConfig.Instance.calamityToggles["Reaver Orb Minion"] = reaverMinion;
                SoulConfig.Instance.calamityToggles["Omega Blue Tentacles"] = omegaTentacles;
                SoulConfig.Instance.calamityToggles["Silva Crystal Minion"] = silvaMinion;
                SoulConfig.Instance.calamityToggles["Godly Soul Artifact"] = godlySoulArtifact;
                SoulConfig.Instance.calamityToggles["Mechworm Minion"] = mechwormMinion;
                SoulConfig.Instance.calamityToggles["Nebulous Core"] = nebulousCore;
                SoulConfig.Instance.calamityToggles["Red Devil Minion"] = devilMinion;
                SoulConfig.Instance.calamityToggles["Permafrost's Concoction"] = permafrostPotion;
                SoulConfig.Instance.calamityToggles["Daedalus Crystal Minion"] = daedalusMinion;
                SoulConfig.Instance.calamityToggles["Polterghast Mines"] = polterMines;
                SoulConfig.Instance.calamityToggles["Plague Hive"] = plagueHive;
                SoulConfig.Instance.calamityToggles["Chaos Spirit Minion"] = chaosMinion;
                SoulConfig.Instance.calamityToggles["Valkyrie Minion"] = valkyrieMinion;
                SoulConfig.Instance.calamityToggles["Yharim's Gift"] = yharimGift;
                SoulConfig.Instance.calamityToggles["Fungal Clump Minion"] = fungalMinion;
                SoulConfig.Instance.calamityToggles["Elemental Waifus"] = waifuMinions;
                SoulConfig.Instance.calamityToggles["Shellfish Minions"] = shellfishMinion;
                SoulConfig.Instance.calamityToggles["Amidias' Pendant"] = amidiasPendant;
                SoulConfig.Instance.calamityToggles["Giant Pearl"] = giantPearl;
                SoulConfig.Instance.calamityToggles["Poisonous Sea Water"] = poisonSeawater;
                SoulConfig.Instance.calamityToggles["Daedalus Effects"] = daedalusEffects;
                SoulConfig.Instance.calamityToggles["Reaver Effects"] = reaverEffects;
                SoulConfig.Instance.calamityToggles["Astral Stars"] = astralStars;
                SoulConfig.Instance.calamityToggles["Ataxia Effects"] = ataxiaEffects;
                SoulConfig.Instance.calamityToggles["Xeroc Effects"] = xerocEffects;
                SoulConfig.Instance.calamityToggles["Tarragon Effects"] = tarragonEffects;
                SoulConfig.Instance.calamityToggles["Bloodflare Effects"] = bloodflareEffects;
                SoulConfig.Instance.calamityToggles["God Slayer Effects"] = godSlayerEffects;
                SoulConfig.Instance.calamityToggles["Silva Effects"] = silvaEffects;
                SoulConfig.Instance.calamityToggles["Auric Tesla Effects"] = auricEffects;
                SoulConfig.Instance.calamityToggles["Elemental Quiver"] = elementalQuiver;
                SoulConfig.Instance.calamityToggles["Luxor's Gift"] = luxorGift;
                SoulConfig.Instance.calamityToggles["Gladiator's Locket"] = gladiatorLocket;
                SoulConfig.Instance.calamityToggles["Unstable Prism"] = unstablePrism;
                SoulConfig.Instance.calamityToggles["Regenator"] = regenerator;
                SoulConfig.Instance.calamityToggles["Abyssal Diving Suit"] = divingSuit;

                SoulConfig.Instance.calamityToggles["Kendra Pet"] = kendraPet;
                SoulConfig.Instance.calamityToggles["Perforator Pet"] = perforatorPet;
                SoulConfig.Instance.calamityToggles["Bear Pet"] = bearPet;
                SoulConfig.Instance.calamityToggles["Third Sage Pet"] = thirdSagePet;
                SoulConfig.Instance.calamityToggles["Brimling Pet"] = brimlingPet;
                SoulConfig.Instance.calamityToggles["Danny Pet"] = dannyPet;
                SoulConfig.Instance.calamityToggles["Siren Pet"] = sirenPet;
                SoulConfig.Instance.calamityToggles["Chibii Pet"] = chibiiPet;
                SoulConfig.Instance.calamityToggles["Akato Pet"] = akatoPet;
                SoulConfig.Instance.calamityToggles["Fox Pet"] = foxPet;
                SoulConfig.Instance.calamityToggles["Levi Pet"] = leviPet;
            }
        }

        public override void OnChanged()
        {
            terrmenu.terenchmenu.Change();
            terrmenu.soulmenu.Change();
            masomenu.Change();
            masomenu.wallet.Change();
            petmenu.Change();
            thoriummenu.Change();
            calamenu.Change();
        }
        public override void OnLoaded()
        {
            SoulConfig.Instance = this;
            enchantToggles.Add("Boreal Snowballs", terrmenu.terenchmenu.borealSnow);
            enchantToggles.Add("Ebonwood Shadowflame", terrmenu.terenchmenu.ebonFlame);
            enchantToggles.Add("Blood Geyser On Hit", terrmenu.terenchmenu.shadeBlood);
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
            enchantToggles.Add("Shield of Cthulhu", terrmenu.terenchmenu.cthulhuShield);
            enchantToggles.Add("Tin Crits", terrmenu.terenchmenu.tinCrit);
            enchantToggles.Add("Tungsten Effect", terrmenu.terenchmenu.tung);

            enchantToggles.Add("Gladiator Rain", terrmenu.terenchmenu.gladRain);
            enchantToggles.Add("Gold Lucky Coin", terrmenu.terenchmenu.goldCoin);
            enchantToggles.Add("Red Riding Super Bleed", terrmenu.terenchmenu.redBleed);
            enchantToggles.Add("Valhalla Knockback", terrmenu.terenchmenu.valhalKnock);

            enchantToggles.Add("Beetles", terrmenu.terenchmenu.beetBeetles);
            enchantToggles.Add("Cactus Needles", terrmenu.terenchmenu.cacNeedle);
            enchantToggles.Add("Pumpkin Fire", terrmenu.terenchmenu.pumpFire);
            enchantToggles.Add("Spider Swarm", terrmenu.terenchmenu.spidSwarm);
            enchantToggles.Add("Turtle Shell Buff", terrmenu.terenchmenu.turtShell);

            enchantToggles.Add("Chlorophyte Leaf Crystal", terrmenu.terenchmenu.chloroCrystal);
            enchantToggles.Add("Crimson Regen", terrmenu.terenchmenu.crimsonRegen);
            enchantToggles.Add("Frost Icicles", terrmenu.terenchmenu.frostIce);
            enchantToggles.Add("Jungle Spores", terrmenu.terenchmenu.jungleSpores);
            enchantToggles.Add("Molten Inferno Buff", terrmenu.terenchmenu.moltInfern);
            enchantToggles.Add("Shroomite Stealth", terrmenu.terenchmenu.shroomStealth);

            enchantToggles.Add("Dark Artist Effect", terrmenu.terenchmenu.dArtEffect);
            enchantToggles.Add("Necro Guardian", terrmenu.terenchmenu.necroGuard);
            enchantToggles.Add("Shadow Darkness", terrmenu.terenchmenu.shadowDark);
            enchantToggles.Add("Shinobi Through Walls", terrmenu.terenchmenu.shinWalls);
            enchantToggles.Add("Spooky Scythes", terrmenu.terenchmenu.spookScythe);

            enchantToggles.Add("Forbidden Storm", terrmenu.terenchmenu.forbidStorm);
            enchantToggles.Add("Hallowed Enchanted Sword Familiar", terrmenu.terenchmenu.hallowSword);
            enchantToggles.Add("Hallowed Shield", terrmenu.terenchmenu.hallowShield);
            enchantToggles.Add("Silver Sword Familiar", terrmenu.terenchmenu.silverSword);
            enchantToggles.Add("Tiki Minions", terrmenu.terenchmenu.tikiMinion);
            enchantToggles.Add("Spectre Orbs", terrmenu.terenchmenu.spectreOrb);

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
            masoTogDict.Add("Stabilized Gravity", masomenu.gravGlobe2);
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

            soulToggles.Add("Melee Speed", terrmenu.soulmenu.gladSpeed);
            soulToggles.Add("Sniper Scope", terrmenu.soulmenu.sharpSniper);
            soulToggles.Add("Universe Attack Speed", terrmenu.soulmenu.universeSpeed);
            soulToggles.Add("Mining Hunter Buff", terrmenu.soulmenu.mineHunt);
            soulToggles.Add("Mining Dangersense Buff", terrmenu.soulmenu.mineDanger);
            soulToggles.Add("Mining Spelunker Buff", terrmenu.soulmenu.mineSpelunk);
            soulToggles.Add("Mining Shine Buff", terrmenu.soulmenu.mineShine);
            soulToggles.Add("Builder Mode", terrmenu.soulmenu.worldBuild);
            soulToggles.Add("Spore Sac", terrmenu.soulmenu.colSpore);
            soulToggles.Add("Stars On Hit", terrmenu.soulmenu.colStar);
            soulToggles.Add("Bees On Hit", terrmenu.soulmenu.colBee);
            soulToggles.Add("Supersonic Speed Boosts", terrmenu.soulmenu.supersonicSpeed);
            soulToggles.Add("Eternity Stacking", terrmenu.soulmenu.eternityStack);

            //thorium
            thoriumToggles.Add("Air Walkers", thoriummenu.airWalkers);
            thoriumToggles.Add("Crystal Scorpion", thoriummenu.crystalScorpion);
            thoriumToggles.Add("Yuma's Pendant", thoriummenu.yumasPendant);
            thoriumToggles.Add("Head Mirror", thoriummenu.headMirror);
            thoriumToggles.Add("Celestial Aura", thoriummenu.celestialAura);
            thoriumToggles.Add("Ascension Statuette", thoriummenu.ascensionStatue);
            thoriumToggles.Add("Mana-Charged Rocketeers", thoriummenu.manaBoots);
            thoriumToggles.Add("Bronze Lightning", thoriummenu.bronzeLightning);
            thoriumToggles.Add("Illumite Missile", thoriummenu.illumiteMissile);
            thoriumToggles.Add("Jester Bell", thoriummenu.jesterBell);
            thoriumToggles.Add("Eye of the Beholder", thoriummenu.beholderEye);
            thoriumToggles.Add("Terrarium Spirits", thoriummenu.terrariumSpirits);
            thoriumToggles.Add("Crietz", thoriummenu.crietz);
            thoriumToggles.Add("Yew Wood Crits", thoriummenu.yewCrits);
            thoriumToggles.Add("Cryo-Magus Damage", thoriummenu.cryoDamage);
            thoriumToggles.Add("White Dwarf Flares", thoriummenu.whiteDwarf);
            thoriumToggles.Add("Tide Hunter Foam", thoriummenu.tideFoam);
            thoriumToggles.Add("Whispering Tentacles", thoriummenu.whisperingTentacles);
            thoriumToggles.Add("Icy Barrier", thoriummenu.icyBarrier);
            thoriumToggles.Add("Plague Lord's Flask", thoriummenu.plagueFlask);
            thoriumToggles.Add("Tide Turner Globules", thoriummenu.tideGlobules);
            thoriumToggles.Add("Tide Turner Daggers", thoriummenu.tideDaggers);
            thoriumToggles.Add("Folv's Aura", thoriummenu.folvAura);
            thoriumToggles.Add("Folv's Bolts", thoriummenu.folvBolts);
            thoriumToggles.Add("Vampire Gland", thoriummenu.vampireGland);
            thoriumToggles.Add("Flesh Drops", thoriummenu.fleshDrops);
            thoriumToggles.Add("Dragon Flames", thoriummenu.dragonFlames);
            thoriumToggles.Add("Harbinger Overcharge", thoriummenu.harbingerOvercharge);
            thoriumToggles.Add("Assassin Damage", thoriummenu.assassinDamage);
            thoriumToggles.Add("Pyromancer Bursts", thoriummenu.pyromancerBursts);
            thoriumToggles.Add("Conduit Shield", thoriummenu.conduitShield);
            thoriumToggles.Add("Incandescent Spark", thoriummenu.incandescentSpark);
            thoriumToggles.Add("Greedy Magnet", thoriummenu.greedyMagnet);
            thoriumToggles.Add("Cyber Punk States", thoriummenu.cyberStates);
            thoriumToggles.Add("Metronome", thoriummenu.metronome);
            thoriumToggles.Add("Mix Tape", thoriummenu.mixTape);
            thoriumToggles.Add("Lodestone Resistance", thoriummenu.lodestoneResist);
            thoriumToggles.Add("Biotech Probe", thoriummenu.biotechProbe);
            thoriumToggles.Add("Proof of Avarice", thoriummenu.proofAvarice);
            thoriumToggles.Add("Slag Stompers", thoriummenu.slagStompers);
            thoriumToggles.Add("Spring Steps", thoriummenu.springSteps);
            thoriumToggles.Add("Berserker Effect", thoriummenu.berserker);
            thoriumToggles.Add("Bee Booties", thoriummenu.beeBooties);
            thoriumToggles.Add("Ghastly Carapace", thoriummenu.ghastlyCarapace);
            thoriumToggles.Add("Spirit Trapper Wisps", thoriummenu.spiritWisps);
            thoriumToggles.Add("Warlock Wisps", thoriummenu.warlockWisps);
            thoriumToggles.Add("Dread Speed", thoriummenu.dreadSpeed);
            thoriumToggles.Add("Spawn Divers", thoriummenu.divers);
            thoriumToggles.Add("Demon Blood Effect", thoriummenu.demonBlood);

            thoriumToggles.Add("Li'l Devil Minion", thoriummenu.devilMinion);
            thoriumToggles.Add("Li'l Cherub Minion", thoriummenu.cherubMinion);
            thoriumToggles.Add("Sapling Minion", thoriummenu.saplingMinion);

            thoriumToggles.Add("Omega Pet", thoriummenu.omegaPet);
            thoriumToggles.Add("I.F.O. Pet", thoriummenu.ifoPet);
            thoriumToggles.Add("Bio-Feeder Pet", thoriummenu.bioFeederPet);
            thoriumToggles.Add("Blister Pet", thoriummenu.blisterPet);
            thoriumToggles.Add("Wyvern Pet", thoriummenu.wyvernPet);
            thoriumToggles.Add("Inspiring Lantern Pet", thoriummenu.lanternPet);
            thoriumToggles.Add("Lock Box Pet", thoriummenu.boxPet);
            thoriumToggles.Add("Life Spirit Pet", thoriummenu.spiritPet);
            thoriumToggles.Add("Holy Goat Pet", thoriummenu.goatPet);
            thoriumToggles.Add("Owl Pet", thoriummenu.owlPet);
            thoriumToggles.Add("Jellyfish Pet", thoriummenu.jellyfishPet);
            thoriumToggles.Add("Moogle Pet", thoriummenu.mooglePet);
            thoriumToggles.Add("Maid Pet", thoriummenu.maidPet);
            thoriumToggles.Add("Pink Slime Pet", thoriummenu.slimePet);
            thoriumToggles.Add("Glitter Pet", thoriummenu.glitterPet);
            thoriumToggles.Add("Coin Bag Pet", thoriummenu.coinPet);

            //calamity
            calamityToggles.Add("Victide Sea Urchin", calamenu.urchin);
            calamityToggles.Add("Profaned Soul Artifact", calamenu.profanedSoulArtifact);
            calamityToggles.Add("Slime God Minion", calamenu.slimeMinion);
            calamityToggles.Add("Reaver Orb Minion", calamenu.reaverMinion);
            calamityToggles.Add("Omega Blue Tentacles", calamenu.omegaTentacles);
            calamityToggles.Add("Silva Crystal Minion", calamenu.silvaMinion);
            calamityToggles.Add("Godly Soul Artifact", calamenu.godlySoulArtifact);
            calamityToggles.Add("Mechworm Minion", calamenu.mechwormMinion);
            calamityToggles.Add("Nebulous Core", calamenu.nebulousCore);
            calamityToggles.Add("Red Devil Minion", calamenu.devilMinion);
            calamityToggles.Add("Permafrost's Concoction", calamenu.permafrostPotion);
            calamityToggles.Add("Daedalus Crystal Minion", calamenu.daedalusMinion);
            calamityToggles.Add("Polterghast Mines", calamenu.polterMines);
            calamityToggles.Add("Plague Hive", calamenu.plagueHive);
            calamityToggles.Add("Chaos Spirit Minion", calamenu.chaosMinion);
            calamityToggles.Add("Valkyrie Minion", calamenu.valkyrieMinion);
            calamityToggles.Add("Yharim's Gift", calamenu.yharimGift);
            calamityToggles.Add("Fungal Clump Minion", calamenu.fungalMinion);
            calamityToggles.Add("Elemental Waifus", calamenu.waifuMinions);
            calamityToggles.Add("Shellfish Minions", calamenu.shellfishMinion);
            calamityToggles.Add("Amidias' Pendant", calamenu.amidiasPendant);
            calamityToggles.Add("Giant Pearl", calamenu.giantPearl);
            calamityToggles.Add("Poisonous Sea Water", calamenu.poisonSeawater);
            calamityToggles.Add("Daedalus Effects", calamenu.daedalusEffects);
            calamityToggles.Add("Reaver Effects", calamenu.reaverEffects);
            calamityToggles.Add("Astral Stars", calamenu.reaverEffects);
            calamityToggles.Add("Ataxia Effects", calamenu.reaverEffects);
            calamityToggles.Add("Xeroc Effects", calamenu.xerocEffects);
            calamityToggles.Add("Tarragon Effects", calamenu.tarragonEffects);
            calamityToggles.Add("Bloodflare Effects", calamenu.bloodflareEffects);
            calamityToggles.Add("God Slayer Effects", calamenu.godSlayerEffects);
            calamityToggles.Add("Silva Effects", calamenu.silvaEffects);
            calamityToggles.Add("Auric Tesla Effects", calamenu.auricEffects);
            calamityToggles.Add("Elemental Quiver", calamenu.elementalQuiver);
            calamityToggles.Add("Luxor's Gift", calamenu.luxorGift);
            calamityToggles.Add("Gladiator's Locket", calamenu.gladiatorLocket);
            calamityToggles.Add("Unstable Prism", calamenu.unstablePrism);
            calamityToggles.Add("Regenator", calamenu.regenerator);
            calamityToggles.Add("Abyssal Diving Suit", calamenu.divingSuit);

            calamityToggles.Add("Kendra Pet", calamenu.kendraPet);
            calamityToggles.Add("Perforator Pet", calamenu.perforatorPet);
            calamityToggles.Add("Bear Pet", calamenu.bearPet);
            calamityToggles.Add("Third Sage Pet", calamenu.thirdSagePet);
            calamityToggles.Add("Brimling Pet", calamenu.brimlingPet);
            calamityToggles.Add("Danny Pet", calamenu.dannyPet);
            calamityToggles.Add("Siren Pet", calamenu.sirenPet);
            calamityToggles.Add("Chibii Pet", calamenu.chibiiPet);
            calamityToggles.Add("Akato Pet", calamenu.akatoPet);
            calamityToggles.Add("Fox Pet", calamenu.foxPet);
            calamityToggles.Add("Levi Pet", calamenu.leviPet);
        }

        public bool GetValue(string input)
        {
            if (Main.player[Main.myPlayer].GetModPlayer<FargoPlayer>().MutantPresence)
            {
                return false;
            }

            if (enchantToggles.ContainsKey(input))
            {
                return enchantToggles[input];
            }
            if (petToggles.ContainsKey(input))
            {
                return petToggles[input];
            }
            if (soulToggles.ContainsKey(input))
            {
                return soulToggles[input];
            }
            if (walletToggles.ContainsKey(input))
            {
                return walletToggles[input];
            }

            if (masoTogDict.ContainsKey(input))
            {
                return masoTogDict[input];
            }

            if (thoriumToggles.ContainsKey(input))
            {
                return thoriumToggles[input];
            }

            if (calamityToggles.ContainsKey(input))
            {
                return calamityToggles[input];
            }

            return false;
        }
    }
}
