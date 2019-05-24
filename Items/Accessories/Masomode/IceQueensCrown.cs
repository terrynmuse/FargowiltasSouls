using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class IceQueensCrown : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Queen's Crown");
            Tooltip.SetDefault(@"'The royal symbol of a defeated foe'
Grants immunity to Frozen
Increases damage reduction by 5%
Summons a friendly super Flocko");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
            item.defense = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.05f;
            player.buffImmune[BuffID.Frozen] = true;
            if (Soulcheck.GetValue("Flocko Minion"))
                player.AddBuff(mod.BuffType("SuperFlocko"), 2);
        }
    }
}
