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

            /*if(thorium != null)
            {
                tooltip +=
@"Your symphonic damage empowers all nearby allies with: Spider Bite
Damage done against envenomed enemies is increased by 8%
Doubles the range of your empowerments effect radius
";
            }*/

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

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.subwooferVenom = true;
            thoriumPlayer.bardRangeBoost += 450;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SpiderMask);
            recipe.AddIngredient(ItemID.SpiderBreastplate);
            recipe.AddIngredient(ItemID.SpiderGreaves);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("VenomSubwoofer"));
                recipe.AddIngredient(thorium.ItemType("Webgun"));
                recipe.AddIngredient(ItemID.SpiderStaff);
                recipe.AddIngredient(ItemID.QueenSpiderStaff);
                recipe.AddIngredient(ItemID.BatScepter);
                recipe.AddIngredient(thorium.ItemType("ZereneButterfly"));
            }
            else
            {*/
                recipe.AddIngredient(ItemID.SpiderStaff);
                recipe.AddIngredient(ItemID.QueenSpiderStaff);
                recipe.AddIngredient(ItemID.BatScepter);
            //}   
            
            recipe.AddIngredient(ItemID.SpiderEgg);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
