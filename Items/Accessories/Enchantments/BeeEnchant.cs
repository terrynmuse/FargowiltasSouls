using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class BeeEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bee Enchantment");
			Tooltip.SetDefault("'According to all known laws of aviation, there is no way a bee should be able to fly' \n" +
								"5% increased minion damage \n" +
								"Increases the strength of friendly bees \n" +
								"Any damage you deal has a chance to spawn additional bees\n" +
								"Summons a pet Baby Hornet");
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
			
			if(Soulcheck.GetValue("Bees on Hit"))
			{
				modPlayer.beeEnchant = true;
			}
			
			player.minionDamage += 0.05f;
			player.strongBees = true;  	
			
			//pet
			if (player.whoAmI == Main.myPlayer)
            {
				if(Soulcheck.GetValue("Baby Hornet Pet"))
				{
					modPlayer.beePet = true;
					
					if(player.FindBuffIndex(51) == -1)
					{
						if (player.ownedProjectileCounts[ProjectileID.BabyHornet] < 1)
						{
							Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.BabyHornet, 0, 2f, Main.myPlayer, 0f, 0f);
						}
					}
				}
				else
				{
					modPlayer.beePet = false;
				}
            }
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeeHeadgear);
			recipe.AddIngredient(ItemID.BeeBreastplate);
			recipe.AddIngredient(ItemID.BeeGreaves);
			recipe.AddIngredient(ItemID.HiveBackpack);
			recipe.AddIngredient(ItemID.Nectar);
			
			recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}	
}
		
