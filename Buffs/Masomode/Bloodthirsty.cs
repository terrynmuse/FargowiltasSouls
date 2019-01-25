using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Bloodthirsty : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Bloodthirsty");
            Description.SetDefault("Hugely increased enemy spawn rate");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //crazy spawn rate
            FargoWorld.Bloodthirsty = true;
        }
    }
}