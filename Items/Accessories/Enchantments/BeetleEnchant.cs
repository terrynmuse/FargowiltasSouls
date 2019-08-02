using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BeetleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Enchantment");

            string tooltip =
@"'The unseen life of dung courses through your veins'
Beetles protect you from damage
Increases flight time by 50%";
            string tooltip_ch = 
@"'你的血管里流淌着看不见的粪便生命'
甲虫保护你免受伤害
增加100%飞行时间";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "甲虫魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //defense beetle bois
            modPlayer.BeetleEffect();
            modPlayer.wingTimeModifier += .5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHelmet);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyBeetle");
            recipe.AddIngredient(ItemID.BeetleLeggings);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "FlightEnchant");
            }

            recipe.AddIngredient(ItemID.BeetleWings);
            recipe.AddIngredient(ItemID.BeeWings);
            recipe.AddIngredient(ItemID.ButterflyWings);
            recipe.AddIngredient(ItemID.MothronWings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.GolemFist);
                recipe.AddIngredient(thorium.ItemType("SolScorchedSlab"));
            }
              
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
