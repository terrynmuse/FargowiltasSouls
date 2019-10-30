using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace FargowiltasSouls.Items.Accessories.Enchantments
{
    public class NinjaEnchant : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ninja Enchantment");
            Tooltip.SetDefault(
@"'Now you see me, now you don’t'
Throw a smoke bomb to teleport to it and gain the First Strike Buff
Using the Rod of Discord will also grant this buff
First Strike enhances your next attack 
Melee attacks will crit for 3x damage
Projectile attacks fire in a barrage of 3
Summons a pet Black Cat");
            DisplayName.AddTranslation(GameCulture.Chinese, "忍者魔石");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'你看到我了,现在又不见了'
扔烟雾弹进行传送,获得先发制人Buff
使用裂位法杖也会获得该Buff
先发制人Buff会强化你的下一次攻击
近战暴击造成3倍伤害
抛射物攻击连续射击3次
召唤一只黑色小猫咪");
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(48, 49, 52);
                }
            }
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 2;
            item.value = 30000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().NinjaEffect(hideVisual);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NinjaHood);
            recipe.AddIngredient(ItemID.NinjaShirt);
            recipe.AddIngredient(ItemID.NinjaPants);
            recipe.AddIngredient(ItemID.Shuriken, 300);
            
            if(Fargowiltas.Instance.ThoriumLoaded)
            {   
                recipe.AddIngredient(ItemID.StarAnise, 300);
                recipe.AddIngredient(ItemID.SpikyBall, 300);
                recipe.AddIngredient(thorium.ItemType("Scorpain"));
                recipe.AddIngredient(ItemID.SmokeBomb, 50);
            }
            else
            {
                recipe.AddIngredient(ItemID.SmokeBomb, 50);
            }

            recipe.AddIngredient(ItemID.SlimySaddle);
            recipe.AddIngredient(ItemID.UnluckyYarn);
            
            recipe.AddTile(TileID.DemonAltar);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
