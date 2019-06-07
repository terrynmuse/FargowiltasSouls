using FargowiltasSouls.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Oiled : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Oiled");
            Description.SetDefault("Taking more damage from being on fire");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().Oiled = true;
        }
    }
}