using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class NukeFishron : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nuke Fishron");
            Tooltip.SetDefault("'The highly weaponized remains of a defeated foe...'");
            DisplayName.AddTranslation(GameCulture.Chinese, "核子猪鲨");
            Tooltip.AddTranslation(GameCulture.Chinese, "'高度武器化的遗骸...'");
        }

        public override void SetDefaults()
        {
            item.damage = 370;
            item.ranged = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 17;
            item.useAnimation = 17;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 7.7f;
            item.UseSound = new LegacySoundStyle(2, 62);
            item.useAmmo = AmmoID.Rocket;
            item.value = Item.sellPrice(0, 70);
            item.rare = 11;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FishNuke");
            item.shootSpeed = 7.7f;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(Main.DiscoR, 51, 255 - (int)(Main.DiscoR * 0.4));
                }
            }
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 speed = new Vector2(speedX, speedY).RotatedBy((Main.rand.NextDouble() - 0.5) * MathHelper.ToRadians(15));
            Projectile.NewProjectile(position, speed, item.shoot, damage, knockBack, player.whoAmI, -1f, 0f);
            return false;
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(mod.ItemType("FishStick"), 10);
                recipe.AddIngredient(mod.ItemType("CyclonicFin"), 5);
                recipe.AddIngredient(mod.ItemType("Sadism"), 30);
                recipe.AddIngredient(ItemID.ShrimpyTruffle);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerFish"));

                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}