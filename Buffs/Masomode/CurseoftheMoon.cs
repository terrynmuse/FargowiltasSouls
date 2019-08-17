using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class CurseoftheMoon : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Curse of the Moon");
            Description.SetDefault("The moon's wrath consumes you");
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
            canBeCleared = true;
            DisplayName.AddTranslation(GameCulture.Chinese, "月之诅咒");
            Description.AddTranslation(GameCulture.Chinese, "月亮的愤怒吞噬了你");
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.statDefense -= 10;
            player.endurance -= 0.1f;
            player.GetModPlayer<FargoPlayer>(mod).CurseoftheMoon = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<NPCs.FargoSoulsGlobalNPC>(mod).CurseoftheMoon = true;
        }
    }
}