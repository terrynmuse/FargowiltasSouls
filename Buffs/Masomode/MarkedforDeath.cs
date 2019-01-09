using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Buffs.Masomode
{
	public class MarkedforDeath : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Marked for Death");
			Description.SetDefault("On death's doorstep when time runs out");
			Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
		}

		//note: clearing this buff (i.e. nurse) will remove it without killing the player
		public override void Update(Player player, ref int buffIndex)
        {
			if (player.buffTime[buffIndex] < 3)
			{
                int damage = player.statLife - player.statLifeMax2 / 10;

                if (damage > 0) //simulating basics of player.hurt()
                {
                    if (Main.netMode == 1)
                        NetMessage.SendData(84, -1, -1, null, player.whoAmI);

                    player.statLife = player.statLifeMax2 / 10;

                    NetMessage.SendData(13, -1, -1, null, player.whoAmI);
                    NetMessage.SendData(16, -1, -1, null, player.whoAmI);
                    NetMessage.SendPlayerHurt(player.whoAmI, PlayerDeathReason.ByCustomReason(player.name + " was reaped by the cold hand of death."), damage, 0, false, false, -1, -1, -1);

                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), CombatText.DamagedFriendly, damage, false, false);

                    player.immune = true;
                    player.immuneTime = player.longInvince ? 80 : 40;

                    player.lifeRegenTime = 0;

                    if (player.Male)
                        Main.PlaySound(1, (int)player.position.X, (int)player.position.Y, 1, 1f, 0.0f);
                    else
                        Main.PlaySound(20, (int)player.position.X, (int)player.position.Y, 1, 1f, 0.0f);
                }
                else
                {
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + " was reaped by the cold hand of death."), 4444, 0);
                }

                player.DelBuff(buffIndex);
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
