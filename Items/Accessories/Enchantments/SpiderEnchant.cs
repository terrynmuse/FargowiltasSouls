using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpiderEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

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
            player.GetModPlayer<FargoPlayer>(mod).SpiderEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpiderMask);
            recipe.AddIngredient(ItemID.SpiderBreastplate);
            recipe.AddIngredient(ItemID.SpiderGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("VenomSubwoofer"));
                recipe.AddIngredient(thorium.ItemType("Webgun"));
                recipe.AddIngredient(ItemID.SpiderStaff);
                recipe.AddIngredient(ItemID.QueenSpiderStaff);
                recipe.AddIngredient(ItemID.BatScepter);
                recipe.AddIngredient(thorium.ItemType("ZereneButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.SpiderStaff);
                recipe.AddIngredient(ItemID.QueenSpiderStaff);
                recipe.AddIngredient(ItemID.BatScepter);
            }   
            
            recipe.AddIngredient(ItemID.SpiderEgg);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
