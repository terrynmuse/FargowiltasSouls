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
            recipe.AddRecipeGroup("Fargowiltas:AnyForge");
            recipe.AddRecipeGroup("Fargowiltas:AnyAnvil");
            recipe.AddIngredient(ItemID.AlchemyTable);
            recipe.AddIngredient(ItemID.Sawmill);
            recipe.AddIngredient(ItemID.CookingPot);
            recipe.AddIngredient(ItemID.Solidifier);
            recipe.AddIngredient(ItemID.DyeVat);
            recipe.AddIngredient(ItemID.TinkerersWorkshop);
            recipe.AddRecipeGroup("Fargowiltas:AnyBookcase");
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