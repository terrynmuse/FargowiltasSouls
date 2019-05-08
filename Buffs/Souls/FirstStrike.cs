using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class FirstStrike : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("First Strike");
            Description.SetDefault("Your next attack will be enhanced");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex]++;
            player.GetModPlayer<FargoPlayer>(mod).FirstStrike = true;
        }
    }
}