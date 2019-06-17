using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class TribalCharm : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tribal Charm");
            Tooltip.SetDefault(@"'An idol of the ancient jungle dwellers'
Grants immunity to Webbed and Purified
Grants autofire to all weapons");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            player.GetModPlayer<FargoPlayer>().TribalCharm = true;
        }
    }
}
