using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class MutantsFury : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mutant's Fury");
            Tooltip.SetDefault("'REALLY enrages Mutant... or doesn't'");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.maxStack = 999;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
            item.value = Item.buyPrice(1);
        }

        public override bool UseItem(Player player)
        {
            FargoSoulsWorld.AngryMutant = !FargoSoulsWorld.AngryMutant;
            string text = FargoSoulsWorld.AngryMutant ? "Mutant is angered!" : "Mutant is calm.";
            if (Main.netMode == 0)
            {
                Main.NewText(text, 175, 75, 255);
            }
            else if (Main.netMode == 2)
            {
                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), new Color(175, 75, 255));
                NetMessage.SendData(7); //sync world
            }
            Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            return true;
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
