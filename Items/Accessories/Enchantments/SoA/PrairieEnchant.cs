using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class PrairieEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prairie Enchantment");
            Tooltip.SetDefault(
@"'Subdued Serenity'
40% increased thrown velocity
5% increased thrown and ranged damage");
            DisplayName.AddTranslation(GameCulture.Chinese, "草原魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'柔和宁静'
增加40%投掷物速度
增加5%投掷和远程伤害");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            player.thrownDamage += 0.05f;
            player.rangedDamage += 0.05f;
            player.thrownVelocity += 0.4f;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(soa.ItemType("PrairieHelmet"));
            recipe.AddIngredient(soa.ItemType("PrairieChest"));
            recipe.AddIngredient(soa.ItemType("PrairieLegs"));
            recipe.AddIngredient(soa.ItemType("AncientCharm"));
            recipe.AddIngredient(soa.ItemType("WoodJavelin"), 300);
            recipe.AddIngredient(ItemID.RottenEgg, 300);
            recipe.AddIngredient(soa.ItemType("GoldJavelin"), 300);
            recipe.AddIngredient(ItemID.EnchantedBoomerang);
            recipe.AddIngredient(ItemID.PoisonedKnife, 300);
            recipe.AddIngredient(ItemID.BallOHurt);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
