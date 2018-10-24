using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CactusEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Enchantment");
            Tooltip.SetDefault(
                @"'It's the quenchiest!' 
50% of contact damage is reflected
Getting hit by a projectile causes a needle spray
Needles scale with melee damage");
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
            player.GetModPlayer<FargoPlayer>(mod).CactusEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CactusHelmet);
            recipe.AddIngredient(ItemID.CactusBreastplate);
            recipe.AddIngredient(ItemID.CactusLeggings);
            recipe.AddIngredient(ItemID.CactusSword);
            recipe.AddIngredient(ItemID.CactusPickaxe);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.SandGun);
                recipe.AddIngredient(ItemID.ThornsPotion);
                recipe.AddIngredient(thorium.ItemType("CactusFruit"));
                recipe.AddIngredient(thorium.ItemType("PricklyJam"));
                recipe.AddIngredient(thorium.ItemType("SandyButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.PinkPricklyPear);
            }
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
