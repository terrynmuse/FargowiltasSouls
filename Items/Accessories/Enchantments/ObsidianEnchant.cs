using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ObsidianEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Obsidian Enchantment");
            Tooltip.SetDefault(
@"'The earth calls'
Grants immunity to fire, fall damage, and 5 seconds of lava immunity
While standing in lava, you gain 20 armor penetration, 15% attack speed, and your attacks ignite enemies");
            DisplayName.AddTranslation(GameCulture.Chinese, "黑曜石魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'大地在呼唤'
免疫火焰,掉落伤害,获得5秒岩浆免疫
在岩浆中时,获得20点护甲穿透,15%攻击速度,攻击会点燃敌人");
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
            player.GetModPlayer<FargoPlayer>().ObsidianEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ObsidianHelm);
            recipe.AddIngredient(ItemID.ObsidianShirt);
            recipe.AddIngredient(ItemID.ObsidianPants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("aObsidianHelmet"));
                recipe.AddIngredient(thorium.ItemType("bObsidianChestGuard"));
                recipe.AddIngredient(thorium.ItemType("cObsidianGreaves"));
                recipe.AddIngredient(thorium.ItemType("ObsidianScale"));
                recipe.AddIngredient(ItemID.ObsidianRose);
                recipe.AddIngredient(ItemID.SharkToothNecklace);
                recipe.AddIngredient(thorium.ItemType("MagmaBlade"));
            }
            else
            {
                recipe.AddIngredient(ItemID.ObsidianRose);
                recipe.AddIngredient(ItemID.ObsidianHorseshoe);
                recipe.AddIngredient(ItemID.SharkToothNecklace);
                recipe.AddIngredient(ItemID.Fireblossom);
            }
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
