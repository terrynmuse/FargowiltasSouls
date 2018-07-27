using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
	public class HallowShield : ModProjectile
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallow Shield");
		}

		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 10;
			projectile.height = 10;
			projectile.friendly = true;
			projectile.penetrate = -1; 
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		
		public override void AI()
        {
			Player player = Main.player[projectile.owner];
			//FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            const int focusRadius = 50;
            
            for (int i = 0; i < 25; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * focusRadius);
                offset.Y += (float)(Math.Cos(angle) * focusRadius);
                Dust dust = Main.dust[Dust.NewDust(
                    player.Center + offset - new Vector2(4, 4), 0, 0,
                    DustID.GoldFlame, 0, 0, 100, Color.White, 1f
                    )];
                dust.velocity = player.velocity;
                //dust.fadeIn = 0.5f;
                dust.noGravity = true;
            } 
        }
    }
}