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
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(120, 600));
                target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 300));
                bool isVenomed = false;
                for (int i = 0; i < 22; i++)
                {
                    if (target.buffType[i] == BuffID.Venom && target.buffTime[i] > 1)
                    {
                        isVenomed = true;
                        target.buffTime[i] += Main.rand.Next(60, 300);
                        if (target.buffTime[i] > 1200)
                        {
                            target.AddBuff(mod.BuffType("Infested"), target.buffTime[i]);
                            Main.PlaySound(15, (int)target.Center.X, (int)target.Center.Y, 0);
                        }
                        break;
                    }
                }
                if (!isVenomed)
                {
                    target.AddBuff(BuffID.Venom, Main.rand.Next(60, 300));
                }
            }
        }
    }
}