using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class AbominationnVoodooDoll : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("???");
            Tooltip.SetDefault("???");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.value = Item.sellPrice(0, 1);
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (item.lavaWet)
            {
                item.active = false;
                if (Fargowiltas.Instance.FargosLoaded && Main.netMode != 1)
                {
                    int abominationn = NPC.FindFirstNPC(ModLoader.GetMod("Fargowiltas").NPCType("Abominationn"));
                    int mutant = NPC.FindFirstNPC(ModLoader.GetMod("Fargowiltas").NPCType("Mutant"));
                    if (abominationn > -1 && Main.npc[abominationn].active)
                    {
                        Main.npc[abominationn].life = 0;
                        Main.npc[abominationn].checkDead();
                        if (mutant > -1 && Main.npc[mutant].active)
                        {
                            Main.npc[mutant].Transform(mod.NPCType("MutantBoss"));
                            if (Main.netMode == 0)
                                Main.NewText("Mutant has been enraged by the death of his brother!", 175, 75, 255);
                            else if (Main.netMode == 2)
                                NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Mutant has been enraged by the death of his brother!"), new Color(175, 75, 255));
                        }
                    }
                }
            }
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
