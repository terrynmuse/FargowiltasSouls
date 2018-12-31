using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TungstenEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tungsten Enchantment");

            string tooltip = 
@"'Juggernaut'
Your weapons shoot at 1/8 the speed
300% increased damage
25% increased crit chance";

            /*if(thorium != null)
            {
                tooltip += "While in combat, you generate a 15 life shield";
            }*/

            Tooltip.SetDefault(tooltip);
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
            player.GetModPlayer<FargoPlayer>().TungstenEffect();

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.metallurgyShield = true;
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 15;
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
            }*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.TungstenHelmet);
            recipe.AddIngredient(ItemID.TungstenChainmail);
            recipe.AddIngredient(ItemID.TungstenGreaves);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("TungstenBulwark"));
                recipe.AddIngredient(ItemID.TungstenHammer);
                recipe.AddIngredient(ItemID.EmeraldStaff);
                recipe.AddIngredient(ItemID.GreenPhaseblade);
                recipe.AddIngredient(ItemID.Snail);
                recipe.AddIngredient(ItemID.Sluggy);
                recipe.AddIngredient(thorium.ItemType("EmeraldButterfly"));
            }
            else
            {*/
                recipe.AddIngredient(ItemID.TungstenHammer);
                recipe.AddIngredient(ItemID.EmeraldStaff);
                recipe.AddIngredient(ItemID.Snail);
            //}

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
