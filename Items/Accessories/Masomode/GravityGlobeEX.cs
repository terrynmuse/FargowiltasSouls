using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class GravityGlobeEX : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Globe of the Cosmos");
            Tooltip.SetDefault(@"Grants immunity to Flipped, Unstable, and Distorted
Allows the holder to control gravity
Increases flight time by 100%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 10;
            item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Flipped")] = true;
            player.buffImmune[mod.BuffType("FlippedHallow")] = true;
            player.buffImmune[mod.BuffType("Unstable")] = true;
            player.buffImmune[BuffID.VortexDebuff] = true;
            player.gravControl = true;
            player.GetModPlayer<FargoPlayer>().GravityGlobeEX = true;
        }
    }
}
