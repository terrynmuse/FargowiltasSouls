using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SandsofTime : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        //sprite should be an hourglass
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sands of Time");
            Tooltip.SetDefault(@"'Whatever you do, don't drop it'
Works in your inventory
Grants immunity to Mighty Wind
You respawn twice as fast when no boss is alive");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateInventory(Player player)
        {
            player.buffImmune[BuffID.WindPushed] = true;

            //respawn faster ech
            player.GetModPlayer<FargoPlayer>().SandsofTime = true;
        }
    }
}
