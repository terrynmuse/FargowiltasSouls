using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ConjuristsSoul : ModItem
    {
        private Mod thorium;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Conjurist's Soul");

            Tooltip.SetDefault(
                @"'An army at your disposal'
30% increased summon damage
Increases your max number of minions by 4
Increases your max number of sentries by 2
Increased minion knockback");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 1000000;
            item.rare = -12;
            item.expert = true;
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.3f;
            player.maxMinions += 4;
            player.maxTurrets += 2;
            player.minionKB += 3f;
        }

        /*private void Waifus(Player player)
        {
            CalamityMod.CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityMod.CalamityPlayer>(calamity);
            
            calamityPlayer.brimstoneWaifu = true;
            calamityPlayer.sandBoobWaifu = true;
            calamityPlayer.sandWaifu = true;
            calamityPlayer.cloudWaifu = true;
            calamityPlayer.sirenWaifu = true;
        }*/

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "OccultistsEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ItemID.PapyrusScarab);
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
                recipe.AddIngredient(ItemID.MoonlordTurretStaff);

                /*
                 * 
                 * */
            }
            else
            {
                recipe.AddIngredient(ItemID.PapyrusScarab);
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
                recipe.AddIngredient(ItemID.MoonlordTurretStaff);
            }

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}