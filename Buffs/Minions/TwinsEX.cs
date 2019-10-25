using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Minions
{
    public class TwinsEX : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Twins EX");
            Description.SetDefault("The real Twins will fight for you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderBuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[mod.ProjectileType("OpticRetinazer")] > 0)
            {
                player.GetModPlayer<FargoPlayer>().TwinsEX = true;
                player.buffTime[buffIndex] = 2;
            }
        }
    }
}