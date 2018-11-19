using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CopperEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Enchantment");

            string tooltip = "'Behold'";

            if(thorium != null)
            {
                tooltip += "\nWhile in combat, you generate a 10 life shield\n";
            }

            tooltip +=
@"Attacks have a chance to shock enemies with lightning
If an enemy is wet, the chance and damage is increased
Attacks that cause Wet cannot proc the lightning
Lightning scales with magic damage";

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 40000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).CopperEnchant = true;

            if (!Fargowiltas.Instance.ThoriumLoaded) return;
            
            //copper buckler
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            thoriumPlayer.metallurgyShield = true;
            if (!thoriumPlayer.outOfCombat)
            {
                timer++;
                if (timer >= 30)
                {
                    int num = 10;
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

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CopperHelmet);
            recipe.AddIngredient(ItemID.CopperChainmail);
            recipe.AddIngredient(ItemID.CopperGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("CopperBuckler"));
                recipe.AddIngredient(ItemID.CopperShortsword);
                recipe.AddIngredient(ItemID.AmethystStaff);
                recipe.AddIngredient(thorium.ItemType("ThunderTalon"));
                recipe.AddIngredient(thorium.ItemType("Zapper"));
                recipe.AddIngredient(ItemID.Wire, 20);
                recipe.AddIngredient(thorium.ItemType("AmethystButterfly"));
            }
            else
            {
                recipe.AddIngredient(ItemID.CopperShortsword);
                recipe.AddIngredient(ItemID.AmethystStaff);
                recipe.AddIngredient(ItemID.Wire, 20);
            }
                       
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
