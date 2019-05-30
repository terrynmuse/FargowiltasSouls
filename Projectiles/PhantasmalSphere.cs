using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class PhantasmalSphere : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_454";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Sphere");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 120;
            projectile.tileCollide = false;
            projectile.alpha = 200;
            projectile.GetGlobalProjectile<FargoGlobalProjectile>().CanSplit = false;
        }

        public override void AI()
        {
            //dust!
            /*int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width / 2, projectile.height + 5, 56, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), .5f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width / 2, projectile.height + 5, 56, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), .5f);
            Main.dust[dustId3].noGravity = true;*/

            const int aislotHomingCooldown = 0;
            const int homingDelay = 20;
            const float desiredFlySpeedInPixelsPerFrame = 60;
            const float amountOfFramesToLerpBy = 20; // minimum of 1, please keep in full numbers even though it's a float!
            if (++projectile.ai[aislotHomingCooldown] > homingDelay)
            {
                projectile.ai[aislotHomingCooldown] = 0; //reset
                int foundTarget = HomeOnTarget();
                if (foundTarget != -1)
                {
                    NPC n = Main.npc[foundTarget];
                    Vector2 desiredVelocity = projectile.DirectionTo(n.Center) * desiredFlySpeedInPixelsPerFrame;
                    projectile.velocity = Vector2.Lerp(projectile.velocity, desiredVelocity, 1f / amountOfFramesToLerpBy);
                }
            }

            if (projectile.alpha > 0)
            {
                projectile.alpha -= 20;
                if (projectile.alpha < 0)
                    projectile.alpha = 0;
            }
            projectile.scale = (1f - projectile.alpha / 255f) * .75f;
            for (int i = 0; i < 2; i++)
            {
                float num = Main.rand.NextFloat(-0.5f, 0.5f);
                Vector2 vector2 = new Vector2(-projectile.width * 0.65f * projectile.scale, 0.0f).RotatedBy(num * 6.28318548202515, new Vector2()).RotatedBy(projectile.velocity.ToRotation(), new Vector2());
                int index2 = Dust.NewDust(projectile.Center - Vector2.One * 5f, 10, 10, 229, -projectile.velocity.X / 3f, -projectile.velocity.Y / 3f, 150, Color.Transparent, 0.7f);
                Main.dust[index2].velocity = Vector2.Zero;
                Main.dust[index2].position = projectile.Center + vector2;
                Main.dust[index2].noGravity = true;
            }
            if (++projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame > 1)
                    projectile.frame = 0;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 600);
        }

        private int HomeOnTarget()
        {
            const bool homingCanAimAtWetEnemies = true;
            const float homingMaximumRangeInPixels = 1000;

            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile) && (!n.wet || homingCanAimAtWetEnemies))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override void Kill(int timeleft)
        {
            Main.PlaySound(4, projectile.Center, 6);
            projectile.position = projectile.Center;
            projectile.width = projectile.height = 208;
            projectile.Center = projectile.position;
            for (int index1 = 0; index1 < 3; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Main.dust[index2].position = new Vector2((float)(projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
            }
            for (int index1 = 0; index1 < 10; ++index1)
            {
                int index2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0.0f, 0.0f, 0, new Color(), 2.5f);
                Main.dust[index2].position = new Vector2((float)(projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
                Main.dust[index2].noGravity = true;
                Dust dust1 = Main.dust[index2];
                dust1.velocity = dust1.velocity * 1f;
                int index3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 229, 0.0f, 0.0f, 100, new Color(), 1.5f);
                Main.dust[index3].position = new Vector2((float)(projectile.width / 2), 0.0f).RotatedBy(6.28318548202515 * Main.rand.NextDouble(), new Vector2()) * (float)Main.rand.NextDouble() + projectile.Center;
                Dust dust2 = Main.dust[index3];
                dust2.velocity = dust2.velocity * 1f;
                Main.dust[index3].noGravity = true;
            }
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}