using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class SaucerLaser : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_449";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Laser");
        }

        public override void SetDefaults()
        {
            projectile.width = 5;
            projectile.height = 5;
            projectile.aiStyle = 1;
            aiType = ProjectileID.SaucerLaser;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.extraUpdates = 1;
            projectile.timeLeft = 600;
            projectile.minion = true;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
        }

        public override void Kill(int timeLeft)
        {
            int num = Main.rand.Next(3, 7);
            for (int index1 = 0; index1 < num; ++index1)
            {
                int index2 = Dust.NewDust(projectile.Center - projectile.velocity / 2f, 0, 0, 228, 0.0f, 0.0f, 100, new Color(), 2.1f);
                Dust dust = Main.dust[index2];
                dust.velocity = dust.velocity * 2f;
                Main.dust[index2].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
    }
}