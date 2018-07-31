using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
	public class Flipped : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Flipped");
			Description.SetDefault("Your gravity has reversed.");
			Main.buffNoSave[Type] = true;
			canBeCleared = true;
			Main.debuff[Type] = true;
		}

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderDebuff";

            return true;
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