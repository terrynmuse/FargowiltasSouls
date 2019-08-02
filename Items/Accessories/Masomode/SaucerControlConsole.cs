using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SaucerControlConsole : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saucer Control Console");
            Tooltip.SetDefault(@"'Just keep it in airplane mode'
Grants immunity to Electrified
Summons a friendly Mini Saucer");
            DisplayName.AddTranslation(GameCulture.Chinese, "飞碟控制台");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'保持在飞行模式'
免疫带电
召唤一个友善的迷你飞碟");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Electrified] = true;
            if (Soulcheck.GetValue("Saucer Minion"))
                player.AddBuff(mod.BuffType("SaucerMinion"), 2);
        }
    }
}
