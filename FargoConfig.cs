
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
        public Dictionary<string, bool> enchantToggles = new Dictionary<string, bool>()
        {
            {"Boreal Snowballs", true}
        };
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
                public bool borealsnow = true;
                [Label("[i:619][c/645a8d: Ebonwood Shadowflame]")]
                public bool ebonflame = true;
                [Label("[i:620][c/b56c64: Mahogany Hook Speed]")]
                public bool mahoganyhook = true;
                [Label("[i:2504][c/b78d56: Palmwood Sentry]")]
                public bool palmsentry = true;
                [Label("[i:621][c/ad9a5f: Pearlwood Rainbow]")]
                public bool pearltrail = true;
                [Header("[i:3] Force of Earth")]
                [Label("[i:391][c/dd557d: Adamantite Projectile Splitting]")]
                public bool adamsplit = true;
                [Label("[i:381][c/3da4c4: Cobalt Shards]")]
                public bool cobaltshards = true;
                [Label("[i:382][c/9dd290: Mythril Weapon Speed]")]
                public bool mythspeed = true;
                [Label("[i:1191][c/eb3291: Orichalcum Fireballs]")]
                public bool orifire = true;
                [Label("[i:1184][c/f5ac28: Palladium Healing]")]
                public bool palheal = true;
                [Label("[i:1198][c/828c88: Titanium Shadow Dodge]")]
                public bool titdodge = true;
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
                    SoulConfig.Instance.enchantToggles["Boreal Snowballs"] = borealsnow;
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


        }
    }
}
