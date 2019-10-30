using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ValhallaKnightEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valhalla Knight Enchantment");
            Tooltip.SetDefault(
@"'Valhalla calls'
Attacks will slowly remove enemy knockback immunity
Greatly enhances Ballista effectiveness
Effects of Shiny Stone
Summons a pet Dragon");
            DisplayName.AddTranslation(GameCulture.Chinese, "瓦尔哈拉骑士魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'瓦尔哈拉的呼唤'
剑和矛将慢慢地移除敌人的击退免疫
大大提高弩车能力
拥有闪耀石的效果
召唤一只宠物小龙");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(147, 101, 30);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().ValhallaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SquireAltHead);
            recipe.AddIngredient(ItemID.SquireAltShirt);
            recipe.AddIngredient(ItemID.SquireAltPants);
            recipe.AddIngredient(ItemID.SquireShield);
            recipe.AddIngredient(ItemID.ShinyStone);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("BlobhornCoralStaff"));
                recipe.AddIngredient(ItemID.MonkStaffT2);
                recipe.AddIngredient(ItemID.DD2SquireBetsySword);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
            }
            else
            {
                recipe.AddIngredient(ItemID.DD2SquireBetsySword);
            }
            
            recipe.AddIngredient(ItemID.DD2PetDragon);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
