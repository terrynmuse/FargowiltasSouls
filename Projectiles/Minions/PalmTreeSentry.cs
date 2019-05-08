using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class PalmTreeSentry : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palm Tree");
        }

        public override void SetDefaults()
        {
            projectile.width = 80;
            projectile.height = 66;
            projectile.aiStyle = -1;
            projectile.tileCollide = true;
            projectile.ignoreWater = true;
            projectile.sentry = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 7200;
        }

        public override void AI()
        {
            Player owner = Main.player[projectile.owner];

            projectile.velocity.Y = projectile.velocity.Y + 0.2f;
            if (projectile.velocity.Y > 16f)
            {
                projectile.velocity.Y = 16f;
            }

            projectile.ai[1] += 1f;

            int attackRate = 60;

            if (owner.ZoneDesert || owner.ZoneBeach)
            {
                attackRate = 30;
            }

            if (projectile.ai[1] >= attackRate)
            {
                float num = 2000f;
                int npcIndex = -1;
                for (int i = 0; i < 200; i++)
                {
                    float dist = Vector2.Distance(projectile.Center, Main.npc[i].Center);

                    if (dist < num && dist < 300 && Main.npc[i].CanBeChasedBy(projectile, false))
                    {
                        npcIndex = i;
                        num = dist;
                    }
                }

                if (npcIndex != -1)
                {
                    NPC target = Main.npc[npcIndex];

                    if (Collision.CanHit(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height))
                    {
                        Vector2 velocity = Vector2.Normalize(target.Center - projectile.Center) * 10;

                        Projectile.NewProjectile(projectile.Center, velocity, ProjectileID.SeedlerNut, projectile.damage, 2, projectile.owner);
                    }
                }
                projectile.ai[1] = 0f;

                //kill if too far away
                if (Vector2.Distance(Main.player[projectile.owner].Center, projectile.Center) > 2000)
                {
                    projectile.Kill();
                }
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.position += projectile.velocity;
            projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}
