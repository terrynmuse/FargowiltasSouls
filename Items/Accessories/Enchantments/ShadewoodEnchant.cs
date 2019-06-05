using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShadewoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadewood Enchantment");
            Tooltip.SetDefault(
@"'Surprisingly clean'
When you take damage, blood flies everywhere
While in the Crimson, you are instead inflicted with Super Bleeding on hit");
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
            player.GetModPlayer<FargoPlayer>().ShadeEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(ItemID.ShadewoodHelmet);
            recipe.AddIngredient(ItemID.ShadewoodBreastplate);
            recipe.AddIngredient(ItemID.ShadewoodGreaves);
            recipe.AddIngredient(ItemID.ShadewoodSword);
            recipe.AddIngredient(ItemID.CrimsonTigerfish);
            recipe.AddIngredient(ItemID.ViciousMushroom);
            recipe.AddIngredient(ItemID.DeadlandComesAlive);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
