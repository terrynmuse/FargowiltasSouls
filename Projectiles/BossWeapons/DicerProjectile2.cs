using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
	class DicerProjectile2 : ModProjectile
	{
		public int Counter = 1;

		public override void SetDefaults()
		{
			projectile.extraUpdates = 0;
			projectile.width = 19;
			projectile.height = 19;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
			projectile.timeLeft = 90;
		}
		
		public override void AI()
		{
			if(Counter >= 75)
			{
				projectile.scale += .1f;
				projectile.rotation += 0.2f;
			}
			
			Counter++;
		}
		
		public override void Kill(int timeLeft)
        {
				int proj2 = 484;//374;
				
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 5f, proj2, (int)(projectile.damage * 0.5f), 2/*kb*/, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, 0f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -5f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, 0f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, 4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, -4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, -4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, 4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
		}
	}
}
