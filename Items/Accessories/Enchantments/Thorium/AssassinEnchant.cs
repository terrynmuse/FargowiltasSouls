using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class AssassinEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Assassin Enchantment");
            Tooltip.SetDefault(
@"'Blacken the skies and cull the weak'
Ranged damage applies Cursed Inferno and Ichor to hit enemies
Ranged damage has a 10% chance to duplicate and become increased by 15%
Ranged damage has a 5% chance to instantly kill the enemy");
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
            //damage duplicate
            thoriumPlayer.omniArcherSet = true;
            //ichor and death arrows 2hich dont work I guess
            thoriumPlayer.omniArrowHat = true;
            //insta kill
            thoriumPlayer.omniBulletSet = true;
            //cursed flame
            thoriumPlayer.omniBulletHat = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("OmniMarkHead"));
            recipe.AddIngredient(thorium.ItemType("OmniArablastHood"));
            recipe.AddIngredient(thorium.ItemType("OmniBody"));
            recipe.AddIngredient(thorium.ItemType("OmniGreaves"));
            recipe.AddIngredient(ItemID.NailGun);
            recipe.AddIngredient(thorium.ItemType("DMR"));
            recipe.AddIngredient(thorium.ItemType("KillCounter"));
            recipe.AddIngredient(thorium.ItemType("OmniBow"));
            recipe.AddIngredient(thorium.ItemType("WyrmDecimator"));
            recipe.AddIngredient(thorium.ItemType("CelestialBow"));
            
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
