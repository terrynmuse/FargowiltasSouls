using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Linq;
using CalamityMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class CalamitySoul : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        public int dragonTimer = 60;
        public const int FireProjectiles = 2;
        public const float FireAngleSpread = 120f;
        public int FireCountdown;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Tyrant");
            Tooltip.SetDefault(
@"'And the land grew quiet once more...'
All armor bonuses from Aerospec, Statigel, Daedalus, and Bloodflare
All armor bonuses from Victide, Xeroc, and Omega Blue
All armor bonuses from God Slayer, Silva, and Auric Tesla
All armor bonuses from Mollusk, Reaver, and Ataxia
All armor bonuses from Astral, Tarragon, and Demonshade
Effects of the Spirit Glyph, Raider's Talisman, and Trinket of Chi
Effects of Gladiator's Locket and Unstable Prism
Effects of Counter Scarf and Fungal Symbiote
Effects of Permafrost's Concoction and Regenerator
Effects of the Core of the Blood God and Affliction
Effects of Deep Diver, The Transformer, and Luxor's Gift
Effects of The Community, Abyssal Diving Suit, and Lumenous Amulet
Effects of the Aquatic Emblem, Nebulous Core, and Draedon's Heart
Effects of the The Amalgam, Godly Soul Artifact, and Yharim's Gift
Effects of Heart of the Elements and The Sponge
Effects of Giant Pearl and Amidias' Pendant
Effects of Fabled Tortoise Shell and Plague Hive
Effects of the Astral Arcanum and Hide of Astrum Deus
Effects of the Profaned Soul Artifact and Dark Sun Ring");
            DisplayName.AddTranslation(GameCulture.Chinese, "暴君之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'于是大地再次恢复了平静...'
拥有天蓝, 斯塔提斯, 代达罗斯和血炎的套装效果
拥有胜潮, 克希洛克和蓝色欧米茄的套装效果
拥有弑神者, 始源林海和古圣金源的套装效果
拥有软壳, 掠夺者和阿塔西亚的套装效果
拥有炫星, 龙蒿和魔影的套装效果
拥有灵魂浮雕, 掠袭者护符和气之挂坠的效果
拥有角斗士链坠和不稳定棱镜的效果
拥有反击围巾和真菌共生体的效果
拥有佩码·福洛斯特之融魔台和再生器的效果
拥有血神核心和灾劫之尖啸的效果
拥有深潜者, 变压器和祖玛的礼物的效果
拥有归一元心石, 幽影潜渊服和流明护身符的效果
拥有海波纹章, 星云之核和嘉登之心的效果
拥有聚合之脑, 圣魂神物和魔君的礼物的效果
拥有元灵之心和化绵留香石的效果
拥有大珍珠和阿米迪亚斯之垂饰的效果
拥有寓言龟壳和瘟疫蜂巢的效果
拥有星陨幻空石和星神游龙外壳的效果
拥有渎魂神物和蚀日尊戒的效果");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;//
            item.value = 20000000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);

            //Apocalypse
            mod.GetItem("ApocalypseForce").UpdateAccessory(player, hideVisual);
            //Desolation
            mod.GetItem("DesolationForce").UpdateAccessory(player, hideVisual);
            //Devastation
            mod.GetItem("DevastationForce").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ApocalypseForce");
            recipe.AddIngredient(null, "DevastationForce");
            recipe.AddIngredient(null, "DesolationForce");

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
