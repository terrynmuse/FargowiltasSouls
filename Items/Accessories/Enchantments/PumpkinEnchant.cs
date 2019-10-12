using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PumpkinEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pumpkin Enchantment");
            Tooltip.SetDefault(
@"'Your sudden pumpkin craving will never be satisfied'
You leave behind a trail of fire when you walk
Eating Pumpkin Pie heals you to full HP and inflicts Potion Sickness for 3 minutes
Summons a pet Squashling");
            DisplayName.AddTranslation(GameCulture.Chinese, "南瓜魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你对南瓜的突发渴望永远不会得到满足'
走路时会留下一道火焰路径
南瓜派会使你回满血, 并获得3分钟的抗药性
召唤一个宠物南瓜娃娃");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().PumpkinEffect(12, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PumpkinHelmet);
            recipe.AddIngredient(ItemID.PumpkinBreastplate);
            recipe.AddIngredient(ItemID.PumpkinLeggings);
            recipe.AddIngredient(ItemID.MolotovCocktail, 50);
            recipe.AddIngredient(ItemID.Sickle);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("BentZombieArm"));
                recipe.AddIngredient(ItemID.BladedGlove);
                recipe.AddIngredient(ItemID.GoodMorning);
            }

            recipe.AddIngredient(ItemID.PumpkinPie);
            recipe.AddIngredient(ItemID.MagicalPumpkinSeed);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
