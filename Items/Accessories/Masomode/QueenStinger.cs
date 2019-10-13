using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class QueenStinger : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Queen's Stinger");
            Tooltip.SetDefault(@"'Ripped right off of a defeated foe'
Grants immunity to Infested
Increases armor penetration by 10
Your attacks inflict Poisoned
Bees and weak Hornets become friendly");
            DisplayName.AddTranslation(GameCulture.Chinese, "女王的毒刺");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'从一个被打败的敌人身上撕下来'
免疫感染
增加10点护甲穿透
攻击造成中毒效果
蜜蜂和虚弱黄蜂变得友好");
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
            //pen
            player.armorPenetration += 10;

            player.buffImmune[mod.BuffType("Infested")] = true;

            //bees
            player.npcTypeNoAggro[210] = true;
            player.npcTypeNoAggro[211] = true;

            //hornets
            player.npcTypeNoAggro[42] = true;
            player.npcTypeNoAggro[231] = true;
            player.npcTypeNoAggro[232] = true;
            player.npcTypeNoAggro[233] = true;
            player.npcTypeNoAggro[234] = true;
            player.npcTypeNoAggro[235] = true;

            //stinger immmune
            player.GetModPlayer<FargoPlayer>().QueenStinger = true;
        }
    }
}
