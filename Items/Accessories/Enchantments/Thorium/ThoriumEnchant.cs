using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class ThoriumEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Thorium Enchantment");
            Tooltip.SetDefault(
@"'It pulses with energy'
10% increased damage
Taking more than three damage will replenish health and mana
Symphonic critical strikes ring a bell over your head, slowing all nearby enemies briefly
Increases max inspiration by 2");
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
            player.meleeDamage += 0.1f;
            player.thrownDamage += 0.1f;
            player.rangedDamage += 0.1f;
            player.magicDamage += 0.1f;
            player.minionDamage += 0.1f;
            thoriumPlayer.radiantBoost += 0.1f;
            thoriumPlayer.symphonicDamage += 0.1f;
            //band of replenishment
            thoriumPlayer.BandofRep = true;
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
            recipe.AddIngredient(thorium.ItemType("BandofReplenishment"));
            recipe.AddIngredient(thorium.ItemType("WhirlpoolSaber"));
            recipe.AddIngredient(thorium.ItemType("ThoriumCube"));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
