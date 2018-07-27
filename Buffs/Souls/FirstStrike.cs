using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class FirstStrike : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("First Strike");
            Description.SetDefault("You have 20% damage reduction and hitting an enemy at full HP will always crit");
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.endurance += 0.20f;
            player.GetModPlayer<FargoPlayer>(mod).FirstStrike = true;
        }
    }
}