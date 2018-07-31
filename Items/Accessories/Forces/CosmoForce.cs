using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class CosmoForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Cosmos");
            Tooltip.SetDefault("'Been around since the Big Bang' \n" +
                                "20% increased damage\n" +
                                "Solar shield allows you to dash through enemies \n" +
                                "Sets your critical strike chance to 5% \n" +
                                "Every crit will increase it \n" +
                                "Getting hit drops your crit back down \n" +
                                "Hurting enemies has a chance to spawn buff boosters \n" +
                                "Double tap down to direct your guardian");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 300000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MeteorEnchant");
            recipe.AddIngredient(null, "SolarEnchant");
            recipe.AddIngredient(null, "VortexEnchant");
            recipe.AddIngredient(null, "NebulaEnchant");
            recipe.AddIngredient(null, "StardustEnchant");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


