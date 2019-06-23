using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class LihzahrdTreasureBox : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lihzahrd Treasure Box");
            Tooltip.SetDefault(@"'Too many booby traps to open'
Grants immunity to Burning and Fused
You erupt into spiky balls when injured
Press down in the air to fastfall
Fastfall will create a fiery eruption on impact after falling a certain distance");
            DisplayName.AddTranslation(GameCulture.Chinese, "神庙蜥蜴宝藏盒");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'陷阱太多,打不开'
免疫燃烧和导火线
受伤时爆发尖钉球
在空中按'下'键快速下落
在一定高度使用快速下落,会在撞击地面时产生猛烈的火焰喷发");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
            item.defense = 8;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Burning] = true;
            player.buffImmune[mod.BuffType("Fused")] = true;
            player.GetModPlayer<FargoPlayer>().LihzahrdTreasureBox = true;
        }
    }
}
