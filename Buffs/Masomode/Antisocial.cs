using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Antisocial : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Antisocial");
            Description.SetDefault("You have no friends");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //disables minions, disables pets, -50% minion dmg
            player.GetModPlayer<FargoPlayer>(mod).Asocial = true;
        }
    }
}