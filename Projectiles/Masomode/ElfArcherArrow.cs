using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class ElfArcherArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frostburn Arrow");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.FrostArrow);
            aiType = ProjectileID.FrostArrow;
            projectile.friendly = false;
            projectile.ranged = false;
            projectile.arrow = false;
            projectile.hostile = true;
            projectile.coldDamage = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, Main.rand.Next(60, 480));
            target.AddBuff(BuffID.Chilled, Main.rand.Next(900, 1800));
        }
    }
}