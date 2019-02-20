using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class CosmoForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Cosmos");

            string tooltip =
@"'Been around since the Big Bang'
";

            if (thorium == null)
            {
                tooltip += "A meteor shower initiates every few seconds while attacking\n";
            }

            tooltip += 
@"Solar shield allows you to dash through enemies
Attacks may inflict the Solar Flare debuff
Double tap down to toggle stealth and spawn a vortex
Hurting enemies has a chance to spawn buff boosters
Reach maxed buff boosters to gain drastically increased attack speed
Double tap down to direct your guardian
Press the Freeze Key to freeze time for 5 seconds
There is a 60 second cooldown for this effect, a sound effect plays when it's back
Summons a pet Companion Cube";

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
            //meme speed, solar flare, white dwarf flames, tide turner daggers, pyro bursts, assassin insta kill
            modPlayer.CosmoForce = true;

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                //meteor shower
                modPlayer.MeteorEffect(75);
            }

            //solar shields
            modPlayer.SolarEffect();
            //flare debuff
            modPlayer.SolarEnchant = true;
            //stealth, voids, pet
            modPlayer.VortexEffect(hideVisual);
            //boosters and meme speed
            modPlayer.NebulaEffect();
            //guardian and time freeze
            modPlayer.StardustEffect();
            modPlayer.AddPet("Suspicious Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "MeteorEnchant");
            }
            
            recipe.AddIngredient(null, "SolarEnchant");
            recipe.AddIngredient(null, "VortexEnchant");
            recipe.AddIngredient(null, "NebulaEnchant");
            recipe.AddIngredient(null, "StardustEnchant");
            recipe.AddIngredient(ItemID.SuspiciousLookingTentacle);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}