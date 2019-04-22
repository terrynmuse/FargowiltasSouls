using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class EskimoEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eskimo Enchantment");
            Tooltip.SetDefault(
@"''
goes into frost enchant
You can walk on water and when you do, it freezes and creates spikes
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

            recipe.AddIngredient(ItemID.PinkEskimoHood);
            recipe.AddIngredient(ItemID.PinkEskimoCoat);
            recipe.AddIngredient(ItemID.PinkEskimoPants);
            recipe.AddIngredient(ItemID.FrostMinnow);
            recipe.AddIngredient(ItemID.AtlanticCod);
            recipe.AddIngredient(ItemID.MarshmallowonaStick);
            recipe.AddIngredient(ItemID.SnowmanCannon);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
