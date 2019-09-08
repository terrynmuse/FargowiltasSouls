using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class CrierEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crier Enchantment");
            Tooltip.SetDefault(
@"'Nothing to cry about'
10% increased inspiration regeneration
Effects of Music Notes");
            DisplayName.AddTranslation(GameCulture.Chinese, "传迅员魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'没什么可说的'
增加10%灵感回复
拥有音符的效果");
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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.bardResourceRecharge += 10;
            //music notes
            thoriumPlayer.bardBuffDuration += 120;
        }
        
        private readonly string[] items =
        {
            "BardCap",
            "BardChest",
            "BardLeggings",
            "MusicNotes",
            "WoodenWhistle",
            "Flute",
            "DrumMallet",
            "Harmonica",
            "DynastyGuzheng",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            //because bards attract birds?
            recipe.AddIngredient(ItemID.Cardinal);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
