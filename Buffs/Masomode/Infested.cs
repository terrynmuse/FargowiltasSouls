using Terraria;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Infested : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Infested");
            Description.SetDefault("This can only get worse.");
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

        public override void Update(Player player, ref int buffIndex)
        {
            FargoPlayer p = player.GetModPlayer<FargoPlayer>(mod);

            //weak DOT that grows exponentially stronger
            if (p.FirstInfection)
            {
                p.MaxInfestTime = player.buffTime[buffIndex];
                p.FirstInfection = false;
            }

            p.Infested = true;
        }

        /*public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }*/

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<FargoGlobalNPC>().Infested = true;
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return true;
        }
    }
}