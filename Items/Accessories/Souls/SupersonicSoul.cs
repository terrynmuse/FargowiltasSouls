using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Shoes)]
    public class SupersonicSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supersonic Soul");
            Tooltip.SetDefault(
                @"'Sound barriers forever broken'
25% increased movement speed
Allows supersonic fast running, and extra mobility on ice
Provides lava immunity and permanent light
Grants the ability to swim and greatly extends underwater breathing
Increases jump height, allows auto jump, and negates fall damage
Allows the player to dash into the enemy");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.defense = 4;
            item.accessory = true;
            item.value = 750000;
            item.expert = true;
            item.rare = -12;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //frost spark
            player.accRunSpeed = 6.75f;
            player.rocketBoots = 3;
            player.moveSpeed += 0.08f;
            player.iceSkate = true;

            //arctic diving gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;

            if (player.wet) Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);

            //frostspark
            /*if (Soulcheck.GetValue("Super Speed"))
            {
                player.GetModPlayer<FargoPlayer>(mod).SpeedEffect = true;
                player.accRunSpeed = 2.00f;
                player.moveSpeed += 5f;
            }
            else
            {
                player.accRunSpeed = 35.00f;
                player.moveSpeed += 0.25f;
            }*/


            //lava waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            //frog legs
            player.autoJump = true;
            player.jumpSpeedBoost += 2.4f;
            player.extraFall += 15;

            //bundle
            player.doubleJumpCloud = true;
            player.doubleJumpSandstorm = true;
            player.doubleJumpBlizzard = true;
            player.jumpBoost = true;

            //player.jumpAgainBlizzard = true;

            //slime mount
            player.maxFallSpeed += 5f;
            player.autoJump = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
            }
            else
            {
                //no others
                recipe.AddIngredient(ItemID.FrostsparkBoots);
                recipe.AddIngredient(ItemID.LavaWaders);
                recipe.AddIngredient(ItemID.ArcticDivingGear);
                recipe.AddIngredient(ItemID.FrogLeg);
                recipe.AddIngredient(ItemID.BundleofBalloons);


                recipe.AddIngredient(ItemID.SlimySaddle);
                recipe.AddIngredient(ItemID.FuzzyCarrot);
                recipe.AddIngredient(ItemID.BlessedApple);
                recipe.AddIngredient(ItemID.AncientHorn);
                recipe.AddIngredient(ItemID.ShrimpyTruffle);
                recipe.AddIngredient(ItemID.ReindeerBells);
                recipe.AddIngredient(ItemID.BrainScrambler);
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
