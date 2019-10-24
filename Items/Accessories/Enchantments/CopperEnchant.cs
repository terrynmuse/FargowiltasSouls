using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class CopperEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Copper Enchantment");

            string tooltip = 
@"'Behold'
Attacks have a chance to shock enemies with lightning
If an enemy is wet, the chance and damage is increased
Attacks that cause Wet cannot proc the lightning";
            string tooltip_ch =
@"'注视'
攻击有概率用闪电打击敌人
如果敌人处于潮湿状态,增加概率和伤害
造成潮湿的攻击不能触发闪电";

            if(thorium != null)
            {
                tooltip += "\nEffects of the Copper Buckler";
                tooltip_ch += "\n拥有铜制圆盾的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "铜魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 3;
            item.value = 100000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().CopperEnchant = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = (ThoriumPlayer)player.GetModPlayer(thorium, "ThoriumPlayer");
            //copper shield
            timer++;
            if (timer >= 30)
            {
                int num = 10;
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
                recipe.AddIngredient(ItemID.PurplePhaseblade);
                recipe.AddIngredient(thorium.ItemType("ThunderTalon"));
                recipe.AddIngredient(thorium.ItemType("Zapper"));
                recipe.AddIngredient(ItemID.FirstEncounter);
            }
            else
            {
                recipe.AddIngredient(ItemID.CopperShortsword);
                recipe.AddIngredient(ItemID.AmethystStaff);
                recipe.AddIngredient(ItemID.FirstEncounter);
                //recipe.AddIngredient(ItemID.PurplePhaseblade);
                recipe.AddIngredient(ItemID.Wire, 20);
            }
                       
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        /*public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < 3 + maxAccessoryIndex; i++)
                {
                    if (slot != i && player.armor[i].type == mod.ItemType("TrueCopperEnchant"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }*/
    }
}
