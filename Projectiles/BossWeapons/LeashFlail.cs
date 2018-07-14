using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class LeashFlail : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Leash Flail");
		}
        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.penetrate = -1; 
            projectile.melee = true; 
        }
		
		public override void AI()
		{
				if(projectile.timeLeft == 120)
				{
					projectile.ai[0] = 1f;
				}		
		
				if (Main.player[projectile.owner].dead)
				{
					projectile.Kill();
					return;
				}

				Main.player[projectile.owner].itemAnimation = 5;
				Main.player[projectile.owner].itemTime = 5;

				if (projectile.alpha == 0)
				{
					if (projectile.position.X + (float)(projectile.width / 2) > Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2))
					{
						Main.player[projectile.owner].ChangeDir(1);
					}
					else
					{
						Main.player[projectile.owner].ChangeDir(-1);
					}
				}
				Vector2 vector14 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num166 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector14.X;
				float num167 = Main.player[projectile.owner].position.Y + (float)(Main.player[projectile.owner].height / 2) - vector14.Y;
				float num168 = (float)Math.Sqrt((double)(num166 * num166 + num167 * num167));
				if (projectile.ai[0] == 0f)
				{
					if (num168 > 700f)
					{
						projectile.ai[0] = 1f;
					}
					else if (num168 > 500f)
					{
						projectile.ai[0] = 1f;
					}
					projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
					projectile.ai[1] += 1f;
					if (projectile.ai[1] > 5f)
					{
						projectile.alpha = 0;
					}
					if (projectile.ai[1] > 8f)
					{
						projectile.ai[1] = 8f;
					}
					if (projectile.ai[1] >= 10f)
					{
						projectile.ai[1] = 15f;
						projectile.velocity.Y = projectile.velocity.Y + 0.3f;
					}
					if (projectile.velocity.X < 0f)
					{
						projectile.spriteDirection = -1;
					}
					else
					{
						projectile.spriteDirection = 1;
					}
				}
				else if (projectile.ai[0] == 1f)
				{
					projectile.tileCollide = false;
					projectile.rotation = (float)Math.Atan2((double)num167, (double)num166) - 1.57f;
					float num169 = 30f;

					if (num168 < 50f)
					{
						projectile.Kill();
					}
					num168 = num169 / num168;
					num166 *= num168;
					num167 *= num168;
					projectile.velocity.X = num166;
					projectile.velocity.Y = num167;
					if (projectile.velocity.X < 0f)
					{
						projectile.spriteDirection = 1;
					}
					else
					{
						projectile.spriteDirection = -1;
					}
					
				}
				//Spew eyes
				if ((int)projectile.ai[1] % 8 == 0 && projectile.owner == Main.myPlayer && Main.rand.Next(50) == 0) //higher # means later on in the attack
				{
					Vector2 vector54 = Main.player[projectile.owner].Center - projectile.Center;
					Vector2 vector55 = vector54 * -1f;
					vector55.Normalize();
					vector55 *= (float)Main.rand.Next(45, 65) * 0.1f;
					vector55 = vector55.RotatedBy((Main.rand.NextDouble() - 0.5) * 1.5707963705062866, default(Vector2));
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector55.X, vector55.Y, mod.ProjectileType("EyeProjectile2"), projectile.damage, projectile.knockBack, projectile.owner, -10f, 0f);
					return;
				}
		}
		
		public override void OnHitNPC (NPC target, int damage, float knockback, bool crit)
		{
			projectile.ai[0] = 1f;
		}
		
		public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            width = 30;
            height = 30;
            return true;
        }
		
		public override bool OnTileCollide (Vector2 oldVelocity)
		{
			//projectile.tileCollide = false;
			//projectile.timeLeft = 20;
			projectile.ai[0] = 1f;
			return false;
		}
		
 
        // chain voodoo
        public override bool PreDraw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch, Color lightColor)
        {
			
            Texture2D texture = ModLoader.GetTexture("FargowiltasSouls/Projectiles/LeashFlailChain");
 
            Vector2 position = projectile.Center;
            Vector2 mountedCenter = Main.player[projectile.owner].MountedCenter;
            Microsoft.Xna.Framework.Rectangle? sourceRectangle = new Microsoft.Xna.Framework.Rectangle?();
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            float num1 = (float)texture.Height;
            Vector2 vector2_4 = mountedCenter - position;
            float rotation = (float)Math.Atan2((double)vector2_4.Y, (double)vector2_4.X) - 1.57f;
            bool flag = true;
            if (float.IsNaN(position.X) && float.IsNaN(position.Y))
                flag = false;
            if (float.IsNaN(vector2_4.X) && float.IsNaN(vector2_4.Y))
                flag = false;
            while (flag)
            {
                if ((double)vector2_4.Length() < (double)num1 + 1.0)
                {
                    flag = false;
                }
                else
                {
                    Vector2 vector2_1 = vector2_4;
                    vector2_1.Normalize();
                    position += vector2_1 * num1;
                    vector2_4 = mountedCenter - position;
                    Microsoft.Xna.Framework.Color color2 = Lighting.GetColor((int)position.X / 16, (int)((double)position.Y / 16.0));
                    color2 = projectile.GetAlpha(color2);
                    Main.spriteBatch.Draw(texture, position - Main.screenPosition, sourceRectangle, color2, rotation, origin, 1.35f, SpriteEffects.None, 0.0f);
                }
            }
 
            return true;
        }
    }
}