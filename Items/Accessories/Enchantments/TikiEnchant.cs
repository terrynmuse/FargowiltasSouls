using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TikiEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tiki Enchantment");
            Tooltip.SetDefault(
@"'Aku Aku!'
Attacks will inflict a random debuff
Summons a Tiki Spirit");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.TikiEnchant = true;
            modPlayer.AddPet("Tiki Pet", hideVisual, BuffID.TikiSpirit, ProjectileID.TikiSpirit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TikiMask);
            recipe.AddIngredient(ItemID.TikiShirt);
            recipe.AddIngredient(ItemID.TikiPants);
            recipe.AddIngredient(ItemID.PygmyNecklace);
            recipe.AddIngredient(ItemID.PygmyStaff);
            recipe.AddIngredient(ItemID.Blowgun);
            recipe.AddIngredient(ItemID.TikiTotem);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}