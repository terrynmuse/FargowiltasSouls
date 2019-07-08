using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

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
            DisplayName.AddTranslation(GameCulture.Chinese, "无尽诅咒");
            Description.AddTranslation(GameCulture.Chinese, "所有物品均不消耗,但有时会对自己造成伤害");
		}

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().Infinity = true;
        }
    }
}