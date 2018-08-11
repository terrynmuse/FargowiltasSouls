using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class NebulaEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Enchantment");
			Tooltip.SetDefault(
@"'The pillars of creation have shined upon you'
Hurting enemies has a chance to spawn buff boosters
Once you get to the last tier with each booster type, your magic power hits obscene levels");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 10; 
			item.value = 400000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.NebulaEffect();
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NebulaHelmet);
			recipe.AddIngredient(ItemID.NebulaBreastplate);
			recipe.AddIngredient(ItemID.NebulaLeggings);
            recipe.AddIngredient(ItemID.ShadowbeamStaff);
            recipe.AddIngredient(ItemID.NebulaArcanum);
			recipe.AddIngredient(ItemID.NebulaBlaze);
			recipe.AddIngredient(ItemID.LunarFlareBook);
			recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}	
}