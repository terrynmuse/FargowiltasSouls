using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class MutantAntibodies : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant Antibodies");
            Tooltip.SetDefault(@"'Healthy drug recommended by 0 out of 10 doctors'
Grants immunity to Feral Bite and Mutant Nibble
Grants immunity to most debuffs caused by entering water
Increases damage by 20%");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 10;
            item.value = Item.sellPrice(0, 7);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.Rabies] = true;
            player.buffImmune[mod.BuffType("MutantNibble")] = true;
            player.GetModPlayer<FargoPlayer>().MutantAntibodies = true;
            player.meleeDamage += 0.2f;
            player.rangedDamage += 0.2f;
            player.magicDamage += 0.2f;
            player.minionDamage += 0.2f;
            player.thrownDamage += 0.2f;
        }
    }
}
