using FargowiltasSouls.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
	public class SuperBleed : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Super Bleed");
			Main.buffNoSave[Type] = true;

		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<FargoGlobalNpc>(mod).SBleed = true;
		}
	}
}