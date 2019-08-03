using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class CelestialEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Enchantment");
            Tooltip.SetDefault(
@"'Harmonious energy embraces you'
Pressing the 'Special Ability' key will summon an incredibly powerful aura around your cursor
Creating this aura costs 150 mana
Effects of Ascension Statuette");
            DisplayName.AddTranslation(GameCulture.Chinese, "天界魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'谐能环绕着你'
按下'特殊能力'键将在光标处召唤无比强大的光环
召唤光环消耗150法力
拥有飞升雕像的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.celestialSet = true;
            //ascension statue
            thoriumPlayer.ascension = true;
        }
        
        private readonly string[] items =
        {
            "CelestialCrown",
            "CelestialVestment",
            "CelestialLeggings",
            "CelestialTrinity",
            "AscensionStatuette",
            "HealingRain",
            "AncientTome",
            "BlackScythe",
            "CelestialNova",
            "DivineStaff"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
