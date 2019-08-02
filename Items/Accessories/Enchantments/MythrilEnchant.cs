using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MythrilEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythril Enchantment");
            Tooltip.SetDefault(
@"'You feel the knowledge of your weapons seep into your mind'
20% increased weapon use speed");
            DisplayName.AddTranslation(GameCulture.Chinese, "秘银魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"你感觉你对武器的知识渗透到脑海中'
增加25%武器使用速度");
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
            player.GetModPlayer<FargoPlayer>(mod).AttackSpeed *= 1.2f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyMythrilHead");
            recipe.AddIngredient(ItemID.MythrilChainmail);
            recipe.AddIngredient(ItemID.MythrilGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("MythrilStaff"));
                recipe.AddIngredient(ItemID.LaserRifle);
                recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
                recipe.AddIngredient(thorium.ItemType("BulletStorm"));
                recipe.AddIngredient(ItemID.Gatligator);
                recipe.AddIngredient(ItemID.Megashark);
                recipe.AddIngredient(thorium.ItemType("Trigun"));  
            }
            else
            {
                recipe.AddIngredient(ItemID.LaserRifle);
                recipe.AddIngredient(ItemID.ClockworkAssaultRifle);
                recipe.AddIngredient(ItemID.Gatligator);
                recipe.AddIngredient(ItemID.OnyxBlaster);
            }
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
