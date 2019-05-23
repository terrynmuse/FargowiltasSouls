using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class GroundStick : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Remote Control");
            Tooltip.SetDefault(@"'A defeated foe's segment with an antenna glued on'
Grants immunity to Lightning Rod
Your attacks have a small chance to inflict Lightning Rod
Two friendly probes fight by your side");
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
            if (Soulcheck.GetValue("Probes Minion"))
                player.AddBuff(mod.BuffType("Probes"), 2);
        }
    }
}
