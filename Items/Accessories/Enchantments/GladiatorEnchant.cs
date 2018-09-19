using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class GladiatorEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gladiator Enchantment");
            Tooltip.SetDefault(
                @"'Are you not entertained?'
Throwing weapons will speed up drastically over time
Summons a pet Minotaur");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).GladiatorEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GladiatorHelmet);
            recipe.AddIngredient(ItemID.GladiatorBreastplate);
            recipe.AddIngredient(ItemID.GladiatorLeggings);
            recipe.AddIngredient(ItemID.Javelin, 200);
            recipe.AddIngredient(ItemID.MarbleChest);
            recipe.AddIngredient(ItemID.TartarSauce);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}