using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class GalacticGlobe : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galactic Globe");
            Tooltip.SetDefault(@"'Always watching'
Grants immunity to Flipped, Unstable, Distorted, and Chaos State
Allows the holder to control gravity
Summons the true eyes of Cthulhu to protect you
Increases flight time by 100%");
            DisplayName.AddTranslation(GameCulture.Chinese, "银河球");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'时刻注视'
免疫翻转,不稳定,扭曲和混沌
允许使用者改变重力
召唤真·克苏鲁之眼保护你
增加100%飞行时间");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 10;
            item.value = Item.sellPrice(0, 8);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
            //player.buffImmune[mod.BuffType("CurseoftheMoon")] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.buffImmune[BuffID.ChaosState] = true;

            if (Soulcheck.GetValue("Gravity Control"))
                player.gravControl = true;

            if (Soulcheck.GetValue("True Eyes Minion"))
                player.AddBuff(mod.BuffType("TrueEyes"), 2);
            
            player.GetModPlayer<FargoPlayer>().GravityGlobeEX = true;
            player.GetModPlayer<FargoPlayer>().wingTimeModifier += 1f;
        }
    }
}
