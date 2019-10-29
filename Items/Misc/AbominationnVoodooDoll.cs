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
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Abominationn Voodoo Doll");
            Tooltip.SetDefault("Summons Abominationn to your town\n'You are a terrible person'");
            DisplayName.AddTranslation(GameCulture.Chinese, "憎恶巫毒娃娃");
            Tooltip.AddTranslation(GameCulture.Chinese, "你可真是个坏东西");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 11;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.value = Item.sellPrice(0, 1);
        }

        public override bool CanUseItem(Player player)
        {
            return Fargowiltas.Instance.FargosLoaded && !NPC.AnyNPCs(ModLoader.GetMod("Fargowiltas").NPCType("Abominationn"));
        }

        public override bool UseItem(Player player)
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModLoader.GetMod("Fargowiltas").NPCType("Abominationn"));
            }
            return true;
        }

        public override void Update(ref float gravity, ref float maxFallSpeed)
        {
            if (item.lavaWet)
            {
                item.active = false;
                if (Fargowiltas.Instance.FargosLoaded)
                {
                    if (Main.netMode != 1)
                    {
                        int abominationn = NPC.FindFirstNPC(ModLoader.GetMod("Fargowiltas").NPCType("Abominationn"));
                        int mutant = NPC.FindFirstNPC(ModLoader.GetMod("Fargowiltas").NPCType("Mutant"));
                        if (abominationn > -1 && Main.npc[abominationn].active)
                        {
                            Main.npc[abominationn].StrikeNPC(9999, 0f, 0);
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
                else
                {
                    NPC.SpawnOnPlayer(Player.FindClosest(item.position, 0, 0), mod.NPCType("MutantBoss"));
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
