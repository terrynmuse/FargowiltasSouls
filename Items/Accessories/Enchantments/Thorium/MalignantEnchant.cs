using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class MalignantEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malignant Enchantment");
            Tooltip.SetDefault(
@"'How evil is too evil?'
Critical strikes engulf enemies in a long lasting void flame
Effects of Mana-Charged Rocketeers");
            DisplayName.AddTranslation(GameCulture.Chinese, "妖术魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'要多邪恶才能算得上太邪恶呢?'
魔法暴击释放虚空之焰吞没敌人
拥有魔力充能火箭靴的效果");
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

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.MalignantEnchant = true;
            //mana charge rockets
            thorium.GetItem("ManaChargedRocketeers").UpdateAccessory(player, hideVisual);
        }
        
        private readonly string[] items =
        {
            "ManaChargedRocketeers",
            "JellyPondWand",
            "DarkTome",
            "ChampionBomberStaff",
            "GaussSpark",
            "MagicThorHammer"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("MalignantCap"));
            recipe.AddIngredient(thorium.ItemType("MalignantRobe"));
            recipe.AddIngredient(null, "SilkEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.PurpleEmperorButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
