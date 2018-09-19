using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using static Terraria.ID.ItemID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class ArchWizardsSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Arch Wizard's Soul");
            Tooltip.SetDefault(
                @"'Arcane to the core'
30% increased magic damage
20% increased spell casting speed
15% increased magic crit chance
Increases your maximum mana by 200
Restores mana when damaged
Increased pickup range for mana stars
Automatically use mana potions when needed");
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
            player.GetModPlayer<FargoPlayer>().MagicSoul = true;
            player.magicDamage += .3f;
            player.magicCrit += 15;
            player.statManaMax2 += 200;
            //accessorys
            player.manaFlower = true;
            player.manaMagnet = true;
            player.magicCuffs = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ApprenticesEssence");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(ManaFlower);
                recipe.AddIngredient(WizardHat);
                recipe.AddIngredient(CelestialCuffs);
                recipe.AddIngredient(CelestialEmblem);
                recipe.AddIngredient(GoldenShower);
                recipe.AddIngredient(RainbowGun);
                recipe.AddIngredient(MagnetSphere);
                recipe.AddIngredient(ApprenticeStaffT3);
                recipe.AddIngredient(RazorbladeTyphoon);
                recipe.AddIngredient(BatScepter);
                recipe.AddIngredient(BlizzardStaff);
                recipe.AddIngredient(LaserMachinegun);
                recipe.AddIngredient(LastPrism);

                /*
                 * 
                 * */
            }
            else
            {
                recipe.AddIngredient(ManaFlower);
                recipe.AddIngredient(WizardHat);
                recipe.AddIngredient(CelestialCuffs);
                recipe.AddIngredient(CelestialEmblem);
                recipe.AddIngredient(GoldenShower);
                recipe.AddIngredient(RainbowGun);
                recipe.AddIngredient(MagnetSphere);
                recipe.AddIngredient(ApprenticeStaffT3);
                recipe.AddIngredient(RazorbladeTyphoon);
                recipe.AddIngredient(BatScepter);
                recipe.AddIngredient(BlizzardStaff);
                recipe.AddIngredient(LaserMachinegun);
                recipe.AddIngredient(LastPrism);
            }

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
