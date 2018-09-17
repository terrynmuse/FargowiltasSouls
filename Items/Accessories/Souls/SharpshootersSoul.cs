using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Neck)]
    public class SharpshootersSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Sharpshooter's Soul");
            Tooltip.SetDefault(
                @"'Ready, aim, fire'
30% increased range damage
20% increased firing speed
15% increased ranged critical chance
Increases view range for guns (Right click to zoom out)");
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
            player.GetModPlayer<FargoPlayer>(mod).RangedSoul = true;

            player.rangedDamage += .3f;
            player.rangedCrit += 15;
            //sniper scope
            player.scope = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "SnipersEssence");


            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ItemID.MagicQuiver);
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

                /*
                 * 
                 * */
            }
            else
            {
                recipe.AddIngredient(ItemID.MagicQuiver);
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

            //range2.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}