using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class MutantAntibodies : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant Antibodies");
            Tooltip.SetDefault(@"'Healthy drug recommended by 0 out of 10 doctors'
Grants immunity to Wet, Feral Bite, and Mutant Nibble
Grants immunity to most debuffs caused by entering water
Grants effects of Wet debuff while riding Cute Fishron
Increases damage by 20%");
            DisplayName.AddTranslation(GameCulture.Chinese, "突变抗体");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'推荐健康药物指数: 0/10'
免疫潮湿,野性咬噬和突变啃啄
免疫大部分由水造成的Debuff
骑乘超可爱猪鲨时获得潮湿状态
增加20%伤害");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 9;
            item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Wet] = true;
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[mod.BuffType("MutantNibble")] = true;
            player.GetModPlayer<FargoPlayer>().MutantAntibodies = true;
            player.GetModPlayer<FargoPlayer>().AllDamageUp(0.2f);
            if (player.mount.Active && player.mount.Type == MountID.CuteFishron)
                player.dripping = true;
        }
    }
}
