using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Shoes)]
    public class SupersonicSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supersonic Soul");
            Tooltip.SetDefault(
@"'Sound barriers forever broken'
Allows Supersonic running, flight, and extra mobility on ice
Allows the holder to quadruple jump if no wings are equipped
Increases jump height, jump speed, and allows auto-jump
Grants the ability to swim and greatly extends underwater breathing
Provides the ability to walk on water and lava
Grants immunity to lava and fall damage");


//air walkers
//15% increased movement and maximum speed
//Allows you to walk on air briefly after leaving a solid block

//survivalist boots
//15% increased movement and maximum speed
//While running, your life, mana, and inspiration regeneration are lightly increased

//terrarium
//Provides the ability to walk on water, Fire blocks and lava
//Allows flight
//Increased flight time
//The wearer can run impossibly fast

//travelers boots
//5% increased movement speed
//Your dash will reach its peak faster

//weighted winglets
//15% increased movement and maximum speed
//Allows you to control the rate of your descent with Up and Down

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
            //frost spark plus super speed
            player.moveSpeed += 0.5f;
            player.maxRunSpeed += 10f;
            player.runAcceleration += 1f;
            player.rocketBoots = 3;
            player.iceSkate = true;
            //arctic diving gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            //lava waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            //frog legs
            player.autoJump = true;
            player.jumpSpeedBoost += 2.4f;
            player.noFallDmg = true;
            //bundle
            if(player.wingTime == 0)
            {
                player.doubleJumpCloud = true;
                player.doubleJumpSandstorm = true;
                player.doubleJumpBlizzard = true;
            }
            player.jumpBoost = true;
            //slime mount
            player.maxFallSpeed += 5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            /*if (Fargowiltas.Instance.ThoriumLoaded)
            {
            /*
            TerrariumParticleSprinters
            AirWalkers
            SurvivalistBoots
            TravelersBoots
            WeightedWinglets
            
           
            
            }
            else
            {*/
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
            //}

            if (Fargowiltas.Instance.FargosLoaded)
                recipe.AddTile(ModLoader.GetMod("Fargowiltas"), "CrucibleCosmosSheet");
            else
                recipe.AddTile(TileID.LunarCraftingStation);
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
