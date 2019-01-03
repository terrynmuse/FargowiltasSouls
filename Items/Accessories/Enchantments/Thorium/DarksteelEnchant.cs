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
            return false;// ModLoader.GetLoadedMods().Contains("ThoriumMod");
        }

        public override string Texture => "FargowiltasSouls/Items/Placeholder";
        
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Darksteel Enchantment");
            Tooltip.SetDefault(
@"'Light yet durable'
Grants the ability to dash into the enemy, knockback immunity and Ice Skates effect
8% damage reduction
Right Click to guard with your shield
You attract items from a larger range
While in combat, you generate a 25 life shield
35% of the damage you take is also dealt to the attacker");
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
            //iron shield raise
            modPlayer.IronEffect();
            //magnet
            modPlayer.IronEnchant = true;
            //iron shield
            //thoriumPlayer.metallurgyShield = true;
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 25;
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
