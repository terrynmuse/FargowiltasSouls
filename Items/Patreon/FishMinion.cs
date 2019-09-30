using FargowiltasSouls.Projectiles.Minions;
using Terraria;
using Terraria.ID;

namespace FargowiltasSouls.Items.Patreon
{
    public class FishMinion : HoverShooter
    {
        int count = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Minion");
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 20;
            projectile.height = 16;
            Main.projFrames[projectile.type] = 4;
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
            
            Shoot = mod.ProjectileType("WaterStream");
            Inertia = 20f;
            ShootSpeed = 12f; // 
            ShootCool = 10;
            ViewDist = 25;
        }

        public override void CheckActive()
        {
            Player player = Main.player[projectile.owner];
            PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>(mod);
            if (player.dead) modPlayer.FishMinion = false;
            if (modPlayer.FishMinion) projectile.timeLeft = 2;
        }

        public override void CreateDust()
        {
            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.6f, 0.9f, 0.3f);
        }

        public override void SelectFrame()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame = (projectile.frame + 1) % 4;
            }
        }
    }
}