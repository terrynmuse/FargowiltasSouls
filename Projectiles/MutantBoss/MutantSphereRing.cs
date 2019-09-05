using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantSphereRing : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_454";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Sphere");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 480;
            projectile.alpha = 200;
            cooldownSlot = 1;
        }

        public override bool CanHitPlayer(Player target)
        {
            return target.hurtCooldowns[1] == 0;
        }

        public override void AI()
        {
            //float ratio = projectile.timeLeft / 600f;
            //projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[0] * ratio + projectile.ai[1] * (1 - ratio));
            /*projectile.localAI[0] += projectile.ai[0] * projectile.timeLeft / 300f;
            projectile.velocity.X = (float)(Math.Cos(projectile.localAI[0] + projectile.ai[1]) - projectile.localAI[0] * Math.Sin(projectile.localAI[0] + projectile.ai[1]));
            projectile.velocity.Y = (float)(Math.Sin(projectile.localAI[0] + projectile.ai[1]) + projectile.localAI[0] * Math.Cos(projectile.localAI[0] + projectile.ai[1]));*/
            //projectile.velocity *= (projectile.timeLeft > 300 ? projectile.timeLeft / 300f : 1f);
            //Main.NewText(projectile.velocity.Length().ToString());
            //projectile.velocity *= 1f + projectile.ai[0];
            //projectile.velocity += projectile.velocity.RotatedBy(Math.PI / 2) * projectile.ai[1];
            /*if (spawn == Vector2.Zero)
                spawn = projectile.position;
            projectile.localAI[0] += projectile.ai[0] * (projectile.timeLeft > 300 ? projectile.timeLeft / 300f : 1f);
            Vector2 vel = new Vector2(projectile.localAI[0] * (float)Math.Cos(projectile.localAI[0] + projectile.ai[1]) * 120f,
                projectile.localAI[0] * (float)Math.Sin(projectile.localAI[0] + projectile.ai[1]) * 120f);
            projectile.position = spawn + vel;
            vel = projectile.position - projectile.oldPosition;*/
            projectile.velocity = projectile.velocity.RotatedBy(projectile.ai[1] / (2 * Math.PI * projectile.ai[0] * ++projectile.localAI[0]));

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 20;
                if (projectile.alpha < 0)
                    projectile.alpha = 0;
            }
            projectile.scale = (1f - projectile.alpha / 255f);
            for (int i = 0; i < 2; i++)
            {
                float num = Main.rand.NextFloat(-0.5f, 0.5f);
                Vector2 vector2 = new Vector2(-projectile.width * 0.65f * projectile.scale, 0.0f).RotatedBy(num * 6.28318548202515, new Vector2()).RotatedBy(projectile.velocity.ToRotation(), new Vector2());
                int index2 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 229, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                Main.dust[index2].velocity = Vector2.Zero;
                Main.dust[index2].position = projectile.Center + vector2;
                Main.dust[index2].noGravity = true;
            }
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame > 1)
                    projectile.frame = 0;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.GetModPlayer<FargoPlayer>().MaxLifeReduction += 100;
            target.AddBuff(mod.BuffType("OceanicMaul"), 5400);
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 300);
            target.AddBuff(mod.BuffType("MutantFang"), 300);
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(4, projectile.Center, 6);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 208;
            projectile.Center = projectile.position;
            for (int index1 = 0; index1 < 3; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Main.dust[index2].position = new Vector2((float)(projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
            }
            for (int index1 = 0; index1 < 10; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
                Main.dust[index2].position = new Vector2((float)(projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
                Main.dust[index2].noGravity = true;
                Dust dust1 = Main.dust[index2];
                dust1.velocity = dust1.velocity * 1f;
                int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Main.dust[index3].position = new Vector2((float)(projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
                Dust dust2 = Main.dust[index3];
                dust2.velocity = dust2.velocity * 1f;
                Main.dust[index3].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}