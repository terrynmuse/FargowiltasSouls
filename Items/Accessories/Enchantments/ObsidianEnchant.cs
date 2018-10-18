using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ObsidianEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Enchantment");
            Tooltip.SetDefault(
                @"'The earth calls'
Grants immunity to fire blocks and lava
Increases armor penetration by 10
While standing in lava, you gain 10 more armor penetration, 10% attack speed, and your attacks ignite enemies");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).ObsidianEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ObsidianHelm);
            recipe.AddIngredient(ItemID.ObsidianShirt);
            recipe.AddIngredient(ItemID.ObsidianPants);
            recipe.AddIngredient(ItemID.ObsidianRose);
            recipe.AddIngredient(ItemID.LavaWaders);
            recipe.AddIngredient(ItemID.SharkToothNecklace); //hmmm reee
            
            /*
ObsidianScale
ObsidianButterfly
Obsidian armor thorium
Obsidian armor thorium
Obsidian armor thorium
            */
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
