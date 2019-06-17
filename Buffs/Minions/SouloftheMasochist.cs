using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Minions
{
    public class SouloftheMasochist : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Soul of the Masochist");
            Description.SetDefault("The power of Masochist Mode is with you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderBuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();

                if (Soulcheck.GetValue("Skeletron Arms Minion"))
                {
                    fargoPlayer.SkeletronArms = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("SkeletronArmL")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("SkeletronArmL"), 0, 8f, player.whoAmI);
                    if (player.ownedProjectileCounts[mod.ProjectileType("SkeletronArmR")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("SkeletronArmR"), 0, 8f, player.whoAmI);
                }

                if (Soulcheck.GetValue("Pungent Eye Minion"))
                {
                    fargoPlayer.PungentEyeballMinion = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("PungentEyeball")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("PungentEyeball"), 0, 0f, player.whoAmI);
                }

                if (Soulcheck.GetValue("Rainbow Slime Minion"))
                {
                    fargoPlayer.RainbowSlime = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("RainbowSlime")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("RainbowSlime"), 0, 3f, player.whoAmI);
                }

                if (Soulcheck.GetValue("Probes Minion"))
                {
                    fargoPlayer.Probes = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("Probe1")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Probe1"), 0, 9f, player.whoAmI);
                    if (player.ownedProjectileCounts[mod.ProjectileType("Probe2")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("Probe2"), 0, 9f, player.whoAmI, 0f, -1f);
                }

                if (Soulcheck.GetValue("Plantera Minion"))
                {
                    fargoPlayer.MagicalBulb = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("PlanterasChild")] < 1)
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, -0.15f, -0.1f, mod.ProjectileType("PlanterasChild"), 0, 3f, player.whoAmI);
                }

                if (Soulcheck.GetValue("Flocko Minion"))
                {
                    fargoPlayer.SuperFlocko = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("SuperFlocko")] < 1)
                        Projectile.NewProjectile(player.Center, new Vector2(0f, -10f), mod.ProjectileType("SuperFlocko"), 0, 4f, player.whoAmI);
                }

                if (Soulcheck.GetValue("Saucer Minion"))
                {
                    fargoPlayer.MiniSaucer = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("MiniSaucer")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("MiniSaucer"), 0, 3f, player.whoAmI);
                }

                if (Soulcheck.GetValue("Cultist Minion"))
                {
                    fargoPlayer.LunarCultist = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("LunarCultist")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("LunarCultist"), 0, 2f, player.whoAmI, -1f);
                }

                if (Soulcheck.GetValue("True Eyes Minion"))
                {
                    fargoPlayer.TrueEyes = true;
                    if (player.ownedProjectileCounts[mod.ProjectileType("TrueEyeL")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("TrueEyeL"), 0, 3f, player.whoAmI, -1f);

                    if (player.ownedProjectileCounts[mod.ProjectileType("TrueEyeR")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("TrueEyeR"), 0, 3f, player.whoAmI, -1f);

                    if (player.ownedProjectileCounts[mod.ProjectileType("TrueEyeS")] < 1)
                        Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("TrueEyeS"), 0, 3f, player.whoAmI, -1f);
                }
            }
        }
    }
}