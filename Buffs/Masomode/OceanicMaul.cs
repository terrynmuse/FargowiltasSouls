using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class OceanicMaul : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Oceanic Maul");
            Description.SetDefault("Defensive stats and max life are savaged");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
            DisplayName.AddTranslation(GameCulture.Chinese, "海洋重击");
            Description.AddTranslation(GameCulture.Chinese, "降低防御力和最大生命值");
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>(mod).OceanicMaul = true;
            player.GetModPlayer<FargoPlayer>(mod).CurseoftheMoon = true;
            player.bleed = true;
            player.onFrostBurn = true;
            player.statDefense -= 30;
            player.endurance -= 0.3f;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.FargoGlobalNPC>(mod).OceanicMaul = true;
            npc.GetGlobalNPC<NPCs.FargoGlobalNPC>(mod).CurseoftheMoon = true;
            npc.onFrostBurn = true;
        }
    }
}