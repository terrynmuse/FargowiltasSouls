using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
	public class MutantNibble : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Mutant Nibble");
			Description.SetDefault("You cannot heal at all.");
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
			//disables potions, moon bite effect, feral bite effect, disables lifesteal
			player.GetModPlayer<FargoPlayer>(mod).MutantNibble = true;
			
			player.potionDelay = player.buffTime[buffIndex];
			player.moonLeech = true;

			//feral bite stuff
			player.rabid = true;
			if (Main.rand.Next(1200) == 0)
			{
				int rng = Main.rand.Next(6);
				double duration = Main.rand.Next(60, 100) * 0.01;
				if (rng == 0)
					player.AddBuff(22, 180 * (int) duration);
				else if (rng == 1)
					player.AddBuff(23, 45 * (int) duration);
				else if (rng == 2)
					player.AddBuff(31, 90 * (int) duration);
				else if (rng == 3)
					player.AddBuff(32, 210 * (int) duration);
				else if (rng == 4)
					player.AddBuff(33, 300 * (int) duration);
				else if (rng == 5)
					player.AddBuff(35, 60 * (int) duration);
			}

			player.meleeDamage = player.meleeDamage + 0.2f;
			player.magicDamage = player.magicDamage + 0.2f;
			player.rangedDamage = player.rangedDamage + 0.2f;
			player.thrownDamage = player.thrownDamage + 0.2f;
			player.minionDamage = player.minionDamage + 0.2f;
        }
	}
}
