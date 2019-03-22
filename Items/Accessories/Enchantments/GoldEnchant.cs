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

            string tooltip =
@"'Gold makes the world go round'
Your attacks inflict Midas
Press the Gold hotkey to be encased in a Golden Shell
You will not be able to move or attack, but will be immune to all damage
";

            if (thorium != null)
            {
                tooltip += 
@"Effects of Gold Aegis, Proof of Avarice, and Greedy Ring
Summons a pet Parrot and Coin Bag";
            }
            else
            {
                tooltip +=
@"Effects of Greedy Ring
Summons a pet Parrot";
            }

            Tooltip.SetDefault(tooltip);
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.AllDamageUp(Damage);
            modPlayer.GoldEffect(hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);
        }

        private void Thorium(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            if (Soulcheck.GetValue("Proof of Avarice"))
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
