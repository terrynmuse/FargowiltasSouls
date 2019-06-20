using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class RichMahoganyEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rich Mahogany Enchantment");
            Tooltip.SetDefault(
@"'Guaranteed to keep you hooked'
All grappling hooks pull you in and retract twice as fast
While in the Jungle, any hook will periodically fire homing shots at enemies");
            DisplayName.AddTranslation(GameCulture.Chinese, "红木魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'保证能勾住你'
所有抓钩速度翻倍
在丛林时,所有抓钩会定期向敌人发射追踪射击");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().MahoganyEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RichMahoganyHelmet);
            recipe.AddIngredient(ItemID.RichMahoganyBreastplate);
            recipe.AddIngredient(ItemID.RichMahoganyGreaves);
            recipe.AddIngredient(ItemID.IvyWhip);
            recipe.AddIngredient(ItemID.Frog);
            recipe.AddIngredient(ItemID.NeonTetra);
            recipe.AddIngredient(ItemID.DoNotStepontheGrass);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
