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
All projectiles gain 5 pierce
Your attacks deal increasing damage to low HP enemies
You ignore enemy knockback immunity with all weapons
No enemy is ever safe from you
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
            player.pickSpeed -= 0.5f;
            modPlayer.MinerEnchant = true;

            if (Soulcheck.GetValue("Spelunker Buff"))
            {
                player.findTreasure = true;
            }

            if (Soulcheck.GetValue("Hunter Buff"))
            {
                player.detectCreature = true;
            }

            if (Soulcheck.GetValue("Dangersense Buff"))
            {
                player.dangerSense = true;
            }

            if (Soulcheck.GetValue("Shine Buff"))
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }

            modPlayer.GoldEnchant = true;
            //gold ring
            player.goldRing = true;
            //lucky coin
            player.coins = true;
            //discount card
            player.discount = true;
            //extra loot
            modPlayer.PlatinumEnchant = true;
            //speed up
            modPlayer.GladEnchant = true;
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
            //increase dmg to low HP and super bleed
            modPlayer.RedEnchant = true;
            player.setSquireT2 = true;
            player.setSquireT3 = true;
            //knockback and immune memes
            modPlayer.ValhallaEnchant = true;
            player.shinyStone = true;
            modPlayer.AddPet("Magic Lantern Pet", BuffID.MagicLantern, ProjectileID.MagicLantern);
            modPlayer.AddPet("Parrot Pet", BuffID.PetParrot, ProjectileID.Parrot);
            modPlayer.AddPet("Mini Minotaur Pet", BuffID.MiniMinotaur, ProjectileID.MiniMinotaur);
            modPlayer.AddPet("Puppy Pet", BuffID.Puppy, ProjectileID.Puppy);
            modPlayer.AddPet("Dragon Pet", BuffID.PetDD2Dragon, ProjectileID.DD2PetDragon);
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