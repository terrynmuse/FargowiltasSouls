using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class ChlorophyteEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chlorophyte Enchantment");
			Tooltip.SetDefault("'The jungle's essence crystallizes above you' \n" +
                                "Grants poison and venom immunity as well as on hit \n" +
                                "Summons a modified leaf crystal to shoot at nearby enemies \n" +
                                "The leaf crystal shoots slower with slighlty more damage\n" +
                                "All herb collection is doubled");
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
			if(Soulcheck.GetValue("Leaf Crystal") == true)
			{
			modPlayer.chloroEnchant = true;
			
			if (player.whoAmI == Main.myPlayer)
            {
				if (player.ownedProjectileCounts[mod.ProjectileType("Chlorofuck")] < 1)
				{
					Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("Chlorofuck"), 0, 0f, Main.myPlayer, 0f, 0f);
				}
            }
			}
			
			player.buffImmune[20] = true; //Poisoned
			player.buffImmune[70] = true; //Venom
			
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyChloroHead");
			recipe.AddIngredient(ItemID.ChlorophytePlateMail);
			recipe.AddIngredient(ItemID.ChlorophyteGreaves);
			recipe.AddIngredient(ItemID.JungleRose);
			recipe.AddIngredient(ItemID.LeafWings);
			recipe.AddIngredient(ItemID.LeafBlower);
			recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}
