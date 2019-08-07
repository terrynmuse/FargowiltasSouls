using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WoodForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Wood");

            Tooltip.SetDefault(
@"'Extremely rigid'
Critters have massively increased defense
Killing critters no longer inflicts Guilty
When critters die, they release their souls to aid you
Every 5th attack will be accompanied by several snowballs
All grappling hooks pull you in and retract twice as fast
Any hook will periodically fire homing shots at enemies
You have a large aura of Shadowflame
When you take damage, you are inflicted with Super Bleeding
Double tap down to spawn a palm tree sentry that throws nuts at enemies
You leave behind a trail of rainbows that may shrink enemies");
            DisplayName.AddTranslation(GameCulture.Chinese, "森林之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'很刚'
大幅增加动物防御力
杀死动物不会再获得内疚Debuff
动物死后,释放它们的灵魂来帮助你
每5次攻击附带着数个雪球
所有抓钩速度翻倍
所有抓钩会定期向敌人发射追踪射击
周围环绕巨大暗影烈焰光环
受伤时,对敌人造成大出血
留下一道可以让敌人退缩的彩虹路径");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.WoodForce = true;
            //wood
            modPlayer.WoodEnchant = true;
            //boreal
            modPlayer.BorealEnchant = true;
            //mahogany
            modPlayer.MahoganyEnchant = true;

            //ebon
            if (!modPlayer.TerrariaSoul)
                modPlayer.EbonEffect();

            //shade
            modPlayer.ShadeEnchant = true;
            //palm
            modPlayer.PalmEffect();
            //pearl
            modPlayer.PearlEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "WoodEnchant");
            recipe.AddIngredient(null, "BorealWoodEnchant");
            recipe.AddIngredient(null, "RichMahoganyEnchant");
            recipe.AddIngredient(null, "EbonwoodEnchant");
            recipe.AddIngredient(null, "ShadewoodEnchant");
            recipe.AddIngredient(null, "PalmWoodEnchant");
            recipe.AddIngredient(null, "PearlwoodEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}