using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class NecromanticBrew : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Necromantic Brew");
            Tooltip.SetDefault(@"'The bone-growing solution of a defeated foe'
Grants immunity to Lethargic
Summons 2 Skeletron arms to whack enemies
May attract baby skeleton heads");
            DisplayName.AddTranslation(GameCulture.Chinese, "死灵密酿");
            Tooltip.AddTranslation(GameCulture.Chinese, @"被击败敌人的促进骨生长的溶液
免疫昏昏欲睡
召唤2个骷髅王手臂重击敌人
可能会吸引宝宝骷髅头");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Lethargic")] = true;
            player.GetModPlayer<FargoPlayer>().NecromanticBrew = true;
            if (SoulConfig.Instance.GetValue("Skeletron Arms Minion"))
                player.AddBuff(mod.BuffType("SkeletronArms"), 2);
        }
    }
}
