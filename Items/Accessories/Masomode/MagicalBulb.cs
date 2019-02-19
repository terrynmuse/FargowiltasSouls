using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class MagicalBulb : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magical Bulb");
            Tooltip.SetDefault(@"Grants immunity to Venom
Increases life regeneration
Attracts a legendary plant's offspring which flourishes in combat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 7;
            item.value = Item.sellPrice(0, 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Venom] = true;
            player.AddBuff(mod.BuffType("PlanterasChild"), 5);
            player.lifeRegen += 2;
        }
    }
}
