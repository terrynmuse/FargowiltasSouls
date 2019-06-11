using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class LunarRitual : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_454";

        private const float PI = (float)Math.PI;
        private const float rotationPerTick = PI / 140f;
        private const float threshold = 1600f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lunar Ritual");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.scale *= 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            int ai1 = (int)projectile.ai[1];
            if (projectile.ai[1] >= 0f && projectile.ai[1] < 200f &&
                Main.npc[ai1].active && Main.npc[ai1].type == NPCID.MoonLordCore)
            {
                projectile.alpha -= 2;
                if (projectile.alpha < 0)
                    projectile.alpha = 0;

                projectile.Center = Main.npc[ai1].Center;

                Player player = Main.player[Main.myPlayer];
                if (player.active && !player.dead)
                {
                    float distance = player.Distance(projectile.Center);
                    if (Math.Abs(distance - threshold) < 46f && player.hurtCooldowns[0] == 0 && projectile.alpha == 0)
                    {
                        int hitDirection = projectile.Center.X > player.Center.X ? 1 : -1;
                        player.Hurt(PlayerDeathReason.ByProjectile(player.whoAmI, projectile.whoAmI),
                            projectile.damage, hitDirection, false, false, false, 0);
                        player.AddBuff(mod.BuffType("CurseoftheMoon"), Main.rand.Next(300, 600));
                    }
                    if (distance > threshold && distance < threshold * 2f)
                    {
                        if (distance > threshold * 1.5f)
                        {
                            player.frozen = true;
                            player.controlHook = false;
                            player.controlUseItem = false;
                            if (player.mount.Active)
                                player.mount.Dismount(player);
                            player.velocity.X = 0f;
                            player.velocity.Y = -0.4f;
                        }

                        Vector2 movement = projectile.Center - player.Center;
                        float difference = movement.Length() - threshold;
                        movement.Normalize();
                        movement *= difference < 17f ? difference : 17f;
                        player.position += movement;

                        for (int i = 0; i < 20; i++)
                        {
                            int d = Dust.NewDust(player.position, player.width, player.height, 135, 0f, 0f, 0, default(Color), 2.5f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].noLight = true;
                            Main.dust[d].velocity *= 5f;
                        }
                    }
                }
            }
            else
            {
                projectile.velocity = Vector2.Zero;
                projectile.alpha += 2;
                if (projectile.alpha > 255)
                {
                    projectile.Kill();
                    return;
                }
            }

            projectile.timeLeft = 2;
            projectile.scale = (1f - projectile.alpha / 255f) * 2f;
            projectile.ai[0] -= rotationPerTick;
            if (projectile.ai[0] < -PI)
            {
                projectile.ai[0] += 2f * PI;
                projectile.netUpdate = true;
            }

            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 1)
                    projectile.frame = 0;
            }
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = projectile.GetAlpha(lightColor);

            for (int x = 0; x < 32; x++)
            {
                Vector2 drawOffset = new Vector2(threshold * projectile.scale / 2f, 0f).RotatedBy(projectile.ai[0]);
                drawOffset = drawOffset.RotatedBy(2f * PI / 32f * x);
                const int max = 4;
                for (int i = 0; i < max; i++)
                {
                    Color color27 = color26;
                    color27 *= (float)(max - i) / max;
                    Vector2 value4 = projectile.Center + drawOffset.RotatedBy(rotationPerTick * i);
                    float num165 = projectile.rotation;
                    Main.spriteBatch.Draw(texture2D13, value4 - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, SpriteEffects.None, 0f);
                }
                Main.spriteBatch.Draw(texture2D13, projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }
    }
}