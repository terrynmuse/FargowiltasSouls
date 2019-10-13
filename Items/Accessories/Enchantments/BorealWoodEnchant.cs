using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BorealWoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boreal Wood Enchantment");
            Tooltip.SetDefault(
@"'The cooler wood'
Attack will be periodically accompanied by a snowball
While in the Snow Biome, you shoot 5 snowballs instead");
            DisplayName.AddTranslation(GameCulture.Chinese, "针叶木魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'冷木'
每5次攻击附带着一个雪球
在冰雪地形时, 发射5个雪球");
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
            player.GetModPlayer<FargoPlayer>().BorealEnchant = true;
            player.GetModPlayer<FargoPlayer>().AdditionalAttacks = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWoodHelmet);
            recipe.AddIngredient(ItemID.BorealWoodBreastplate);
            recipe.AddIngredient(ItemID.BorealWoodGreaves);
            recipe.AddIngredient(ItemID.SnowballCannon);
            recipe.AddIngredient(ItemID.Penguin);
            recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
            recipe.AddIngredient(ItemID.Shiverthorn);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
