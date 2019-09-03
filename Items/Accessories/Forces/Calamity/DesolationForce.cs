using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using CalamityMod;
using Terraria.Localization;
using System;

namespace FargowiltasSouls.Items.Accessories.Forces.Calamity
{
    public class DesolationForce : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Desolation");
            Tooltip.SetDefault(
@"'When the world is barren and cold, you will be all that remains'
All armor bonuses from Victide, Xeroc, and Omega Blue
All armor bonuses from God Slayer, Silva, and Auric Tesla
Effects of Deep Diver, The Transformer, and Luxor's Gift
Effects of The Community, Abyssal Diving Suit, and Lumenous Amulet
Effects of the Aquatic Emblem, Nebulous Core, and Draedon's Heart
Effects of the The Amalgam, Godly Soul Artifact, and Yharim's Gift
Effects of Heart of the Elements and The Sponge");
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

            //VICTIDE
            mod.GetItem("VictideEnchant").UpdateAccessory(player, hideVisual);
            //XEROC
            mod.GetItem("XerocEnchant").UpdateAccessory(player, hideVisual);
            //OMEGA BLUE
            mod.GetItem("OmegaBlueEnchant").UpdateAccessory(player, hideVisual);
            //GODSLAYER
            mod.GetItem("GodSlayerEnchant").UpdateAccessory(player, hideVisual);
            //SILVA
            mod.GetItem("SilvaEnchant").UpdateAccessory(player, hideVisual);
            //AURIC
            mod.GetItem("AuricEnchant").UpdateAccessory(player, hideVisual);
        }


        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "VictideEnchant");
            recipe.AddIngredient(null, "XerocEnchant");
            recipe.AddIngredient(null, "SilvaEnchant");
            recipe.AddIngredient(null, "OmegaBlueEnchant");
            recipe.AddIngredient(null, "GodSlayerEnchant");
            recipe.AddIngredient(null, "AuricEnchant");

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
