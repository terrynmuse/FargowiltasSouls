using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PalladiumEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Enchantment");
            Tooltip.SetDefault(
                @"'You feel your wounds slowly healing' 
Greatly increases life regeneration after striking an enemy 
Small chance for an attack to gain 33% life steal");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).PalladiumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyPallaHead");
            recipe.AddIngredient(ItemID.PalladiumBreastplate);
            recipe.AddIngredient(ItemID.PalladiumLeggings);
            recipe.AddIngredient(ItemID.PalladiumSword);
            recipe.AddIngredient(ItemID.PalladiumRepeater);
            recipe.AddIngredient(ItemID.SoulDrain);
            recipe.AddIngredient(ItemID.VampireKnives);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}