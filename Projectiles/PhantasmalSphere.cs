using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
	public class PhantasmalSphere : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("PhantasmalSphere");
		}
		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 100;
			aiType = ProjectileID.Bullet;
		}
		

		// public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		// {
				  // target.AddBuff(BuffID.Ichor, 240, true);
				  // target.AddBuff(BuffID.CursedInferno, 240, true);
				  // target.AddBuff(BuffID.Confused, 120, true);
				  // target.AddBuff(BuffID.Venom, 240, true);
				  // target.AddBuff(BuffID.ShadowFlame, 240, true);
				  // target.AddBuff(BuffID.OnFire, 240, true);
				  // target.AddBuff(BuffID.Frostburn, 240, true);
		// }
		
		
		public override void AI()
		{
			//dust!
			int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width / 2, projectile.height + 5, 56, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), .5f);
			Main.dust[DustID].noGravity = true;
			int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width / 2, projectile.height + 5, 56, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), .5f);
			Main.dust[DustID3].noGravity = true;
			
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
		
		public override void Kill(int timeleft)
		{
			for (int num468 = 0; num468 < 20; num468++)
			{
				int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 15, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 2f;
				num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 15, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
				Main.dust[num469].velocity *= 2f;
			}
		}
		
	}
}