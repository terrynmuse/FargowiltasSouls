using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class MagicalBulb : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magical Bulb");
            Tooltip.SetDefault(@"'Matricide?'
Grants immunity to Venom and Ivy Venom
Increases life regeneration
Attracts a legendary plant's offspring which flourishes in combat");
            DisplayName.AddTranslation(GameCulture.Chinese, "魔法球茎");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'杀妈?'
免疫毒液和常春藤毒
增加生命回复
吸引一个传奇植物的后代,在战斗中茁壮成长");
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
            player.lifeRegen += 2;
            if (Soulcheck.GetValue("Plantera Minion"))
                player.AddBuff(mod.BuffType("PlanterasChild"), 2);
        }
    }
}
