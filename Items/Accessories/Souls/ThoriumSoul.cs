using Microsoft.Xna.Framework;
using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ThoriumSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int whisperingTimer;
        public int terrariumTimer;
        public int tideTimer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Yggdrasil");

            Tooltip.SetDefault(@"'The true might of the 9 realms is yours!'
An icy aura surrounds you, which freezes nearby enemies after a short delay
You occasionally birth a tentacle of abyssal energy that attacks nearby enemies
Summons a living wood sapling, a biotech probe, a Li'l Cherub, a and Li'l Devil
Grants the ability to dash into the enemy, Right Click to guard with your shield
The energy of Terraria seeks to protect you, A meteor shower initiates every few seconds while attacking
Attacks have a chance to unleash aquatic homing daggers, duplicate, or instantly kill the enemy
Pressing the 'Special Ability' key will unleash shadow wisps, summon ghosts, put you in a bubble, and unleash Slag Fury,
Effects of Flawless Chrysalis, Guide to Plant Fiber Cordage, and Agnor's Bowl
Effects of Ice Bound Strider Hide, Mix Tape, and Eye of the Storm
Effects of Champion's Rebuttal, Ogre Sandals, Greedy Magnet, and Abyssal Shell
Effects of Eye of the Beholder, Crietz, Mana-Charged Rocketeers, and Crash Boots
Effects of Vampire Gland, Demon Blood Badge, Lich's Gaze, and Plague Lord's Flask
Lots of other effects from material Forces, Summons several pets");
            DisplayName.AddTranslation(GameCulture.Chinese, "世界树之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'九界的真正力量属于你!'
冰霜光环围绕着你,在短暂的延迟后冻结附近的敌人
偶尔会产生一个深渊能量触手,攻击附近的敌人
召唤一个生命之木树苗,一个生物探测器,一个小天使和一个小恶魔
获得向敌人冲刺的能力,右键用盾牌保护你
泰拉瑞亚的能量试图保护你,攻击时每隔几秒钟就会有一场流星雨
攻击有机会释放追踪水匕首,复制,或立即杀死敌人
按下'特殊能力'键将会释放出影子,召唤鬼魂,用泡泡盾保护你,释放熔火之灵,
拥有无暇之蛹,植物纤维绳索宝典和琵琶鱼球碗的效果
拥有遁蛛契约,杂集磁带和风暴之眼的效果
拥有反击之盾,食人魔的凉鞋,贪婪磁铁和深远贝壳的效果
拥有注视者之眼,精准项链,魔力充能火箭靴和震地靴的效果
拥有吸血鬼腺体,魔血徽章,巫妖之视和瘟疫之主的药剂瓶的效果
许多材料魂的效果,召唤数个宠物");

            /*
             -not listed

            Moving around generates up to 5 static rings, then a bubble of energy will protect you from one attack
            Attacks have a 33% chance to heal you lightly
Damage will duplicate itself for 33% of the damage and apply the Frozen debuff to hit enemies
            Attacks have a chance to shock enemies with chain lightning and a lightning bolt
            Attacks will heavily burn and damage all adjacent enemies
            Every third attack will unleash an illumite missile
            Your attacks have a chance to unleash an explosion of Dragon's Flame
Consecutive attacks against enemies might drop flesh
            Every seventh attack will unleash damaging mana bolts
Critical strikes engulf enemies in a long lasting void flame and unleash ivory flares
            Damage reduction is increased by 10% at every 25% segment of life
            Briefly become invulnerable after striking an enemy

            Critical strikes release a splash of foam, slowing nearby enemies
After four consecutive non-critical strikes, your next attack will mini-crit for 150% damage
             Critical strikes ring a bell over your head, slowing all nearby enemies briefly
             Produces a floating globule every half second
Every globule increases defense and makes your next attack a mini-crit

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
                thorium.GetItem("EyeoftheStorm").UpdateAccessory(player, hideVisual);
            }
            //rebuttal
            thoriumPlayer.championShield = true;

            if (Soulcheck.GetValue("Incandescent Spark"))
            {
                thorium.GetItem("IncandescentSpark").UpdateAccessory(player, hideVisual);
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
            //modPlayer.AddPet("Omega Pet", hideVisual, thorium.BuffType("OmegaBuff"), thorium.ProjectileType("Omega"));
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
                thorium.GetItem("EyeofBeholder").UpdateAccessory(player, hideVisual);
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

            if (Soulcheck.GetValue("Folv's Aura"))
            {
                //folv
                thoriumPlayer.folvSet = true;
                thoriumPlayer.folvBonus2 = true;
            }

            if (Soulcheck.GetValue("Mana-Charged Rocketeers"))
            {
                //mana charge rockets
                thorium.GetItem("ManaChargedRocketeers").UpdateAccessory(player, hideVisual);
            }

            //HELHEIM
            //plague lord flask effect
            modPlayer.HelheimForce = true;
            if (Soulcheck.GetValue("Dread Speed"))
            {
                //dread
                player.moveSpeed += 0.8f;
                player.maxRunSpeed += 10f;
                player.runAcceleration += 0.05f;
                if (player.velocity.X > 0f || player.velocity.X < 0f)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int num = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 65, 0f, 0f, 0, default(Color), 1.75f);
                        int num2 = Dust.NewDust(new Vector2(player.position.X, player.position.Y) - player.velocity * 0.5f, player.width, player.height, 75, 0f, 0f, 0, default(Color), 1f);
                        Main.dust[num].noGravity = true;
                        Main.dust[num2].noGravity = true;
                        Main.dust[num].noLight = true;
                        Main.dust[num2].noLight = true;
                    }
                }
            }
            //crash boots
            thorium.GetItem("CrashBoots").UpdateAccessory(player, hideVisual);
            player.moveSpeed -= 0.15f;
            player.maxRunSpeed -= 1f;
            if (Soulcheck.GetValue("Dragon Flames"))
            {
                //dragon 
                thoriumPlayer.dragonSet = true;
            }
            //wyvern pet
            modPlayer.AddPet("Wyvern Pet", hideVisual, thorium.BuffType("WyvernPetBuff"), thorium.ProjectileType("WyvernPet"));
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
            //pet
            modPlayer.AddPet("Moogle Pet", hideVisual, thorium.BuffType("LilMogBuff"), thorium.ProjectileType("LilMog"));
            modPlayer.KnightEnchant = true;
            //lich gaze
            thoriumPlayer.lichGaze = true;

            //ASGARD
            //includes tide turner daggers, assassin duplicate and insta kill, pyro burst
            modPlayer.AsgardForce = true;
            //tide turner
            if (Soulcheck.GetValue("Tide Turner Globules"))
            {
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