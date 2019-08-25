using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using Terraria.GameInput;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TikiEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private int actualMinions;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tiki Enchantment");
            Tooltip.SetDefault(
@"'Aku Aku!'
You may continue to summon temporary minions after maxing out on your slots
Summons a pet Tiki Spirit");
            DisplayName.AddTranslation(GameCulture.Chinese, "提基魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'Aku Aku!'
召唤数量达到上限后, 仍然可以召唤临时召唤物
召唤提基之灵");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).TikiEffect(hideVisual);

            actualMinions = player.maxMinions + 1; //the free one is not counted
            player.maxMinions = 100;

            if (player.numMinions >= actualMinions)
            {
                player.GetModPlayer<FargoPlayer>(mod).TikiMinion = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TikiMask);
            recipe.AddIngredient(ItemID.TikiShirt);
            recipe.AddIngredient(ItemID.TikiPants);
            recipe.AddIngredient(ItemID.PygmyNecklace);
            recipe.AddIngredient(ItemID.PygmyStaff);
            recipe.AddIngredient(ItemID.Blowgun);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("HexWand"));
                recipe.AddIngredient(thorium.ItemType("TheIncubator"));
                recipe.AddIngredient(ItemID.GoldFrog);
            }
            
            recipe.AddIngredient(ItemID.TikiTotem);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
