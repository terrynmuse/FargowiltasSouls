using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ObsidianEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Enchantment");
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
            item.rare = 8;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);


            //obsidian armor
            //obsdoan skull
            //lva charm?

            //drop lava, immune, chips fly off in direction

            recipe.AddIngredient(ItemID.ObsidianHelm);
            recipe.AddIngredient(ItemID.ObsidianChest);
            recipe.AddIngredient(ItemID.ObsidianPants);

            recipe.AddIngredient(ItemID.Yelets);
            recipe.AddIngredient(ItemID.Seedler);
            recipe.AddIngredient(ItemID.ButchersChainsaw);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

