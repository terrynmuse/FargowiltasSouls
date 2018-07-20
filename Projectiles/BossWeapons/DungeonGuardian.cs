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
			projectile.width = 80;
			projectile.height = 102;
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
				const int homingDelay = 10;
				const float desiredFlySpeedInPixelsPerFrame = 60;
				const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!
	
				projectile.ai[aislotHomingCooldown]++;
				if(projectile.ai[aislotHomingCooldown] > homingDelay)
				{
					projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 
	
					int foundTarget = HomeOnTarget();
					if(foundTarget != -1)
					{
						NPC n = Main.npc[foundTarget];
						Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
						projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
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
			//dust idk
		}
		
	}
}