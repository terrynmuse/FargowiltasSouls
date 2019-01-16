using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class FlippedHallow : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Flipped");
            Description.SetDefault("Your gravity is reversed");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
            canBeCleared = false;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.gravControl = true;
            player.controlUp = false;
            player.gravDir = -1f;
            //player.fallStart = (int)(player.position.Y / 16f);
            //player.jump = 0;
        }
    }
}