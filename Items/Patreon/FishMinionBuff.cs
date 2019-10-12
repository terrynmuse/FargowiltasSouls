using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Patreon
{
    public class FishMinionBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fish");
            Description.SetDefault("This fish will fight for you");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            PatreonPlayer modPlayer = player.GetModPlayer<PatreonPlayer>();
            if (player.ownedProjectileCounts[mod.ProjectileType("FishMinion")] > 0) modPlayer.FishMinion = true;
            if (!modPlayer.FishMinion)
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