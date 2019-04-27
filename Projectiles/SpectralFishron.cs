using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class SpectralFishron : ModProjectile
    {
        public override string Texture => "Terraria/NPC_370";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spectral Fishron");
            Main.projFrames[projectile.type] = 8;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 11;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 150;
            projectile.height = 100;
            projectile.aiStyle = -1;
            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.alpha = 100;
            projectile.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
        }

        public override void AI()
        {
            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = projectile.ai[1] + 1;
                Main.PlaySound(29, (int)projectile.Center.X, (int)projectile.Center.Y, 20);
                switch ((int)projectile.ai[1])
                {
                    case 1: projectile.melee = true; break;
                    case 2: projectile.ranged = true; break;
                    case 3: projectile.magic = true; break;
                    case 4: projectile.minion = true; break;
                    case 5: projectile.thrown = true; break;
                    default: break;
                }
                projectile.ai[1] = 0f;
                projectile.netUpdate = true;
            }

            if (projectile.localAI[0]++ > 30f)
            {
                projectile.localAI[0] = 0f;
                projectile.ai[1]++;
            }

            if (projectile.ai[1] % 2 == 1) //dash
            {
                projectile.rotation = projectile.velocity.ToRotation();
                projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
                projectile.frameCounter = 5;
                projectile.frame = 6;

                int num22 = 7;
                for (int index1 = 0; index1 < num22; ++index1)
                {
                    Vector2 vector2_1 = (Vector2.Normalize(projectile.velocity) * new Vector2((projectile.width + 50) / 2f, projectile.height) * 0.75f).RotatedBy((index1 - (num22 / 2 - 1)) * Math.PI / num22, new Vector2()) + projectile.Center;
                    Vector2 vector2_2 = ((float)(Main.rand.NextDouble() * 3.14159274101257) - 1.570796f).ToRotationVector2() * Main.rand.Next(3, 8);
                    Vector2 vector2_3 = vector2_2;
                    int index2 = Dust.NewDust(vector2_1 + vector2_3, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].velocity /= 4f;
                    Main.dust[index2].velocity -= projectile.velocity;
                }
            }
            else //preparing to dash
            {
                int ai0 = (int)projectile.ai[0];
                const float moveSpeed = 1f;
                if (projectile.localAI[0] == 30f) //just about to dash
                {
                    if (projectile.ai[0] >= 0 && Main.npc[ai0].CanBeChasedBy()) //has target
                    {
                        projectile.velocity = Main.npc[ai0].Center + Main.npc[ai0].velocity * 15f - projectile.Center;
                        projectile.velocity.Normalize();
                        projectile.velocity *= 27f;
                        projectile.rotation = projectile.velocity.ToRotation();
                        projectile.direction = projectile.spriteDirection = projectile.velocity.X > 0 ? 1 : -1;
                        projectile.frameCounter = 5;
                        projectile.frame = 6;
                    }
                    else //no target
                    {
                        projectile.localAI[0] = -1f;
                        TargetEnemies();
                        if (++projectile.frameCounter > 5)
                        {
                            projectile.frameCounter = 0;
                            if (++projectile.frame > 5)
                                projectile.frame = 0;
                        }
                    }
                }
                else //regular movement
                {
                    if (projectile.ai[0] >= 0 && Main.npc[ai0].CanBeChasedBy()) //has target
                    {
                        Vector2 vel = Main.npc[ai0].Center - projectile.Center;
                        projectile.rotation = vel.ToRotation();
                        if (vel.X > 0) //projectile is on left side of target
                        {
                            vel.X -= 300;
                            projectile.direction = projectile.spriteDirection = 1;
                        }
                        else //projectile is on right side of target
                        {
                            vel.X += 300;
                            projectile.direction = projectile.spriteDirection = -1;
                        }
                        vel.Y -= 200f;
                        vel.Normalize();
                        vel *= 12f;
                        if (projectile.velocity.X < vel.X)
                        {
                            projectile.velocity.X += moveSpeed;
                            if (projectile.velocity.X < 0 && vel.X > 0)
                                projectile.velocity.X += moveSpeed;
                        }
                        else if (projectile.velocity.X > vel.X)
                        {
                            projectile.velocity.X -= moveSpeed;
                            if (projectile.velocity.X > 0 && vel.X < 0)
                                projectile.velocity.X -= moveSpeed;
                        }
                        if (projectile.velocity.Y < vel.Y)
                        {
                            projectile.velocity.Y += moveSpeed;
                            if (projectile.velocity.Y < 0 && vel.Y > 0)
                                projectile.velocity.Y += moveSpeed;
                        }
                        else if (projectile.velocity.Y > vel.Y)
                        {
                            projectile.velocity.Y -= moveSpeed;
                            if (projectile.velocity.Y > 0 && vel.Y < 0)
                                projectile.velocity.Y -= moveSpeed;
                        }
                    }
                    else //no target
                    {
                        if (projectile.velocity.X < -1f)
                            projectile.velocity.X += moveSpeed;
                        else if (projectile.velocity.X > 1f)
                            projectile.velocity.X -= moveSpeed;
                        if (projectile.velocity.Y > -8f)
                            projectile.velocity.Y -= moveSpeed;
                        else if (projectile.velocity.Y < -10f)
                            projectile.velocity.Y += moveSpeed;
                        TargetEnemies();
                    }
                    if (++projectile.frameCounter > 5)
                    {
                        projectile.frameCounter = 0;
                        if (++projectile.frame > 5)
                            projectile.frame = 0;
                    }
                }
            }
            projectile.position += projectile.velocity / 4f;
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.defense > 0)
                damage += target.defense / 2;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            target.AddBuff(mod.BuffType("OceanicMaul"), 900);
            target.AddBuff(mod.BuffType("MutantNibble"), 900);
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 900);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(SoundID.Item84, projectile.Center);
            if (projectile.owner == Main.myPlayer)
            {
                SpawnRazorbladeRing(12, 12.5f, 0.75f);
                SpawnRazorbladeRing(12, 10f, -2f);
            }
        }

        private void SpawnRazorbladeRing(int max, float speed, float rotationModifier)
        {
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = projectile.velocity;
            vel.Normalize();
            vel *= speed;
            int type = mod.ProjectileType("RazorbladeTyphoonFriendly");
            for (int i = 0; i < max; i++)
            {
                vel = vel.RotatedBy(rotation);
                Projectile.NewProjectile(projectile.Center, vel, type, projectile.damage / 3,
                    projectile.knockBack / 4f, projectile.owner, rotationModifier * projectile.spriteDirection, projectile.localAI[1] - 1);
            }
        }

        private void TargetEnemies()
        {
            float maxDistance = 1000f;
            int possibleTarget = -1;
            bool isBoss = false;
            for (int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(projectile))// && Collision.CanHitLine(projectile.Center, 0, 0, npc.Center, 0, 0))
                {
                    float npcDistance = projectile.Distance(npc.Center);
                    if (npcDistance < maxDistance && (npc.boss || !isBoss))
                    {
                        if (npc.boss)
                            isBoss = true;
                        maxDistance = npcDistance;
                        possibleTarget = i;
                    }
                }
            }
            projectile.ai[0] = possibleTarget;
            projectile.netUpdate = true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = projectile.GetAlpha(color26);

            SpriteEffects spriteEffects = projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += 2)
            {
                Color color27 = Color.Lerp(color26, Color.Blue, 0.5f);
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value4 = projectile.oldPos[i];
                float num165 = projectile.oldRot[i];
                if (projectile.spriteDirection < 0)
                    num165 += (float)Math.PI;
                Main.spriteBatch.Draw(texture2D13, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, spriteEffects, 0f);
            }

            float drawRotation = projectile.rotation;
            if (projectile.spriteDirection < 0)
                drawRotation += (float)Math.PI;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), drawRotation, origin2, projectile.scale, spriteEffects, 0f);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            float ratio = (255 - projectile.alpha) / 255f;
            float blue = MathHelper.Lerp(ratio, 1f, 0.25f);
            if (blue > 1f)
                blue = 1f;
            return new Color((int)(lightColor.R * ratio), (int)(lightColor.G * ratio), (int)(lightColor.B * blue), (int)(lightColor.A * ratio));
        }
    }
}