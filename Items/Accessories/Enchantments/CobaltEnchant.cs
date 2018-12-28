using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CobaltEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cobalt Enchantment");
            Tooltip.SetDefault(
@"'I can't believe it's not Palladium' 
25% chance for your projectiles to explode into shards");
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
            player.GetModPlayer<FargoPlayer>(mod).CobaltEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyCobaltHead");
            recipe.AddIngredient(ItemID.CobaltBreastplate);
            recipe.AddIngredient(ItemID.CobaltLeggings);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("CobaltPopper"));
                recipe.AddIngredient(thorium.ItemType("CobaltStaff"));
                recipe.AddIngredient(thorium.ItemType("CrystalPhaser"));
            }*/
            
            recipe.AddIngredient(ItemID.Chik);
            recipe.AddIngredient(ItemID.CrystalDart, 300);
            recipe.AddIngredient(ItemID.CrystalStorm);
            recipe.AddIngredient(ItemID.CrystalVileShard);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
