using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Misc
{
	public class BloodiedSkull : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bloodied Skull");
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
            return !NPC.AnyNPCs(NPCID.SkeletronHead) && !NPC.AnyNPCs(NPCID.OldMan);
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                if (!NPC.downedBoss3)
                {
                    Main.dayTime = false;
                    Main.time = 0;
                }
                if (Main.netMode == 2) //sync time, downed boss flags, other world stuff
                    NetMessage.SendData(7, -1, -1, null, 0, 0f, 0f, 0f, 0, 0, 0);
                Main.PlaySound(15, player.Center, 0);
                NPC.SpawnOnPlayer(player.whoAmI, NPC.downedBoss3 ? NPCID.DungeonGuardian : NPCID.OldMan);
            }
            return true;
        }
    }
}
