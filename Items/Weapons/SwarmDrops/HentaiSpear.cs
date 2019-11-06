using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class HentaiSpear : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Penetrator");
            Tooltip.SetDefault("'The reward for slaughtering many...'");
            DisplayName.AddTranslation(GameCulture.Chinese, "洞察者");
            Tooltip.AddTranslation(GameCulture.Chinese, "'屠戮众多的奖励...'");
        }

        public override void SetDefaults()
        {
            item.damage = 1700;
            item.useStyle = 5;
            item.useAnimation = 16;
            item.useTime = 16;
            item.shootSpeed = 3.7f;
            item.knockBack = 7f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("HentaiSpear");
            item.value = Item.sellPrice(0, 70);
            item.noMelee = true; // Important because the spear is acutally a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.melee = true;
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2) //right click
            {
                item.useAnimation = 32;
                item.useTime = 32;
            }
            else
            {
                item.useAnimation = 16;
                item.useTime = 16;
            }
            return player.ownedProjectileCounts[item.shoot] < 1; // This is to ensure the spear doesn't bug out when using autoReuse = true
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, 255, Main.DiscoB);
                }
            }
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.altFunctionUse == 2) //right click
            {
                Vector2 speed = new Vector2(speedX, speedY);
                speed.Normalize();
                speed *= 3.7f * 32 / player.itemAnimationMax;
                speedX = speed.X;
                speedY = speed.Y;
                return true;
            }

            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, item.shoot, damage, knockBack, item.owner, 0f, 1f);
            Projectile.NewProjectile(position.X, position.Y, speedX * 5f, speedY * 5f, mod.ProjectileType("Dash"), damage, knockBack, player.whoAmI);
            return false;
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargowiltasLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerMoon"));
                recipe.AddIngredient(mod.ItemType("Sadism"), 15);

                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}