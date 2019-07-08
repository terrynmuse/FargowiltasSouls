using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class PungentEyeball : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pungent Eyeball");
            Tooltip.SetDefault(@"'It's fermenting'
Grants immunity to Blackout and Obstructed
Increases your max number of minions by 2
Increases your max number of sentries by 2");
            DisplayName.AddTranslation(GameCulture.Chinese, "辛辣的眼球");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'它在发酵'
免疫致盲和阻塞
+2最大召唤栏
+2最大哨兵栏");
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
            player.buffImmune[BuffID.Blackout] = true;
            player.buffImmune[BuffID.Obstructed] = true;
            player.maxMinions += 2;
            player.maxTurrets += 2;
        }
    }
}
