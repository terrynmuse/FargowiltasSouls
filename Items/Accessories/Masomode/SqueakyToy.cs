using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class SqueakyToy : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Squeaky Toy");
            Tooltip.SetDefault(@"''
Grants immunity to Squeaky Toy and Purified
Every 4th? hit you take will squeak and deal 1 damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 8);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            
            //do a squeak

            
        }
    }
}
