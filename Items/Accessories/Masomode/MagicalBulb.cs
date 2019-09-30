using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class MagicalBulb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magical Bulb");
            Tooltip.SetDefault(@"'Matricide?'
Grants immunity to Venom, Ivy Venom, and Swarming
Increases life regeneration
Attracts a legendary plant's offspring which flourishes in combat");
            DisplayName.AddTranslation(GameCulture.Chinese, "魔法球茎");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'杀妈?'
免疫毒液, 常春藤毒和蜂群
增加生命回复
吸引一株传奇植物的后代, 其会在战斗中茁壮成长");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Venom] = true;
            player.buffImmune[mod.BuffType("IvyVenom")] = true;
            player.buffImmune[mod.BuffType("Swarming")] = true;
            player.lifeRegen += 2;
            if (SoulConfig.Instance.GetValue("Plantera Minion"))
                player.AddBuff(mod.BuffType("PlanterasChild"), 2);
        }
    }
}
