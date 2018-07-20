using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items
{
    public class Masochist : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Mutant's Gift");
            Tooltip.SetDefault("'Use this to turn on/off Masochist Mode'");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = true;
        }

        public override string Texture
        {
            get
            {
                return "FargowiltasSouls/Items/Placeholder";
            }
        }

        public override bool UseItem(Player player)
        {
            FargoWorld.MasochistMode = !FargoWorld.MasochistMode;

            if (FargoWorld.MasochistMode)
            {
                Main.NewText("Masochist Mode initiated!", 175, 75);
            }
            else
            {
                Main.NewText("Masochist Mode deactivated!", 175, 75);
            }

            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
        }
    }
}