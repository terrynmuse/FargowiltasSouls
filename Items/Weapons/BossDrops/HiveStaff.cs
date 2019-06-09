using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class HiveStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hive Staff");
            Tooltip.SetDefault("'The enslaved minions of a defeated foe..'");
        }

        public override void SetDefaults()
        {
            item.damage = 15;
            item.summon = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 1;
            item.noMelee = true;
            item.UseSound = new LegacySoundStyle(4, 13);
            item.value = 50000;
            item.rare = 3;
            item.shoot = mod.ProjectileType("HiveSentry");
            item.shootSpeed = 20f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 mouse = Main.MouseWorld;

            Projectile.NewProjectile(mouse.X, mouse.Y - 10, 0f, 0f, type, damage, knockBack, player.whoAmI);

            player.UpdateMaxTurrets();

            return false;
        }
    }
}