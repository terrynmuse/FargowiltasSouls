using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BorealWoodEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boreal Wood Enchantment");
            Tooltip.SetDefault(
@"''
Every 10th attack will be accompanied by a snowball
While in the Snow Biome, there are several snowballs instead");
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
            /*Boreal - shoot snowballs
            
            player.getModPlayer<FargoPlayer>().BorealEnchant = true;

            every 10 attacks
            
            CanUseItem

normally one snowball

if(player.snow)
{
	snowball.Split into 5 or something
}
*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BorealWoodHelmet);
            recipe.AddIngredient(ItemID.BorealWoodBreastplate);
            recipe.AddIngredient(ItemID.BorealWoodGreaves);
            recipe.AddIngredient(ItemID.Penguin);
            recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);

            recipe.AddIngredient(ItemID.Shiverthorn);

            /*
            SnowballCannon - launcher in essence now */

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
