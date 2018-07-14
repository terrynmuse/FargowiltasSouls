using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
	public class Bonez : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bonez");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 24;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.CrystalBullet;
		}
		
		public override void AI()
		{
			projectile.rotation += 0.3f;
			
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			for (int num489 = 0; num489 < 5; num489++)
			{
				int num490 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 68, 10f, 30f, 0, default(Color), 1f);
				Main.dust[num490].noGravity = true;
				Main.dust[num490].velocity *= 1.5f;
				Main.dust[num490].scale *= 0.9f;
			}

			for (int i = 0; i < 3; i++)
			{
				float num492 = -projectile.velocity.X * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				float num493 = -projectile.velocity.Y * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				Projectile.NewProjectile(projectile.position.X + num492, projectile.position.Y + num493, num492, num493, 90, (int)((double)projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
			}
		}
		
	}
}