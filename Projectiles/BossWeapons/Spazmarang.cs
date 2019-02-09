using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class Spazmarang : ModProjectile
    {
        private int counter = 0;
        

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spazmarang");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            aiType = ProjectileID.EnchantedBoomerang;

            projectile.width = 22;
            projectile.height = 66;
            projectile.penetrate = 4;
        }

        public override void AI()
        {
            counter++;

            if (counter >= 30)
            {
                Vector2[] velocity = { projectile.velocity / 2, -projectile.velocity / 2, projectile.velocity.RotatedBy(Math.PI / 2) / 2, -projectile.velocity.RotatedBy(Math.PI / 2) / 2 };

                for (int i = 0; i < 4; i++)
                {
                    Projectile p = Projectile.NewProjectileDirect(projectile.Center, velocity[i], ProjectileID.EyeFire, projectile.damage / 2, 0, projectile.owner);
                    p.hostile = false;
                    p.friendly = true;
                }

                counter = 0;
            }

            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 75, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            //smaller tile hitbox
            width = 22;
            height = 22;
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.CursedInferno, 120);
        }
    }
}