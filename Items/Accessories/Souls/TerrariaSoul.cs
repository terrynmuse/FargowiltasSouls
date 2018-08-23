using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class TerrariaSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Terraria");
            Tooltip.SetDefault(
@"'A true master of Terraria'
Summons a fireballs, icicles, a leaf crystal, Hallowed sword and shield, Beetles, and every pet
Toggle vanity to remove all Pets
Right Click to Guard, Double tap down to call an ancient storm, toggle stealth, spawn a portal, and direct your guardian
Press the Freeze Key to freeze time for 5 seconds
Solar shield allows you to dash, Dash into any walls, to teleport through them
Attacks may spawn spectre orbs, a Dungeon Guardian, or buff boosters
Attacks cause increased life regen, and shadow dodge
Critical strike chance is set to 25%, Every crit will increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down
Every 4th projectile will split, Hearts and Stars heal twice as much, Nearby enemies are ignited
All sentrys improved, You attract items, fall quickly, Grants Crimson Regen and lava immunity
Effects of Hive Pack, Flower Boots, Master Ninja Gear, Celestial Shell, Shiny Stone, and Greedy Ring
When you die, you explode and revive with 200 HP");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 2000000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //includes revive, both spectres, adamantite
            modPlayer.TerrariaSoul = true;
            modPlayer.TerrariaPets(hideVisual);
            
            player.onHitDodge = true;
            player.onHitRegen = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.strongBees = true;

            modPlayer.BeetleEffect(); //just beetles
            modPlayer.ChloroEffect(hideVisual, 100); //everything
            modPlayer.CrimsonEffect(hideVisual); //everything
            modPlayer.DarkArtistEffect(hideVisual); //no meme effect
            modPlayer.ForbiddenEffect(); //everything, maybe kill storm boost
            modPlayer.FrostEffect(80, hideVisual);
            modPlayer.GoldEffect(hideVisual); // just greed ring effect
            modPlayer.HallowEffect(hideVisual, 100); //everything
            modPlayer.IronEffect(); //everything
            modPlayer.MoltenEffect(20); //everything
            modPlayer.NebulaEffect(); //no meme speed
            modPlayer.NecroEffect(hideVisual); //everything
            modPlayer.OrichalcumEffect(); //no petals, permanent 4 balls instead of spawning
            modPlayer.RedRidingEffect(hideVisual); //no super bleed or increase low Hp dmg
            modPlayer.ShinobiEffect(hideVisual); //all
            modPlayer.SolarEffect(); //no solar flare debuff
            modPlayer.StardustEffect(); //all, may kill freeze time
            modPlayer.TinEffect(); //lifesteal bonus
            modPlayer.ValhallaEffect(hideVisual); //no KB memes
            modPlayer.VortexEffect(hideVisual); //all may kill voids

            //CUT
            //cactus, cobalt, copper, fossil bone zone, gladiator, jungle, lead, meteor, nebula meme speed, ninja, obsidian, platinum, pumpkin, red riding blood, shadow, shroomite, solar flare debuff, spider, spooky, tungsten, turtle, dark artist shadow shoot

            //ALTERED
            //ori changed, tin and palladium fused, titanium changed

            //ELSEWHERE
            //mythril, tiki, mining
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "TerraForce");
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "ShadowForce");
            recipe.AddIngredient(null, "WillForce");
            recipe.AddIngredient(null, "CosmoForce");

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