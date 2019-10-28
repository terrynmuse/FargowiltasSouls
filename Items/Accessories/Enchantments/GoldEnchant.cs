using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class GoldEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Enchantment");

            string tooltip =
@"'Gold makes the world go round'
Your attacks inflict Midas
Press the Gold hotkey to be encased in a Golden Shell
You will not be able to move or attack, but will be immune to all damage
";
            string tooltip_ch =
@"'黄金使世界运转'
攻击造成点金手效果
按下金身热键,使自己被包裹在一个黄金壳中
你将不能移动或攻击,但免疫所有伤害
";

            if (thorium != null)
            {
                tooltip += 
@"Effects of Gold Aegis, Proof of Avarice, and Greedy Ring
Summons a pet Parrot and Coin Bag";
                tooltip_ch +=
@"拥有金之庇护,贪婪之证和贪婪戒指的效果
召唤一个宠物鹦鹉和钱币袋";
            }
            else
            {
                tooltip +=
@"Effects of Greedy Ring
Summons a pet Parrot";
                tooltip_ch +=
@"拥有贪婪戒指的效果
召唤一个宠物鹦鹉";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "黄金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(231, 178, 28);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 5;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.GoldEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);
        }

        private void Thorium(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            if (SoulConfig.Instance.GetValue("Proof of Avarice"))
            {
                //proof of avarice
                thoriumPlayer.avarice2 = true;
            }
            
            //shield
            timer++;
            if (timer >= 30)
            {
                int num = 16;
                if (thoriumPlayer.shieldHealth <= num)
                {
                    thoriumPlayer.shieldHealthTimerStop = true;
                }
                if (thoriumPlayer.shieldHealth < num)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 255, 255), 1, false, true);
                    thoriumPlayer.shieldHealth++;
                    player.statLife++;
                }
                timer = 0;
            }

            player.GetModPlayer<FargoPlayer>().AddPet("Coin Bag Pet", hideVisual, thorium.BuffType("DrachmaBuff"), thorium.ProjectileType("DrachmaBag"));
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldHelmet);
            recipe.AddIngredient(ItemID.GoldChainmail);
            recipe.AddIngredient(ItemID.GoldGreaves);
           
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("GoldAegis"));
                recipe.AddIngredient(thorium.ItemType("ProofAvarice"));
                recipe.AddIngredient(ItemID.GreedyRing);
                recipe.AddIngredient(ItemID.CoinGun);
                recipe.AddIngredient(ItemID.SquirrelGold);
                recipe.AddIngredient(ItemID.ParrotCracker);
                recipe.AddIngredient(thorium.ItemType("AncientDrachma"));
            }
            else
            {
                recipe.AddIngredient(ItemID.GreedyRing);
                recipe.AddIngredient(ItemID.CoinGun);
                recipe.AddIngredient(ItemID.SquirrelGold);
                recipe.AddIngredient(ItemID.ParrotCracker);
            }
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
