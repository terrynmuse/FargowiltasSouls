using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class TacticalSkeletonBullet : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Shot");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.MeteorShot);
            aiType = ProjectileID.MeteorShot;
            projectile.friendly = false;
            projectile.ranged = false;
            projectile.hostile = true;
            projectile.penetrate = 3;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.penetrate > 1)
            {
                Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
                Main.PlaySound(SoundID.Item10, projectile.position);
                projectile.penetrate--;

                if (projectile.velocity.X != projectile.oldVelocity.X)
                    projectile.velocity.X = -projectile.oldVelocity.X;
                if (projectile.velocity.Y != projectile.oldVelocity.Y)
                    projectile.velocity.Y = -projectile.oldVelocity.Y;

                return false;
            }
            return true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 360);
            target.AddBuff(BuffID.Burning, 180);
        }
    }
}