using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class GolemTome2 : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Weapons/BossDrops/GolemTome";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Landslide EX");
            Tooltip.SetDefault("'text'");
        }

        public override void SetDefaults()
        {
            item.damage = 160;//
            item.magic = true;
            item.width = 24;
            item.height = 28;
            item.useTime = 60;//
            item.useAnimation = 60;//
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Item.sellPrice(0, 25);
            item.rare = 11;//
            item.mana = 24;//
            item.UseSound = SoundID.Item21;//
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GolemHeadProj");
            item.shootSpeed = 20f;//
        }

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(null, "GolemTome");
                recipe.AddIngredient(null, "MutantScale", 10);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("EnergizerGolem"));
                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
