using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CrimsonEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crimson Enchantment");
            Tooltip.SetDefault(
@"'The blood of your enemy is your rebirth'
Greatly increases life regen
Summons a pet Face Monster and Crimson Heart");
            DisplayName.AddTranslation(GameCulture.Chinese, "血腥魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你从敌人的血中重生'
大幅度增加生命回复速度
召唤巨脸怪宝宝和血腥心脏");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).CrimsonEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimsonHelmet);
            recipe.AddIngredient(ItemID.CrimsonScalemail);
            recipe.AddIngredient(ItemID.CrimsonGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.BloodLustCluster);
                recipe.AddIngredient(ItemID.TheRottedFork);
                recipe.AddIngredient(thorium.ItemType("CrimtaneTomahawk"), 300);
            }
            
            recipe.AddIngredient(ItemID.TheUndertaker);
            recipe.AddIngredient(ItemID.TheMeatball);
            recipe.AddIngredient(ItemID.BoneRattle);
            recipe.AddIngredient(ItemID.CrimsonHeart);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
