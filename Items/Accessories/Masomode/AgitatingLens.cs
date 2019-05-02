using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class AgitatingLens : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Agitating Lens");
            Tooltip.SetDefault(@"'The irritable remnant of a defeated foe'
Grants immunity to Berserked
10% increased damage when below half HP
While dashing or running quickly you will create a trail of demon scythes");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 4);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Berserked")] = true;
            
            if(player.statLife < player.statLifeMax2 / 2)
                player.GetModPlayer<FargoPlayer>().AllDamageUp(.10f);

            player.GetModPlayer<FargoPlayer>().AgitatingLens = true;
        }
    }
}
