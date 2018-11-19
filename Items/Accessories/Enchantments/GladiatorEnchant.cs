using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

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
Thrown projectiles will speed up drastically over time
Summons a pet Minotaur");
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
                recipe.AddIngredient(ItemID.MarbleChest);
            }
            
            recipe.AddIngredient(ItemID.TartarSauce);
 
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
