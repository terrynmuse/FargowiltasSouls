using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Forces.SoA
{
    public class GenerationsForce : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Generations");
            Tooltip.SetDefault(
@"'Through all this world's years, none have seen anything quite like you'
All armor bonuses from Bismuth, Frosthunter, and Blightbone
All armor bonuses from Dreadfire, Space Junk, and Marstech
Effects of Dreadflame Emblem, Lapis Pendant, Frigid Pendant, and Pumpkin Amulet");
            DisplayName.AddTranslation(GameCulture.Chinese, "世代之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'这么多年来, 从未出现过像你这样的人'
拥有铋, 霜冻猎人和荒骨的套装效果
拥有惧焰, 太空垃圾和火星科技的套装效果
拥有恐惧火焰徽记, 青金石挂饰, 极寒吊坠和南瓜护身符的效果
召唤数个宠物");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SoALoaded) return;

            FargoPlayer fargoPlayer = player.GetModPlayer<FargoPlayer>();
            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //bismuth
            modPlayer.bismuthArmor = true;

            //frosthunter
            modPlayer.frostburnRanged = true;
            //frigid pendant
            modPlayer.decreePendant = true;
            if (hideVisual)
            {
                modPlayer.decreePendantHide = true;
            }
            //blightbone
            modPlayer.blightEmpowerment = true;
            //dreadflame emblem
            modPlayer.dreadEmblem = true;
            //dreadfire
            modPlayer.DreadEffect = true;
            //pumpkin amulet
            modPlayer.pumpkinAmulet = true;
            //marstech
            modPlayer.marsArmor = true;
            //space junk
            modPlayer.spaceJunk = true;

            //pets soon tm
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SoALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "PrairieEnchant");
            recipe.AddIngredient(null, "BismuthEnchant");
            recipe.AddIngredient(null, "LapisEnchant");
            recipe.AddIngredient(null, "FrosthunterEnchant");
            recipe.AddIngredient(null, "BlightboneEnchant");
            recipe.AddIngredient(null, "DreadfireEnchant");
            recipe.AddIngredient(null, "MarstechEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
