using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class VortexEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Vortex Enchantment");
			Tooltip.SetDefault(
@"'Tear into reality'
15% increased ranged damage
Sets your ranged critical strike chance to 4%
Every crit will increase it by 4%
Getting hit drops your crit back down");
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
			player.rangedDamage+= .15f;
			player.setVortex = true;
			//player.setVortex = true; why u no work
			//player.vortexStealthActive = true;
			
			
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			modPlayer.VortexEnchant = true;
			player.rangedCrit = FargoPlayer.VortexCrit;
			
			/*if ((player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15) 
                {
                    float num29 = player.stealth;
                    player.stealth -= 0.04f;
                    if (player.stealth < 0f)
                    {
                        player.stealth = 0f;
                    }

                    player.rangedDamage += (1f - player.stealth) * 0.8f;
                    player.rangedCrit += (int)((1f - player.stealth) * 20f);
                    player.aggro -= (int)((1f - player.stealth) * 1200f);
                    player.moveSpeed *= 0.3f;

                    if (player.mount.Active)
                    {
                        player.vortexStealthActive = false;
                    }
				}   
			}*/
			
        }
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexHelmet);
			recipe.AddIngredient(ItemID.VortexBreastplate);
			recipe.AddIngredient(ItemID.VortexLeggings);
			recipe.AddIngredient(ItemID.VortexBeater);
			recipe.AddIngredient(ItemID.Phantasm);
			recipe.AddIngredient(ItemID.FireworksLauncher);
			recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}