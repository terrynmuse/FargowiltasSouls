using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    internal class DicerProj : ModProjectile
    {
        public int Counter = 1;

        public override void SetStaticDefaults()
        {
            // Vanilla values range from 3f(Wood) to 16f(Chik), and defaults to -1f. Leaving as -1 will make the time infinite.
            ProjectileID.Sets.YoyosLifeTimeMultiplier[projectile.type] = 16f;
            // Vanilla values range from 130f(Wood) to 400f(Terrarian), and defaults to 200f
            ProjectileID.Sets.YoyosMaximumRange[projectile.type] = 275;
            // Vanilla values range from 9f(Wood) to 17.5f(Terrarian), and defaults to 10f
            ProjectileID.Sets.YoyosTopSpeed[projectile.type] = 15f;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Kraken);
            projectile.extraUpdates = 0;
            projectile.width = 30;
            projectile.height = 30;
            //yoyo ai
            projectile.aiStyle = 99;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.melee = true;
            projectile.scale = 1f;
        }

        public override void AI()
        {
            if (Counter % 30 == 0)
            {
                int proj2 = mod.ProjectileType("DicerProj2"); 

                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, proj2, projectile.damage, 0, Main.myPlayer);
            }

            Counter++;
        }

        public override void PostAI()
        {
            /*if (Main.rand.Next(2) == 0)
            {
                int dustIndex = Dust.NewDust(projectile.position, projectile.width, projectile.height, 16, 0f, 0f, 0, default(Color), 1f);
                Main.dust[dustIndex].noGravity = true;
                Main.dust[dustIndex].scale = 1.6f;
            }*/
        }
    }
}