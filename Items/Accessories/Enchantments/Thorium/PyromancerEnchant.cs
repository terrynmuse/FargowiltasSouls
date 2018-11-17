using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class PyromancerEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        
        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pyromancer Enchantment");
            Tooltip.SetDefault(
@"'Your magma fortified army's molten gaze shall be feared'
Magic damage will heavily burn and damage all adjacent enemies
Pressing the 'Special Ability' key will unleash an echo of Slag Fury's power");
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

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(255, 128, 0));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            PyroEffect(player);
        }
        
        private void PyroEffect(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //pyro magic set
            thoriumPlayer.pyro = true;
            thoriumPlayer.pyroSet = true;
            //pyro summon bonus
            thoriumPlayer.napalmSet = true;
            Lighting.AddLight(player.position, 0.5f, 0.35f, 0f);
        }
        
        private readonly string[] items =
        {
            "PyroSummonHat",
            "PyromancerCowl",
            "PyromancerTabard",
            "PyromancerLeggings",
            "StalagmiteBook",
            "DevilDagger",
            "TrueSilversBlade",
            "AncientFlame",
            "MoltenBanner",
            "DevilsClaw"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            //stardust, nebula
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
