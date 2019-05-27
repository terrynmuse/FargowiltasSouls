using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Patreon
{
    public class RoombaPet : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Roomba");
            Tooltip.SetDefault("Summons a Roomba to follow you around in hopes of cleaning the whole world");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.shoot = mod.ProjectileType("RoombaPetProj");
            item.buffType = mod.BuffType("RoombaPetBuff");
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}