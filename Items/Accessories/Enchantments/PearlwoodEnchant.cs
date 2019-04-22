using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PearlwoodEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pearlwood Enchantment");
            Tooltip.SetDefault(
@"''
You leave behind a trail of rainbows that may shrink enemies
While in the Hallowed, the rainbow trail lasts much longer
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PearlwoodHelmet);
            recipe.AddIngredient(ItemID.PearlwoodBreastplate);
            recipe.AddIngredient(ItemID.PearlwoodGreaves);
            recipe.AddIngredient(ItemID.UnicornonaStick);
            recipe.AddIngredient(ItemID.LightningBug);
            recipe.AddIngredient(ItemID.Prismite);
            recipe.AddIngredient(ItemID.TheLandofDeceivingLooks);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
