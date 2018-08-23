using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class RedRidingEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Riding Enchantment");
            Tooltip.SetDefault(
@"'Big Bad Red Riding Hood'
Greatly enhances Explosive Traps effectiveness
Celestial Shell effects
Your attacks deal increasing damage to low HP enemies
During a Full Moon, ranged attacks cause enemies to Super Bleed
Summons a Puppy");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 8;
            item.value = 250000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).RedRidingEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.HuntressAltHead);
            recipe.AddIngredient(ItemID.HuntressAltShirt);
            recipe.AddIngredient(ItemID.HuntressAltPants);
            recipe.AddIngredient(ItemID.HuntressBuckler);
            recipe.AddIngredient(ItemID.CelestialShell);
            recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
            recipe.AddIngredient(ItemID.DogWhistle);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}