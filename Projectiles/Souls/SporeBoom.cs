using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Projectiles;

namespace FargowiltasSouls.Projectiles.Souls
{
	public class SporeBoom : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spore Boom");
		}
		public override void SetDefaults()
		{
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.penetrate = 2;
			projectile.timeLeft = 30;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
		}
		
		public override void AI()
		{
			projectile.velocity.Y += projectile.ai[0];
			if (Main.rand.Next(2) == 0)
			{
				int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 44, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.9f);
				Main.dust[DustID].noGravity = true;
				int DustID2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 107, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.6f);
				Main.dust[DustID2].noGravity = true;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 44, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
			Main.dust[DustID].noGravity = true;
		}
	}
}