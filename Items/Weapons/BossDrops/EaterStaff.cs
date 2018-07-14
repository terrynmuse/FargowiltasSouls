using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
	public class EaterStaff : ModItem
	{
		public int shoot = 0;
		public float permaX = 0, permaY = 0, permaXPos = 0, permaYPos = 0;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eater of Worlds Wand");
			Tooltip.SetDefault("'An old foe beaten into submission..'");//
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.summon=true;
			item.mana = 20;
			item.width = 40;
			item.height = 40;
			item.useTime = 4;
			item.useAnimation = 32;
			item.useStyle = 5;
			item.knockBack = 2;
			item.value = 10000; //
			item.rare = 2;
			item.UseSound = SoundID.Item1; 
			item.autoReuse = true; 
			item.shoot = mod.ProjectileType("EaterHead");
			item.shootSpeed = 8f;
			item.reuseDelay = 80;
			

			item.noMelee = true;

			item.UseSound = SoundID.Item44; //

		}
		
		//make them hold it different
		public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -15);
        } 
		
		public override bool UseItem (Player player)
		{
			shoot = 0;
			return true;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockback)
		{
			
			
			if(shoot == 0)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EaterHead"), damage, knockback, player.whoAmI, 0f, 0f);
				permaX = speedX;
				permaY = speedY;
				permaXPos = position.X;
				permaYPos = position.Y;
			}
			else if(shoot == 1 || shoot == 2)
			{
				Projectile.NewProjectile(permaXPos, permaYPos, permaX, permaY, mod.ProjectileType("EaterBody"), damage, knockback, player.whoAmI, 0f, 0f);
			}
			else if(shoot == 3)
			{
				Projectile.NewProjectile(permaXPos, permaYPos, permaX, permaY, mod.ProjectileType("EaterTail"), damage, knockback, player.whoAmI, 0f, 0f);
			}
			else if(shoot == 7)
			{
				shoot = 0;
				return false;
			}
			
			shoot++;
            return false;
        }



		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(BuffID.Slimed, 120);
		}
	}
}