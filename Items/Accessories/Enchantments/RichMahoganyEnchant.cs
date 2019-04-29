using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class RichMahoganyEnchant : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rich Mahogany Enchantment");
            Tooltip.SetDefault(
@"''
All grappling hooks can damage enemies and have extra range
While in the Jungle, they shoot spores??
");
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
            /*
            GlobalProjectile.GrapplePullSpeed	
            massively increased grapple pull speed (bat is 16) do 25
            
            GlobalProjectile.GrappleRetreatSpeed
            and increased retreat speed, lunar is 24, do 30
            
            GraapleRange
            static is 600, maybe double whatever you have
            
            while in jungle they also are damaging? or inflict poison and shoot spores?*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RichMahoganyHelmet);
            recipe.AddIngredient(ItemID.RichMahoganyBreastplate);
            recipe.AddIngredient(ItemID.RichMahoganyGreaves);
            recipe.AddIngredient(ItemID.IvyWhip);
            recipe.AddIngredient(ItemID.Frog);
            recipe.AddIngredient(ItemID.NeonTetra);
            recipe.AddIngredient(ItemID.DoNotStepontheGrass);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
