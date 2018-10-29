using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PalladiumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Enchantment");
            Tooltip.SetDefault(
                @"'You feel your wounds slowly healing' 
Greatly increases life regeneration after striking an enemy 
Small chance for an attack to gain 33% life steal"); //actual CD and flat cap or some shit but more common chance
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
            player.GetModPlayer<FargoPlayer>(mod).PalladiumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyPallaHead");
            recipe.AddIngredient(ItemID.PalladiumBreastplate);
            recipe.AddIngredient(ItemID.PalladiumLeggings);
            recipe.AddIngredient(ItemID.PalladiumSword);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("PalladiumSub"));
                recipe.AddIngredient(thorium.ItemType("PalladiumStaff"));
                recipe.AddIngredient(thorium.ItemType("eeeLifeLeech")); //um WTF
                recipe.AddIngredient(thorium.ItemType("VampireScepter"));
            }
            else
            {
                recipe.AddIngredient(ItemID.PalladiumRepeater);
            }
            
            recipe.AddIngredient(ItemID.SoulDrain);
            recipe.AddIngredient(ItemID.VampireKnives);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
