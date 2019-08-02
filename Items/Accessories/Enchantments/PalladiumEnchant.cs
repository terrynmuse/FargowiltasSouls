using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PalladiumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palladium Enchantment");
            Tooltip.SetDefault(
@"'You feel your wounds slowly healing' 
Greatly increases life regeneration after striking an enemy 
One attack gains 10% life steal every 4 seconds, capped at 8 HP");
            DisplayName.AddTranslation(GameCulture.Chinese, "钯金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你感到你的伤口在慢慢愈合'
攻击敌人后大大增加生命回复
一次攻击获得每秒5%的生命窃取,上限为5点");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).PalladiumEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyPallaHead");
            recipe.AddIngredient(ItemID.PalladiumBreastplate);
            recipe.AddIngredient(ItemID.PalladiumLeggings);
            recipe.AddIngredient(ItemID.PalladiumSword);
            recipe.AddIngredient(ItemID.PalladiumRepeater);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("PalladiumStaff"));
                recipe.AddIngredient(thorium.ItemType("eeeLifeLeech")); //um WTF
                recipe.AddIngredient(thorium.ItemType("VampireScepter"));
            }
            
            recipe.AddIngredient(ItemID.SoulDrain);
            recipe.AddIngredient(ItemID.HeartLantern, 5);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
