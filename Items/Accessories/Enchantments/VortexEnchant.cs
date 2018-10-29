using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class VortexEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vortex Enchantment");
            Tooltip.SetDefault(
                @"'Tear into reality'
Double tap down to toggle stealth, reducing chance for enemies to target you but slowing movement
You also spawn a vortex to draw in and massively damage enemies when you enter stealth
Summons a Companion Cube Pet");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 400000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).VortexEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.VortexHelmet);
            recipe.AddIngredient(ItemID.VortexBreastplate);
            recipe.AddIngredient(ItemID.VortexLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(ItemID.WingsVortex);
                recipe.AddIngredient(thorium.ItemType("VoidLance"));
                recipe.AddIngredient(thorium.ItemType("BlackBow"));
            }
            
            recipe.AddIngredient(ItemID.VortexBeater);
            recipe.AddIngredient(ItemID.Phantasm);
            recipe.AddIngredient(ItemID.SDMG);
            recipe.AddIngredient(ItemID.CompanionCube);

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
