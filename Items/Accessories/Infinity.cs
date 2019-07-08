using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using static Terraria.ID.ItemID;


namespace FargowiltasSouls.Items.Accessories
{
    public class Infinity : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infinity Relic");
            Tooltip.SetDefault(
@"'Is it really worth it?
You consume no ammo, mana, or consumables
Every few attacks damage you slightly");
            DisplayName.AddTranslation(GameCulture.Chinese, "无尽遗物");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'这真的值得吗'
不消耗弹药,魔力和消耗品
每几次攻击都会对你造成轻微的伤害");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.AddBuff(mod.BuffType("InfinityDebuff"), 7200, false);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(Gel, 999);
            recipe.AddIngredient(Seed, 999);
            recipe.AddIngredient(Flare, 999);
            recipe.AddIngredient(Snowball, 999);
            recipe.AddIngredient(Bone, 999);
            recipe.AddIngredient(PoisonDart, 999);
            recipe.AddIngredient(CursedDart, 999);
            recipe.AddIngredient(IchorDart, 999);
            recipe.AddIngredient(RocketIII, 999);
            recipe.AddIngredient(Nail, 999);
            recipe.AddIngredient(StyngerBolt, 999);
            recipe.AddIngredient(CandyCorn, 999);
            recipe.AddIngredient(Stake, 999);
            recipe.AddIngredient(ManaCrystal, 100);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}