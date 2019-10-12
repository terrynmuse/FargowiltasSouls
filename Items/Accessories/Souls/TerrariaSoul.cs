using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using System.Collections.Generic;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class TerrariaSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;
        public bool allowJump = true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Terraria");

            string tooltip =
@"'A true master of Terraria'
Summons fireballs, icicles, leaf crystals, hallowed sword and shield, beetles, and several pets
Toggle vanity to remove all Pets, Right Click to Guard
Double tap down to spawn a sentry, call an ancient storm, toggle stealth, spawn a portal, and direct your empowered guardian
Gold Key encases you in gold, Freeze Key freezes time for 5 seconds, minions spew scythes
Solar shield allows you to dash, Dash into any walls, to teleport through them
Throw a smoke bomb to teleport to it and gain the First Strike Buff
Attacks may spawn lightning, flower petals, spectre orbs, a Dungeon Guardian, snowballs, spears, or buff boosters
Attacks cause increased life regen, shadow dodge, Flameburst shots, meteor showers, and reduced enemy knockback immunity
Critical chance is set to 25%, Crit to increase it by 5%, At 100% every 10th attack gains 4% life steal
Getting hit drops your crit back down, inflicts Blood Geyser, releases a spore explosion and reflects damage
Projectiles may split or shatter, item and projectile size increased, attract items from further away
Nearby enemies are ignited, You leave behind a trail of fire and rainbows when you walk
Grants Crimson regen, immunity to fire, fall damage, and lava, and doubled herb collection
Grants 50% chance for Mega Bees, 15% chance for minion crits, 20% chance for bonus loot
Critters have increased defense and their souls will aid you, You may summon temporary minions
All grappling hooks are more effective and fire homing shots, Greatly enhances all DD2 sentries
Your attacks inflict Midas, Enemies explode into needles
When you die, you explode and revive with 200 HP
Effects of Flower Boots, Master Ninja Gear, Greedy Ring, Celestial Shell, and Shiny Stone";


            string tooltip_ch =
@"'真·泰拉之主'
召唤火球, 冰柱, 叶绿水晶, 神圣剑盾, 甲虫和许多宠物
切换可见度以移除所有宠物, 右键防御
双击'下'键生成一个哨兵, 召唤远古风暴, 切换潜行, 生成一个传送门, 指挥你的强化替身
按下金身热键, 使自己被包裹在一个黄金壳中, 按下时间冻结热键时停5秒, 召唤物发出镰刀
日耀护盾允许你双击冲刺, 遇到墙壁自动穿透
扔烟雾弹进行传送, 获得先发制人Buff
攻击可以产生闪电, 花瓣, 幽灵球, 地牢守卫者, 雪球, 长矛或者增益
攻击造成生命回复增加, 暗影闪避, 焰爆射击, 流星雨, 降低敌人的击退免疫
暴击率设为25%, 每次暴击增加5%, 达到100%时, 每10次攻击附带4%生命偷取
被击中会降低暴击率, 使敌人大出血, 释放出孢子爆炸, 并反弹伤害
抛射物可能会分裂或散开, 物品和抛射物尺寸增加, 增加物品拾取范围
点燃附近敌人, 在身后留下火焰路径
获得血腥套的生命回复效果, 免疫火焰, 坠落伤害和岩浆, 药草收获翻倍
蜜蜂有50%概率变为巨型蜜蜂, 召唤物获得15%暴击率, 20%获得额外掉落
大幅增加动物防御力, 它们的灵魂会在死后帮助你, 你有可能召唤临时召唤物
增强所有抓钩, 抓钩会发射追踪射击, 极大增强所有地牢守卫者2(联动的塔防内容)的哨兵
攻击造成点金术, 敌人会爆炸成刺
死亡时爆炸并以200生命值重生
拥有花之靴, 忍者极意, 贪婪戒指, 天界贝壳和闪耀石的效果";

            if (thorium != null)
            {
                tooltip +=
@"Effects of Spring Steps, Slag Stompers, and Proof of Avarice";
                tooltip_ch +=
@"拥有弹簧鞋, 熔渣重踏和贪婪之证的效果";
            }


            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "泰拉之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB));
                }
            }
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            //includes revive, both spectres, adamantite, and star heal
            modPlayer.TerrariaSoul = true;

            //WOOD
            mod.GetItem("WoodForce").UpdateAccessory(player, hideVisual);
            //TERRA
            mod.GetItem("TerraForce").UpdateAccessory(player, hideVisual);
            //EARTH
            mod.GetItem("EarthForce").UpdateAccessory(player, hideVisual);
            //NATURE
            mod.GetItem("NatureForce").UpdateAccessory(player, hideVisual);
            //LIFE
            mod.GetItem("LifeForce").UpdateAccessory(player, hideVisual);
            //SPIRIT
            mod.GetItem("SpiritForce").UpdateAccessory(player, hideVisual);
            //SHADOW
            mod.GetItem("ShadowForce").UpdateAccessory(player, hideVisual);
            //WILL
            mod.GetItem("WillForce").UpdateAccessory(player, hideVisual);
            //COSMOS
            mod.GetItem("CosmoForce").UpdateAccessory(player, hideVisual);
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WoodForce");
            recipe.AddIngredient(null, "TerraForce");
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "ShadowForce");
            recipe.AddIngredient(null, "WillForce");
            recipe.AddIngredient(null, "CosmoForce");

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
