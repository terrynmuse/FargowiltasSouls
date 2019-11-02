using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class FuseBomb : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/Explosion";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fuse Bomb");
        }

        public override void SetDefaults()
        {
            projectile.width = 300;
            projectile.height = 300;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hide = true;
            projectile.extraUpdates = 1;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 600);
            target.AddBuff(mod.BuffType("Defenseless"), 600);
            target.AddBuff(BuffID.WitheredArmor, 600);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int) projectile.Center.X, (int) projectile.Center.Y, 14);

            for (int num615 = 0; num615 < 45; num615++)
            {
                int num616 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num616].velocity *= 1.4f;
            }

            for (int num617 = 0; num617 < 30; num617++)
            {
                int num618 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                Main.dust[num618].noGravity = true;
                Main.dust[num618].velocity *= 7f;
                num618 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num618].velocity *= 3f;
            }

            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 0.4f;
                if (num619 == 1) scaleFactor9 = 0.8f;
                int num620 = Gore.NewGore(projectile.Center, default(Vector2), Main.rand.Next(61, 64));
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;
                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;
                num620 = Gore.NewGore(projectile.Center, default(Vector2), Main.rand.Next(61, 64));
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;
                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;
                num620 = Gore.NewGore(projectile.Center, default(Vector2), Main.rand.Next(61, 64));
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;
                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;
                num620 = Gore.NewGore(projectile.Center, default(Vector2), Main.rand.Next(61, 64));
                Main.gore[num620].velocity *= scaleFactor9;
                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;
                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
            }
        }
    }
}