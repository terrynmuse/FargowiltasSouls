using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BeetleEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Enchantment");
            Tooltip.SetDefault(
                @"'The unseen life of dung courses through your veins'
Beetles protect you from damage
Your wings last 1.5x as long");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).BeetleEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHelmet);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyBeetle");
            recipe.AddIngredient(ItemID.BeetleLeggings);
            recipe.AddIngredient(ItemID.BeetleWings);
            recipe.AddIngredient(ItemID.BeeWings);
            recipe.AddIngredient(ItemID.ButterflyWings);
            recipe.AddIngredient(ItemID.MothronWings);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}