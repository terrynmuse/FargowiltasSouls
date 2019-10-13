using Terraria;
using Terraria.ID;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class BrainProj : HoverShooter
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brain Proj");
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 74;
            projectile.height = 70;
            Main.projFrames[projectile.type] = 11;
            projectile.friendly = true;
            Main.projPet[projectile.type] = true;
            projectile.minion = true;
            projectile.minionSlots = 2;
            projectile.penetrate = -1;
            projectile.timeLeft = 18000;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            ProjectileID.Sets.MinionSacrificable[projectile.type] = true;
            ProjectileID.Sets.Homing[projectile.type] = true;
            Inertia = 20f;
            Shoot = mod.ProjectileType("CreeperProj");
            ShootSpeed = 12f; // 
        }

        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            if (player.dead) modPlayer.BrainMinion = false;
            if (modPlayer.BrainMinion) projectile.timeLeft = 2;
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int) (projectile.Center.X / 16f), (int) (projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 8)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 11;
            }
        }
    }
}