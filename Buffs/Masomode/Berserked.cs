using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Berserked : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Berserked");
            Description.SetDefault("You cannot control yourself");
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
            DisplayName.AddTranslation(GameCulture.Chinese, "狂暴");
            Description.AddTranslation(GameCulture.Chinese, "你控几不住你记几");
        }

        public override void Update(Player player, ref int buffIndex)
        {
            //causes player to constantly use weapon
            //seemed to have strange interactions with stunning debuffs like frozen or stoned...
            player.HeldItem.autoReuse = true;
            player.controlUseItem = true;
            player.releaseUseItem = true;
        }
    }
}