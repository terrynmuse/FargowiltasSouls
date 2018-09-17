using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class HellFlame : ModProjectile
    {
        private static int _currentShade = 76; //77;//79;//83;//82;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 4;
            projectile.extraUpdates = 2;
            projectile.ranged = true;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Black;
        }

        public override void AI()
        {
            if (projectile.timeLeft > 60) projectile.timeLeft = 60;
            if (projectile.ai[1] > 5f)
            {
                Dust dust;
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.SolarFlare, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
                dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);

                if (Main.rand.Next(3) != 0) dust.scale *= 2f;

                dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.SolarFlare, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100);
                dust = Main.dust[dustIndex];
                dust.noGravity = true;
                dust.shader = GameShaders.Armor.GetSecondaryShader(56, Main.LocalPlayer);

                if (Main.rand.Next(3) != 0) dust.scale *= 2f;

                dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0, default(Color),
                    1f);
                dust = Main.dust[dustIndex];
                if (Main.rand.Next(3) != 0)
                {
                    dust.scale *= 1.5f;
                    dust.velocity *= 2f;
                }

                dust.velocity *= 1.2f;

                dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 0,
                    default(Color), .5f);
                dust = Main.dust[dustIndex];
                if (Main.rand.Next(3) != 0)
                {
                    dust.scale *= 2f;
                    dust.velocity *= 2f;
                }

                dust.velocity *= 1.2f;
            }
            else
            {
                projectile.ai[1] += 1f;
            }

            projectile.rotation += 0.3f * projectile.direction;
        }

        public override void ModifyDamageHitbox(ref Rectangle hitbox)
        {
            hitbox.X -= 30;
            hitbox.Y -= 30;
            hitbox.Width += 60;
            hitbox.Height += 60;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("HellFire"), 300);
        }

        public override void OnHitPvp(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("HellFire"), 300);
        }
    }
}