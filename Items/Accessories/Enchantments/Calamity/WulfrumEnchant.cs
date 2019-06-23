using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class WulfrumEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("CalamityMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wulfrum Enchantment");
            Tooltip.SetDefault(
@"'Not to be confused with Tungsten Enchantment…'
+5 defense when below 50% life
Effects of the Spirit Glyph, Raider's Talisman, and Trinket of Chi");
            DisplayName.AddTranslation(GameCulture.Chinese, "钨钢魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'别和钨金魔石搞混了...'
生命值低于50%时, 增加5点防御
拥有灵魂浮雕, 古墓护符和极之挂坠的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 40000;
            item.defense = 3;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            if (player.statLife <= (int)(player.statLifeMax2 * 0.5))
            {
                player.statDefense += 5;
            }
            //spirit glyph
            modPlayer.sGenerator = true;
            //raiders talisman
            modPlayer.raiderTalisman = true;
            //trinket of chi
            modPlayer.trinketOfChi = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("WulfrumHelm"));
            recipe.AddIngredient(calamity.ItemType("WulfrumHeadgear"));
            recipe.AddIngredient(calamity.ItemType("WulfrumHood"));
            recipe.AddIngredient(calamity.ItemType("WulfrumHelmet"));
            recipe.AddIngredient(calamity.ItemType("WulfrumMask"));
            recipe.AddIngredient(calamity.ItemType("WulfrumArmor"));
            recipe.AddIngredient(calamity.ItemType("WulfrumLeggings"));
            recipe.AddIngredient(calamity.ItemType("SpiritGenerator"));
            recipe.AddIngredient(calamity.ItemType("RaidersTalisman"));
            recipe.AddIngredient(calamity.ItemType("TrinketofChi"));
            recipe.AddIngredient(calamity.ItemType("IcicleStaff"));
            recipe.AddIngredient(calamity.ItemType("MandibleBow"));
            recipe.AddIngredient(calamity.ItemType("MarniteSpear"));
            recipe.AddIngredient(calamity.ItemType("Pumpler"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
