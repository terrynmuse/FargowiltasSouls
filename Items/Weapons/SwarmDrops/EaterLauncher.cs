using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class EaterLauncher : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rockeater Launcher");
            Tooltip.SetDefault("'The reward for slaughtering many..'");
            DisplayName.AddTranslation(GameCulture.Chinese, "吞噬者发射器");
            Tooltip.AddTranslation(GameCulture.Chinese, "'屠戮众多的奖励..'");
        }

        public override void SetDefaults()
        {
            item.damage = 40; //
            item.ranged = true;
            item.width = 24; 
            item.height = 24; 
            item.useTime = 20; 
            item.useAnimation = 20; 
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 5f; 
            item.UseSound = new LegacySoundStyle(2, 62); 
            item.useAmmo = AmmoID.Rocket;
            item.value = Item.sellPrice(0, 10); 
            item.rare = 4; 
            item.autoReuse = true; 
            item.shoot = mod.ProjectileType("EaterRocket");
            item.shootSpeed = 16f; 
            item.scale = .7f;
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, -2);
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            type = mod.ProjectileType("EaterRocket");
            return true;
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);

                recipe.AddIngredient(mod.ItemType("EaterStaff"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerWorm"));

                recipe.AddTile(TileID.MythrilAnvil);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}