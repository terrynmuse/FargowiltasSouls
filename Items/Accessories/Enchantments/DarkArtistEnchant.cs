using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class DarkArtistEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Artist Enchantment");
            Tooltip.SetDefault(
@"''
Greatly enhances Flameburst effectiveness");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.TurtleHelmet);
            recipe.AddIngredient(ItemID.TurtleScaleMail);
            recipe.AddIngredient(ItemID.TurtleLeggings);
            recipe.AddIngredient(ItemID.Yelets);
            recipe.AddIngredient(ItemID.Seedler);
            recipe.AddIngredient(ItemID.ButchersChainsaw);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

