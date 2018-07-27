using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class TerrariaSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Terraria");
            Tooltip.SetDefault(
@"'Welcome back master'
All effects of material Forces, some upgraded");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 500000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.TerrariaSoul = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "CosmoForce");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}