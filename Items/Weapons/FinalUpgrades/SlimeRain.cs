using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.FinalUpgrades
{
    public class SlimeRain : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Rain");
            Tooltip.SetDefault("The King's innards spread across the land..");
            DisplayName.AddTranslation(GameCulture.Chinese, "史莱姆雨");
            Tooltip.AddTranslation(GameCulture.Chinese, "史莱姆王的内腑撒得遍地都是..");
        }

        public override void SetDefaults()
        {
            item.damage = 300;
            item.melee = true;
            item.width = 72;
            item.height = 90;
            item.useTime = 10;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.melee = true;
            item.knockBack = 6;
            item.value = Item.sellPrice(0, 70);
            item.rare = 11;
            item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("SlimeBall");
            item.shootSpeed = 16f;
            item.useAnimation = 12;
            item.useTime = 4;
            item.reuseDelay = 14;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = new Color(0, Main.DiscoG, 255);
                }
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slimed, 180);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockBack)
        {
            float x;
            float y = player.Center.Y - Main.rand.Next(500, 701);
            for (int i = 0; i < 5; i++)
            {
                x = player.Center.X + 2f * Main.rand.Next(-400, 401);
                int p = Projectile.NewProjectile(x, y, Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(15f, 20f), type, damage, knockBack, player.whoAmI);
                if (p < 1000)
                    Main.projectile[p].timeLeft = 60;
            }
            return false;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod.ItemType("SlimeSword"), 10);
            recipe.AddIngredient(mod.ItemType("Sadism"), 15);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}