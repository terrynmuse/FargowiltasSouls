using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
	class BlenderProjectile : ModProjectile
	{
		public int counter = 1;
		public override void SetStaticDefaults()
		{
			// Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
			ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = -1f;
			// Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
			ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 400f;
			// Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
			ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 17.5f;
		}

		public override void SetDefaults()
		{
			projectile.CloneDefaults(ProjectileID.Kraken);
			projectile.extraUpdates = 0;
			projectile.width = 19;
			projectile.height = 19;
			//yoyo ai
			projectile.aiStyle = 99;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.melee = true;
			projectile.scale = 1f;
		}
		// notes for aiStyle 99: 
		// localAI[0] is used for timing up to YoyosLifeTimeMultiplier
		// localAI[1] can be used freely by specific types
		// ai[0] and ai[1] usually point towards the x and y world coordinate hover point
		// ai[0] is -1f once YoyosLifeTimeMultiplier is reached, when the player is stoned/frozen, when the yoyo is too far away, or the player is no longer clicking the shoot button.
		// ai[0] being negative makes the yoyo move back towards the player
		// Any AI method can be used for dust, spawning projectiles, etc specific to your yoyo.
		
		public override void AI()
		{
			if (counter <= 24)
			{
                int proj = mod.ProjectileType("DicerProjectile");
				
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 5f, proj, (int)(projectile.damage * 0.5f), 2/*kb*/, Main.myPlayer, 0f, 0f);
			}
			
			counter++;
		}
		
		public override void PostAI()
		{
			/*if (Main.rand.Next(2) == 0)
			{
				int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 16, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dustIndex].noGravity = true;
				Main.dust[dustIndex].scale = 1.6f;
			}*/
		}
	}
}
