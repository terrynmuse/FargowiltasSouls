using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WillForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Will");

            string tooltip =
@"''
Increases coin pickup range and shops have lower prices
Hitting enemies will sometimes drop extra coins
Your attacks inflict Midas
10% chance for enemies to drop 4x loot
If the enemy has Midas, the chance and bonus is doubled
Shot projectiles will speed up drastically over time
Attacks may cause enemies to Super Bleed
Celestial Shell effects
All attacks will slowly remove enemy knockback immunity
Shiny Stone effects
Summons several pets";

            /*if (thorium != null)
            {
                tooltip +=
@"Summons a curious bag of ancient coins
Summons some living glitter to follow you around";
            }
            else
            {*/
                //tooltip += "Summons a pet Parrot";
            //}

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
            

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //proof of avarice
            thoriumPlayer.avarice2 = true;*/
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