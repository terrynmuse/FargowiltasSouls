using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class TribalCharm : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tribal Charm");
            Tooltip.SetDefault(@"''
Grants immunity to Webbed and Suffocation
Increases max life by 50
Increases flight time by 25%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 5;
            item.value = Item.sellPrice(0, 4);
            item.defense = 6;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Webbed] = true;
            player.buffImmune[BuffID.Suffocation] = true;
            player.statLifeMax2 += 50;
            player.GetModPlayer<FargoPlayer>().wingTimeModifier += 0.25f;
        }
    }
}
