using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class WhiteDwarfEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("White Dwarf Enchantment");
            Tooltip.SetDefault(
@"'Throw with the force of nuclear fusion'
Critical strikes will unleash ivory flares from the cosmos
Ivory flares deal 0.1% of the hit target's maximum life as damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 300000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            player.GetModPlayer<ThoriumPlayer>(thorium).whiteDwarfSet = true;
        }
        
        private readonly string[] items =
        {
            "WhiteDwarfMask",
            "WhiteDwarfGuard",
            "WhiteDwarfGreaves",
            "BlackHammer",
            "GeodeMallet",
            "LodeStoneHammer",
            "BlackDagger",
            "CosmicDagger"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("WhiteDwarfKunai"), 300);
            recipe.AddIngredient(thorium.ItemType("AngelsEnd"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
