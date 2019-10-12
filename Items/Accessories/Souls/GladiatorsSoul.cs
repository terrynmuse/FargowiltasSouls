using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ID.ItemID;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CalamityMod.CalPlayer;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Waist)]
    public class GladiatorsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Berserker's Soul");

            string tooltip = 
@"'None shall live to tell the tale'
30% increased melee damage
20% increased melee speed
15% increased melee crit chance
Increased melee knockback
";
            string tooltip_ch =
@"'不留活口'
增加30%近战伤害
增加30%近战速度
增加15%近战暴击率
增加近战击退";

            if (calamity == null)
            {
                tooltip += "Effects of the Fire Gauntlet and Yoyo Bag";
                tooltip_ch += "拥有烈火手套和悠悠球袋的效果";
            }
            else
            {
                tooltip += "Effects of the Elemental Gauntlet and Yoyo Bag";
                tooltip_ch += "元素之握和悠悠球袋的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "狂战士之魂");
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
                    tooltipLine.overrideColor = new Color?(new Color(255, 111, 6));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += .3f;
            player.meleeSpeed += .2f;
            player.meleeCrit += 15;

            //gauntlet
            player.magmaStone = true;
            player.kbGlove = true;
            //yoyo bag
            player.counterWeight = 556 + Main.rand.Next(6);
            player.yoyoGlove = true;
            player.yoyoString = true;

            if (Fargowiltas.Instance.CalamityLoaded) Calamity(player);
        }

        private void Calamity(Player player)
        {
            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
            modPlayer.eGauntlet = true;
            //removing the extra boosts it adds because meme calamity
            player.meleeDamage -= .15f;
            player.meleeSpeed -= .15f;
            player.meleeCrit -= 5;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "BarbariansEssence");
            recipe.AddIngredient(Fargowiltas.Instance.CalamityLoaded ? calamity.ItemType("ElementalGauntlet") : FireGauntlet);
            recipe.AddIngredient(YoyoBag);
            recipe.AddIngredient(Arkhalis);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(KOCannon);
                recipe.AddIngredient(IceSickle);
                recipe.AddIngredient(thorium.ItemType("PrimesFury"));
                recipe.AddIngredient(MonkStaffT2);
                recipe.AddIngredient(TerraBlade);
                recipe.AddIngredient(ScourgeoftheCorruptor);
                recipe.AddIngredient(thorium.ItemType("Spearmint"));
            }
            else
            {
                recipe.AddIngredient(IceSickle);
                recipe.AddIngredient(MonkStaffT2);
                recipe.AddIngredient(TerraBlade);
                recipe.AddIngredient(ScourgeoftheCorruptor);
                recipe.AddIngredient(Kraken);
                recipe.AddIngredient(Flairon);
                recipe.AddIngredient(TheHorsemansBlade);
            }

            recipe.AddIngredient(NorthPole);
            recipe.AddIngredient(InfluxWaver);
            recipe.AddIngredient(Meowmere);

            recipe.AddTile(mod, "CrucibleCosmosSheet");

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
