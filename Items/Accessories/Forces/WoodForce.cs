using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WoodForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Wood");

            Tooltip.SetDefault(
@"''
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);



           
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            /*recipe.AddIngredient(null, "WoodEnchant");
            recipe.AddIngredient(null, "BorealWoodEnchant");
            recipe.AddIngredient(null, "RichMahoganyEnchant");
            recipe.AddIngredient(null, "EbonwoodEnchant");
            recipe.AddIngredient(null, "ShadewoodEnchant");
            recipe.AddIngredient(null, "PalmwoodEnchant");
            recipe.AddIngredient(null, "PearlwoodEnchant");*/

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}