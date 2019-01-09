using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class SkeletonArcherArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom Arrow");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.VenomArrow);
            aiType = ProjectileID.VenomArrow;
            projectile.friendly = false;
            projectile.ranged = false;
            projectile.arrow = false;
            projectile.hostile = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Venom, Main.rand.Next(60, 480));
        }
    }
}