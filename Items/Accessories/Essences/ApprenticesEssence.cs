using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class ApprenticesEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Apprentice's Essence");
            Tooltip.SetDefault(
                @"'This is only the beginning..'
18% increased magic damage
5% increased magic crit
Increases your maximum mana by 50");
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
            player.magicDamage += .18f;
            player.magicCrit += 5;
            player.statManaMax2 += 50;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ItemID.SorcererEmblem);
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddIngredient(ItemID.Vilethorn);
                recipe.AddIngredient(ItemID.CrimsonRod);
                recipe.AddIngredient(ItemID.WaterBolt);
                recipe.AddIngredient(ItemID.BookofSkulls);
                recipe.AddIngredient(ItemID.MagicMissile);
                recipe.AddIngredient(ItemID.Flamelash);

                /*
                 * magik staff
                 * magic conch
                 * grave buster
                 * dark tome
                 * */
            }
            else
            {
                recipe.AddIngredient(ItemID.SorcererEmblem);
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddIngredient(ItemID.Vilethorn);
                recipe.AddIngredient(ItemID.CrimsonRod);
                recipe.AddIngredient(ItemID.WaterBolt);
                recipe.AddIngredient(ItemID.BookofSkulls);
                recipe.AddIngredient(ItemID.MagicMissile);
                recipe.AddIngredient(ItemID.Flamelash);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}