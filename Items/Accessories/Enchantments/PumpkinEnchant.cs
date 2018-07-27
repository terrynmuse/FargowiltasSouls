using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PumpkinEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin Enchantment");
            Tooltip.SetDefault(
@"''
You leave behind a trail of fire when you walk
Flames scale with magic damage
Eating Pumpkin Pie also heals you to full HP
Summons a pet squashling");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 0; 
            item.value = 20000; 
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.PumpkinEnchant = true;
            modPlayer.AddPet("Squashling Pet", BuffID.Squashling, ProjectileID.Squashling);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PumpkinHelmet);
            recipe.AddIngredient(ItemID.PumpkinBreastplate);
            recipe.AddIngredient(ItemID.PumpkinLeggings);
            recipe.AddIngredient(ItemID.MolotovCocktail, 50);
            recipe.AddIngredient(ItemID.JackOLantern);
            recipe.AddIngredient(ItemID.PumpkinSeed);
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}