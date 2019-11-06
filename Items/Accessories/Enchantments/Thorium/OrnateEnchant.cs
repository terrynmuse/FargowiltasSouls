using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class OrnateEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ornate Enchantment");
            Tooltip.SetDefault(
@"'Beautifully crafted'
Symphonic critical strikes cause the attack's empowerment to ascend to a fourth level of intensity
Effects of Concert Tickets and Brown Music Player");
            DisplayName.AddTranslation(GameCulture.Chinese, "华贵魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'精心设计'
音波暴击使玩家的咒音增幅提升到等级4
拥有音乐会门票和棕色播放器效果");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 7;
            item.value = 200000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            thoriumPlayer.setOrnate = true;
            //concert tickets
            thoriumPlayer.bardResourceMax2 += 2;
            for (int i = 0; i < Main.myPlayer; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && i != player.whoAmI && (!player2.hostile || (player2.team == player.team && player2.team != 0)) && player2.DistanceSQ(player.Center) < 202500f)
                {
                    thoriumPlayer.inspirationRegenBonus += 0.02f;
                }
            }
            //music player
            //thoriumPlayer.musicPlayer = true;
            //thoriumPlayer.MP3AmmoConsumption = 2;
        }
        
        private readonly string[] items =
        {
            "OrnateHat",
            "OrnateJerkin",
            "OrnateLeggings",
            "ConcertTickets",
            "TunePlayerAmmoConsume",
            "GreenTambourine",
            "VuvuzelaBlue",
            "VuvuzelaGreen",
            "VuvuzelaRed",
            "VuvuzelaYellow"
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
