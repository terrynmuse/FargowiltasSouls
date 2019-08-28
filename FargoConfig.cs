
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
                [Header("[i:3509] Terra Force")]

                [Label("[i:20][c/d56617: Copper Lightning]")]
                public bool coplight = true;
                [Label("[i:22][c/988e83: Iron Magnet]")]
                [Header("$Mods.FargowiltasSouls.ConfigHeader")]
                public bool ironmag = true;
                [Label("[i:22][c/988e83: Iron Shield]")]
                public bool ironshield = true;
                [Label("[i:703][c/a28b4e: Tin Crits]")]
                public bool tincrit = true;
                [Label("[i:705][c/b0d2b2: Tungsten Effect]")]
                public bool tung = true;
                [Header("[i:4] Force of Will")]
                [Label("[i:3094][c/9c924e: Gladiator Rain]")]
                public bool gladrain = true;
                [Label("[i:19][c/e7b21c: Gold Lucky Coin]")]
                public bool goldcoin = true;
                [Label("[i:3877][c/c01b3c: Red Riding Super Bleed]")]
                public bool redbleed = true;
                [Label("[i:3871][c/93651e: Valhalla Knockback]")]
                public bool valhalknock = true;
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
            //enchantToggles.Add("Boreal Snowballs", terrmenu.terenchmenu.borealsnow);

        }
        public bool GetValue(string input)
        {
            return enchantToggles[input];
        }
    }
}
