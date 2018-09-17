using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class Bonezone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Bone Zone");
            Tooltip.SetDefault("'The shattered remains of a defeated foe..'");
        }

        public override void SetDefaults()
        {
            item.damage = 18;
            item.ranged = true;
            item.width = 54;
            item.height = 14;
            item.useTime = 15;
            item.useAnimation = 15; // must be the same^
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = SoundID.Item2;
            item.value = 50000;
            item.rare = 3;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Bonez");
            item.shootSpeed = 5.5f;
            item.useAmmo = ItemID.Bone;
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-30, 4);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int rand = Main.rand.Next(3);
            int shoot = 0;

            if (rand == 0)
                shoot = ProjectileID.ClothiersCurse;
            else
                shoot = mod.ProjectileType("Bonez");

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, shoot, damage, knockBack, player.whoAmI);

            return false;
        }
    }
}