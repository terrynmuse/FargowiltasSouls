using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Buffs.Souls
{
	public class Shock : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Shock");
			Main.buffNoSave[Type] = true;
		}

		public override void Update(NPC npc, ref int buffIndex)
		{
			npc.GetGlobalNPC<FargoGlobalNPC>(mod).shock = true;
		}
	}
}