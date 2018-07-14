using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Souls
{
    public class Shock : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shock");
		}
        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 0;
            projectile.timeLeft = 20;
            aiType = 48;
        }
		
		public override void AI()
		{
			//dust!
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width * 2, projectile.height * 2, 226, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), .5f);
			Main.dust[DustID].noGravity = true;
			//int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 226, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f);
			//Main.dust[DustID3].noGravity = true;
		}
	   
    }
}