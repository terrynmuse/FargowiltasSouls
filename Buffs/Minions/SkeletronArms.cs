using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Buffs.Minions
{
    public class SkeletronArms : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Skeletron Arms");
            Description.SetDefault("The Skeletron arms will protect you");
            Main.buffNoTimeDisplay[Type] = true;
            Main.buffNoSave[Type] = true;
        }

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "FargowiltasSouls/Buffs/PlaceholderBuff";
            return true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<FargoPlayer>().SkeletronArms = true;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.ownedProjectileCounts[mod.ProjectileType("SkeletronArmL")] < 1)
                    Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("SkeletronArmL"), 0, 8f, player.whoAmI);
                //if (player.ownedProjectileCounts[mod.ProjectileType("SkeletronArmR")] < 1)
                    //Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType("SkeletronArmR"), 0, 8f, player.whoAmI);
            }
        }
    }
}