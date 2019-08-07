using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class ChlorophyteEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Chlorophyte Enchantment");

            string tooltip =
@"'The jungle's essence crystallizes above you'
Summons a leaf crystal to shoot at nearby enemies
Chance to steal 4 mana with each attack
Taking damage will release a poisoning spore explosion
All herb collection is doubled
";
            string tooltip_ch = 
@"'丛林的精华在你上方结晶'
召唤一个叶绿水晶射击附近的敌人
每次攻击有机会偷取4点法力
受到伤害时会释放出有毒的孢子爆炸
所有草药收集翻倍
";

            if(thorium != null)
            {
                tooltip +=
@"Your attacks have a chance to poison hit enemies with a spore cloud
Effects of Night Shade Petal, Petal Shield, Toxic Subwoofer, and Flower Boots
";
                tooltip_ch +=
@"攻击有概率释放孢子云使敌人中毒
拥有影缀花,花之盾,剧毒音箱和花之靴的效果
";
            }
            else
            {
                tooltip += "Effects of Guide to Plant Fiber Cordage and Flower Boots\n";
                tooltip_ch += "拥有植物纤维绳索指南的效果\n";
            }

            tooltip += "Summons a pet Seedling";
            tooltip_ch += "召唤一颗宠物幼苗";

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "叶绿魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 150000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            //crystal and pet
            modPlayer.ChloroEffect(hideVisual, 100);
            //herb double and bulb effect with thorium
            modPlayer.ChloroEnchant = true;
            modPlayer.FlowerBoots();
            modPlayer.JungleEffect();

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player, hideVisual);
        }

        private void Thorium(Player player, bool hideVisual)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //subwoofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerPoison = true;
                }
            }
            //petal shield
            thorium.GetItem("PetalShield").UpdateAccessory(player, hideVisual);
            player.statDefense -= 2;
            //night shade petal
            thoriumPlayer.nightshadeBoost = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddRecipeGroup("FargowiltasSouls:AnyChloroHead");
            recipe.AddIngredient(ItemID.ChlorophytePlateMail);
            recipe.AddIngredient(ItemID.ChlorophyteGreaves);
            recipe.AddIngredient(null, "JungleEnchant");
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(null, "BulbEnchant");
                recipe.AddIngredient(ItemID.FlowerBoots);
                recipe.AddIngredient(ItemID.StaffofRegrowth);
                recipe.AddIngredient(ItemID.LeafBlower);
                recipe.AddIngredient(thorium.ItemType("BudBomb"), 300);
            }
            else
            {
                recipe.AddIngredient(ItemID.FlowerBoots);
                recipe.AddIngredient(ItemID.StaffofRegrowth);
            }
            
            recipe.AddIngredient(ItemID.Seedling);
            
            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
