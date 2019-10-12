using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ConjuristsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conjurist's Soul");

            string tooltip =
@"'An army at your disposal'
30% increased summon damage
Increases your max number of minions by 4
Increases your max number of sentries by 2
Increased minion knockback";
            string tooltip_ch =
@"'一支听命于你的军队'
增加30%召唤伤害
+4最大召唤栏
+2最大哨兵栏
增加召唤物击退";

            if (thorium != null)
            {
                tooltip += "\nEffects of Phylactery, Crystal Scorpion, and Yuma's Pendant";
                tooltip_ch += "\n拥有魂匣,魔晶蝎和云码垂饰的效果";
            }

            if (calamity != null)
            {
                tooltip += "\nEffects of Statis' Belt of Curses";
                tooltip_ch += "\n拥有斯塔提斯的诅咒系带的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "召唤之魂");
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
                    tooltipLine.overrideColor = new Color?(new Color(0, 255, 255));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.3f;
            player.maxMinions += 4;
            player.maxTurrets += 2;
            player.minionKB += 3f;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>();
            //phylactery
            if (!thoriumPlayer.lichPrevent)
            {
                player.AddBuff(thorium.BuffType("LichActive"), 60, true);
            }
            //crystal scorpion
            if (SoulConfig.Instance.GetValue("Crystal Scorpion"))
            {
                thoriumPlayer.crystalScorpion = true;
            }
            //yumas pendant
            if (SoulConfig.Instance.GetValue("Yuma's Pendant"))
            {
                thoriumPlayer.yuma = true;
            }
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            modPlayer.statisBeltOfCurses = true;
            modPlayer.shadowMinions = true;
            modPlayer.tearMinions = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OccultistsEssence");
            recipe.AddIngredient(Fargowiltas.Instance.CalamityLoaded ? calamity.ItemType("StatisBeltOfCurses") : ItemID.PapyrusScarab);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("Phylactery"));
                recipe.AddIngredient(thorium.ItemType("CrystalScorpion"));
                recipe.AddIngredient(thorium.ItemType("YumasPendant"));
                recipe.AddIngredient(thorium.ItemType("MastersLibram"));
                recipe.AddIngredient(thorium.ItemType("HailBomber"));
                recipe.AddIngredient(ItemID.PirateStaff);
                recipe.AddIngredient(ItemID.OpticStaff);
                recipe.AddIngredient(thorium.ItemType("TrueSilversBlade"));
                recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
                recipe.AddIngredient(ItemID.RavenStaff);
                recipe.AddIngredient(ItemID.XenoStaff);
            }
            else
            {
                recipe.AddIngredient(ItemID.PirateStaff);
                recipe.AddIngredient(ItemID.OpticStaff);
                recipe.AddIngredient(ItemID.DeadlySphereStaff);
                recipe.AddIngredient(ItemID.StaffoftheFrostHydra);
                recipe.AddIngredient(ItemID.DD2BallistraTowerT3Popper);
                recipe.AddIngredient(ItemID.DD2ExplosiveTrapT3Popper);
                recipe.AddIngredient(ItemID.DD2FlameburstTowerT3Popper);
                recipe.AddIngredient(ItemID.DD2LightningAuraT3Popper);
                recipe.AddIngredient(ItemID.TempestStaff);
                recipe.AddIngredient(ItemID.RavenStaff);
                recipe.AddIngredient(ItemID.XenoStaff);
            }

            recipe.AddIngredient(ItemID.MoonlordTurretStaff);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}