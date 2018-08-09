using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MeteorEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Meteor Enchantment");
            Tooltip.SetDefault(
@"'Cosmic power builds your magical prowess'
A meteor shower initiates every few seconds while using magic weapons");
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
            player.GetModPlayer<FargoPlayer>(mod).MeteorEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MeteorHelmet);
            recipe.AddIngredient(ItemID.MeteorSuit);
            recipe.AddIngredient(ItemID.MeteorLeggings);
            recipe.AddIngredient(ItemID.SpaceGun);
            recipe.AddIngredient(ItemID.MeteorStaff);
            recipe.AddIngredient(ItemID.StarCannon);
            recipe.AddIngredient(ItemID.PlaceAbovetheClouds);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}