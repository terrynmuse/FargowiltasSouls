using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

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
@"'Bigger is always better'
150% increased sword size
100% increased projectile size
Projectiles still have the same tile collision hitbox";
            string tooltip_ch =
@"'大就是好'
增加150%剑的尺寸
增加100%抛射物尺寸
减少10%移动速度和近战速度
抛射物仍然具有同样的砖块碰撞箱";

            if(thorium != null)
            {
                tooltip += "\nEffects of Tungsten Bulwark";
                tooltip_ch += "\n拥有钨壁垒的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "钨金魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
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
            player.GetModPlayer<FargoPlayer>().TungstenEnchant = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            timer++;
            if (timer >= 30)
            {
                int num = 15;
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
            recipe.AddIngredient(ItemID.TungstenHelmet);
            recipe.AddIngredient(ItemID.TungstenChainmail);
            recipe.AddIngredient(ItemID.TungstenGreaves);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("TungstenBulwark"));
                recipe.AddIngredient(ItemID.TungstenBroadsword);
                recipe.AddIngredient(ItemID.TungstenHammer);
                recipe.AddIngredient(ItemID.EmeraldStaff);
                recipe.AddIngredient(ItemID.GreenPhaseblade);
                recipe.AddIngredient(ItemID.Snail);
                recipe.AddIngredient(ItemID.Sluggy);
            }
            else
            {
                recipe.AddIngredient(ItemID.GreenPhaseblade);
                recipe.AddIngredient(ItemID.EmeraldStaff);
                recipe.AddIngredient(ItemID.Snail);
                recipe.AddIngredient(ItemID.Sluggy);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
