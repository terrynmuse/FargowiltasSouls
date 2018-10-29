using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class IcyEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Icy Enchantment");
            Tooltip.SetDefault(
                @"'Gives your hair some frosty tips too'
An icy aura surrounds you, which attempts to slow down approaching enemies
Summons a baby penguin");
        } //alternatively 'Ice to meet you' 

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
            
            IcyEffect(player);
        }
        
        private void IcyEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }
        
        private readonly string[] items =
        {
            "IcyBandana",
            "IcyMail",
            "IcyGreaves",
            "FrostFireKatana",
            "FrostFury",
            "Blizzard"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.IceBoomerang);
            recipe.AddIngredient(ItemID.SnowballLauncher);
            recipe.AddIngredient(thorium.ItemType("ShiverthornButterfly"));
            recipe.AddIngredient(ItemID.Fish);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
