using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod;
using System;
using CalamityMod;
using ThoriumMod.Items.Misc;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class EternitySoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        private readonly Mod dbzMod = ModLoader.GetMod("DBZMOD");
        public bool jumped;
        public bool canHover;
        public int hoverTimer;
        public int jumpTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Eternity");

            String tooltip =
@"'Mortal or Immortal, all things acknowledge your claim to divinity'
Drastically increases life regeneration, increases your maximum mana to 999, minions by 30, sentries by 20, HP by 500%, damage reduction by 50%
250% increased damage and attack speed; 100% increased shoot speed and knockback; Increases armor penetration by 50; Crits deal 10x damage and Crit chance is set to 50%
Crit to increase it by 10%, At 100% every attack gains 10% life steal and you gain +10% damage and +10 defense; This stacks up to 200,000 times until you get hit
All attacks inflict Flames of the Universe, Sadism, Midas, and reduce enemy knockback immunity
Summons icicles, leaf crystals, hallowed sword and shield, beetles, several pets, orichalcum fireballs and all Masochist Mode bosses to your side
Attacks may spawn lightning, flower petals, spectre orbs, a Dungeon Guardian, snowballs, spears, or buff boosters
Attacks cause increased life regen, shadow dodge, Flameburst shots and meteor showers
Projectiles may split or shatter, item and projectile size increased, attacks create additional attacks and spawn hearts
You leave a trail of fire and rainbows; Nearby enemies are ignited; minions occasionally spew scythes, and you may spawn temporary minions
Critters have increased defense and their souls will aid you; Enemies explode into needles; Greatly enhances all DD2 sentries
Double-tap down to spawn a sentry, call an ancient storm, toggle stealth, spawn a portal, and direct your empowered guardian
Right Click to Guard; Press the Gold Key to encase yourself in gold; Press the Freeze Key to freeze time for 5 seconds
Solar shield allows you to dash, dashing into solid blocks teleports you through them; Throw a smoke bomb to teleport to it and gain the first strike buff
Getting hit reflects damage, releases a spore explosion, inflicts super bleeding, may squeak and causes you to erupt into various things when injured
Grants Crimson regen, immunity to fire, fall damage, and lava, doubled herb collection, 50% chance for Mega Bees, 15% chance for minion crits, 20% chance for bonus loot
Grants immunity to knockback and most debuffs; Allows Supersonic running and infinite flight; Increases fishing skill substantially and all fishing rods will have 10 extra lures
You respawn 10x as fast; Prevents boss spawns, increases spawn rates, reduces skeletons hostility outside of the dungeon and empowers Cute Fishron
Grants autofire, modifier protection, gravity control, fast fall, and immunity to knockback, all Masochist Mode debuffs, enhances grappling hooks and more
Increased block and wall placement speed by 50%, Near infinite block placement and mining reach, Mining speed dramatically increased
Summons an impenatrable ring of death around you and you reflect all projectiles; When you die, you explode and revive with full HP
Effects of the Fire Gauntlet, Yoyo Bag, Sniper Scope, Celestial Cuffs, Mana Flower, Brain of Confusion, Star Veil, Sweetheart Necklace, and Bee Cloak
Effects of the Spore Sac, Paladin's Shield, Frozen Turtle Shell, Arctic Diving Gear, Frog Legs, Flying Carpet, Lava Waders, and Angler Tackle Bag
Effects of Paint Sprayer, Presserator, Cell Phone, Gravity Globe, Flower Boots, Master Ninja Gear, Greedy Ring, Celestial Shell, and Shiny Stone
Effects of Shine, Spelunker, Hunter and Dangersense potions; Effects of Builder Mode, Infinity Relic and you attract items from further away";

            String tooltip_ch =
@"'不论凡人或不朽,都承认你的神性'
大幅增加生命回复, 最大法力值增至999 ,+30最大召唤栏, +20最大哨兵栏, 增加500%最大生命值 , 50%伤害减免
增加250%所有类型伤害和攻击速度; 增加100%射击速度与击退; 增加50点护甲穿透; 暴击造成10倍伤害, 暴击率设为50%
每次暴击提高10%, 达到100%时所有攻击附带10%的生命偷取, 增加10%伤害, 增加10防御力; 可叠加200,000次, 直至被攻击
所有攻击造成宇宙之火, 施虐狂, 点金术效果, 并削减敌人的击退免疫
召唤冰柱, 叶绿水晶, 神圣剑盾，甲虫, 数个宠物, 山铜火球和所有受虐模式的Boss到你身边
攻击概率产生闪电, 花瓣, 幽灵球, 地牢守卫者, 雪球, 长矛或者增益
攻击造成生命回复增加, 暗影闪避, 焰爆射击和流星雨
抛射物可能会分裂或散开, 物品和抛射物尺寸增加, 攻击造成额外攻击并生成心
身后留下火焰与彩虹路径; 点燃附近敌人, 召唤物偶尔发射镰刀
大幅增加动物防御力, 它们的灵魂会在死后帮助你; 敌人会爆炸成刺; 极大增强所有地牢守卫者2(联动的塔防内容)的哨兵
双击'下'键生成一个哨兵, 召唤远古风暴, 切换潜行, 生成一个传送门, 指挥你的替身
右键格挡; 按下金身热键, 使自己被包裹在一个黄金壳中; 按下时间冻结热键时停5秒
日耀护盾允许你双击冲刺, 遇到墙壁自动穿透; 扔烟雾弹进行传送, 获得先发制人Buff
受击反弹伤害, 释放包孢子爆炸, 使敌人大出血, 敌人攻击概率无效化, 受伤时爆发各种乱七八糟的玩意
获得血腥套的生命回复效果, 免疫火焰, 坠落伤害和岩浆, 药草收获翻倍, 蜜蜂50%概率变为巨型蜜蜂, 召唤物获得15%暴击率, 20%概率获得额外掉落
免疫击退和诸多Debuff; 允许超音速奔跑和无限飞行; 大幅提升钓鱼能力, 所有钓竿获得额外10个鱼饵
重生速度x10倍；阻止Boss自然生成, 增加刷怪速率, 减少地牢外骷髅的敌意, 增强超可爱猪鲨
武器自动连发, 获得词缀保护, 能够控制重力, 增加掉落速度, 免疫击退和所有受虐模式的Debuff, 增强抓钩以及更多其他效果
增加50%放置物块及墙壁的速度, 近乎无限的放置和采掘距离, 采掘速度加倍
召唤无可阻挡的死亡之环环绕周围, 反弹所有抛射物; 死亡时爆炸并满血复活
拥有烈火手套, 悠悠球袋, 狙击镜, 星体手铐, 魔力花, 混乱之脑, 星辰项链, 甜心项链和蜜蜂斗篷的效果
拥有孢子囊, 圣骑士护盾, 冰霜龟壳, 北极潜水装备, 蛙腿, 飞毯, 熔岩行走靴和渔具包的效果
拥有油漆喷雾器, 促动安装器, 手机, 重力球, 花之靴, 忍者极意, 贪婪戒指, 天界贝壳和闪耀石的效果
获得发光, 探索者, 猎人和危险感知效果; 获得建造模式权限, 拥有无尽遗物的效果, 可以超远程拾取物品";

            if (thorium != null)
            {
                tooltip += @"Effects of Phylactery, Crystal Scorpion, and Yuma's Pendant
                Effects of Guide to Expert Throwing - Volume III, Mermaid's Canteen, and Deadman's Patch
                Effects of Support Sash, Saving Grace, Soul Guard, Archdemon's Curse, Archangel's Heart, and Medical Bag
                Effects of Epic Mouthpiece, Straight Mute, Digital Tuner, and Guitar Pick Claw
                Effects of Ocean's Retaliation and Cape of the Survivor
                Effects of Blast Shield and Terrarium Defender
                Effects of Air Walkers, Survivalist Boots, and Weighted Winglets";

                tooltip_ch += @"拥有魂匣, 魔晶蝎和云码垂饰的效果
                拥有投手大师指导:卷三, 美人鱼水壶和亡者眼罩的效果
                拥有支援腰带, 救世恩典, 灵魂庇佑, 大恶魔之咒, 圣天使之心和医疗包的效果
                拥有史诗吹口, 金属弱音器, 数码调谐器和吉他拨片的效果
                拥有海潮之噬和生存者披风的效果
                拥有爆炸盾和界元之庇护的效果
                拥有履空靴, 我命至上主义者之飞靴和举足轻重靴的效果";
            }

            if (calamity != null)
            {
                tooltip += @"Effects of Elemental Gauntlet, Elemental Quiver, Ethereal Talisman, Statis' Belt of Curses, and Nanotech
Effects of Asgardian Aegis";

                tooltip_ch += @"拥有元素之握, 元素箭袋, 空灵护符, 斯塔提斯的诅咒系带和纳米技术的效果
                拥有阿斯加德之庇护的效果";
            }

            if (dbzMod != null)
            {
                tooltip += "Effects of Zenkai Charm and Aspera Crystallite";

                tooltip_ch += "拥有全开符咒和原始晶粒的效果";
            }


            DisplayName.AddTranslation(GameCulture.Chinese, "永恒之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);

            Tooltip.SetDefault(tooltip);

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
            item.defense = 100;
        }

        public override void UpdateInventory(Player player)
        {
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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //auto use, debuffs, mana up
            modPlayer.Eternity = true;

            //UNIVERSE
            modPlayer.UniverseEffect = true;
            modPlayer.AllDamageUp(2f);
            if (Soulcheck.GetValue("Universe Attack Speed"))
            {
                modPlayer.AttackSpeed *= 2;
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

            //brain of confusion
            player.brainOfConfusion = true;
            //charm of myths
            player.pStone = true;
            //bee cloak, sweet heart necklace, star veil
            if (Soulcheck.GetValue("Stars On Hit"))
            {
                player.starCloak = true;
            }
            if (Soulcheck.GetValue("Bees On Hit"))
            {
                player.bee = true;
            }
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
            if (Soulcheck.GetValue("Supersonic Speed Boosts") && !player.GetModPlayer<FargoPlayer>().noSupersonic)
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
            //magic carpet
            player.carpet = true;
            //frog legs
            player.autoJump = true;
            player.jumpSpeedBoost += 2.4f;
            player.jumpBoost = true;
            //bundle
            if (player.wingTime == 0)
            {
                player.doubleJumpCloud = true;
                player.doubleJumpSandstorm = true;
                player.doubleJumpBlizzard = true;
                player.doubleJumpFart = true;
            }

            //FLIGHT MASTERY
            player.wingTimeMax = 999999;
            player.ignoreWater = true;
            player.wingTime = player.wingTimeMax;

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
            player.pickSpeed -= 0.90f;
            //mining helmet
            if (Soulcheck.GetValue("Shine Buff")) Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            //presserator
            player.autoActuator = true;
            //builder mode
            if (Soulcheck.GetValue("Builder Mode"))
                modPlayer.BuilderMode = true;
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
            mod.GetItem("TerrariaSoul").UpdateAccessory(player, hideVisual);

            //MASOCHIST
            mod.GetItem("MasochistSoul").UpdateAccessory(player, hideVisual);

            //INFINITY
            modPlayer.Infinity = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player, hideVisual);

            if (Fargowiltas.Instance.DBTLoaded) DBT(player);
        }

        private void Thorium(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //phylactery
            if (!thoriumPlayer.lichPrevent)
            {
                player.AddBuff(thorium.BuffType("LichActive"), 60, true);
            }
            //crystal scorpion
            if (Soulcheck.GetValue("Crystal Scorpion"))
            {
                thoriumPlayer.crystalScorpion = true;
            }
            //yumas pendant
            if (Soulcheck.GetValue("Yuma's Pendant"))
            {
                thoriumPlayer.yuma = true;
            }

            //THROWING
            thoriumPlayer.throwGuide2 = true;
            //dead mans patch
            thoriumPlayer.deadEyeBool = true;
            //mermaid canteen
            thoriumPlayer.canteenEffect += 750;
            thoriumPlayer.canteenCadet = true;

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
            //head mirror arrow 
            if (Soulcheck.GetValue("Head Mirror"))
            {
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
            //WORLD SHAPER
            //pets
            modPlayer.AddPet("Inspiring Lantern Pet", hideVisual, thorium.BuffType("SupportLanternBuff"), thorium.ProjectileType("SupportLantern"));
            modPlayer.AddPet("Lock Box Pet", hideVisual, thorium.BuffType("LockBoxBuff"), thorium.ProjectileType("LockBoxPet"));

            //THORIUM SOUL
            mod.GetItem("ThoriumSoul").UpdateAccessory(player, hideVisual);
        }

        private void Calamity(Player player, bool hideVisual)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            //UNIVERSE
            //melee
            modPlayer.eGauntlet = true;
            //removing the extra boosts it adds because meme calamity
            player.meleeDamage -= .15f;
            player.meleeSpeed -= .15f;
            player.meleeCrit -= 5;

            if (Soulcheck.GetValue("Elemental Quiver"))
            {
                //range
                modPlayer.eQuiver = true;
            }

            //magic
            modPlayer.eTalisman = true;
            //summon
            modPlayer.statisBeltOfCurses = true;
            modPlayer.shadowMinions = true;
            modPlayer.tearMinions = true;
            //throw
            modPlayer.nanotech = true;
            //DIMENSIONS
            //tank soul
            //rampart of dieties
            modPlayer.dAmulet = true;
            //becase calamity made it itself for some reason no duplicate
            player.starCloak = false;
            //asgardian aegis
            modPlayer.dashMod = 4;
            modPlayer.elysianAegis = true;
            player.buffImmune[calamity.BuffType("BrimstoneFlames")] = true;
            player.buffImmune[calamity.BuffType("HolyLight")] = true;
            player.buffImmune[calamity.BuffType("GlacialState")] = true;
            //celestial tracers
            modPlayer.IBoots = !hideVisual;
            modPlayer.elysianFire = !hideVisual;
            modPlayer.cTracers = true;
            //TERRARIA
            mod.GetItem("CalamityForce").UpdateAccessory(player, hideVisual);
            //TYRANT
            mod.GetItem("CalamitySoul").UpdateAccessory(player, hideVisual);
        }

        private void DBT(Player player)
        {
            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>(dbzMod);

            dbtPlayer.chargeMoveSpeed = Math.Max(dbtPlayer.chargeMoveSpeed, 2f);
            dbtPlayer.kiKbAddition += 0.4f;
            dbtPlayer.kiDrainMulti -= 0.5f;
            dbtPlayer.kiMaxMult += 0.4f;
            dbtPlayer.kiRegen += 5;
            dbtPlayer.orbGrabRange += 6;
            dbtPlayer.orbHealAmount += 150;
            dbtPlayer.chargeLimitAdd += 8;
            dbtPlayer.flightSpeedAdd += 0.6f;
            dbtPlayer.flightUsageAdd += 3;
            dbtPlayer.zenkaiCharm = true;
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
            recipe.AddIngredient(null, "MasochistSoul");

            if (Fargowiltas.Instance.ThoriumLoaded)
                recipe.AddIngredient(null, "ThoriumSoul");

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(null, "CalamitySoul");
                recipe.AddIngredient(calamity.ItemType("Rock"));
            }
                
            recipe.AddIngredient(null, "Infinity");

            recipe.AddIngredient(null, "Sadism", 30);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
