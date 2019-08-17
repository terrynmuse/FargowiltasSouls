using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class MutantsCurse : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mutant's Curse");
            Tooltip.SetDefault("'At least this way, you don't need that doll'");
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
            item.consumable = true;
            item.value = Item.buyPrice(1);
        }

        public override bool UseItem(Player player)
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                int mutant = NPC.FindFirstNPC(ModLoader.GetMod("Fargowiltas").NPCType("Mutant"));
                if (mutant > -1 && Main.npc[mutant].active)
                {
                    Main.npc[mutant].Transform(mod.NPCType("MutantBoss"));
                    if (Main.netMode == 0)
                        Main.NewText("Mutant has awoken!", 175, 75, 255);
                    else if (Main.netMode == 2)
                        NetMessage.BroadcastChatMessage(NetworkText.FromLiteral("Mutant has awoken!"), new Color(175, 75, 255));
                }
                else
                {
                    NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("MutantBoss"));
                }
            }
            else
            {
                NPC.SpawnOnPlayer(player.whoAmI, mod.NPCType("MutantBoss"));
            }
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
