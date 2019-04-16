using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class FirstStrike : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("First Strike");
            Description.SetDefault("You may dodge incoming attacks and your critical strike chance is doubled");
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.stealth = .5f; //idk if this works at all
            player.blackBelt = true;
            player.magicCrit *= 2;
            player.meleeCrit *= 2;
            player.rangedCrit *= 2;
            player.thrownCrit *= 2;

            player.GetModPlayer<FargoPlayer>(mod).FirstStrike = true;
        }
    }
}