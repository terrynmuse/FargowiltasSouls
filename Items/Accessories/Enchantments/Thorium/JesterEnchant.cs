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
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jester Enchantment");
            Tooltip.SetDefault(
@"'Clowning around'
Symphonic critical strikes ring a bell over your head, slowing all nearby enemies briefly
Effects of Fan Letter");
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
            thoriumPlayer.jesterSet = true;
            //fan letter
            thoriumPlayer.bardResourceMax2 += 2;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddRecipeGroup("FargowiltasSouls:AnyJesterMask");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyJesterShirt");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyJesterLeggings");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyLetter");
            recipe.AddRecipeGroup("FargowiltasSouls:AnyTambourine");
            recipe.AddIngredient(thorium.ItemType("Oboe"));
            recipe.AddIngredient(thorium.ItemType("SkywareLute"));
            recipe.AddIngredient(thorium.ItemType("Panflute"));
            recipe.AddIngredient(thorium.ItemType("ConchShell"));
            recipe.AddIngredient(ItemID.Mouse);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
