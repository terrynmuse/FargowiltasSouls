using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class Retirang : ModProjectile
    {
        private int counter = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Retirang");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.EnchantedBoomerang);
            aiType = ProjectileID.EnchantedBoomerang;

            projectile.width = 22;
            projectile.height = 66;
            projectile.penetrate = 4;
        }

        public override void AI()
        {
            counter++;

            if (counter >= 15)
            {
                for (int k = 0; k < 200; k++)
                {
                    NPC npc = Main.npc[k];
                    float distance = Vector2.Distance(npc.Center, projectile.Center);

                    if ((distance < 500) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
                    {
                        Vector2 velocity = (npc.Center - projectile.Center) * 20;

                        Projectile.NewProjectile(projectile.Center, velocity, ProjectileID.PurpleLaser, projectile.damage / 2, 0, projectile.owner);
                        break;
                    }
                }

                counter = 0;
            }

            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;

        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            //smaller tile hitbox
            width = 22;
            height = 22;
            return true;
        }
    }
}