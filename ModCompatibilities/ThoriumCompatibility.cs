using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.MeleeItems;
using ThoriumMod.Items.NPCItems;
using ThoriumMod.Items.Steel;
using ThoriumMod.Tiles;

namespace FargowiltasSouls.ModCompatibilities
{
    public sealed class ThoriumCompatibility : ModCompatibility
    {
        public ThoriumCompatibility(Mod callerMod) : base(callerMod, nameof(ThoriumMod))
        {
        }


        protected override void AddRecipes()
        {
            int 
                foldedMetal = ModContent.ItemType<FoldedMetal>(),
                arcaneArmorFabricator = ModContent.TileType<ArcaneArmorFabricator>();


            ModRecipe recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);
            
            recipe.SetResult(ModContent.ItemType<SteelArrow>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelAxe>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelBattleAxe>(), 10);
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelBlade>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelBow>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelChestplate>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelGreaves>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelHelmet>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelMallet>());
            recipe.AddRecipe();



            recipe = new ModRecipe(CallerMod);

            recipe.AddIngredient(foldedMetal);
            recipe.AddTile(arcaneArmorFabricator);

            recipe.SetResult(ModContent.ItemType<SteelPickaxe>());
            recipe.AddRecipe();
        }

        protected override void AddRecipeGroups()
        {
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " Combination Yoyo", ModContent.ItemType<Nocturnal>(), ModContent.ItemType<Sanguine>());
            RecipeGroup.RegisterGroup("FargowiltasSouls:AnyThoriumYoyo", group);
        }
    }
}