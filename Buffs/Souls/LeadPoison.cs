using FargowiltasSouls.NPCs;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Souls
{
    public class LeadPoison : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lead Poison");
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            Main.debuff[Type] = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<FargoGlobalNPC>().LeadPoison = true;

            if (!npc.boss)
            {
                npc.velocity.X *= .9f;
                npc.velocity.Y *= .9f;
            }
        }
    }
}