using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SqueakyToy : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squeaky Toy");
            Tooltip.SetDefault(@"'The beloved toy of a defeated foe...?'
Grants immunity to Squeaky Toy and Guilty
Attacks have a chance to squeak and deal 1 damage to you");
            DisplayName.AddTranslation(GameCulture.Chinese, "吱吱响的玩具");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'被打败的敌人心爱的玩具...?
免疫吱吱响的玩具和净化
敌人攻击概率发出吱吱声,并只造成1点伤害");
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
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
            player.GetModPlayer<FargoPlayer>().SqueakyAcc = true;
        }
    }
}
