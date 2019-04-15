using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs
{
    public class LunarCultist : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Lunar Cultist");
            Description.SetDefault("The Lunar Cultist will protect you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().LunarCultist = true;

            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[mod.ProjectileType("LunarCultist")] < 1)
                Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("LunarCultist"), (int)(80f * player.minionDamage), 2f, player.whoAmI);
        }
    }
}