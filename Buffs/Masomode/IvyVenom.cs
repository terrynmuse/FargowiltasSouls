using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class IvyVenom : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Ivy Venom");
            Description.SetDefault("Losing life, will become Infested at 20 seconds");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            player.buffTime[buffIndex] += time;
            return false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.buffTime[buffIndex] > 1200)
            {
                player.AddBuff(mod.BuffType("Infested"), player.buffTime[buffIndex]);
                player.buffTime[buffIndex] = 1;
                Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0);
            }
            player.venom = true;
        }
    }
}