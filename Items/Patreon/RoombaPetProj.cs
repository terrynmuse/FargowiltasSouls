using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Patreon
{
    public class RoombaPetProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 16;
            Main.projPet[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.Turtle);
            aiType = ProjectileID.Turtle;
        }

        public override bool PreAI()
        {
            Player player = Main.player[projectile.owner];
            player.turtle = false; // Relic from aiType
            return true;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            if (player.dead)
            {
                modPlayer.RoombaPet = false;
            }
            if (modPlayer.RoombaPet)
            {
                projectile.timeLeft = 2;
            }

            int num113 = Dust.NewDust(new Vector2(projectile.Center.X - projectile.direction * (projectile.width / 2), projectile.Center.Y + projectile.height / 2), projectile.width, 6, 76, 0f, 0f, 0, default(Color), 1f);
            Main.dust[num113].noGravity = true;
            Main.dust[num113].velocity *= 0.3f;
            Main.dust[num113].noLight = true;
        }
    }
}