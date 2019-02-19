using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ValhallaKnightEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valhalla Knight Enchantment");
            Tooltip.SetDefault(
@"'Valhalla calls'
Swords and spears will slowly remove enemy knockback immunity
Greatly enhances Ballista effectiveness
Effects of Shiny Stone
Summons a pet Dragon");
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
            player.GetModPlayer<FargoPlayer>(mod).ValhallaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SquireAltHead);
            recipe.AddIngredient(ItemID.SquireAltShirt);
            recipe.AddIngredient(ItemID.SquireAltPants);
            recipe.AddIngredient(ItemID.SquireShield);
            recipe.AddIngredient(ItemID.ShinyStone);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("BlobhornCoralStaff"));
                recipe.AddIngredient(ItemID.DD2BallistraTowerT2Popper);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
                recipe.AddIngredient(thorium.ItemType("CloudwingButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
            }
            
            recipe.AddIngredient(ItemID.DD2PetDragon);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
