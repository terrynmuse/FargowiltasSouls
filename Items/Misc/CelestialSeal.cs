using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class CelestialSeal : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Celestial Seal");
            Tooltip.SetDefault(@"Permanently increases the number of accessory slots
Only usable after Demon Heart");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.maxStack = 99;
            item.useStyle = 4;
            item.useAnimation = 30;
            item.useTime = 30;
            item.consumable = true;
            item.UseSound = SoundID.Item123;
        }

        /*public override bool CanUseItem(Player player)
        {
            return player.extraAccessorySlots == 1;
        }*/

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0
                && player.extraAccessorySlots == 1)
            {
                player.GetModPlayer<FargoPlayer>().CelestialSeal = true;
            }
            return true;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB);
                }
            }
        }
    }
}
