using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
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
            Tooltip.SetDefault(@"35% increased damage and critical strike chance
Increases max life and mana by 200
Increases damage reduction by 20%
Your attacks inflict God Eater");
            DisplayName.AddTranslation(GameCulture.Chinese, "真·突变之躯");
            Tooltip.AddTranslation(GameCulture.Chinese, @"增加35%伤害和暴击率
增加200最大生命和法力值
增加20%伤害减免
攻击造成噬神者效果");
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
            const float damageUp = 0.35f;
            const int critUp = 35;
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

            player.endurance += 0.2f;
            
            player.GetModPlayer<FargoPlayer>().GodEaterImbue = true;
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
            if (Fargowiltas.Instance.FargosLoaded)
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
