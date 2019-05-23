using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SaucerControlConsole : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Saucer Control Console");
            Tooltip.SetDefault(@"'Just keep it in airplane mode'
Grants immunity to Electrified
Summons a friendly Mini Saucer");
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
            player.buffImmune[BuffID.Electrified] = true;
            if (Soulcheck.GetValue("Saucer Minion"))
                player.AddBuff(mod.BuffType("SaucerMinion"), 2);
        }
    }
}
