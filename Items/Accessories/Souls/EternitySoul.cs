using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class EternitySoul : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

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
All weapons have double knockback and have auto swing
Increases your maximum mana to 999, minions by 20, sentries by 10
All attacks inflict Flames of the Universe
400% increased HP, 40% damage reduction, 15 life regeneration
Grants immunity to knockback and most debuffs
Allows Supersonic running and infinite flight
Increases fishing skill substantially, All rods will have 10 extra lures
Summon an impenatrable ring of death around you
When you die, you explode and revive with full HP
All other effects of material Souls");
            //all debuffs soon tm
            /*
Effects of the Yoyo Bag, Sniper Scope, Celestial Cuffs, and Mana Flower
Effects of the Brain of Confusion, Star Veil, Sweetheart Necklace, and Bee Cloak
Effects of Spore Sac, Paladin's Shield, Frozen Turtle Shell, and Arctic Diving Gear
Effects of Frog Legs, Lava Waders, Angler Tackle Bag
and most of SoT not mentioned because meme tooltip length


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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //auto use, debuffs, mana up
            modPlayer.Eternity = true;

            //UNIVERSE
            modPlayer.AllDamageUp(2f);
            if (Soulcheck.GetValue("Universe Speedup"))
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
            }
            else
            {
                player.maxRunSpeed += 5f;
                player.runAcceleration += .1f;
            }
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
            player.autoJump = true;
            player.jumpSpeedBoost += 2.4f;
            player.jumpBoost = true;
            //slime mount
            player.maxFallSpeed += 5f;

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
            if (!Soulcheck.GetValue("Builder Mode"))
            {
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
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.9f; //0.85f
            ascentWhenRising = 0.2f; //0.15f
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
