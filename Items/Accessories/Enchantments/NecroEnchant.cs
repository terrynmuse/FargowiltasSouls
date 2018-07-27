using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class NecroEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necro Enchantment");
            Tooltip.SetDefault(
@"'Welcome to the bone zone' 
25% chance to not consume ammo 
A Dungeon Guardian will aid you occasionally");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            player.ammoCost75 = true;
           
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NecroHelmet);
            recipe.AddIngredient(ItemID.NecroBreastplate);
            recipe.AddIngredient(ItemID.NecroGreaves);
            recipe.AddIngredient(ItemID.BoneSword);
            recipe.AddIngredient(ItemID.PhoenixBlaster);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}