using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
	public class BeetleEnchant : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Beetle Enchantment");
			Tooltip.SetDefault(
@"'The unseen life of dung courses through your veins'
Beetles protect you from damage
Your wings last 1.5x as long");
		}

		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.accessory = true;			
			ItemID.Sets.ItemNoGravity[item.type] = true;
			item.rare = 8; 
			item.value = 250000; 
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            (player.GetModPlayer<FargoPlayer>(mod)).beetleEnchant = true;
		
		    if(Soulcheck.GetValue("Beetles") == true)
		    {
				player.beetleDefense = true;
				player.beetleCounter += 1f;
				int num5 = 180;
				if (player.beetleCounter >= (float)num5)
				{
					if (player.beetleOrbs > 0 && player.beetleOrbs < 3)
					{
						for (int k = 0; k < 22; k++)
						{
							if (player.buffType[k] >= 95 && player.buffType[k] <= 96)
							{
								player.DelBuff(k);
							}
						}
					}
					if (player.beetleOrbs < 3)
					{
						player.AddBuff(95 + player.beetleOrbs, 5, false);
						player.beetleCounter = 0f;
					}
					else
					{
						player.beetleCounter = (float)num5;
					}
				}

			    if (!player.beetleDefense && !player.beetleOffense)
			    {
			    	player.beetleCounter = 0f;
			    }
			    else
			    {
			    	player.beetleFrameCounter++;
			    	if (player.beetleFrameCounter >= 1)
			    	{
			    		player.beetleFrameCounter = 0;
			    		player.beetleFrame++;
			    		if (player.beetleFrame > 2)
			    		{
			    			player.beetleFrame = 0;
			    		}
			    	}
			    	for (int l = player.beetleOrbs; l < 3; l++)
			    	{
			    		player.beetlePos[l].X = 0f;
			    		player.beetlePos[l].Y = 0f;
			    	}
			    	for (int m = 0; m < player.beetleOrbs; m++)
			    	{
			    		player.beetlePos[m] += player.beetleVel[m];
			    		Vector2[] expr_6EC_cp_0 = player.beetleVel;
			    		int expr_6EC_cp_1 = m;
			    		expr_6EC_cp_0[expr_6EC_cp_1].X = expr_6EC_cp_0[expr_6EC_cp_1].X + (float)Main.rand.Next(-100, 101) *     0.005f;
			    		Vector2[] expr_71A_cp_0 = player.beetleVel;
			    		int expr_71A_cp_1 = m;
			    		expr_71A_cp_0[expr_71A_cp_1].Y = expr_71A_cp_0[expr_71A_cp_1].Y + (float)Main.rand.Next(-100, 101) *     0.005f;
			    		float num6 = player.beetlePos[m].X;
			    		float num7 = player.beetlePos[m].Y;
			    		float num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
			    		if (num8 > 100f)
			    		{
			    			num8 = 20f / num8;
			    			num6 *= -num8;
			    			num7 *= -num8;
			    			int num9 = 10;
			    			player.beetleVel[m].X = (player.beetleVel[m].X * (float)(num9 - 1) + num6) / (float)num9;
			    			player.beetleVel[m].Y = (player.beetleVel[m].Y * (float)(num9 - 1) + num7) / (float)num9;
			    		}
			    		else if (num8 > 30f)
			    		{
			    			num8 = 10f / num8;
			    			num6 *= -num8;
			    			num7 *= -num8;
			    			int num10 = 20;
			    			player.beetleVel[m].X = (player.beetleVel[m].X * (float)(num10 - 1) + num6) / (float)num10;
			    			player.beetleVel[m].Y = (player.beetleVel[m].Y * (float)(num10 - 1) + num7) / (float)num10;
			    		}
			    		num6 = player.beetleVel[m].X;
			    		num7 = player.beetleVel[m].Y;
			    		num8 = (float)Math.Sqrt((double)(num6 * num6 + num7 * num7));
			    		if (num8 > 2f)
			    		{
			    			player.beetleVel[m] *= 0.9f;
			    		}
			    		player.beetlePos[m] -= player.velocity * 0.25f;
			    	}
			    }
            }
		}
		
		public override void AddRecipes()
		{
            ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.BeetleHelmet);
			recipe.AddRecipeGroup("FargowiltasSouls:AnyBeetle");
			recipe.AddIngredient(ItemID.BeetleLeggings);
			recipe.AddIngredient(ItemID.BeetleWings);
			recipe.AddIngredient(ItemID.BeeWings);
            recipe.AddIngredient(ItemID.ButterflyWings);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}	
}
		
	





