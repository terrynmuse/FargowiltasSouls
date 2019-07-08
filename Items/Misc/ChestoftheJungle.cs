using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class KeyoftheJungle : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chest of the Jungle");
            Tooltip.SetDefault(@"'Charged with the essence of many souls'");
            DisplayName.AddTranslation(GameCulture.Chinese, "丛林之箱");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'充满了许多灵魂精华'");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 0;
            item.useStyle = 4;
            item.useAnimation = 20;
            item.useTime = 20;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                Main.PlaySound(15, player.Center, 0);
                if (Main.netMode != 1)
                {//NPC.SpawnOnPlayer(player.whoAmI, NPCID.BigMimicJungle);
                    int n = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 300, NPCID.BigMimicJungle);
                    if (n != 200 && Main.netMode == 2)
                        NetMessage.SendData(23, -1, -1, null, n);
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.SoulofLight, 7);
            recipe.AddIngredient(ItemID.SoulofNight, 7);
            recipe.AddIngredient(ItemID.PurificationPowder);
            recipe.AddIngredient(mod.ItemType("VolatileEnergy"));
            recipe.AddIngredient(ItemID.Chest);

            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
