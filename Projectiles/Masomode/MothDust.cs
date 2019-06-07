using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class MothDust : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/Explosion";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Moth Dust");
        }

        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = -1;
            projectile.hide = true;
            projectile.hostile = true;
            projectile.timeLeft = 180;
        }

        public override void AI()
        {
            projectile.velocity *= .98f;

            int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 70);
            Main.dust[d].noGravity = true;
            Main.dust[d].velocity *= 2.5f;

            Lighting.AddLight(projectile.position, .3f, .1f, .3f);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Darkness, Main.rand.Next(300, 600));
            target.AddBuff(BuffID.Blackout, Main.rand.Next(60, 300));
            target.AddBuff(BuffID.Obstructed, Main.rand.Next(30, 120));
            target.AddBuff(BuffID.Confused, Main.rand.Next(300, 600));
            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(120, 360));
            target.AddBuff(mod.BuffType("Unstable"), Main.rand.Next(30, 120));
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.velocity.X != oldVelocity.X)
                projectile.velocity.X = -oldVelocity.X;
            if (projectile.velocity.Y != oldVelocity.Y)
                projectile.velocity.Y = -oldVelocity.Y;
            return false;
        }
    }
}