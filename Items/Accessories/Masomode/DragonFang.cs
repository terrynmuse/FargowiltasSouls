using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class DragonFang : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon's Fang");
            Tooltip.SetDefault(@"Grants immunity to Clipped Wings and Crippled
Your attacks have a 10% chance to inflict Clipped Wings on non-boss enemies");
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
            player.buffImmune[mod.BuffType("ClippedWings")] = true;
            player.buffImmune[mod.BuffType("Crippled")] = true;
            player.GetModPlayer<FargoPlayer>().DragonFang = true;
        }
    }
}
