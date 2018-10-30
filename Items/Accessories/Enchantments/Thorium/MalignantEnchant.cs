using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class MalignantEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malignant Enchantment");
            Tooltip.SetDefault(
                @"''
Magic critical strikes inflict Light Curse & On Fire!
Allows flight at the cost of mana. Increases mana regeneration slightly");
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
            
            MalignantEffect(player);
        }
        
        private void MalignantEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }
        
        private readonly string[] items =
        {
            "MalignantCap",
            "MalignantRobe",
            "ManaChargedRocketeers",
            "ThoriumStaff",
            "JellyPondWand",
            "ChampionBomberStaff",
            "GaussSpark",
            "MagicThorHammer",
            "SpineBreaker"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.PurpleEmperorButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
