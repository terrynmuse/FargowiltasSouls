using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class GoldEnchant : ModItem
    {
        public float Damage;

        public override bool CloneNewInstances => true;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Enchantment");
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine[] lines = new TooltipLine[9];
            lines[0] = new TooltipLine(mod, "1", "'Gold makes the world go round'");
            lines[1] = new TooltipLine(mod, "2", "Increased damage based on current coin count");
            lines[2] = new TooltipLine(mod, "3", "Current: " + (Damage * 100).ToString("0.00") + "% increased damage");
            lines[2].overrideColor = Color.LimeGreen;
            lines[3] = new TooltipLine(mod, "4", "Picking up gold coins gives you extra life regen or movement speed for a short time");
            lines[4] = new TooltipLine(mod, "5", "You will throw away any lesser valued coins you pick up");
            lines[5] = new TooltipLine(mod, "6", "Increases coin pickup range and shops have lower prices");
            lines[6] = new TooltipLine(mod, "7", "Hitting enemies will sometimes drop extra coins");
            lines[7] = new TooltipLine(mod, "8", "Your attacks inflict Midas");
            lines[8] = new TooltipLine(mod, "9", "Summons a Pet Parrot");

            for (int i = 0; i < lines.Length; i++) tooltips.Add(lines[i]);
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
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.GoldHelmet);
            recipe.AddIngredient(ItemID.GoldChainmail);
            recipe.AddIngredient(ItemID.GoldGreaves);
            recipe.AddIngredient(ItemID.GreedyRing);
            recipe.AddIngredient(ItemID.RubyStaff);
            recipe.AddIngredient(ItemID.SquirrelGold);
            recipe.AddIngredient(ItemID.ParrotCracker);
            
            /*
gold butterfly
GoldAeigis
Proof of Avarice
            */
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
