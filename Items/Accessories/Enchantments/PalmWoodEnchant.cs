using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PalmWoodEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palm Wood Enchantment");
            Tooltip.SetDefault(
@"''
Doublte tap down to spawn a literal palm tree that throws coconuts at enemies
While in the Ocean or Desert, it attacks much faster
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

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.);

            Trident?
            Breathing Reed
            Black Ink
            Purple Mucos
            Lime Kelp
            Seashell
            Starfish
            Tuna
            Shrimp
            Red Snapper
            Palm Wood Sword/Bow

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
