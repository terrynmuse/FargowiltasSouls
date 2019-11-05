using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class TheBigSting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Big Sting");
            Tooltip.SetDefault("'The reward for slaughtering many..'");
            DisplayName.AddTranslation(GameCulture.Chinese, "大螫刺");
            Tooltip.AddTranslation(GameCulture.Chinese, "'屠戮众多的奖励..'");
        }

        public override void SetDefaults()
        {
            item.damage = 48;
            item.ranged = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 8;
            item.useAnimation = 8;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.value = 50000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = ProjectileID.HornetStinger;
            //item.useAmmo = ItemID.Stinger;
            item.shootSpeed = 20f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //tsunami code
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float num = 0.314159274f;
            int numShots = 2;
            Vector2 vel = new Vector2(speedX, speedY);
            vel.Normalize();
            vel *= 40f;
            bool collide = Collision.CanHit(vector, 0, 0, vector + vel, 0, 0);

            for (int i = 0; i < numShots; i++)
            {
                float num3 = (float)i - ((float)numShots - 1f) / 2f;
                Vector2 value = Utils.RotatedBy(vel, (num * num3), default(Vector2));

                if (!collide)
                {
                    value -= vel;
                }

                int p = Projectile.NewProjectile(vector.X + value.X, vector.Y + value.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
                if (p < 1000)
                {
                    Main.projectile[p].ranged = true;
                    Main.projectile[p].minion = false;
                }
            }

            return false;
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        /*public override bool ConsumeAmmo(Player p)
        {
            return Main.rand.Next(4) != 0;
        }*/

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargowiltasLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "HiveStaff");
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerBee"));
                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}