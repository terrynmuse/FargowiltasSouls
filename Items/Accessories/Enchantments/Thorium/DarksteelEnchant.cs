using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using ThoriumMod;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Thorium
{
    public class DarksteelEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("ThoriumMod") != null;
        }
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darksteel Enchantment");
            Tooltip.SetDefault(
@"'Light yet durable'
8% damage reduction
Grants the ability to dash into the enemy
Right Click to guard with your shield
You attract items from a larger range
Effects of Iron Shield and Spiked Bracer");
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

            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //darksteel bonuses
            player.noKnockback = true;
            player.iceSkate = true;
            //EoC Shield
            player.dash = 2;
            //steel set bonus
            thoriumPlayer.thoriumEndurance += 0.08f;
            //spiked bracers
            player.thorns += 0.35f;
            if (Soulcheck.GetValue("Iron Shield"))
            {
                //iron shield raise
                modPlayer.IronEffect();
            }
            //magnet
            if (Soulcheck.GetValue("Iron Magnet"))
            {
                modPlayer.IronEnchant = true;
            }
            //iron shield
            timer++;
            if (timer >= 30)
            {
                int num = 12;
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
        }
        
        private readonly string[] items =
        {
            "BallnChain",
            "eeDarksteelMace",
            "eeSoulSiphon",
            "ManHacker",
            "DarksteelHelmetStand",
            "GrayDPaintingItem",
        };

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(thorium.ItemType("hDarksteelFaceGuard"));
            recipe.AddIngredient(thorium.ItemType("iDarksteelBreastPlate"));
            recipe.AddIngredient(thorium.ItemType("jDarksteelGreaves"));
            recipe.AddIngredient(null, "SteelEnchant");

            foreach (string i in items) recipe.AddIngredient(thorium.ItemType(i));

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
