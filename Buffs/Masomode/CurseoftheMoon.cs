using Terraria;
using Terraria.ModLoader;

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
            npc.GetGlobalNPC<NPCs.FargoGlobalNPC>(mod).CurseoftheMoon = true;
        }
    }
}