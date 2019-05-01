using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class WoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wood Enchantment");
            Tooltip.SetDefault(
@"''
Critters have massively increased defense
Certain critters will attack enemies");
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
            player.GetModPlayer<FargoPlayer>().WoodEnchant = true;
            /*
            squirrels throw acorns, scorpions poison/venom, birds shoot feathers, bunnys leap at enemies
          

GlobalNPC
Critters all have 999 defense (remove from pandroas box)

Squirrels (all variants, gold fires extremely quick) basically hve the masomode Shoot except target nearest enemy and fire acorns (shuriken AI)
Birds (all variants, gold drops massive gold eggs) drop explosive eggs as they fly and enemies are nearby
Bunnies (Gold inflicts several debuffs) have contact damage and dash at enemies, also explode into blood
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

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
