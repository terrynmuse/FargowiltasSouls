using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class ConcentratedRainbowMatter : ModItem
    {        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Concentrated Rainbow Matter");
            Tooltip.SetDefault(@"'Taste the rainbow'
Grants immunity to Flames of the Universe
Summons a baby rainbow slime");
            DisplayName.AddTranslation(GameCulture.Chinese, "浓缩彩虹物质");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'品尝彩虹'
免疫宇宙之火
召唤一个彩虹史莱姆宝宝");
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
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            if (SoulConfig.Instance.GetValue("Rainbow Slime Minion"))
                player.AddBuff(mod.BuffType("RainbowSlime"), 2);
        }
    }
}
