using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class GuttedHeart : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gutted Heart");
            Tooltip.SetDefault(@"'Once beating in the mind of a defeated foe'
Grants immunity to Bloodthirsty
10% increased max life
Creepers hover around you blocking some damage
A new Creeper appears every 15 seconds, and 5 can exist at once");
            DisplayName.AddTranslation(GameCulture.Chinese, "破碎的心");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'曾经还在敌人的脑中跳动着'
免疫嗜血
增加10%最大生命值
爬行者徘徊周围来阻挡伤害
每15秒生成一个新的爬行者,最多同时存在5个");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 3;
            item.value = Item.sellPrice(0, 2);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            player.statLifeMax2 += player.statLifeMax / 10;
            player.buffImmune[mod.BuffType("Bloodthirsty")] = true;
            fargoPlayer.GuttedHeart = true;
        }
    }
}
