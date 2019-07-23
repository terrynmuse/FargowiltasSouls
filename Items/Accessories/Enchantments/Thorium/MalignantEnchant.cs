using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class MalignantEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malignant Enchantment");
            Tooltip.SetDefault(
@"'How evil is too evil?'
Magic critical strikes engulf enemies in a long lasting void flame
Effects of Mana-Charged Rocketeers");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.malignantSet = true;
            //mana charge rockets
            thorium.GetItem("ManaChargedRocketeers").UpdateAccessory(player, hideVisual);
        }
        
        private readonly string[] items =
        {
            "ManaChargedRocketeers",
            "JellyPondWand",
            "DarkTome",
            "ChampionBomberStaff",
            "GaussSpark",
            "MagicThorHammer"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("MalignantCap"));
            recipe.AddIngredient(thorium.ItemType("MalignantRobe"));
            recipe.AddIngredient(null, "SilkEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.PurpleEmperorButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
