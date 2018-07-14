using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace FargowiltasSouls.Projectiles
{
	public class FargoBulletProj : ModProjectile
	{
		private int bounce = 10;
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fargo Bullet");     
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;    //The length of old position to be recorded
			ProjectileID.Sets.TrailingMode[projectile.type] = 0;        //The recording mode
		}

		public override void SetDefaults()
		{
			projectile.width = 12;
			projectile.height = 12;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.hostile = false;
			projectile.ranged = true;
			projectile.penetrate = -1; //same as luminite
			projectile.timeLeft = 300;          
			projectile.alpha = 255;    //The transparency of the projectile, 255 for completely transparent. (aiStyle 1 quickly fades the projectile in)
			projectile.light = 0.5f;  //How much light emit around the projectile
			projectile.ignoreWater = true;          
			projectile.tileCollide = true; //maybe..     
			projectile.extraUpdates = 1;           
			aiType = ProjectileID.Bullet; //Act exactly like default Bullet
			
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 2;
		}
		
		
		public override void AI()
		{
			//chloro
			// if (projectile.alpha < 170)
			// {
				// for (int i = 0; i < 10; i++)
				// {
					// float x2 = projectile.position.X - projectile.velocity.X / 10f * (float)i;
					// float y2 = projectile.position.Y - projectile.velocity.Y / 10f * (float)i;
					// int num164 = Dust.NewDust(new Vector2(x2, y2), 1, 1, 75, 0f, 0f, 0, default(Color), 1f);
					// Main.dust[num164].alpha = projectile.alpha;
					// Main.dust[num164].position.X = x2;
					// Main.dust[num164].position.Y = y2;
					// Main.dust[num164].velocity *= 0f;
					// Main.dust[num164].noGravity = true;
				// }
			// }
			
			//dust
			// int DustID = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
			// Main.dust[DustID].noGravity = true;
			// int DustID3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width, projectile.height + 5, 60, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
			// Main.dust[DustID3].noGravity = true;
			
			float num165 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
			float num166 = projectile.localAI[0];
			if (num166 == 0f)
			{
				projectile.localAI[0] = num165;
				num166 = num165;
			}
			if (projectile.alpha > 0)
			{
				projectile.alpha -= 25;
			}
			if (projectile.alpha < 0)
			{
				projectile.alpha = 0;
			}
			float num167 = projectile.position.X;
			float num168 = projectile.position.Y;
			float num169 = 300f;
			bool flag4 = false;
			int num170 = 0;
			if (projectile.ai[1] == 0f)
			{
				for (int num171 = 0; num171 < 200; num171++)
				{
					if (Main.npc[num171].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num171 + 1)))
					{
						float num172 = Main.npc[num171].position.X + (float)(Main.npc[num171].width / 2);
						float num173 = Main.npc[num171].position.Y + (float)(Main.npc[num171].height / 2);
						float num174 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num172) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num173);
						if (num174 < num169 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num171].position, Main.npc[num171].width, Main.npc[num171].height))
						{
							num169 = num174;
							num167 = num172;
							num168 = num173;
							flag4 = true;
							num170 = num171;
						}
					}
				}
				if (flag4)
				{
					projectile.ai[1] = (float)(num170 + 1);
				}
				flag4 = false;
			}
			if (projectile.ai[1] > 0f)
			{
				int num175 = (int)(projectile.ai[1] - 1f);
				if (Main.npc[num175].active && Main.npc[num175].CanBeChasedBy(projectile, true) && !Main.npc[num175].dontTakeDamage)
				{
					float num176 = Main.npc[num175].position.X + (float)(Main.npc[num175].width / 2);
					float num177 = Main.npc[num175].position.Y + (float)(Main.npc[num175].height / 2);
					float num178 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num176) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num177);
					if (num178 < 1000f)
					{
						flag4 = true;
						num167 = Main.npc[num175].position.X + (float)(Main.npc[num175].width / 2);
						num168 = Main.npc[num175].position.Y + (float)(Main.npc[num175].height / 2);
					}
				}
				else
				{
					projectile.ai[1] = 0f;
				}
			}
			if (!projectile.friendly)
			{
				flag4 = false;
			}
			if (flag4)
			{
				float num179 = num166;
				Vector2 vector19 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
				float num180 = num167 - vector19.X;
				float num181 = num168 - vector19.Y;
				float num182 = (float)Math.Sqrt((double)(num180 * num180 + num181 * num181));
				num182 = num179 / num182;
				num180 *= num182;
				num181 *= num182;
				int num183 = 8;
				projectile.velocity.X = (projectile.velocity.X * (float)(num183 - 1) + num180) / (float)num183;
				projectile.velocity.Y = (projectile.velocity.Y * (float)(num183 - 1) + num181) / (float)num183;
			}
		}
		
		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			OnHit();
			
			//meteor
			if (bounce > 1)
			{
				Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
				Main.PlaySound(SoundID.Item10, projectile.position);
				bounce--;
				if (projectile.velocity.X != oldVelocity.X)
				{
					projectile.velocity.X = -oldVelocity.X;
				}
				if (projectile.velocity.Y != oldVelocity.Y)
				{
					projectile.velocity.Y = -oldVelocity.Y;
				}
			}
			else
			{
				projectile.Kill();
			}
			
			return false;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			OnHit();
		
			//cursed
			target.AddBuff(39, 420, false);
			
			//ichor
			target.AddBuff(69, 600, false);
			
			//venom
			target.AddBuff(70, 600, false);
			
			//nano
			if (Main.rand.Next(3) == 0)
			{
				target.AddBuff(31, 180, false);
			}
			else
			{
				target.AddBuff(31, 60, false);
			}
			
			//golden
			target.AddBuff(72, 120, false);
			
        }
		
		public void OnHit() 
		{
			//crystal 
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 1, 1f, 0f);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 68, 0f, 0f, 0, default(Color), 1f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 1.5f;
				Main.dust[dust].scale *= 0.9f;
			}

			for (int num491 = 0; num491 < 3; num491++)
			{
				float num492 = -projectile.velocity.X * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				float num493 = -projectile.velocity.Y * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				Projectile.NewProjectile(projectile.position.X + num492, projectile.position.Y + num493, num492, num493, ProjectileID.CrystalBullet, (int)((double)projectile.damage), 0f, projectile.owner, 0f, 0f);
			}
			
			//explosion
			Main.PlaySound(SoundID.Item14, projectile.position);
			for (int i = 0; i < 7; i++)
			{
				Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			}
			for (int i = 0; i < 3; i++)
			{
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity *= 3f;
				dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
				Main.dust[dust].velocity *= 2f;
			}
			int gore = Gore.NewGore(new Vector2(projectile.position.X - 10f, projectile.position.Y - 10f), default(Vector2), Main.rand.Next(61, 64), 1f);
			Main.gore[gore].velocity *= 0.3f;
			Gore gore1 = Main.gore[gore];
			gore1.velocity.X = gore1.velocity.X + (float)Main.rand.Next(-10, 11) * 0.05f;
			Gore gore2 = Main.gore[gore];
			gore2.velocity.Y = gore2.velocity.Y + (float)Main.rand.Next(-10, 11) * 0.05f;
			if (projectile.owner == Main.myPlayer)
			{
				projectile.localAI[1] = -1f;
				projectile.maxPenetrate = 0;
				projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
				projectile.width = 80;
				projectile.height = 80;
				projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
				projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
				projectile.Damage();
			}
		}
		
		public override void Kill(int timeleft)
		{
			//venom dust
			Main.PlaySound(SoundID.Item10, projectile.position);
			for (int i = 0; i < 10; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 171, 0f, 0f, 100, default(Color), 1f);
				Main.dust[dust].scale = (float)Main.rand.Next(1, 10) * 0.1f;
				Main.dust[dust].noGravity = true;
				Main.dust[dust].fadeIn = 1.5f;
				Main.dust[dust].velocity *= 0.75f;
			}
			
			//party dust 
			for (int i = 0; i < 10; i++)
			{
				int rand = Main.rand.Next(139, 143);
				int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, rand, -projectile.velocity.X * 0.3f, -projectile.velocity.Y * 0.3f, 0, default(Color), 1.2f);
				Dust dust1 = Main.dust[dust];
				dust1.velocity.X = dust1.velocity.X + (float)Main.rand.Next(-50, 51) * 0.01f;
				Dust dust2 = Main.dust[dust];
				dust2.velocity.Y = dust2.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.01f;
				Dust dust3 = Main.dust[dust];
				dust3.velocity.X = dust3.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
				Dust dust4 = Main.dust[dust];
				dust4.velocity.Y = dust4.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
				Dust dust5 = Main.dust[dust];
				dust5.velocity.X = dust5.velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
				Dust dust6 = Main.dust[dust];
				dust6.velocity.Y = dust6.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
				Main.dust[dust].scale *= 1f + (float)Main.rand.Next(-30, 31) * 0.01f;
			}
			for (int i = 0; i < 5; i++)
			{
				int rand = Main.rand.Next(276, 283);
				int gore = Gore.NewGore(projectile.position, -projectile.velocity * 0.3f, rand, 1f);
				Gore gore1 = Main.gore[gore];
				gore1.velocity.X = gore1.velocity.X + (float)Main.rand.Next(-50, 51) * 0.01f;
				Gore gore2 = Main.gore[gore];
				gore2.velocity.Y = gore2.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.01f;
				Gore gore3 = Main.gore[gore];
				gore3.velocity.X = gore3.velocity.X * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
				Gore gore4 = Main.gore[gore];
				gore4.velocity.Y = gore4.velocity.Y * (1f + (float)Main.rand.Next(-50, 51) * 0.01f);
				Main.gore[gore].scale *= 1f + (float)Main.rand.Next(-20, 21) * 0.01f;
				Gore gore5 = Main.gore[gore];
				gore5.velocity.X = gore5.velocity.X + (float)Main.rand.Next(-50, 51) * 0.05f;
				Gore gore6 = Main.gore[gore];
				gore6.velocity.Y = gore6.velocity.Y + (float)Main.rand.Next(-50, 51) * 0.05f;
			}
			
		}
	}
}
