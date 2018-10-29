using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DreadEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dread Enchantment");
            Tooltip.SetDefault(
                @"'Infused with souls of the damned'
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            DreadEffect(player);
        }
        
        private void DreadEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }
        
        private readonly string[] items =
        {
            "DreadSkull",
            "DreadChestPlate",
            "DreadGreaves",
            "CrashBoots",
            "CursedCore",
            "CorruptSubwoofer",
            "TunePlayerMovementSpeed"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.ChainGuillotines);
            recipe.AddIngredient(thorium.ItemType("ImpactDrill"));
            recipe.AddIngredient(thorium.ItemType("DreadLauncher"));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
