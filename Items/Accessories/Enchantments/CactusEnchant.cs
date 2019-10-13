using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
Enemies may explode into needles on death");
            DisplayName.AddTranslation(GameCulture.Chinese, "仙人掌魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'太解渴了!'
反射25%接触伤害
敌人在死亡时可能会爆出刺");
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
            player.GetModPlayer<FargoPlayer>().CactusEffect();
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
