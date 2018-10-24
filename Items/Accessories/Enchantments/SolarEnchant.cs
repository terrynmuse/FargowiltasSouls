using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SolarEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Solar Enchantment");
            Tooltip.SetDefault(
                @"'Too hot to handle' 
Solar shield allows you to dash through enemies
Attacks may inflict the Solar Flare debuff
Melee attacks inflict it for less time (which is a good thing)");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).SolarEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SolarFlareHelmet);
            recipe.AddIngredient(ItemID.SolarFlareBreastplate);
            recipe.AddIngredient(ItemID.SolarFlareLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.SolarWings);
                recipe.AddIngredient(ItemID.HelFire);
                recipe.AddIngredient(thorium.ItemType("BlackBlade"));
                recipe.AddIngredient(thorium.ItemType("EruptingFlare"));
            }
            else
            {
                recipe.AddIngredient(ItemID.HelFire);
            }
            
            recipe.AddIngredient(ItemID.SolarEruption);
            recipe.AddIngredient(ItemID.DayBreak);
            recipe.AddIngredient(ItemID.StarWrath);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
