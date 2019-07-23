using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

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

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.ornateSet = true;
            //concert tickets
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && player2 != player && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.bardResourceRecharge++;
                }
            }
            //music player
            thoriumPlayer.musicPlayer = true;
            thoriumPlayer.MP3AmmoConsumption = 2;
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
