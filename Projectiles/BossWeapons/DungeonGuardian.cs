using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
	public class DungeonGuardian : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("DG");
		}

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 40;
			projectile.aiStyle = 0;
			aiType = ProjectileID.Bullet;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 1000;
			
			//ignore immune frame
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 1;
			
		}
		
		public override void AI()
		{
			projectile.rotation += 0.2f;

			const int aislotHomingCooldown = 0;
            //int homingDelay = 10;
            int homingDelay = (int)projectile.ai[1];
			const float desiredFlySpeedInPixelsPerFrame = 60;
			const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's  float!
	
			projectile.ai[aislotHomingCooldown]++;
			if(projectile.ai[aislotHomingCooldown] > homingDelay)
			{
				projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 
	
				int foundTarget = HomeOnTarget();
				if(foundTarget != -1)
				{
					NPC n = Main.npc[foundTarget];
					Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
					projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f /amountOfFramesToLerpBy);
				}
			}
		}
		
		int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if(n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if(distance <= homingMaximumRangeInPixels &&
                        (
                        selectedTarget == -1 ||  //there is no selected target
                        projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                        )
                    {
                        selectedTarget = i;
                    }
                }
            }

            return selectedTarget;
        }
	
		public override void Kill(int timeLeft)
		{
            for (int i = 0; i < 50; i++)
            {
                Vector2 pos = new Vector2(projectile.Center.X + Main.rand.Next(-20, 20), projectile.Center.Y + Main.rand.Next(-20, 20));
                int dust = Dust.NewDust(pos, projectile.width, projectile.height, DustID.Blood, 0, 0, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
            }
        }
	}
}
