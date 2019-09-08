using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class VulcanReaperEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vulcan Reaper Enchantment");
            Tooltip.SetDefault(
@"'Reap the rewards of your near-endless grind'
Provides immunity to Flarium Inferno and Obsidian Curse
Summons a dragon spirit");
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
            player.buffImmune[soa.BuffType("SerpentWrath")] = true;
            player.buffImmune[soa.BuffType("ObsidianCurse")] = true;

            //pet soon tm
        }

        private readonly string[] items =
        {
            "VulcanHelm",
            "VulcanChest",
            "VulcanLegs",
            "SmolderingSpicyCurry",
            "SerpentChain",
            "RuneOfHaldez",
            "Exterdaeviam",
            "CrimsonVeins",
            "CrimLash",
            "Warmth"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
