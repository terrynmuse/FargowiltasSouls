using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
	public class DamnedBook : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cultist's Spellbook");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
		{
            item.damage = 5;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 28;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 2;        
            item.value = 1000;
            item.rare = 6;
            item.mana = 1;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = 1;
            item.shootSpeed = 8f;    //projectile speed when shoot
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{	

		  float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(speedX, speedY)- spread/2;
            double deltaAngle = spread/8f;
            double offsetAngle;
            int i;
			int j = Main.rand.Next(5);
			
			if(j == 0)
			{
				for (i = 0; i < 1; i++ )
				{
					offsetAngle = startAngle + deltaAngle * (i + i*i) / 2f + 32f * i;
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 464, damage, knockBack, player.whoAmI);
				}
			}
			
			else if(j == 1)
			{
				for (i = 0; i < 1; i++ )
				{
					offsetAngle = startAngle + deltaAngle * (i + i*i) / 2f + 32f * i;
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 465, damage, knockBack, player.whoAmI);
				}
			}
			
			else if(j == 2)
			{
				for (i = 0; i < 1; i++ )
				{
					offsetAngle = startAngle + deltaAngle * (i + i*i) / 2f + 32f * i;
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 466, damage, knockBack, player.whoAmI);
				}
			}
			
			else if(j == 3)
			{
				for (i = 0; i < 1; i++ )
				{
					offsetAngle = startAngle + deltaAngle * (i + i*i) / 2f + 32f * i;
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 467, damage, knockBack, player.whoAmI);
				}
			}
			
			else
			{
				for (i = 0; i < 1; i++ )
				{
					offsetAngle = startAngle + deltaAngle * (i + i*i) / 2f + 32f * i;
					Projectile.NewProjectile(position.X, position.Y, speedX, speedY, 468, damage, knockBack, player.whoAmI);
				}
			}
			
            return false;
			
		}
		
		public override void AddRecipes()
		{	
			ModRecipe recipe = new ModRecipe(mod);
			
			recipe.AddIngredient(ItemID.AncientCultistTrophy);
			
			recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
		}
	}
}