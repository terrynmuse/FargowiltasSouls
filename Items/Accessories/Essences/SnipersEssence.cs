using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class SnipersEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sniper's Essence");
            Tooltip.SetDefault(
@"'This is only the beginning..'
18% increased ranged damage
5% increased ranged critical chance
5% increased ranged use time");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 150000;
            item.rare = 4;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).RangedEssence = true;
            player.rangedCrit += 5;
            player.rangedDamage += .18f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ItemID.RangerEmblem);
                recipe.AddIngredient(ItemID.RedRyder);
                recipe.AddIngredient(ItemID.PainterPaintballGun);
                recipe.AddIngredient(ItemID.SnowballCannon);
                recipe.AddIngredient(ItemID.Harpoon);
                recipe.AddIngredient(ItemID.Musket);
                recipe.AddIngredient(ItemID.Boomstick);
                recipe.AddIngredient(ItemID.BeesKnees);
                recipe.AddIngredient(ItemID.HellwingBow);

                /*
                 * 
                 * */
            }
            else
            {
                //no others
                recipe.AddIngredient(ItemID.RangerEmblem);
                recipe.AddIngredient(ItemID.RedRyder);
                recipe.AddIngredient(ItemID.PainterPaintballGun);
                recipe.AddIngredient(ItemID.SnowballCannon);
                recipe.AddIngredient(ItemID.Harpoon);
                recipe.AddIngredient(ItemID.Musket);
                recipe.AddIngredient(ItemID.Boomstick);
                recipe.AddIngredient(ItemID.BeesKnees);
                recipe.AddIngredient(ItemID.HellwingBow);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}