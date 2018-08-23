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
            modPlayer.TerrariaPets(hideVisual);
            
            //includes:
            //modPlayer.AdamantiteEnchant = true;
            //includes revive, both spectres, shadow dodge, palad heal

            player.onHitDodge = true;
            player.onHitRegen = true;

            modPlayer.CosmoForce = true;
            modPlayer.SpiritForce = true;
            modPlayer.ShadowForce = true;
            modPlayer.WillForce = true;

            
            modPlayer.BeetleEffect(); //just beetles
            modPlayer.ChloroEffect(hideVisual, 100); //everything
            modPlayer.CrimsonEffect(hideVisual); //everything
            modPlayer.ForbiddenEffect(); //everything, maybe kill storm boost
            modPlayer.GoldEffect(hideVisual); // cut coin trash and buff memes
            modPlayer.HallowEffect(hideVisual, 100); //everything
            modPlayer.IronEffect(); //everything
            modPlayer.MoltenEffect(20); //everything
            modPlayer.NebulaEffect(); //no meme speed
            modPlayer.NecroEffect(hideVisual); //everything
            modPlayer.PlatinumEnchant = true;
            modPlayer.RedRidingEffect(hideVisual); //no super bleed or increase low Hp dmg
            modPlayer.ShinobiEffect(hideVisual); //no tele thru walls, hmm
            modPlayer.SolarEffect(); //all effects, maybe kill solar flare debuff
            modPlayer.StardustEffect(); //all, may kill freeze time
            modPlayer.TinEffect(); //lifesteal bonus
            modPlayer.ValhallaEffect(hideVisual); //all, may kill KB
            modPlayer.VortexEffect(hideVisual); //all may kill voids



            modPlayer.OrichalcumEffect(); //no petals, permanent 4 balls instead of spawning

            /*
            modPlayer.DarkArtistEffect(hideVisual);
            modPlayer.GladiatorEffect(hideVisual);
            
            */

            //modPlayer.MythrilEnchant = true; effects in class souls
            //modPlayer.TikiEffect(hideVisual); effect in SoU
            //modPlayer.MinerEffect(hideVisual, .5f); into world shaper

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