using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class CobaltEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cobalt Enchantment");
			Tooltip.SetDefault(
@"'I can't believe it's not Palladium' 
50% chance for your projectiles to explode into shards");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 4; 
			item.value = 40000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			(player.GetModPlayer<FargoPlayer>(mod)).cobaltEnchant = true;
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyCobaltHead");
			recipe.AddIngredient(ItemID.CobaltBreastplate);
			recipe.AddIngredient(ItemID.CobaltLeggings);
			recipe.AddIngredient(ItemID.CrystalStorm);
            recipe.AddIngredient(ItemID.CrystalVileShard);
            recipe.AddIngredient(ItemID.Chik);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
		
	





