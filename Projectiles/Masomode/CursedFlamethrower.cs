using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class CursedFlamethrower : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_101";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eye Fire");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EyeFire); //has 4 updates per tick
            aiType = ProjectileID.EyeFire;
            projectile.magic = false;
            projectile.tileCollide = false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (Main.rand.Next(6) == 0)
                target.AddBuff(39, 480, true);
            else if (Main.rand.Next(4) == 0)
                target.AddBuff(39, 300, true);
            else if (Main.rand.Next(2) == 0)
                target.AddBuff(39, 180, true);

            target.AddBuff(BuffID.OnFire, Main.rand.Next(60, 300));
            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(120, 240));
            target.AddBuff(mod.BuffType("Crippled"), 60);
        }
    }
}