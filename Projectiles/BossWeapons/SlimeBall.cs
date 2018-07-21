using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    	public class SlimeBall : ModProjectile
    	{
			public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Slime Ball");
		}
		public override void SetDefaults()
		{
			projectile.damage = 20;
			projectile.width = 16;
			projectile.height = 16;
			projectile.aiStyle = 14;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.timeLeft = 65;
		}
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.GhostWhite;
		}
        	public override void AI()
        	{
			projectile.rotation += 0.3f * projectile.direction;
			for (int num103 = 0; num103 < 1; num103++)
			{
				int num104 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 59, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[num104].noGravity = true;
				Dust dust7 = Main.dust[num104];
				dust7.velocity.X = dust7.velocity.X * 0.3f;
				Dust dust8 = Main.dust[num104];
				dust8.velocity.Y = dust8.velocity.Y * 0.3f;
			}
		}
		public override void Kill(int timeleft)
		{
			for (int num468 = 0; num468 < 20; num468++)
			{
				int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 59, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 2f;
				num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 59, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100);
				Main.dust[num469].velocity *= 2f;
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(137, 240);
		}
    	}
}