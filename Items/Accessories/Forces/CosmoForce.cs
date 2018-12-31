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

            //if(thorium == null)
            //{
                tooltip +=
@"A meteor shower initiates every few seconds while attacking
Solar shield allows you to dash through enemies
Attacks may inflict the Solar Flare debuff
Double tap down to toggle stealth and spawn a vortex
Hurting enemies has a chance to spawn buff boosters
Reach maxed buff boosters to gain drastically increased attack speed
Double tap down to direct your guardian
Press the Freeze Key to freeze time for 5 seconds
There is a 60 second cooldown for this effect, a sound effect plays when it's back
Summons a pet Companion Cube";
            /*}
            else
            {
                tooltip +=
@"A meteor shower initiates every few seconds while attacking
A bubble of energy will protect you from one attack
Solar shield allows you to dash through enemies
Attacks may inflict the Solar Flare debuff
Double tap down to toggle stealth, spawn a vortex, and direct your guardian
Hurting enemies has a chance to spawn buff boosters
Reach maxed buff boosters to gain drastically increased attack speed
Critical strikes will unleash ivory flares from the cosmos
Produces a floating globule every 5 seconds
Every globule increases your defense by 5% and makes your next attack a mini-crit
Damage has a 20% chance to unleash aquatic homing daggers and a 5% chance to instantly kill the enemy
Damage will heavily burn and damage all adjacent enemies
Press the Freeze Key to freeze time for 5 seconds
Pressing the 'Special Ability' key will envelop you in an impervious bubble, unleash an echo of Slag Fury's power, 
place you within the Dream, bend the very fabric of reality, and overload all nearby allies with every empowerment III
Summons a several pets";
            }*/

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
            //meme speed, solar flare, white dwarf flames, tide turner daggers, pyro bursts, assassin insta kill
            modPlayer.CosmoForce = true;
            //meteor shower
            modPlayer.MeteorEffect(75);
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

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //conduit set bonus
            thoriumPlayer.conduitSet = true;
            thoriumPlayer.orbital = true;
            thoriumPlayer.orbitalRotation1 = Utils.RotatedBy(thoriumPlayer.orbitalRotation1, -0.10000000149011612, default(Vector2));
            Lighting.AddLight(player.position, 0.2f, 0.35f, 0.7f);
            if ((player.velocity.X > 0f || player.velocity.X < 0f) && thoriumPlayer.circuitStage < 6)
            {
                thoriumPlayer.circuitCharge++;
                for (int i = 0; i < 1; i++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 185, 0f, 0f, 100, default(Color), 1f);
                    Main.dust[num].noGravity = true;
                }
            }
            //pets
            modPlayer.AddPet("Omega Pet", hideVisual, thorium.BuffType("OmegaBuff"), thorium.ProjectileType("Omega"));
            modPlayer.AddPet("I.F.O. Pet", hideVisual, thorium.BuffType("Identified"), thorium.ProjectileType("IFO"));
            modPlayer.AddPet("Bio-Feeder Pet", hideVisual, thorium.BuffType("BioFeederBuff"), thorium.ProjectileType("BioFeederPet"));
            //tide turner bonus globs
            thoriumPlayer.tideHelmet = true;
            if (thoriumPlayer.tideOrb < 5)
            {
                timer++;
                if (timer > 120)
                {
                    float num = 30f;
                    int num2 = 0;
                    while (num2 < num)
                    {
                        Vector2 vector = Vector2.UnitX * 0f;
                        vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(25f, 25f);
                        vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                        int num3 = Dust.NewDust(player.Center, 0, 0, 113, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num3].scale = 1.6f;
                        Main.dust[num3].noGravity = true;
                        Main.dust[num3].position = player.Center + vector;
                        Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                        int num4 = num2;
                        num2 = num4 + 1;
                    }
                    thoriumPlayer.tideOrb++;
                    timer = 0;
                }
            }
            //tide turner healing hotkey
            thoriumPlayer.tideSet = true;
            //pyro summon hotkey
            thoriumPlayer.napalmSet = true;
            //dream weaver bonuses
            //all allies invuln hotkey
            thoriumPlayer.dreamHoodSet = true;
            //enemies slowed and take more dmg hotkey
            thoriumPlayer.dreamSet = true;
            //maid pet
            modPlayer.AddPet("Maid Pet", hideVisual, thorium.BuffType("MaidBuff"), thorium.ProjectileType("Maid1"));
            modPlayer.DreamEnchant = true;
            //rhapsodist hotkey buff allies 
            thoriumPlayer.rallySet = true;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "ConduitEnchant");
                recipe.AddIngredient(null, "SolarEnchant");
                recipe.AddIngredient(null, "VortexEnchant");
                recipe.AddIngredient(null, "NebulaEnchant");
                recipe.AddIngredient(null, "StardustEnchant");
                recipe.AddIngredient(null, "WhiteDwarfEnchant");
                recipe.AddIngredient(null, "CelestialEnchant");
                recipe.AddIngredient(null, "BalladeerEnchant");
                recipe.AddIngredient(null, "TideTurnerEnchant");
                recipe.AddIngredient(null, "AssassinEnchant");
                recipe.AddIngredient(null, "PyromancerEnchant");
                recipe.AddIngredient(null, "DreamWeaverEnchant");
                recipe.AddIngredient(null, "RhapsodistEnchant");
            }
            else
            {*/
                recipe.AddIngredient(null, "MeteorEnchant");
                recipe.AddIngredient(null, "SolarEnchant");
                recipe.AddIngredient(null, "VortexEnchant");
                recipe.AddIngredient(null, "NebulaEnchant");
                recipe.AddIngredient(null, "StardustEnchant");
            //}

            recipe.AddIngredient(ItemID.SuspiciousLookingTentacle);

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}