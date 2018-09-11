using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
	public class FishNuke : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Fish Nuke");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 24;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.ranged = true;
			projectile.penetrate = 1;
			projectile.timeLeft = 600;
			aiType = ProjectileID.Bullet;
		}

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            damage = target.lifeMax / 10;
            if (damage < 50)
            {
                damage = 50;
            }
        }
    }
}