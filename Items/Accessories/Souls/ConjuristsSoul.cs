using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ConjuristsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conjurist's Soul");

            string tooltip =
@"'An army at your disposal'
30% increased summon damage
Increases your max number of minions by 4
Increases your max number of sentries by 2
Increased minion knockback";

            if (thorium != null)
            {
                tooltip += "\nEffects of Phylactery, Crystal Scorpion, and Yuma's Pendant";
            }

            Tooltip.SetDefault(tooltip);
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
                    tooltipLine.overrideColor = new Color?(new Color(0, 255, 255));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.3f;
            player.maxMinions += 4;
            player.maxTurrets += 2;
            player.minionKB += 3f;

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //phylactery
            if (!thoriumPlayer.lichPrevent)
            {
                player.AddBuff(thorium.BuffType("LichActive"), 60, true);
            }
            //crystal scorpion
            thoriumPlayer.crystalScorpion = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("CrystalScorpionMinion")] > 0)
            {
                thoriumPlayer.flatSummonDamage += 3;
            }
            //yumas pendant
            thoriumPlayer.yuma = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OccultistsEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("Phylactery"));
                recipe.AddIngredient(thorium.ItemType("CrystalScorpion"));
                recipe.AddIngredient(ItemID.PapyrusScarab);
                recipe.AddIngredient(thorium.ItemType("YumasPendant"));
                recipe.AddIngredient(thorium.ItemType("ButterflyStaff5"));
                recipe.AddIngredient(thorium.ItemType("HailBomber"));
                recipe.AddIngredient(ItemID.PirateStaff);
                recipe.AddIngredient(ItemID.OpticStaff);
                recipe.AddIngredient(thorium.ItemType("TrueSilversBlade"));
                recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
                recipe.AddIngredient(ItemID.RavenStaff);
                recipe.AddIngredient(ItemID.MoonlordTurretStaff);
            }
            else
            {
                recipe.AddIngredient(ItemID.PapyrusScarab);
                recipe.AddIngredient(ItemID.PirateStaff);
                recipe.AddIngredient(ItemID.OpticStaff);
                recipe.AddIngredient(ItemID.DeadlySphereStaff);
                recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper);
                recipe.AddIngredient(ItemID.TempestStaff);
                recipe.AddIngredient(ItemID.RavenStaff);
                recipe.AddIngredient(ItemID.XenoStaff);
                recipe.AddIngredient(ItemID.MoonlordTurretStaff);
            }

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}