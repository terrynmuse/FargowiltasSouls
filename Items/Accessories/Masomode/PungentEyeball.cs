using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class PungentEyeball : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pungent Eyeball");
            Tooltip.SetDefault(@"Grants immunity to Rotting and the Tongue
Increases your max number of minions by 2
Increases your max number of sentries by 2
'It's fermenting...'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Rotting")] = true;
            player.buffImmune[BuffID.TheTongue] = true;
            player.maxMinions += 2;
            player.maxTurrets += 2;
        }
    }
}
