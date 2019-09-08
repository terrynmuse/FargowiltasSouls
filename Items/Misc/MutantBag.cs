using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
    public class MutantBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Mutant's Grab Bag");
            Tooltip.SetDefault("Right click to open");
            DisplayName.AddTranslation(GameCulture.Chinese, "突变体的摸彩袋");
            Tooltip.AddTranslation(GameCulture.Chinese, "右键打开");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 24;
            item.height = 24;
            item.rare = 0;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            player.QuickSpawnItem(mod.ItemType("Sadism"), Main.rand.Next(5) + 5);

            if (!Fargowiltas.Instance.CalamityLoaded)
                player.QuickSpawnItem(mod.ItemType("MutantsFury"));

            player.QuickSpawnItem(ItemID.GoldCoin, Main.rand.Next(50, 60));
        }
    }
}
