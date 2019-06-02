using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class WretchedPouch : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wretched Pouch");
            Tooltip.SetDefault(
@"'The accursed incendiary powder of a defeated foe'
Grants immunity to Shadowflame
You erupt into Shadowflame tentacles when injured");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.ShadowFlame] = true;
            player.GetModPlayer<FargoPlayer>().WretchedPouch = true;
        }
    }
}
