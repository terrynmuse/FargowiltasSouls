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
            Main.buffNoTimeDisplay[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";

            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //all ranged weapons shoot confetti 
            player.GetModPlayer<FargoPlayer>(mod).ReverseManaFlow = true;
        }
    }
}