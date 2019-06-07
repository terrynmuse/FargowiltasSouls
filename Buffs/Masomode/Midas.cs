using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Midas : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Midas");
            Description.SetDefault("Drop money on hit");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>(mod).Midas = true;
        }
    }
}