using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class ReinforcedPlating : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Reinforced Plating");
            Tooltip.SetDefault(@"'The sturdiest piece of a defeated foe'
Grants immunity to Defenseless, Stunned, and knockback
Reduces damage taken by 10%");
            DisplayName.AddTranslation(GameCulture.Chinese, "强化钢板");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'被打败的敌人最坚强的一面'
免疫毫无防御,昏迷和击退
减少10%所受伤害");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 6;
            item.value = Item.sellPrice(0, 4);
            item.defense = 10;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Defenseless")] = true;
            player.buffImmune[mod.BuffType("Stunned")] = true;
            player.endurance += 0.1f;
            player.noKnockback = true;
        }
    }
}
