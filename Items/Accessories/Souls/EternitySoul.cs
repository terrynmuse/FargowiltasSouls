using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod;
using System;
using ThoriumMod.Items.Misc;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class EternitySoul : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public bool jumped;
        public bool canHover;
        public int hoverTimer;
        public int jumpTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Eternity");
            Tooltip.SetDefault(
@"''
200% increased all damage, 100% increased shoot speed
Maximum use speed for all weapons
Crits deal 10x damage
Crit chance is set to 50%, Crit to increase it by 10% 
At 100% every attack gains 10% life steal and you gain +10% damage and +10 defense
This stacks forever until you get hit
Increases your maximum mana to 999, minions by 20, sentries by 10
400% increased HP, 40% damage reduction, 15 life regeneration
Grants immunity to knockback and most debuffs
Summon an impenatrable ring of death around you
When you die, you explode and revive with full HP
You respawn 10x as fast
All other effects of material Souls");
            //all debuffs soon tm
            /*
Effects of the Yoyo Bag, Sniper Scope, Celestial Cuffs, and Mana Flower
Effects of the Brain of Confusion, Star Veil, Sweetheart Necklace, and Bee Cloak
Effects of Spore Sac, Paladin's Shield, Frozen Turtle Shell, and Arctic Diving Gear
Effects of Frog Legs, Lava Waders, Angler Tackle Bag
and most of SoT not mentioned because meme tooltip length

            +20 inspiration
             * */
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 100000000;
            item.shieldSlot = 5;
            item.defense = 50;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //auto use, debuffs, mana up
            modPlayer.Eternity = true;

            //UNIVERSE
            modPlayer.AllDamageUp(2f);
            if (Soulcheck.GetValue("Universe Attack Speed"))
            {
                modPlayer.AttackSpeed *= 100f;
            }
            player.maxMinions += 20;
            player.maxTurrets += 10;
            //accessorys
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
            if (Soulcheck.GetValue("Universe Scope"))
            {
                player.scope = true;
            }
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;

            //DIMENSIONS
            //COLOSSUS
            player.statLifeMax2 *= 5;
            player.endurance += 0.4f;
            player.lifeRegen += 15;
            //hand warmer, pocket mirror, ankh shield
            player.buffImmune[BuffID.Chilled] = true;
            player.buffImmune[BuffID.Frozen] = true;
            player.buffImmune[BuffID.Stoned] = true;
            player.buffImmune[BuffID.Weak] = true;
            player.buffImmune[BuffID.BrokenArmor] = true;
            player.buffImmune[BuffID.Bleeding] = true;
            player.buffImmune[BuffID.Poisoned] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[BuffID.Confused] = true;
            player.buffImmune[BuffID.Silenced] = true;
            player.buffImmune[BuffID.Cursed] = true;
            player.buffImmune[BuffID.Darkness] = true;
            player.buffImmune[BuffID.ChaosState] = true;
            player.noKnockback = true;
            player.fireWalk = true;

            //soon tm
            //buffloader.buffcount something or other meme
            /*for(int i = 0; i < BuffLoa; i ++)
            {
                ModLoader.B

                if()
                {

                }
                player.buffImmune[i] = true;
            }*/

            //brain of confusion
            player.brainOfConfusion = true;
            //charm of myths
            player.pStone = true;
            //bee cloak, sweet heart necklace, star veil
            player.starCloak = true;
            player.bee = true;
            player.panic = true;
            player.longInvince = true;
            //spore sac
            if (Soulcheck.GetValue("Spore Sac"))
            {
                player.SporeSac();
                player.sporeSac = true;
            }
            //flesh knuckles
            player.aggro += 400;
            //frozen turtle shell
            if (player.statLife <= player.statLifeMax2 * 0.5) player.AddBuff(BuffID.IceBarrier, 5, true);
            //paladins shield
            if (player.statLife > player.statLifeMax2 * .25)
            {
                player.hasPaladinShield = true;
                for (int k = 0; k < 255; k++)
                {
                    Player target = Main.player[k];

                    if (target.active && player != target && Vector2.Distance(target.Center, player.Center) < 400) target.AddBuff(BuffID.PaladinsShield, 30);
                }
            }

            //SUPERSONIC
            //frost spark plus super speed
            if (Soulcheck.GetValue("Supersonic Speed Boosts"))
            {
                player.maxRunSpeed += 15f;
                player.runAcceleration += .25f;
                player.autoJump = true;
                player.jumpSpeedBoost += 2.4f;
                player.jumpBoost = true;
                player.maxFallSpeed += 5f;
            }
            /*else
            {
                player.maxRunSpeed += 5f;
                player.runAcceleration += .1f;
            }*/
            player.moveSpeed += 0.5f;
            player.accRunSpeed = 12f;
            player.rocketBoots = 3;
            player.iceSkate = true;
            //arctic diving gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            //lava waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            //frog legs
            //player.autoJump = true;
            //player.jumpSpeedBoost += 2.4f;
            //player.jumpBoost = true;
            //slime mount
            //player.maxFallSpeed += 5f;

            //FLIGHT MASTERY
            player.wingTimeMax = 999999;
            player.ignoreWater = true;

            //TRAWLER
            //extra lures
            modPlayer.FishSoul2 = true;
            //modPlayer.AddPet("Zephyr Fish Pet", hideVisual, BuffID.ZephyrFish, ProjectileID.ZephyrFish);
            player.sonarPotion = true;
            player.fishingSkill += 100;
            player.cratePotion = true;
            player.accFishingLine = true;
            player.accTackleBox = true;
            player.accFishFinder = true;

            //WORLD SHAPER
            //placing speed up
            player.tileSpeed += 0.5f;
            player.wallSpeed += 0.5f;
            //toolbox
            Player.tileRangeX += 50;
            Player.tileRangeY += 50;
            //gizmo pack
            player.autoPaint = true;
            //pick speed
            player.pickSpeed -= 0.50f;
            //mining helmet
            if (Soulcheck.GetValue("Shine Buff")) Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            //presserator
            player.autoActuator = true;
            //builder mode
            if (Soulcheck.GetValue("Builder Mode"))
            {
                FargoWorld.BuilderMode = true;
                modPlayer.BuilderMode = true;
            }
            //cell phone
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accDreamCatcher = true;
            player.accOreFinder = true;
            player.accStopwatch = true;
            player.accCritterGuide = true;
            player.accJarOfSouls = true;
            player.accThirdEye = true;
            player.accCalendar = true;
            player.accWeatherRadio = true;

            //TERRARIA
            //includes revive, both spectres, adamantite, heart and star heal
            modPlayer.TerrariaSoul = true;

            //TERRA
            modPlayer.TerraForce = true; //crit effect improved
            modPlayer.CopperEnchant = true; //lightning
            modPlayer.TinEffect(); //crits
            player.dash = 2;
            modPlayer.IronEffect(); //shield
            modPlayer.IronEnchant = true; //magnet
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

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //phylactery
            if (!thoriumPlayer.lichPrevent)
            {
                player.AddBuff(thorium.BuffType("LichActive"), 60, true);
            }
            //crystal scorpion
            thoriumPlayer.crystalScorpion = true;
            if (player.ownedProjectileCounts[thorium.ProjectileType("CrystalScorpionMinion")] > 0)
            {
                thoriumPlayer.flatSummonDamage += 3;
            }
            //yumas pendant
            thoriumPlayer.yuma = true;
            //complete set
            thoriumPlayer.throwGuide4 = true;

            //HEALER
            thoriumPlayer.radiantBoost += 0.4f;
            thoriumPlayer.radiantSpeed -= 0.25f;
            thoriumPlayer.healingSpeed += 0.25f;
            thoriumPlayer.radiantCrit += 20;
            //support stash
            thoriumPlayer.supportSash = true;
            thoriumPlayer.quickBelt = true;
            //saving grace
            thoriumPlayer.crossHeal = true;
            thoriumPlayer.healBloom = true;
            //soul guard
            thoriumPlayer.graveGoods = true;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && player2 != player && Vector2.Distance(player2.Center, player.Center) < 400f)
                {
                    player2.AddBuff(thorium.BuffType("AegisAura"), 30, false);
                }
            }
            //archdemon's curse
            thoriumPlayer.darkAura = true;
            //archangels heart
            thoriumPlayer.healBonus += 5;
            //medical bag
            thoriumPlayer.medicalAcc = true;
            float num = 0f;
            int num2 = player.whoAmI;
            for (int i = 0; i < 255; i++)
            {
                if (Main.player[i].active && Main.player[i] != player && !Main.player[i].dead && (Main.player[i].statLifeMax2 - Main.player[i].statLife) > num)
                {
                    num = (Main.player[i].statLifeMax2 - Main.player[i].statLife);
                    num2 = i;
                }
            }
            if (player.ownedProjectileCounts[thorium.ProjectileType("HealerSymbol")] < 1)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, thorium.ProjectileType("HealerSymbol"), 0, 0f, player.whoAmI, 0f, 0f);
            }
            for (int j = 0; j < 1000; j++)
            {
                Projectile projectile = Main.projectile[j];
                if (projectile.active && projectile.owner == player.whoAmI && projectile.type == thorium.ProjectileType("HealerSymbol"))
                {
                    projectile.timeLeft = 2;
                    projectile.ai[1] = num2;
                }
            }
            //BARD
            thoriumPlayer.symphonicDamage += 0.3f;
            thoriumPlayer.symphonicSpeed += .2f;
            thoriumPlayer.symphonicCrit += 15;
            thoriumPlayer.bardResourceMax2 = 20; //the max allowed in thorium
            //epic mouthpiece
            thoriumPlayer.bardHomingBool = true;
            thoriumPlayer.bardHomingBonus = 5f;
            //straight mute
            thoriumPlayer.bardMute2 = true;
            //digital tuner
            thoriumPlayer.tuner2 = true;
            //guitar pick claw
            thoriumPlayer.bardBounceBonus = 5;
            //COLOSSUS
            //terrarium defender
            if (player.statLife < player.statLifeMax * 0.2f)
            {
                player.AddBuff(thorium.BuffType("TerrariumRegen"), 10, true);
                player.lifeRegen += 20;
            }
            if (player.statLife < player.statLifeMax * 0.25f)
            {
                player.AddBuff(thorium.BuffType("TerrariumDefense"), 10, true);
                player.statDefense += 20;
            }
            //blast shield
            thoriumPlayer.blastHurt = true;
            //cape of the survivor
            if (player.FindBuffIndex(thorium.BuffType("Corporeal")) < 0)
            {
                thoriumPlayer.spiritBand2 = true;
            }
            //sweet vengeance
            thoriumPlayer.sweetVengeance = true;
            //oceans retaliation
            thoriumPlayer.turtleShield2 = true;
            thoriumPlayer.SpinyShield = true;
            //TRAWLER
            MagmaBoundFishingLineMP magmaPlayer = player.GetModPlayer<MagmaBoundFishingLineMP>();
            magmaPlayer.magmaLine = true;
            //SUPERSONIC
            //terrarium particle sprinters dust
            if (Collision.SolidCollision(player.position, player.width, player.height + 4) && Math.Abs(player.velocity.X) >= 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 57, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust1 = Main.dust[dust];
                    dust1.velocity *= 0f;
                }
                for (int j = 0; j < 1; j++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 61, 0f, 0f, 100, default(Color), 1.35f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust2 = Main.dust[dust];
                    dust2.velocity *= 0f;
                }
                for (int k = 0; k < 1; k++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 229, 0f, 0f, 100, default(Color), 1.15f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust3 = Main.dust[dust];
                    dust3.velocity *= 0f;
                }
                for (int l = 0; l < 1; l++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 60, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust4 = Main.dust[dust];
                    dust4.velocity *= 0f;
                }
                for (int m = 0; m < 1; m++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 127, 0f, 0f, 100, default(Color), 1.75f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust5 = Main.dust[dust];
                    dust5.velocity *= 0f;
                }
                for (int n = 0; n < 1; n++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 59, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust6 = Main.dust[dust];
                    dust6.velocity *= 0f;
                }
                for (int num7 = 0; num7 < 1; num7++)
                {
                    int dust = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 62, 0f, 0f, 100, default(Color), 1.35f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].noLight = true;
                    Dust dust7 = Main.dust[dust];
                    dust7.velocity *= 0f;
                }
            }
            //air walkers
            if (Soulcheck.GetValue("Air Walkers"))
            {
                if (player.controlDown)
                {
                    jumped = true;
                }
                else
                {
                    jumped = false;
                }
                if (!Collision.SolidCollision(player.position, player.width, player.height + 4))
                {
                    hoverTimer++;
                }
                else
                {
                    hoverTimer = 0;
                }
                if (hoverTimer >= 1000)
                {
                    canHover = false;
                }
                else
                {
                    canHover = true;
                }
                if (canHover && !jumped && !Collision.SolidCollision(player.position, player.width, player.height + 4))
                {
                    player.maxFallSpeed = 0f;
                    player.fallStart = (int)(player.position.Y / 16f);
                    int dust1 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 229, 0f, 0f, 100, default(Color), 1.25f);
                    Main.dust[dust1].noGravity = true;
                    Main.dust[dust1].noLight = true;
                    Dust dust = Main.dust[dust1];
                    dust.velocity *= 0f;
                }
            }
            //survivalist boots
            if (Math.Abs(player.velocity.X) > 2f)
            {
                player.lifeRegen += 2;
                player.lifeRegenTime++;
                player.manaRegenBonus += 2;
                player.manaRegenDelayBonus++;
                thoriumPlayer.bardResourceRecharge += 2;
            }
            //weighted winglets
            if (player.controlDown && !player.controlUp)
            {
                player.maxFallSpeed *= (player.wet ? 2.4f : 1.6f);
            }
            if (player.controlUp && !player.controlDown)
            {
                player.maxFallSpeed *= 0.4f;
                player.fallStart = (int)(player.position.Y / 16f);
            }
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.9f; //0.85f
            ascentWhenRising = 0.3f; //0.15f
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.14f; //0.135f
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = Soulcheck.GetValue("Dimension Speed Boosts") ? 25f : 15f;
            acceleration *= 3.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "UniverseSoul");
            recipe.AddIngredient(null, "DimensionSoul");
            recipe.AddIngredient(null, "TerrariaSoul");

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
