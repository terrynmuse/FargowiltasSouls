using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class ReverseManaFlow : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Reverse Mana Flow");
            Description.SetDefault("Your magic weapons cost life instead of mana");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //mana cost also damages
            player.GetModPlayer<FargoPlayer>(mod).ReverseManaFlow = true;
            player.magicDamage -= 0.9f;
        }
    }
}