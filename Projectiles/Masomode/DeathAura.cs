using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
	public class DeathAura : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Death Aura");
		}

		public override void SetDefaults()
		{
			projectile.width = 10;
			projectile.height = 10;
			projectile.penetrate = -1;
			projectile.timeLeft *= 5;
			projectile.hide = true;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;

			//projectile.friendly = false;
			//projectile.hostile = true;
		}

		public override void AI()
		{
			if(Main.player[projectile.owner].FindBuffIndex(mod.BuffType("LivingWasteland")) == -1 || !Main.player[projectile.owner].active)
			{
				projectile.hide = true;
				projectile.Kill();
				Main.player[projectile.owner].ownedProjectileCounts[mod.ProjectileType("DeathAura")]--;
			}
			else
			{
				projectile.timeLeft = 2;

				projectile.Center = Main.player[projectile.owner].Center;
			}
		}

		public override bool CanHitPlayer(Player target)
		{
			if (target.whoAmI != projectile.owner)
			{
				Vector2 distance = Vector2.Subtract(Main.player[projectile.owner].Center, target.Center);

				if (distance.Length() <= 250)
					target.AddBuff(mod.BuffType("Rotting"), 2);
			}

			return false;
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (target.active && !target.dontTakeDamage && (target.townNPC || target.friendly || target.lifeMax <= 100))
			{
				Vector2 distance = Vector2.Subtract(Main.player[projectile.owner].Center, target.Center);

				if (distance.Length() <= 250)
				{
					int rotID = mod.BuffType("Rotting");

					if (target.buffImmune[rotID])
						target.buffImmune[rotID] = false;
				
					target.AddBuff(rotID, 2);
				}
			}

			return false;
		}
	}
}
