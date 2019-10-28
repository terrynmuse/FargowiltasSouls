using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class WoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wood Enchantment");
            Tooltip.SetDefault(
@"'Humble beginnings…'
Critters have massively increased defense
Grants immunity to Guilty
When critters die, they release their souls to aid you");
            DisplayName.AddTranslation(GameCulture.Chinese, "木魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'卑微的开始...'
大幅增加动物防御力
杀死动物不会再获得内疚Debuff
动物死后,释放它们的灵魂来帮助你");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(151, 107, 75);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().WoodEnchant = true;
            player.buffImmune[mod.BuffType("Guilty")] = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.WoodHelmet);
            recipe.AddIngredient(ItemID.WoodBreastplate);
            recipe.AddIngredient(ItemID.WoodGreaves);
            recipe.AddIngredient(ItemID.LivingWoodWand);
            recipe.AddIngredient(ItemID.Bunny);
            recipe.AddIngredient(ItemID.Squirrel);
            recipe.AddIngredient(ItemID.Bird);        

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
