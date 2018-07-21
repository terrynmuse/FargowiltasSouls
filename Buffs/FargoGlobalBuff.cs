using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs
{
    class FargoGlobalBuff : GlobalBuff
    {
        public override void Update(int type, Player player, ref int buffIndex)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            if (type == BuffID.ShadowFlame)
            {
                modPlayer.Shadowflame = true;
            }

            if(type == BuffID.Slimed)
            {
                Main.buffNoTimeDisplay[type] = false;
                modPlayer.Slimed = true;
            }

            base.Update(type, player, ref buffIndex);
        }
    }
}
