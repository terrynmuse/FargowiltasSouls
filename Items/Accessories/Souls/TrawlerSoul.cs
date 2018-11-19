using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Back)]
    public class TrawlerSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Trawler Soul");
            Tooltip.SetDefault(
                @"'The fish catch themselves'
Increases fishing skill substantially
All fishing rods will have 10 extra lures
Fishing line will never break
Decreases chance of bait consumption
Permanent Sonar and Crate Buffs
Effects of the Frog Legs and Spore Sac
");

//Allows you to see what's biting your hook
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>();
            modPlayer.FishSoul2 = true;
            modPlayer.AddPet("Zephyr Fish Pet", hideVisual, BuffID.ZephyrFish, ProjectileID.ZephyrFish);

            player.sonarPotion = true;
            player.fishingSkill += 50;
            player.cratePotion = true;
            player.accFishingLine = true;
            player.accTackleBox = true;
            player.accFishFinder = true;

            //froglegs
            player.autoJump = true;
            player.jumpSpeedBoost += 2.4f;

            if (Soulcheck.GetValue("Spore Sac"))
            {
                player.SporeSac();
                player.sporeSac = true;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "AnglerEnchantment");
            recipe.AddIngredient(ItemID.AnglerTackleBag);
            recipe.AddIngredient(ItemID.MechanicsRod);
            recipe.AddIngredient(ItemID.SittingDucksFishingRod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
            //hi tech sonar device
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("AquaticSonarDevice"));
                //CartilagedCatcher
                recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerrariumFisher"));
            }
            else
            {
                recipe.AddIngredient(ItemID.GoldenFishingRod);
            }

            recipe.AddIngredient(ItemID.FrogLeg);
            recipe.AddIngredient(ItemID.FinWings);
            recipe.AddIngredient(ItemID.Toxikarp);
            recipe.AddIngredient(ItemID.Bladetongue);
            recipe.AddIngredient(ItemID.CrystalSerpent);
            recipe.AddIngredient(ItemID.ObsidianSwordfish);
            recipe.AddIngredient(ItemID.SporeSac);
            recipe.AddIngredient(ItemID.ZephyrFish);

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
