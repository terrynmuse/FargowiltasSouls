using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Recovering : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Recovering");
            Description.SetDefault("The Nurse cannot heal you again yet");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }
    }
}