using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TitaniumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Enchantment");
            Tooltip.SetDefault(
                @"'Hit me with your best shot' 
Any damage you take while at full HP is reduced by 90%
Briefly become invulnerable after striking an enemy when below 50% HP
Increases all knockback"); //how about literally just shadow dodge ech
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
            player.GetModPlayer<FargoPlayer>(mod).TitaniumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyTitaHead");
            recipe.AddIngredient(ItemID.TitaniumBreastplate);
            recipe.AddIngredient(ItemID.TitaniumLeggings);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.TitaniumSword);
                recipe.AddIngredient(thorium.ItemType("TitaniumStaff"));
                recipe.AddIngredient(ItemID.SlapHand);
                recipe.AddIngredient(ItemID.Anchor);
                recipe.AddIngredient(thorium.ItemType("Saba"));
                recipe.AddIngredient(thorium.ItemType("IceAxe"));
                recipe.AddIngredient(ItemID.MonkStaffT1);  
            }
            else
            {
                recipe.AddIngredient(ItemID.TitaniumSword);
                recipe.AddIngredient(ItemID.SlapHand);
                recipe.AddIngredient(ItemID.Anchor);
                recipe.AddIngredient(ItemID.MonkStaffT1);
            }

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
