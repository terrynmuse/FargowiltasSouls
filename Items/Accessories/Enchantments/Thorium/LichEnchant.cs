using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class LichEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lich Enchantment");
            Tooltip.SetDefault(
@"'Embrace death...'
Killing an enemy will release a soul fragment
Touching a soul fragment greatly increases your movement and throwing speed briefly
Effects of Lich's Gaze");
            DisplayName.AddTranslation(GameCulture.Chinese, "巫妖魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'拥抱死亡...'
杀死敌人会释放灵魂碎片
触摸灵魂碎片会在短时间内极大增加移动速度和投掷速度
拥有巫妖之视效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //lich effect
            modPlayer.LichEnchant = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;
        }
        
        private readonly string[] items =
        {
            "LichCowl",
            "LichCarapace",
            "LichTalon",
            "LichGaze",
            "RocketFist",
            "SoulCleaver"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(thorium.ItemType("SoulBomb"), 300);
            recipe.AddIngredient(thorium.ItemType("CadaverCornet"));
            recipe.AddIngredient(thorium.ItemType("TitanJavelin"), 300);
            recipe.AddIngredient(thorium.ItemType("PumpkinPaint"));
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
