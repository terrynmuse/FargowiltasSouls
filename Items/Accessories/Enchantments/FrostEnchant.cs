using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;
using System.Collections.Generic;

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
            string tooltip_ch =
@"'让我们给世界披上一层厚厚的冰衣'
周围将出现冰柱
当冰柱达到三个时,攻击会将它们向光标位置发射
攻击造成寒焰效果
";

            if (thorium != null)
            {
                tooltip +=
@"Effects of Sub-Zero Subwoofer
Summons a pet Snowman";
                tooltip_ch +=
@"拥有零度音箱的效果
召唤一个小雪人";
            }
            else
            {
                tooltip += "Summons a pet Penguin and Snowman";
                tooltip_ch += "召唤一个宠物企鹅和小雪人";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "霜冻魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(122, 189, 185);
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
            player.GetModPlayer<FargoPlayer>().FrostEffect(50, hideVisual);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
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
