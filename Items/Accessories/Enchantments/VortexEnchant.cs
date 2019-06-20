using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class VortexEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Enchantment");
            Tooltip.SetDefault(
@"'Tear into reality'
Double tap down to toggle stealth, reducing chance for enemies to target you but slowing movement
You also spawn a vortex to draw in and massively damage enemies when you enter stealth
Summons a pet Companion Cube");
            DisplayName.AddTranslation(GameCulture.Chinese, "星旋魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'撕裂现实'
双击'下'键切换潜行,减少敌人攻击你的机会,但减慢移动速度
进入潜行状态时,会产生一个漩涡,吸引敌人并造成巨大伤害
召唤一个伙伴方块");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).VortexEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexHelmet);
            recipe.AddIngredient(ItemID.VortexBreastplate);
            recipe.AddIngredient(ItemID.VortexLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.WingsVortex);
                recipe.AddIngredient(thorium.ItemType("VoidLance"));
                recipe.AddIngredient(thorium.ItemType("BlackBow"));
            }
            
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddIngredient(ItemID.Phantasm);
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(ItemID.CompanionCube);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
