using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class VoidSoul : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Void");
            Tooltip.SetDefault("The Mutant's Grab Bags have unlocked their true potential\n" +
                               "You respawn twice as fast\n");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).VoidSoul = true;
        }
    }
}
