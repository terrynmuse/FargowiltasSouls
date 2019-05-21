using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class MysticSkull : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystic Skull");
            Tooltip.SetDefault(@"'The quietly muttering head of a defeated foe'
Grants immunity to Suffocation
Works in your inventory
10% reduced magic damage
Automatically use mana potions when needed");
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
            player.buffImmune[BuffID.Suffocation] = true;
            player.magicDamage -= 0.1f;
            player.manaFlower = true;
        }
    }
}
