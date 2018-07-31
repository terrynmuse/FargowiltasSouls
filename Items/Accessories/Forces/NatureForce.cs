using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class NatureForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Nature");
            Tooltip.SetDefault("'Tapped into every secret of the wilds'\n" +
                                "Taking damage will release a spore explosion\n" +
                                "Provides immunity to lava and some debuffs\n" +
                                "Nearby enemies are ignited with vanity on\n" +
                                "Melee and ranged attacks cause frostburn and emit light\n" +
                                "All attacks inflict poison and venom\n" +
                                "Summons a modified leaf crystal to shoot at nearby enemies\n" +
                                "Not moving puts you in stealth\n" +
                                "Spores spawn on enemies when you attack in stealth mode\n" +
                                "You cheat death, returning with 20 HP\n" +
                                "5 minute cooldown");
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
            recipe.AddIngredient(null, "JungleEnchant");
            recipe.AddIngredient(null, "FossilEnchant");
            recipe.AddIngredient(null, "MoltenEnchant");
            recipe.AddIngredient(null, "FrostEnchant");
            recipe.AddIngredient(null, "ChlorophyteEnchant");
            recipe.AddIngredient(null, "ShroomiteEnchant");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


