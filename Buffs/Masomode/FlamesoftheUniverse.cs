using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
	public class FlamesoftheUniverse : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Flames of the Universe");
			Description.SetDefault("The heavens themselves have judged you.");
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
           //activates various vanilla debuffs
		   player.GetModPlayer<FargoPlayer>(mod).FlamesoftheUniverse = true;
		   player.GetModPlayer<FargoPlayer>(mod).Shadowflame = true;
		   player.onFire = true;
		   player.onFire2 = true;
		   player.onFrostBurn = true;
		   player.burned = true;
		   player.ichor = true;
           player.venom = true;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.onFire = true;
            npc.onFire2 = true;
            npc.shadowFlame = true;
            npc.onFrostBurn = true;
            npc.ichor = true;
            npc.venom = true;
        }
    }
}
