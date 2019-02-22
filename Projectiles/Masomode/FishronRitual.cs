using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class FishronRitual : ModProjectile
    {
        //public override string Texture => "Terraria/Projectile_490";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Oceanic Ritual");
        }

        public override void SetDefaults()
        {
            projectile.width = 320;
            projectile.height = 320;
            //projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 300;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            int ai1 = (int)projectile.ai[1];
            if (projectile.ai[1] >= 0f && projectile.ai[1] < 200f &&
                Main.npc[ai1].active && Main.npc[ai1].type == NPCID.DukeFishron)
            {
                projectile.Center = Main.npc[ai1].Center;
            }
            else
            {
                projectile.Kill();
                return;
            }

            projectile.alpha = (Main.npc[ai1].ai[0] < 0f) ? projectile.alpha - 17 : projectile.alpha + 9;
            if (projectile.alpha < 0)
                projectile.alpha = 0;
            if (projectile.alpha > 255)
            {
                projectile.Kill();
                return;
            }
            projectile.scale = 1f - projectile.alpha / 255f;
            projectile.rotation += (float)Math.PI / 70f;

            if (projectile.alpha == 0)
            {
                for (int index1 = 0; index1 < 2; ++index1)
                {
                    float num = Main.rand.Next(2, 4);
                    float scale = projectile.scale * 0.6f;
                    if (index1 == 1)
                    {
                        scale *= 0.42f;
                        num *= -0.75f;
                    }
                    Vector2 vector2 = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                    vector2.Normalize();
                    int index2 = Dust.NewDust(projectile.Center, 0, 0, 135, 0f, 0f, 100, new Color(), 2f);
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = true;
                    Main.dust[index2].position += vector2 * 204f * scale;
                    Main.dust[index2].velocity = vector2 * -num;
                    if (Main.rand.Next(8) == 0)
                    {
                        Main.dust[index2].velocity *= 2f;
                        Main.dust[index2].scale += 0.5f;
                    }
                }
            }

            projectile.ai[0]++;
            if (projectile.ai[0] < 75f)
            {
                int num1 = (int)projectile.ai[0] / 60;
                float num2 = projectile.scale * 0.4f;
                float num3 = Main.rand.Next(1, 3);
                Vector2 vector2 = new Vector2(Main.rand.Next(-10, 11), Main.rand.Next(-10, 11));
                vector2.Normalize();
                int index2 = Dust.NewDust(projectile.Center, 0, 0, 135, 0f, 0f, 100, new Color(), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity = vector2 * num3;
                if (Main.rand.Next(2) == 0)
                {
                    Main.dust[index2].velocity *= 2f;
                    Main.dust[index2].scale += 0.5f;
                }
                Main.dust[index2].fadeIn = 2f;
            }

            Lighting.AddLight(projectile.Center, 0.4f, 0.9f, 1.1f);
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}