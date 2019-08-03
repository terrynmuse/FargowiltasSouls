using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BronzeEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bronze Enchantment");
            Tooltip.SetDefault(
@"'You have the favor of Zeus'
Thrown damage has a chance to cause a lightning bolt to strike
Effects of Olympic Torch, Champion's Rebuttal, Spartan Sadals, and Spartan's Subwoofer");
            DisplayName.AddTranslation(GameCulture.Chinese, "青铜魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'宙斯的青睐'
投掷伤害有概率释放闪电链
拥有奥林匹克圣火, 反击之盾, 斯巴达凉鞋和斯巴达音箱的效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 60000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //lightning
            thoriumPlayer.greekSet = true;
            //rebuttal
            thoriumPlayer.championShield = true;
            //sandles
            thorium.GetItem("SpartanSandles").UpdateAccessory(player, hideVisual);
            player.moveSpeed -= 0.15f;
            player.maxRunSpeed -= 1f;
            //subwoofer
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerMarble = true;
                }
            }
            thoriumPlayer.bardRangeBoost += 450;

            //olympic torch
            thoriumPlayer.olympicTorch = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("BronzeHelmet"));
            recipe.AddIngredient(thorium.ItemType("BronzeBreastplate"));
            recipe.AddIngredient(thorium.ItemType("BronzeGreaves"));
            recipe.AddIngredient(thorium.ItemType("OlympicTorch"));
            recipe.AddIngredient(thorium.ItemType("ChampionsBarrier"));
            recipe.AddIngredient(thorium.ItemType("SpartanSandles"));
            recipe.AddIngredient(thorium.ItemType("BronzeSubwoofer"));
            recipe.AddIngredient(thorium.ItemType("ChampionBlade"));
            recipe.AddIngredient(thorium.ItemType("BronzeThrowing"), 300);
            recipe.AddIngredient(thorium.ItemType("GraniteThrowingAxe"), 300);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
