using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SandsofTime : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sands of Time");
            Tooltip.SetDefault(@"'Whatever you do, don't drop it'
Works in your inventory
Grants immunity to Mighty Wind and cactus damage
You respawn twice as fast when no boss is alive");
            DisplayName.AddTranslation(GameCulture.Chinese, "时之沙");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'无论你做什么,都不要丢下它'
放在物品栏中即可生效
免疫强风和仙人掌伤害
当没有Boss存活时,重生速度加倍");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateInventory(Player player)
        {
            player.buffImmune[BuffID.WindPushed] = true;

            //respawn faster ech
            player.GetModPlayer<FargoPlayer>().SandsofTime = true;
        }
    }
}
