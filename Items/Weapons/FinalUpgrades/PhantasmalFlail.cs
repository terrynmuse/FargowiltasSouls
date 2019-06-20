using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.FinalUpgrades
{
    public class PhantasmalFlail : ModItem
    {
        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Leash of Cthulhu");
            Tooltip.SetDefault("''");
            DisplayName.AddTranslation(GameCulture.Chinese, "幻影克苏鲁连枷");
            Tooltip.AddTranslation(GameCulture.Chinese, "''");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.width = 30;
            item.height = 10;
            item.value = Item.sellPrice(0, 10);
            item.rare = 1;
            item.noMelee = true;
            item.useStyle = 5;
            item.autoReuse = true;
            item.useAnimation = 25;
            item.useTime = 25;
            item.knockBack = 6f;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("PhantasmalFlail");
            item.shootSpeed = 25f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
        }

        public override void AddRecipes()
        {
            /*if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "EyeFlail");
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerEye"));
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }*/
        }
    }
}