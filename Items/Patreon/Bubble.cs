using FargowiltasSouls.Projectiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Patreon
{
    public class Bubble : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bubble");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Bubble);
            aiType = ProjectileID.Bubble;

            projectile.penetrate = 4;
        }

        public override void AI()
        {


            /*//dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;*/

        }

        public override void Kill(int timeLeft)
        {
            FargoGlobalProjectile.XWay(4, projectile.position, mod.ProjectileType("WaterStream"), 5, projectile.damage / 2, projectile.knockBack / 2);
        }
    }
}