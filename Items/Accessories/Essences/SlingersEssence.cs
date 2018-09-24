using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class SlingersEssence : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slinger's Essence");
            Tooltip.SetDefault(
                @"'This is only the beginning..'
18% increased throwing damage
5% increased throwing critical chance
5% increased throwing velocity");
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
            player.thrownDamage += 0.18f;
            player.thrownCrit += 5;
            player.thrownVelocity += 0.05f;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
                //just thorium
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("WoodenYoyoThrown"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("BloodyMacheteThrown"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("IceBoomerangThrown"));
                recipe.AddIngredient(ItemID.MolotovCocktail, 99);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("MeatballThrown"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("ThornChakramThrown"));
                recipe.AddIngredient(ItemID.BoneGlove);
                recipe.AddIngredient(ItemID.BlueMoon);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("FlamarangThrown"));

            ninja emblem
            champion god hand // champion
            granite throwing axes
            goblin war spear
            gauss flinger //saucer
            pod bomb
            severed hand
            sea ninja star


                
            }
            else if (Fargowiltas.Instance.FargosLoaded)
            {
                //no others
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("WoodenYoyoThrown"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("BloodyMacheteThrown"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("IceBoomerangThrown"));
                recipe.AddIngredient(ItemID.MolotovCocktail, 99);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("MeatballThrown"));
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("ThornChakramThrown"));
                recipe.AddIngredient(ItemID.BoneGlove);
                recipe.AddIngredient(ItemID.BlueMoon);
                recipe.AddIngredient(ModLoader.GetMod("Fargowiltas").ItemType("FlamarangThrown"));
            }
            else
            {
                recipe.AddIngredient(ItemID.BlueMoon);
            }*/

            recipe.AddIngredient(ItemID.BlueMoon);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
