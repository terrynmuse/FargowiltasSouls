using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class GolemBoulder : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_261";

        public float vel = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boulder");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.BoulderStaffOfEarth);
            aiType = ProjectileID.BoulderStaffOfEarth;
            projectile.hostile = true;
            projectile.magic = false;
            projectile.friendly = false;
            projectile.tileCollide = false;
            projectile.alpha = 255;
        }

        public override bool CanDamage()
        {
            return projectile.alpha == 0;
        }

        public override void AI()
        {
            projectile.alpha -= 5;
            if (projectile.alpha < 0)
                projectile.alpha = 0;

            if (!projectile.tileCollide)
            {
                Tile tile = Framing.GetTileSafely(projectile.Center - Vector2.UnitY * 26);
                if (!(tile.nactive() && Main.tileSolid[tile.type]))
                    projectile.tileCollide = true;
            }

            if (projectile.velocity.Y < 0 && projectile.velocity.X == 0)
            {
                int p = Player.FindClosest(projectile.Center, 0, 0);
                if (p != -1)
                {
                    projectile.velocity.X = vel = projectile.Center.X < Main.player[p].Center.X ? 5f : -5f;
                    projectile.velocity.Y = 0;
                }
                else
                {
                    projectile.timeLeft = 0;
                }
            }

            if (Math.Sign(projectile.velocity.X) == Math.Sign(vel))
                projectile.velocity.X = vel;
            else
                projectile.timeLeft = 0;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 26;
            height = 26;
            fallThrough = false;
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0.0f);
            for (int index = 0; index < 5; ++index)
                Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 148, 0.0f, 0.0f, 0, new Color(), 1f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("Defenseless"), 600);
            target.AddBuff(BuffID.WitheredArmor, 600);
        }
    }
}