using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class MolluskEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mollusk Enchantment");
            Tooltip.SetDefault(
@"''
soon TM");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 9;//
            item.value = 1000000;//
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("MolluskShellmet"));
            recipe.AddIngredient(calamity.ItemType("MolluskShellplate"));
            recipe.AddIngredient(calamity.ItemType("Mollusk Shelleggings"));
            recipe.AddIngredient(calamity.ItemType("GiantPearl"));
            recipe.AddIngredient(calamity.ItemType("AmidiasPendant"));
            recipe.AddIngredient(calamity.ItemType("SeafoamBomb"));
            recipe.AddIngredient(calamity.ItemType("Riptide"));
            recipe.AddIngredient(calamity.ItemType("MagicalConch"));
            recipe.AddIngredient(calamity.ItemType("Waywasher"));
            recipe.AddIngredient(calamity.ItemType("AmidiasTrident"));
            recipe.AddIngredient(calamity.ItemType("EutrophicShank"));
            recipe.AddIngredient(calamity.ItemType("Serpentine"));
            recipe.AddIngredient(calamity.ItemType("ClamCrusher"));
            recipe.AddIngredient(calamity.ItemType("ClamorRifle"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
