using FargowiltasSouls.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Lovestruck : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lovestruck");
            Description.SetDefault("You are in love!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.loveStruck = true;
        }
    }
}