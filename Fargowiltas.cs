using FargowiltasSouls.Items.Accessories.Enchantments;
using FargowiltasSouls.Items.Accessories.Forces;
using FargowiltasSouls.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace FargowiltasSouls
{
    internal class Fargowiltas : Mod
    {
        internal static ModHotKey FreezeKey;
        internal static ModHotKey GoldKey;
        internal static ModHotKey SmokeBombKey;

        internal static List<int> DebuffIDs;

        internal static Fargowiltas Instance;
        //loaded
        internal bool FargosLoaded;
        internal bool ThoriumLoaded;
        internal bool AALoaded;
        internal bool BlueMagicLoaded;
        internal bool CalamityLoaded;
        internal bool DBTLoaded;
        internal bool SOALoaded;
        internal bool ApothLoaded;
        internal bool MasomodeEX;

        internal bool LoadedNewSprites;

        public UserInterface CustomResources;

        internal static readonly Dictionary<int, int> ModProjDict = new Dictionary<int, int>();

        public Fargowiltas()
        {
            Properties = new ModProperties
            {
                Autoload = true,
                AutoloadGores = true,
                AutoloadSounds = true
            };
        }

        public override void Load()
        {
            Instance = this;

            if (Language.ActiveCulture == GameCulture.Chinese)
            {
                FreezeKey = RegisterHotKey("冻结时间", "P");
                GoldKey = RegisterHotKey("金身", "O");
                SmokeBombKey = RegisterHotKey("Throw Smoke Bomb", "I");
            }
            else
            {
                FreezeKey = RegisterHotKey("Freeze Time", "P");
                GoldKey = RegisterHotKey("Turn Gold", "O");
                SmokeBombKey = RegisterHotKey("Throw Smoke Bomb", "I");
            }
            
            AddMusicBox(GetSoundSlot(SoundType.Music, "Sounds/Music/SteelRed"), ItemType("MutantMusicBox"), TileType("MutantMusicBoxSheet"));

            #region Toggles
            #region enchants
            ModTranslation text = CreateTranslation("WoodHeader");
            text.SetDefault("[i:" + Instance.ItemType("WoodForce") + "] Force of Wood");
            AddTranslation(text);
            text = CreateTranslation("LifeHeader");
            text.SetDefault("[i:" + Instance.ItemType("LifeForce") + "] Force of Life");
            AddTranslation(text);
            text = CreateTranslation("NatureHeader");
            text.SetDefault("[i:" + Instance.ItemType("NatureForce") + "] Force of Nature");
            AddTranslation(text);
            text = CreateTranslation("ShadowHeader");
            text.SetDefault("[i:" + Instance.ItemType("ShadowForce") + "] Shadow Force");
            AddTranslation(text);
            text = CreateTranslation("SpiritHeader");
            text.SetDefault("[i:" + Instance.ItemType("SpiritForce") + "] Force of Spirit");
            AddTranslation(text);
            text = CreateTranslation("CosmoHeader");
            text.SetDefault("[i:" + Instance.ItemType("CosmoForce") + "] Force of Cosmos");
            AddTranslation(text);
            ModTranslation borealtrans = CreateTranslation("BorealConfig");
            borealtrans.SetDefault("[i:" + Instance.ItemType("BorealWoodEnchant") + "][c/8B7464: Boreal Snowballs]");
            AddTranslation(borealtrans);
            text = CreateTranslation("MahoganyConfig");
            text.SetDefault("[i:" + Instance.ItemType("RichMahoganyEnchant") + "][c/b56c64: Mahogany Hook Speed]");
            AddTranslation(text);
            text = CreateTranslation("EbonConfig");
            text.SetDefault("[i:" + Instance.ItemType("EbonwoodEnchant") + "][c/645a8d: Ebonwood Shadowflame]");
            AddTranslation(text);
            text = CreateTranslation("ShadeConfig");
            text.SetDefault("[i:" + Instance.ItemType("ShadewoodEnchant") + "][c/586876: Blood Geyser On Hit]");
            AddTranslation(text);
            text = CreateTranslation("PalmConfig");
            text.SetDefault("[i:" + Instance.ItemType("PalmWoodEnchant") + "][c/b78d56: Palmwood Sentry]");
            AddTranslation(text);
            text = CreateTranslation("PearlConfig");
            text.SetDefault("[i:" + Instance.ItemType("PearlwoodEnchant") + "][c/ad9a5f: Pearlwood Rainbow]");
            AddTranslation(text);
            text = CreateTranslation("EarthHeader");
            text.SetDefault("[i:" + Instance.ItemType("EarthForce") + "] Force of Earth");
            AddTranslation(text);
            text = CreateTranslation("AdamantiteConfig");
            text.SetDefault("[i:" + Instance.ItemType("AdamantiteEnchant") + "][c/dd557d: Adamantite Projectile Splitting]");
            AddTranslation(text);
            text = CreateTranslation("CobaltConfig");
            text.SetDefault("[i:" + Instance.ItemType("CobaltEnchant") + "][c/3da4c4: Cobalt Shards]");
            AddTranslation(text);
            text = CreateTranslation("MythrilConfig");
            text.SetDefault("[i:" + Instance.ItemType("MythrilEnchant") + "][c/9dd290: Mythril Weapon Speed]");
            AddTranslation(text);
            text = CreateTranslation("OrichalcumConfig");
            text.SetDefault("[i:" + Instance.ItemType("OrichalcumEnchant") + "][c/eb3291: Orichalcum Fireballs]");
            AddTranslation(text);
            text = CreateTranslation("PalladiumConfig");
            text.SetDefault("[i:" + Instance.ItemType("PalladiumEnchant") + "][c/f5ac28: Palladium Healing]");
            AddTranslation(text);
            text = CreateTranslation("TitaniumConfig");
            text.SetDefault("[i:" + Instance.ItemType("TitaniumEnchant") + "][c/828c88: Titanium Shadow Dodge]");
            AddTranslation(text);
            text = CreateTranslation("TerraHeader");
            text.SetDefault("[i:" + Instance.ItemType("TerraForce") + "] Terra Force");
            AddTranslation(text);
            text = CreateTranslation("CopperConfig");
            text.SetDefault("[i:" + Instance.ItemType("CopperEnchant") + "][c/d56617: Copper Lightning]");
            AddTranslation(text);
            text = CreateTranslation("IronMConfig");
            text.SetDefault("[i:" + Instance.ItemType("IronEnchant") + "][c/988e83: Iron Magnet]");
            AddTranslation(text);
            text = CreateTranslation("IronSConfig");
            text.SetDefault("[i:" + Instance.ItemType("IronEnchant") + "][c/988e83: Iron Shield]");
            AddTranslation(text);
            text = CreateTranslation("CthulhuShield");
            text.SetDefault("[i:" + Instance.ItemType("IronEnchant") + "][c/988e83: Shield of Cthulhu]");
            AddTranslation(text);
            text = CreateTranslation("TinConfig");
            text.SetDefault("[i:" + Instance.ItemType("TinEnchant") + "][c/a28b4e: Tin Crits]");
            AddTranslation(text);
            text = CreateTranslation("TungstenConfig");
            text.SetDefault("[i:" + Instance.ItemType("TungstenEnchant") + "][c/b0d2b2: Tungsten Effect]");
            AddTranslation(text);
            text = CreateTranslation("WillHeader");
            text.SetDefault("[i:" + Instance.ItemType("WillForce") + "] Force of Will");
            AddTranslation(text);
            text = CreateTranslation("GladiatorConfig");
            text.SetDefault("[i:" + Instance.ItemType("GladiatorEnchant") + "][c/9c924e: Gladiator Rain]");
            AddTranslation(text);
            text = CreateTranslation("GoldConfig");
            text.SetDefault("[i:" + Instance.ItemType("GoldEnchant") + "][c/e7b21c: Gold Lucky Coin]");
            AddTranslation(text);
            text = CreateTranslation("RedRidingConfig");
            text.SetDefault("[i:" + Instance.ItemType("RedRidingEnchant") + "][c/c01b3c: Red Riding Super Bleed]");
            AddTranslation(text);
            text = CreateTranslation("ValhallaConfig");
            text.SetDefault("[i:" + Instance.ItemType("ValhallaKnightEnchant") + "][c/93651e: Valhalla Knockback]");
            AddTranslation(text);
            string[] EnchConfig = {
            //force of life
            "BeetleConfig",
            "CactusConfig",
            "PumpkinConfig",
            "SpiderConfig",
            "TurtleConfig",
            //force of nature
            "ChlorophyteConfig",
            "CrimsonConfig",
            "FrostConfig",
            "JungleConfig",
            "MoltenConfig",
            "ShroomiteConfig",
            //shadow force
            "DarkArtConfig",
            "NecroConfig",
            "ShadowConfig",
            "ShinobiConfig",
            "SpookyConfig",
            //force of spirit
            "ForbiddenConfig",
            "HallowedConfig",
            "HalllowSConfig",
            "SilverConfig",
            "SpectreConfig",
            "TikiConfig",
            //force of cosmos
            "MeteorConfig",
            "NebulaConfig",
            "SolarConfig",
            "StardustConfig",
            "VortexSConfig",
            "VortexVConfig"
            };
            string[] EnchName = {
            //force of life
            "Beetles",
            "Cactus Needles",
            "Pumpkin Fire",
            "Spider Swarm",
            "Turtle Shell Buff",
            //force of nature
            "Chlorophyte Leaf Crystal",
            "Crimson Regen",
            "Frost Icicles",
            "Jungle Spores",
            "Molten Inferno Buff",
            "Shroomite Stealth",
            //shadow force
            "Dark Artist Effect",
            "Necro Guardian",
            "Shadow Darkness",
            "Shinobi Through Walls",
            "Spooky Scythes",
            //force of spirit
            "Forbidden Storm",
            "Hallowed Enchanted Sword Familiar",
            "Hallowed Shield",
            "Silver Sword Familiar",
            "Spectre Orbs",
            "Tiki Minions",
            //force of cosmos
            "Meteor Shower",
            "Nebula Boosters",
            "Solar Shield",
            "Stardust Guardian",
            "Vortex Stealth",
            "Vortex Voids"
            };
            string[] EnchColor = {
            //force of life
            "3357e4",
            "799e1d",
            "e3651c",
            "6d4e45",
            "f89c5c",
            //force of nature
            "248900",
            "C8364B",
            "7abdb9",
            "71971f",
            "c12b2b",
            "008cf4",
            //shadow force
            "9b5cb0",
            "565643",
            "42356f",
            "935b18",
            "644e74",
            //force of spirit
            "e7b21c",
            "968564",
            "968564",
            "b4b4cc",
            "accdfc",
            "56A52B",
            //force of cosmos
            "5f4752",
            "fe7ee5",
            "fe9e23",
            "00aeee",
            "00f2aa",
            "00f2aa"
            };
            string[] EnchItem = {
            //force of life
            "BeetleEnchant",
            "CactusEnchant",
            "PumpkinEnchant",
            "SpiderEnchant",
            "TurtleEnchant",
            //force of nature
            "ChlorophyteEnchant",
            "CrimsonEnchant",
            "FrostEnchant",
            "JungleEnchant",
            "MoltenEnchant",
            "ShroomiteEnchant",
            //shadow force
            "DarkArtistEnchant",
            "NecroEnchant",
            "ShadowEnchant",
            "ShinobiEnchant",
            "SpookyEnchant",
            //force of spirit
            "ForbiddenEnchant",
            "HallowEnchant",
            "HallowEnchant",
            "SilverEnchant",
            "SpectreEnchant",
            "TikiEnchant",
            //force of cosmos
            "MeteorEnchant",
            "NebulaEnchant",
            "SolarEnchant",
            "StardustEnchant",
            "VortexEnchant",
            "VortexEnchant"
            };
            for (int x = 0; x < EnchConfig.Length; x++)
            {
                text = CreateTranslation(EnchConfig[x]);
                text.SetDefault("[i:" + Instance.ItemType(EnchItem[x]) + "][c/" + EnchColor[x] + ": " + EnchName[x] + "]");
                AddTranslation(text);
            }
            #endregion
            #region masomode toggles
            string[] masoTogName = { 
            //deathbringer fairy
            "Slimy Shield Effects",
            "Scythes When Dashing",
            "Skeletron Arms Minion",
            //pure heart
            "Tiny Eaters",
            "Creeper Shield",
            //bionomic cluster
            "Rainbow Slime Minion",
            "Frostfireballs",
            "Attacks Spawn Hearts",
            "Squeaky Toy On Hit",
            "Tentacles On Hit",
            "Inflict Clipped Wings",
            //dubious circutry
            "Inflict Lightning Rod",
            "Probes Minion",
            //heart of the masochist
            "Gravity Control",
            "Pumpking's Cape Support",
            "Flocko Minion",
            "Saucer Minion",
            "True Eyes Minion",
            //chalice of the moon
            "Celestial Rune Support",
            "Plantera Minion",
            "Lihzahrd Ground Pound",
            "Ancient Visions On Hit",
            "Cultist Minion",
            "Spectral Fishron",
            //lump of flesh
            "Pungent Eye Minion",
            //mutant armor
            "Abominationn Minion",
            "Phantasmal Ring Minion",
            //other
            "Spiky Balls On Hit",
            "Sinister Icon",
            "Boss Recolors (Restart to use)"};
            string[] masoTogNameCh = { 
            //deathbringer fairy
            "Slimy Shield Effects",
            "Scythes When Dashing",
            "Skeletron Arms Minion",
            //pure heart
            "Tiny Eaters",
            "Creeper Shield",
            //bionomic cluster
            "Rainbow Slime Minion",
            "Frostfireballs",
            "Attacks Spawn Hearts",
            "Squeaky Toy On Hit",
            "Tentacles On Hit",
            "Inflict Clipped Wings",
            //dubious circutry
            "Inflict Lightning Rod",
            "Probes Minion",
            //heart of the masochist
            "Gravity Control",
            "Pumpking's Cape Support",
            "Flocko Minion",
            "Saucer Minion",
            "True Eyes Minion",
            //chalice of the moon
            "Celestial Rune Support",
            "Plantera Minion",
            "Lihzahrd Ground Pound",
            "Ancient Visions On Hit",
            "Cultist Minion",
            "Spectral Fishron",
            //lump of flesh
            "Pungent Eye Minion",
            //mutant armor
            "Abominationn Minion",
            "Phantasmal Ring Minion",
            //other
            "Spiky Balls On Hit",
            "Sinister Icon",
            "Boss Recolors (Restart to use)"};
            string[] masoTogConfigName = {
            //deathbringer fairy
            "MasoSlimeConfig",
            "MasoEyeConfig",
            "MasoSkeleConfig",
            //pure heart
            "MasoEaterConfig",
            "MasoBrainConfig",
            //bionomic cluster
            "MasoRainbowConfig",
            "MasoFrigidConfig",
            "MasoNymphConfig",
            "MasoSqueakConfig",
            "MasoPouchConfig",
            "MasoClippedConfig",
            //dubious circutry
            "MasoLightningConfig",
            "MasoProbeConfig",
            //heart of the masochist
            "MasoGravConfig",
            "MasoPump",
            "MasoFlockoConfig",
            "MasoUfoConfig",
            "MasoTrueEyeConfig",
            //chalice of the moon
            "MasoCelestConfig",
            "MasoPlantConfig",
            "MasoGolemConfig",
            "MasoVisionConfig",
            "MasoCultistConfig",
            "MasoFishronConfig",
            //lump of flesh
            "MasoPugentConfig",
            //mutant armor
            "MasoAbomConfig",
            "MasoRingConfig",
            //other
            "MasoSpikeConfig",
            "MasoIconConfig",
            "MasoBossRecolors"};
            for (int x = 0; x <= 29; x++)
            {
                text = CreateTranslation(masoTogConfigName[x]);
                if (Language.ActiveCulture == GameCulture.Chinese)
                {
                    text.SetDefault(masoTogNameCh[x]);
                }
                else
                {
                    text.SetDefault(masoTogName[x]);
                }
                AddTranslation(text);
            }
            #endregion
            #region pet toggles
            int[] petnums = {
            //NORMAL PETS
            1810,//black cat
            3628,//companion cube
            1837, //cursed sapling
            1242, //dino pet
            3857, //dragon
            994, //eater
            1311, //eye spring
            3060, //face monster
            3855, //gato
            1170, //hornet
            1172, //lizard
            2587, //mini minitaur
            1180, //parrot
            669, //penguin
            1927, //puppy
            1182, //seedling
            1169, //dungeon guardian
            1312, // snowman
            1798, // spider
            1799, //squashling
            1171, //tiki
            1181, //truffle
            753, //turtle
            2420, //zephyr fish
                  //LIGHT PETS
            3062, //crimson heart
            425, //fairy
            3856, //flickerwick
            3043, //magic lanturn
            115, //shadow orb
            3577, //suspicious eye
            1183//wisp
            };
            string[] petTogName = {
            "Black Cat Pet",
            "Companion Cube Pet",
            "Cursed Sapling Pet",
            "Dino Pet",
            "Dragon Pet",
            "Eater Pet",
            "Eye Spring Pet",
            "Face Monster Pet",
            "Gato Pet",
            "Hornet Pet",
            "Lizard Pet",
            "Mini Minotaur Pet",
            "Parrot Pet",
            "Penguin Pet",
            "Puppy Pet",
            "Seedling Pet",
            "Skeletron Pet",
            "Snowman Pet",
            "Spider Pet",
            "Squashling Pet",
            "Tiki Pet",
            "Truffle Pet",
            "Turtle Pet",
            "Zephyr Fish Pet",
            //LIGHT PETS
            "Crimson Heart Pet",
            "Fairy Pet",
            "Flickerwick Pet",
            "Magic Lantern Pet",
            "Shadow Orb Pet",
            "Suspicious Eye Pet",
            "Wisp Pet" };
            string[] petTogConfigName = {
            "PetCatConfig",
            "PetCubeConfig",
            "PetCurseSapConfig",
            "PetDinoConfig",
            "PetDragonConfig",
            "PetEaterConfig",
            "PetEyeSpringConfig",
            "PetFaceMonsterConfig",
            "PetGatoConfig",
            "PetHornetConfig",
            "PetLizardConfig",
            "PetMinitaurConfig",
            "PetParrotConfig",
            "PetPenguinConfig",
            "PetPupConfig",
            "PetSeedConfig",
            "PetDGConfig",
            "PetSnowmanConfig",
            "PetSpiderConfig",
            "PetSquashConfig",
            "PetTikiConfig",
            "PetShroomConfig",
            "PetTurtleConfig",
            "PetZephyrConfig",
            //LIGHT PETS
            "PetHeartConfig",
            "PetNaviConfig",
            "PetFlickerConfig",
            "PetLanturnConfig",
            "PetOrbConfig",
            "PetSuspEyeConfig",
            "PetWispConfig" };
            for (int x = 0; x <= 30; x++)
            {
                text = CreateTranslation(petTogConfigName[x]);
                text.SetDefault("[I:" + petnums[x] + "] " + petTogName[x]);
                AddTranslation(text);
            }
            #endregion
            #region wallet toggles
            string[] prefix = {
        "Warding",
        "Violent",
        "Quick",
        "Lucky",
        "Menacing",
        "Legendary",
        "Unreal",
        "Mythical",
        "Godly",
        "Demonic",
        "Ruthless",
        "Light",
        "Deadly",
        "Rapid"};
            string[] prefixconf = {
        "WalletWardingConfig",
        "WalletViolentConfig",
        "WalletQuickConfig",
        "WalletLuckyConfig",
        "WalletMenacingConfig",
        "WalletLegendaryConfig",
        "WalletUnrealConfig",
        "WalletMythicalConfig",
        "WalletGodlyConfig",
        "WalletDemonicConfig",
        "WalletRuthlessConfig",
        "WalletLightConfig",
        "WalletDeadlyConfig",
        "WalletRapidConfig" };
            for (int x = 0; x <= 13; x++)
            {
                text = CreateTranslation(prefixconf[x]);
                text.SetDefault(prefix[x]);
                AddTranslation(text);
            }
            #endregion
            #region soul toggles
            string[] soultognames = {
            //Universe
            "Melee Speed",
            "Sniper Scope",
            "Universe Attack Speed",
            //dimensions
            "Mining Hunter Buff",
            "Mining Dangersense Buff",
            "Mining Spelunker Buff",
            "Mining Shine Buff",
            "Builder Mode",
            "Spore Sac",
            "Stars On Hit",
            "Bees On Hit",
            "Supersonic Speed Boosts",
            //idk 
            "Eternity Stacking"};
            string[] soultogconfig = {
            //Universe
            "MeleeConfig",
            "SniperConfig",
            "UniverseConfig",
            //dimensions
            "MiningHuntConfig",
            "MiningDangerConfig",
            "MiningSpelunkConfig",
            "MiningShineConfig",
            "BuilderConfig",
            "DefenseSporeConfig",
            "DefenseStarConfig",
            "DefenseBeeConfig",
            "SupersonicConfig",
            //idk 
            "EternityConfig" };
            string[] soultogitemnames = {
            //Universe
            "GladiatorsSoul",
            "SharpshootersSoul",
            "UniverseSoul",
            //dimensions
            "MinerEnchant",
            "MinerEnchant",
            "MinerEnchant",
            "MinerEnchant",
            "WorldShaperSoul",
            "ColossusSoul",
            "ColossusSoul",
            "ColossusSoul",
            "SupersonicSoul",
            //idk 
            "EternitySoul" };
            string[] soulcolor = {
            //Universe
            "ffffff",
            "ffffff",
            "ffffff",
            //dimensions
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            //idk 
            "ffffff" };
            for (int x = 0; x <= 12; x++)
            {
                text = CreateTranslation(soultogconfig[x]);
                text.SetDefault("[i:" + Instance.ItemType(soultogitemnames[x]) + "][c/" + soulcolor[x] + ": " + soultognames[x] + "]");
                AddTranslation(text);
            }
            #endregion

            #region thorium

            string[] thoriumTogNames = {
            "Air Walkers",
            "Crystal Scorpion",
            "Yuma's Pendant",
            "Head Mirror",
            "Celestial Aura",
            "Ascension Statuette",
            "Mana-Charged Rocketeers",
            "Bronze Lightning",
            "Illumite Missile",
            "Jester Bell",
            "Eye of the Beholder",
            "Terrarium Spirits",
            "Crietz",
            "Yew Wood Crits",
            "Cryo-Magus Damage",
            "White Dwarf Flares",
            "Tide Hunter Foam",
            "Whispering Tentacles",
            "Icy Barrier",
            "Plague Lord's Flask",
            "Tide Turner Globules",
            "Tide Turner Daggers",
            "Folv's Aura",
            "Folv's Bolts",
            "Vampire Gland",
            "Flesh Drops",
            "Dragon Flames",
            "Harbinger Overcharge",
            "Assassin Damage",
            "Pyromancer Bursts",
            "Conduit Shield",
            "Incandescent Spark",
            "Greedy Magnet",
            "Cyber Punk States",
            "Metronome",
            "Mix Tape",
            "Lodestone Resistance",
            "Biotech Probe",
            "Proof of Avarice",
            "Slag Stompers",
            "Spring Steps",
            "Berserker Effect",
            "Bee Booties",
            "Ghastly Carapace",
            "Spirit Trapper Wisps",
            "Warlock Wisps",
            "Dread Speed",
            "Spawn Divers",
            "Demon Blood Effect",
            "Li'l Devil Minion",
            "Li'l Cherub Minion",
            "Sapling Minion",
            "Omega Pet",
            "I.F.O. Pet",
            "Bio-Feeder Pet",
            "Blister Pet",
            "Wyvern Pet",
            "Inspiring Lantern Pet",
            "Lock Box Pet",
            "Life Spirit Pet",
            "Holy Goat Pet",
            "Owl Pet",
            "Jellyfish Pet",
            "Moogle Pet",
            "Maid Pet",
            "Pink Slime Pet",
            "Glitter Pet",
            "Coin Bag Pet"};

            string[] thoriumTogConfig = {
            "ThoriumAirWalkersConfig",
            "ThoriumCrystalScorpionConfig",
            "ThoriumYumasPendantConfig",
            "ThoriumHeadMirrorConfig",
            "ThoriumCelestialAuraConfig",
            "ThoriumAscensionStatueConfig",
            "ThoriumManaBootsConfig",
            "ThoriumBronzeLightningConfig",
            "ThoriumIllumiteMissileConfig",
            "ThoriumJesterBellConfig",
            "ThoriumBeholderEyeConfig",
            "ThoriumTerrariumSpiritsConfig",
            "ThoriumCrietzConfig",
            "ThoriumYewCritsConfig",
            "ThoriumCryoDamageConfig",
            "ThoriumWhiteDwarfConfig",
            "ThoriumTideFoamConfig",
            "ThoriumWhisperingTentaclesConfig",
            "ThoriumIcyBarrierConfig",
            "ThoriumPlagueFlaskConfig",
            "ThoriumTideGlobulesConfig",
            "ThoriumTideDaggersConfig",
            "ThoriumFolvAuraConfig",
            "ThoriumFolvBoltsConfig",
            "ThoriumVampireGlandConfig",
            "ThoriumFleshDropsConfig",
            "ThoriumDragonFlamesConfig",
            "ThoriumHarbingerOverchargeConfig",
            "ThoriumAssassinDamageConfig",
            "ThoriumpyromancerBurstsConfig",
            "ThoriumConduitShieldConfig",
            "ThoriumIncandescentSparkConfig",
            "ThoriumGreedyMagnetConfig",
            "ThoriumCyberStatesConfig",
            "ThoriumMetronomeConfig",
            "ThoriumMixTapeConfig",
            "ThoriumLodestoneConfig",
            "ThoriumBiotechProbeConfig",
            "ThoriumProofAvariceConfig",
            "ThoriumSlagStompersConfig",
            "ThoriumSpringStepsConfig",
            "ThoriumBerserkerConfig",
            "ThoriumBeeBootiesConfig",
            "ThoriumGhastlyCarapaceConfig",
            "ThoriumSpiritWispsConfig",
            "ThoriumWarlockWispsConfig",
            "ThoriumDreadConfig",
            "ThoriumDiverConfig",
            "ThoriumDemonBloodConfig",
            "ThoriumDevilMinionConfig",
            "ThoriumCherubMinionConfig",
            "ThoriumSaplingMinionConfig",
            "ThoriumOmegaPetConfig",
            "ThoriumIFOPetConfig",
            "ThoriumBioFeederPetConfig",
            "ThoriumBlisterPetConfig",
            "ThoriumWyvernPetConfig",
            "ThoriumLanternPetConfig",
            "ThoriumBoxPetConfig",
            "ThoriumSpiritPetConfig",
            "ThoriumGoatPetConfig",
            "ThoriumOwlPetConfig",
            "ThoriumJellyfishPetConfig",
            "ThoriumMooglePetConfig",
            "ThoriumMaidPetConfig",
            "ThoriumSlimePetConfig",
            "ThoriumGlitterPetConfig",
            "ThoriumCoinPetConfig"};

            string[] thoriumTogItems = {
            "SupersonicSoul",
            "ConjuristsSoul",
            "ConjuristsSoul",
            "GuardianAngelsSoul",
            "CelestialEnchant",
            "CelestialEnchant",
            "MalignantEnchant",
            "BronzeEnchant",
            "IllumiteEnchant",
            "JesterEnchant",
            "ValadiumEnchant",
            "TerrariumEnchant",
            "ThoriumEnchant",
            "YewWoodEnchant",
            "CryoMagusEnchant",
            "WhiteDwarfEnchant",
            "TideHunterEnchant",
            "WhisperingEnchant",
            "IcyEnchant",
            "PlagueDoctorEnchant",
            "TideTurnerEnchant",
            "TideTurnerEnchant",
            "FolvEnchant",
            "FolvEnchant",
            "FleshEnchant",
            "FleshEnchant",
            "DragonEnchant",
            "HarbingerEnchant",
            "AssassinEnchant",
            "PyromancerEnchant",
            "ConduitEnchant",
            "DurasteelEnchant",
            "DurasteelEnchant",
            "CyberPunkEnchant",
            "ConductorEnchant",
            "NobleEnchant",
            "LodestoneEnchant",
            "BiotechEnchant",
            "GoldEnchant",
            "MagmaEnchant",
            "MagmaEnchant",
            "BerserkerEnchant",
            "BeeEnchant",
            "SpectreEnchant",
            "SpiritTrapperEnchant",
            "WarlockEnchant",
            "DreadEnchant",
            "ThoriumEnchant",
            "DemonBloodEnchant",
            "WarlockEnchant",
            "SacredEnchant",
            "LivingWoodEnchant",
            "ConduitEnchant",
            "ConduitEnchant",
            "MeteorEnchant",
            "FleshEnchant",
            "DragonEnchant",
            "GeodeEnchant",
            "GeodeEnchant",
            "SacredEnchant",
            "LifeBinderEnchant",
            "CryoMagusEnchant",
            "DepthDiverEnchant",
            "WhiteKnightEnchant",
            "DreamWeaverEnchant",
            "IllumiteEnchant",
            "PlatinumEnchant",
            "GoldEnchant"};

            string[] thoriumColor = {
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff"};

            for (int x = 0; x < thoriumTogNames.Length; x++)
            {
                text = CreateTranslation(thoriumTogConfig[x]);
                text.SetDefault("[i:" + Instance.ItemType(thoriumTogItems[x]) + "][c/" + thoriumColor[x] + ": " + thoriumTogNames[x] + "]");
                AddTranslation(text);
            }

            #endregion

            #region calamity
            string[] calamityTogNames = {

            "Victide Sea Urchin",
            "Profaned Soul Artifact",
            "Slime God Minion",
            "Reaver Orb Minion",
            "Omega Blue Tentacles",
            "Silva Crystal Minion",
            "Godly Soul Artifact",
            "Mechworm Minion",
            "Nebulous Core",
            "Red Devil Minion",
            "Permafrost's Concoction",
            "Daedalus Crystal Minion",
            "Polterghast Mines",
            "Plague Hive",
            "Chaos Spirit Minion",
            "Valkyrie Minion",
            "Yharim's Gift",
            "Fungal Clump Minion",
            "Elemental Waifus",
            "Shellfish Minions",
            "Amidias' Pendant",
            "Giant Pearl",
            "Poisonous Sea Water",
            "Daedalus Effects",
            "Reaver Effects",
            "Astral Stars",
            "Ataxia Effects",
            "Xeroc Effects",
            "Tarragon Effects",
            "Bloodflare Effects",
            "God Slayer Effects",
            "Silva Effects",
            "Auric Tesla Effects",
            "Elemental Quiver",
            "Luxor's Gift",
            "Gladiator's Locket",
            "Unstable Prism",
            "Regenator",
            "Abyssal Diving Suit",
            "Kendra Pet",
            "Perforator Pet",
            "Bear Pet",
            "Third Sage Pet",
            "Brimling Pet",
            "Danny Pet",
            "Siren Pet",
            "Chibii Pet",
            "Akato Pet",
            "Fox Pet",
            "Levi Pet"
        };

            string[] calamityTogConfig = {
            "CalamityUrchinConfig",
            "CalamityProfanedArtifactConfig",
            "CalamitySlimeMinionConfig",
            "CalamityReaverMinionConfig",
            "CalamityOmegaTentaclesConfig",
            "CalamitySilvaMinionConfig",
            "CalamityGodlyArtifactConfig",
            "CalamityMechwormMinionConfig",
            "CalamityNebulousCoreConfig",
            "CalamityDevilMinionConfig",
            "CalamityPermafrostPotionConfig",
            "CalamityDaedalusMinionConfig",
            "CalamityPolterMinesConfig",
            "CalamityPlagueHiveConfig",
            "CalamityChaosMinionConfig",
            "CalamityValkyrieMinionConfig",
            "CalamityYharimGiftConfig",
            "CalamityFungalMinionConfig",
            "CalamityWaifuMinionsConfig",
            "CalamityShellfishMinionConfig",
            "CalamityAmidiasPendantConfig",
            "CalamityGiantPearlConfig",
            "CalamityPoisonSeawaterConfig",
            "CalamityDaedalusEffectsConfig",
            "CalamityReaverEffectsConfig",
            "CalamityAstralStarsConfig",
            "CalamityAtaxiaEffectsConfig",
            "CalamityXerocEffectsConfig",
            "CalamityTarragonEffectsConfig",
            "CalamityBloodflareEffectsConfig",
            "CalamityGodSlayerEffectsConfig",
            "CalamitySilvaEffectsConfig",
            "CalamityAuricEffectsConfig",
            "CalamityElementalQuiverConfig",
            "CalamityLuxorGiftConfig",
            "CalamityGladiatorLocketConfig",
            "CalamityUnstablePrismConfig",
            "CalamityRegeneratorConfig",
            "CalamityDivingSuitConfig",
            "CalamityKendraConfig",
            "CalamityPerforatorConfig",
            "CalamityBearConfig",
            "CalamityThirdSageConfig",
            "CalamityBrimlingConfig",
            "CalamityDannyConfig",
            "CalamitySirenConfig",
            "CalamityChibiiConfig",
            "CalamityAkatoConfig",
            "CalamityFoxConfig",
            "CalamityLeviConfig"
    };

            string[] calamityTogItems = {
            "VictideEnchant",
            "TarragonEnchant",
            "StatigelEnchant",
            "ReaverEnchant",
            "OmegaBlueEnchant",
            "SilvaEnchant",
            "SilvaEnchant",
            "GodSlayerEnchant",
            "GodSlayerEnchant",
            "DemonShadeEnchant",
            "DaedalusEnchant",
            "DaedalusEnchant",
            "BloodflareEnchant",
            "AtaxiaEnchant",
            "AtaxiaEnchant",
            "AerospecEnchant",
            "SilvaEnchant",
            "SilvaEnchant",
            "AuricEnchant",
            "MolluskEnchant",
            "MolluskEnchant",
            "MolluskEnchant",
            "SilvaEnchant",
            "DaedalusEnchant",
            "ReaverEnchant",
            "AstralEnchant",
            "AtaxiaEnchant",
            "XerocEnchant",
            "TarragonEnchant",
            "BloodflareEnchant",
            "GodSlayerEnchant",
            "SilvaEnchant",
            "AuricEnchant",
            "SharpshootersSoul",
            "VictideEnchant",
            "AerospecEnchant",
            "AerospecEnchant",
            "DaedalusEnchant",
            "OmegaBlueEnchant",
            "AerospecEnchant",
            "StatigelEnchant",
            "DaedalusEnchant",
            "DaedalusEnchant",
            "AtaxiaEnchant",
            "MolluskEnchant",
            "OmegaBlueEnchant",
            "GodSlayerEnchant",
            "SilvaEnchant",
            "SilvaEnchant",
            "DemonShadeEnchant"};

            string[] calamityColor = {
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff",
            "ffffff"};

            for (int x = 0; x < calamityTogNames.Length; x++)
            {
                text = CreateTranslation(calamityTogConfig[x]);
                text.SetDefault("[i:" + Instance.ItemType(calamityTogItems[x]) + "][c/" + calamityColor[x] + ": " + calamityTogNames[x] + "]");
                AddTranslation(text);
            }

            #endregion

            #endregion

            
        }

        public override void Unload()
        {
            if (DebuffIDs != null)
                DebuffIDs.Clear();
        }

        public override object Call(params object[] args)
        {
            if ((string)args[0] == "FargoSoulsAI")
            {
                /*int n = (int)args[1];
                Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>().AI(Main.npc[n]);*/
            }
            return base.Call(args);
        }

        //bool sheet
        public override void PostSetupContent()
        {
            try
            {
                FargosLoaded = ModLoader.GetMod("Fargowiltas") != null;
                BlueMagicLoaded = ModLoader.GetMod("Bluemagic") != null;
                CalamityLoaded = ModLoader.GetMod("CalamityMod") != null;
                ThoriumLoaded = ModLoader.GetMod("ThoriumMod") != null;
                AALoaded = ModLoader.GetMod("AAMod") != null;
                DBTLoaded = ModLoader.GetMod("DBZMOD") != null;
                SOALoaded = ModLoader.GetMod("SacredTools") != null;
                ApothLoaded = ModLoader.GetMod("ApothTestMod") != null;
                MasomodeEX = ModLoader.GetMod("MasomodeEX") != null;

                DebuffIDs = new List<int> { 20, 22, 23, 24, 36, 39, 44, 46, 47, 67, 68, 69, 70, 80,
                    88, 94, 103, 137, 144, 145, 148, 149, 156, 160, 163, 164, 195, 196, 197, 199 };
                DebuffIDs.Add(BuffType("Antisocial"));
                DebuffIDs.Add(BuffType("Atrophied"));
                DebuffIDs.Add(BuffType("Berserked"));
                DebuffIDs.Add(BuffType("Bloodthirsty"));
                DebuffIDs.Add(BuffType("ClippedWings"));
                DebuffIDs.Add(BuffType("Crippled"));
                DebuffIDs.Add(BuffType("CurseoftheMoon"));
                DebuffIDs.Add(BuffType("Defenseless"));
                DebuffIDs.Add(BuffType("FlamesoftheUniverse"));
                DebuffIDs.Add(BuffType("Flipped"));
                DebuffIDs.Add(BuffType("FlippedHallow"));
                DebuffIDs.Add(BuffType("Fused"));
                DebuffIDs.Add(BuffType("GodEater"));
                DebuffIDs.Add(BuffType("Guilty"));
                DebuffIDs.Add(BuffType("Hexed"));
                DebuffIDs.Add(BuffType("Infested"));
                DebuffIDs.Add(BuffType("IvyVenom"));
                DebuffIDs.Add(BuffType("Jammed"));
                DebuffIDs.Add(BuffType("Lethargic"));
                DebuffIDs.Add(BuffType("LightningRod"));
                DebuffIDs.Add(BuffType("LivingWasteland"));
                DebuffIDs.Add(BuffType("Lovestruck"));
                DebuffIDs.Add(BuffType("MarkedforDeath"));
                DebuffIDs.Add(BuffType("Midas"));
                DebuffIDs.Add(BuffType("MutantNibble"));
                DebuffIDs.Add(BuffType("NullificationCurse"));
                DebuffIDs.Add(BuffType("Oiled"));
                DebuffIDs.Add(BuffType("OceanicMaul"));
                DebuffIDs.Add(BuffType("Purified"));
                DebuffIDs.Add(BuffType("Recovering"));
                DebuffIDs.Add(BuffType("ReverseManaFlow"));
                DebuffIDs.Add(BuffType("Rotting"));
                DebuffIDs.Add(BuffType("Shadowflame"));
                DebuffIDs.Add(BuffType("SqueakyToy"));
                DebuffIDs.Add(BuffType("Stunned"));
                DebuffIDs.Add(BuffType("Swarming"));
                DebuffIDs.Add(BuffType("Unstable"));

                DebuffIDs.Add(BuffType("MutantFang"));
                DebuffIDs.Add(BuffType("MutantPresence"));

                DebuffIDs.Add(BuffType("TimeFrozen"));

                Mod bossChecklist = ModLoader.GetMod("BossChecklist");
                if (bossChecklist != null)
                {
                    bossChecklist.Call("AddBossWithInfo", "Duke Fishron EX", 14.01f, (Func<bool>)(() => FargoSoulsWorld.downedFishronEX), "Fish using a [i:" + ItemType("TruffleWormEX") + "]");
                    bossChecklist.Call("AddBossWithInfo", "Mutant", 14.02f, (Func<bool>)(() => FargoSoulsWorld.downedMutant), "Spawn by throwing [i:" + ItemType("AbominationnVoodooDoll") + "] in lava in Mutant's presence");
                }

                if (ThoriumLoaded)
                {
                    Mod thorium = ModLoader.GetMod("ThoriumMod");
                    ModProjDict.Add(thorium.ProjectileType("IFO"), 1);
                    ModProjDict.Add(thorium.ProjectileType("BioFeederPet"), 2);
                    ModProjDict.Add(thorium.ProjectileType("BlisterPet"), 3);
                    ModProjDict.Add(thorium.ProjectileType("WyvernPet"), 4);
                    ModProjDict.Add(thorium.ProjectileType("SupportLantern"), 5);
                    ModProjDict.Add(thorium.ProjectileType("LockBoxPet"), 6);
                    ModProjDict.Add(thorium.ProjectileType("Devil"), 7);
                    ModProjDict.Add(thorium.ProjectileType("Angel"), 8);
                    ModProjDict.Add(thorium.ProjectileType("LifeSpirit"), 9);
                    ModProjDict.Add(thorium.ProjectileType("HolyGoat"), 10);
                    ModProjDict.Add(thorium.ProjectileType("MinionSapling"), 11);
                    ModProjDict.Add(thorium.ProjectileType("SnowyOwlPet"), 12);
                    ModProjDict.Add(thorium.ProjectileType("JellyfishPet"), 13);
                    ModProjDict.Add(thorium.ProjectileType("LilMog"), 14);
                    ModProjDict.Add(thorium.ProjectileType("Maid1"), 15);
                    ModProjDict.Add(thorium.ProjectileType("PinkSlime"), 16);
                    ModProjDict.Add(thorium.ProjectileType("ShinyPet"), 17);
                    ModProjDict.Add(thorium.ProjectileType("DrachmaBag"), 18);
                }

                if (CalamityLoaded)
                {
                    Mod calamity = ModLoader.GetMod("CalamityMod");
                    ModProjDict.Add(calamity.ProjectileType("Kendra"), 101);
                    ModProjDict.Add(calamity.ProjectileType("PerforaMini"), 102);
                    ModProjDict.Add(calamity.ProjectileType("ThirdSage"), 103);
                    ModProjDict.Add(calamity.ProjectileType("Bear"), 104);
                    ModProjDict.Add(calamity.ProjectileType("Brimling"), 105);
                    ModProjDict.Add(calamity.ProjectileType("DannyDevito"), 106);
                    ModProjDict.Add(calamity.ProjectileType("SirenYoung"), 107);
                    ModProjDict.Add(calamity.ProjectileType("ChibiiDoggo"), 108);
                    ModProjDict.Add(calamity.ProjectileType("ChibiiDoggoFly"), 109);
                    ModProjDict.Add(calamity.ProjectileType("Akato"), 110);
                    ModProjDict.Add(calamity.ProjectileType("Fox"), 111);
                    ModProjDict.Add(calamity.ProjectileType("Levi"), 112);
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Log("FargowiltasSouls PostSetupContent Error: " + e.StackTrace + e.Message);
            }
        }

        public override void AddRecipes()
        {
            if (ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");

                ModRecipe recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelArrow"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelAxe"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelBattleAxe"), 10);
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelBlade"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelBow"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelChestplate"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelGreaves"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelHelmet"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelMallet"));
                recipe.AddRecipe();

                recipe = new ModRecipe(this);
                recipe.AddIngredient(thorium.ItemType("FoldedMetal"));
                recipe.AddTile(thorium, "ArcaneArmorFabricator");
                recipe.SetResult(thorium.ItemType("SteelPickaxe"));
                recipe.AddRecipe();
            }
        }

        public override void AddRecipeGroups()
        {
            //drax
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Drax", ItemID.Drax, ItemID.PickaxeAxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDrax", group);

            //cobalt
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Repeater", ItemID.CobaltRepeater, ItemID.PalladiumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltRepeater", group);

            //mythril
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Repeater", ItemID.MythrilRepeater, ItemID.OrichalcumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilRepeater", group);

            //adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Repeater", ItemID.AdamantiteRepeater, ItemID.TitaniumRepeater);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantiteRepeater", group);

            if (Instance.ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");

                //combo yoyos
                group = new RecipeGroup(() => Lang.misc[37] + " Combination Yoyo", thorium.ItemType("Nocturnal"), thorium.ItemType("Sanguine"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyThoriumYoyo", group);
            }

            if (Instance.CalamityLoaded)
            {
                Mod calamity = ModLoader.GetMod("CalamityMod");

                //aerospec
                group = new RecipeGroup(() => Lang.misc[37] + " Aerospec Helmet", calamity.ItemType("AerospecHat"), calamity.ItemType("AerospecHeadgear"), calamity.ItemType("AerospecHelm"), calamity.ItemType("AerospecHood"), calamity.ItemType("AerospecHelmet"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAerospecHelmet", group);
                //ataxia
                group = new RecipeGroup(() => Lang.misc[37] + " Ataxia Helmet", calamity.ItemType("AtaxiaHeadgear"), calamity.ItemType("AtaxiaHelm"), calamity.ItemType("AtaxiaHood"), calamity.ItemType("AtaxiaHelmet"), calamity.ItemType("AtaxiaMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAtaxiaHelmet", group);
                //auric
                group = new RecipeGroup(() => Lang.misc[37] + " Auric Helmet", calamity.ItemType("AuricTeslaHelm"), calamity.ItemType("AuricTeslaPlumedHelm"), calamity.ItemType("AuricTeslaHoodedFacemask"), calamity.ItemType("AuricTeslaSpaceHelmet"), calamity.ItemType("AuricTeslaWireHemmedVisage"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAuricHelmet", group);
                //bloodflare
                group = new RecipeGroup(() => Lang.misc[37] + " Bloodflare Helmet", calamity.ItemType("BloodflareHelm"), calamity.ItemType("BloodflareHelmet"), calamity.ItemType("BloodflareHornedHelm"), calamity.ItemType("BloodflareHornedMask"), calamity.ItemType("BloodflareMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBloodflareHelmet", group);
                //daedalus
                group = new RecipeGroup(() => Lang.misc[37] + " Daedalus Helmet", calamity.ItemType("DaedalusHelm"), calamity.ItemType("DaedalusHelmet"), calamity.ItemType("DaedalusHat"), calamity.ItemType("DaedalusHeadgear"), calamity.ItemType("DaedalusVisor"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDaedalusHelmet", group);
                //godslayer
                group = new RecipeGroup(() => Lang.misc[37] + " Godslayer Helmet", calamity.ItemType("GodSlayerHelm"), calamity.ItemType("GodSlayerHelmet"), calamity.ItemType("GodSlayerVisage"), calamity.ItemType("GodSlayerHornedHelm"), calamity.ItemType("GodSlayerMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGodslayerHelmet", group);
                //reaver
                group = new RecipeGroup(() => Lang.misc[37] + " Reaver Helmet", calamity.ItemType("ReaverHelm"), calamity.ItemType("ReaverVisage"), calamity.ItemType("ReaverMask"), calamity.ItemType("ReaverHelmet"), calamity.ItemType("ReaverCap"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyReaverHelmet", group);
                //silva
                group = new RecipeGroup(() => Lang.misc[37] + " Silva Helmet", calamity.ItemType("SilvaHelm"), calamity.ItemType("SilvaHornedHelm"), calamity.ItemType("SilvaMaskedCap"), calamity.ItemType("SilvaHelmet"), calamity.ItemType("SilvaMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnySilvaHelmet", group);
                //statigel
                group = new RecipeGroup(() => Lang.misc[37] + " Statigel Helmet", calamity.ItemType("StatigelHelm"), calamity.ItemType("StatigelHeadgear"), calamity.ItemType("StatigelCap"), calamity.ItemType("StatigelHood"), calamity.ItemType("StatigelMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyStatigelHelmet", group);
                //tarragon
                group = new RecipeGroup(() => Lang.misc[37] + " Tarragon Helmet", calamity.ItemType("TarragonHelm"), calamity.ItemType("TarragonVisage"), calamity.ItemType("TarragonMask"), calamity.ItemType("TarragonHornedHelm"), calamity.ItemType("TarragonHelmet"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTarragonHelmet", group);
                //victide
                group = new RecipeGroup(() => Lang.misc[37] + " Victide Helmet", calamity.ItemType("VictideHelm"), calamity.ItemType("VictideVisage"), calamity.ItemType("VictideMask"), calamity.ItemType("VictideHelmet"), calamity.ItemType("VictideHeadgear"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyVictideHelmet", group);
                //wulfrum
                group = new RecipeGroup(() => Lang.misc[37] + " Wulfrum Helmet", calamity.ItemType("WulfrumHelm"), calamity.ItemType("WulfrumHeadgear"), calamity.ItemType("WulfrumHood"), calamity.ItemType("WulfrumHelmet"), calamity.ItemType("WulfrumMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyWulfrumHelmet", group);
            }

            if (Instance.SOALoaded)
            {
                Mod soa = ModLoader.GetMod("SacredTools");

                //flarium
                group = new RecipeGroup(() => Lang.misc[37] + " Flarium Helmet", soa.ItemType("FlariumCowl"), soa.ItemType("FlariumHelmet"), soa.ItemType("FlariumHood"), soa.ItemType("FlariumCrown"), soa.ItemType("FlariumMask"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyFlariumHelmet", group);
                //asthraltite
                group = new RecipeGroup(() => Lang.misc[37] + " Asthraltite Helmet", soa.ItemType("AsthralMelee"), soa.ItemType("AsthralRanged"), soa.ItemType("AsthralMage"), soa.ItemType("AsthralSummon"), soa.ItemType("AsthralThrown"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAstralHelmet", group);
            }

            //evil wood
            group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood", ItemID.Ebonwood, ItemID.Shadewood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyEvilWood", group);

            //anvil HM
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Anvil", ItemID.MythrilAnvil, ItemID.OrichalcumAnvil);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAnvil", group);

            //forge HM
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Forge", ItemID.AdamantiteForge, ItemID.TitaniumForge);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyForge", group);

            //any adamantite
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Bar", ItemID.AdamantiteBar, ItemID.TitaniumBar);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamantite", group);

            //shroomite head
            group = new RecipeGroup(() => Lang.misc[37] + " Shroomite Head Piece", ItemID.ShroomiteHeadgear, ItemID.ShroomiteMask, ItemID.ShroomiteHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyShroomHead", group);

            //orichalcum head
            group = new RecipeGroup(() => Lang.misc[37] + " Orichalcum Head Piece", ItemID.OrichalcumHeadgear, ItemID.OrichalcumMask, ItemID.OrichalcumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyOriHead", group);

            //palladium head
            group = new RecipeGroup(() => Lang.misc[37] + " Palladium Head Piece", ItemID.PalladiumHeadgear, ItemID.PalladiumMask, ItemID.PalladiumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPallaHead", group);

            //cobalt head
            group = new RecipeGroup(() => Lang.misc[37] + " Cobalt Head Piece", ItemID.CobaltHelmet, ItemID.CobaltHat, ItemID.CobaltMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyCobaltHead", group);

            //mythril head
            group = new RecipeGroup(() => Lang.misc[37] + " Mythril Head Piece", ItemID.MythrilHat, ItemID.MythrilHelmet, ItemID.MythrilHood);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyMythrilHead", group);

            //titanium head
            group = new RecipeGroup(() => Lang.misc[37] + " Titanium Head Piece", ItemID.TitaniumHeadgear, ItemID.TitaniumMask, ItemID.TitaniumHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTitaHead", group);

            //hallowed head
            group = new RecipeGroup(() => Lang.misc[37] + " Hallowed Head Piece", ItemID.HallowedMask, ItemID.HallowedHeadgear, ItemID.HallowedHelmet);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyHallowHead", group);

            //adamantite head
            group = new RecipeGroup(() => Lang.misc[37] + " Adamantite Head Piece", ItemID.AdamantiteHelmet, ItemID.AdamantiteMask, ItemID.AdamantiteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyAdamHead", group);

            //chloro head
            group = new RecipeGroup(() => Lang.misc[37] + " Chlorophyte Head Piece", ItemID.ChlorophyteMask, ItemID.ChlorophyteHelmet, ItemID.ChlorophyteHeadgear);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyChloroHead", group);

            //spectre head
            group = new RecipeGroup(() => Lang.misc[37] + " Spectre Head Piece", ItemID.SpectreHood, ItemID.SpectreMask);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnySpectreHead", group);

            //book cases
            group = new RecipeGroup(() => Lang.misc[37] + " Wooden Bookcase", ItemID.Bookcase, ItemID.EbonwoodBookcase, ItemID.RichMahoganyBookcase, ItemID.LivingWoodBookcase,
                ItemID.ShadewoodBookcase, ItemID.PalmWoodBookcase, ItemID.BorealWoodBookcase);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBookcase", group);

            //beetle body
            group = new RecipeGroup(() => Lang.misc[37] + " Beetle Body", ItemID.BeetleShell, ItemID.BeetleScaleMail);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyBeetle", group);

            //phasesabers
            group = new RecipeGroup(() => Lang.misc[37] + " Phasesaber", ItemID.RedPhasesaber, ItemID.BluePhasesaber, ItemID.GreenPhasesaber, ItemID.PurplePhasesaber, ItemID.WhitePhasesaber,
                ItemID.YellowPhasesaber);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyPhasesaber", group);

            //vanilla butterflies
            group = new RecipeGroup(() => Lang.misc[37] + " Butterfly", ItemID.JuliaButterfly, ItemID.MonarchButterfly, ItemID.PurpleEmperorButterfly,
                ItemID.RedAdmiralButterfly, ItemID.SulphurButterfly, ItemID.TreeNymphButterfly, ItemID.UlyssesButterfly, ItemID.ZebraSwallowtailButterfly);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyButterfly", group);

            group = new RecipeGroup(() => Lang.misc[37] + " Gold Pickaxe", ItemID.GoldPickaxe, ItemID.PlatinumPickaxe);
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyGoldPickaxe", group);

            if (ThoriumLoaded)
            {
                Mod thorium = ModLoader.GetMod("ThoriumMod");

                //jester mask
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Mask", thorium.ItemType("JestersMask"), thorium.ItemType("JestersMask2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterMask", group);
                //jester shirt
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Shirt", thorium.ItemType("JestersShirt"), thorium.ItemType("JestersShirt2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterShirt", group);
                //jester legging
                group = new RecipeGroup(() => Lang.misc[37] + " Jester Leggings", thorium.ItemType("JestersLeggings"), thorium.ItemType("JestersLeggings2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyJesterLeggings", group);
                //evil wood tambourine
                group = new RecipeGroup(() => Lang.misc[37] + " Evil Wood Tambourine", thorium.ItemType("EbonWoodTambourine"), thorium.ItemType("ShadeWoodTambourine"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyTambourine", group);
                //fan letter
                group = new RecipeGroup(() => Lang.misc[37] + " Fan Letter", thorium.ItemType("FanLetter"), thorium.ItemType("FanLetter2"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyLetter", group);

                //butterflies
                group = new RecipeGroup(() => Lang.misc[37] + " Dungeon Butterfly", thorium.ItemType("BlueDungeonButterfly"), thorium.ItemType("GreenDungeonButterfly"), thorium.ItemType("PinkDungeonButterfly"));
                RecipeGroup.RegisterGroup("FargowiltasSouls:AnyDungeonButterfly", group);
            }
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            switch (reader.ReadByte())
            {
                case 0: //server side spawning creepers
                    if (Main.netMode == 2)
                    {
                        byte p = reader.ReadByte();
                        int multiplier = reader.ReadByte();
                        int n = NPC.NewNPC((int)Main.player[p].Center.X, (int)Main.player[p].Center.Y, NPCType("CreeperGutted"), 0,
                            p, 0f, multiplier, 0f);
                        if (n != 200)
                        {
                            Main.npc[n].velocity = Vector2.UnitX.RotatedByRandom(2 * Math.PI) * 8;
                            NetMessage.SendData(23, -1, -1, null, n);
                        }
                    }
                    break;

                case 1: //server side synchronize pillar data request
                    if (Main.netMode == 2)
                    {
                        byte pillar = reader.ReadByte();
                        if (!Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[1])
                        {
                            Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[1] = true;
                            Main.npc[pillar].GetGlobalNPC<FargoSoulsGlobalNPC>().SetDefaults(Main.npc[pillar]);
                            Main.npc[pillar].life = Main.npc[pillar].lifeMax;
                        }
                    }
                    break;

                case 2: //net updating maso
                    FargoSoulsGlobalNPC fargoNPC = Main.npc[reader.ReadByte()].GetGlobalNPC<FargoSoulsGlobalNPC>();
                    fargoNPC.masoBool[0] = reader.ReadBoolean();
                    fargoNPC.masoBool[1] = reader.ReadBoolean();
                    fargoNPC.masoBool[2] = reader.ReadBoolean();
                    fargoNPC.masoBool[3] = reader.ReadBoolean();
                    break;

                case 3: //rainbow slime/paladin, MP clients syncing to server
                    if (Main.netMode == 1)
                    {
                        byte npc = reader.ReadByte();
                        Main.npc[npc].lifeMax = reader.ReadInt32();
                        float newScale = reader.ReadSingle();
                        Main.npc[npc].position = Main.npc[npc].Center;
                        Main.npc[npc].width = (int)(Main.npc[npc].width / Main.npc[npc].scale * newScale);
                        Main.npc[npc].height = (int)(Main.npc[npc].height / Main.npc[npc].scale * newScale);
                        Main.npc[npc].scale = newScale;
                        Main.npc[npc].Center = Main.npc[npc].position;
                    }
                    break;

                case 4: //moon lord vulnerability synchronization
                    if (Main.netMode == 1)
                    {
                        int ML = reader.ReadByte();
                        Main.npc[ML].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter = reader.ReadInt32();
                        FargoSoulsGlobalNPC.masoStateML = reader.ReadByte();
                    }
                    break;

                case 5: //retinazer laser MP sync
                    if (Main.netMode == 1)
                    {
                        int reti = reader.ReadByte();
                        Main.npc[reti].GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[2] = reader.ReadBoolean();
                        Main.npc[reti].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter = reader.ReadInt32();
                    }
                    break;

                case 6: //shark MP sync
                    if (Main.netMode == 1)
                    {
                        int shark = reader.ReadByte();
                        Main.npc[shark].GetGlobalNPC<FargoSoulsGlobalNPC>().SharkCount = reader.ReadByte();
                    }
                    break;

                case 7: //client to server activate dark caster family
                    if (Main.netMode == 2)
                    {
                        int caster = reader.ReadByte();
                        if (Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 == 0)
                            Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 = reader.ReadInt32();
                    }
                    break;

                case 8: //server to clients reset counter
                    if (Main.netMode == 1)
                    {
                        int caster = reader.ReadByte();
                        Main.npc[caster].GetGlobalNPC<FargoSoulsGlobalNPC>().Counter2 = 0;
                    }
                    break;

                case 9: //client to server, request heart spawn
                    if (Main.netMode == 2)
                    {
                        int n = reader.ReadByte();
                        Item.NewItem(Main.npc[n].Hitbox, ItemID.Heart);
                    }
                    break;

                case 10: //client to server, sync cultist data
                    if (Main.netMode == 2)
                    {
                        int cult = reader.ReadByte();
                        FargoSoulsGlobalNPC cultNPC = Main.npc[cult].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        cultNPC.Counter += reader.ReadInt32();
                        cultNPC.Counter2 += reader.ReadInt32();
                        cultNPC.Timer += reader.ReadInt32();
                        Main.npc[cult].localAI[3] += reader.ReadSingle();
                    }
                    break;

                case 11: //refresh creeper
                    if (Main.netMode != 0)
                    {
                        byte player = reader.ReadByte();
                        NPC creeper = Main.npc[reader.ReadByte()];
                        if (creeper.active && creeper.type == NPCType("CreeperGutted") && creeper.ai[0] == player)
                        {
                            int damage = creeper.lifeMax - creeper.life;
                            creeper.life = creeper.lifeMax;
                            if (damage > 0)
                                CombatText.NewText(creeper.Hitbox, CombatText.HealLife, damage);
                            if (Main.netMode == 2)
                                creeper.netUpdate = true;
                        }
                    }
                    break;

                case 12: //prime limbs spin
                    if (Main.netMode == 1)
                    {
                        int n = reader.ReadByte();
                        FargoSoulsGlobalNPC limb = Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        limb.masoBool[2] = reader.ReadBool();
                        limb.Counter = reader.ReadInt();
                        Main.npc[n].localAI[3] = reader.ReadFloat();
                    }
                    break;

                case 13: //prime limbs swipe
                    if (Main.netMode == 1)
                    {
                        int n = reader.ReadByte();
                        FargoSoulsGlobalNPC limb = Main.npc[n].GetGlobalNPC<FargoSoulsGlobalNPC>();
                        limb.Counter = reader.ReadInt();
                        limb.Counter2 = reader.ReadInt();
                    }
                    break;

                case 77: //server side spawning fishron EX
                    if (Main.netMode == 2)
                    {
                        byte target = reader.ReadByte();
                        int x = reader.ReadInt32();
                        int y = reader.ReadInt32();
                        FargoSoulsGlobalNPC.spawnFishronEX = true;
                        NPC.NewNPC(x, y, NPCID.DukeFishron, 0, 0f, 0f, 0f, 0f, target);
                        FargoSoulsGlobalNPC.spawnFishronEX = false;
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Duke Fishron EX has awoken!"), new Color(50, 100, 255));
                    }
                    break;

                case 78: //confirming fish EX max life
                    int f = reader.ReadInt32();
                    Main.npc[f].lifeMax = reader.ReadInt32();
                    break;

                default:
                    break;
            }

            //BaseMod Stuff
            MsgType msg = (MsgType)reader.ReadByte();
            if (msg == MsgType.ProjectileHostility) //projectile hostility and ownership
            {
                int owner = reader.ReadInt32();
                int projID = reader.ReadInt32();
                bool friendly = reader.ReadBoolean();
                bool hostile = reader.ReadBoolean();
                if (Main.projectile[projID] != null)
                {
                    Main.projectile[projID].owner = owner;
                    Main.projectile[projID].friendly = friendly;
                    Main.projectile[projID].hostile = hostile;
                }
                if (Main.netMode == 2) MNet.SendBaseNetMessage(0, owner, projID, friendly, hostile);
            }
            else
            if (msg == MsgType.SyncAI) //sync AI array
            {
                int classID = reader.ReadByte();
                int id = reader.ReadInt16();
                int aitype = reader.ReadByte();
                int arrayLength = reader.ReadByte();
                float[] newAI = new float[arrayLength];
                for (int m = 0; m < arrayLength; m++)
                {
                    newAI[m] = reader.ReadSingle();
                }
                if (classID == 0 && Main.npc[id] != null && Main.npc[id].active && Main.npc[id].modNPC != null && Main.npc[id].modNPC is ParentNPC)
                {
                    ((ParentNPC)Main.npc[id].modNPC).SetAI(newAI, aitype);
                }
                else
                if (classID == 1 && Main.projectile[id] != null && Main.projectile[id].active && Main.projectile[id].modProjectile != null && Main.projectile[id].modProjectile is ParentProjectile)
                {
                    ((ParentProjectile)Main.projectile[id].modProjectile).SetAI(newAI, aitype);
                }
                if (Main.netMode == 2) BaseNet.SyncAI(classID, id, newAI, aitype);
            }
        }

        public override void UpdateMusic(ref int music, ref MusicPriority priority)
        {
            if (Main.musicVolume != 0 && Main.myPlayer != -1 && !Main.gameMenu && Main.LocalPlayer.active)
            {
                if (MMWorld.MMArmy && priority <= MusicPriority.Environment)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/MonsterMadhouse");
                    priority = MusicPriority.Event;
                }
            }
        }

        public static bool NoInvasion(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.invasion && (!Main.pumpkinMoon && !Main.snowMoon || spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) &&
                   (!Main.eclipse || spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime);
        }

        public static bool NoBiome(NPCSpawnInfo spawnInfo)
        {
            Player player = spawnInfo.player;
            return !player.ZoneJungle && !player.ZoneDungeon && !player.ZoneCorrupt && !player.ZoneCrimson && !player.ZoneHoly && !player.ZoneSnow && !player.ZoneUndergroundDesert;
        }

        public static bool NoZoneAllowWater(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.sky && !spawnInfo.player.ZoneMeteor && !spawnInfo.spiderCave;
        }

        public static bool NoZone(NPCSpawnInfo spawnInfo)
        {
            return NoZoneAllowWater(spawnInfo) && !spawnInfo.water;
        }

        public static bool NormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return !spawnInfo.playerInTown && NoInvasion(spawnInfo);
        }

        public static bool NoZoneNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZone(spawnInfo);
        }

        public static bool NoZoneNormalSpawnAllowWater(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoZoneAllowWater(spawnInfo);
        }

        public static bool NoBiomeNormalSpawn(NPCSpawnInfo spawnInfo)
        {
            return NormalSpawn(spawnInfo) && NoBiome(spawnInfo) && NoZone(spawnInfo);
        }
    }

    enum MsgType : byte
    {
        ProjectileHostility,
        SyncAI
    }
}
