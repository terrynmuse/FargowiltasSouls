using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class PumpkingsCape : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpking's Cape");
            Tooltip.SetDefault(@"'Somehow, it's the right size'
Grants immunity to Living Wasteland
Increases damage and critical strike chance by 5%
Your critical strikes inflict Rotting
You may periodically fire additional attacks depending on weapon type");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 8;
            item.value = Item.sellPrice(0, 6);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.05f;
            player.rangedDamage += 0.05f;
            player.magicDamage += 0.05f;
            player.thrownDamage += 0.05f;
            player.minionDamage += 0.05f;
            player.meleeCrit += 5;
            player.rangedCrit += 5;
            player.magicCrit += 5;
            player.thrownCrit += 5;
            player.buffImmune[mod.BuffType("LivingWasteland")] = true;
            player.GetModPlayer<FargoPlayer>().PumpkingsCape = true;
            player.GetModPlayer<FargoPlayer>().AdditionalAttacks = true;
        }
    }
}
