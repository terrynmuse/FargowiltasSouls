using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class OrichalcumEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Enchantment");
            Tooltip.SetDefault(
@"'Nature blesses you' 
Flower petals will cause extra damage to your target 
Attacks may spawn fireballs to rotate around you");
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
            player.GetModPlayer<FargoPlayer>(mod).OrichalcumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyOriHead");
            recipe.AddIngredient(ItemID.OrichalcumBreastplate);
            recipe.AddIngredient(ItemID.OrichalcumLeggings);
            recipe.AddIngredient(ItemID.OrichalcumWaraxe);
            recipe.AddIngredient(ItemID.FlowerofFire);
            recipe.AddIngredient(ItemID.FlowerofFrost);
            recipe.AddIngredient(ItemID.CursedFlames);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}