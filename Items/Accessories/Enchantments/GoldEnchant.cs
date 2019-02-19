using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class GoldEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public float Damage;
        public int timer;

        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Enchantment");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine[] lines = new TooltipLine[8]; 
            
            lines[0] = new TooltipLine(mod, "1", "'Gold makes the world go round'");
            lines[1] = new TooltipLine(mod, "2", "Increased damage based on current coin count");
            lines[2] = new TooltipLine(mod, "3", "Current: " + (Damage * 100).ToString("0.00") + "% increased damage");
            lines[2].overrideColor = Color.LimeGreen;

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                lines[3] = new TooltipLine(mod, "4", "You constantly generate a 16 life shield");
                lines[4] = new TooltipLine(mod, "5", "Increases coin pickup range and shops have lower prices");
                lines[5] = new TooltipLine(mod, "6", "Your attacks inflict Midas");
                lines[6] = new TooltipLine(mod, "7", "Effects of Proof of Avarice");
                lines[7] = new TooltipLine(mod, "8", "Summons a pet Coin Bag");
            }
            else
            {
                lines[3] = new TooltipLine(mod, "4", "Increases coin pickup range and shops have lower prices");
                lines[4] = new TooltipLine(mod, "5", "Hitting enemies will sometimes drop extra coins");
                lines[5] = new TooltipLine(mod, "6", "Your attacks inflict Midas");
                lines[6] = new TooltipLine(mod, "7", "Summons a pet Parrot");
            }

            int length = lines.Length;

            if(!Fargowiltas.Instance.ThoriumLoaded)
            {
                length--;
            }

            for (int i = 0; i < length; i++) tooltips.Add(lines[i]);
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

        public static long GetCoins(Player player)
        {
            bool overflowing;
            long inventoryCoins = Utils.CoinsCount(out overflowing, player.inventory, 58, 57, 56, 55, 54);
            long piggyBankCoins = Utils.CoinsCount(out overflowing, player.bank.item);
            long safeCoins = Utils.CoinsCount(out overflowing, player.bank2.item);
            long defendersForgeCoins = Utils.CoinsCount(out overflowing, player.bank3.item);
            long totalCoins = Utils.CoinsCombineStacks(out overflowing, inventoryCoins, piggyBankCoins, safeCoins, defendersForgeCoins);
            return totalCoins;
        }

        public void UpdateDamage(Player player)
        {
            long totalCoins = GetCoins(player);
            int[] coins = Utils.CoinsSplit(totalCoins);
            int plat = coins[3];
            int gold = coins[2];

            if (plat <= 0)
                Damage = coins[2] * .0004f;
            else if (plat < 5) // 1-4 plat
                Damage = .05f;
            else if (plat < 15) // 5-14 plat
                Damage = .10f;
            else if (plat < 30) // 15-29 plat 
                Damage = .15f;
            else if (plat < 50) // 30-49 plat
                Damage = .20f;
            else if (plat < 75) // 50-74 plat
                Damage = .25f;
            else // 75+ plat
                Damage = .30f;
        }

        public override void UpdateInventory(Player player)
        {
            UpdateDamage(player);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            UpdateDamage(player);
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.AllDamageUp(Damage);
            modPlayer.GoldEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);
        }

        private void Thorium(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //proof of avarice
            thoriumPlayer.avarice2 = true;
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

            player.GetModPlayer<FargoPlayer>(mod).AddPet("Coin Bag Pet", hideVisual, thorium.BuffType("DrachmaBuff"), thorium.ProjectileType("DrachmaBag"));
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
                recipe.AddIngredient(ItemID.RubyStaff);
                recipe.AddIngredient(ItemID.GoldButterfly);
                recipe.AddIngredient(ItemID.SquirrelGold);
                recipe.AddIngredient(thorium.ItemType("AncientDrachma"));
            }
            else
            {
                recipe.AddIngredient(ItemID.GreedyRing);
                recipe.AddIngredient(ItemID.RubyStaff);
                recipe.AddIngredient(ItemID.SquirrelGold);
                recipe.AddIngredient(ItemID.ParrotCracker);
            }
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
