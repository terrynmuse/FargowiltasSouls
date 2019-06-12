using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class GolemGib : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Golem Gibs");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 24;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            //aiType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("Explosion"),
                    projectile.damage, projectile.knockBack * 2f, projectile.owner);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item84, projectile.Center);

            if (projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("Explosion"),
                    projectile.damage, projectile.knockBack * 2f, projectile.owner);

            int num1 = 36;
            for (int index1 = 0; index1 < num1; ++index1)
            {
                Vector2 vector2_1 = (Vector2.Normalize(projectile.velocity) * new Vector2((float)projectile.width / 2f, (float)projectile.height) * 0.75f).RotatedBy((double)(index1 - (num1 / 2 - 1)) * 6.28318548202515 / (double)num1, new Vector2()) + projectile.Center;
                Vector2 vector2_2 = vector2_1 - projectile.Center;
                int index2 = Dust.NewDust(vector2_1 + vector2_2, 0, 0, 172, vector2_2.X * 2f, vector2_2.Y * 2f, 100, new Color(), 1.4f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
                Main.dust[index2].velocity = vector2_2;
            }
        }
    }
}