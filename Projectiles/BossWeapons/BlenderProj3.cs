using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    internal class BlenderProj3 : ModProjectile
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
            projectile.timeLeft = 120;
        }

        public override void AI()
        {
            if (Counter >= 75)
            {
                projectile.scale += .1f;
                projectile.rotation += 0.2f;
            }

            Counter++;

            const int aislotHomingCooldown = 0;
            const int homingDelay = 30;
            const float desiredFlySpeedInPixelsPerFrame = 70;
            const float amountOfFramesToLerpBy = 10; // minimum of 1, please keep in full numbers even though it's a float!

            projectile.ai[aislotHomingCooldown]++;
            if (projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = homingDelay; //cap this value 

                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void Kill(int timeLeft)
        {
            int proj2 = 484; //374;

            if (projectile.owner == Main.myPlayer)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 5f, proj2, (int)(projectile.damage * 0.5f), 2 /*kb*/, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                    p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 5f, 0f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                    p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, -5f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                    p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -5f, 0f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                }
                else
                {
                    int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, 4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                    p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, -4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                    p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 4f, -4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                    p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, -4f, 4f, proj2, (int)(projectile.damage * 0.5f), 2, Main.myPlayer);
                    Main.projectile[p].timeLeft = 180;
                }
            }
        }
    }
}