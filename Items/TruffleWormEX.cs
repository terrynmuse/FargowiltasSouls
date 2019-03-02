using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items
{
    public class TruffleWormEX : ModItem
    {
        public override string Texture => "Terraria/Item_2673";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Truffle Worm EX");
            Tooltip.SetDefault("Only usable in Masochist Mode");
        }

        public override void SetDefaults()
        {
            item.maxStack = 20;
            item.rare = 1;
            item.width = 12;
            item.height = 12;
            item.bait = 777;
            item.value = Item.sellPrice(0, 15, 0, 0);
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.TruffleWorm);
            recipe.AddIngredient(mod.ItemType("LunarCrystal"));

            recipe.AddTile(mod.TileType("CrucibleCosmosSheet"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}