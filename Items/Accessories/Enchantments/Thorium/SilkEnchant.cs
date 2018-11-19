using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class SilkEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silk Enchantment");
            Tooltip.SetDefault(
@"''
7% increased magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 0;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            SilkEffect(player);
        }
        
        private void SilkEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            player.magicDamage += 0.07f;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("SilkCap"));
            recipe.AddIngredient(thorium.ItemType("SilkHat"));
            recipe.AddIngredient(thorium.ItemType("SilkTabard"));
            recipe.AddIngredient(thorium.ItemType("SilkLeggings"));
            recipe.AddIngredient(ItemID.WandofSparking);
            recipe.AddIngredient(thorium.ItemType("IceCube"));
            recipe.AddIngredient(thorium.ItemType("EighthPlagueStaff")); //nice diver, dam memer
            recipe.AddIngredient(thorium.ItemType("WindGust"));
            recipe.AddIngredient(thorium.ItemType("Cure"));
            recipe.AddIngredient(ItemID.ZebraSwallowtailButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
