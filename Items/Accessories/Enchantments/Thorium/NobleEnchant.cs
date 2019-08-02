using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class NobleEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Noble Enchantment");
            Tooltip.SetDefault(
@"'Rich with culture'
Your symphonic empowerments will last an additional 5 seconds
Effects of Ring of Unity, Mix Tape and Devil's Subwoofer");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 80000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //noble set bonus
            thoriumPlayer.bardBuffDuration += 300;
            //ring of unity
            thorium.GetItem("RingofUnity").UpdateAccessory(player, hideVisual);

            if (Soulcheck.GetValue("Mix Tape"))
            {
                //mix tape
                thoriumPlayer.mixtapeBool = true;
            }
            
            //molten woofer
            thoriumPlayer.bardRangeBoost += 450;
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && !player2.dead && Vector2.Distance(player2.Center, player.Center) < 450f)
                {
                    thoriumPlayer.empowerFire = true;
                }
            }
        }
        
        private readonly string[] items =
        {
            "NoblesHat",
            "NoblesJerkin",
            "NoblesLeggings",
            "MixTape",
            "RingofUnity",
            "MoltenSubwoofer",
            "GoldenBugleHorn",
            "Microphone",
            "Bongos",
            "MusicSheet3"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
