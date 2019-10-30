using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class AdamantiteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Adamantite Enchantment");
            Tooltip.SetDefault(
@"'Who needs to aim?'
Every 8th projectile you shoot will split into 3
Any secondary projectiles may also split"); 
            DisplayName.AddTranslation(GameCulture.Chinese, "精金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'谁需要瞄准?'
第8个抛射物将会分裂成3个
分裂出的抛射物同样可以分裂");

        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(221, 85, 125);
                }
            }
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
            player.GetModPlayer<FargoPlayer>().AdamantiteEnchant = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyAdamHead");
            recipe.AddIngredient(ItemID.AdamantiteBreastplate);
            recipe.AddIngredient(ItemID.AdamantiteLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("AdamantiteStaff"));
                recipe.AddIngredient(ItemID.CrystalSerpent);
                recipe.AddIngredient(thorium.ItemType("DynastyWarFan"));
                recipe.AddIngredient(thorium.ItemType("Scorn"));
                recipe.AddIngredient(thorium.ItemType("OgreSnotGun"));
                recipe.AddIngredient(thorium.ItemType("MidasMallet"));
            }
            else
            {
                recipe.AddIngredient(ItemID.AdamantiteGlaive);
                recipe.AddIngredient(ItemID.TitaniumTrident);
                recipe.AddIngredient(ItemID.CrystalSerpent);
            }
            
            recipe.AddIngredient(ItemID.VenomStaff);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
