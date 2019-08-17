using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class BloodScythe : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_44";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Sickle");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.DemonSickle);
            aiType = ProjectileID.DemonSickle;
            projectile.hostile = true;
            projectile.friendly = false;
            projectile.magic = false;
            projectile.timeLeft = 300;
            projectile.tileCollide = false;
            cooldownSlot = 1;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.Red;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Shadowflame"), 300);
            target.AddBuff(BuffID.Bleeding, 600);
        }
    }
}