using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class VenomSpit : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_472";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom Spit");
        }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            aiType = ProjectileID.WebSpit;
            projectile.hostile = true;
            projectile.timeLeft = 300;
        }

        public override void Kill(int timeLeft)
        {
            for (int index1 = 0; index1 < 20; ++index1)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 30);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.45f;
                Main.dust[d].velocity += projectile.velocity * 0.9f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, lightColor.G, 255, lightColor.A);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Venom, 600);
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