using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class DarkStarCannon : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dark Star Cannon");
            Tooltip.SetDefault("'Modified from the arm of a defeated foe..'");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.StarCannon);
            item.damage = 70;
            item.useTime = 8;
            item.useAnimation = 8;
            item.shootSpeed = 15f;
            item.value = 100000;
            item.rare = 5;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 0);
        }

        public override bool ConsumeAmmo(Player player)
        {
            return Main.rand.Next(2) == 0;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("DarkStarFriendly");
            return base.Shoot(player, ref position, ref speedX, ref speedY, ref type, ref damage, ref knockBack);
        }


    }
}
