using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class FrosthunterEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frosthunter Enchantment");
            Tooltip.SetDefault(
@"'The hunter now hunted, the prey now predator'
15% increased ranged damage while in the snow biome
Ranged projectiles frostburn enemies
Effects of Frigid Pendant
Summons a Howling Death pup and a Tabby Slime to follow you around");
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

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.frostburnRanged = true;
            if (player.ZoneSnow)
            {
                player.rangedDamage += 0.15f;
            }

            //frigid pendant
            modPlayer.decreePendant = true;
            if (hideVisual)
            {
                modPlayer.decreePendantHide = true;
            }

            //pets soon tm
        }

        private readonly string[] items =
        {
            "FrosthunterHeaddress",
            "FrosthunterWrappings",
            "FrosthunterBoots",
            "DecreeCharm",
            "OmegaStrongbow",
            "IceclawShuriken",
            "FrostGlobeStaff",
            "FrostBeam",
            "DecreeChop",
            "CharmOfH"
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
