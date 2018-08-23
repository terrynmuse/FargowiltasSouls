using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class NinjaEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Enchantment");
            Tooltip.SetDefault(
@"'Now you see me, now you don’t'
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
Summons a pet Black cat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).NinjaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NinjaHood);
            recipe.AddIngredient(ItemID.NinjaShirt);
            recipe.AddIngredient(ItemID.NinjaPants);
            recipe.AddIngredient(ItemID.Shuriken, 100);
            recipe.AddIngredient(ItemID.SmokeBomb, 50);
            recipe.AddIngredient(ItemID.UnluckyYarn);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}