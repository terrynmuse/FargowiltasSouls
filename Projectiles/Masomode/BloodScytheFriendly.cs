using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class BloodScytheFriendly : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_44";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Scythe");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.DemonScythe);
            aiType = ProjectileID.DemonScythe;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
            projectile.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.ShadowFlame, 300);
        }
    }
}