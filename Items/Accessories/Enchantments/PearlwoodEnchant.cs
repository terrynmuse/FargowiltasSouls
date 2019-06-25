using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PearlwoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pearlwood Enchantment");
            Tooltip.SetDefault(
@"'Too little, too late…'
You leave behind a trail of rainbows that may shrink enemies
While in the Hallow, the rainbow trail lasts much longer");
            DisplayName.AddTranslation(GameCulture.Chinese, "珍珠木魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'为时已晚'
留下一道使敌人退缩的彩虹路径
在神圣地形中,彩虹路径持续时间变长");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().PearlEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PearlwoodHelmet);
            recipe.AddIngredient(ItemID.PearlwoodBreastplate);
            recipe.AddIngredient(ItemID.PearlwoodGreaves);
            recipe.AddIngredient(ItemID.UnicornonaStick);
            recipe.AddIngredient(ItemID.LightningBug);
            recipe.AddIngredient(ItemID.Prismite);
            recipe.AddIngredient(ItemID.TheLandofDeceivingLooks);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
