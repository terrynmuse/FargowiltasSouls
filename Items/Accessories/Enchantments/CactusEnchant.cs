using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CactusEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cactus Enchantment");
            Tooltip.SetDefault(
@"'It's the quenchiest!' 
25% of contact damage is reflected
Enemies may explode into needles on death
Needles scale with melee damage");
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
            player.GetModPlayer<FargoPlayer>(mod).CactusEffect();
            player.thorns = .25f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CactusHelmet);
            recipe.AddIngredient(ItemID.CactusBreastplate);
            recipe.AddIngredient(ItemID.CactusLeggings);
            recipe.AddIngredient(ItemID.CactusSword);
            recipe.AddIngredient(ItemID.Sandgun);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("CactusNeedle"), 300);
                recipe.AddIngredient(ItemID.ThornsPotion, 5);
                recipe.AddIngredient(thorium.ItemType("CactusFruit"), 5);
                recipe.AddIngredient(thorium.ItemType("PricklyJam"), 5);
            }
            else
            {
                recipe.AddIngredient(ItemID.PinkPricklyPear);
            }
            
            recipe.AddIngredient(ItemID.SecretoftheSands);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
