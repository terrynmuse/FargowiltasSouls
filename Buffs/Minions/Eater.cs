using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Minions
{
	public class Eater : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Eater");
			Description.SetDefault("The Eater of Worlds will protect you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.buffNoSave[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
			if (player.ownedProjectileCounts[mod.ProjectileType("EaterHead")] > 0)
			{
				modPlayer.Eater = true;
			}
			if (!modPlayer.Eater)
			{
				player.DelBuff(buffIndex);
				buffIndex--;
			}
			else
			{
				player.buffTime[buffIndex] = 18000;
			}
		}
	}
}