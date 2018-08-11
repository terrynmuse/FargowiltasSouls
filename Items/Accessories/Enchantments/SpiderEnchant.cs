using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpiderEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spider Enchantment");
            Tooltip.SetDefault(
@"'Arachniphobia is punishable by arachnid induced death'
Summon damage may cause the enemy to be Swarmed
Summons a pet Spider");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.SpiderEnchant = true;
            modPlayer.AddPet("Spider Pet", BuffID.PetSpider, ProjectileID.Spider);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpiderMask);
            recipe.AddIngredient(ItemID.SpiderBreastplate);
            recipe.AddIngredient(ItemID.SpiderGreaves);
            recipe.AddIngredient(ItemID.SpiderStaff);
            recipe.AddIngredient(ItemID.QueenSpiderStaff);
            recipe.AddIngredient(ItemID.BatScepter);
            recipe.AddIngredient(ItemID.SpiderEgg);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}