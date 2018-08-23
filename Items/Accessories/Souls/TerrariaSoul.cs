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
Attacks may gain 4% life steal, inflict several debuffs, spawn lightning, petals, fireballs, damaging/healing orbs, a Dungeon Guardian, buff boosters
Attacks deal more damage to low HP enemies, ignore knockback immunity, cause meteor showers, escalating Darkness debuffs, increased life regen, and shadow dodge
Sets your critical strike chance to 25%, Every crit will increase it by 5%, Getting hit drops your crit back down
Increases armor penetration by 30, 50% increased use speed, 50% chance for a spread shot, Double mining speed, Double wing time
Most projectiles will explode into shards, shoot from where you used to be, speed up drastically, gain 5 pierce
Summons icicles, a leaf crystal, a reflecting shield, an Enchanted Sword familiar, Beetles to protect you, and every pet
Right Click to guard with your shield, Double tap down to call an ancient storm, toggle stealth, spawna portal, direct your guardian, and freeze time
When stealthed, crits deal 4x damage and spores spawn, When still, you gain Shell Hide 
Solar shield allows you to dash, Dash into any walls, to teleport through them
You leave a trail of fire, teleport to smoke bombs, Minions spew scythes, Hearts heal twice as much
Taking damage will release a spore explosion, causes a needle spray, reflect 100% of contact damage
When you die, you explode and may cheat death, returning with half HP
Most other effects of material Forces");

            /*

Sets your critical strike chance to 25%, Every crit will increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down
Every 4th projectile will split
Summons a leaf crystal, a reflecting shield, an Enchanted Sword familiar, Beetles to protect you, and every pet
Right Click to guard with your shield, Double tap down to call a projectile buffing ancient storm, toggle stealth, spawn a portal, and direct your guardian
Freeze time
Solar shield allows you to dash, Dash into any walls, to teleport through them
Hearts and Stars heal twice as much
When you die, you explode
Most other effects of material Forces



            Effects of Flower Boots, Master Ninja Gear and all herb collection is doubled
            Nearby enemies are ignited
            Grants immunity to fire blocks and lava
            All sentry effectiveness
            Press the Freeze Key to freeze time for 5 seconds
            Greatly increases life regen (crimson)
            You attract items from a much larger range and fall 5 times as quickly
             * */

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

            modPlayer.TerrariaSoul = true;
            //includes:
            //modPlayer.AdamantiteEnchant = true;

            modPlayer.CosmoForce = true;
            modPlayer.SpiritForce = true;
            modPlayer.ShadowForce = true;
            modPlayer.WillForce = true;

            modPlayer.BeeEffect(hideVisual);
            modPlayer.BeetleEffect(); //no extra wing time
            modPlayer.ChloroEffect(hideVisual, 100);
            modPlayer.CrimsonEffect(hideVisual);
            modPlayer.ForbiddenEffect();
            modPlayer.FossilEffect(20, hideVisual); //no bone zone, 200HP, 4 minute CD
            modPlayer.FrostEffect(80, hideVisual);
            modPlayer.GoldEffect(hideVisual);
            modPlayer.HallowEffect(hideVisual, 100);
            modPlayer.IronEffect();
            modPlayer.MoltenEffect(20);
            modPlayer.NebulaEffect(); //no meme speed
            modPlayer.NecroEffect(hideVisual);
            modPlayer.NinjaEffect(hideVisual);
            modPlayer.ObsidianEffect(); //no armor pen or lava bonuses
            modPlayer.OrichalcumEffect(); //no petals, permanent 4 balls instead of spawning
            player.onHitRegen = true; // no life steal
            modPlayer.PlatinumEnchant = true;
            modPlayer.PumpkinEffect(40, hideVisual); //no fire trail
            modPlayer.RedRidingEffect(hideVisual); 
            modPlayer.ShadowEffect(hideVisual); //no darkness
            modPlayer.ShinobiEffect(hideVisual);
            modPlayer.ShroomiteEffect(hideVisual); //just the pet
            modPlayer.SolarEffect();
            modPlayer.SpectreEffect(hideVisual);
            modPlayer.SpiderEffect(hideVisual); //just pet
            modPlayer.SpookyEffect(hideVisual); //just pet
            modPlayer.StardustEffect();
            modPlayer.TinEffect(); //lifesteal bonus
            modPlayer.TitaniumEffect(); //just the shadow dodge
            modPlayer.TurtleEffect(hideVisual);
            modPlayer.ValhallaEffect(hideVisual);
            modPlayer.VortexEffect(hideVisual);
            modPlayer.AddPet("Suspicious Looking Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);


            /*
            modPlayer.TerraForce = true;
            modPlayer.EarthForce = true;

            modPlayer.CobaltEnchant = true;
            modPlayer.DarkArtistEffect(hideVisual);
            modPlayer.GladiatorEffect(hideVisual);
            */

            //modPlayer.MythrilEnchant = true; effects in class souls
            //modPlayer.TikiEffect(hideVisual); effect in SoU
            //modPlayer.MinerEffect(hideVisual, .5f); into world shaper


            //modPlayer.CactusEffect(); cut
            
            //modPlayer.MeteorEffect(75); cut
            //modPlayer.CopperEnchant = true; cut
            //modPlayer.LeadEnchant = true; cut
            //modPlayer.JungleEffect(); cut
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