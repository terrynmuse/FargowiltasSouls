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
                tooltip += "\nEffects of The Complete Set";
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

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            Thorium(player);
        }

        private void Thorium(Player player)
        {
            //complete set
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.throwGuide4 = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SlingersEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("BoneGrip"));
                recipe.AddIngredient(thorium.ItemType("TheCompleteSet"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("BananarangThrown") : ItemID.Bananarang, 5);
                recipe.AddIngredient(thorium.ItemType("CryoFang"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("ShadowflameKnifeThrown") : ItemID.ShadowFlameKnife);
                recipe.AddIngredient(thorium.ItemType("HotPot"));
                recipe.AddIngredient(thorium.ItemType("VoltTomahawk"));
                recipe.AddIngredient(thorium.ItemType("SparkTaser"));
                recipe.AddIngredient(thorium.ItemType("PharaohsSlab"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("VampireKnivesThrown") : ItemID.VampireKnives);
                recipe.AddIngredient(fargos != null ? fargos.ItemType("PaladinsHammerThrown") : ItemID.PaladinsHammer);
                recipe.AddIngredient(thorium.ItemType("CosmicDagger"));
                recipe.AddIngredient(fargos != null ? fargos.ItemType("TerrarianThrown") : ItemID.Terrarian);
            }
            else
            {
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

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
