using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class SupersonicSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supersonic Soul");
            Tooltip.SetDefault("'Sound barriers forever broken' \n25% increased movement speed \nAllows supersonic fast running, and extra mobility on ice \nProvides lava immunity and permanent light \nGrants the ability to swim and greatly extends underwater breathing \nIncreases jump height, allows auto jump, and negates fall damage \nAllows the player to dash into the enemy");

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                Tooltip.SetDefault("'Sound barriers forever broken' \n25% increased movement speed \nAllows supersonic fast running, and extra mobility on ice \nProvides lava immunity and permanent light \nGrants the ability to swim and greatly extends underwater breathing \nIncreases jump height, allows auto jump, and negates fall damage \nAllows the player to dash into the enemy \nReflects 35% of damage back to attackers");
            }
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

            //arctic diving gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;
            player.ignoreWater = true;

            if (player.wet)
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);

            //frostspark
            if (Soulcheck.GetValue("Super Speed"))
            {
                player.GetModPlayer<FargoPlayer>(mod).SpeedEffect = true;
                player.accRunSpeed = 2.00f;
                player.moveSpeed += 5f;
            }
            else
            {
                player.accRunSpeed = 35.00f;
                player.moveSpeed += 0.25f;
            }

            player.rocketBoots = 3;
            player.iceSkate = true;

            //lava waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;

            //spiked stompers
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                player.thorns += 0.35f;
            }

            //slime mount
            player.maxFallSpeed += 5f;
            player.autoJump = true;

            //elysian tracers
            if (Fargowiltas.Instance.CalamityLoaded && !hideVisual)
            {
                CalamityBoots(player);
            }

            //shield of cthulu
            player.dash = 2;

        }

        public void CalamityBoots(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).IBoots = true;
            player.GetModPlayer<CalamityMod.CalamityPlayer>(ModLoader.GetMod("CalamityMod")).elysianFire = true;
        }


        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    //thorium and calamity
                    recipe.AddIngredient(ItemID.EoCShield);
                    recipe.AddIngredient(ItemID.BundleofBalloons);
                    recipe.AddIngredient(ItemID.ArcticDivingGear);
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("SpikedStompers"));
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerrariumBoots"));
                    recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("ElysianTracers"));
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("WarpCore"));
                    recipe.AddIngredient(ItemID.SlimySaddle);
                    recipe.AddIngredient(ItemID.FuzzyCarrot);
                    recipe.AddIngredient(ItemID.BlessedApple);
                    recipe.AddIngredient(ItemID.AncientHorn);
                    recipe.AddIngredient(ItemID.ShrimpyTruffle);
                    recipe.AddIngredient(ItemID.ReindeerBells);
                    recipe.AddIngredient(ItemID.BrainScrambler);
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    //just thorium
                    recipe.AddIngredient(ItemID.EoCShield);
                    recipe.AddIngredient(ItemID.BundleofBalloons);
                    recipe.AddIngredient(ItemID.ArcticDivingGear);
                    recipe.AddIngredient(ItemID.FlowerBoots);
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("SpikedStompers"));
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("TerrariumBoots"));
                    recipe.AddIngredient(ModLoader.GetMod("ThoriumMod").ItemType("WarpCore"));
                    recipe.AddIngredient(ItemID.SlimySaddle);
                    recipe.AddIngredient(ItemID.FuzzyCarrot);
                    recipe.AddIngredient(ItemID.BlessedApple);
                    recipe.AddIngredient(ItemID.AncientHorn);
                    recipe.AddIngredient(ItemID.ShrimpyTruffle);
                    recipe.AddIngredient(ItemID.ReindeerBells);
                    recipe.AddIngredient(ItemID.BrainScrambler);
                }
            }

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    //just calamity
                    recipe.AddIngredient(ItemID.EoCShield);
                    recipe.AddIngredient(ItemID.FlyingCarpet);
                    recipe.AddIngredient(ItemID.BundleofBalloons);
                    recipe.AddIngredient(ItemID.ArcticDivingGear);
                    recipe.AddIngredient(ItemID.FlowerBoots);
                    recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("ElysianTracers"));
                    recipe.AddIngredient(ItemID.SlimySaddle);
                    recipe.AddIngredient(ItemID.FuzzyCarrot);
                    recipe.AddIngredient(ItemID.BlessedApple);
                    recipe.AddIngredient(ItemID.AncientHorn);
                    recipe.AddIngredient(ItemID.ShrimpyTruffle);
                    recipe.AddIngredient(ItemID.ReindeerBells);
                    recipe.AddIngredient(ItemID.BrainScrambler);
                }

                if (!Fargowiltas.Instance.CalamityLoaded)
                {
                    //no others
                    recipe.AddIngredient(ItemID.ArcticDivingGear);
                    recipe.AddIngredient(ItemID.BundleofBalloons);
                    recipe.AddIngredient(ItemID.FlyingCarpet);
                    recipe.AddIngredient(ItemID.FrostsparkBoots);
                    recipe.AddIngredient(ItemID.LavaWaders);
                    //green horseshoe balloon
                    //pink horsehsoe balloon





                    recipe.AddIngredient(ItemID.EoCShield);
                    
                    
                    
                    recipe.AddIngredient(ItemID.FlowerBoots);
                    
                    recipe.AddIngredient(ItemID.SlimySaddle);
                    recipe.AddIngredient(ItemID.FuzzyCarrot);
                    recipe.AddIngredient(ItemID.BlessedApple);
                    recipe.AddIngredient(ItemID.AncientHorn);
                    recipe.AddIngredient(ItemID.ShrimpyTruffle);
                    recipe.AddIngredient(ItemID.ReindeerBells);
                    recipe.AddIngredient(ItemID.BrainScrambler);
                }
            }

            //speed.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}