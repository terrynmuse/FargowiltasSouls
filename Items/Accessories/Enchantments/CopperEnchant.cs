using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class CopperEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Copper Enchantment");
			Tooltip.SetDefault(
@"'Behold'
Attacks have a chance to shock enemies
If an enemy is wet, the chance and damage is increased");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;			
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 8; //
			item.value = 200000; //
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            (player.GetModPlayer<FargoPlayer>(mod)).copperEnchant = true;
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.CopperHelmet);
			recipe.AddIngredient(ItemID.CopperChainmail);
			recipe.AddIngredient(ItemID.CopperGreaves);
			recipe.AddIngredient(ItemID.Wire, 10);
			recipe.AddIngredient(ItemID.AmethystStaff);			
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
		
