using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class JungleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Jungle Enchantment");

            string tooltip = 
@"'The wrath of the jungle dwells within'
25% chance to steal 4 mana with each attack
Taking damage will release a poisoning spore explosion
Spore damage scales with magic damage";

            if(thorium != null)
                tooltip += "Effects of Toxic Subwoofer";
            else
                tooltip += "Effects of Guide to Plant Fiber Cordage";

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 50000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).JungleEffect();

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerPoison = true;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.JungleHat);
            recipe.AddIngredient(ItemID.JungleShirt);
            recipe.AddIngredient(ItemID.JunglePants);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("PoisonSubwoofer"));
                recipe.AddIngredient(ItemID.JungleRose);
                recipe.AddIngredient(ItemID.ThornChakram);
                recipe.AddIngredient(ItemID.Boomstick);
                
                
                recipe.AddIngredient(ItemID.Frog);
                recipe.AddIngredient(thorium.ItemType("JungleSporeButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.CordageGuide);
                recipe.AddIngredient(ItemID.JungleRose);
                recipe.AddIngredient(ItemID.ThornChakram);
                recipe.AddIngredient(ItemID.Frog);
            }
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
