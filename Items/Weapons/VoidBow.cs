using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons
{
    public class VoidBow : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Void Bow");
            Tooltip.SetDefault(
                "'A glimpse to the other side' \nConverts all arrows to void arrows \n40% chance to not consume ammo");
        }

        public override void SetDefaults()
        {
            item.damage = 175;
            item.ranged = true;
            item.width = 40;
            item.height = 20;
            item.useTime = 6;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 1000;
            item.rare = 11;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("VoidArrow");
            item.shootSpeed = 12f;
            item.useAmmo = AmmoID.Arrow;
            item.crit = 25;
        }

        public override bool ConsumeAmmo(Player p)
        {
            return Main.rand.Next(5) <= 2;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            float spread = 45f * 0.0174f;
            double startAngle = Math.Atan2(speedX, speedY) - spread / 2;
            double deltaAngle = spread / 8f;
            double offsetAngle;
            int i;
            for (i = 0; i < 1; i++)
            {
                offsetAngle = startAngle + deltaAngle * (i + i * i) / 2f + 32f * i;
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("VoidArrow"),
                    damage, knockBack, player.whoAmI);
            }

            return false;
        }

        public override Vector2? HoldoutOffset()
        {
            return Vector2.Zero;
        }
    }
}