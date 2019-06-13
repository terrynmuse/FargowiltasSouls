using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class Sadism : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sadism");
            Tooltip.SetDefault(@"'Proof of having embraced suffering'
Grants immunity to almost all Masochist Mode debuffs");
            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 3));
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
            item.buffTime = 25200;
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
