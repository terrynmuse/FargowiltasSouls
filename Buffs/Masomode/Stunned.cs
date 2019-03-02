using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Stunned : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Stunned");
            Description.SetDefault("You're too dizzy to move");
            Main.buffNoSave[Type] = true;
            canBeCleared = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.controlLeft = false;
            player.controlRight = false;
            player.controlJump = false;
            player.controlDown = false;
            player.controlUseItem = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            if (!npc.boss)
            {
                npc.velocity.X *= 0;
                npc.velocity.Y *= 0;
                npc.frameCounter = 0;
            }
        }
    }
}