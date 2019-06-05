using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class WoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wood Enchantment");
            Tooltip.SetDefault(
@"'Humble beginnings…'
Critters have massively increased defense
When critters die, they release their souls to aid you");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().WoodEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodHelmet);
            recipe.AddIngredient(ItemID.WoodBreastplate);
            recipe.AddIngredient(ItemID.WoodGreaves);
            recipe.AddIngredient(ItemID.LivingWoodWand);
            recipe.AddIngredient(ItemID.Bunny);
            recipe.AddIngredient(ItemID.Squirrel);
            recipe.AddIngredient(ItemID.Bird);        

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
