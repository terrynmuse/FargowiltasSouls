using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class CrystalLeafShot : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_227";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Leaf");
        }

        public override void SetDefaults()
        {
            projectile.light = 0.1f;
            projectile.width = 14;
            projectile.height = 14;
            projectile.hostile = true;
            projectile.tileCollide = false;
            projectile.timeLeft = 180;
            projectile.aiStyle = 43;
            aiType = ProjectileID.CrystalLeafShot;
            cooldownSlot = 1;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (target.hurtCooldowns[1] == 0)
            {
                target.AddBuff(BuffID.Poisoned, 300);
                target.AddBuff(mod.BuffType("Infested"), 300);
                target.AddBuff(mod.BuffType("IvyVenom"), 300);
            }
        }
    }
}