using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class SandstoneEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sandstone Enchantment");
            Tooltip.SetDefault(
                @"'Enveloped by desert winds'
Grants Sandstorm in a Bottle effect
Thrown attacks might refresh your jump
10% increased chance to not consume throwing items");
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
            
            SandEffect(player);
        }
        
        private void SandEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        } 

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("hSandStoneHelmet"));
            recipe.AddIngredient(thorium.ItemType("iSandStoneMail"));
            recipe.AddIngredient(thorium.ItemType("jSandStoneGreaves"));
            recipe.AddIngredient(thorium.ItemType("Wreath"));
            recipe.AddIngredient(thorium.ItemType("DesertWindRune"));
            recipe.AddIngredient(thorium.ItemType("StoneThrowingSpear"), 300);
            recipe.AddIngredient(thorium.ItemType("OceanThrowingAxe"), 300);
            recipe.AddIngredient(thorium.ItemType("gSandStoneThrowingKnife"), 300);
            recipe.AddIngredient(thorium.ItemType("TalonBurst"));
            recipe.AddIngredient(ItemID.BlackScorpion);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
