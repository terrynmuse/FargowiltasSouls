using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ApprenticeEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice Enchantment");
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
            recipe.AddIngredient(ItemID.);
            recipe.AddIngredient(ItemID.ApprenticeScarf);
            recipe.AddIngredient(ItemID.FlameStaff2);
            recipe.AddIngredient(ItemID.TomeofInfiniteWisdom);



            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                
            }
            else
            {
                
            }

            

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
