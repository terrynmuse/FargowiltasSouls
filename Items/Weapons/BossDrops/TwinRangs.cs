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
            DisplayName.SetDefault("The Twins");
            Tooltip.SetDefault("The compressed forms of defeated foes..");
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.melee = true;
            item.width = 30;
            item.height = 30;
            item.useTime = 25;
            item.useAnimation = 25;
            item.noUseGraphic = true;
            item.useStyle = 1;
            item.knockBack = 3;
            item.value = 100000;
            item.rare = 5;
            item.shootSpeed = 20;
            item.shoot = 1;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (shoot == 0)
            {
                type = mod.ProjectileType("Retirang");
                shoot = 1;
            }
            else
            {
                type = mod.ProjectileType("Spazmarang");
                shoot = 0;
            }

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);

            return false;
        }
    }
}