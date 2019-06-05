using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class FakeHeart : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fake Heart");
        }

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.timeLeft = 300;
            projectile.hostile = true;
            projectile.aiStyle = -1;
            projectile.ignoreWater = true;
            cooldownSlot = 0;
        }

        public override void AI()
        {
            float gravity = .1f;
            float yMax = 7f;
            if (projectile.honeyWet)
            {
                gravity = .05f;
                yMax = 3f;
            }
            else if (projectile.wet)
            {
                gravity = .08f;
                yMax = 5f;
            }

            try
            {
                projectile.wet = Collision.WetCollision(projectile.position, projectile.width, projectile.height);
                if (Collision.honey)
                    projectile.honeyWet = true;
            }
            catch
            {
                projectile.active = false;
                return;
            }

            projectile.velocity.Y += gravity;
            if (projectile.velocity.Y > yMax)
                projectile.velocity.Y = yMax;
            projectile.velocity.X *= .95f;
            if (projectile.velocity.X < .1f && projectile.velocity.X > -.1f)
                projectile.velocity.X = 0f;

            float rand = Main.rand.Next(90, 111) * 0.01f * (Main.essScale * 0.5f);
            Lighting.AddLight(projectile.Center, 0.5f * rand, 0.1f * rand, 0.1f * rand);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            damage = projectile.damage;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.hurtCooldowns[0] = 0;
            projectile.timeLeft = 0;
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