using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class EaterRocket : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater Rocket");
        }
        public override void SetDefaults()
        {
            projectile.width = 17;
            projectile.height = 23;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 99;
            projectile.timeLeft = 600;
            aiType = ProjectileID.Bullet;
        }

        public override void AI()
        {
            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, DustID.PurpleCrystalShard, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 1f);
            Main.dust[dustId].noGravity = true;

            if (projectile.penetrate < 99 && projectile.ai[1] != 1)
            {
                projectile.ai[1] = 1;
                projectile.timeLeft = 10;
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            Player owner = Main.player[projectile.owner];
            float dist = Vector2.Distance(target.Center, owner.Center);

            if (dist > 300 && dist < 400)
            {
                damage *= 2;
                crit = true;
            }
            else
            {
                crit = false;
            }
        }

        public override void Kill(int timeLeft)
        {
            //dust
            for (int num468 = 0; num468 < 20; num468++)
            {
                int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.PurpleCrystalShard, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color), 1.5f);
                Main.dust[num469].noGravity = true;
                Main.dust[num469].velocity *= 2f;
                num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.PurpleCrystalShard, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[num469].velocity *= 1.5f;
            }

            if (projectile.ai[1] == 1)
            {
                for (int i = 0; i < 5; i++)
                    Projectile.NewProjectile(projectile.Center, new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), ProjectileID.TinyEater, projectile.damage / 4, 1f, Main.myPlayer);
            }
        }


    }
}