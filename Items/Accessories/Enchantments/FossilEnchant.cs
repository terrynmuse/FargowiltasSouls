using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FossilEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fossil Enchantment");
            Tooltip.SetDefault(
@"'Beyond a forgotten age'
If you reach zero HP you cheat death, returning with 20 HP
For a few seconds after reviving, you are immune to all damage and spawn bones
Summons a pet Baby Dino");
            DisplayName.AddTranslation(GameCulture.Chinese, "化石魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'被遗忘的记忆'
血量为0时避免死亡, 以20点生命值重生
在复活后的几秒钟内, 免疫所有伤害, 并且可以产生骨头
召唤一只小恐龙");
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
            player.GetModPlayer<FargoPlayer>(mod).FossilEffect(10, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FossilHelm);
            recipe.AddIngredient(ItemID.FossilShirt);
            recipe.AddIngredient(ItemID.FossilPants);
            recipe.AddIngredient(ItemID.AntlionClaw);
            recipe.AddIngredient(ItemID.AmberStaff);
            recipe.AddIngredient(ItemID.BoneDagger, 300);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.BoneJavelin, 300);
                recipe.AddIngredient(thorium.ItemType("SeveredHand"), 300);
                recipe.AddIngredient(thorium.ItemType("Sitar"));
            }
            
            recipe.AddIngredient(ItemID.AmberMosquito);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
