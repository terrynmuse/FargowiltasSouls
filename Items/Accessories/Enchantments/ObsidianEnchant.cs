using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ObsidianEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Enchantment");
            Tooltip.SetDefault(
                @"'The earth calls'
Grants immunity to fire blocks and lava
Increases armor penetration by 10
While standing in lava, you gain 10 more armor penetration, 10% attack speed, and your attacks ignite enemies");

//Enemies that directly attack you will be burned.
//Grants immunity to fire blocks.
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
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("aObsidianHelmet"));
                recipe.AddIngredient(thorium.ItemType("bObsidianChestGuard"));
                recipe.AddIngredient(thorium.ItemType("cObsidianGreaves"));
                recipe.AddIngredient(thorium.ItemType("ObsidianScale"));
                recipe.AddIngredient(ItemID.ObsidianRose);
                recipe.AddIngredient(ItemID.SharkToothNecklace);
                recipe.AddIngredient(thorium.ItemType("ObsidianButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.ObsidianRose);
                recipe.AddIngredient(ItemID.LavaWaders);
                recipe.AddIngredient(ItemID.SharkToothNecklace);
            }
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
