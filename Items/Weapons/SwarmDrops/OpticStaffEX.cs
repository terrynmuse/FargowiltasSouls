using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class OpticStaffEX : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Optic Staff EX");
            Tooltip.SetDefault("'The reward for slaughtering many...'\nSummons the real twins to fight for you\nRequires 4 minion slots per use");
        }

        public override void SetDefaults()
        {
            item.damage = 150;
            item.mana = 10;
            item.summon = true;
            item.width = 24;
            item.height = 24;
            item.useAnimation = 37;
            item.useTime = 37;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3f;
            item.UseSound = SoundID.Item82;
            item.value = Item.sellPrice(0, 25);
            item.rare = 11;
            item.buffType = mod.BuffType("TwinsEX");
            item.shoot = mod.ProjectileType("OpticRetinazer");
            item.shootSpeed = 10f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            player.AddBuff(item.buffType, 2);
            Vector2 spawnPos = Main.MouseWorld;
            Vector2 speed = new Vector2(speedX, speedY).RotatedBy(Math.PI / 2);
            Projectile.NewProjectile(spawnPos, speed, mod.ProjectileType("OpticRetinazer"), damage, knockBack, player.whoAmI, -1);
            Projectile.NewProjectile(spawnPos, -speed, mod.ProjectileType("OpticSpazmatism"), damage, knockBack, player.whoAmI, -1);
            return false;
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargowiltasLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "TwinRangs");
                recipe.AddIngredient(null, "MutantScale", 10);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerTwins"));
                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}