using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MinerEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner Enchantment");
            Tooltip.SetDefault(
                @"'The planet trembles with each swing of your pick'
50% increased mining speed
Shows the location of enemies, traps, and treasures
You emit an aura of light
Summons a magic lantern");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).MinerEffect(hideVisual, .5f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddIngredient(ItemID.MiningShirt);
            recipe.AddIngredient(ItemID.MiningPants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("SandstonePickaxe"));
                recipe.AddRecipeGroup("FargowiltasSouls:AnyGoldPick");
                recipe.AddIngredient(ItemID.BonePickaxe);
                recipe.AddIngredient(thorium.ItemType("EnforcedThoriumPax"));
                recipe.AddIngredient(ItemID.MoltenPickaxe);
                recipe.AddIngredient(thorium.ItemType("BlinkrootButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.BonePickaxe);
                recipe.AddIngredient(ItemID.MoltenPickaxe);
            }
            
            recipe.AddIngredient(ItemID.MagicLantern);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
