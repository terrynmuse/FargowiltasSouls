using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using ThoriumMod;
using System;
using CalamityMod.CalPlayer;
using ThoriumMod.Items.Misc;
using Terraria.Localization;
using System.Collections;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class EternitySoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");
        private readonly Mod dbzMod = ModLoader.GetMod("DBZMOD");
        private readonly Mod soa = ModLoader.GetMod("SacredTools");
        public bool jumped;
        public bool canHover;
        public int hoverTimer;
        //public int jumpTimer;

        public static int tooltipIndex = 0;
        public static int Counter = 10;

        List<String> tooltipsFull = new List<String>();

        String[] vanillaTooltips = new String[]
        {
    "250% increased damage",
    "250% increased attack speed",
    "100% increased shoot speed",
    "100% increased knockback",
    "Increases armor penetration by 50",
    "Crits deal 10x damage",
    "Drastically increases life regeneration",
    "Increases your maximum mana to 999",
    "Increases your maximum minions by 30",
    "Increases your maximum sentries by 20",
    "Increases your maximum HP by 50%",
    "All attacks inflict Flames of the Universe",
    "All attacks inflict Sadism",
    "All attacks inflict Midas",
    "All attacks reduce enemy knockback immunity",
    "Summons fireballs arund you",
    "Summons icicles around you",
    "Summons leaf crystals around you",
    "Summons a hallowed sword and shield",
    "Summons beetles to protect you",
    "Summons a ton of pets",
    "Summons all Masochist Mode bosses to your side ",
    "Attacks may spawn lightning",
    "Attacks may spawn flower petals",
    "Attacks may spawn spectre orbs",
    "Attacks may spawn a Dungeon Guardian",
    "Attacks may spawn snowballs",
    "Attacks may spawn spears",
    "Attacks may spawn hearts",
    "Attacks may spawn buff boosters",
    "Attacks cause increased life regen",
    "Attacks cause shadow dodge",
    "Attacks cause Flameburst shots",
    "Attacks cause Pumpking attacks",
    "Attacks cause Cultist spells",
    "Attacks cause meteor showers",
    "Projectiles may split",
    "Projectiles may shatter",
    "Item and projectile size increased",
    "You leave a trail of fire",
    "You leave a trail of rainbows",
    "Nearby enemies are ignited",
    "Minions occasionally spew scythes",
    "You may spawn temporary minions",
    "Critters have increased defense",
    "Critter's souls may aid you",
    "Enemies explode into needles",
    "Greatly enhances all DD2 sentries",
    "Double-tap down to spawn a palm tree sentry",
    "Double-tap down to call an ancient storm",
    "Double-tap down to toggle stealth",
    "Double-tap down to spawn a portal",
    "Double-tap down to direct your empowered guardian",
    "Right Click to Guard",
    "Press the Gold Key to encase yourself in gold",
    "Press the Freeze Key to freeze time for 5 seconds",
    "Solar shield allows you to dash",
    "Dashing into solid blocks teleports you through them",
    "Throw a smoke bomb to teleport to it and gain the first strike buff",
    "Getting hit reflects damage",
    "Getting hit releases a spore explosion",
    "Getting hit inflicts Blood Geyser",
    "Getting hit may squeak",
    "Getting hit causes you to erupt into spiky balls",
    "Getting hit causes you to erupt into Ancient Visions",
    "Grants Crimson regen",
    "Grants immunity to fire",
    "Grants immunity to fall damage",
    "Grants immunity to lava",
    "Grants immunity to knockback",
    "Grants immunity to most debuffs", //expand?? ech
	"Grants doubled herb collection",
    "Grants 50% chance for Mega Bees",
    "15% chance for minion crits",
    "20% chance for bonus loot",
    "Allows Supersonic running and ",
    "Allows infinite flight",
    "Increases fishing skill substantially",
    "All fishing rods will have 10 extra lures",
    "You respawn 10x as fast",
    "Prevents boss spawns",
    "Increases spawn rates",
    "Reduces skeletons hostility outside of the dungeon",
    "Empowers Cute Fishron",
    "Grants autofire",
    "Grants modifier protection",
    "Grants gravity control",
    "Grants fast fall",
    "Enhances grappling hooks",
    "You attract items from further away",
    "Increased block and wall placement speed by 50%",
    "Near infinite block placement",
    "Near infinite mining reach",
    "Mining speed dramatically increased",
    "You reflect all projectiles",
    "When you die, you explode",
    "When you die, you revive with full HP",
    "Effects of Fire Gauntlet",
    "Effects of Yoyo Bag",
    "Effects of Sniper Scope",
    "Effects of Celestial Cuffs",
    "Effects of Mana Flower",
    "Effects of Brain of Confusion",
    "Effects of Star Veil",
    "Effects of Sweetheart Necklace",
    "Effects of Bee Cloak",
    "Effects of Spore Sac",
    "Effects of Paladin's Shield",
    "Effects of Frozen Turtle Shell",
    "Effects of Arctic Diving Gear",
    "Effects of Frog Legs",
    "Effects of Flying Carpet",
    "Effects of Lava Waders",
    "Effects of Angler Tackle Bag",
    "Effects of Paint Sprayer",
    "Effects of Presserator",
    "Effects of Cell Phone",
    "Effects of Flower Boots",
    "Effects of Master Ninja Gear",
    "Effects of Greedy Ring",
    "Effects of Celestial Shell",
    "Effects of Shiny Stone",
    "Effects of Spelunker potion",
    "Effects of Dangersense potion",
    "Effects of Hunter potion",
    "Effects of Shine potion",
    "Effects of Builder Mode"
        };

        String[] thoriumTooltips = new String[]
        {
            "Armor bonuses from Living Wood",
            "Armor bonuses from Life Bloom",
            "Armor bonuses from Yew Wood",
            "Armor bonuses from Tide Hunter",
            "Armor bonuses from Icy",
            "Armor bonuses from Cryo Magus",
            "Armor bonuses from Whispering",
            "Armor bonuses from Sacred",
            "Armor bonuses from Warlock",
            "Armor bonuses from Biotech",
            "Armor bonuses from Cyber Punk",
            "Armor bonuses from Conductor",
            "Armor bonuses from Bronze",
            "Armor bonuses from Darksteel",
            "Armor bonuses from Durasteel",
            "Armor bonuses from Conduit",
            "Armor bonuses from Lodestone",
            "Armor bonuses from Illumite",
            "Armor bonuses from Jester",
            "Armor bonuses from Thorium",
            "Armor bonuses from Terrarium",
            "Armor bonuses from Malignant",
            "Armor bonuses from Folv",
            "Armor bonuses from White Dwarf",
            "Armor bonuses from Celestial",
            "Armor bonuses from Spirit Trapper",
            "Armor bonuses from Dragon",
            "Armor bonuses from Dread",
            "Armor bonuses from Flesh",
            "Armor bonuses from Demon Blood",
            "Armor bonuses from Tide Turner",
            "Armor bonuses from Assassin",
            "Armor bonuses from Pyromancer",
            "Armor bonuses from Dream Weaver",
            "Effects of Flawless Chrysalis",
            "Effects of Bubble Magnet",
            "Effects of Agnor's Bowl",
            "Effects of Ice Bound Strider Hide",
            "Effects of Ring of Unity",
            "Effects of Mix Tape",
            "Effects of Eye of the Storm",
            "Effects of Champion's Rebuttal",
            "Effects of Incandescent Spark",
            "Effects of Greedy Magnet",
            "Effects of Abyssal Shell",
            "Effects of Astro-Beetle Husk",
            "Effects of Eye of the Beholder",
            "Effects of Crietz",
            "Effects of Mana-Charged Rocketeers",
            "Effects of Inner Flame",
            "Effects of Crash Boots",
            "Effects of Vampire Gland",
            "Effects of Demon Blood Badge",
            "Effects of Lich's Gaze",
            "Effects of Plague Lord's Flask",
            "Effects of Phylactery",
            "Effects of Crystal Scorpion",
            "Effects of Yuma's Pendant",
            "Effects of Guide to Expert Throwing - Volume III",
            "Effects of Mermaid's Canteen",
            "Effects of Deadman's Patch",
            "Effects of Support Sash",
            "Effects of Saving Grace",
            "Effects of Soul Guard",
            "Effects of Archdemon's Curse",
            "Effects of Archangel's Heart",
            "Effects of Medical Bag",
            "Effects of Epic Mouthpiece",
            "Effects of Straight Mute",
            "Effects of Digital Tuner",
            "Effects of Guitar Pick Claw",
            "Effects of Ocean's Retaliation",
            "Effects of Cape of the Survivor",
            "Effects of Blast Shield",
            "Effects of Terrarium Defender",
            "Effects of Air Walkers",
            "Effects of Survivalist Boots",
            "Effects of Weighted Winglets"
        };

        String[] calamityTooltips = new String[]
        {
            "Armor bonuses from Aerospec",
            "Armor bonuses from Statigel",
            "Armor bonuses from Daedalus",
            "Armor bonuses from Bloodflare",
            "Armor bonuses from Victide",
            "Armor bonuses from Xeroc",
            "Armor bonuses from Omega Blue",
            "Armor bonuses from God Slayer",
            "Armor bonuses from Silva",
            "Armor bonuses from Auric Tesla",
            "Armor bonuses from Mollusk",
            "Armor bonuses from Reaver",
            "Armor bonuses from Ataxia",
            "Armor bonuses from Astral",
            "Armor bonuses from Tarragon",
            "Armor bonuses from Demonshade",
            "Effects of Spirit Glyph",
            "Effects of Raider's Talisman",
            "Effects of Trinket of Chi",
            "Effects of Gladiator's Locket",
            "Effects of Unstable Prism",
            "Effects of Counter Scarf",
            "Effects of Fungal Symbiote",
            "Effects of Permafrost's Concoction",
            "Effects of Regenator",
            "Effects of Core of the Blood God",
            "Effects of Affliction",
            "Effects of Deep Dive",
            "Effects of The Transformer",
            "Effects of Luxor's Gift",
            "Effects of The Community",
            "Effects of Abyssal Diving Suit",
            "Effects of Lumenous Amulet",
            "Effects of Aquatic Emblem",
            "Effects of Nebulous Core",
            "Effects of Draedon's Heart",
            "Effects of The Amalgam",
            "Effects of Godly Soul Artifact",
            "Effects of Yharim's Gift",
            "Effects of Heart of the Elements",
            "Effects of The Sponge",
            "Effects of Giant Pearl",
            "Effects of Amidias' Pendant",
            "Effects of Fabled Tortoise Shell",
            "Effects of Plague Hive",
            "Effects of Astral Arcanum",
            "Effects of Hide of Astrum Deus",
            "Effects of Profaned Soul Artifact",
            "Effects of Dark Sun Ring",
            "Effects of Elemental Gauntlet",
            "Effects of Elemental Quiver",
            "Effects of Ethereal Talisman",
            "Effects of Statis' Belt of Curses",
            "Effects of Nanotech",
            "Effects of Asgardian Aegis"
        };

        String[] dbtTooltips = new String[]
        {
            "Effects of Zenkai Charm",
            "Effects of Aspera Crystallite"
        };

        String[] soaTooltips = new String[]
        {
            "Armor bonuses from Bismuth",
            "Armor bonuses from Frosthunter",
            "Armor bonuses from Blightbone",
            "Armor bonuses from Dreadfire",
            "Armor bonuses from Space Junk",
            "Armor bonuses from Marstech",
            "Armor bonuses from Blazing Brute",
            "Armor bonuses from Cosmic Commander",
            "Armor bonuses from Nebulous Apprentic",
            "Armor bonuses from Stellar Priest",
            "Armor bonuses from Fallen Prince",
            "Armor bonuses from Void Warden",
            "Armor bonuses from Vulcan Reaper",
            "Armor bonuses from Flarium",
            "Armor bonuses from Asthraltite",
            "Effects of Dreadflame Emblem",
            "Effects of Lapis Pendant",
            "Effects of Frigid Pendant",
            "Effects of Pumpkin Amulet",
            "Effects of Nuba's Blessing",
            "Effects of Novaniel's Resolve",
            "Effects of Celestial Ring",
            "Effects of Ring of the Fallen",
            "Effects of Memento Mori",
            "Effects of Arcanum of the Caster"
        };

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Eternity");

            //oh no idk even for translate 
            String tooltip_ch =
@"'不论凡人或不朽, 都承认你的神性'
大幅增加生命回复, 最大法力值增至999 ,+30最大召唤栏, +20最大哨兵栏, 增加500%最大生命值 , 50%伤害减免
增加250%所有类型伤害和攻击速度; 增加100%射击速度与击退; 增加50点护甲穿透; 暴击造成10倍伤害, 暴击率设为50%
每次暴击提高10%, 达到100%时所有攻击附带10%的生命偷取, 增加10%伤害, 增加10防御力; 可叠加200,000次, 直至被攻击
所有攻击造成宇宙之火, 施虐狂, 点金术效果, 并削减敌人的击退免疫
召唤冰柱, 叶绿水晶, 神圣剑盾，甲虫, 数个宠物, 山铜火球和所有受虐模式的Boss到你身边
攻击概率产生闪电, 花瓣, 幽灵球, 地牢守卫者, 雪球, 长矛或者增益
攻击造成生命回复增加, 暗影闪避, 焰爆射击和流星雨
抛射物可能会分裂或散开, 物品和抛射物尺寸增加, 攻击造成额外攻击并生成心
身后留下火焰与彩虹路径; 点燃附近敌人; 召唤物偶尔发射镰刀, 有概率生成临时召唤物
大幅增加动物防御力, 它们的灵魂会在死后帮助你; 敌人会爆炸成刺; 极大增强所有地牢守卫者2(联动的塔防内容)的哨兵
双击'下'键生成一个哨兵, 召唤远古风暴, 切换潜行, 生成一个传送门, 指挥你的替身
右键格挡; 按下金身热键, 使自己被包裹在一个黄金壳中; 按下时间冻结热键时停5秒
日耀护盾允许你双击冲刺, 遇到墙壁自动穿透; 扔烟雾弹进行传送, 获得先发制人Buff
受击反弹伤害, 释放包孢子爆炸, 使敌人大出血, 敌人攻击概率无效化, 受伤时爆发各种乱七八糟的玩意
获得血腥套的生命回复效果, 免疫火焰, 坠落伤害和岩浆, 药草收获翻倍, 蜜蜂50%概率变为巨型蜜蜂, 召唤物获得15%暴击率, 20%概率获得额外掉落
免疫击退和诸多Debuff; 允许超音速奔跑和无限飞行; 大幅提升钓鱼能力, 所有钓竿获得额外10个鱼饵
重生速度x10倍；阻止Boss自然生成, 增加刷怪速率, 减少地牢外骷髅的敌意, 增强超可爱猪鲨
武器自动连发, 获得词缀保护, 能够控制重力, 增加掉落速度, 免疫击退和所有受虐模式的Debuff, 增强抓钩以及更多其他效果
增加50%放置物块及墙壁的速度, 近乎无限的放置和采掘距离, 极大提高采掘速度
召唤无可阻挡的死亡之环环绕周围, 反弹所有抛射物; 死亡时爆炸并满血复活
拥有烈火手套, 悠悠球袋, 狙击镜, 星体手铐, 魔力花, 混乱之脑, 星辰项链, 甜心项链和蜜蜂斗篷的效果
拥有孢子囊, 圣骑士护盾, 冰霜龟壳, 北极潜水装备, 蛙腿, 飞毯, 熔岩行走靴和渔具包的效果
拥有油漆喷雾器, 促动安装器, 手机, 重力球, 花之靴, 忍者极意, 贪婪戒指, 天界贝壳和闪耀石的效果
获得发光, 探索者, 猎人和危险感知效果; 获得建造模式权限, 拥有无尽遗物的效果, 可以超远程拾取物品";
            String tooltip_sp = @"'Mortal o Inmortal, todas las cosas reconocen tu reclamación a la divinidad'
Drasticamente incrementa regeneración de vida, incrementa tu mana máximo a 999, súbditos por 30, torretas por 20, vida maxima por 500%, reducción de daño por 50%
250% daño incrementado y velocidad de ataque; 100% velocidad de disparo y retroceso; Incrementa penetración de armadura por 50; Críticos hacen 10x daño y la probabilidad de Crítico se vuelve 50%
Consigue un crítico para incrementarlo por 10%, a 100% cada ataque gana 10% robo de vida y ganas +10% daño y +10 defensa; Esto se apila hasta 200,000 veces hasta que te golpeen
Todos los ataques inflijen Llamas del Universo, Sadismo, Midas y reduce inmunidad de retroceso de los enemigos
Invoca estalactitas, un cristal de hojas, espada y escudo benditos, escarabajos, varias mascotas, bolas de fuego de oricalco y todos los jefes del Modo Masoquista a tu lado
Ataques pueden crear rayos, pétalos, orbes espectrales, un Guardián de la mazmorra, bolas de nieve, lanzas, o potenciadores
Ataques provocan regeneración de vida incrementada, Sombra de Esquivo, explociones de llamas y lluvias de meteoros
Projectiles pueden dividirse o quebrarse, tamaño de objetos y projectiles incrementado, ataques crean ataques adicionales y corazones
Dejas un rastro de fuego y arcoirises; Enemigos cercanos son incinerados y súbditos escupen guadañas ocasionalmente
Animales tienen defensa incrementada y sus almas te ayudarán; Enemigos explotan en agujas; Mejora todas las torretas DD2 grandemente
Tocar dos veces abajo para invocar una torreta, llamar a una tormenta antigua, activar sigilo, invocar un portal, y dirigir tu guardián
Click derecho para Defender; Presiona la Llave dorada para encerrarte en oro; Presiona la Llave congelada para congelar el tiempo por 5 segundos
Escudo de bengala solar te permite embestir, embestir bloques sólidos te teletransporta a través de ellos; Tira una bomba de huma para teletransportarte a ella y obtener el buff de primer golpe
Ser golpeado refleja el daño, suelta una exploción de esporas, inflije super sangrado, puede chillar y causa que erupciones en varias cosas cuando seas dañado
Otorga regeneración carmesí, inmunidad al fuego, daño por caída, y lava, duplica la colección de hierbas
50% probabilidad de Abejas gigantes, 15% probabilidad de críticos de súbditos, 20% probabilidad de botín extra
Otorga inmunidad a la mayoría de estados alterados; Permite velocidad Supersónica y vuelo infinito; Incrementa poder de pesca substancialmente y todas las cañas de pescar tienen 10 señuelos extra 
Revives 10x más rapido; Evita invocación de jefes, incrementa generación de enemigos, reduce la hostilidad de esqueletos fuera de la mazmorra y fortalece a Cute Fishron
Otorga ataque continuo, protección de modificadores, control de gravedad, caída rápida, e inmunidad a retroceso, todos los estados alterados del Modo Masoquista, mejora ganchos y más
Incrementa velocidad de colocación de bloques y paredes por 50%, Casi infinito alcance de colocación de bloques y alcance de minar, Velocidad de minería duplicada 
Invoca un anillo de la muerte inpenetrable alrededor de tí y tu reflejas todos los projectiles; Cuando mueres, explotas y revives con vida al máximo
Efectos del Guantelete de fuego, Bolsa yoyó, Mira de francotirador, Esposas celestiales, Flor de maná, Cerebro de confusión, Velo estelar, Collar del cariño, y Capa de abejas
Efectos del Saco de esporas, Escudo de paladín, Caparazón de tortuga congelado, Equipo de buceo ártico, Anca de rana, Alfombra voladora, Katiuskas de lava, y Bolsa de aparejos de pescador
Efectos del Spray de pintura, Pulsificador, Móvil, Globo gravitacional, Botas floridas, Equipo de maestro ninja, Anillo codicioso, Caparazón celestial, y Piedra brillante
Efectos de pociones de Brillo, Espeleólogo, Cazador, y Sentido del peligro; Efectos del Modo Constructor, Reliquia del Infinito y atraes objectos desde más lejos";

            if (thorium != null)
            {

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

                tooltip_ch += @"拥有元素之握, 元素箭袋, 空灵护符, 斯塔提斯的诅咒系带和纳米技术的效果
                拥有阿斯加德之庇护的效果";
            }

            if (dbzMod != null)
            {

                tooltip_ch += "拥有全开符咒和原始晶粒的效果";
            }

            DisplayName.AddTranslation(GameCulture.Chinese, "永恒之魂");
            DisplayName.AddTranslation(GameCulture.Spanish, "Alma de la Eternidad");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
            Tooltip.AddTranslation(GameCulture.Spanish, tooltip_sp);

            Tooltip.SetDefault(
@"'Mortal or Immortal, all things acknowledge your claim to divinity'
Crit chance is set to 50%
Crit to increase it by 10%
At 100% every attack gains 10% life steal
You also gain +10% damage and +10 defense
This stacks up to 200,000 times until you get hit
Additionally grants:");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltipsFull.AddRange(vanillaTooltips);

            if (thorium != null)
            {
                tooltipsFull.AddRange(thoriumTooltips);
            }

            if (calamity != null)
            {
                tooltipsFull.AddRange(calamityTooltips);
            }

            if (dbzMod != null)
            {
                tooltipsFull.AddRange(dbtTooltips);
            }

            if (soa != null)
            {
                tooltipsFull.AddRange(soaTooltips);
            }

            tooltips.Add(new TooltipLine(mod, "tooltip", tooltipsFull[tooltipIndex]));

            Counter--;

            if (Counter <= 0)
            {
                tooltipIndex = Main.rand.Next(tooltipsFull.Count);

                Counter = 10;
            }
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //auto use, debuffs, mana up
            modPlayer.Eternity = true;

            //UNIVERSE
            modPlayer.UniverseEffect = true;
            modPlayer.AllDamageUp(2f);
            if (SoulConfig.Instance.GetValue("Universe Attack Speed"))
            {
                modPlayer.AttackSpeed *= 2;
            }
            player.maxMinions += 20;
            player.maxTurrets += 10;
            //accessorys
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;
            if (SoulConfig.Instance.GetValue("Universe Scope"))
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
            if (SoulConfig.Instance.GetValue("Stars On Hit"))
            {
                player.starCloak = true;
            }
            if (SoulConfig.Instance.GetValue("Bees On Hit"))
            {
                player.bee = true;
            }
            player.panic = true;
            player.longInvince = true;
            //spore sac
            if (SoulConfig.Instance.GetValue("Spore Sac"))
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
            if (SoulConfig.Instance.GetValue("Supersonic Speed Boosts") && !player.GetModPlayer<FargoPlayer>().noSupersonic)
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
            if (SoulConfig.Instance.GetValue("Mining Shine Buff")) Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            //presserator
            player.autoActuator = true;
            //royal gel
            player.npcTypeNoAggro[1] = true;
            player.npcTypeNoAggro[16] = true;
            player.npcTypeNoAggro[59] = true;
            player.npcTypeNoAggro[71] = true;
            player.npcTypeNoAggro[81] = true;
            player.npcTypeNoAggro[138] = true;
            player.npcTypeNoAggro[121] = true;
            player.npcTypeNoAggro[122] = true;
            player.npcTypeNoAggro[141] = true;
            player.npcTypeNoAggro[147] = true;
            player.npcTypeNoAggro[183] = true;
            player.npcTypeNoAggro[184] = true;
            player.npcTypeNoAggro[204] = true;
            player.npcTypeNoAggro[225] = true;
            player.npcTypeNoAggro[244] = true;
            player.npcTypeNoAggro[302] = true;
            player.npcTypeNoAggro[333] = true;
            player.npcTypeNoAggro[335] = true;
            player.npcTypeNoAggro[334] = true;
            player.npcTypeNoAggro[336] = true;
            player.npcTypeNoAggro[537] = true;
            //builder mode
            if (SoulConfig.Instance.GetValue("Builder Mode"))
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

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player, hideVisual);

            if (Fargowiltas.Instance.DBTLoaded) DBT(player);

            if (Fargowiltas.Instance.SOALoaded) SOA(player, hideVisual);

            if (Fargowiltas.Instance.ApothLoaded)
            {
                ModLoader.GetMod("ApothTestMod").GetItem("Ataraxia").UpdateAccessory(player, hideVisual);
            }
        }

        private void Thorium(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //phylactery
            if (!thoriumPlayer.lichPrevent)
            {
                player.AddBuff(thorium.BuffType("LichActive"), 60, true);
            }
            //crystal scorpion
            if (SoulConfig.Instance.GetValue("Crystal Scorpion"))
            {
                thoriumPlayer.crystalScorpion = true;
            }
            //yumas pendant
            if (SoulConfig.Instance.GetValue("Yuma's Pendant"))
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
            if (SoulConfig.Instance.GetValue("Head Mirror"))
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
            //air walkers
            if (SoulConfig.Instance.GetValue("Air Walkers"))
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
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            //UNIVERSE
            //melee
            modPlayer.eGauntlet = true;
            //removing the extra boosts it adds because meme calamity
            player.meleeDamage -= .15f;
            player.meleeSpeed -= .15f;
            player.meleeCrit -= 5;

            if (SoulConfig.Instance.GetValue("Elemental Quiver"))
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
            //TYRANT
            mod.GetItem("CalamitySoul").UpdateAccessory(player, hideVisual);
        }

        private void DBT(Player player)
        {
            DBZMOD.MyPlayer dbtPlayer = player.GetModPlayer<DBZMOD.MyPlayer>();

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

        private void SOA(Player player, bool hideVisual)
        {
            mod.GetItem("GenerationsForce").UpdateAccessory(player, hideVisual);
            mod.GetItem("SoranForce").UpdateAccessory(player, hideVisual);
            mod.GetItem("SyranForce").UpdateAccessory(player, hideVisual);
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
            speed = SoulConfig.Instance.GetValue("Dimension Speed Boosts") ? 25f : 15f;
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

            if (Fargowiltas.Instance.SOALoaded)
            {
                recipe.AddIngredient(null, "SoASoul");
            }

            if (Fargowiltas.Instance.ApothLoaded)
            {
                recipe.AddIngredient(ModLoader.GetMod("ApothTestMod").ItemType("Ataraxia"));
            }

            recipe.AddIngredient(null, "Sadism", 30);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
