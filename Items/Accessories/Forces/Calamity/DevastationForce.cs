using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;
using System;

namespace FargowiltasSouls.Items.Accessories.Forces.Calamity
{
    public class DevastationForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Devastation");
            Tooltip.SetDefault(
@"'Rain hell down on those who resist your power'
All armor bonuses from Mollusk, Reaver, and Ataxia
All armor bonuses from Astral, Tarragon, and Demonshade
Effects of Giant Pearl and Amidias' Pendant
Effects of Fabled Tortoise Shell and Plague Hive
Effects of the Astral Arcanum and Hide of Astrum Deus
Effects of the Profaned Soul Artifact and Dark Sun Ring");
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

            //MOLLUSK
            mod.GetItem("MolluskEnchant").UpdateAccessory(player, hideVisual);
            //REAVER
            mod.GetItem("ReaverEnchant").UpdateAccessory(player, hideVisual);
            //ATAXIA
            mod.GetItem("AtaxiaEnchant").UpdateAccessory(player, hideVisual);
            //ASTRAL
            mod.GetItem("AstralEnchant").UpdateAccessory(player, hideVisual);
            //TARRAGON
            mod.GetItem("TarragonEnchant").UpdateAccessory(player, hideVisual);
            //DEMON SHADE
            mod.GetItem("DemonShadeEnchant").UpdateAccessory(player, hideVisual);
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "MolluskEnchant");
            recipe.AddIngredient(null, "ReaverEnchant");
            recipe.AddIngredient(null, "AtaxiaEnchant");
            recipe.AddIngredient(null, "AstralEnchant");
            recipe.AddIngredient(null, "TarragonEnchant");
            recipe.AddIngredient(null, "DemonShadeEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
