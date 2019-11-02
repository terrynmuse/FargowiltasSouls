using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class SteelEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Enchantment");
            Tooltip.SetDefault(
@"'Expertly forged by the Blacksmith'
33% damage reduction at Full HP
Effects of Spiked Bracers");
            DisplayName.AddTranslation(GameCulture.Chinese, "钢魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'铁匠精心打造'
满血时增加33%伤害减免
拥有尖刺索的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //steel effect
            if (player.statLife == player.statLifeMax2)
            {
                player.endurance += .33f;
            }
            
            //spiked bracers
            player.thorns += 0.25f;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("SteelHelmet"));
            recipe.AddIngredient(thorium.ItemType("SteelChestplate"));
            recipe.AddIngredient(thorium.ItemType("SteelGreaves"));
            recipe.AddIngredient(thorium.ItemType("SpikedBracer"));
            recipe.AddIngredient(ItemID.Katana);
            recipe.AddIngredient(thorium.ItemType("SteelAxe"));
            recipe.AddIngredient(thorium.ItemType("SteelMallet"));
            recipe.AddIngredient(thorium.ItemType("SteelBlade"));
            recipe.AddIngredient(thorium.ItemType("WarForger"));
            recipe.AddIngredient(thorium.ItemType("SuperAnvil"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
