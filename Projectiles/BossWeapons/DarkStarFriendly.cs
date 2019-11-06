using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class DarkStarFriendly : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_12";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Star");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 5;
            aiType = ProjectileID.FallingStar;
            projectile.alpha = 50;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 900;
            projectile.friendly = true;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            Lighting.AddLight(projectile.Center, 0.9f, 0.8f, 0.1f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.ai[0] == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    Vector2 pos = projectile.Center + projectile.velocity * 30f;//new Vector2(projectile.Center.X + Main.rand.Next(-150, 150), projectile.Center.Y - 500);
                    pos.X += Main.rand.Next(-150, 150);
                    pos.Y += Main.rand.Next(-150, 150);
                    Vector2 velocity = Vector2.Normalize(target.Center - pos) * 15;

                    int p = Projectile.NewProjectile(pos, velocity, mod.ProjectileType("DarkStarFriendly"), projectile.damage / 2, projectile.knockBack, projectile.owner);

                    
                    Main.projectile[p].ai[0] = 1;

                    Main.projectile[p].tileCollide = false;
                    Main.projectile[p].ai[1] = 0f;
                    Main.projectile[p].netUpdate = true;
                }

                projectile.ai[0] = 1;
            }
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            int num1 = 10;
            int num2 = 3;

            for (int index = 0; index < num1; ++index)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 58, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, new Color(), 1.2f);
            for (int index = 0; index < num2; ++index)
            {
                int Type = Main.rand.Next(16, 18);
                if (projectile.type == 503)
                    Type = 16;
                Gore.NewGore(projectile.position, new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f), Type, 1f);
            }

            for (int index = 0; index < 10; ++index)
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 57, projectile.velocity.X * 0.1f, projectile.velocity.Y * 0.1f, 150, new Color(), 1.2f);
            for (int index = 0; index < 3; ++index)
                Gore.NewGore(projectile.position, new Vector2(projectile.velocity.X * 0.05f, projectile.velocity.Y * 0.05f), Main.rand.Next(16, 18), 1f);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 100, 100, lightColor.A - projectile.alpha);
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