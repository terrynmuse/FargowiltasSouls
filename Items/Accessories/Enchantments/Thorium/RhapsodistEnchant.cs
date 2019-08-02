using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class RhapsodistEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rhapsodist Enchantment");
            Tooltip.SetDefault(
@"'Allow your song to inspire an army, Prove to all that your talent is second to none'
Inspiration notes that drop will become more potent
Additionally, they give a random level 1 empowerment to all nearby allies
Pressing the 'Special Ability' key will grant you infinite inspiration and increased symphonic damage and playing speed
It also overloads all nearby allies with every empowerment III for 15 seconds
These effects needs to recharge for 1 minute");
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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //notes heal more and give random empowerments
            thoriumPlayer.inspirator = true;
            //hotkey buff allies 
            thoriumPlayer.rallySet = true;
            //hotkey buff self
            thoriumPlayer.soloistSet = true;
        }
        
        private readonly string[] items =
        {
            "SoloistHat",
            "RallyHat",
            "RhapsodistChestWoofer",
            "RhapsodistBoots",
            "MusicSheet6",
            "SirensAllure",
            "TerrariumAutoharp",
            "Sousaphone",
            "EdgeofImagination",
            "BlackMIDI"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
