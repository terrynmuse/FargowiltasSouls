using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class Corrupt : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Corrupt Heart");
            Tooltip.SetDefault(@"''
Grants immunity to Rotting
10% increased movement speed
You spawn mini eaters to seek out enemies every few attacks");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Rotting")] = true;
            player.moveSpeed += 0.1f;
            
            modPlayer.CorruptHeart = true;
        }
    }
}
