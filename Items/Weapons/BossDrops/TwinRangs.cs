using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class TwinRangs : ModItem
    {
        private int shoot;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Twin Rangs");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.damage = 30;
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 8;
            item.rare = 6;
            item.shootSpeed = 12f;
            item.shoot = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (shoot == 0)
            {
                type = ProjectileID.IceBoomerang;
                shoot = 1;
            }
            else
            {
                type = ProjectileID.EnchantedBoomerang;
                shoot = 0;
            }

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);

            return false;
        }
    }
}