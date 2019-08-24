using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class HellFlame : ModProjectile
    {
        private static int _currentShade = 76; //77;//79;//83;//82;

        public int targetID = -1;
        public int searchTimer = 18;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 4;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
            projectile.aiStyle = -1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Black;
        }

        public override void AI()
        {
            if (projectile.timeLeft > 120) projectile.timeLeft = 120;
            if (projectile.ai[1] > 2f)
            {
                projectile.ai[1] = 0;
                Dust dust;
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.SolarFlare, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
                dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);

                if (Main.rand.Next(3) != 0) dust.scale *= 2f;

                dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.SolarFlare, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
                dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);

                if (Main.rand.Next(3) != 0) dust.scale *= 2f;

                dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color),
                    1f);
                dust = Main.dust[dustIndex];
                if (Main.rand.Next(3) != 0)
                {
                    dust.scale *= 1.5f;
                    dust.velocity *= 2f;
                }

                dust.velocity *= 1.2f;

                dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0,
                    default(Color), .5f);
                dust = Main.dust[dustIndex];
                if (Main.rand.Next(3) != 0)
                {
                    dust.scale *= 2f;
                    dust.velocity *= 2f;
                }

                dust.velocity *= 1.2f;
            }
            else
            {
                projectile.ai[1] += 1f;
            }

            projectile.rotation += 0.3f * projectile.direction;

            if (targetID == -1) //no target atm
            {
                if (searchTimer == 0) //search every 18/3=6 ticks
                {
                    searchTimer = 18;

                    int possibleTarget = -1;
                    float closestDistance = 500f;

                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];

                        if (npc.active && npc.chaseable && npc.lifeMax > 5 && !npc.dontTakeDamage && !npc.friendly && !npc.immortal)
                        {
                            float distance = Vector2.Distance(projectile.Center, npc.Center);

                            if (closestDistance > distance)
                            {
                                closestDistance = distance;
                                possibleTarget = i;
                            }
                        }
                    }

                    if (possibleTarget != -1)
                    {
                        targetID = possibleTarget;
                        projectile.netUpdate = true;
                    }
                }
                searchTimer--;
            }
            else //currently have target
            {
                NPC npc = Main.npc[targetID];

                if (npc.active && npc.chaseable && !npc.dontTakeDamage /*&& npc.immune[projectile.owner] == 0*/) //target is still valid
                {
                    Vector2 distance = npc.Center - projectile.Center;
                    double angle = distance.ToRotation() - projectile.velocity.ToRotation();
                    if (angle > Math.PI)
                        angle -= 2.0 * Math.PI;
                    if (angle < -Math.PI)
                        angle += 2.0 * Math.PI;

                    if (projectile.ai[0] == -1)
                    {
                        if (Math.Abs(angle) > Math.PI * 0.75)
                        {
                            projectile.velocity = projectile.velocity.RotatedBy(angle * 0.07);
                        }
                        else
                        {
                            float range = distance.Length();
                            float difference = 12.7f / range;
                            distance *= difference;
                            distance /= 7f;
                            projectile.velocity += distance;
                            if (range > 70f)
                            {
                                projectile.velocity *= 0.977f;
                            }
                        }
                    }
                    else
                    {
                        projectile.velocity = projectile.velocity.RotatedBy(angle * 0.1);
                    }
                }
                else //target lost, reset
                {
                    targetID = -1;
                    searchTimer = 0;
                    projectile.netUpdate = true;
                }
            }
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 30;
            hitbox.Y -= 30;
            hitbox.Width += 60;
            hitbox.Height += 60;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 5;
            //target.AddBuff(mod.BuffType("HellFire"), 300);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            //target.AddBuff(mod.BuffType("HellFire"), 300);
        }
    }
}