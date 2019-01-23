using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class TheBigSting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Big Sting");
            Tooltip.SetDefault("'The reward for slaughtering many..'");
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
            item.UseSound = new LegacySoundStyle(4, 13);
            item.value = 50000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = ProjectileID.HornetStinger;
            //item.useAmmo = ItemID.Stinger;
            item.shootSpeed = 20f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile p = Projectile.NewProjectileDirect(new Vector2(position.X, position.Y), new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            p.ranged = true;
            p.minion = false;

            return false;
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-10, 0);
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "QueenStinger");
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerBee"));
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}