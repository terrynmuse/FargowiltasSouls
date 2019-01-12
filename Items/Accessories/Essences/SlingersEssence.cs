using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Essences
{
    public class SlingersEssence : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod fargos = ModLoader.GetMod("Fargowiltas");

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

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("NinjaEmblem"));
                recipe.AddIngredient(thorium.ItemType("DesertWindRune"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("WoodenYoyoThrown") : ItemID.WoodYoyo);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BloodyMacheteThrown") : ItemID.BloodyMachete);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("IceBoomerangThrown") : ItemID.IceBoomerang);
                recipe.AddIngredient(thorium.ItemType("GoblinWarSpear"), 300);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("TheMeatballThrown") : ItemID.TheMeatball);
                recipe.AddIngredient(thorium.ItemType("StarfishSlicer"), 300);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("ThornChakramThrown") : ItemID.ThornChakram);
                recipe.AddIngredient(ItemID.BoneGlove);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BlueMoonThrown") : ItemID.BlueMoon);
                recipe.AddIngredient(thorium.ItemType("ChampionsGodHand"));
                recipe.AddIngredient(thorium.ItemType("GaussKnife"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("FlamarangThrown") : ItemID.Flamarang);
            }
            else
            {
                //no others
                recipe.AddIngredient(fargos != null ? fargos.ItemType("WoodenYoyoThrown") : ItemID.WoodYoyo);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BloodyMacheteThrown") : ItemID.BloodyMachete);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("IceBoomerangThrown") : ItemID.IceBoomerang);
                recipe.AddIngredient(ItemID.MolotovCocktail, 99);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("TheMeatballThrown") : ItemID.TheMeatball);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("ThornChakramThrown") : ItemID.ThornChakram);
                recipe.AddIngredient(ItemID.BoneGlove);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BlueMoonThrown") : ItemID.BlueMoon);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("FlamarangThrown") : ItemID.Flamarang);
            }

            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
