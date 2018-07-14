using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

				const int AISLOT_HOMING_COOLDOWN = 0;
				const int HOMING_DELAY = 10;
				const float DESIRED_FLY_SPEED_IN_PIXELS_PER_FRAME = 60;
				const float AMOUNT_OF_FRAMES_TO_LERP_BY = 20; // minimum of 1, please keep in full numbers even though it's a float!
	
				projectile.ai[AISLOT_HOMING_COOLDOWN]++;
				if(projectile.ai[AISLOT_HOMING_COOLDOWN] > HOMING_DELAY)
				{
					projectile.ai[AISLOT_HOMING_COOLDOWN] = HOMING_DELAY; //cap this value 
	
					int foundTarget = HomeOnTarget();
					if(foundTarget != -1)
					{
						NPC n = Main.npc[foundTarget];
						Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * DESIRED_FLY_SPEED_IN_PIXELS_PER_FRAME;
						projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / AMOUNT_OF_FRAMES_TO_LERP_BY);
					}
				}

			
		}
		
		int HomeOnTarget()
        {
            const bool HOMING_CAN_AIM_AT_WET_ENEMIES = true;
            const float HOMING_MAXIMUM_RANGE_IN_PIXELS = 1000;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if(n.CanBeChasedBy(projectile, false) && (!n.wet || HOMING_CAN_AIM_AT_WET_ENEMIES))
                {
                    float distance = projectile.Distance(n.Center);
                    if(distance <= HOMING_MAXIMUM_RANGE_IN_PIXELS &&
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