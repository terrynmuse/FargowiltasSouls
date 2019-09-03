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
拥有蓝色欧米茄,圣金源和魔影的套装效果
拥有亵渎神物,血神核心和灾劫之尖啸的效果
拥有星云之核,圣魂神物和魔君的礼物的效果
拥有反击围巾,归一元心石,嘉登之心和聚合之脑的效果
拥有元灵之心,化绵留香石和蚀日尊戒的效果");

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
            mod.GetItem("DesolationEnchant").UpdateAccessory(player, hideVisual);
            //Devastation
            mod.GetItem("DevastationEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CalamityForce");
            recipe.AddIngredient(null, "TarragonEnchant");
            recipe.AddIngredient(null, "BloodflareEnchant");
            recipe.AddIngredient(null, "OmegaBlueEnchant");
            recipe.AddIngredient(null, "GodSlayerEnchant");
            recipe.AddIngredient(null, "SilvaEnchant");
            recipe.AddIngredient(null, "AuricEnchant");
            recipe.AddIngredient(null, "DemonShadeEnchant");

            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
