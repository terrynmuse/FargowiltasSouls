using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class LunaticSigil : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lunatic Sigil");
            DisplayName.AddTranslation(GameCulture.Chinese, "血迹斑斑的头骨");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 1;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(NPCID.CultistBoss) && !NPC.AnyNPCs(NPCID.CultistDevote) && !NPC.AnyNPCs(NPCID.CultistArcherBlue);
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                Main.PlaySound(15, player.Center, 0);
                if (Main.netMode != 1)
                {
                    int n = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 300, NPCID.CultistBoss);
                    if (n != 200 && Main.netMode == 2)
                        NetMessage.SendData(23, -1, -1, null, n);
                }
                if (NPC.downedAncientCultist)
                    for (int i = 0; i < 9; i++)
                        NPC.SpawnOnPlayer(player.whoAmI, NPCID.CultistBoss);
            }
            return true;
        }
    }
}
