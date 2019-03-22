using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PumpkinEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin Enchantment");
            Tooltip.SetDefault(
@"'Your sudden pumpkin craving will never be satisfied'
You leave behind a trail of fire when you walk
Flames scale with magic damage
Eating Pumpkin Pie also heals you to full HP
Summons a pet Squashling");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).PumpkinEffect(12, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PumpkinHelmet);
            recipe.AddIngredient(ItemID.PumpkinBreastplate);
            recipe.AddIngredient(ItemID.PumpkinLeggings);
            recipe.AddIngredient(ItemID.MolotovCocktail, 50);
            recipe.AddIngredient(ItemID.Sickle);
            recipe.AddIngredient(ItemID.BladedGlove);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("BentZombieArm"));
                recipe.AddIngredient(ItemID.PumpkinPie);
                recipe.AddIngredient(ItemID.ThroughtheWindow);
            }
            
            recipe.AddIngredient(ItemID.MagicalPumpkinSeed);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
