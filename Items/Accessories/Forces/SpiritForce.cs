using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class SpiritForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Spirit");
            Tooltip.SetDefault("'The strength of your spirit amazes even the Mutant'\n" +
                                "Attacks have a chance to inflict shadow flame\n" +
                                "You are immune to all skeletons and knockback\n" +
                                "Double tap down to call an ancient storm to the cursor location\n" +
                                "Summons a shield that can reflect projectiles into enchanted swords \n" +
                                "You also summon enchanted swords to attack enemies\n" +
                                "Magic damage has a chance to spawn damaging orbs\n" +
                                "When you crit, you get a burst of healing orbs on hit instead\n" +
                                "On hit, you release a legion of scythes\n" +
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
            recipe.AddIngredient(null, "ShadowEnchant");
            recipe.AddIngredient(null, "NecroEnchant");
            recipe.AddIngredient(null, "ForbiddenEnchant");
            recipe.AddIngredient(null, "HallowEnchant");
            recipe.AddIngredient(null, "SpectreEnchant");
            recipe.AddIngredient(null, "SpookyEnchant");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


