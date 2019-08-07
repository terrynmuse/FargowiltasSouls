using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShroomiteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Enchantment");

            string tooltip = 
@"'Made with real shrooms!'
Not moving puts you in stealth
While in stealth, crits deal 2x damage
";
            string tooltip_ch =
@"'真的是用蘑菇做的!'
站立不动时潜行
潜行时,暴击造成4倍伤害
";

            if(thorium != null)
            {
                tooltip += "Attacks may inflict Fungal Growth\n";
                tooltip_ch += "攻击概率造成真菌寄生效果";
            }

            tooltip += "Summons a pet Truffle";
            tooltip_ch += "召唤一个小蘑菇人";

            Tooltip.SetDefault(tooltip); 
            DisplayName.AddTranslation(GameCulture.Chinese, "蘑菇魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
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
            player.GetModPlayer<FargoPlayer>(mod).ShroomiteEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyShroomHead");
            recipe.AddIngredient(ItemID.ShroomiteBreastplate);
            recipe.AddIngredient(ItemID.ShroomiteLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "FungusEnchant");
                recipe.AddIngredient(ItemID.MushroomSpear);
                recipe.AddIngredient(thorium.ItemType("MyceliumGattlingPulser"));
                recipe.AddIngredient(ItemID.Uzi);
                recipe.AddIngredient(ItemID.TacticalShotgun);
                recipe.AddIngredient(thorium.ItemType("RedFragmentBlaster"));
            }
            else
            {
                recipe.AddIngredient(ItemID.MushroomSpear);
                recipe.AddIngredient(ItemID.Uzi);
                recipe.AddIngredient(ItemID.TacticalShotgun);
            }
            
            recipe.AddIngredient(ItemID.StrangeGlowingMushroom);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
