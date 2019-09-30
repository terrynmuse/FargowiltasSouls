using FargowiltasSouls.Projectiles;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;

namespace FargowiltasSouls.Items.Weapons
{
    public class TophatSquirrel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Top Hat Squirrel");
            Tooltip.SetDefault("'Who knew this squirrel had phenomenal cosmic power?'");
            DisplayName.AddTranslation(GameCulture.Chinese, "高顶礼帽松鼠");
            Tooltip.AddTranslation(GameCulture.Chinese, "'谁能知道,这只松鼠竟然有着非凡的宇宙力量呢?'");
        }

        public override void SetDefaults()
        {
            item.damage = 50;

            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.rare = 1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.consumable = true;

            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 3f;

            item.autoReuse = true;

            item.shoot = mod.ProjectileType("Squirrel1");
            item.shootSpeed = 8f;
        }

        public override bool UseItem(Player player)
        {
            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType<Squirrel1>(), 0, 0,
                Main.myPlayer);
            return true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Squirrel);
            recipe.AddIngredient(ItemID.TopHat);

            recipe.AddTile(TileID.CrystalBall);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}