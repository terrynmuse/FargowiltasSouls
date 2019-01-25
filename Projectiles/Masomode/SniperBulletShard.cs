using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
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
            target.AddBuff(mod.BuffType("Defenseless"), Main.rand.Next(1200, 3600));

            int buffTime = Main.rand.Next(300, 600);
            target.AddBuff(mod.BuffType("Crippled"), buffTime);
            target.AddBuff(mod.BuffType("ClippedWings"), buffTime);
        }
    }
}