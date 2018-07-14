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
	public class FleshHand : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flesh Hand");
			Tooltip.SetDefault("'The enslaved minions of a defeated foe..'");
		}
		 public override void SetDefaults()
	    {
			item.damage = 32;
			item.thrown = true;
			item.width = 24;
			item.height = 24;
			item.useTime = 15;
			item.useAnimation = 15;// must be the same^
			item.useStyle = 5; 
			item.noMelee = true; 
			item.knockBack = 1.5f; 
			item.UseSound = new LegacySoundStyle(4, 13);
			item.value = 50000; 
			item.rare = 5; 
			item.autoReuse = true; 
			item.shoot = mod.ProjectileType("Hungry"); 
			item.shootSpeed = 20f; 
			item.noUseGraphic = true;
		}
	}
}