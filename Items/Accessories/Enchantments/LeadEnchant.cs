using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class LeadEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lead Enchantment");
            Tooltip.SetDefault(
@"''
Attacks inflict enemies with Lead Poisoning");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 0;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.LeadEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeadHelmet);
            recipe.AddIngredient(ItemID.LeadChainmail);
            recipe.AddIngredient(ItemID.LeadGreaves);
            recipe.AddIngredient(ItemID.LeadShortsword);
            recipe.AddIngredient(ItemID.LeadPickaxe);
            recipe.AddIngredient(ItemID.GrayPaint);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}