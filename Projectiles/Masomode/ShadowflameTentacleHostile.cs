using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class ShadowflameTentacleHostile : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_496";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowflame Tentacle");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.aiStyle = 91;
            aiType = ProjectileID.ShadowFlame;
            projectile.alpha = 255;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.MaxUpdates = 3;
            projectile.penetrate = 3;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Shadowflame"), 300);
        }
    }
}