using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
				int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 44, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.9f);
				Main.dust[dustId].noGravity = true;
				int dustId2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 107, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.6f);
				Main.dust[dustId2].noGravity = true;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width + 5, projectile.height + 5, 44, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
			Main.dust[dustId].noGravity = true;
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            target.AddBuff(BuffID.Poisoned, 300);
        }
    }
}