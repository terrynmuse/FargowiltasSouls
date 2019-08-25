using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantSpearThrownFriendly : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/BossWeapons/HentaiSpear";

        //throw with 25 velocity, 1000 damage, 10 knockback

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Penetrator");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 58;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 180;
            projectile.extraUpdates = 1;
            projectile.scale = 1.3f;
            projectile.alpha = 0;
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
                if (Main.netMode != 1)
                    Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("PhantasmalSphere"), projectile.damage, projectile.knockBack / 2, projectile.owner);
            }

            if (projectile.localAI[1] == 0f)
            {
                projectile.localAI[1] = 1f;
                Main.PlaySound(SoundID.Item1, projectile.Center);
            }

            projectile.rotation = projectile.velocity.ToRotation() + MathHelper.ToRadians(135f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.NewProjectile(target.position + new Vector2(Main.rand.Next(target.width), Main.rand.Next(target.height)), Vector2.Zero, mod.ProjectileType("PhantasmalBlast"), projectile.damage, 0f, projectile.owner);
            target.AddBuff(mod.BuffType("Sadism"), 600);
            target.AddBuff(mod.BuffType("GodEater"), 600);
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