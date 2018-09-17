using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class VoidSoul : ModItem
    {
        public int Cd;
        public int VoidTimer = 600;

        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Void");
            Tooltip.SetDefault("'The depths of the Void have surrendered to your might' \n" +
                               "The Mutant's Grab Bags have unlocked their true potential\n" +
                               "You respawn twice as fast\n");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.VoidSoul = true;
        }
    }
}