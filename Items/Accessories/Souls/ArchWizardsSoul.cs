using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ID.ItemID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod;
using Terraria.Localization;
using CalamityMod.CalPlayer;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ArchWizardsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arch Wizard's Soul");

            string tooltip = 
@"'Arcane to the core'
30% increased magic damage
20% increased spell casting speed
15% increased magic crit chance
Increases your maximum mana by 200
";
            string tooltip_ch =
@"'神秘核心'
增加30%魔法伤害
增加20%施法速度
增加15%魔法暴击率
增加200最大法力值
";

            if (calamity == null)
            {
                tooltip += "Effects of Celestial Cuffs and Mana Flower";
                tooltip_ch += "拥有星体手铐和魔力花的效果";
            }
            else
            {
                tooltip += "Effects of Celestial Cuffs and Ethereal Talisman";
                tooltip_ch += "拥有星体手铐和空灵护符的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "巫师之魂");
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
                    tooltipLine.overrideColor = new Color?(new Color(255, 83, 255));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>().MagicSoul = true;
            player.magicDamage += .3f;
            player.magicCrit += 15;
            player.statManaMax2 += 200;
            //accessorys
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player);
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            modPlayer.eTalisman = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ApprenticesEssence");
            recipe.AddIngredient(Fargowiltas.Instance.CalamityLoaded ? calamity.ItemType("EtherealTalisman") : ManaFlower);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(CelestialCuffs);
                recipe.AddIngredient(MedusaHead);
                recipe.AddIngredient(thorium.ItemType("TwinsIre"));
                recipe.AddIngredient(thorium.ItemType("TerraStaff"));
                recipe.AddIngredient(RainbowGun);
                recipe.AddIngredient(thorium.ItemType("SpectrelBlade"));
                recipe.AddIngredient(thorium.ItemType("LightningStaff"));
                recipe.AddIngredient(ApprenticeStaffT3);
                recipe.AddIngredient(thorium.ItemType("NuclearFury"));            
            }
            else
            {
                recipe.AddIngredient(WizardHat);
                recipe.AddIngredient(CelestialCuffs);
                recipe.AddIngredient(CelestialEmblem);
                recipe.AddIngredient(MedusaHead);
                recipe.AddIngredient(GoldenShower);
                recipe.AddIngredient(RainbowGun);
                recipe.AddIngredient(MagnetSphere);
                recipe.AddIngredient(ApprenticeStaffT3);
                recipe.AddIngredient(RazorbladeTyphoon);
            }

            recipe.AddIngredient(BlizzardStaff);
            recipe.AddIngredient(LaserMachinegun);
            recipe.AddIngredient(LastPrism);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
