using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using System;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DurasteelEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Durasteel Enchantment");
            Tooltip.SetDefault(
@"'Masterfully forged by the Blacksmith'
12% damage reduction
Grants the ability to dash into the enemy
Right Click to guard with your shield
Effects of the Incandescent Spark, Spiked Bracers, and Greedy Magnet");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 80000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //durasteel set bonus
            thoriumPlayer.thoriumEndurance += 0.12f;

            if (Soulcheck.GetValue("Incandescent Spark"))
            {
                thorium.GetItem("IncandescentSpark").UpdateAccessory(player, hideVisual);
            }

            if (Soulcheck.GetValue("Greedy Magnet"))
            {
                thorium.GetItem("GreedyMagnet").HoldItem(player);
            }
            
            //EoC Shield
            player.dash = 2;
            //spiked bracers
            player.thorns += 0.25f;

            if (Soulcheck.GetValue("Iron Shield"))
            {
                //shield
                player.GetModPlayer<FargoPlayer>(mod).IronEffect();
            }
        }
        
        private readonly string[] items =
        {
            "IncandescentSpark",
            "GreedyMagnet",
            "DurasteelRepeater",
            "SpudBomber",
            "ThiefDagger",
            "SeaMine"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);
            
            recipe.AddIngredient(thorium.ItemType("DurasteelHelmet"));
            recipe.AddIngredient(thorium.ItemType("DurasteelChestplate"));
            recipe.AddIngredient(thorium.ItemType("DurasteelGreaves"));
            recipe.AddIngredient(null, "DarksteelEnchant");
            
            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.CrystalBall);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
