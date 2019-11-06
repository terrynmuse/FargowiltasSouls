using Terraria;

namespace FargowiltasSouls
{
	public class MNet
	{
		public static void SendBaseNetMessage(int msg, params object[] param)
		{
			if (Main.netMode == 0) { return; } //nothing to sync in SP
            BaseNet.WriteToPacket(Fargowiltas.Instance.GetPacket(), (byte)msg, param).Send();
		}
	}
}