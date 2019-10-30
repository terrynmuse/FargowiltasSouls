using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class OccultistsEssence : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Occultist's Essence");
            Tooltip.SetDefault(
@"'This is only the beginning..'
18% increased summon damage
Increases your max number of minions by 1
Increases your max number of sentries by 1");
            DisplayName.AddTranslation(GameCulture.Chinese, "术士精华");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'这才刚刚开始..'
增加18%召唤伤害
+1最大召唤栏
+1最大哨兵栏");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(0, 255, 255));
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 150000;
            item.rare = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.18f;
            player.maxMinions += 1;
            player.maxTurrets += 1;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ItemID.SummonerEmblem);
                recipe.AddIngredient(thorium.ItemType("RosySlimeStaff"));
                recipe.AddIngredient(thorium.ItemType("HatchlingStaff"));
                recipe.AddIngredient(thorium.ItemType("MeatBallStaff"));
                recipe.AddIngredient(thorium.ItemType("AmberMinion"));
                recipe.AddIngredient(thorium.ItemType("MeteorStaff"));
                recipe.AddIngredient(thorium.ItemType("NanoClamCane"));
                recipe.AddIngredient(thorium.ItemType("ViscountCane"));
                recipe.AddIngredient(ItemID.HornetStaff);
                recipe.AddIngredient(ItemID.ImpStaff);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT1Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT1Popper);
            }
            else
            {
                //no others
                recipe.AddIngredient(ItemID.SummonerEmblem);
                recipe.AddIngredient(ItemID.SlimeStaff);
                recipe.AddIngredient(ItemID.HornetStaff);
                recipe.AddIngredient(ItemID.ImpStaff);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT1Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT1Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT1Popper);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
