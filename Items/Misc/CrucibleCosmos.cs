using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
    public class CrucibleCosmos : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crucible of the Cosmos");
            Tooltip.SetDefault("'It seems to be hiding magnificent power'\nFunctions as nearly every crafting station");
            DisplayName.AddTranslation(GameCulture.Chinese, "宇宙坩埚");
            Tooltip.AddTranslation(GameCulture.Chinese, "'它似乎隐藏着巨大的力量'\n包含几乎所有制作环境");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 14;
            item.maxStack = 99;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.consumable = true;
            item.createTile = mod.TileType("CrucibleCosmosSheet");
            item.expert = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HeavyWorkBench);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyForge");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyAnvil");
            recipe.AddIngredient(ItemID.AlchemyTable);
            recipe.AddIngredient(ItemID.Sawmill);
            recipe.AddIngredient(ItemID.CookingPot);
            recipe.AddIngredient(ItemID.Solidifier);
            recipe.AddIngredient(ItemID.DyeVat);
            recipe.AddIngredient(ItemID.TinkerersWorkshop);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyBookcase");
            recipe.AddIngredient(ItemID.CrystalBall);
            recipe.AddIngredient(ItemID.Autohammer);
            recipe.AddIngredient(ItemID.LunarCraftingStation);
            recipe.AddIngredient(ItemID.LunarBar, 25);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}