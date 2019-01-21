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
Summons fireballs, icicles, a leaf crystal, Hallowed sword and shield, Beetles, and every pet
Toggle vanity to remove all Pets, Right Click to Guard
Double tap down to call an ancient storm, toggle stealth, spawn a portal, and direct your guardian
Press the Freeze Key to freeze time for 5 seconds, minions spew scythes 
Solar shield allows you to dash, Dash into any walls, to teleport through them
Attacks may spawn lightning, flower petals, spectre orbs, a Dungeon Guardian, or buff boosters
Attacks cause increased life regen, shadow dodge, meteor showers, reduced enemy knockback immunity
Critical chance is set to 25%, Crit to increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down, releases a spore explosion and reflects damage
One attack gains 5% life steal every second, capped at 5 HP
Projectiles may split or shatter, Hearts and Stars heal twice as much, 
Nearby enemies are ignited, You leave behind a trail of fire when you walk
Most other effects of material Forces
When you die, you explode and revive with 200 HP");

            /*
             -not listed
             Throw a smoke bomb to teleport to it
             Your attacks inflict Midas
            10% chance for enemies to drop 4x loot
            If the enemy has Midas, the chance and bonus is doubled
            Effects of Hive Pack, Flower Boots, Master Ninja Gear, Celestial Shell, Shiny Stone, and Greedy Ring
            Your weapon's projectiles occasionally shoot from the shadows of where you used to be
            Enemies will explode into needles on death            
             * */

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.value = 5000000;
            item.shieldSlot = 5;

            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //includes revive, both spectres, adamantite, heart and star heal
            modPlayer.TerrariaSoul = true;

            //TERRA
            modPlayer.TerraForce = true; //crit effect improved
            modPlayer.CopperEnchant = true; //lightning
            modPlayer.TinEffect(); //crits
            player.dash = 2;
            modPlayer.IronEffect(); //shield
            if (Soulcheck.GetValue("Iron Magnet"))
            {
                modPlayer.IronEnchant = true;
            }
            player.fireWalk = true;
            player.lavaImmune = true;

            //EARTH
            modPlayer.CobaltEnchant = true; //shards
            modPlayer.PalladiumEffect(); //regen on hit, heals
            modPlayer.OrichalcumEffect(); //fireballs and petals
            modPlayer.AdamantiteEnchant = true; //split
            modPlayer.TitaniumEffect(); //shadow dodge, full hp resistance

            //NATURE
            modPlayer.NatureForce = true; //bulb, cryo effect
            modPlayer.CrimsonEffect(hideVisual); //regen, hearts heal more, pets
            modPlayer.MoltenEffect(25); //inferno and explode
            modPlayer.FrostEffect(75, hideVisual); //icicles, pets
            modPlayer.JungleEffect(); //spores
            modPlayer.ChloroEffect(hideVisual, 100); //crystal and pet
            modPlayer.ShroomiteEffect(hideVisual); //pet

            //LIFE
            modPlayer.LifeForce = true; //tide hunter, yew wood, iridescent effects
            modPlayer.BeeEffect(hideVisual); //bees ignore defense, super bees, pet
            modPlayer.SpiderEffect(hideVisual); //pet
            modPlayer.BeetleEffect(); //defense beetle bois
            modPlayer.PumpkinEffect(50, hideVisual); //flame trail, pie heal, pet
            modPlayer.TurtleEffect(hideVisual); //thorns, pets
            player.thorns = 1f;
            player.turtleThorns = true;
            modPlayer.CactusEffect(); //needle spray

            //SPIRIT
            modPlayer.SpiritForce = true; //spectre works for all, spirit trapper works for all
            modPlayer.FossilEffect(40, hideVisual); //revive, bone zone, pet
            modPlayer.ForbiddenEffect(); //storm
            modPlayer.HallowEffect(hideVisual, 100); //sword, shield, pet
            modPlayer.TikiEffect(hideVisual); //pet
            modPlayer.SpectreEffect(hideVisual); //pet

            //SHADOW
            modPlayer.ShadowForce = true; //warlock, shade, plague accessory effect for all
            modPlayer.DarkArtistEffect(hideVisual); //shoot from where you were meme, pet
            modPlayer.NecroEffect(hideVisual); //DG meme, pet
            modPlayer.ShadowEffect(hideVisual); //pets
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            modPlayer.ShinobiEffect(hideVisual); //tele thru walls, pet
            modPlayer.NinjaEffect(hideVisual); //smoke bomb nonsense, pet
            modPlayer.SpookyEffect(hideVisual); //scythe doom, pets

            //WILL
            modPlayer.WillForce = true; //knockback remove for all
            modPlayer.GoldEffect(hideVisual); //midas, greedy ring, pet
            modPlayer.PlatinumEnchant = true; //loot multiply
            modPlayer.GladiatorEffect(hideVisual); //pet
            modPlayer.RedRidingEffect(hideVisual); //pet
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            modPlayer.ValhallaEffect(hideVisual); //knockback remove
            player.shinyStone = true;

            //COSMOS
            modPlayer.CosmoForce = true; //white dwarf flames, tide turner daggers, pyro bursts, assassin insta kill
            modPlayer.MeteorEffect(75); //meteor shower
            modPlayer.SolarEffect(); //solar shields
            modPlayer.VortexEffect(hideVisual); //stealth, voids, pet
            modPlayer.NebulaEffect(); //boosters
            modPlayer.StardustEffect(); //guardian and time freeze
            modPlayer.AddPet("Suspicious Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);
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
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}