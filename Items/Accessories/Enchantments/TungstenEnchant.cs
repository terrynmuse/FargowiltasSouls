using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TungstenEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Enchantment");
            Tooltip.SetDefault(
                @"'Juggernaut'
Your weapons shoot at 1/8 the speed
300% increased damage
25% increased crit chance");

//While in combat, you generate a 15 life shield
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
            player.GetModPlayer<FargoPlayer>().TungstenEffect(3);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenHelmet);
            recipe.AddIngredient(ItemID.TungstenChainmail);
            recipe.AddIngredient(ItemID.TungstenGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("TungstenBulwark"));
                recipe.AddIngredient(ItemID.TungstenHammer);
                recipe.AddIngredient(ItemID.EmeraldStaff);
                recipe.AddIngredient(ItemID.GreenPhaseblade);
                recipe.AddIngredient(ItemID.Snail);
                recipe.AddIngredient(ItemID.Sluggy);
                recipe.AddIngredient(thorium.ItemType("EmeraldButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.TungstenHammer);
                recipe.AddIngredient(ItemID.EmeraldStaff);
                recipe.AddIngredient(ItemID.Snail);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
