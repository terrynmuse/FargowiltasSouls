using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class OrichalcumEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Orichalcum Enchantment");
            Tooltip.SetDefault(
@"'Nature blesses you' 
Flower petals will cause extra damage to your target 
Magic attacks may spawn fireballs to rotate around you");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            if (Soulcheck.GetValue("Orichalcum Fireball"))
            {
                modPlayer.OriEnchant = true;
                player.onHitPetal = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyOriHead");
            recipe.AddIngredient(ItemID.OrichalcumBreastplate);
            recipe.AddIngredient(ItemID.OrichalcumLeggings);
            recipe.AddIngredient(ItemID.FlowerofFire);
            recipe.AddIngredient(ItemID.FlowerofFrost);
            recipe.AddIngredient(ItemID.CursedFlames);
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}