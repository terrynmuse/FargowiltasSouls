using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Purified : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Purified");
            Description.SetDefault("You are cleansed");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //purges all other buffs. does NOT play nice with luiafk?
            player.GetModPlayer<FargoPlayer>(mod).Purified = true;
        }
    }
}