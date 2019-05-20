using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ShroomiteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shroomite Enchantment");

            string tooltip = 
@"'Made with real shrooms!'
Not moving puts you in stealth
While in stealth, crits deal 4x damage
";

            if(thorium != null)
            {
                tooltip += "Attacks may inflict Fungal Growth\n";
            }

            tooltip += "Summons a pet Truffle";

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
            player.GetModPlayer<FargoPlayer>(mod).ShroomiteEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyShroomHead");
            recipe.AddIngredient(ItemID.ShroomiteBreastplate);
            recipe.AddIngredient(ItemID.ShroomiteLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "FungusEnchant");
                recipe.AddIngredient(ItemID.MushroomSpear);
                recipe.AddIngredient(thorium.ItemType("MyceliumGattlingPulser"));
                recipe.AddIngredient(ItemID.Uzi);
                recipe.AddIngredient(ItemID.TacticalShotgun);
                recipe.AddIngredient(thorium.ItemType("RedFragmentBlaster"));
            }
            else
            {
                recipe.AddIngredient(ItemID.MushroomSpear);
                recipe.AddIngredient(ItemID.Uzi);
                recipe.AddIngredient(ItemID.TacticalShotgun);
            }
            
            recipe.AddIngredient(ItemID.StrangeGlowingMushroom);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
