using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MythrilEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Enchantment");
            Tooltip.SetDefault(
                @"'You feel the knowledge of your weapons seep into your mind'
30% increased weapon use speed");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).MythrilEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyMythrilHead");
            recipe.AddIngredient(ItemID.MythrilChainmail);
            recipe.AddIngredient(ItemID.MythrilGreaves);
            recipe.AddIngredient(ItemID.MythrilRepeater);
            
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("MythrilPelter"));
                recipe.AddIngredient(thorium.ItemType("MythrilStaff"));
                recipe.AddIngredient(thorium.ItemType("DrenchedDirk"));  
            }
            
            recipe.AddIngredient(ItemID.LaserRifle);
            recipe.AddIngredient(ItemID.FetidBaghnakhs);
            recipe.AddIngredient(ItemID.Megashark);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
