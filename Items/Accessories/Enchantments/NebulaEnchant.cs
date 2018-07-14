using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class NebulaEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nebula Enchantment");
			Tooltip.SetDefault("'The pillars of creation have shined upon you' \n15% increased magic damage \nHurting enemies has a chance to spawn buff boosters");
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
			if (player.nebulaCD > 0)
			{
				player.nebulaCD--;
			}
				player.setNebula = true;
				
				player.magicDamage += 0.15f;
			
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.NebulaHelmet);
			recipe.AddIngredient(ItemID.NebulaBreastplate);
			recipe.AddIngredient(ItemID.NebulaLeggings);
			recipe.AddIngredient(ItemID.NebulaArcanum);
			recipe.AddIngredient(ItemID.NebulaBlaze);
			recipe.AddIngredient(ItemID.LunarFlareBook);
			recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
		
		
}
		
	





