using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class EyeFlail : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leash of Cthulhu");
			Tooltip.SetDefault("'The mutilated carcass of a defeated foe..'");
		}
        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 10;
            item.value = Item.sellPrice(0, 0, 50, 0); 
            item.rare = 1; 
 
            item.noMelee = true; 
            item.useStyle = 5; 
            item.useAnimation = 40; 
            item.useTime = 40; 
            item.knockBack = 7.5F;
            item.damage = 16;
            item.scale = 2F;
            item.noUseGraphic = true; 
            item.shoot = mod.ProjectileType("LeashFlail");
            item.shootSpeed = 15.1F;
            item.UseSound = SoundID.Item1;
            item.melee = true; 
        } 
    }
}