using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class DreadfireEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dreadfire Enchantment");
            Tooltip.SetDefault(
@"'Ralnek's spirit guides you'
Pressing [Ability] will activate 'Dreadfire Aura'.
'Dreadfire Aura' increases minion damage greatly for a minute. 
3 minute cooldown
Effects of Pumpkin Amulet");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 70000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.DreadEffect = true;

            //pumpkin amulet
            modPlayer.pumpkinAmulet = true;
        }

        private readonly string[] items =
        {
            "PumpkinMask",
            "PumpkinArmor",
            "PumpkinBoots",
            "PumpkinAmulet",
            "VineSpear",
            "PumpkinCarver",
            "PumpkinFlare",
            "HarvestStaff",
            "MoodPainting",
            "TheFlamingBeanbag"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
