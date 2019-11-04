using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
    public class MutantStatue : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant Statue");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.rare = 1;
            item.useAnimation = 15;
            item.useTime = 15;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("MutantStatue");
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyButterfly");
            recipe.AddIngredient(ItemID.GoldDust, 500);
            recipe.AddTile(mod.TileType("GoldenDippingVatSheet"));
            recipe.SetResult(ItemID.GoldButterfly);
            recipe.AddRecipe();
        }*/
    }
}