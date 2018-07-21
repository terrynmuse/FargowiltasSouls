using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class DualSaberProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spinning Fire Staff");
        }

        public override void SetDefaults()
        {
            projectile.width = 100;
            projectile.height = 100;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.melee = true;
        }

        public override void AI()
        {
            //-------------------------------------------------------------Sound-------------------------------------------------------
            projectile.soundDelay--;
            if (projectile.soundDelay <= 0)
            {
                Main.PlaySound(2, (int) projectile.Center.X, (int) projectile.Center.Y,
                    15);
                projectile.soundDelay = 45;
            }

            //-----------------------------------------------How the projectile works---------------------------------------------------------------------
            Player player = Main.player[projectile.owner];
            if (Main.myPlayer == projectile.owner)
            {
                if (!player.channel || player.noItems || player.CCed)
                {
                    projectile.Kill();
                }
            }

            Lighting.AddLight(projectile.Center, 1f, 0.6f, 0f);
            projectile.Center = player.MountedCenter;
            projectile.position.X +=
                player.width / 2 * player.direction; 
            projectile.spriteDirection = player.direction;
            projectile.rotation += 0.3f * player.direction; //this is the projectile rotation/spinning speed
            if (projectile.rotation > MathHelper.TwoPi)
            {
                projectile.rotation -= MathHelper.TwoPi;
            }
            else if (projectile.rotation < 0)
            {
                projectile.rotation += MathHelper.TwoPi;
            }

            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = projectile.rotation;
        }

        public override bool
            PreDraw(SpriteBatch spriteBatch,
                Color lightColor) //this make the projectile sprite rotate perfectaly around the player
        {
            Texture2D texture = Main.projectileTexture[projectile.type];
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, null, Color.White, projectile.rotation,
                // ReSharper disable twice PossibleLossOfFraction
                new Vector2(texture.Width / 2, texture.Height / 2), 1f,
                projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }

        /*public override bool Colliding (Rectangle projHitbox, Rectangle targetHitbox)
        {
            if(projHitbox == targetHitbox)
            {
                
            }
        }*/
    }
}