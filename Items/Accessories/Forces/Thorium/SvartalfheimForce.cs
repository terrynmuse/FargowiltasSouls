using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces.Thorium
{
    public class SvartalfheimForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Svartalfheim");
            Tooltip.SetDefault(
@"'Behold the craftsmanship of the Dark Elves...'
10% increased damage and damage reduction
Immune to intense heat
Attacks have a chance to cause a lightning bolt to strike
Grants the ability to dash into the enemy
Right Click to guard with your shield
A meteor shower initiates every few seconds while attacking
Moving around generates up to 5 static rings, then a bubble of energy will protect you from one attack
Effects of Eye of the Storm, Energized Subwoofer, and Spartan's Subwoofer
Effects of Champion's Rebuttal, Incandescent Spark, and Spiked Bracers
Effects of the Greedy Magnet, Mask of the Crystal Eye, and Abyssal Shell
Summons a pet Omega, I.F.O., and Bio-Feeder");
            DisplayName.AddTranslation(GameCulture.Chinese, "瓦特阿尔海姆之力");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'黑暗精灵的精湛技艺'
增加10%伤害和伤害减免
免疫高温
攻击时概率释放闪电链和闪电箭
获得冲刺能力
右键用盾牌防御
攻击时每隔几秒召唤一次流星雨
移动时产生最多5层静电环, 接着产生一个能量泡保护你免受一次伤害
拥有风暴之眼, 充能音箱, 斯巴达音箱的效果
拥有反击之盾, 食人魔凉鞋和尖刺锁的效果
拥有贪婪磁铁, 水晶之眼和深渊贝壳的效果
召唤宠物欧米茄核心, 天外来客和生化水母");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 11;
            item.value = 600000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //includes bronze lightning
            modPlayer.SvartalfheimForce = true;
            //granite
            player.fireWalk = true;
            player.lavaImmune = true;
            player.buffImmune[24] = true;

            if (Soulcheck.GetValue("Eye of the Storm"))
            {
                //eye of the storm
                thorium.GetItem("EyeoftheStorm").UpdateAccessory(player, hideVisual);
            }
            
            //woofers
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerGranite = true;
                    thoriumPlayer.empowerMarble = true;
                }
            }

            //bronze
            //rebuttal
            thoriumPlayer.championShield = true;

            //durasteel
            mod.GetItem("DurasteelEnchant").UpdateAccessory(player, hideVisual);
            thoriumPlayer.thoriumEndurance -= 0.02f; //meme way to make it 10%

            //titan
            modPlayer.AllDamageUp(.1f);
            //crystal eye mask
            thoriumPlayer.critDamage += 0.1f;
            //abyssal shell
            thoriumPlayer.AbyssalShell = true;

            //conduit
            mod.GetItem("ConduitEnchant").UpdateAccessory(player, hideVisual);
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "GraniteEnchant");
            recipe.AddIngredient(null, "BronzeEnchant");
            recipe.AddIngredient(null, "DurasteelEnchant");
            recipe.AddIngredient(null, "TitanEnchant");
            recipe.AddIngredient(null, "ConduitEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
