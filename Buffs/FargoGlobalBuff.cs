using FargowiltasSouls.NPCs;
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

        public override void Update(int type, NPC npc, ref int buffIndex)
        {
            FargoGlobalNPC globalNPC = npc.GetGlobalNPC<FargoGlobalNPC>();

            if(type == BuffID.Chilled)
            {
                //globalNPC.
                npc.color = Colors.RarityBlue;

                if(!npc.boss)
                {
                    npc.velocity *= .5f;
                }
            }
        }
    }
}
