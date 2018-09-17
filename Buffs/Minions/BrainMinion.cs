using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Minions
{
    public class BrainMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Brain of Cthulhu");
            Description.SetDefault("The mini Brain of Cthulhu will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("BrainProj")] > 0) modPlayer.BrainMinion = true;
            if (!modPlayer.BrainMinion)
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
            else
            {
                player.buffTime[buffIndex] = 18000;
            }
        }
    }
}