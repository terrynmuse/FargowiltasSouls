
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
            //enchantToggles.Add("Boreal Snowballs", terrmenu.terenchmenu.borealsnow);

        }
        public bool GetValue(string input)
        {
            return enchantToggles[input];
        }
    }
}
