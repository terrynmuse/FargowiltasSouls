using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantGuardian : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Skeletron Prime");
            Main.projFrames[projectile.type] = 3;
        }

		public override void SetDefaults()
		{
			projectile.width = 40;
			projectile.height = 50;
			projectile.aiStyle = 0;
			aiType = ProjectileID.Bullet;
            projectile.hostile = true;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.timeLeft = 300;
		}
		
        public override void AI()
        {
            projectile.frame = 2;
            projectile.rotation += 0.2f * Math.Sign(projectile.velocity.X);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("GodEater"), 420);
            target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 420);
            target.AddBuff(mod.BuffType("MarkedforDeath"), 420);
            target.AddBuff(mod.BuffType("Defenseless"), 480);
            target.AddBuff(mod.BuffType("MutantFang"), 300);
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 50; i++)
            {
                Vector2 pos = new Vector2(projectile.Center.X + Main.rand.Next(-20, 20), projectile.Center.Y + Main.rand.Next(-20, 20));
                int dust = Dust.NewDust(pos, projectile.width, projectile.height, DustID.Blood, 0, 0, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
            }
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

