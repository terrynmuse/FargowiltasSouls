using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WillForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Will");
            Tooltip.SetDefault(
@"''
50% increased mining speed
Shows the location of enemies, traps, and treasures
You emit an aura of light
Picking up gold coins gives you extra life regen or movement speed, you will throw away any lesser valued coins you pick up
Increases coin pickup range, shops have lower prices, Hitting enemies will sometimes drop extra coins
Your attacks inflict Midas and may cause Super Bleed
20% chance for enemies to drop 6x loot
All projectiles will speed up drastically over time
Greatly enhances Explosive Traps and Ballista effectiveness
Celestial Shell and Shiny Stone effects
Your attacks deal increasing damage to low HP enemies
You ignore enemy knockback immunity with all weapons
Summons a pet Magic Lantern, Parrot, Minotaur, Puppy, and Dragon");
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

        public override string Texture
        {
            get
            {
                return "FargowiltasSouls/Items/Placeholder";
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.WillForce = true;
            modPlayer.MinerEffect(hideVisual, .5f);
            modPlayer.GoldEffect(hideVisual);
            modPlayer.PlatinumEnchant = true;
            modPlayer.GladiatorEffect(hideVisual);
            modPlayer.RedRidingEffect(hideVisual);
            modPlayer.ValhallaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "MinerEnchant");
            recipe.AddIngredient(null, "GoldEnchant");
            recipe.AddIngredient(null, "PlatinumEnchant");
            recipe.AddIngredient(null, "GladiatorEnchant");
            recipe.AddIngredient(null, "RedRidingEnchant");
            recipe.AddIngredient(null, "ValhallaKnightEnchant");

            if (Fargowiltas.Instance.FargosLoaded)
            {
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            }
            else
            {
                recipe.AddTile(TileID.LunarCraftingStation);
            }

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}