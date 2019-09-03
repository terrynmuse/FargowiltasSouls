using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class BlightboneEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blightbone Enchantment");
            Tooltip.SetDefault(
@"'Your spooks and scares will send shivers down your enemies' spines'
Empowers certain bone-related weapons
Effects Dreadflame Emblem");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.blightEmpowerment = true;

            //dreadflame emblem
            modPlayer.dreadEmblem = true;
        }

        private readonly string[] items =
        {
            "BlightMask",
            "BlightChest",
            "BlightLegs",
            "DreadflameEmblem",
            "FeatherHairpin",
            "MoodSummon",
            "HarpyBoomerang",
            "BoneThrone",
            "PumpGlove",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddIngredient(soa.ItemType("Pumpnade"), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
