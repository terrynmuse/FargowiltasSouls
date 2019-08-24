using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls;

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