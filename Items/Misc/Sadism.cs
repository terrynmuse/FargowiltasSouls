using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class Sadism : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sadism");
            Tooltip.SetDefault( "Grants immunity to almost all Masochist Mode debuffs");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 11;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.consumable = true;
            item.buffType = mod.BuffType("Sadism");
            item.buffTime = 7200;
            item.UseSound = SoundID.Item3;
            item.value = Item.sellPrice(0, 1);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }
    }
}
