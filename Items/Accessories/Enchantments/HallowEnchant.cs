using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class HallowEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hallowed Enchantment");
            Tooltip.SetDefault(
                @"'Hallowed be your sword and shield'
You gain a shield that can reflect projectiles
Summons an Enchanted Sword familiar that scales with minion damage
Summons a magical fairy");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 180000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).HallowEffect(hideVisual, 80);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyHallowHead");
            recipe.AddIngredient(ItemID.HallowedPlateMail);
            recipe.AddIngredient(ItemID.HallowedGreaves);
            recipe.AddIngredient(ItemID.Excalibur);
            recipe.AddIngredient(null, "SilverEnchant");
            recipe.AddIngredient(ItemID.TheLandofDeceivingLooks);
            recipe.AddIngredient(ItemID.FairyBell);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}