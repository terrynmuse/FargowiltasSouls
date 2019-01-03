using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DepthDiverEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Depth Diver Enchantment");
            Tooltip.SetDefault(
@"'Become a selfless protector'
Allows you and nearby allies to breathe underwater
Grants the ability to swim
Being in water increases damage and damage reduction by 10%
Attracts all nearby air bubbles found within the Aquatic Depths
Doubles the duration of 'Refreshing Bubble' when held
You and nearby allies gain 10% increased damage and movement speed
Your symphonic damage empowers all nearby allies with: Coral Edge
Damage done against gouged enemies is increased by 8%
Doubles the range of your empowerments effect radius
Summons a pet Jellyfish");
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

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //depth diver set
            for (int i = 0; i < 255; i++)
            {
                Player player2 = Main.player[i];
                if (player2.active && Vector2.Distance(player2.Center, player.Center) < 250f)
                {
                    player2.AddBuff(thorium.BuffType("DepthSpeed"), 30, false);
                    player2.AddBuff(thorium.BuffType("DepthDamage"), 30, false);
                    player2.AddBuff(thorium.BuffType("DepthBreath"), 30, false);
                }
            }
            //depth woofer
            //thoriumPlayer.subwooferGouge = true;
            thoriumPlayer.bardRangeBoost += 450;
            modPlayer.DepthEnchant = true;
            modPlayer.AddPet("Jellyfish Pet", hideVisual, thorium.BuffType("JellyPet"), thorium.ProjectileType("JellyfishPet"));
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
            "DepthSubwoofer",
            "MagicConch",
            "GeyserStaff",
            "MistyTotemCaller",
            "AnglerBulb",
            "JellyFishIdol"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("DepthDiverHelmet"));
            recipe.AddIngredient(thorium.ItemType("DepthDiverChestplate"));
            recipe.AddIngredient(thorium.ItemType("DepthDiverGreaves"));
            recipe.AddIngredient(null, "OceanEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
