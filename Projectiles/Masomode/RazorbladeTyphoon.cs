using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class RazorbladeTyphoon : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_409";
        //public Vector2 spawn = Vector2.Zero;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Razorblade Typhoon");
            Main.projFrames[projectile.type] = 3;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 8;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 600;
            projectile.alpha = 100;
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

            //vanilla typhoon dust (ech)
            int cap = Main.rand.Next(3);
            for (int index1 = 0; index1 < cap; ++index1)
            {
                Vector2 vector2_1 = projectile.velocity;
                vector2_1.Normalize();
                vector2_1.X *= projectile.width;
                vector2_1.Y *= projectile.height;
                vector2_1 /= 2;
                vector2_1 = vector2_1.RotatedBy((index1 - 2) * Math.PI / 6);
                vector2_1 += projectile.Center;
                Vector2 vector2_2 = (Main.rand.NextFloat() * (float)Math.PI - (float)Math.PI / 2f).ToRotationVector2();
                vector2_2 *= Main.rand.Next(3, 8);
                int index2 = Dust.NewDust(vector2_1 + vector2_2, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity /= 4f;
                Main.dust[index2].velocity -= projectile.velocity;
            }
            projectile.rotation += 0.2f * (projectile.velocity.X > 0f ? 1f : -1f);
            projectile.frame++;
            if (projectile.frame > 2)
                projectile.frame = 0;
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(600, 900));
            player.AddBuff(BuffID.WitheredWeapon, Main.rand.Next(300, 600));
            if (NPCs.FargoSoulsGlobalNPC.BossIsAlive(ref NPCs.FargoSoulsGlobalNPC.fishBossEX, NPCID.DukeFishron))
            {
                player.GetModPlayer<FargoPlayer>().MaxLifeReduction += 50;
                player.AddBuff(mod.BuffType("OceanicMaul"), Main.rand.Next(1800, 3600));
            }
        }

        public override void Kill(int timeLeft)
        {
            int num1 = 36;
            for (int index1 = 0; index1 < num1; ++index1)
            {
                Vector2 vector2_1 = (Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f).RotatedBy((double)(index1 - (num1 / 2 - 1)) * 6.28318548202515 / (double)num1, new Vector2()) + projectile.Center;
                Vector2 vector2_2 = vector2_1 - projectile.Center;
                int index2 = Dust.NewDust(vector2_1 + vector2_2, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity = vector2_2;
            }
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

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += 2)
            {
                Color color27 = color26;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value4 = projectile.oldPos[i];
                float num165 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D13, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, SpriteEffects.None, 0f);
            }

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(100, 100, 250, 200);
        }
    }
}