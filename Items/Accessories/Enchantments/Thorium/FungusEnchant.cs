using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class FungusEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fungus Enchantment");
            Tooltip.SetDefault(
@"'There's a fungus among us'
Damage done against mycelium infected enemies is increased by 10%
Dealing damage to enemies infected with mycelium briefly increases throwing speed by 10%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 120000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.fungusSet = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(thorium.ItemType("FungusHat"));
            recipe.AddIngredient(thorium.ItemType("FungusGuard"));
            recipe.AddIngredient(thorium.ItemType("FungusLeggings"));
            recipe.AddIngredient(thorium.ItemType("Chum"), 300);
            recipe.AddIngredient(thorium.ItemType("VenomKunai"), 300);
            recipe.AddIngredient(thorium.ItemType("MorelGrenade"), 300);
            recipe.AddIngredient(thorium.ItemType("MyceliumWhip"));
            recipe.AddIngredient(thorium.ItemType("SporeBook"));
            recipe.AddIngredient(thorium.ItemType("LegionOrnament"), 300);
            recipe.AddIngredient(thorium.ItemType("SwampSpike"));
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
