using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class GladiatorEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gladiator Enchantment");
            Tooltip.SetDefault(
@"'Are you not entertained?'
Spears will rain down on struck enemies 
Summons a pet Minotaur");
            DisplayName.AddTranslation(GameCulture.Chinese, "角斗士魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你难道不高兴吗?'
长矛将倾泄在被攻击的敌人身上
召唤一个小牛头人");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).GladiatorEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GladiatorHelmet);
            recipe.AddIngredient(ItemID.GladiatorBreastplate);
            recipe.AddIngredient(ItemID.GladiatorLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.Javelin, 300);
                recipe.AddIngredient(thorium.ItemType("SteelBattleAxe"), 300);
                recipe.AddIngredient(thorium.ItemType("GoblinWarSpear"), 300);
                recipe.AddIngredient(thorium.ItemType("BronzeGladius"));
                recipe.AddIngredient(thorium.ItemType("GorganGazeStaff"));
                recipe.AddIngredient(thorium.ItemType("RodAsclepius"));
            }
            else
            {
                recipe.AddIngredient(ItemID.Javelin, 300);
                recipe.AddIngredient(ItemID.BoneJavelin, 300);
                recipe.AddIngredient(ItemID.AngelStatue);
            }
            
            recipe.AddIngredient(ItemID.TartarSauce);
 
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
