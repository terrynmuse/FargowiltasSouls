using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class GolemTome : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Landslide");
            Tooltip.SetDefault("'The crumbling remains of a defeated foe..'");
            DisplayName.AddTranslation(GameCulture.Chinese, "山崩");
            Tooltip.AddTranslation(GameCulture.Chinese, "'被击败的敌人的破碎残骸'");
        }

        public override void SetDefaults()
        {
            item.damage = 77;//
            item.magic = true;
            item.width = 24;
            item.height = 28;
            item.useTime = 8;//
            item.useAnimation = 8;//
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = 100000;//
            item.rare = 8;//
            item.mana = 10;//
            item.UseSound = SoundID.Item21;//
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GolemGib1");
            item.shootSpeed = 12f;//
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            float shootspeed = item.shootSpeed;
            int dmg = item.damage;
            float kb = item.knockBack;
            kb = player.GetWeaponKnockback(item, kb);
            player.itemTime = item.useTime;
            Vector2 vector2 = player.RotatedRelativePoint(player.MountedCenter);
            Vector2.UnitX.RotatedBy(player.fullRotation);
            float num78 = Main.mouseX + Main.screenPosition.X - vector2.X;
            float num79 = Main.mouseY + Main.screenPosition.Y - vector2.Y;
            if (player.gravDir == -1f) num79 = Main.screenPosition.Y + Main.screenHeight - Main.mouseY - vector2.Y;

            float num80 = (float)Math.Sqrt(num78 * num78 + num79 * num79);

            if (float.IsNaN(num78) && float.IsNaN(num79) || num78 == 0f && num79 == 0f)
            {
                num78 = player.direction;
                num79 = 0f;
                num80 = shootspeed;
            }
            else
            {
                num80 = shootspeed / num80;
            }

            num78 *= num80;
            num79 *= num80;
            int num146 = 2;
            if (Main.rand.Next(2) == 0) num146++;

            if (Main.rand.Next(4) == 0) num146++;

            if (Main.rand.Next(8) == 0) num146++;

            if (Main.rand.Next(16) == 0) num146++;

            for (int num147 = 0; num147 < num146; num147++)
            {
                float num148 = num78;
                float num149 = num79;
                float num150 = 0.05f * num147;
                num148 += Main.rand.Next(-25, 26) * num150;
                num149 += Main.rand.Next(-25, 26) * num150;
                num80 = (float)Math.Sqrt(num148 * num148 + num149 * num149);
                num80 = shootspeed / num80;
                num148 *= num80;
                num149 *= num80;
                float x4 = vector2.X;
                float y4 = vector2.Y;

                String gibstring = "GolemGib" + (Main.rand.Next(11) + 1);
                int gib = mod.ProjectileType(gibstring);

                Projectile.NewProjectile(position.X, position.Y, num148, num149, gib, dmg, kb, Main.myPlayer);
            }

            return false;
        }
    }
}
