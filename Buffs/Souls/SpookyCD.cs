using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
	public class SpookyCd : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Scythe Cooldown");
			Description.SetDefault("No Scythes for a bit");
			Main.buffNoSave[Type] = true;
			canBeCleared = false;
			Main.debuff[Type] = true;
		}

	}
}