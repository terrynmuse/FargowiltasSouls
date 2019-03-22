using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SpiderEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spider Enchantment");

            string tooltip = 
@"'Arachniphobia is punishable by arachnid induced death'
You may summon nearly twice as many spider minions
";

            if(thorium != null)
            {
                tooltip += "Effects of Arachnid's Subwoofer\n";
            }

            tooltip += "Summons a pet Spider";

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 6;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).SpiderEffect(hideVisual);

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
                    thoriumPlayer.empowerVenom = true;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpiderMask);
            recipe.AddIngredient(ItemID.SpiderBreastplate);
            recipe.AddIngredient(ItemID.SpiderGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("VenomSubwoofer"));
                recipe.AddIngredient(thorium.ItemType("Webgun"));
                recipe.AddIngredient(ItemID.SpiderStaff);
                recipe.AddIngredient(ItemID.QueenSpiderStaff);
                recipe.AddIngredient(ItemID.BatScepter);
                recipe.AddIngredient(thorium.ItemType("ZereneButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.SpiderStaff);
                recipe.AddIngredient(ItemID.QueenSpiderStaff);
                recipe.AddIngredient(ItemID.BatScepter);
            }   
            
            recipe.AddIngredient(ItemID.SpiderEgg);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
