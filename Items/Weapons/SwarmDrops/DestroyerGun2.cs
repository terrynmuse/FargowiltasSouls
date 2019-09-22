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
        int shootNum = 0;
        int head;
        int current;
        int previous = 0;

        public override string Texture => "FargowiltasSouls/Items/Weapons/BossDrops/DestroyerGun";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Destroyer Gun EX");
            Tooltip.SetDefault("'The reward for slaughtering many...'");
            DisplayName.AddTranslation(GameCulture.Chinese, "毁灭者之枪 EX");
            Tooltip.AddTranslation(GameCulture.Chinese, "'屠戮众多的奖励...'");
        }

        public override void SetDefaults()
        {
            item.damage = 340;
            item.mana = 10;
            item.summon = true;
            item.width = 24;
            item.height = 24;
            item.useAnimation = 40;
            item.useTime = 2;
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = new LegacySoundStyle(4, 13);
            item.value = Item.sellPrice(0, 25);
            item.rare = 11;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DestroyerHead2");
            item.shootSpeed = 18f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            //looks kinda weird but prob less buggy :ech: we'll see
            /*int current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerHead2"), damage, 0f, player.whoAmI);
            for (int i = 0; i < 18; i++)
                current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerBody2"), damage, 0f, player.whoAmI, current);
            int previous = current;
            current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerTail2"), damage, 0f, player.whoAmI, current);
            Main.projectile[previous].localAI[1] = current;
            Main.projectile[previous].netUpdate = true;
            return false;*/

            //shoot head
            if (shootNum == 0 || player.ownedProjectileCounts[mod.ProjectileType("DestroyerHead2")] == 0)
            {
                current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerHead2"), damage, knockBack, player.whoAmI, 0f, 0f);
                head = current;

                shootNum++;
            }
            //shoot tail
            else if (shootNum == 19)
            {
                current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerTail2"), damage, knockBack, player.whoAmI, current, 0f);
                Main.projectile[current].timeLeft = Main.projectile[head].timeLeft;
                Main.projectile[previous].localAI[1] = current;
                Main.projectile[previous].netUpdate = true;

                shootNum = 0;
            }
            //shoot body
            else
            {
                current = Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DestroyerBody2"), damage, knockBack, player.whoAmI, current, 0f);
                Main.projectile[current].timeLeft = Main.projectile[head].timeLeft;

                previous = current;

                shootNum++;
            }

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