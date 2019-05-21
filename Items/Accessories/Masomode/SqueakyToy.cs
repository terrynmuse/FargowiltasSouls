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
            Tooltip.SetDefault(@"'The beloved toy of a defeated foe...?'
Grants immunity to Squeaky Toy and Purified
Attacks have a chance to squeak and deal 1 damage to you");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 3);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("SqueakyToy")] = true;
            player.buffImmune[mod.BuffType("Purified")] = true;
            player.GetModPlayer<FargoPlayer>().SqueakyAcc = true;
        }
    }
}
