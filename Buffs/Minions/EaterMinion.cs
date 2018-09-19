using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Minions
{
    public class EaterMinion : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Eater of Worlds");
            Description.SetDefault("The mini Eater of Worlds will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            if (player.ownedProjectileCounts[mod.ProjectileType("EaterHead")] > 0) modPlayer.EaterMinion = true;
            if (!modPlayer.EaterMinion)
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