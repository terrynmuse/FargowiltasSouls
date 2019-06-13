using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Ammos
{
    public class FargoArrow : ModItem
    {
        Mod fargos = ModLoader.GetMod("Fargowiltas");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("Fargowiltas") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Amalgamated Arrow Quiver");
            Tooltip.SetDefault("Bounces several times\n" +
                "Each impact explodes, summons falling stars, and fires laser arrows\n" +
                "Inflicts several debuffs");
        }

        public override void SetDefaults()
        {
            item.damage = 45;
            item.ranged = true;
            item.width = 26;
            item.height = 26;
            item.knockBack = 8f; //same as hellfire
            item.rare = 10;
            item.shoot = mod.ProjectileType("FargoArrowProj");
            item.shootSpeed = 6.5f; // same as hellfire arrow          
            item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.FargosLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            //recipe.AddIngredient(ItemID.EndlessQuiver);
            recipe.AddIngredient(fargos, "FlameQuiver");
            recipe.AddIngredient(fargos, "FrostburnQuiver");
            recipe.AddIngredient(fargos, "UnholyQuiver");
            recipe.AddIngredient(fargos, "BoneQuiver");
            recipe.AddIngredient(fargos, "JesterQuiver");
            recipe.AddIngredient(fargos, "HellfireQuiver");
            recipe.AddIngredient(fargos, "CursedQuiver");
            recipe.AddIngredient(fargos, "IchorQuiver");
            recipe.AddIngredient(fargos, "HolyQuiver");
            recipe.AddIngredient(fargos, "VenomQuiver");
            recipe.AddIngredient(fargos, "ChlorophyteQuiver");
            recipe.AddIngredient(fargos, "LuminiteQuiver");
            recipe.AddIngredient(mod.ItemType("Sadism"));
            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}