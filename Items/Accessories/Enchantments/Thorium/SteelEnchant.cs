using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class SteelEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Steel Enchantment");
            Tooltip.SetDefault(
@"'Expertly forged by the Blacksmith'
5% damage reduction
Allows the player to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
Effects of Iron Shield and Spiked Bracers");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 40000;
            item.shieldSlot = 5;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //steel set bonus
            thoriumPlayer.thoriumEndurance += 0.05f;
            //spiked bracers
            player.thorns += 0.25f;
            //iron shield raise
            player.GetModPlayer<FargoPlayer>(mod).IronEffect();
            //item attract
            if (Soulcheck.GetValue("Iron Magnet"))
            {
                modPlayer.IronEnchant = true;
            }
            //EoC Shield
            player.dash = 2;
            //iron sheild
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 22;
                    if (thoriumPlayer.shieldHealth < num)
                    {
                        CombatText.NewText(new Rectangle((int)player.position.X, (int)player.position.Y, player.width, player.height), new Color(51, 255, 255), 1, false, true);
                        thoriumPlayer.shieldHealth++;
                    }
                    timer = 0;
                    return;
                }
            }
            else
            {
                timer = 0;
            }
        }
        
        private readonly string[] items =
        {
            "SpikedBracer",
            "SteelAxe",
            "SteelMallet",
            "SteelBlade",
            "WarForger",
            "SuperAnvil"
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("SteelHelmet"));
            recipe.AddIngredient(thorium.ItemType("SteelChestplate"));
            recipe.AddIngredient(thorium.ItemType("SteelGreaves"));
            recipe.AddIngredient(null, "IronEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
