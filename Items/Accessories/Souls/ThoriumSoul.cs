using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ThoriumSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int whisperingTimer;
        public int stormTimer;
        public int lightGen;
        public int terrariumTimer;
        public int tideTimer;

        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Yggdrasil");

            Tooltip.SetDefault(@"'The true might of the 9 realms is yours!'
Does a ton of things, tooltip coming soon TM");

            /*
             -not listed
Attacks have a 33% chance to heal you lightly
Summons a living wood sapling and its attacks will home in on enemies

Critical strikes release a splash of foam, slowing nearby enemies
After four consecutive non-critical strikes, your next attack will mini-crit for 150% damage
Damage will duplicate itself for 33% of the damage and apply the Frozen debuff to hit enemies
An icy aura surrounds you, which freezes nearby enemies after a short delay
You occasionally birth a tentacle of abyssal energy that attacks nearby enemies

Critical strikes will generate up to 15 shadow wisps
Pressing the 'Special Ability' key will unleash them towards your cursor's position
A biotech probe will assist you in healing your allies
Summons a Li'l Cherub and Li'l Devil

Pressing the 'Special Ability' key will summon a chorus of music playing ghosts

Attacks have a chance to shock enemies with chain lightning and a lightning bolt
Grants the ability to dash into the enemy
Right Click to guard with your shield
Briefly become invulnerable after striking an enemy
A meteor shower initiates every few seconds while attacking
Moving around generates up to 5 static rings, then a bubble of energy will protect you from one attack

Damage reduction is increased by 10% at every 25% segment of life
Every third attack will unleash an illumite missile
The energy of Terraria seeks to protect you
Critical strikes ring a bell over your head, slowing all nearby enemies briefly

Every seventh attack will unleash damaging mana bolts
Critical strikes engulf enemies in a long lasting void flame and unleash ivory flares

Your attacks have a chance to unleash an explosion of Dragon's Flame
Your attacks may inflict Darkness on enemies
Consecutive attacks against enemies might drop flesh
Greatly increases life regen
Hearts heal for 1.5x as much

Produces a floating globule every half second
Every globule increases defense and makes your next attack a mini-crit
Attacks have a chance to unleash aquatic homing daggers, duplicate, or instantly kill the enemy
Attacks will heavily burn and damage all adjacent enemies
Pressing the 'Special Ability' key will envelop you within an impervious bubble and unleash an echo of Slag Fury's power,

Effects of Flawless Chrysalis, Guide to Plant Fiber Cordage, and Agnor's Bowl
Effects of Ice Bound Strider Hide, Mix Tape, and Eye of the Storm
Effects of Champion's Rebuttal, Ogre Sandals, and Greedy Magnet
Effects of Abyssal Shell, Eye of the Beholder, and Crietz
Effects of Mana-Charged Rocketeers, Crash Boots, and Vampire Gland
Effects of Demon Blood Badge, Lich's Gaze, and Plague Lord's Flask
Summons several pets           

             * */
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
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //warlock wisps, mix tape
            modPlayer.ThoriumSoul = true;

            //MUSPELHEIM
            //life bloom heals
            modPlayer.MuspelheimForce = true;
            //chrysalis
            thoriumPlayer.cocoonAcc = true;
            //living wood set bonus
            thoriumPlayer.livingWood = true;
            //free boi
            modPlayer.LivingWoodEnchant = true;
            modPlayer.AddMinion("Sapling Minion", thorium.ProjectileType("MinionSapling"), 25, 2f);
            //vine rope thing
            player.cordage = true;

            //JOTUNHEIM
            //tide hunter foam, yew crits, cryo duplicate
            modPlayer.JotunheimForce = true;
            modPlayer.DepthEnchant = true;
            modPlayer.AddPet("Jellyfish Pet", hideVisual, thorium.BuffType("JellyPet"), thorium.ProjectileType("JellyfishPet"));
            //tide hunter
            //angler bowl
            if (!hideVisual)
            {
                if (player.direction > 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X + 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
                if (player.direction < 0 && Main.rand.Next(2) == 0)
                {
                    Projectile.NewProjectile(player.Center.X - 56f, player.Center.Y - 10f, 0f, 0f, thorium.ProjectileType("AnglerLight"), 0, 0f, Main.myPlayer, 0f, 0f);
                }
            }
            //strider hide
            thoriumPlayer.frostBonusDamage = true;
            //pets
            modPlayer.IcyEnchant = true;
            modPlayer.AddPet("Penguin Pet", hideVisual, BuffID.BabyPenguin, ProjectileID.Penguin);
            modPlayer.AddPet("Owl Pet", hideVisual, thorium.BuffType("SnowyOwlBuff"), thorium.ProjectileType("SnowyOwlPet"));
            if (Soulcheck.GetValue("Icy Barrier"))
            {
                //icy set bonus
                thoriumPlayer.icySet = true;
                if (player.ownedProjectileCounts[thorium.ProjectileType("IcyAura")] < 1)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("IcyAura"), 0, 0f, player.whoAmI, 0f, 0f);
                }
            }
            if (Soulcheck.GetValue("Whispering Tentacles"))
            {
                //whispering
                thoriumPlayer.whisperingSet = true;
                if (player.ownedProjectileCounts[thorium.ProjectileType("WhisperingTentacle")] + player.ownedProjectileCounts[thorium.ProjectileType("WhisperingTentacle2")] < 6 && player.ownedProjectileCounts[thorium.ProjectileType("WhisperingTentacleSpawn")] < 1)
                {
                    whisperingTimer++;
                    if (whisperingTimer > 30)
                    {
                        Projectile.NewProjectile(player.Center.X + (float)Main.rand.Next(-300, 300), player.Center.Y, 0f, 0f, thorium.ProjectileType("WhisperingTentacleSpawn"), 50, 0f, player.whoAmI, 0f, 0f);
                        whisperingTimer = 0;
                    }
                }
            }
            
            //ALFHEIM
            //lil cherub
            modPlayer.SacredEnchant = true;
            modPlayer.AddMinion("Li'l Cherub Minion", thorium.ProjectileType("Angel"), 0, 0f);
            //twinkle pet
            modPlayer.AddPet("Life Spirit Pet", hideVisual, thorium.BuffType("LifeSpiritBuff"), thorium.ProjectileType("LifeSpirit"));
            //lil devil
            modPlayer.WarlockEnchant = true;
            modPlayer.AddMinion("Li'l Devil Minion", thorium.ProjectileType("Devil"), 20, 2f);
            //biotech
            mod.GetItem("BiotechEnchant").UpdateAccessory(player, hideVisual);
            //warlock
            thoriumPlayer.warlockSet = true;
            //goat pet
            modPlayer.BinderEnchant = true;
            modPlayer.AddPet("Holy Goat Pet", hideVisual, thorium.BuffType("HolyGoatBuff"), thorium.ProjectileType("HolyGoat"));
            thoriumPlayer.goatPet = true;

            //NIFLHEIM
            //conductor
            thoriumPlayer.conductorSet = true;

            //SVARTALFHEIM
            //includes bronze lightning
            modPlayer.SvartalfheimForce = true;
            if (Soulcheck.GetValue("Eye of the Storm"))
            {
                //eye of the storm
                stormTimer++;
                if (stormTimer > 60)
                {
                    if (player.direction > 0)
                    {
                        Projectile.NewProjectile(player.Center.X + 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X + 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                        stormTimer = 0;
                    }
                    if (player.direction < 0)
                    {
                        Projectile.NewProjectile(player.Center.X - 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                        Projectile.NewProjectile(player.Center.X - 14f, player.Center.Y - 20f, Main.rand.Next(-5, 5), Main.rand.Next(-5, -1), thorium.ProjectileType("StormHome"), 25, 0f, player.whoAmI, 0f, 0f);
                        stormTimer = 0;
                    }
                }
            }
            //rebuttal
            thoriumPlayer.championShield = true;
            //copper enchant
            player.GetModPlayer<FargoPlayer>(mod).CopperEnchant = true;
            if (Soulcheck.GetValue("Ogre Sandals"))
            {
                //ogre sandals
                if (player.velocity.Y > 0f && thoriumPlayer.falling < 120)
                {
                    thoriumPlayer.falling += 3;
                }
                if (player.velocity.Y < 0f)
                {
                    thoriumPlayer.falling = 0;
                }
                if (player.velocity.Y == 0f && Collision.SolidCollision(player.position, player.width, player.height + 4) && thoriumPlayer.falling > 50)
                {
                    if (thoriumPlayer.falling >= 100)
                    {
                        Main.PlaySound(SoundID.Item70, player.position);
                        Main.PlaySound(SoundID.Item69, player.position);
                        float num = 16f;
                        int num2 = 0;
                        while (num2 < num)
                        {
                            Vector2 vector = Vector2.UnitX * 0f;
                            vector += -Utils.RotatedBy(Vector2.UnitY, (num2 * (6.28318548f / num)), default(Vector2)) * new Vector2(20f, 5f);
                            vector = Utils.RotatedBy(vector, Utils.ToRotation(player.velocity), default(Vector2));
                            int num3 = Dust.NewDust(player.Center, 0, 0, 0, 0f, 0f, 0, default(Color), 1f);
                            Main.dust[num3].scale = 1.35f;
                            Main.dust[num3].noGravity = true;
                            Main.dust[num3].position = player.Center + vector;
                            Dust dust = Main.dust[num3];
                            dust.position.Y = dust.position.Y + 12f;
                            Main.dust[num3].velocity = player.velocity * 0f + Utils.SafeNormalize(vector, Vector2.UnitY) * 1f;
                            int num4 = num2;
                            num2 = num4 + 1;
                        }
                    }
                    Main.PlaySound(SoundID.Item69, player.position);
                    float num5 = 6f + 0.05f * thoriumPlayer.falling;
                    int num6 = (int)(50.0 + thoriumPlayer.falling * 0.25);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 8f, 5f + thoriumPlayer.falling * 0.035f, 0f, thorium.ProjectileType("CrashSurge"), num6, num5, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y + 8f, -5f - thoriumPlayer.falling * 0.035f, 0f, thorium.ProjectileType("CrashSurge"), num6, num5, Main.myPlayer, 0f, 0f);
                    thoriumPlayer.falling = 0;
                }
            }
            if (Soulcheck.GetValue("Greedy Magnet"))
            {
                //greedy magnet
                for (int i = 0; i < 400; i++)
                {
                    if (Main.item[i].active && Main.item[i].noGrabDelay == 0 && Vector2.Distance(player.Center, Main.item[i].position) < 700f)
                    {
                        Main.item[i].beingGrabbed = true;
                        float num = 10f;
                        Vector2 vector = new Vector2(Main.item[i].position.X + (Main.item[i].width / 2), Main.item[i].position.Y + (Main.item[i].height / 2));
                        float num2 = player.Center.X - vector.X;
                        float num3 = player.Center.Y - vector.Y;
                        float num4 = (float)Math.Sqrt((num2 * num2 + num3 * num3));
                        num4 = num / num4;
                        num2 *= num4;
                        num3 *= num4;
                        int num5 = 5;
                        Main.item[i].velocity.X = (Main.item[i].velocity.X * (num5 - 1) + num2) / num5;
                        Main.item[i].velocity.Y = (Main.item[i].velocity.Y * (num5 - 1) + num3) / num5;
                    }
                }
            }  
            //EoC Shield
            player.dash = 2;
            if (Soulcheck.GetValue("Iron Shield"))
            {
                //iron shield raise
                modPlayer.IronEffect();
            }
            //titanium
            modPlayer.TitaniumEffect();
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;
            if (Soulcheck.GetValue("Conduit Shield"))
            {
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
            }
            
            //meteor effect
            modPlayer.MeteorEffect(60);
            //pets
            modPlayer.AddPet("Omega Pet", hideVisual, thorium.BuffType("OmegaBuff"), thorium.ProjectileType("Omega"));
            modPlayer.AddPet("I.F.O. Pet", hideVisual, thorium.BuffType("Identified"), thorium.ProjectileType("IFO"));
            modPlayer.AddPet("Bio-Feeder Pet", hideVisual, thorium.BuffType("BioFeederBuff"), thorium.ProjectileType("BioFeederPet"));

            //MIDGARD
            //includes illumite rocket and jester bell
            modPlayer.MidgardForce = true;
            if (Soulcheck.GetValue("Lodestone Resistance"))
            {
                //lodestone
                thoriumPlayer.orbital = true;
                thoriumPlayer.orbitalRotation3 = Utils.RotatedBy(thoriumPlayer.orbitalRotation3, -0.05000000074505806, default(Vector2));
                if (player.statLife > player.statLifeMax * 0.75)
                {
                    thoriumPlayer.thoriumEndurance += 0.1f;
                    thoriumPlayer.lodestoneStage = 1;
                }
                if (player.statLife <= player.statLifeMax * 0.75 && player.statLife > player.statLifeMax * 0.5)
                {
                    thoriumPlayer.thoriumEndurance += 0.2f;
                    thoriumPlayer.lodestoneStage = 2;
                }
                if (player.statLife <= player.statLifeMax * 0.5)
                {
                    thoriumPlayer.thoriumEndurance += 0.3f;
                    thoriumPlayer.lodestoneStage = 3;
                }
            }
            if (Soulcheck.GetValue("Eye of the Beholder"))
            {
                //eye of beholder
                lightGen++;
                if (lightGen >= 40)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("BeholderGaze"), 0, 0f, player.whoAmI, i, 0f);
                    }
                    for (int j = 0; j < 10; j++)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("BeholderGaze2"), 0, 0f, player.whoAmI, j, 0f);
                    }
                    lightGen = 0;
                }
            }
            //slime pet
            modPlayer.AddPet("Pink Slime Pet", hideVisual, thorium.BuffType("PinkSlimeBuff"), thorium.ProjectileType("PinkSlime"));
            modPlayer.IllumiteEnchant = true;
            if (Soulcheck.GetValue("Terrarium Spirits"))
            {
                //terrarium set bonus
                terrariumTimer++;
                if (terrariumTimer > 60)
                {
                    Projectile.NewProjectile(player.Center.X + 14f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraRed"), 50, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X + 9f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraOrange"), 50, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X + 4f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraYellow"), 50, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraGreen"), 50, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X - 4f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraBlue"), 50, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X - 9f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraIndigo"), 50, 0f, Main.myPlayer, 0f, 0f);
                    Projectile.NewProjectile(player.Center.X - 14f, player.Center.Y - 20f, 0f, 2f, thorium.ProjectileType("TerraPurple"), 50, 0f, Main.myPlayer, 0f, 0f);
                    terrariumTimer = 0;
                }
            }
            if (Soulcheck.GetValue("Crietz"))
            {
                //crietz
                thoriumPlayer.crietzAcc = true;
            }

            //VANAHEIM
            //includes malignant debuff, folv bolts, white dwarf flares
            modPlayer.VanaheimForce = true;
            //folv
            thoriumPlayer.folvSet = true;
            thoriumPlayer.folvBonus2 = true;

            if (Soulcheck.GetValue("Mana-Charged Rocketeers"))
            {
                //mana charge rockets
                player.manaRegen++;
                player.manaRegenDelay -= 2;
                if (player.statMana > 0)
                {
                    player.rocketBoots = 1;
                    if (player.rocketFrame)
                    {
                        if (Main.rand.Next(2) == 0)
                        {
                            player.statMana -= 2;
                            Dust.NewDust(new Vector2(player.position.X, player.position.Y + 20f), player.width, player.height, 15, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default(Color), 1.5f);
                        }
                        player.rocketTime = 1;
                    }
                }
            }

            //HELHEIM
            //plague lord flask effect
            modPlayer.HelheimForce = true;
            //crash boots
            player.moveSpeed += 0.0015f * thoriumPlayer.momentum;
            player.maxRunSpeed += 0.0025f * thoriumPlayer.momentum;
            if (player.velocity.X > 0f || player.velocity.X < 0f)
            {
                if (thoriumPlayer.momentum < 180)
                {
                    thoriumPlayer.momentum++;
                }
                if (thoriumPlayer.momentum > 60 && Collision.SolidCollision(player.position, player.width, player.height + 4))
                {
                    int num = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + player.height - 2f), player.width + 4, 4, 6, 0f, 0f, 100, default(Color), 0.625f + 0.0075f * thoriumPlayer.momentum);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].noLight = true;
                    Dust dust = Main.dust[num];
                    dust.velocity *= 0f;
                }
            }
            if (Soulcheck.GetValue("Dragon Flames"))
            {
                //dragon 
                thoriumPlayer.dragonSet = true;
            }
            //wyvern pet
            modPlayer.AddPet("Wyvern Pet", hideVisual, thorium.BuffType("WyvernPetBuff"), thorium.ProjectileType("WyvernPet"));
            //darkness, pets
            modPlayer.ShadowEffect(hideVisual);
            //demon blood badge
            thoriumPlayer.CrimsonBadge = true;
            if (Soulcheck.GetValue("Flesh Drops"))
            {
                //flesh set bonus
                thoriumPlayer.Symbiotic = true;
            }
            if (Soulcheck.GetValue("Vampire Gland"))
            {
                //vampire gland
                thoriumPlayer.vampireGland = true;
            }
            //blister pet
            modPlayer.AddPet("Blister Pet", hideVisual, thorium.BuffType("BlisterBuff"), thorium.ProjectileType("BlisterPet"));
            //crimson regen, pets
            modPlayer.CrimsonEffect(hideVisual);
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;

            //ASGARD
            //includes tide turner daggers, assassin duplicate and insta kill, pyro burst
            modPlayer.AsgardForce = true;
            //tide turner
            //floating globs and defense
            thoriumPlayer.tideHelmet = true;
            if (thoriumPlayer.tideOrb < 8)
            {
                tideTimer++;
                if (tideTimer > 30)
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
                    tideTimer = 0;
                }
            }
            //set bonus damage to healing hot key
            thoriumPlayer.tideSet = true;
            //pyro summon bonus
            thoriumPlayer.napalmSet = true;
            //maid pet
            modPlayer.AddPet("Maid Pet", hideVisual, thorium.BuffType("MaidBuff"), thorium.ProjectileType("Maid1"));
            modPlayer.DreamEnchant = true;
        }

        

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "MuspelheimForce");
            recipe.AddIngredient(null, "JotunheimForce");
            recipe.AddIngredient(null, "AlfheimForce");
            recipe.AddIngredient(null, "NiflheimForce");
            recipe.AddIngredient(null, "SvartalfheimForce");
            recipe.AddIngredient(null, "MidgardForce");
            recipe.AddIngredient(null, "VanaheimForce");
            recipe.AddIngredient(null, "HelheimForce");
            recipe.AddIngredient(null, "AsgardForce");

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}