using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class IridescentEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iridescent Enchantment");
            Tooltip.SetDefault(
@"'All the colors of the rainbow'
Your radiant damage has a 15% chance to release a blinding flash of light
The flash heals nearby allies equal to your bonus healing and confuses enemies
Effects of Equalizer");
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
            //set bonus
            thoriumPlayer.iridescentSet = true;
            //equalizer 
            thoriumPlayer.equilibrium = true;
        }
        
        private readonly string[] items =
        {
            "IridescentHelmet",
            "IridescentMail",
            "IridescentGreaves",
            "Equalizer",
            "HereticBreaker",
            "AquaiteScythe",
            "SpiritPouch", 
            "TranquilLyre",
            "IridescentStaff", 
            "SerpentsBubbleWand"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
