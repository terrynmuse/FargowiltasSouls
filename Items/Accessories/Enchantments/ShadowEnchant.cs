using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShadowEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Enchantment");
            Tooltip.SetDefault(
                @"'You feel your body slip into the deepest of shadows'
You will recieve escalating Darkness debuffs while hitting enemies
Surrounding enemies will take rapid damage when it is the darkest
Summons a Baby Eater of Souls and a Shadow Orb");
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
            player.GetModPlayer<FargoPlayer>(mod).ShadowEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.ShadowHelmet);
            recipe.AddIngredient(ItemID.ShadowScalemail);
            recipe.AddIngredient(ItemID.ShadowGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.WarAxeoftheNight);
                recipe.AddIngredient(ItemID.BallOHurt);
                recipe.AddIngredient(ItemID.PurpleBlubberfish);
                recipe.AddIngredient(ItemID.LightlessChasms);
                recipe.AddIngredient(thorium.ItemType("CorruptButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.LightlessChasms);
            }
            
            recipe.AddIngredient(ItemID.EatersBone);
            recipe.AddIngredient(ItemID.ShadowOrb);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
