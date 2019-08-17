using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.MutantBoss
{
    public class MutantFang : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Mutant Fang");
            Description.SetDefault("You cannot heal at all");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
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
            player.GetModPlayer<FargoPlayer>(mod).MutantNibble = true;
            player.potionDelay = player.buffTime[buffIndex];
        }
    }
}