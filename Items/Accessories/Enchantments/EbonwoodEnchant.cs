using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class EbonwoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebonwood Enchantment");
            Tooltip.SetDefault(
@"''
You have an aura of Shadowflame
While in the Corruption, the radius is doubled
");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 10000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().EbonEffect();
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.EbonwoodHelmet);
            recipe.AddIngredient(ItemID.EbonwoodBreastplate);
            recipe.AddIngredient(ItemID.EbonwoodGreaves);
            recipe.AddIngredient(ItemID.EbonwoodSword);
            recipe.AddIngredient(ItemID.Ebonkoi);
            recipe.AddIngredient(ItemID.VileMushroom);
            recipe.AddIngredient(ItemID.LightlessChasms);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
