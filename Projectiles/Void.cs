using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class Void : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 15;
            projectile.aiStyle = 0;
            projectile.scale = 1f;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 50;
            projectile.timeLeft = 240;
            projectile.tileCollide = false;
            aiType = ProjectileID.Bullet;
            Main.projFrames[projectile.type] = 10;
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++; //Making the timer go up.
            if (projectile.frameCounter >= 4) //Change the 4 to how fast you want the animation to be
            {
                projectile.frame++; //Making the frame go up...
                projectile.frameCounter = 0; //Resetting the timer.
                if (projectile.frame > 9) //Change the 3 to the amount of frames your projectile has.
                    projectile.frame = 0;
            }

            return true;
        }

        public override void AI()
        {
            for (int i = 0; i < 200; i++)
            {
                float distance = projectile.Distance(Main.npc[i].Center);
                NPC npc = Main.npc[i];

                if (!npc.active || npc.townNPC || npc.type == NPCID.TargetDummy || npc.type == NPCID.DD2EterniaCrystal || npc.type == NPCID.DD2LanePortal || npc.boss ||
                    !(distance < 200)) continue;

                Vector2 dir = projectile.position - npc.Center;
                npc.velocity = dir.SafeNormalize(Vector2.Zero) * 8;
            }
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Shadowflame, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 2f;
                dust = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, DustID.Shadowflame, -projectile.velocity.X * 0.2f,
                    -projectile.velocity.Y * 0.2f, 100);
                Main.dust[dust].velocity *= 2f;
            }
        }
    }
}