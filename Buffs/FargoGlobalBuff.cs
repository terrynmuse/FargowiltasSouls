using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                modPlayer.shadowflame = true;
            }

            if(type == BuffID.Slimed)
            {
                Main.buffNoTimeDisplay[type] = false;
                modPlayer.slimed = true;
            }

            base.Update(type, player, ref buffIndex);
        }
    }
}
