using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WillForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Will");

            string tooltip =
@"''
Increases coin pickup range and shops have lower prices
Hitting enemies will sometimes drop extra coins
Your attacks inflict Midas and Super Bleed
10% chance for enemies to drop 4x loot
If the enemy has Midas, the chance and bonus is doubled
Shot projectiles will speed up drastically over time
All attacks will slowly remove enemy knockback immunity
Effects of Celestial Shell and Shiny Stone effects
";

            if (thorium != null)
            {
                tooltip += "Effects of Proof of Avarice\n";
            }

            tooltip += "Summons several pets";

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //makes speed up for all, super bleed on all, knockback remove for all
            modPlayer.WillForce = true; 
            //midas, greedy ring, pet
            modPlayer.GoldEffect(hideVisual);
            //loot multiply
            modPlayer.PlatinumEnchant = true;
            //speed up and pets
            modPlayer.GladiatorEffect(hideVisual);
            //super bleed, pet
            modPlayer.RedRidingEffect(hideVisual);
            //celestial shell
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            //knockback kill, pet
            modPlayer.ValhallaEffect(hideVisual);
            player.shinyStone = true;
            

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);
        }

        public void Thorium(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            //proof of avarice
            thoriumPlayer.avarice2 = true;

            modPlayer.AddPet("Coin Bag Pet", hideVisual, thorium.BuffType("DrachmaBuff"), thorium.ProjectileType("DrachmaBag"));
            modPlayer.AddPet("Glitter Pet", hideVisual, thorium.BuffType("ShineDust"), thorium.ProjectileType("ShinyPet"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldEnchant");
            recipe.AddIngredient(null, "PlatinumEnchant");
            recipe.AddIngredient(null, "GladiatorEnchant");
            recipe.AddIngredient(null, "RedRidingEnchant");
            recipe.AddIngredient(null, "ValhallaKnightEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}