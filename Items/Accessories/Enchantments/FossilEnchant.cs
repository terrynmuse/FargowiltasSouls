using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class FossilEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fossil Enchantment");
			Tooltip.SetDefault(
@"'Beyond a forgotten age'
You cheat death, returning with 20 HP
5 minute cooldown
Summons a pet Baby Dino");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;			
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 1; 
			item.value = 20000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			modPlayer.FossilEnchant = true;
			
			//pet
			if (player.whoAmI == Main.myPlayer)
            {
				if(Soulcheck.GetValue("Baby Dino Pet"))
				{
					modPlayer.DinoPet = true;
					
					if(player.FindBuffIndex(61) == -1)
					{
						if (player.ownedProjectileCounts[ProjectileID.BabyDino] < 1)
						{
							Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.BabyDino, 0, 2f, Main.myPlayer);
						}
					}
				}
				else
				{
					modPlayer.DinoPet = false;
				}
			}
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(ItemID.FossilShirt);
			recipe.AddIngredient(ItemID.FossilPants);
			recipe.AddIngredient(ItemID.BoneJavelin, 100);
			recipe.AddIngredient(ItemID.AntlionMandible);
			recipe.AddIngredient(ItemID.AmberMosquito);
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
		
