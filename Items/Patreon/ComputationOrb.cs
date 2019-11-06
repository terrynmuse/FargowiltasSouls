using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Patreon
{
    public class ComputationOrb : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Computation Orb");
            Tooltip.SetDefault(
@"Non magic attacks will deal 25% extra damage and consume 10 mana");
            DisplayName.AddTranslation(GameCulture.Chinese, "演算宝珠");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"非魔法攻击将额外造成25%伤害, 并消耗10法力");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
            modPlayer.CompOrb = true;
        }
    }
}
