using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class AdamantiteEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Enchantment");
            Tooltip.SetDefault(
@"'Who needs to aim?' 
25% chance for any weapon to shoot in a spread
Any secondary projectiles may also split");
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
            if (Soulcheck.GetValue("Splitting Projectiles"))
            {
                player.GetModPlayer<FargoPlayer>(mod).AdamantiteEnchant = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyAdamHead");
            recipe.AddIngredient(ItemID.AdamantiteBreastplate);
            recipe.AddIngredient(ItemID.AdamantiteLeggings);
            recipe.AddIngredient(ItemID.AdamantiteGlaive);
            recipe.AddIngredient(ItemID.DarkLance);
            recipe.AddIngredient(ItemID.Shotgun);
            recipe.AddIngredient(ItemID.VenomStaff);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}