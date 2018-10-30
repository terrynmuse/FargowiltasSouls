using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class RhapsodistEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rhapsodist Enchantment");
            Tooltip.SetDefault(
                @"''
+15% chance for inspiration notes to drop from enemies
Symphonic empowerments last 6 seconds longer
increased wind instrument homing speed, string instrument projectiles bounce an additional time
Pressing the Special Ability key will give you endless amounts of inspiration and greatly increased symphonic damage and playing speed. This effect lasts for 10 seconds and needs to recharge for 1 minute
dropped inspiration notes are more potent and give a random level I empowerment to all nearby allies when picked up
Pressing the Special Ability key will overload all nearby allies with every empowerment III for 15 seconds. Using this ability requires 20 inspiration and must recharge for one minute
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1; //blood orange
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            RhapsodistEffect(player);
        }
        
        private void RhapsodistEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            
            
        }
        
        private readonly string[] items =
        {
            "SoloistHat",
            "RallyHat",
            "RhapsodistChestWoofer",
            "RhapsodistBoots",
            "MusicSheet6",
            "SirensAllure",
            "TerrariumAutoharp",
            "Holophonor",
            "Sousaphone",
            "EdgeofImagination"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            //ballader
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
