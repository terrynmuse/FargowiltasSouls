using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MoltenEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Enchantment");
            Tooltip.SetDefault(
                @"'They shall know the fury of hell.' 
Nearby enemies are ignited
When you die, you violently explode dealing massive damage to surrounding enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).MoltenEffect(10);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MoltenHelmet);
            recipe.AddIngredient(ItemID.MoltenBreastplate);
            recipe.AddIngredient(ItemID.MoltenGreaves);
            recipe.AddIngredient(ItemID.FieryGreatsword);
            recipe.AddIngredient(ItemID.Sunfury);
            recipe.AddIngredient(ItemID.DemonsEye);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}