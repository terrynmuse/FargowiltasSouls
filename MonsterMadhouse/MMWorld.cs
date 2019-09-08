using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;

namespace FargowiltasSouls
{
	public class MMWorld : ModWorld
	{
		public static int MMPoints = 0;
		public static bool MMArmy;

        public static bool downedMage = false;
        public static bool downedSummoner = false;
        public static bool downedDutchman = false;
        public static bool downedOgre = false;
        public static bool downedWood = false;
        public static bool downedPumpking = false;
        public static bool downedEverscream = false;
        public static bool downedSantaNK1 = false;
        public static bool downedElsa = false;
        public static bool downedBetsy = false;

		public override void Initialize()
        {
            downedMage = false;
            downedSummoner = false;
            downedDutchman = false;
            downedOgre = false;
            downedWood = false;
            downedPumpking = false;
            downedEverscream = false;
            downedSantaNK1 = false;
            downedElsa = false;
            downedBetsy = false;
            MMArmy = false;
		}

		public override void PostUpdate()
		{
            if (MMPoints >= 1000)
            {
                FargoSoulsWorld.downedMM = true;
                Main.NewText("The armies of Terraria retreat! Victory!", 250, 170, 50);
                MMPoints = 0;
                MMArmy = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            }
        }
	}
}
