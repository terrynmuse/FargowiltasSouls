using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class WoodEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wood Enchantment");
            Tooltip.SetDefault(
@"''
Critters have massively increased defense
Certain critters will attack or debuff enemies
When critters die, they explode into blood
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
            /*
            squirrels throw acorns, scorpions poison/venom, birds shoot feathers, bunnys leap at enemies
            */
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodHelmet);
            recipe.AddIngredient(ItemID.WoodBreastplate);
            recipe.AddIngredient(ItemID.WoodGreaves);
            recipe.AddIngredient(ItemID.LivingWoodWand);
            recipe.AddIngredient(ItemID.Bunny);
            recipe.AddIngredient(ItemID.Squirrel);
            recipe.AddIngredient(ItemID.Bird);
            
            /*LeafWand
            daybloom 
            grasshopper
            
            fireblossom to obsidian when
            all painter paintings when
            all vanilla buteflie swhen*/
            

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
