using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class BeetleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Beetle Enchantment");

            string tooltip = 
@"'The unseen life of dung courses through your veins'
Beetles protect you from damage
Your wings last twice as long";

            /*if(thorium != null)
            {
                tooltip += "\nSummons a Pet Parrot";
            }*/

            Tooltip.SetDefault(tooltip);
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //defense beetle bois
            modPlayer.BeetleEffect();
            //extra wing time
            modPlayer.BeetleEnchant = true;

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            //pet
            modPlayer.AddPet("Parrot Pet", hideVisual, BuffID.PetParrot, ProjectileID.Parrot);
            modPlayer.FlightEnchant = true;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.BeetleHelmet);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyBeetle");
            recipe.AddIngredient(ItemID.BeetleLeggings);

            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "FlightEnchant");
            }*/

            recipe.AddIngredient(ItemID.BeetleWings);
            recipe.AddIngredient(ItemID.BeeWings);
            recipe.AddIngredient(ItemID.ButterflyWings);
            recipe.AddIngredient(ItemID.MothronWings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.GolemFist);
                recipe.AddIngredient(thorium.ItemType("SolScorchedSlab"));
                recipe.AddIngredient(thorium.ItemType("TempleButterfly"));
            }
              
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
