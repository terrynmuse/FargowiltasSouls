using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
    public class Nuke2 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Universal Collapse");
            Tooltip.SetDefault("Destroys the Universe");
            DisplayName.AddTranslation(GameCulture.Chinese, "宇宙坍缩");
            Tooltip.AddTranslation(GameCulture.Chinese, "毁灭宇宙");
        }

        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 32;
            item.maxStack = 99;
            item.consumable = true;
            item.useStyle = 1;
            item.expert = true;
            item.UseSound = SoundID.Item1;
            item.useAnimation = 20;
            item.useTime = 20;
            item.value = Item.buyPrice(0, 0, 3);
            item.noUseGraphic = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("NukeProj2");
            item.shootSpeed = 5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "Nuke", 999);
            recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}