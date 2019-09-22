using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SecurityWallet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Security Wallet");
            Tooltip.SetDefault(@"'Results not guaranteed in multiplayer'
Grants immunity to Midas and enemies that steal items
Prevents you from reforging items with certain modifiers
Protected modifiers can be chosen in the toggles menu");
            DisplayName.AddTranslation(GameCulture.Chinese, "安全钱包");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'无法保证在多人游戏中的效果'
免疫点金手和偷取物品的敌人
阻止你重铸带有特定词缀的物品
可以在灵魂开关菜单中选择受保护的词缀");
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
            player.buffImmune[mod.BuffType("Midas")] = true;
            player.GetModPlayer<FargoPlayer>().SecurityWallet = true;
        }
    }
}
