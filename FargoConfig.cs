
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
        [Label("test effect")]
        public bool loltesto = true;
        [Label("Terraria")]
        public TerraMenu terrmenu = new TerraMenu();
        [Label("Calamity")]
        public CalamMenu calamenu = new CalamMenu();
        [Label("Thorium")]
        public ThorMenu thoriummenu = new ThorMenu();
        [SeparatePage]
        public class TerraMenu
        {
            public bool loltesta = true;
            [Label("Enchants")]
            public TerEncMenu terenchmenu = new TerEncMenu();
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










        //enchantToggles.Add("Boreal Snowballs", terrmenu.terenchmenu.borealsnow);

    }
    public bool GetValue(string input)
        {
            return enchantToggles[input];
        }
    }
}
