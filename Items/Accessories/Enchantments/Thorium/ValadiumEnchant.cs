using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ValadiumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Valadium Enchantment");
            Tooltip.SetDefault(
@"'Which way is up?'
Reverse gravity by pressing UP
While reversed, damage is increased by 12%
Effects of Eye of the Beholder");
            DisplayName.AddTranslation(GameCulture.Chinese, "虚金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'哪儿是上?'
按'上'键逆转重力
重力颠倒时增加12%远程伤害
拥有注视者之眼的效果");
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
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //set bonus
            player.gravControl = true;
            if (player.gravDir == -1f)
            {
                modPlayer.AllDamageUp(.12f);
            }
            if (Soulcheck.GetValue("Eye of the Beholder"))
            {
                //eye of beholder
                thorium.GetItem("EyeofBeholder").UpdateAccessory(player, hideVisual);
            }
        }
        
        private readonly string[] items =
        {
            "ValadiumHelmet",
            "ValadiumBreastPlate",
            "ValadiumGreaves",
            "EyeofBeholder",
            "GlacialSting",
            "Obliterator",
            "ValadiumBow",
            "ValadiumStaff",
            "LodeStoneQuickDraw",
            "TommyGun"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
