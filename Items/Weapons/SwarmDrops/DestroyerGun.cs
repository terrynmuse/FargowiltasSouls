using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class DestroyerGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destroyer Gun");
            Tooltip.SetDefault("''");
        }

        public override void SetDefaults()
        {
            item.damage = 1;
            item.ranged = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = new LegacySoundStyle(4, 13);
            item.value = 50000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = 1;
            item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerHead"), damage, knockBack, player.whoAmI, 0f, 0f);

            int previous = 0;

            for (int i = 0; i < 10; i++)
            {
                current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerBody"), damage, knockBack, player.whoAmI, current, 0f);
                previous = current;
            }

            current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerTail"), damage, knockBack, player.whoAmI, current, 0f);

            Main.projectile[previous].localAI[1] = current;
            Main.projectile[previous].netUpdate = true;

            return false;
        }
    }
}