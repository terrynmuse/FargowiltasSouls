using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TinEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tin Enchantment");
            Tooltip.SetDefault(
@"'Return of the Crit'
Sets your critical strike chance to 4%
Every crit will increase it by 4%
Getting hit drops your crit back down");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.TinEnchant = true;
            modPlayer.AllCritEquals(modPlayer.TinCrit);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TinHelmet);
            recipe.AddIngredient(ItemID.TinChainmail);
            recipe.AddIngredient(ItemID.TinGreaves);
            recipe.AddIngredient(ItemID.TinBow);
            recipe.AddIngredient(ItemID.TopazStaff);
            recipe.AddIngredient(ItemID.Daylight);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}