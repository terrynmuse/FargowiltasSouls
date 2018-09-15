using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
	public class MarkedforDeath : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Marked for Death");
			Description.SetDefault("You will die when time runs out.");
			Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
		}
		
		public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";

            return true;
        }

		//note: deleting this buff (i.e. nurse) will remove it without killing the player
		public override void Update(Player player, ref int buffIndex)
        {
			if (player.buffTime[buffIndex] < 3)
			{
				player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was reaped by the cold hand of death."), 4444, 0);
			}
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            if (time < player.buffTime[buffIndex])
                player.buffTime[buffIndex] = time;

            return true;
        }
    }
}
