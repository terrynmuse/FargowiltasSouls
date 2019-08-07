using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SinisterIcon : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sinister Icon");
            Tooltip.SetDefault(@"'Most definitely not alive'
Prevents Masochist Mode-induced natural boss spawns
Increases spawn rate");
            DisplayName.AddTranslation(GameCulture.Chinese, "邪恶画像");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'肯定不是活着的'
阻止受虐模式导致的Boss自然生成
提高刷怪速率");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Soulcheck.GetValue("Sinister Icon"))
                player.GetModPlayer<FargoPlayer>().SinisterIcon = true;
        }
    }
}
