using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class XerocEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xeroc Enchantment");
            Tooltip.SetDefault(
@"'The power of an ancient god at your command…'
All attacks have a chance to inflict On Fire! and Cursed Inferno
Melee attacks create Xeroc Blast explosions
Ranged attacks spawn Xeroc Fire sparks
Magic attacks spawn Xeroc Orbs
Minion attacks spawn Xeroc Bubbles
Rogue attacks spawn Xeroc Stars");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 9;
            item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            if (Soulcheck.GetValue("Xeroc Effects"))
            {
                CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
                modPlayer.xerocSet = true;
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("XerocMask"));
            recipe.AddIngredient(calamity.ItemType("XerocPlateMail"));
            recipe.AddIngredient(calamity.ItemType("XerocCuisses"));
            recipe.AddIngredient(calamity.ItemType("BrinyBaron"));
            recipe.AddIngredient(calamity.ItemType("StormRuler"));
            recipe.AddIngredient(calamity.ItemType("Hydra"));
            recipe.AddIngredient(calamity.ItemType("Interfacer"));
            recipe.AddIngredient(calamity.ItemType("ElephantKiller"));
            recipe.AddIngredient(calamity.ItemType("Effervescence"));
            recipe.AddIngredient(calamity.ItemType("PlagueKeeper"));
            recipe.AddIngredient(calamity.ItemType("UltraLiquidator"));
            recipe.AddIngredient(calamity.ItemType("Shredder"));
            recipe.AddIngredient(calamity.ItemType("SpatialLance"));
            recipe.AddIngredient(calamity.ItemType("ElementalBlaster"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
