using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class DragonFang : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Fang");
            Tooltip.SetDefault(@"'Warm to the touch'
Grants immunity to Clipped Wings and Crippled
Your attacks have a 10% chance to inflict Clipped Wings on non-boss enemies");
            DisplayName.AddTranslation(GameCulture.Chinese, "龙牙");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'触感温暖'
免疫剪除羽翼和残疾
攻击有10%概率对非Boss单位造成剪除羽翼效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            if (SoulConfig.Instance.GetValue("Inflict Clipped Wings"))
                player.GetModPlayer<FargoPlayer>().DragonFang = true;
        }
    }
}
