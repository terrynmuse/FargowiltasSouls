using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace FargowiltasSouls.Items.Weapons
{
	public class TophatSquirrel : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Top Hat Squirrel");
			Tooltip.SetDefault("'Who knew this squirrel had phenomenal cosmic power?'");
		}
		public override void SetDefaults()
		{
			item.damage = 50;  
			
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.rare = 1;
			item.useAnimation = 20;
			item.useTime = 20;
			item.consumable = true;
			
			item.thrown = true;  
			item.noMelee = true;
			item.noUseGraphic = true;
			item.useStyle = 1; 
			item.knockBack = 3f;  //Ranges from 1 to 9.
			
			//item.UseSound = SoundID.Item1; //?
			item.autoReuse = true;  

			item.shoot = mod.ProjectileType("Squirrel1");
			item.shootSpeed = 8f; 
		}
		
		public override bool UseItem(Player player)
		{
			Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType<Projectiles.Squirrel1>(), 0, 0, Main.myPlayer, 0f, 0f);
			
			return true;
		}
	}
}