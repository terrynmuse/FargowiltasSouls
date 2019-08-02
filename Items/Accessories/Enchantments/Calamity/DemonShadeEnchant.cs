using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using CalamityMod;

namespace FargowiltasSouls.Items.Accessories.Enchantments.Calamity
{
    public class DemonShadeEnchant : ModItem
    {
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        public override bool Autoload(ref string name)
        {
            return ModLoader.GetMod("CalamityMod") != null;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Demonshade Enchantment");
            Tooltip.SetDefault(
@"'Demonic power emanates from you…'
All attacks inflict Demon Flames
Shadowbeams and Demon Scythes fall from the sky on hit
A friendly red devil follows you around
Enemies take ungodly damage when they touch you
Standing still lets you absorb the shadows and boost your life regen
Press Y to enrage nearby enemies with a dark magic spell for 10 seconds
This makes them do 1.5 times more damage but they also take five times as much damage");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 50000000;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(255, 0, 255));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>(calamity);
            //body
            modPlayer.shadeRegen = true;
            player.thorns = 100f;
            //legs
            modPlayer.shadowSpeed = true;
            //set bonus
            modPlayer.dsSetBonus = true;

            if (Soulcheck.GetValue("Red Devil Minion"))
            {
                modPlayer.redDevil = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(calamity.BuffType("RedDevil")) == -1)
                    {
                        player.AddBuff(calamity.BuffType("RedDevil"), 3600, true);
                    }
                    if (player.ownedProjectileCounts[calamity.ProjectileType("RedDevil")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, calamity.ProjectileType("RedDevil"), 0, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
        }

        public override void AddRecipes()
        {
            if (!Fargowiltas.Instance.CalamityLoaded) return;

            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(calamity.ItemType("DemonshadeHelm"));
            recipe.AddIngredient(calamity.ItemType("DemonshadeBreastplate"));
            recipe.AddIngredient(calamity.ItemType("DemonshadeGreaves"));
            recipe.AddIngredient(calamity.ItemType("Animus"));
            recipe.AddIngredient(calamity.ItemType("Earth"));
            recipe.AddIngredient(calamity.ItemType("Azathoth"));
            recipe.AddIngredient(calamity.ItemType("CrystylCrusher"));
            recipe.AddIngredient(calamity.ItemType("Contagion"));
            recipe.AddIngredient(calamity.ItemType("Megafleet"));
            recipe.AddIngredient(calamity.ItemType("SomaPrime"));
            recipe.AddIngredient(calamity.ItemType("Judgement"));
            recipe.AddIngredient(calamity.ItemType("Apotheosis"));
            recipe.AddIngredient(calamity.ItemType("RoyalKnives"));
            recipe.AddIngredient(calamity.ItemType("TriactisTruePaladinianMageHammerofMight"));
            recipe.AddTile(calamity, "DraedonsForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
