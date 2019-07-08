using FargowiltasSouls.NPCs;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Souls
{
    public class TimeFrozen : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Time Frozen");
            Main.buffNoSave[Type] = true;
            DisplayName.AddTranslation(GameCulture.Chinese, "时间冻结");
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";

            return true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<FargoGlobalNPC>(mod).TimeFrozen = true;
        }
    }
}