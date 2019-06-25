using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using System;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class TerraForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Terra Force");
            string tooltip = 
@"'The land lends its strength'
Attacks have a chance to shock enemies with lightning
Sets your critical strike chance to 10%
Every crit will increase it by 5%
Getting hit drops your crit back down
";
            string tooltip_ch =
@"'大地赐予它力量'
攻击有概率用闪电打击敌人
暴击率设为10%
每次暴击增加5%
被击中降低暴击率
";

            if (thorium == null)
            {
                tooltip +=
@"Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
";
                tooltip_ch +=
@"允许玩家向敌人冲刺
右键用盾牌防御
拾取物品半径增大";
            }

            tooltip +=
@"150% increased weapon size
Attacks may inflict enemies with Lead Poisoning
Grants immunity to fire, fall damage, and lava
Increases armor penetration by 5
While standing in lava, you gain 15 more armor penetration, 15% attack speed, and your attacks ignite enemies";
            tooltip_ch +=
@"增加150%武器尺寸
攻击概率使敌人铅中毒
免疫火焰,坠落伤害和岩浆
增加5点护甲穿透
在岩浆中时,再多获得15点护甲穿透,15%攻击速度,攻击会点燃敌人";
                
            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "泰拉之力");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //lightning
            modPlayer.CopperEnchant = true;
            //crit effect improved
            modPlayer.TerraForce = true;
            //crits
            modPlayer.TinEffect();
            //lead poison
            modPlayer.LeadEnchant = true;
            //tungsten
            modPlayer.TungstenEnchant = true;
            //lava immune (obsidian)
            modPlayer.ObsidianEffect();

            //because absorbed somewhere else with thorium
            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                //EoC Shield
                player.dash = 2;
                if (Soulcheck.GetValue("Iron Shield"))
                {
                    //shield
                    modPlayer.IronEffect();
                }
                //magnet
                if (Soulcheck.GetValue("Iron Magnet"))
                {
                    modPlayer.IronEnchant = true;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "CopperEnchant");
            recipe.AddIngredient(null, "TinEnchant");

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "IronEnchant");
            }

            recipe.AddIngredient(null, "LeadEnchant");
            recipe.AddIngredient(null, "TungstenEnchant");
            recipe.AddIngredient(null, "ObsidianEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
