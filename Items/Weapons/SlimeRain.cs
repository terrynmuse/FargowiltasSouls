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
            item.melee = true;
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

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slimed, 180);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            float x;
            float y = player.Center.Y - 400f;

            for (int i = 0; i < 5; i++)
            {
                x = player.Center.X + 2f * Main.rand.Next(-400, 401);
                Projectile p = Projectile.NewProjectileDirect(new Vector2(x, y), new Vector2(Main.rand.Next(-4, 4), 12f), type, damage, knockBack, player.whoAmI);
                p.timeLeft = 60;
            }

            return false;
        }
    }
}