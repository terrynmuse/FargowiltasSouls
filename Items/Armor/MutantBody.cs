using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MutantBody : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Mutant Body");
            Tooltip.SetDefault(@"70% increased damage and 30% increased critical strike chance
Increases max life and mana by 200
Increases damage reduction by 30%
Drastically increases life regen");
            DisplayName.AddTranslation(GameCulture.Chinese, "真·突变之躯");
            Tooltip.AddTranslation(GameCulture.Chinese, @"增加70%伤害和30%暴击率
增加200最大生命和法力值
增加50%伤害抗性
极大提升生命回复");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = 11;
            item.value = Item.sellPrice(0, 70);
            item.defense = 70;
        }

        public override void UpdateEquip(Player player)
        {
            const float damageUp = 0.7f;
            const int critUp = 30;
            player.meleeDamage += damageUp;
            player.rangedDamage += damageUp;
            player.magicDamage += damageUp;
            player.thrownDamage += damageUp;
            player.minionDamage += damageUp;
            player.meleeCrit += critUp;
            player.rangedCrit += critUp;
            player.magicCrit += critUp;
            player.thrownCrit += critUp;

            player.statLifeMax2 += 200;
            player.statManaMax2 += 200;

            player.endurance += 0.3f;

            player.lifeRegen += 7;
            player.lifeRegenCount += 7;
            player.lifeRegenTime += 7;
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

        public override void AddRecipes()
        {
            if (Fargowiltas.Instance.FargowiltasLoaded)
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("MutantBody"));
                recipe.AddIngredient(null, "MutantScale", 15);
                recipe.AddIngredient(null, "Sadism", 15);
                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
