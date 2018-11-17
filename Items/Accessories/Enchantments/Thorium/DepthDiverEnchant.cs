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
            return ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }
        
        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Depth Diver Enchantment");
            Tooltip.SetDefault(
@"'Become a selfless protector'
You and nearby allies gain 8% increased damage
You and nearby allies gain 10% increased movement speed
You and nearby allies can breathe underwater
Your symphonic damage empowers all nearby allies with: Coral Edge. Damage done against gouged enemies is increased by 8%. Doubles the range of your empowerments effect radius
Summons a Jellyfish in a Bubble to follow you around");
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
            
            DepthEffect(player);
        }
        
        private void DepthEffect(Player player)
        {
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
            thoriumPlayer.subwooferGouge = true;
            thoriumPlayer.bardRangeBoost += 450;
        }
        
        private readonly string[] items =
        {
            "DepthDiverHelmet",
            "DepthDiverChestplate",
            "DepthDiverGreaves",
            "DepthSubwoofer",
            "MagicConch",
            "AquamarineWineGlass",
            "GeyserStaff",
            "MistyTotemCaller",
            "AnglerBulb",
            "JellyFishIdol"
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
