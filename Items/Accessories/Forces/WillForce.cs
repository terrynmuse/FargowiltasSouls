using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class WillForce : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Will");

            string tooltip =
@"'A mind of unbreakable determination'
Your attacks inflict Midas and Super Bleed
Press the Gold hotkey to be encased in a Golden Shell
20% chance for enemies to drop 8x loot
Spears will rain down on struck enemies 
All attacks will slowly remove enemy knockback immunity
Effects of Greedy Ring, Celestial Shell, and Shiny Stone
";
            string tooltip_ch =
@"'坚不可摧的决心'
攻击造成点金手和大出血
按下金身热键,使自己被包裹在一个黄金壳中
敌人20%概率8倍掉落
长矛将倾泄在被攻击的敌人身上
所有的攻击都会缓慢地移除敌人的击退免疫
拥有贪婪戒指,天界贝壳和闪耀石效果
";

            if (thorium != null)
            {
                tooltip += "Effects of Proof of Avarice\n";
                tooltip_ch += "拥有贪婪之证的效果\n";
            }

            tooltip += "Summons several pets";
            tooltip_ch += "召唤数个宠物";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "意志之力");
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
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //super bleed on all, knockback remove for all
            modPlayer.WillForce = true; 
            //midas, greedy ring, pet, zhonyas
            modPlayer.GoldEffect(hideVisual);
            //loot multiply
            modPlayer.PlatinumEnchant = true;
            //javelins and pets
            modPlayer.GladiatorEffect(hideVisual);
            //super bleed, pet
            modPlayer.RedRidingEffect(hideVisual);
            //celestial shell
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
            //knockback kill, pet
            modPlayer.ValhallaEffect(hideVisual);
            player.shinyStone = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);
        }

        public void Thorium(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);

            if (Soulcheck.GetValue("Proof of Avarice"))
            {
                //proof of avarice
                thoriumPlayer.avarice2 = true;
            }

            modPlayer.AddPet("Coin Bag Pet", hideVisual, thorium.BuffType("DrachmaBuff"), thorium.ProjectileType("DrachmaBag"));
            modPlayer.AddPet("Glitter Pet", hideVisual, thorium.BuffType("ShineDust"), thorium.ProjectileType("ShinyPet"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "GoldEnchant");
            recipe.AddIngredient(null, "PlatinumEnchant");
            recipe.AddIngredient(null, "GladiatorEnchant");
            recipe.AddIngredient(null, "RedRidingEnchant");
            recipe.AddIngredient(null, "ValhallaKnightEnchant");

            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}