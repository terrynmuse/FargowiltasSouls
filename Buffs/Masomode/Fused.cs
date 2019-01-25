using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Fused : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fused");
            Description.SetDefault("You're going out with a bang");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().Fused = true;

            if (player.buffTime[buffIndex] < 3) Projectile.NewProjectile(player.position, player.velocity * 0, mod.ProjectileType("FusionBomb"), 150, 4f);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}