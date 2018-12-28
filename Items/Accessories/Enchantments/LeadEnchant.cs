using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class LeadEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lead Enchantment");

            string tooltip = 
@"'Not recommended for eating'
Attacks may inflict enemies with Lead Poisoning
Lead Poisoning deals damage over time and slows enemies slightly";

            /*if(thorium != null)
            {
                tooltip += "While in combat, you generate a 13 life shield";
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
            item.value = 20000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).LeadEnchant = true;

            /*if (!Fargowiltas.Instance.ThoriumLoaded) return;

            ThoriumPlayer thoriumPlayer = ((ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer"));
            thoriumPlayer.metallurgyShield = true;
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 13;
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
            recipe.AddIngredient(ItemID.LeadHelmet);
            recipe.AddIngredient(ItemID.LeadChainmail);
            recipe.AddIngredient(ItemID.LeadGreaves);
            
            /*if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("LeadShield"));
                recipe.AddIngredient(ItemID.LeadShortsword);
                recipe.AddIngredient(ItemID.LeadPickaxe);
                recipe.AddIngredient(thorium.ItemType("OnyxStaff"));
                recipe.AddIngredient(thorium.ItemType("RustySword"));
                recipe.AddIngredient(ItemID.GrayPaint, 100);
                recipe.AddIngredient(ItemID.SulphurButterfly);
            }
            else
            {*/
                recipe.AddIngredient(ItemID.LeadShortsword);
                recipe.AddIngredient(ItemID.LeadPickaxe);
                recipe.AddIngredient(ItemID.GrayPaint);
            //}
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
