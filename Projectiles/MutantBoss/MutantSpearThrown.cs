using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantSpearThrown : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/BossWeapons/HentaiSpear";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Penetrator");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.extraUpdates = 1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;
            cooldownSlot = 1;
            projectile.GetGlobalProjectile<FargoGlobalProjectile>().TimeFreezeImmune = true;
        }

        public override void AI()
        {
            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width / 2, projectile.height + 5, 15, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width / 2, projectile.height + 5, 15, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId3].noGravity = true;
            
            if (--projectile.localAI[0] < 0)
            {
                projectile.localAI[0] = 4;
                if (projectile.ai[1] == 0 && Main.netMode != 1)
                    Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("MutantSphereSmall"), projectile.damage, 0f, projectile.owner, projectile.ai[0]);
            }

            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
                Main.PlaySound(SoundID.Item1, projectile.Center);
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            Projectile.NewProjectile(target.Center + Main.rand.NextVector2Circular(100, 100), Vector2.Zero, mod.ProjectileType("PhantasmalBlast"), 0, 0f, projectile.owner);
            target.GetModPlayer<FargoPlayer>().MaxLifeReduction += 100;
            target.AddBuff(mod.BuffType("OceanicMaul"), 5400);
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 600);
            target.AddBuff(mod.BuffType("MutantFang"), 180);
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
    }
}