using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class TideHunterEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tide Hunter Enchantment");
            Tooltip.SetDefault(
@"'Not just for hunting fish'
Ranged critical strikes release a splash of foam, slowing nearby enemies
After four consecutive non-critical strikes, your next ranged attack will mini-crit for 150% damage
Effects of Goblin War Shield and Agnor's Bowl");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 80000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //tide hunter set bonus
            thoriumPlayer.tideHunterSet = true;
            //angler bowl
            thorium.GetItem("AnglerBowl").UpdateAccessory(player, hideVisual);
            //yew set bonus
            thoriumPlayer.yewCharging = true;
            //goblin war shield
            thorium.GetItem("GoblinWarshield").UpdateAccessory(player, hideVisual);
        }
        
        private readonly string[] items =
        {
            "AnglerBowl",
            "ThoriumBow",
            "BlunderBuss",
            "PearlPike",
            "HydroCannon",
            "MarineLauncher"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("TideHunterCap"));
            recipe.AddIngredient(thorium.ItemType("TideHunterChestpiece"));
            recipe.AddIngredient(thorium.ItemType("TideHunterLeggings"));
            recipe.AddIngredient(null, "YewWoodEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
