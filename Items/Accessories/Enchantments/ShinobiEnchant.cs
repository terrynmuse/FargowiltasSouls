using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShinobiEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shinobi Infiltrator Enchantment");
            Tooltip.SetDefault(
                @"'Hidden in the Wall'
Greatly enhances Lightning Aura effectiveness
Effects of the Master Ninja Gear
Dash into any walls, to teleport through them to the next opening
Summons a pet gato");
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
            player.GetModPlayer<FargoPlayer>(mod).ShinobiEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MonkAltHead);
            recipe.AddIngredient(ItemID.MonkAltShirt);
            recipe.AddIngredient(ItemID.MonkAltPants);
            recipe.AddIngredient(ItemID.MonkBelt);
            recipe.AddIngredient(ItemID.MasterNinjaGear);
            recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper);
            recipe.AddIngredient(ItemID.DD2PetGato);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}