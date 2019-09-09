using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class LapisEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lapis Enchantment");
            Tooltip.SetDefault(
@"'Gotta go fast'
20% increased movement speed
Effects of Lapis Pendant
Summons a pet Nicky and Buzzy Beetle");

DisplayName.AddTranslation(GameCulture.Chinese, "青金魔石");

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
            player.moveSpeed += 0.2f;

            //lapis pendant
            modPlayer.LapisPendant = true;

            //pets soon tm

        }

        private readonly string[] items =
        {
            "LapisHelmet",
            "LapisChest",
            "LapisLegs",
            "LapisPendant",
            "LapisStaff",
            "LapisBow",
            "LapisJavelin",
            "Haven",
            "LapisPick",
            "SpiritLink"
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
