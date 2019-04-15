using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class NullificationCurse : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Nullification Curse");
            Description.SetDefault("Moon Lord is only vulnerable to one damage type!");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = false;
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            longerExpertDebuff = false;
            canBeCleared = false;
        }
    }
}