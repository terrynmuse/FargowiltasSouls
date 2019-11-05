using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class GoblinSpikyBall : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_24";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spiky Ball");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SpikyBall);
            aiType = ProjectileID.SpikyBall;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.thrown = false;
            projectile.penetrate = 1;
            projectile.timeLeft /= 6;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }
    }
}