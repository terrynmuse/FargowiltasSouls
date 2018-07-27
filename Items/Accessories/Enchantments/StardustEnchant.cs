using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class StardustEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stardust Enchantment");
			Tooltip.SetDefault(
@"'The power of the Stand is yours' 
Double tap down to direct your guardian
When you do, you freeze time temporarily");
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
			if(Soulcheck.GetValue("Stardust Guardian"))
			{
			    player.setStardust = true;
			    if (player.whoAmI == Main.myPlayer)
			    {
			    	if (player.FindBuffIndex(187) == -1)
			    	{
			    		player.AddBuff(187, 3600);
			    	}
			    	if (player.ownedProjectileCounts[623] < 1)
			    	{
			    		Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, 623, 0, 0f, Main.myPlayer);
			    	}
			    }
			}
			
			player.GetModPlayer<FargoPlayer>(mod).StardustEnchant = true;
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StardustHelmet);
			recipe.AddIngredient(ItemID.StardustBreastplate);
			recipe.AddIngredient(ItemID.StardustLeggings);
			recipe.AddIngredient(ItemID.StardustCellStaff);
			recipe.AddIngredient(ItemID.StardustDragonStaff);
			recipe.AddIngredient(ItemID.RainbowCrystalStaff);
			recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}	
}