using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class HentaiSpearThrown : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Weapons/SwarmDrops/HentaiSpear";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Penetrator");
            Tooltip.SetDefault("'The reward for slaughtering many...'");
            DisplayName.AddTranslation(GameCulture.Chinese, "洞察者");
            Tooltip.AddTranslation(GameCulture.Chinese, "'屠戮众多的奖励...'");
        }

        public override void SetDefaults()
        {
            item.damage = 750;
            item.useStyle = 5;
            item.useAnimation = 60;
            item.useTime = 60;
            item.shootSpeed = 25f;
            item.knockBack = 7f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 11;
            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType("HentaiSpearThrown");
            item.value = Item.sellPrice(0, 70);
            item.noMelee = true; // Important because the spear is acutally a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.thrown = true;
            item.autoReuse = true;
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

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargosLoaded)
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