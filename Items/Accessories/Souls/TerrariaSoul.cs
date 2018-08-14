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
Attacks may gain 4% life steal, inflict several debuffs, spawn lightning, petals, fireballs, damaging/healing orbs, a Dungeon Guardian, a vortex, buff boosters
Attacks deal more damage to low HP enemies, ignore knockback immunity, cause meteor showers, escalating Darkness debuffs, increased life regen, and shadow dodge
Sets your critical strike chance to 25%, Every crit will increase it by 5%, Getting hit drops your crit back down
Increases armor penetration by 30, 50% increased use speed, 50% chance for a spread shot, Double mining speed, Double wing time
Most projectiles will explode into shards, shoot from where you used to be, speed up drastically, gain 5 pierce
Summons icicles, a leaf crystal, a reflecting shield, an Enchanted Sword familiar, Beetles to protect you, and every pet
Right Click to guard with your shield, Double tap down to call an ancient storm, toggle stealth, direct your guardian, and freeze time
When stealthed, crits deal 4x damage and spores spawn, When still, you gain Shell Hide 
Solar shield allows you to dash, Dash into any walls, to teleport through them
You leave a trail of fire, teleport to smoke bombs, Minions spew scythes, Hearts heal twice as much
Taking damage will release a spore explosion, causes a needle spray, reflect 100% of contact damage
When you die, you explode and may cheat death, returning with half HP
Most other effects of material Forces");

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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.TerrariaSoul = true;
            modPlayer.TerraForce = true;
            //lightning
            modPlayer.CopperEnchant = true;
            //crit memes
            modPlayer.TinEnchant = true;
            modPlayer.AllCritEquals(modPlayer.TinCrit);
            //shield raise stuff
            modPlayer.IronEffect();
            //item attract
            modPlayer.IronEnchant = true;
            player.maxFallSpeed *= 5;
            modPlayer.LeadEnchant = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.armorPenetration += 10;

            //in lava effects
            if (player.lavaWet)
            {
                player.armorPenetration += 10;
                modPlayer.ObsidianEnchant = true;
            }

            modPlayer.EarthForce = true;
            //shards
            modPlayer.CobaltEnchant = true;
            player.onHitRegen = true;
            //lifesteal
            modPlayer.PalladEnchant = true;
            //use speed
            modPlayer.MythrilEnchant = true;
            player.onHitPetal = true;
            //fireballs
            modPlayer.OriEnchant = true;
            //projectile split
            modPlayer.AdamantiteEnchant = true;
            //knockback, dodge, damage reduce
            modPlayer.TitaniumEffect();

            player.crimsonRegen = true;
            //increase heart heal
            modPlayer.CrimsonEnchant = true;
            player.cordage = true;
            modPlayer.JungleEnchant = true;
            modPlayer.InfernoEffect(20);
            //explode on death
            modPlayer.MoltenEnchant = true;
            player.waterWalk = true;
            //icicles
            modPlayer.FrostEnchant = true;
            modPlayer.FrostEffect(80);
            modPlayer.AddMinion("Leaf Crystal", mod.ProjectileType("Chlorofuck"), 100, 10f);
            modPlayer.FlowerBoots();
            //herb double
            modPlayer.ChloroEnchant = true;
            //stealth and spores
            modPlayer.ShroomiteEffect();
            

            //needle spray
            modPlayer.CactusEnchant = true;
            //pie
            modPlayer.PumpkinEnchant = true;
            //flames
            modPlayer.PumpkinEffect(40);
            player.strongBees = true;
            //bees ignore defense
            modPlayer.BeeEnchant = true;
            //swarm debuff
            modPlayer.SpiderEnchant = true;
            //hide in shell buff
            modPlayer.TurtleEnchant = true;
            player.thorns = 1f;
            player.turtleThorns = true;
            player.aggro += 50;
            //wing time up
            modPlayer.BeetleEnchant = true;
            //beetle resistance
            modPlayer.BeetleEffect();


            

            modPlayer.SpiritForce = true;
            //revive
            modPlayer.FossilEnchant = true;
            //bone zone
            modPlayer.FossilEffect(20);
            //storm
            modPlayer.ForbiddenEffect();
            player.buffImmune[BuffID.WindPushed] = true;
            //sword and shield bois
            modPlayer.HallowEnchant = true;
            modPlayer.AddMinion("Hallowed Shield", mod.ProjectileType("HallowShield"), 0, 0f);
            modPlayer.AddMinion("Enchanted Sword Familiar", mod.ProjectileType("HallowSword"), (int)(80 * player.minionDamage), 0f);
            //random debuffs
            modPlayer.TikiEnchant = true;
            //spectre orbs
            modPlayer.SpectreEffect();

            

            modPlayer.ShadowForce = true;
            //smoke tele
            modPlayer.NinjaEnchant = true;
            //darkness doom
            modPlayer.ShadowEnchant = true;
            //DG hit
            modPlayer.NecroEnchant = true;
            //scythe doom
            modPlayer.SpookyEnchant = true;
            player.setMonkT2 = true;
            player.setMonkT3 = true;
            //ninja gear
            player.blackBelt = true;
            player.spikedBoots = 2;
            player.dash = 1;
            //tele thru walls
            modPlayer.ShinobiEnchant = true;
            player.setApprenticeT2 = true;
            player.setApprenticeT3 = true;
            //shadow shoot meme
            modPlayer.DarkEnchant = true;

           

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

            modPlayer.CosmoForce = true;
            //meteor shower
            modPlayer.MeteorEffect(75, 2);
            modPlayer.SolarShield();
            //solar flare debuff
            modPlayer.SolarEnchant = true;
            //portal spawn
            modPlayer.VortexEnchant = true;
            //stealth memes
            modPlayer.VortexEffect();
            //boosters and meme speed
            modPlayer.NebulaEffect();
            //minion and freeze time
            modPlayer.StardustEnchant = true;
            modPlayer.StardustEffect();
            modPlayer.AddPet("Baby Face Monster Pet", hideVisual, BuffID.BabyFaceMonster, ProjectileID.BabyFaceMonster);
            modPlayer.AddPet("Crimson Heart Pet", hideVisual, BuffID.CrimsonHeart, ProjectileID.CrimsonHeart);
            modPlayer.AddPet("Baby Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Baby Snowman Pet", hideVisual, BuffID.BabySnowman, ProjectileID.BabySnowman);
            modPlayer.AddPet("Seedling Pet", hideVisual, BuffID.PetSapling, ProjectileID.Sapling);
            modPlayer.AddPet("Truffle Pet", hideVisual, BuffID.BabyTruffle, ProjectileID.Truffle);
            modPlayer.AddPet("Squashling Pet", hideVisual, BuffID.Squashling, ProjectileID.Squashling);
            modPlayer.AddPet("Baby Hornet Pet", hideVisual, BuffID.BabyHornet, ProjectileID.BabyHornet);
            modPlayer.AddPet("Spider Pet", hideVisual, BuffID.PetSpider, ProjectileID.Spider);
            modPlayer.AddPet("Turtle Pet", hideVisual, BuffID.PetTurtle, ProjectileID.Turtle);
            modPlayer.AddPet("Lizard Pet", hideVisual, BuffID.PetLizard, ProjectileID.PetLizard);
            modPlayer.AddPet("Baby Dino Pet", hideVisual, BuffID.BabyDinosaur, ProjectileID.BabyDino);
            modPlayer.AddPet("Fairy Pet", hideVisual, BuffID.FairyBlue, ProjectileID.BlueFairy);
            modPlayer.AddPet("Tiki Pet", hideVisual, BuffID.TikiSpirit, ProjectileID.TikiSpirit);
            modPlayer.AddPet("Wisp Pet", hideVisual, BuffID.Wisp, ProjectileID.Wisp);
            modPlayer.AddPet("Black Cat Pet", hideVisual, BuffID.BlackCat, ProjectileID.BlackCat);
            modPlayer.AddPet("Baby Eater Pet", hideVisual, BuffID.BabyEater, ProjectileID.BabyEater);
            modPlayer.AddPet("Shadow Orb Pet", hideVisual, BuffID.ShadowOrb, ProjectileID.ShadowOrb);
            modPlayer.AddPet("Baby Skeletron  Pet", hideVisual, BuffID.BabySkeletronHead, ProjectileID.BabySkeletronHead);
            modPlayer.AddPet("Cursed Sapling Pet", hideVisual, BuffID.CursedSapling, ProjectileID.CursedSapling);
            modPlayer.AddPet("Eye Spring Pet", hideVisual, BuffID.EyeballSpring, ProjectileID.EyeSpring);
            modPlayer.AddPet("Gato Pet", hideVisual, BuffID.PetDD2Gato, ProjectileID.DD2PetGato);
            modPlayer.AddPet("Flickerwick Pet", hideVisual, BuffID.PetDD2Ghost, ProjectileID.DD2PetGhost);
            modPlayer.AddPet("Magic Lantern Pet", hideVisual, BuffID.MagicLantern, ProjectileID.MagicLantern);
            modPlayer.AddPet("Parrot Pet", hideVisual, BuffID.PetParrot, ProjectileID.Parrot);
            modPlayer.AddPet("Mini Minotaur Pet", hideVisual, BuffID.MiniMinotaur, ProjectileID.MiniMinotaur);
            modPlayer.AddPet("Puppy Pet", hideVisual, BuffID.Puppy, ProjectileID.Puppy);
            modPlayer.AddPet("Dragon Pet", hideVisual, BuffID.PetDD2Dragon, ProjectileID.DD2PetDragon);
            modPlayer.AddPet("Companion Cube Pet", hideVisual, BuffID.CompanionCube, ProjectileID.CompanionCube);
            modPlayer.AddPet("Suspicious Looking Eye Pet", hideVisual, BuffID.SuspiciousTentacle, ProjectileID.SuspiciousTentacle);
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