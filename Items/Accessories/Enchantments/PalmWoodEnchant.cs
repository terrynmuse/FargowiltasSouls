using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PalmWoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palm Wood Enchantment");
            Tooltip.SetDefault(
@"'Alarmingly calm'
Double tap down to spawn a palm tree sentry that throws nuts at enemies
While in the Ocean or Desert, it attacks twice as fast");
            DisplayName.AddTranslation(GameCulture.Chinese, "棕榈木魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'出奇的平静'
双击'下'键生成一个向敌人投掷坚果的棕榈树哨兵
在海洋或沙漠中,它的攻击速度翻倍");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().PalmEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PalmWoodHelmet);
            recipe.AddIngredient(ItemID.PalmWoodBreastplate);
            recipe.AddIngredient(ItemID.PalmWoodGreaves);
            recipe.AddIngredient(ItemID.BreathingReed);
            recipe.AddIngredient(ItemID.Tuna);
            recipe.AddIngredient(ItemID.Seashell);
            recipe.AddIngredient(ItemID.LimeKelp);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
