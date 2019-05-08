using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PalmWoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Palm Wood Enchantment");
            Tooltip.SetDefault(
@"''
Doublte tap down to spawn a palm tree sentry that throws coconuts at enemies
While in the Ocean or Desert, it attacks much faster");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (Soulcheck.GetValue("Palm Tree Sentry") && (player.controlDown && player.releaseDown))
            {
                if (player.doubleTapCardinalTimer[0] > 0 && player.doubleTapCardinalTimer[0] != 15)
                {
                    //D U S T

                    if (player.ownedProjectileCounts[mod.ProjectileType("PalmTreeSentry")] == 0)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("PalmTreeSentry"), 15, 0f, player.whoAmI);
                    }
                }
            }

            /*
In PalmTree.cs
Tree disapears if owner is too far away
Throws coconuts, twice as fast in sandy areas (ocean and desert)*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.PalmWoodHelmet);
            recipe.AddIngredient(ItemID.PalmWoodBreastplate);
            recipe.AddIngredient(ItemID.PalmWoodGreaves);
            recipe.AddIngredient(ItemID.Trident);
            recipe.AddIngredient(ItemID.Tuna);
            recipe.AddIngredient(ItemID.Seashell);
            recipe.AddIngredient(ItemID.LimeKelp);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
