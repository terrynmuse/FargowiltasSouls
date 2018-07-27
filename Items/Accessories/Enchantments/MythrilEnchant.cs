using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MythrilEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Enchantment");
            Tooltip.SetDefault(
@"'You feel the knowledge of your weapons seep into your mind'
30% increased weapon use speed");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.MythrilEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyMythrilHead");
            recipe.AddIngredient(ItemID.MythrilChainmail);
            recipe.AddIngredient(ItemID.MythrilGreaves);
            recipe.AddIngredient(ItemID.FetidBaghnakhs);
            recipe.AddIngredient(ItemID.Megashark);
            recipe.AddIngredient(ItemID.LaserRifle);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}