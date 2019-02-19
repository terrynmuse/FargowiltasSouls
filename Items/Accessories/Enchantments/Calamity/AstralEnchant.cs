using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class AstralEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Enchantment");
            Tooltip.SetDefault(
@"'The Astral Infection has consumed you...'
Whenever you crit an enemy fallen, hallowed, and astral stars will rain down
This effect has a 1 second cooldown before it can trigger again
Effects of the Astral Arcanum");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            modPlayer.astralStarRain = true;
            //astral arcanum
            modPlayer.astralArcanum = true;
            modPlayer.aBulwark = true;
            modPlayer.projRef = true;
            player.buffImmune[calamity.BuffType("GodSlayerInferno")] = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("AstralHelm"));
            recipe.AddIngredient(calamity.ItemType("AstralBreastplate"));
            recipe.AddIngredient(calamity.ItemType("AstralLeggings"));
            recipe.AddIngredient(calamity.ItemType("AstralArcanum"));
            recipe.AddIngredient(calamity.ItemType("LunicEye"));
            recipe.AddIngredient(calamity.ItemType("Starfall"));
            recipe.AddIngredient(calamity.ItemType("Nebulash"));
            recipe.AddIngredient(calamity.ItemType("TitanArm"));
            recipe.AddIngredient(calamity.ItemType("HivePod"));
            recipe.AddIngredient(calamity.ItemType("AstralStaff"));
            recipe.AddIngredient(calamity.ItemType("AegisBlade"));
            recipe.AddIngredient(calamity.ItemType("Omniblade"));
            recipe.AddIngredient(calamity.ItemType("Lazhar"));
            recipe.AddIngredient(calamity.ItemType("SolsticeClaymore"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
