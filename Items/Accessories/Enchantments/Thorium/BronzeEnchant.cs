using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class BronzeEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bronze Enchantment");
            Tooltip.SetDefault(
@"'You have the favor of Zeus'
Attacks have a chance to shock enemies with chain lightning
Thrown damage has a chance to cause a lightning bolt to strike
Effects of Champion's Rebuttal, Spartan Sadals, and Spartan's Subwoofer");
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
            thoriumPlayer.spartanSandle = true;
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
            //copper enchant
            player.GetModPlayer<FargoPlayer>(mod).CopperEnchant = true;
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("BronzeHelmet"));
            recipe.AddIngredient(thorium.ItemType("BronzeBreastplate"));
            recipe.AddIngredient(thorium.ItemType("BronzeGreaves"));
            recipe.AddIngredient(null, "CopperEnchant");
            recipe.AddIngredient(thorium.ItemType("ChampionsBarrier"));
            recipe.AddIngredient(thorium.ItemType("SpartanSandles"));
            recipe.AddIngredient(thorium.ItemType("BronzeSubwoofer"));
            recipe.AddIngredient(thorium.ItemType("ChampionBlade"));
            recipe.AddIngredient(thorium.ItemType("BronzeThrowing"), 300);
            recipe.AddIngredient(thorium.ItemType("AncientWingButterfly"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
