using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Souls
{
	public class SpookyScythe : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Scythe");
		}
		public override void SetDefaults()
		{
			projectile.width = 106;
			projectile.height = 84;
			projectile.aiStyle = 0;
			projectile.friendly = true;
			projectile.minion = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 50;
			aiType = ProjectileID.CrystalBullet;
			projectile.tileCollide = false;
		}
		
		public override void AI()
		{
			//dust!
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 55, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
			Main.dust[DustID].noGravity = true;
			int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 55, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
			Main.dust[DustID3].noGravity = true;
			
			projectile.rotation += 0.4f;
			
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			for (int num489 = 0; num489 < 5; num489++)
			{
				int num490 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 55, 10f, 30f, 100, default(Color), 1f);
				Main.dust[num490].noGravity = true;
				Main.dust[num490].velocity *= 1.5f;
				Main.dust[num490].scale *= 0.9f;
			}
			if (projectile.owner == Main.myPlayer)
			{
				for (int num491 = 0; num491 < 3; num491++)
				{
					float num492 = -projectile.velocity.X * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
					float num493 = -projectile.velocity.Y * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
					Projectile.NewProjectile(projectile.position.X + num492, projectile.position.Y + num493, num492, num493, 45, (int)((double)projectile.damage * 0.5), 0f, projectile.owner, 0f, 0f);
				}
			}
		}
		
	}
}