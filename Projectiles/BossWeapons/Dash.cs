using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class Dash : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.melee = true;
            projectile.width = Player.defaultWidth;
            projectile.height = Player.defaultHeight;
            
            projectile.penetrate = -1;
        }

        public float UpdateCount { get { return projectile.ai[0]; }set { projectile.ai[0] = value; } }
        public float DashCount { get { return projectile.ai[0] - 20; } }
        public Vector2 DashStep;
        public const float DASH_STEP_COUNT = 15;
        public const float DASH_STEP_DELAY = 0;

        bool _playedLocalSound = false;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.dead || !player.active)
            {
                projectile.timeLeft = 0;
                return;
            }

            // Get dash location
            if (UpdateCount == 0)
            {
                for (int i = 0; i < DASH_STEP_COUNT * 8; i++)
                {
                    Vector2 move = Collision.TileCollision(
                        projectile.position, projectile.velocity / 2,
                        projectile.width, projectile.height,
                        true, true, (int)player.gravDir);
                    if (move == Vector2.Zero) break;
                    projectile.position += move / 2;
                }
                DashStep = (projectile.Center - player.Center) / DASH_STEP_COUNT;

                projectile.velocity = Vector2.Zero;
            }

            // Dash towards location
            if (UpdateCount >= DASH_STEP_DELAY)
            {
				//spawn along path
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0, 0, mod.ProjectileType("PhantasmalSphere"), (int)(projectile.damage * 0.5f), 0/*kb*/, Main.myPlayer);
				
                if (UpdateCount == DASH_STEP_DELAY)
                {
                    DashStep = (projectile.Center - player.Center) / DASH_STEP_COUNT;
                    player.inventory[player.selectedItem].useStyle = 3;
                }

                // freeze in swing
                player.itemAnimation = player.itemAnimationMax - 2;

                // dash, change position to influence camera lerp
                player.position += Collision.TileCollision(player.position,
                    DashStep / 2,
                    player.width,
                    player.height,
                    true, true, (int)player.gravDir);
                player.velocity = Collision.TileCollision(player.position,
                    DashStep * 0.8f,
                    player.width,
                    player.height,
                    true, true, (int)player.gravDir);

                // Set immunities
                player.immune = true;
                player.immuneTime = Math.Max(player.immuneTime, 2);
                player.immuneNoBlink = true;
                player.fallStart = (int)(player.position.Y / 16f);
                player.fallStart2 = player.fallStart;

                //point in direction
                if (DashStep.X > 0) player.direction = 1;
                if (DashStep.X < 0) player.direction = -1;

                if (UpdateCount >= DASH_STEP_DELAY + DASH_STEP_COUNT - 1)
                {
                    projectile.timeLeft = 0;
                }
            }
            else
            {
                // slow until move
                player.velocity *= 0.8f;
            }

            UpdateCount++;
        }

        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            player.velocity = DashStep / DASH_STEP_COUNT;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false; // slide not stop on tiles
        }
    }
}
