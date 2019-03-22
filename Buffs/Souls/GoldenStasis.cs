using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class GoldenStasis : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Golden Stasis");
            Description.SetDefault("You are immune to all damage, but cannot move");
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>(mod).GoldShell = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderBuff";

            return true;
        }
    }
}