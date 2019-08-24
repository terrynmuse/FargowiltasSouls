using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class DestroyerGun2 : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Weapons/BossDrops/DestroyerGun";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destroyer Gun EX");
            Tooltip.SetDefault("'text'");
        }

        public override void SetDefaults()
        {
            item.damage = 340;
            item.mana = 10;
            item.summon = true;
            item.width = 24;
            item.height = 24;
            item.useAnimation = 50;
            item.useTime = 50;
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = new LegacySoundStyle(4, 13);
            item.value = Item.sellPrice(0, 25);
            item.rare = 11;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DestroyerHead");
            item.shootSpeed = 18f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerHead2"), damage, 0f, player.whoAmI);
            for (int i = 0; i < 18; i++)
                current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerBody2"), damage, 0f, player.whoAmI, current);
            int previous = current;
            current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerTail2"), damage, 0f, player.whoAmI, current);
            Main.projectile[previous].localAI[1] = current;
            Main.projectile[previous].netUpdate = true;
            return false;
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "DestroyerGun");
                recipe.AddIngredient(null, "MutantScale", 10);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerDestroyer"));
                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}