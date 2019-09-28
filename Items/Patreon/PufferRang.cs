using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Patreon
{
    public class PufferRang : ModProjectile
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("PufferRang");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            aiType = ProjectileID.EnchantedBoomerang;

            projectile.width = 32;
            projectile.height = 32;
            projectile.penetrate = 4;
        }

        public override void AI()
        {
            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, DustID.Ice, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), .5f);
            Main.dust[dustId].noGravity = true;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            //smaller tile hitbox
            width = 20;
            height = 20;
            return true;
        }
    }
}