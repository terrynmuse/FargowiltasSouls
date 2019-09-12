using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class XerocEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xeroc Enchantment");
            Tooltip.SetDefault(
@"'The power of an ancient god at your command…'
All attacks have a chance to inflict On Fire! and Cursed Inferno
Melee attacks create Xeroc Blast explosions
Ranged attacks spawn Xeroc Fire sparks
Magic attacks spawn Xeroc Orbs
Minion attacks spawn Xeroc Bubbles
Rogue attacks spawn Xeroc Stars
Effects of The Community");
            DisplayName.AddTranslation(GameCulture.Chinese, "克希洛克魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'掌握着一位上古之神的力量...'
所有攻击概率造成着火和诅咒地狱
近战攻击造成克希洛克爆炸
远程攻击生成克希洛克火花
魔法攻击生成克希洛克法球
召唤攻击生成克希洛克泡泡
盗贼攻击生成克希洛克之星
拥有归一元心石的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 9;
            item.value = 1000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (SoulConfig.Instance.GetValue("Xeroc Effects"))
            {
               
                modPlayer.xerocSet = true;
            }

            //the community
            modPlayer.community = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(calamity.ItemType("XerocMask"));
            recipe.AddIngredient(calamity.ItemType("XerocPlateMail"));
            recipe.AddIngredient(calamity.ItemType("XerocCuisses"));
            recipe.AddIngredient(calamity.ItemType("TheCommunity"));
            recipe.AddIngredient(calamity.ItemType("BrinyBaron"));
            recipe.AddIngredient(calamity.ItemType("StormRuler"));
            recipe.AddIngredient(calamity.ItemType("ThornBlossom"));
            recipe.AddIngredient(calamity.ItemType("Interfacer"));
            recipe.AddIngredient(calamity.ItemType("ElephantKiller"));
            recipe.AddIngredient(calamity.ItemType("UltraLiquidator"));
            recipe.AddIngredient(calamity.ItemType("Shredder"));
            recipe.AddIngredient(calamity.ItemType("Infinity"));
            recipe.AddIngredient(calamity.ItemType("GrandDad"));
            recipe.AddIngredient(calamity.ItemType("ElementalBlaster"));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
