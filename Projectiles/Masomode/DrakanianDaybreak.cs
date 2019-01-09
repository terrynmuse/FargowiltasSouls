using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class DrakanianDaybreak : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Daybreak");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Daybreak);
            aiType = ProjectileID.Daybreak;
            projectile.friendly = false;
            projectile.thrown = false;
            projectile.hostile = true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, Main.rand.Next(300, 600));
            target.AddBuff(BuffID.Burning, Main.rand.Next(30, 120));
        }
    }
}