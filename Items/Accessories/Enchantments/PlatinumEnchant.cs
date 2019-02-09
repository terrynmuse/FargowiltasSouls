using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class PlatinumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Platinum Enchantment");

            string tooltip = @"'Its value is immeasurable'
10% chance for enemies to drop 4x loot
If the enemy has Midas, the chance and bonus is doubled";

            if(thorium != null)
            {
                tooltip +=
@"Effects of Platinum Aegis";
//Summons some living glitter to follow you around";
            }

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 4;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).PlatinumEnchant = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            timer++;
            if (timer >= 30)
            {
                int num = 17;
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
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumHelmet);
            recipe.AddIngredient(ItemID.PlatinumChainmail);
            recipe.AddIngredient(ItemID.PlatinumGreaves);
            recipe.AddIngredient(ItemID.PlatinumCrown);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("PlatinumAegis"));
                recipe.AddIngredient(ItemID.DiamondRing);
                recipe.AddIngredient(ItemID.DiamondStaff);
                recipe.AddIngredient(ItemID.WhitePhasesaber);
                recipe.AddIngredient(thorium.ItemType("DiamondButterfly"));
                recipe.AddIngredient(thorium.ItemType("ShinyObject"));
            }
            else
            {
                recipe.AddIngredient(ItemID.DiamondRing);
                recipe.AddIngredient(ItemID.DiamondStaff);
                recipe.AddIngredient(ItemID.WhitePhasesaber);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
