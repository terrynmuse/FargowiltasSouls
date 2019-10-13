using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class SilverEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        public int timer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Silver Enchantment");

            string tooltip = 
@"'Have you power enough to wield me?'
Summons a sword familiar that scales with minion damage";
            string tooltip_ch =
@"'你有足够的力量驾驭我吗?'
召唤一柄剑";

            if(thorium != null)
            {
                tooltip += "\nEffects of Silver Bulwark";
                tooltip_ch += "\n拥有银壁垒的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "银魔石");
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
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.SilverEnchant = true;
            modPlayer.AddMinion("Silver Sword Familiar", mod.ProjectileType("SilverSword"), (int) (30 * player.minionDamage), 0f);

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player); 
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            timer++;
            if (timer >= 30)
            {
                int num = 14;
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
            recipe.AddIngredient(ItemID.SilverHelmet);
            recipe.AddIngredient(ItemID.SilverChainmail);
            recipe.AddIngredient(ItemID.SilverGreaves);

            if(Fargowiltas.Instance.ThoriumLoaded)
            {      
                recipe.AddIngredient(thorium.ItemType("SilverBulwark"));
                recipe.AddIngredient(ItemID.SilverBroadsword);
                recipe.AddIngredient(ItemID.SilverBow);
                recipe.AddIngredient(ItemID.SapphireStaff);
                recipe.AddIngredient(ItemID.BluePhaseblade);
                recipe.AddIngredient(ItemID.AquaScepter);
                recipe.AddIngredient(ItemID.ThroughtheWindow);
            }
            else
            {
                recipe.AddIngredient(ItemID.SilverBroadsword);
                recipe.AddIngredient(ItemID.SapphireStaff);
                recipe.AddIngredient(ItemID.BluePhaseblade);
                recipe.AddIngredient(ItemID.AquaScepter);
            }

            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
