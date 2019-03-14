using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using ThoriumMod.Items.Misc;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Back)]
    public class TrawlerSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trawler Soul");

            string tooltip = 
@"'The fish catch themselves'
Increases fishing skill substantially
All fishing rods will have 10 extra lures
Fishing line will never break
Decreases chance of bait consumption
Permanent Sonar and Crate Buffs";

            if (thorium != null)
            {
                tooltip += "\nAllows any fishing pole to catch loot in lava";
            }

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(0, 238, 125));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //extra lures
            modPlayer.FishSoul2 = true;
            modPlayer.AddPet("Zephyr Fish Pet", hideVisual, BuffID.ZephyrFish, ProjectileID.ZephyrFish);
            player.fishingSkill += 60;
            player.sonarPotion = true;
            player.cratePotion = true;
            player.accFishingLine = true;
            player.accTackleBox = true;
            player.accFishFinder = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            MagmaBoundFishingLineMP magmaPlayer = player.GetModPlayer<MagmaBoundFishingLineMP>();
            magmaPlayer.magmaLine = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AnglerEnchantment");
            recipe.AddIngredient(Fargowiltas.Instance.CalamityLoaded ? calamity.ItemType("SupremeBaitTackleBoxFishingStation") : ItemID.AnglerTackleBag);
            
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("MagmaBoundFishingLine"));
                recipe.AddIngredient(thorium.ItemType("AquaticSonarDevice"));
                recipe.AddIngredient(ItemID.SittingDucksFishingRod);
                recipe.AddIngredient(thorium.ItemType("CartlidgedCatcher"));
                recipe.AddIngredient(thorium.ItemType("TerrariumFisher"));
            }
            else
            {
                recipe.AddIngredient(ItemID.SittingDucksFishingRod);
                recipe.AddIngredient(ItemID.GoldenFishingRod);
            }

            recipe.AddIngredient(ItemID.FinWings);
            recipe.AddIngredient(ItemID.ReaverShark);
            recipe.AddIngredient(ItemID.Toxikarp);
            recipe.AddIngredient(ItemID.Bladetongue);
            recipe.AddIngredient(ItemID.CrystalSerpent);
            recipe.AddIngredient(ItemID.ObsidianSwordfish);
            recipe.AddIngredient(ItemID.ZephyrFish);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
