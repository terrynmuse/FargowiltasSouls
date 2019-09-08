using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class EbonwoodEnchant : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ebonwood Enchantment");
            Tooltip.SetDefault(
@"'Untapped potential'
You have an aura of Shadowflame
While in the Corruption, the radius is doubled
");
            DisplayName.AddTranslation(GameCulture.Chinese, "乌木魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'未开发的潜力'
环绕一个暗影烈焰光环
在腐地时, 半径加倍
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
