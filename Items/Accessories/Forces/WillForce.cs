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
Shot projectiles will speed up drastically over time
Summons a pet Minotaur
Increases coin pickup range and shops have lower prices
Hitting enemies will sometimes drop extra coins
Your attacks inflict Midas
10% chance for enemies to drop 4x loot
If the enemy has Midas, the chance and bonus is doubled
Greatly enhances Explosive Traps effectiveness
Celestial Shell effects
Your attacks deal increasing damage to low HP enemies
Attacks may cause enemies to Super Bleed
Summons a Puppy
Greatly enhances Ballista effectiveness
All attacks will slowly remove enemy knockback immunity
Shiny Stone effects
Summons a pet Dragon";

            if (thorium != null)
            {
                tooltip +=
@"Summons a curious bag of ancient coins
Summons some living glitter to follow you around";
            }
            else
            {
                tooltip += "Summons a pet Parrot";
            }

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 600000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //makes speed up for all, super bleed on all, knockback remove for all
            modPlayer.WillForce = true; //check all
            //speed up and pets
            modPlayer.GladiatorEffect(hideVisual);
            //midas, greedy ring, pet
            modPlayer.GoldEffect(hideVisual);
            //loot multiply
            modPlayer.PlatinumEnchant = true;
            player.setHuntressT2 = true;
            player.setHuntressT3 = true;
            //celestial shell
            player.accMerman = true;
            player.wolfAcc = true;

            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            //super bleed, low hp dmg, pet
            modPlayer.RedRidingEffect(hideVisual);
            player.setSquireT2 = true;
            player.setSquireT3 = true;
            player.shinyStone = true;
            //knockback kill, pet
            modPlayer.ValhallaEffect(hideVisual);

            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //proof of avarice
            thoriumPlayer.avarice2 = true;
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