using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class CelestialRune : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Celestial Rune");
            Tooltip.SetDefault(
@"Grants immunity to Marked for Death and Hexed
You may periodically fire additional attacks depending on weapon type
Taking damage creates a friendly Ancient Vision to attack enemies"); 
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 10;
            item.value = Item.sellPrice(0, 10);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("MarkedforDeath")] = true;
            player.buffImmune[mod.BuffType("Hexed")] = true;
            player.GetModPlayer<FargoPlayer>().CelestialRune = true;
            if (player.GetModPlayer<FargoPlayer>().CelestialRuneTimer > 0)
                player.GetModPlayer<FargoPlayer>().CelestialRuneTimer--;
        }
    }
}
