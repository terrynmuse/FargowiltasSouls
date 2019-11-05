using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class SniperBulletShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("High Velocity Crystal Bullet");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.CrystalShard);
            aiType = ProjectileID.CrystalShard;
            projectile.friendly = false;
            projectile.ranged = false;
            projectile.hostile = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Defenseless"), 1800);

            int buffTime = 300;
            target.AddBuff(mod.BuffType("Crippled"), buffTime);
            target.AddBuff(mod.BuffType("ClippedWings"), buffTime);
        }
    }
}