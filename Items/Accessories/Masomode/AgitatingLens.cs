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
            DisplayName.SetDefault("AgitatingLens");
            Tooltip.SetDefault(@"''
Grants immunity to Berserked
10% increased damage when below half HP
While Dashing or running quickly you will create a trail of blood scythes");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 4;
            item.value = Item.sellPrice(0, 4);
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Berserked")] = true;
            
            if(player.life < 0player.lifeMax / 2)
            {
              player.AllDamageUp(.10f);
            }
            
            if(player.speed.X ABS > 10? || player.dashing)
            {
              counter++;
              
              if(counter > 30)
              {
                counter = 0;
                
                spawn Blood Scythe
              }
            }
        }
    }
}
