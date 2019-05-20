using CalamityMod;
using CalamityMod.Items.CalamityCustomThrowingDamage;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.HandsOn, EquipType.HandsOff)]
    public class OlympiansSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod fargos = ModLoader.GetMod("Fargowiltas");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Olympian's Soul");

            string tooltip =
@"'Strike with deadly precision'
30% increased throwing damage
20% increased throwing speed
15% increased throwing critical chance and velocity";

            if (thorium != null)
            {
                tooltip += "Effects of Guide to Expert Throwing - Volume III, Mermaid's Canteen, and Deadman's Patch";
            }

            if (calamity != null)
            {
                tooltip += "\nEffects of Nanotech\nBonuses also effect rogue damage";
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
                    tooltipLine.overrideColor = new Color?(new Color(85, 5, 230));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //throw speed
            player.GetModPlayer<FargoPlayer>(mod).ThrowSoul = true;
            player.thrownDamage += 0.3f;
            player.thrownCrit += 15;
            player.thrownVelocity += 0.15f;

            if (Fargowiltas.Instance.CalamityLoaded) Thorium(player);

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.throwGuide2 = true;
            //dead mans patch
            thoriumPlayer.deadEyeBool = true;
            //mermaid canteen
            thoriumPlayer.canteenEffect += 750;
            thoriumPlayer.canteenCadet = true;
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            modPlayer.nanotech = true;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingDamage += 0.3f;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingCrit += 15;
            CalamityCustomThrowingDamagePlayer.ModPlayer(player).throwingVelocity += 0.15f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SlingersEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(Fargowiltas.Instance.CalamityLoaded ? calamity.ItemType("Nanotech") : thorium.ItemType("MagnetoGrip"));
                recipe.AddIngredient(thorium.ItemType("ThrowingGuideVolume3"));
                recipe.AddIngredient(thorium.ItemType("MermaidCanteen"));
                recipe.AddIngredient(thorium.ItemType("DeadEyePatch"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BananarangThrown") : ItemID.Bananarang, 5);
                recipe.AddIngredient(thorium.ItemType("HotPot"));
                recipe.AddIngredient(thorium.ItemType("VoltTomahawk"));
                recipe.AddIngredient(thorium.ItemType("SparkTaser"));
                recipe.AddIngredient(thorium.ItemType("PharaohsSlab"));
                recipe.AddIngredient(thorium.ItemType("TerraKnife"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("VampireKnivesThrown") : ItemID.VampireKnives);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("PaladinsHammerThrown") : ItemID.PaladinsHammer);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("TerrarianThrown") : ItemID.Terrarian);
            }
            else
            {
                if(Fargowiltas.Instance.CalamityLoaded)
                    recipe.AddIngredient( calamity.ItemType("Nanotech"));
                else
                    recipe.AddIngredient(fargos != null ? fargos.ItemType("ChikThrown") : ItemID.Chik);

                recipe.AddIngredient(fargos != null ? fargos.ItemType("MagicDaggerThrown") : ItemID.MagicDagger);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BananarangThrown") : ItemID.Bananarang, 5);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("AmarokThrown") : ItemID.Amarok);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("ShadowflameKnifeThrown") : ItemID.ShadowFlameKnife);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("FlyingKnifeThrown") : ItemID.FlyingKnife);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("LightDiscThrown") : ItemID.LightDisc, 5);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("FlowerPowThrown") : ItemID.FlowerPow);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("ToxicFlaskThrown") : ItemID.ToxicFlask);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("VampireKnivesThrown") : ItemID.VampireKnives);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("PaladinsHammerThrown") : ItemID.PaladinsHammer);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("PossessedHatchetThrown") : ItemID.PossessedHatchet);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("TerrarianThrown") : ItemID.Terrarian);
            }

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
