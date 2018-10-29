using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PlatinumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Platinum Enchantment");
            Tooltip.SetDefault(
                @"'Its value is immeasurable'
10% chance for enemies to drop 3x loot
If the enemy has Midas, the chance and bonus is doubled");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).PlatinumEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumHelmet);
            recipe.AddIngredient(ItemID.PlatinumChainmail);
            recipe.AddIngredient(ItemID.PlatinumGreaves);
            recipe.AddIngredient(ItemID.PlatinumCrown);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("PlatinumAegis"));
                recipe.AddIngredient(ItemID.DiamondRing);
                recipe.AddIngredient(ItemID.DiamondStaff);
                recipe.AddIngredient(ItemID.WhitePhasesaber);
                recipe.AddIngredient(thorium.ItemType("DiamondButterfly"));
                recipe.AddIngredient(thorium.ItemType("AncientDrachma"));
            }
            else
            {
                recipe.AddIngredient(ItemID.DiamondRing);
                recipe.AddIngredient(ItemID.DiamondStaff);
                recipe.AddIngredient(ItemID.WhitePhasesaber);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
