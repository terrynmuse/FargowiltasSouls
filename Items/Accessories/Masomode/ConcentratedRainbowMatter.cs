using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class ConcentratedRainbowMatter : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Concentrated Rainbow Matter");
            Tooltip.SetDefault(@"'Taste the rainbow'
Grants immunity to Flames of the Universe
Summons a rainbow slime (which is just slime AI but inflicts Flames of the Universe and is bigger maybe, 
additionally if he has a target but hasnt hit them in so long, he jumps super high to them or shoots some rainbow shet)");
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
            player.buffImmune[mod.BuffType("FlamesoftheUniverse")] = true;
            player.AddBuff(mod.BuffType("RainbowSlime"), 2);
        }
    }
}
