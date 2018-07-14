using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
	public class HallowProj : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hallow Proj");
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 96;
			projectile.height = 96;
			projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.penetrate = -1; 
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			
		}
		
		public override void AI()
        {
			bool flag64 = projectile.type == mod.ProjectileType("HallowProj");
			
			Player player = Main.player[projectile.owner];
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

			if (player.dead)
			{
				modPlayer.hallowEnchant = false;
			}
			
			projectile.position.X = Main.player[projectile.owner].Center.X - (float)(projectile.width / 2);
			projectile.position.Y = Main.player[projectile.owner].Center.Y - (float)(projectile.height / 2); //+ Main.player[projectile.owner].gfxOffY - 60f;
			if (Main.player[projectile.owner].gravDir == -1f)
			{
				projectile.position.Y = projectile.position.Y + 120f;
				projectile.rotation = 3.14f;
			}
			else
			{
				projectile.rotation = 0f;
			}
			projectile.position.X = (float)((int)projectile.position.X);
			projectile.position.Y = (float)((int)projectile.position.Y);
			float num395 = (float)Main.mouseTextColor / 200f - 0.35f;
			num395 *= 0.2f;
			projectile.scale = num395 + 0.95f;
			
			/*if (projectile.localAI[0] == 0f)
        	{
        		int num501 = 25;
				for (int num502 = 0; num502 < num501; num502++) 
				{
					int num503 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 16f), projectile.width, projectile.height - 16, 244, 0f, 0f, 0, default(Color), 1f);
					Main.dust[num503].velocity *= 2f;
					Main.dust[num503].scale *= 1.15f;
				}
				projectile.localAI[0] += 1f;
        	}*/ //some dust sheet
			
			if (projectile.owner == Main.myPlayer)
			{
				if (projectile.ai[0] != 0f)
				{
					projectile.ai[0] -= 1f;
					return;
				}
				float num396 = projectile.position.X;
				float num397 = projectile.position.Y;
				float num398 = 700f;
				bool flag11 = false;
				for (int num399 = 0; num399 < 200; num399++)
				{
					if (Main.npc[num399].CanBeChasedBy(projectile, true))
					{
						float num400 = Main.npc[num399].position.X + (float)(Main.npc[num399].width / 2);
						float num401 = Main.npc[num399].position.Y + (float)(Main.npc[num399].height / 2);
						float num402 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num400) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num401);
						if (num402 < num398 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num399].position, Main.npc[num399].width, Main.npc[num399].height))
						{
							num398 = num402;
							num396 = num400;
							num397 = num401;
							flag11 = true;
						}
					}
				}
				if (flag11)
				{
					Vector2 vector29 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
					float num404 = num396 - vector29.X;
					float num405 = num397 - vector29.Y;
					float num406 = (float)Math.Sqrt((double)(num404 * num404 + num405 * num405));
					num406 = 10f / num406;
					num404 *= num406;
					num405 *= num406;
					Projectile.NewProjectile(projectile.Center.X - 4f, projectile.Center.Y, num404, num405, mod.ProjectileType("HallowSword"), 80/*dmg*/, 5, projectile.owner, 0f, 0f);
					projectile.ai[0] = 50f;
					return;
				}
			}
		}

	}
}