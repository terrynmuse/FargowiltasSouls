using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class TinEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Tin Enchantment");

            string tooltip = 
@"'Return of the Crit'
Sets your critical strike chance to 4%
Every crit will increase it by 4%
Getting hit drops your crit back down";
            string tooltip_ch =
@"'暴击回归'
暴击率设为4%
每次暴击增加4%
被击中降低暴击率";

            if(thorium != null)
            {
                tooltip += "\nEffects of Tin Buckler";
                tooltip_ch += "\n拥有锡制圆盾的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "锡魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 1;
            item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).TinEffect();

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            timer++;
            if (timer >= 30)
            {
                int num = 11;
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
            recipe.AddIngredient(ItemID.TinHelmet);
            recipe.AddIngredient(ItemID.TinChainmail);
            recipe.AddIngredient(ItemID.TinGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("TinBuckler"));
                recipe.AddIngredient(ItemID.TinShortsword);
                recipe.AddIngredient(ItemID.TinBroadsword);
                recipe.AddIngredient(ItemID.TinBow);
                recipe.AddIngredient(ItemID.TopazStaff);
                recipe.AddIngredient(ItemID.YellowPhaseblade);
                recipe.AddIngredient(ItemID.Daylight);
            }
            else
            {
                recipe.AddIngredient(ItemID.TinBow);
                recipe.AddIngredient(ItemID.TopazStaff);
                recipe.AddIngredient(ItemID.YellowPhaseblade);
                recipe.AddIngredient(ItemID.Daylight);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
