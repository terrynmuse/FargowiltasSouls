using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ThoriumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorium Enchantment");
            Tooltip.SetDefault(
@"'It pulses with energy'
10% increased damage
Symphonic critical strikes ring a bell over your head, slowing all nearby enemies briefly
Effects of Thorium Shield, Crietz, and Fan Letter");
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
            //thorium set bonus 
            player.GetModPlayer<FargoPlayer>().AllDamageUp(.1f);
            //thorium shield
            timer++;
            if (timer >= 30)
            {
                int num = 18;
                if (thoriumPlayer.shieldHealth <= num)
                {
                    thoriumPlayer.shieldHealthTimerStop = true;
                }
                if (thoriumPlayer.shieldHealth < num)
                {
                    CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 255, 255), 1, false, true);
                    thoriumPlayer.shieldHealth++;
                    player.statLife++;
                }
                timer = 0;
            }
            //crietz
            thoriumPlayer.crietzAcc = true;
            //jester bonus
            thoriumPlayer.jesterSet = true;
            //fan letter
            thoriumPlayer.bardResourceMax2 += 2;
        }
        
        private readonly string[] items =
        {
            "ThoriumHelmet",
            "ThoriumMail",
            "ThoriumGreaves",
            "GrandThoriumHelmet",
            "GrandThoriumBreastPlate",
            "GrandThoriumGreaves",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(null, "JesterEnchant");
            recipe.AddIngredient(thorium.ItemType("ThoriumShield"));
            recipe.AddIngredient(thorium.ItemType("Crietz"));
            recipe.AddIngredient(thorium.ItemType("ThoriumCube"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
