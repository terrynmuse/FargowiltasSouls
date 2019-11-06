using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class TimsConcoction : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tim's Concoction");
            Tooltip.SetDefault(@"'Smells funny'
Certain enemies will drop potions when defeated");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (SoulConfig.Instance.GetValue("Tim's Concoction"))
                player.GetModPlayer<FargoPlayer>().TimsConcoction = true;
        }
    }
}
