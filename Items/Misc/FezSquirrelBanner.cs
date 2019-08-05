using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
    public class FezSquirrelBanner : ModItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetDefaults()
        {
            item.width = 14;
            item.height = 34;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.rare = 1;
            item.value = Item.buyPrice(0, 0, 10, 0);
            item.createTile = mod.TileType("FMMBanner");
            item.placeStyle = 1;
            DisplayName.AddTranslation(GameCulture.Chinese, "菲斯帽松鼠旗帜");
        }
    }
}