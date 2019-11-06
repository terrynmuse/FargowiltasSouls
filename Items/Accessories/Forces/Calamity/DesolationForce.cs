using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
Effects of Heart of the Elements and The Sponge
Summons several pets");
            DisplayName.AddTranslation(GameCulture.Chinese, "荒芜之力");
            Tooltip.AddTranslation(GameCulture.Chinese,
@"'你将成为这个荒芜寒冷世界的最后幸存者'
拥有胜潮, 克希洛克和蓝色欧米茄的套装效果
拥有弑神者,  始源林海和古圣金源的套装效果
拥有深潜者, 变压器和祖玛的礼物的效果
拥有归一元心石, 幽影潜渊服和流明护身符的效果
拥有海波纹章, 星云之核和嘉登之心的效果
拥有聚合之脑, 圣魂神物和魔君的礼物的效果
拥有元灵之心和化绵留香石的效果");

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
