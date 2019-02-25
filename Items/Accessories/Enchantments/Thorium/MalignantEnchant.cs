using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class MalignantEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Malignant Enchantment");
            Tooltip.SetDefault(
@"''
Magic critical strikes engulf enemies in a long lasting void flame
Effects of Mana-Charged Rocketeers");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            thoriumPlayer.malignantSet = true;
            //mana charge rockets
            player.manaRegen++;
            player.manaRegenDelay -= 2;
            if (player.statMana > 0)
            {
                player.rocketBoots = 1;
                if (player.rocketFrame)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        player.statMana -= 2;
                        Dust.NewDust(new Vector2(player.position.X, player.position.Y + 20f), player.width, player.height, 15, player.velocity.X * 0.2f, player.velocity.Y * 0.2f, 100, default(Color), 1.5f);
                    }
                    player.rocketTime = 1;
                }
            }
        }
        
        private readonly string[] items =
        {
            "ManaChargedRocketeers",
            "JellyPondWand",
            "DarkTome",
            "ChampionBomberStaff",
            "GaussSpark",
            "MagicThorHammer"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("MalignantCap"));
            recipe.AddIngredient(thorium.ItemType("MalignantRobe"));
            recipe.AddIngredient(null, "SilkEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.PurpleEmperorButterfly);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
