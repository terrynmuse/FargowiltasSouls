using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class FlariumEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flarium Enchantment");
            Tooltip.SetDefault(
@"'Raze your enemies with Araghur's flames'
Taking fatal damage will revive you with 1/3rd of your max health
You will have one minute of increased strength after rebirth
5 minute cooldown
Summons Chibi Araghur and the Sigil of Solace");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 350000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.DragonSetEffect = true;

            //pets soon tm
        }

        private readonly string[] items =
        {
            "FlariumChest",
            "FlariumLeggings",
            "FlariumStaff",
            "FlariumRocketLauncher",
            "FlariumJavelin",
            "SolusKatana",
            "SunSigil",
            "FlariumEarring",
            "SerpentSceptre",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyFlariumHelmet");

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
