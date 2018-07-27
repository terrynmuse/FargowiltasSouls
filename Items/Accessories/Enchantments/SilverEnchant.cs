using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SilverEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silver Enchantment");
            Tooltip.SetDefault(
@"''
Summons a sword familiar");
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
            modPlayer.SilverEnchant = true;
            modPlayer.AddMinion("Silver Sword Familiar", mod.ProjectileType("SilverSword"), 20, 0f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SilverHelmet);
            recipe.AddIngredient(ItemID.SilverChainmail);
            recipe.AddIngredient(ItemID.SilverGreaves);
            recipe.AddIngredient(ItemID.SilverBroadsword);
            recipe.AddIngredient(ItemID.SapphireStaff);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}