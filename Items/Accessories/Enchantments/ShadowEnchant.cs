using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShadowEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Enchantment");
            Tooltip.SetDefault(
@"'You feel your body slip into the deepest of shadows'
Your attacks may inflict Darkness on enemies
Darkened enemies occasionally fire shadowflame tentacles at other enemies
Summons a pet Eater of Souls and Shadow Orb");
            DisplayName.AddTranslation(GameCulture.Chinese, "暗影魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你感觉身体陷入了最深的阴影中'
攻击概率造成黑暗
陷入黑暗的敌人偶尔会向其他敌人发射暗影烈焰触手
召唤一只噬魂者宝宝和阴影珍珠");
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
            player.GetModPlayer<FargoPlayer>().ShadowEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowHelmet);
            recipe.AddIngredient(ItemID.ShadowScalemail);
            recipe.AddIngredient(ItemID.ShadowGreaves);
            recipe.AddIngredient(ItemID.WarAxeoftheNight);
            recipe.AddIngredient(ItemID.PurpleClubberfish);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.BallOHurt);
                recipe.AddIngredient(thorium.ItemType("DemoniteTomahawk"), 300);
            }
            
            recipe.AddIngredient(ItemID.EatersBone);
            recipe.AddIngredient(ItemID.ShadowOrb);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
