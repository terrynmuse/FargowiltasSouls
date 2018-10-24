using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class IronEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Iron Enchantment");
            Tooltip.SetDefault(
                @"'Strike while the iron is hot'
Right Click to guard with your shield
Your shield will also protect you from projectiles
You attract items from a much larger range and fall 5 times as quickly");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 40000;
            //item.shieldSlot = 1;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).IronEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.IronHelmet);
            recipe.AddIngredient(ItemID.IronChainmail);
            recipe.AddIngredient(ItemID.IronGreaves);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("IronShield"));
                recipe.AddIngredient(thorium.ItemType("ThoriumShield"));
                recipe.AddIngredient(ItemID.EoCShield);
                recipe.AddIngredient(ItemID.IronBroadsword);
                recipe.AddIngredient(thorium.ItemType("OpalStaff"));
                recipe.AddIngredient(ItemID.IronAnvil);
                recipe.AddIngredient(thorium.ItemType("OpalButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.EoCShield);
                recipe.AddIngredient(ItemID.IronBroadsword);
                recipe.AddIngredient(ItemID.IronAnvil);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
