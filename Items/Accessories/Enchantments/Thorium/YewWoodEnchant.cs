using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class YewWoodEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Yew-Wood Enchantment");
            Tooltip.SetDefault(
@"'This strange wood comes from a far away land'
After four consecutive non-critical strikes, your next ranged attack will mini-crit for 150% damage
Effects of Goblin War Shield");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //yew set bonus
            thoriumPlayer.yewCharging = true;
            //goblin war shield
            thorium.GetItem("GoblinWarshield").UpdateAccessory(player, hideVisual);
        }
        
        private readonly string[] items =
        {
            "YewWoodaHelmet",
            "YewWoodBreastgaurd", //diver PLEASE
            "YewWoodLeggings",
            "GoblinWarshield",
            "eSandStoneBow",
            "FeatherFoe",
            "YewWoodBow",
            "YewGun",
            "ShadowflameWand"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));
            
            recipe.AddIngredient(thorium.ItemType("SpikeBomb"), 300);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
