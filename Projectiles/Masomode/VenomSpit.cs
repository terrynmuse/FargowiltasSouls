using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class VenomSpit : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_472";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Venom Spit");
        }

        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.aiStyle = 1;
            aiType = ProjectileID.WebSpit;
            projectile.hostile = true;
            projectile.timeLeft = 300;
        }

        public override void Kill(int timeLeft)
        {
            for (int index1 = 0; index1 < 20; ++index1)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 30);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 0.45f;
                Main.dust[d].velocity += projectile.velocity * 0.9f;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(lightColor.R / 2 + 127, lightColor.G, lightColor.B / 2 + 127, lightColor.A);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Venom, 600);
        }
    }
}