using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
	public class InfinityDebuff : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Curse of Infinity");
			Description.SetDefault("You consume nothing, but sometimes damage yourself");
			Main.buffNoSave[Type] = true;
			canBeCleared = false;
			Main.debuff[Type] = true;
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().Infinity = true;
        }
    }
}