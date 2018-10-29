using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FrostEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Enchantment");
            Tooltip.SetDefault(
                @"'Let's coat the world in a deep freeze' 
Icicles will start to appear around you
When there are three, using any weapon will launch them towards the cursor, Chilling and Frostburning enemies
Allows the ability to walk on water
Summons a baby penguin and snowman");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).FrostEffect(50, hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.FrostLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("FrostSubwoofer"));
                recipe.AddIngredient(thorium.ItemType("Glacieor"));
                recipe.AddIngredient(ItemID.IceBow);
                recipe.AddIngredient(thorium.ItemType("FreezeRay"));
                recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
                recipe.AddIngredient(thorium.ItemType("FrozenButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.IceBow);
                recipe.AddIngredient(ItemID.ColdWatersintheWhiteLand);
                recipe.AddIngredient(ItemID.Fish);
            }
            
            recipe.AddIngredient(ItemID.ToySled);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
