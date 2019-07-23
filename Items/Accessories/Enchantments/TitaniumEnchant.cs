using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TitaniumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Titanium Enchantment");
            Tooltip.SetDefault(
@"'Hit me with your best shot' 
Briefly become invulnerable after striking an enemy");
            DisplayName.AddTranslation(GameCulture.Chinese, "钛金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'Hit Me With Your Best Shot(歌名)'
满血状态下所受到的伤害将减少90%
在攻击敌人后的瞬间无敌");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).TitaniumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyTitaHead");
            recipe.AddIngredient(ItemID.TitaniumBreastplate);
            recipe.AddIngredient(ItemID.TitaniumLeggings);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.Cutlass);
                recipe.AddIngredient(thorium.ItemType("TitaniumStaff"));
                recipe.AddIngredient(ItemID.SlapHand);
                recipe.AddIngredient(ItemID.Anchor);
                recipe.AddIngredient(thorium.ItemType("Saba"));
                recipe.AddIngredient(thorium.ItemType("IceAxe"));
                recipe.AddIngredient(ItemID.MonkStaffT1);  
            }
            else
            {
                recipe.AddIngredient(ItemID.Cutlass);
                recipe.AddIngredient(ItemID.SlapHand);
                recipe.AddIngredient(ItemID.Anchor);
                recipe.AddIngredient(ItemID.MonkStaffT1);
            }

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
