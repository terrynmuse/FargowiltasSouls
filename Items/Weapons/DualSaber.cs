using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace FargowiltasSouls.Items.Weapons
    //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class DualSaber : ModItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dual Saber");
			Tooltip.SetDefault("ye \n and thus fargo never added a proper description for this item");
            DisplayName.AddTranslation(GameCulture.Chinese, "双刃光剑");
            Tooltip.AddTranslation(GameCulture.Chinese, "");
		}
		
        public override void SetDefaults()
        {
            item.damage = 59;    //The damage stat for the Weapon.
            item.melee = true;     //This defines if it does Melee damage and if its effected by Melee increasing Armor/Accessories.
            item.width = 80;    //The size of the width of the hitbox in pixels.
            item.height = 80;    //The size of the height of the hitbox in pixels.
            item.useTime = 10;   //How fast the Weapon is used.
            item.useAnimation = 10;     //How long the Weapon is used for.
            item.useStyle = 100;    //The way your Weapon will be used, 1 is the regular sword swing for example
            item.knockBack = 8f;    //The knockback stat of your Weapon.
            item.value = Item.buyPrice(0, 10, 0, 0); // How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 10gold)
            item.rare = 11;   //The color the title of your Weapon when hovering over it ingame                    
            item.shoot = mod.ProjectileType("DualSaberProj");  //This defines what type of projectile this weapon will shoot  
            item.noUseGraphic = true; // this defines if it does not use graphic
        }
		
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.useStyle = 100;
				item.useTime = 10;
				item.useAnimation = 10;
				item.damage = 50; // 
				item.channel = true;
				item.shoot = mod.ProjectileType("DualSaberProj");
			}
			else
			{
				item.useStyle = 1;
				item.useTime = 25;
				item.useAnimation = 25;
				item.damage = 100;
				item.shoot = mod.ProjectileType("DualSaberProj2");
				/*for (int i = 0; i < 1000; ++i)
				{
					if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == item.shoot)
					{
						return false;
					}
				}*/
			}
			return base.CanUseItem(player);
		}
 
        public override bool UseItemFrame(Player player)     //this defines what frame the player use when this weapon is used
        {
			if (player.altFunctionUse == 2)
			{	
				player.bodyFrame.Y = 3 * player.bodyFrame.Height;
				return true;
			}
				return false;
        }
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			/*if (player.altFunctionUse == 1)
			{
				float spread = 45f * 0.0174f;
				float baseSpeed = (float)Math.Sqrt(speedX * speedX + speedY * speedY);
				double startAngle = Math.Atan2(speedX, speedY)- spread/2;
				double deltaAngle = spread/3f;
				double offsetAngle;
				int i;
				for (i = 0; i < 3;i++ )
				{
					offsetAngle = startAngle + deltaAngle * i;
					Terraria.Projectile.NewProjectile(position.X, position.Y, baseSpeed*(float)Math.Sin(offsetAngle), baseSpeed*(float)Math.Cos(offsetAngle), item.shoot, damage, knockBack, item.owner);
				}
				return false;
			}*/
			return true;
		}
    }
}


//blood zombie rarer
//op drop slime mount
//overloaders static
//cycle shop funky