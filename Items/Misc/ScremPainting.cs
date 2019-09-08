using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;

namespace FargowiltasSouls.Items.Misc
{
    public class ScremPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Screm Painting");
            Tooltip.SetDefault("'Merry N. Tuse'");
            DisplayName.AddTranslation(GameCulture.Chinese, "尖叫猫猫");
            Tooltip.AddTranslation(GameCulture.Chinese, "Merry N. Tuse");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 11;
            item.createTile = mod.TileType("ScremPaintingSheet");
        }
    }
}