using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;

namespace FargowiltasSouls.Projectiles.Minions
{
	public class EaterHead : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eater");
		}
		public override void SetDefaults()
		{
			projectile.width = 28;
			projectile.height = 50;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 100;
			projectile.tileCollide = false;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 1;
			
			projectile.aiStyle = 1; //
			aiType = ProjectileID.Bullet; //


		}

		public override void AI()
		{
			//dust!
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 173, projectile.velocity.X * 1.5f, projectile.velocity.Y * 2f, 100, default(Color), .5f);
			Main.dust[DustID].noGravity = true;
			int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 173, projectile.velocity.X * 1.5f, projectile.velocity.Y * 2f, 100, default(Color), .5f);
			Main.dust[DustID3].noGravity = true;
			
			
			projectile.spriteDirection = projectile.direction;
			
			 if (ModLoader.GetLoadedMods().Contains("Luiafk"))
			 {
				 projectile.spriteDirection = -projectile.direction;
			 }
		}
		
		public override void Kill(int timeleft)
		{
			for (int num468 = 0; num468 < 20; num468++)
			{
				int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 62, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 5f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 2f;
				num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 62, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 5f);
				Main.dust[num469].velocity *= 2f;
			}
		}
	}
}
