using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class VolatileEnergy : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Volatile Energy");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.rare = 7;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(this, 50);
            recipe.AddIngredient(ItemID.SoulofLight, 100);
            recipe.AddIngredient(ItemID.HallowedBar, 5);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(ItemID.RodofDiscord);
            recipe.AddRecipe();
        }
    }
}
