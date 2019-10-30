using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class MinerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner Enchantment");
            Tooltip.SetDefault(
@"'The planet trembles with each swing of your pick'
50% increased mining speed
Shows the location of enemies, traps, and treasures
Light is emitted from the player
Summons a pet Magic Lantern");
            DisplayName.AddTranslation(GameCulture.Chinese, "矿工魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你每挥一下镐子, 行星都会震动'
增加50%采掘速度
显示敌人, 陷阱和宝藏
照亮周围
召唤一个魔法灯笼");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(95, 117, 151);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().MinerEffect(hideVisual, .5f);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MiningHelmet);
            recipe.AddIngredient(ItemID.MiningShirt);
            recipe.AddIngredient(ItemID.MiningPants);
            recipe.AddIngredient(ItemID.CopperPickaxe);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("aSandstonePickaxe"));
                recipe.AddIngredient(ItemID.CnadyCanePickaxe); //gj 
                recipe.AddRecipeGroup("FargowiltasSouls:AnyGoldPickaxe");
                recipe.AddIngredient(thorium.ItemType("EnforcedThoriumPax"));
                recipe.AddIngredient(ItemID.MoltenPickaxe);
            }
            else
            {
                recipe.AddIngredient(ItemID.CnadyCanePickaxe);
                recipe.AddIngredient(ItemID.GoldPickaxe);
                recipe.AddIngredient(ItemID.MoltenPickaxe);
            }
            
            recipe.AddIngredient(ItemID.MagicLantern);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
