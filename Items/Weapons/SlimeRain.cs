using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons
{
    public class SlimeRain : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The King's innards spread across the land..");
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.melee = true;
            item.width = 72;
            item.height = 90;
            item.useTime = 10;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.melee = true; //so the item's animation doesn't do damage
            item.knockBack = 6;
            item.value = 10000;
            item.rare = 10;
            item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SlimeBall");
            item.shootSpeed = 16f;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = 14;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, -0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            for (int index = 0; index < 10; ++index)
            {
                Vector2 vector21 = new Vector2(
                    (float) (player.position.X + player.width * 0.5 + Main.rand.Next(201) * -player.direction +
                             (Main.mouseX + (double) Main.screenPosition.X - player.position.X)),
                    (float) (player.position.Y + player.height * 0.5 -
                             600.0)); //this defines the projectile width, direction and position
                vector21.X = (float) ((vector21.X + (double) player.Center.X) / 2.0) + Main.rand.Next(-1000, 1001);
                vector21.Y -= 100 * index;
                float num12 = Main.mouseX + Main.screenPosition.X - vector21.X;
                float num13 = Main.mouseY - Main.screenPosition.Y - vector21.Y;
                if (num13 < 0.0) num13 *= -1f;
                if (num13 < 20.0) num13 = 20f;
                float num14 = (float) Math.Sqrt(num12 * (double) num12 + num13 * (double) num13);
                float num15 = item.shootSpeed / num14;
                float num16 = num12 * num15;
                float num17 = num13 * num15;
                float xspeed =
                    num16 + Main.rand.Next(-40, 41) *
                    0.02f; //this defines the projectile X position speed and randomnes
                float yspeed =
                    num17 + Main.rand.Next(-40, 41) *
                    0.02f; //this defines the projectile Y position speed and randomnes
                Projectile.NewProjectile(vector21.X, vector21.Y, xspeed, yspeed, type, damage, knockBack, Main.myPlayer,
                    0.0f, Main.rand.Next(5));
            }

            return false;
        }
    }
}