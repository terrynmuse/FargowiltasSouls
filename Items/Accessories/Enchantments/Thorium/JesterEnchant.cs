using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class JesterEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jester Enchantment");
            Tooltip.SetDefault(
                @"''
");
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
            
            JesterEffect(player);
        }
        
        private void JesterEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyJesterMask");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyJesterShirt");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyJesterLeggings");
            recipe.AddIngredient(thorium.ItemType("FanLetter"));
            recipe.AddRecipeGroup("FargowiltasSouls:AnyTambourine");
            recipe.AddIngredient(thorium.ItemType("Oboe"));
            recipe.AddIngredient(thorium.ItemType("SkywareLute"));
            recipe.AddIngredient(thorium.ItemType("Panflute"));
            recipe.AddIngredient(thorium.ItemType("DeathweedButterfly"));
            recipe.AddIngredient(ItemID.Mouse);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
