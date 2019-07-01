using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class SilkEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silk Enchantment");
            Tooltip.SetDefault(
@"'You feel silky-smooth'
6% increased magic damage");
            DisplayName.AddTranslation(GameCulture.Chinese, "丝绸魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'丝般光滑'
增加6%魔法伤害");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 0;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            player.magicDamage += 0.06f;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("SilkCap"));
            recipe.AddIngredient(thorium.ItemType("SilkHat"));
            recipe.AddIngredient(thorium.ItemType("SilkTabard"));
            recipe.AddIngredient(thorium.ItemType("SilkLeggings"));
            recipe.AddIngredient(ItemID.WandofSparking);
            recipe.AddIngredient(thorium.ItemType("IceCube"));
            recipe.AddIngredient(thorium.ItemType("EighthPlagueStaff")); //nice diver, dam memer
            recipe.AddIngredient(thorium.ItemType("WindGust"));
            recipe.AddIngredient(thorium.ItemType("Cure"));
            recipe.AddIngredient(ItemID.UlyssesButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
