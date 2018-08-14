using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ID.ItemID;
using static Terraria.ID.ProjectileID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class VoidSoul : ModItem
    {
        public int Cd;
        public int VoidTimer = 600;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of the Void");
            Tooltip.SetDefault("'The depths of the Void have surrendered to your might' \n" +
                                "The Mutant's Grab Bags have unlocked their true potential\n" +
                                "Effects of the Cell Phone, set key to go home\n" +
                                "You respawn twice as fast\n");

        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 500000;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
        }

        public override string Texture
        {
            get
            {
                return "FargowiltasSouls/Items/Placeholder";
            }
        }

        public override void UpdateInventory(Player player)
        {
            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accWeatherRadio = true;
            player.accCalendar = true;
            player.accThirdEye = true;
            player.accJarOfSouls = true;
            player.accCritterGuide = true;
            player.accStopwatch = true;
            player.accOreFinder = true;
            player.accDreamCatcher = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);
            modPlayer.VoidSoul = true;

            player.accWatch = 3;
            player.accDepthMeter = 1;
            player.accCompass = 1;
            player.accFishFinder = true;
            player.accWeatherRadio = true;
            player.accCalendar = true;
            player.accThirdEye = true;
            player.accJarOfSouls = true;
            player.accCritterGuide = true;
            player.accStopwatch = true;
            player.accOreFinder = true;
            player.accDreamCatcher = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PortalGun);
            recipe.AddIngredient(RodofDiscord);
            recipe.AddIngredient(CellPhone);
            recipe.AddIngredient(CoinGun);

            recipe.AddIngredient(AmberMosquito);
            recipe.AddIngredient(Fish);
            recipe.AddIngredient(Seaweed);
            recipe.AddIngredient(ToySled);
            recipe.AddIngredient(ItemID.ZephyrFish);
            recipe.AddIngredient(ItemID.CompanionCube);
            recipe.AddIngredient(LizardEgg);
            recipe.AddIngredient(BabyGrinchMischiefWhistle);
            recipe.AddIngredient(BoneKey);
            recipe.AddIngredient(SuspiciousLookingTentacle);

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}