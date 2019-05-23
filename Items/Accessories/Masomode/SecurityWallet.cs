using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SecurityWallet : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Security Wallet");
            Tooltip.SetDefault(@"'Use caution around other players'
Grants immunity to enemies that steal items or coins
Prevents you from reforging items with certain modifiers
Protected modifiers can be chosen in the toggles menu");
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
            player.GetModPlayer<FargoPlayer>().SecurityWallet = true;
        }
    }
}
