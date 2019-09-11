using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class GroundStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Remote Control");
            Tooltip.SetDefault(@"'A defeated foe's segment with an antenna glued on'
Grants immunity to Lightning Rod
Your attacks have a small chance to inflict Lightning Rod
Two friendly probes fight by your side");
            DisplayName.AddTranslation(GameCulture.Chinese, "遥控装置");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'被击败敌人的残片,上面粘着天线'
免疫避雷针
攻击小概率造成避雷针效果
召唤2个友善的探测器为你而战");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 6;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("LightningRod")] = true;
            player.GetModPlayer<FargoPlayer>().GroundStick = true;
            if (SoulConfig.Instance.GetValue("Probes Minion"))
                player.AddBuff(mod.BuffType("Probes"), 2);
        }
    }
}
