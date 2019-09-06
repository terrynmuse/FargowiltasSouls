﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;
using SacredTools;

namespace FargowiltasSouls.Items.Accessories.Enchantments.SoA
{
    public class BlazingBruteEnchant : ModItem
    {
        private readonly Mod soa = ModLoader.GetMod("SacredTools");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("SacredTools") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blazing Brute Enchantment");
            Tooltip.SetDefault(
@"'Your spirit ignites like the brightest flame. Soon, your enemies will too'
Standing still for 5 seconds charges a shield that increases damage reduction by 25% per level (max of 4) 
Getting hit or moving resets the counter");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 350000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModdedPlayer modPlayer = player.GetModPlayer<ModdedPlayer>();

            //set bonus
            modPlayer.SolariusArmor = true;
        }

        private readonly string[] items =
        {
            "BlazingBruteHelm",
            "BlazingBrutePlate",
            "BlazingBruteLegs",
            "Nyanmere",
            "StarShower",
            "AsteroidShower",
            "OblivionSpear",
            "FlareFlail",
            "AsthralBlade",
            "Phaselash"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.SOALoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            foreach (string i in items) recipe.AddIngredient(soa.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}