using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class RabiesShot : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Rabies Shot");
            Tooltip.SetDefault("Cures Feral Bite");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 3;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.consumable = true;
            item.UseSound = SoundID.Item3;
            item.value = Item.sellPrice(0, 0, 4, 0);
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                player.ClearBuff(BuffID.Rabies);
            }
            return true;
        }
    }
}
