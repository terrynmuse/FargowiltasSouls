using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class StardustEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stardust Enchantment");
            Tooltip.SetDefault(
                @"'The power of the Stand is yours' 
Double tap down to direct your guardian
Press the Freeze Key to freeze time for 5 seconds
There is a 60 second cooldown for this effect, a sound effect plays when it's back");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).StardustEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StardustHelmet);
            recipe.AddIngredient(ItemID.StardustBreastplate);
            recipe.AddIngredient(ItemID.StardustLeggings);
            recipe.AddIngredient(ItemID.StardustPickaxe);
            recipe.AddIngredient(ItemID.StardustCellStaff);
            recipe.AddIngredient(ItemID.StardustDragonStaff);
            recipe.AddIngredient(ItemID.RainbowCrystalStaff);

            /*
StardustWings
ShadowOrbStaff
replace pick with Black Staff 
SpellbookTimeGate
             */

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
