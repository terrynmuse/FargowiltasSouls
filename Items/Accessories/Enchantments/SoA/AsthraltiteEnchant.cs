using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class AsthraltiteEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asthraltite Enchantment");
            Tooltip.SetDefault(
@"'Asph-... Asthath-... How are you meant to pronounce this?'
Press [Ability (Primary)] to deploy one of 4 spells
Press [Ability (Primary)] and Up/Down to cycle between the spells
Deploying a spell will initiate a cooldown of 1 minute
Effects of Ring of the Fallen, Memento Mori, and Arcanum of the Caster
Summons a memory of Anthee and the Insignia of Extinction");
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
            modPlayer.AstralSet = true;

            //ring of the fallen
            ModLoader.GetMod("SacredTools").GetItem("AsthralRing").UpdateAccessory(player, hideVisual);

            //memento mori
            ModLoader.GetMod("SacredTools").GetItem("MementoMori").UpdateAccessory(player, hideVisual);

            //arcanum of the caster
            ModLoader.GetMod("SacredTools").GetItem("CasterArcanum").UpdateAccessory(player, hideVisual);

            //pets soon tm
        }

        private readonly string[] items =
        {
            "AsthralChest",
            "AsthralLegs",
            "AsthralRing",
            "MementoMori",
            "CasterArcanum",
            "Ghrimorhas",
            "MidnightBlade",
            "EmblemOfRemembrance",
            "SeraphSigil"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyAstralHelmet");

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
