using CalamityMod;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Neck)]
    public class SharpshootersSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpshooter's Soul");

            string tooltip = 
@"'Ready, aim, fire'
30% increased ranged damage
20% increased firing speed
15% increased ranged critical chance
";
            string tooltip_ch =
@"'准备,瞄准,开火'
增加30%远程伤害
增加20%开火速度
增加15%远程暴击率
";

            if (calamity == null)
            {
                tooltip += "Effects of Sniper Scope";
                tooltip_ch += "拥有狙击镜的效果";
            }
            else
            {
                tooltip += "Effects of Elemental Quiver and Sniper Scope";
                tooltip_ch += "拥有元素箭袋和狙击镜的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "神枪手之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(188, 253, 68));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //attack speed
            player.GetModPlayer<FargoPlayer>(mod).RangedSoul = true;
            player.rangedDamage += .3f;
            player.rangedCrit += 15;

            if (Soulcheck.GetValue("Sniper Scope"))
            {
                player.scope = true;
            }

            if (Fargowiltas.Instance.CalamityLoaded && Soulcheck.GetValue("Elemental Quiver")) Calamity(player);
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            modPlayer.eQuiver = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SnipersEssence");
            recipe.AddIngredient(Fargowiltas.Instance.CalamityLoaded ? calamity.ItemType("ElementalQuiver") : ItemID.MagicQuiver);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("SpineBuster"));
                recipe.AddIngredient(thorium.ItemType("DestroyersRage"));
                recipe.AddIngredient(thorium.ItemType("TerraBow"));
                recipe.AddIngredient(ItemID.NailGun);
                recipe.AddIngredient(ItemID.PiranhaGun);
                recipe.AddIngredient(thorium.ItemType("LaunchJumper"));
                recipe.AddIngredient(thorium.ItemType("NovaRifle"));
                recipe.AddIngredient(ItemID.DD2BetsyBow);
                recipe.AddIngredient(ItemID.Tsunami);
                recipe.AddIngredient(ItemID.StakeLauncher);
                recipe.AddIngredient(ItemID.EldMelter);
                recipe.AddIngredient(ItemID.FireworksLauncher);
            }
            else
            {
                recipe.AddIngredient(ItemID.SniperScope);
                recipe.AddIngredient(ItemID.DartPistol);
                recipe.AddIngredient(ItemID.Megashark);
                recipe.AddIngredient(ItemID.PulseBow);
                recipe.AddIngredient(ItemID.NailGun);
                recipe.AddIngredient(ItemID.PiranhaGun);
                recipe.AddIngredient(ItemID.SniperRifle);
                recipe.AddIngredient(ItemID.Tsunami);
                recipe.AddIngredient(ItemID.StakeLauncher);
                recipe.AddIngredient(ItemID.EldMelter);
                recipe.AddIngredient(ItemID.Xenopopper);
                recipe.AddIngredient(ItemID.FireworksLauncher);
            }

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
