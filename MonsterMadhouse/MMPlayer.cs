using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace FargowiltasSouls
{
	public class MMPlayer : ModPlayer
	{
		public override void PostUpdate()
		{
            if (MMWorld.MMPoints >= 1000)
            {
                FargoSoulsWorld.downedMM = true;
                Main.NewText("The armies of Terraria retreat! Victory!", 250, 170, 50);
                MMWorld.MMPoints = 0;
                MMWorld.MMArmy = false;
                if (Main.netMode == NetmodeID.Server)
                {
                    NetMessage.SendData(MessageID.WorldData); // Immediately inform clients of new world state.
                }
            }
        }
	}
}
