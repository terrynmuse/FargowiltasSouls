using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class NobleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Noble Enchantment");
            Tooltip.SetDefault(
                @"''
Your symphonic empowerments will last an additional 5 seconds
Your symphonic damage will briefly singe hit enemies. Symphonic critical strikes cause an eruption of molten music notes
Increases damage by 1. Increases damage by additional 1 for every nearby player who wears it
Your symphonic damage empowers all nearby allies with: Burning Soul. Damage done against burning enemies is increased by 8%. 
Doubles the range of your empowerments effect radius.");
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
            
            NobleEffect(player);
        }
        
        private void NobleEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //noble set bonus
            thoriumPlayer.bardBuffDuration += 300;
            //mix tape
            thoriumPlayer.mixtapeBool = true;
            //molten woofer
            //thoriumPlayer.subwooferFire = true;
            thoriumPlayer.bardRangeBoost += 450;
        }
        
        private readonly string[] items =
        {
            "NoblesHat",
            "NoblesJerkin",
            "NoblesLeggings",
            "MixTape",
            "RingofUnity",
            "MoltenSubwoofer",
            "Microphone",
            "Bongos",
            "FieryTotemCaller",
            "FireblossomButterfly"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
