using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
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
            item.rare = 11;
            item.width = 12;
            item.height = 12;
            item.consumable = true;
            item.bait = 777;
            /*item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;*/
            item.value = Item.sellPrice(0, 17, 0, 0);
        }

        /*public override bool CanUseItem(Player player)
        {
            return FargoWorld.MasochistMode;
        }

        public override bool UseItem(Player player)
        {
            FargoWorld.FishronEX = !FargoWorld.FishronEX;
            string text = FargoWorld.FishronEX ? "The ocean stirs..." : "The ocean settles.";
            Color color = new Color(0, 100, 200);
            if (Main.netMode == 0)
            {
                Main.NewText(text, color);
            }
            else if (Main.netMode == 2)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), color);
                NetMessage.SendData(7);
            }
            return true;
        }*/

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
            recipe.AddIngredient(ItemID.ShrimpyTruffle);
            recipe.AddIngredient(mod.ItemType("LunarCrystal"), 5);

            recipe.AddTile(mod.TileType("CrucibleCosmosSheet"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}