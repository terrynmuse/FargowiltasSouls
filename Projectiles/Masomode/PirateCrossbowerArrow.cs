using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class PirateCrossbowerArrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jester's Arrow");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.JestersArrow);
            aiType = ProjectileID.JestersArrow;
            projectile.friendly = false;
            projectile.ranged = false;
            projectile.arrow = false;
            projectile.hostile = true;
        }

        public override void AI()
        {
            int Type;
            switch (Main.rand.Next(3))
            {
                case 0: Type = 15; break;
                case 1: Type = 57; break;
                default: Type = 58; break;
            }
            Dust.NewDust(projectile.position, projectile.width, projectile.height, Type, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 150, default(Color), 1.2f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Midas"), Main.rand.Next(300, 900));
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 0);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int i = 0; i < 60; i++)
            {
                int Type;
                switch (Main.rand.Next(3))
                {
                    case 0: Type = 15; break;
                    case 1: Type = 57; break;
                    default: Type = 58; break;
                }
                Dust.NewDust(projectile.position, projectile.width, projectile.height, Type, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 150, default(Color), 1.2f);
            }
        }
    }
}