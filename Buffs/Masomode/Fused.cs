using Terraria;
using Terraria.DataStructures;
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

            if (player.buffTime[buffIndex] < 3)
            {
                Projectile.NewProjectile(player.position, player.velocity * 0, mod.ProjectileType("FusionBomb"), 0, 4f);

                int damage = (player.statLifeMax2 / 2) + Main.rand.Next(-player.statLifeMax2 / 8, player.statLifeMax2 / 4);

                player.Hurt(PlayerDeathReason.ByCustomReason(player.name + " was blown to bits."), damage, 0, false, false, true);
            }
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}