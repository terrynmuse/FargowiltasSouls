using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class Explosion : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Explosion");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 1000;
            projectile.height = 1000;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 10;
            projectile.tileCollide = false;
            projectile.light = 0.75f;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int) projectile.position.X, (int) projectile.position.Y, 14);
            projectile.position.X = projectile.position.X + projectile.width / 2;
            projectile.position.Y = projectile.position.Y + projectile.height / 2;
            projectile.width = 100;
            projectile.height = 100;
            projectile.position.X = projectile.position.X - projectile.width / 2;
            projectile.position.Y = projectile.position.Y - projectile.height / 2;
            for (int num615 = 0; num615 < 30; num615++)
            {
                int num616 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num616].velocity *= 1.4f;
            }

            for (int num617 = 0; num617 < 20; num617++)
            {
                int num618 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                Main.dust[num618].noGravity = true;
                Main.dust[num618].velocity *= 7f;
                num618 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num618].velocity *= 3f;
            }

            for (int num619 = 0; num619 < 2; num619++)
            {
                float scaleFactor9 = 0.4f;
                if (num619 == 1)
                {
                    scaleFactor9 = 0.8f;
                }

                for (int i = 0; i < 4; i++)
                {
                    int num620 = Gore.NewGore(new Vector2(projectile.position.X, projectile.position.Y),
                        default(Vector2),
                        Main.rand.Next(61, 64));

                    Main.gore[num620].velocity *= scaleFactor9;
                    Main.gore[num620].velocity.X += 1f;
                    Main.gore[num620].velocity.Y += 1f;
                }
            }
        }
    }
}