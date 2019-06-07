using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class FrostEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frost Enchantment");
            
            string tooltip = 
@"'Let's coat the world in a deep freeze' 
Icicles will start to appear around you
When there are three, attacking will launch them towards the cursor
Your attacks inflict Frostburn
";

            if (thorium != null)
            {
                tooltip +=
@"Effects of Sub-Zero Subwoofer
Summons a pet Snowman";
            }
            else
            {
                tooltip += "Summons a pet Penguin and Snowman";
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
            player.GetModPlayer<FargoPlayer>(mod).FrostEffect(50, hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //subwoofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerFrost = true;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrostHelmet);
            recipe.AddIngredient(ItemID.FrostBreastplate);
            recipe.AddIngredient(ItemID.FrostLeggings);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("FrostSubwoofer"));
                recipe.AddIngredient(ItemID.Frostbrand);
                recipe.AddIngredient(thorium.ItemType("BlizzardsEdge"));
                recipe.AddIngredient(thorium.ItemType("Glacieor"));
                recipe.AddIngredient(ItemID.IceBow);
                recipe.AddIngredient(thorium.ItemType("FreezeRay"));
            }
            else
            {
                recipe.AddIngredient(ItemID.Frostbrand);
                recipe.AddIngredient(ItemID.IceBow);
                recipe.AddIngredient(ItemID.Fish);
            }
            
            recipe.AddIngredient(ItemID.ToySled);

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
