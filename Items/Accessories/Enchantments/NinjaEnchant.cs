using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class NinjaEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Enchantment");
            Tooltip.SetDefault(
@"'Now you see me, now you don’t'
Throw a smoke bomb to teleport to it
Standing nearby smoke gives you the First Strike buff
First Strike doubles your crit chance and you have a chance to dodge attacks
Summons a pet Black Cat");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).NinjaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NinjaHood);
            recipe.AddIngredient(ItemID.NinjaShirt);
            recipe.AddIngredient(ItemID.NinjaPants);
            recipe.AddIngredient(ItemID.ThrowingKnife, 300);
            recipe.AddIngredient(ItemID.Shuriken, 300);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {   
                recipe.AddIngredient(ItemID.StarAnise, 300);
                recipe.AddIngredient(ItemID.SmokeBomb, 50);
                recipe.AddIngredient(thorium.ItemType("Scorpain"));
                recipe.AddIngredient(thorium.ItemType("AmphibianEgg"));
            }
            else
            {
                recipe.AddIngredient(ItemID.SmokeBomb, 50);
            }
            
            recipe.AddIngredient(ItemID.UnluckyYarn);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
