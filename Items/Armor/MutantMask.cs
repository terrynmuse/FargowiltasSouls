using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class MutantMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("True Mutant Head");
            Tooltip.SetDefault(@"50% increased damage and 20% increased critical strike chance
Increases max number of minions and sentries by 10
25% reduced mana usage
25% chance not to consume ammo");
            DisplayName.AddTranslation(GameCulture.Chinese, "真·突变之颅");
            Tooltip.AddTranslation(GameCulture.Chinese, @"增加50%伤害和20%暴击率
增加10最大召唤栏和哨兵栏
减少25%法力消耗
25%概率不消耗弹药");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.rare = 11;
            item.value = Item.sellPrice(0, 50);
            item.defense = 50;
        }

        public override void UpdateEquip(Player player)
        {
            const float damageUp = 0.5f;
            const int critUp = 20;
            player.meleeDamage += damageUp;
            player.rangedDamage += damageUp;
            player.magicDamage += damageUp;
            player.thrownDamage += damageUp;
            player.minionDamage += damageUp;
            player.meleeCrit += critUp;
            player.rangedCrit += critUp;
            player.magicCrit += critUp;
            player.thrownCrit += critUp;

            player.maxMinions += 10;
            player.maxTurrets += 10;

            player.manaCost -= 0.25f;
            player.ammoCost75 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("MutantBody") && legs.type == mod.ItemType("MutantPants");
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = @"Phantasmal Spheres shoot deathrays at nearby enemies
Abominationn's Visage fights alongside you
Your attacks inflict God Eater
You can survive fatal damage and recover with 100% life every two minutes
100% increased damage and 20% increased weapon use speed";

            player.AddBuff(mod.BuffType("MutantPower"), 2);

            player.GetModPlayer<FargoPlayer>().MutantSetBonus = true;
            player.GetModPlayer<FargoPlayer>().GodEaterImbue = true;
            player.GetModPlayer<FargoPlayer>().AttackSpeed *= 1.2f;

            const float damageUp = 1f;
            player.meleeDamage += damageUp;
            player.rangedDamage += damageUp;
            player.magicDamage += damageUp;
            player.thrownDamage += damageUp;
            player.minionDamage += damageUp;
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
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("MutantMask"));
                recipe.AddIngredient(null, "MutantScale", 10);
                recipe.AddIngredient(null, "Sadism", 10);
                recipe.AddTile(mod, "CrucibleCosmosSheet");
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}
