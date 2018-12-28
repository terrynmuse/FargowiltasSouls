using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class OceanEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ocean Enchantment");
            Tooltip.SetDefault(
@"''
Allows you to breathe underwater
Grants the ability to swim
Being in water increases damage and damage reduction by 10%
Attracts all nearby air bubbles found within the Aquatic Depths
Doubles the duration of 'Refreshing Bubble' when held");
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
            //set bonus, breath underwater
            if (player.breath <= player.breathMax + 2)
            {
                player.breath = player.breathMax + 3;
            }
            //sea breeze pendant
            player.accFlipper = true;
            if (player.wet)
            {
                player.AddBuff(thorium.BuffType("AquaticAptitude"), 60, true);
                player.meleeDamage += 0.1f;
                player.thrownDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.magicDamage += 0.1f;
                player.minionDamage += 0.1f;
                //missing from divers nice MEME
                thoriumPlayer.radiantBoost += 0.1f;
                thoriumPlayer.symphonicDamage += 0.1f;
                //from ocean set why not
                thoriumPlayer.thoriumEndurance += 0.1f;
            }
            //bubble magnet
            thoriumPlayer.bubbleMagnet = true;
        }
        
        private readonly string[] items =
        {
            "OceanHelmet",
            "OceanChestGuard",
            "OceanGreaves",
            "SeaBreezePendant",
            "BubbleMagnet",
            "OceanSlasher",
            "StarfishWand"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddIngredient(ItemID.BreathingReed);
            recipe.AddIngredient(ItemID.Swordfish);
            recipe.AddIngredient(ItemID.Tuna);

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
