using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;
using System;

namespace FargowiltasSouls.Items.Accessories.Forces.Calamity
{
    public class ApocalypseForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of the Apocalypse");
            Tooltip.SetDefault(
@"'Where once there was life and light, only ruin remains...'
All armor bonuses from Aerospec, Statigel, Daedalus, and Bloodflare
Effects of the Spirit Glyph, Raider's Talisman, and Trinket of Chi
Effects of Gladiator's Locket and Unstable Prism
Effects of Counter Scarf and Fungal Symbiote
Effects of Permafrost's Concoction and Regenerator
Effects of the Core of the Blood God and Affliction");
            DisplayName.AddTranslation(GameCulture.Chinese, "灾厄之力");
            Tooltip.AddTranslation(GameCulture.Chinese,
@"'只带走生命,只留下废土'
拥有胜潮,天蓝和斯塔提斯的套装效果
拥有代达罗斯,掠夺者和星幻的套装效果
拥有软壳,阿塔西亚和克希洛克的套装效果
拥有佩码·福洛斯特之融魔台,星陨幻空石和星神游龙外壳的效果
拥有瘟疫蜂巢,大珍珠和阿米迪亚斯之垂饰的效果");

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
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            if (!modPlayer.TerrariaSoul)
            {
                //WULFRUM
                //spirit glyph
                calamityPlayer.sGenerator = true;
                //raiders talisman
                calamityPlayer.raiderTalisman = true;
                //trinket of chi
                calamityPlayer.trinketOfChi = true;
            }
            //AEROSPEC
            mod.GetItem("AerospecEnchant").UpdateAccessory(player, hideVisual);
            //STATIGEL
            mod.GetItem("StatigelEnchant").UpdateAccessory(player, hideVisual);
            //DAEDALUS
            mod.GetItem("DaedalusEnchant").UpdateAccessory(player, hideVisual);
            //BLOOD FLARE
            mod.GetItem("BloodflareEnchant").UpdateAccessory(player, hideVisual);
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "WulfrumEnchant");
            recipe.AddIngredient(null, "AerospecEnchant");
            recipe.AddIngredient(null, "StatigelEnchant");
            recipe.AddIngredient(null, "DaedalusEnchant");
            recipe.AddIngredient(null, "BloodflareEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
