using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class OrichalcumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Enchantment");
            Tooltip.SetDefault(
@"'Nature blesses you' 
Flower petals will cause extra damage to your target 
Spawns 3 fireballs to rotate around you");
            DisplayName.AddTranslation(GameCulture.Chinese, "山铜魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'自然祝福着你'
花瓣对你的目标造成额外伤害
召唤3个环绕你的火球");
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
            player.GetModPlayer<FargoPlayer>(mod).OrichalcumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyOriHead");
            recipe.AddIngredient(ItemID.OrichalcumBreastplate);
            recipe.AddIngredient(ItemID.OrichalcumLeggings);
            recipe.AddIngredient(ItemID.OrichalcumWaraxe);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("OrichPelter"));
                recipe.AddIngredient(thorium.ItemType("OrichalcumStaff"));
                recipe.AddIngredient(ItemID.FlowerofFire);
                recipe.AddIngredient(ItemID.FlowerofFrost);
                recipe.AddIngredient(ItemID.CursedFlames);
                recipe.AddIngredient(thorium.ItemType("PrismaticSpray"));
            }
            else
            {
                recipe.AddIngredient(ItemID.FlowerofFire);
                recipe.AddIngredient(ItemID.FlowerofFrost);
                recipe.AddIngredient(ItemID.CursedFlames);
            }

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
