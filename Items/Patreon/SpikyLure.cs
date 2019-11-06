using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Patreon
{
    public class SpikyLure : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiky Lure");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SpikyBall);
            aiType = ProjectileID.SpikyBall;


            //projectile.penetrate = 4;
        }

        public override void AI()
        {


            /*//dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;*/

        }
    }
}