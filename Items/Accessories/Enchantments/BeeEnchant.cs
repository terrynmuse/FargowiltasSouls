using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class BeeEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Enchantment");
			Tooltip.SetDefault(
@"'According to all known laws of aviation, there is no way a bee should be able to fly'
Increases the strength of friendly bees
Bees ignore enemies defense
Summons a pet Baby Hornet");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;			
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 3; 
			item.value = 20000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            modPlayer.BeeEnchant = true;
			player.strongBees = true;

            modPlayer.AddPet("Baby Hornet Pet", BuffID.BabyHornet, ProjectileID.BabyHornet);
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeeHeadgear);
			recipe.AddIngredient(ItemID.BeeBreastplate);
			recipe.AddIngredient(ItemID.BeeGreaves);
			recipe.AddIngredient(ItemID.HiveBackpack);
            recipe.AddIngredient(ItemID.BeeGun);
            recipe.AddIngredient(ItemID.Nectar);
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}	
}
		
