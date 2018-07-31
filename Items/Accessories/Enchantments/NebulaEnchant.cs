using FargowiltasSouls.Projectiles;
using Microsoft.Xna.Framework;
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
Hurting enemies has a chance to spawn buff boosters");
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
            modPlayer.NebulaEnchant = true;

            if (player.nebulaCD > 0)
			{
				player.nebulaCD--;
			}
				player.setNebula = true;

            modPlayer.NebulaCounter++;

            if(modPlayer.NebulaCounter > 600)
            {
                modPlayer.NebulaCounter = 600;
            }

            if(player.HasBuff(BuffID.NebulaUpDmg3) && player.HasBuff(BuffID.NebulaUpLife3) && player.HasBuff(BuffID.NebulaUpMana3) && modPlayer.NebulaCounter == 600)
            {
                Vector2 vel = (Main.MouseWorld - player.Center).SafeNormalize(-Vector2.UnitY) * 10;
                Projectile p = Projectile.NewProjectileDirect(player.Center, vel, ProjectileID.NebulaArcanum, 50, 1f, player.whoAmI);
                
                FargoGlobalProjectile.SplitProj(p, 5);

                modPlayer.NebulaCounter = -600;
            }
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