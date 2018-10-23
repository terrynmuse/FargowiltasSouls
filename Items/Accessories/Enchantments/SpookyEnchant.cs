using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpookyEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spooky Enchantment");
            Tooltip.SetDefault(
                @"'Melting souls since 1902'
All of your minions may occasionally spew massive scythes everywhere
Summons a Cursed Sapling and an eyeball spring");
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
            player.GetModPlayer<FargoPlayer>(mod).SpookyEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpookyHelmet);
            recipe.AddIngredient(ItemID.SpookyBreastplate);
            recipe.AddIngredient(ItemID.SpookyLeggings);
            recipe.AddIngredient(ItemID.DemonScythe);
            recipe.AddIngredient(ItemID.DeathSickle);
            recipe.AddIngredient(ItemID.CursedSapling);
            recipe.AddIngredient(ItemID.EyeSpring);
            
            /*
PagansGrasp
BeholderStaff
PhantomWand
            */
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
