using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class FlightMasterySoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flight Mastery Soul");
            Tooltip.SetDefault(
@"'Ascend'
Allows for very long lasting flight");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(56, 134, 255));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.wingTimeMax = 2000;
            player.ignoreWater = true;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.25f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 18f;
            acceleration *= 3.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AngelWings);
            recipe.AddIngredient(ItemID.HarpyWings);
            recipe.AddIngredient(ItemID.BoneWings);
            recipe.AddIngredient(ItemID.LeafWings);
            recipe.AddIngredient(ItemID.FrozenWings);
            recipe.AddIngredient(ItemID.FlameWings);
            recipe.AddIngredient(ItemID.TatteredFairyWings);
            recipe.AddIngredient(ItemID.FestiveWings);
            recipe.AddIngredient(ItemID.BetsyWings);
            recipe.AddIngredient(ItemID.FishronWings);
            recipe.AddIngredient(ItemID.WingsStardust);
            recipe.AddIngredient(ItemID.WingsVortex);
            recipe.AddIngredient(ItemID.WingsNebula);
            recipe.AddIngredient(ItemID.WingsSolar);
            
            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
            /*
            TerrariumWings
            DridersGrace
            DemonBloodWings
            DreadWings
            HarbingerWings
            TitanWings
            PhonicWings
            
            }*/
            

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
