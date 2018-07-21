using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class ChlorophyteEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chlorophyte Enchantment");
			Tooltip.SetDefault(
@"'The jungle's essence crystallizes above you'
Summons a leaf crystal to shoot at nearby enemies
All herb collection is doubled
Summons a pet Seedling");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 7; 
			item.value = 150000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            modPlayer.ChloroEnchant = true;
            modPlayer.AddMinion("Leaf Crystal", mod.ProjectileType("Chlorofuck"), 100, 10f);
            modPlayer.AddPet("Seedling Pet", BuffID.PetSapling, ProjectileID.Sapling);
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyChloroHead");
			recipe.AddIngredient(ItemID.ChlorophytePlateMail);
			recipe.AddIngredient(ItemID.ChlorophyteGreaves);
			recipe.AddIngredient(ItemID.StaffofRegrowth);
            recipe.AddIngredient(ItemID.LeafBlower);
            recipe.AddIngredient(ItemID.Seedling);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
