using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Masomode
{
    public class Fused : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Fused");
            Description.SetDefault("A bomb is gonna go off soon in you...");
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
            player.GetModPlayer<FargoPlayer>().Fused = true;

            if (player.buffTime[buffIndex] < 3) Projectile.NewProjectile(player.position, player.velocity * 0, mod.ProjectileType("FusionBomb"), 150, 4f);
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return true;
        }
    }
}