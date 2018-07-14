using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
	public class PrimeLaser : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Prime Laser");
		}
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 62;
			projectile.height = 40;
			projectile.friendly = true;
			Main.projPet[projectile.type] = true;
			projectile.minion = true;
			projectile.minionSlots = 1;
			projectile.penetrate = -1; 
			projectile.timeLeft = 18000;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
			ProjectileID.Sets.Homing[projectile.type] = true;
			projectile.aiStyle = 66; // mini retinazer

		}

		public override void AI()
		{	
			Player player = Main.player[projectile.owner];
			/*PumpkingPlayer modPlayer = player.GetModPlayer<PumpkingPlayer>(mod);
			if (player.dead)
			{
				modPlayer.Mecharaven = false;
			}
			if (modPlayer.Mecharaven)
			{
				projectile.timeLeft = 2;
			}*/
			float num768 = 400f;
			Vector2 vector58 = projectile.position;
			bool flag31 = false;
			for (int num651 = 0; num651 < 200; num651++)
			{
				NPC nPC2 = Main.npc[num651];
				if (nPC2.CanBeChasedBy(projectile, false))
				{
					float num652 = Vector2.Distance(nPC2.Center, projectile.Center);
					if (((Vector2.Distance(projectile.Center, vector58) > num652 && num652 < num768) || !flag31) && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, nPC2.position, nPC2.width, nPC2.height))
					{
						num768 = num652;
						vector58 = nPC2.Center;
						flag31 = true;
					}
				}
			}
			if (flag31)
			{
				projectile.rotation = (vector58 - projectile.position).ToRotation() + 3.14159274f;
			}
			else
			{
				projectile.rotation = projectile.velocity.ToRotation() + 3.14159274f;
			}
			if (projectile.ai[1] > 0f)
			{
				projectile.ai[1] += 1f;
			}
			if (projectile.ai[1] > 20f)
			{
				projectile.ai[1] = 0f;
				projectile.netUpdate = true;
			}
			if (projectile.ai[0] == 0f)
			{
				float scaleFactor6 = 50f;
				if (flag31 && projectile.ai[1] == 0f)
				{
					projectile.ai[1] += 1f;
					if (Main.myPlayer == projectile.owner)
					{
						Vector2 value16 = vector58 - projectile.Center;
						value16.Normalize();
						value16 *= scaleFactor6;
						int num777 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, value16.X, value16.Y, 389/*laser*/, projectile.damage, 0f, projectile.owner, 0f, 0f);
						projectile.netUpdate = true;
					}
				}
			}
		}
	}
}