using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class LifeForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Life");
            Tooltip.SetDefault("'Rare is a living thing that dare disobey your will' \n" +
                                "25% increased defense \n" +
                                "100% of damage taken by melee attacks is reflected\n" +
                                "Greatly increases life regen \n" +
                                "Enemies drop hearts more often \n" +
                                "Enemies are more likely to target you \n" +
                                "Increases the strength of friendly bees \n" +
                                "Summon damage causes venom and has a chance to spawn additional bees and spiders\n" +
                                 "Beetles protect you from damage\n" +
                                "Summons several pets");
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
            recipe.AddIngredient(null, "CrimsonEnchant");
            recipe.AddIngredient(null, "BeeEnchant");
            recipe.AddIngredient(null, "SpiderEnchant");
            recipe.AddIngredient(null, "TurtleEnchant");
            recipe.AddIngredient(null, "BeetleEnchant");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}