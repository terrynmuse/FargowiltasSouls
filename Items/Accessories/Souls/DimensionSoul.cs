using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ID.BuffID;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    [AutoloadEquip(EquipType.Wings)]
    public class DimensionSoul : ModItem
    {
        private readonly Mod _calamity = ModLoader.GetMod("CalamityMod");

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Dimensions");



            Tooltip.SetDefault("'The dimensions of Terraria are at your fingertips'"
                + "\nDoes various things");
            //33% chance to instantly heal back 75% of any damage taken
            /* + "\nActs as wings \nAllows for infinite flight"
             + "\n30% damage reduction \nIncreases life regeneration by 15 \nIncreases HP by 500 \nReflect 100% of damage back to attackers \nEffects of the Star Veil, Paladin's Shield, and Frozen Turtle Shell \nGrants immunity to even more debuffs"
             + "\n25% increased movement speed \nAllows supersonic fast running, and extra mobility on ice \nAllows the player to dash into the enemy"
             + "\nNear infinite block placement and mining reach \nIncreased block and wall placement speed by 50% \nMining speed tripled and Auto paint effect \nSeveral common enemies are harmless"
             + "\nIncreases fishing skill massively \nPermanent Sonar and Crate Buffs \nAll other effects of material souls");*/
            /* if(Fargowiltas.instance.thoriumLoaded)
             {
                 Tooltip.SetDefault("'The dimensions of Terraria are at your fingertips'"
                 + "\nActs as wings \nAllows for infinite flight"
                 + "\n30% damage reduction \nIncreases life regeneration by 15 \nIncreases HP by 500 \nReflect 100% of damage back to attackers \nEffects of the Star Veil and Terrarium Defender \nAllows you to detect enemies and hazards around you \nGrants immunity to even more debuffs"
                 + "\n25% increased movement speed \nAllows supersonic fast running, and extra mobility on ice \nAllows the player to dash into the enemy"
                 + "\nNear infinite block placement and mining reach \nIncreased block and wall placement speed by 50% \nMining speed tripled and Auto paint effect \nSeveral common enemies are harmless"
                 + "*/

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 18));

        }
        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 32;
            item.accessory = true;
            item.defense = 12;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.value = 1500000;
            item.rare = -12;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<FargoPlayer>(mod).DimensionSoul = true;

            //tank
            player.endurance += 0.30f;
            player.lifeRegen += 15;
            player.thorns += 1f;
            player.aggro += 100;
            player.statLifeMax2 += 500;
            player.statDefense = (int)(player.statDefense * 1.25);

            //ankh shield
            player.buffImmune[46] = true; //chilled
            player.noKnockback = true;
            player.fireWalk = true;
            player.buffImmune[33] = true; //weak
            player.buffImmune[36] = true; //broken armor
            player.buffImmune[30] = true; //bleeding
            player.buffImmune[20] = true; //Poisoned
            player.buffImmune[32] = true; //slow
            player.buffImmune[31] = true; //Confused
            player.buffImmune[35] = true; //silenced
            player.buffImmune[23] = true; //cursed
            player.buffImmune[22] = true; //darkness

            player.buffImmune[44] = true; //Frostburn
            player.buffImmune[47] = true; //Frozen
            player.buffImmune[24] = true; //Fire
            player.buffImmune[69] = true; //Ichor
            player.buffImmune[70] = true; //Venom
            player.buffImmune[80] = true; //Black Out
            player.buffImmune[156] = true; //Stoned

            player.buffImmune[88] = true; //chaos state
            player.buffImmune[37] = true; //horrified
            player.buffImmune[39] = true; //cursed inferno
            player.buffImmune[68] = true; //suffocation

            // sweet vengeance or star veil
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                player.panic = true;
                player.starCloak = true;
                player.longInvince = true;
            }

            if (!Fargowiltas.Instance.ThoriumLoaded)
            {
                player.starCloak = true;
                player.longInvince = true;
            }

            // shiny stone
            player.shinyStone = true;

            //charm of myths
            player.pStone = true;

            //paladin
            if (player.statLife > player.statLifeMax2 * .20)
            {
                player.AddBuff(PaladinsShield, 30);
            }

            //frozenshell
            if (player.statLife < player.statLifeMax2 * .6)
            {
                player.AddBuff(IceBarrier, 30);
            }

            //celestial shell
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }

            //wings
            player.ignoreWater = true;

            player.wingTimeMax = 99999;

            //speed

            //arctic diving gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            player.iceSkate = true;
            player.ignoreWater = true;


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


            //balloons
            player.noFallDmg = true;
            player.jumpBoost = true;

            //honey
            player.bee = true;

            //shield of cthulu
            if (!Fargowiltas.Instance.CalamityLoaded)
            {
                player.dash = 2;
            }


            //slime mount
            player.maxFallSpeed += 6f;
            player.autoJump = true;

            //builder

            player.tileSpeed += 0.5f;
            player.wallSpeed += 0.5f;


            //toolbox
            Player.tileRangeX += 50;//WORKS
            Player.tileRangeY += 50;

            //gizmo pack
            player.autoPaint = true;

            //pick axe stuff
            player.pickSpeed -= 0.66f;

            if (!hideVisual)
            {
                /*player.magicDamage*= 0f;
                player.meleeDamage*= 0f;
                player.rangedDamage*= 0f;
                player.minionDamage*= 0f;
                player.thrownDamage*= 0f;*/

                player.GetModPlayer<FargoPlayer>(mod).BuilderMode = true;
            }


            //mining helmet
            if (Soulcheck.GetValue("Shine Buff"))
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }
            //fishing
            player.sonarPotion = true;
            player.fishingSkill += 50;
            player.cratePotion = true;
            player.accFishingLine = true;
            player.accTackleBox = true;
            player.accFishFinder = true;

            //froglegs
            player.autoJump = true;
            player.jumpSpeedBoost += 2.5f;

            if (Soulcheck.GetValue("Spore Sac"))
            {
                //spore sac
                player.SporeSac();
                player.sporeSac = true;
            }

        player.GetModPlayer<FargoPlayer>(mod).FishSoul2 = true;

            //dread eye
            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                if (Soulcheck.GetValue("Dangersense Buff"))
                {
                    player.dangerSense = true;
                }
                if (Soulcheck.GetValue("Hunter Buff"))
                {
                    player.detectCreature = true;
                }
            }

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                player.buffImmune[_calamity.BuffType("BrimstoneFlames")] = true;
                player.buffImmune[_calamity.BuffType("HolyLight")] = true;
                player.buffImmune[_calamity.BuffType("GlacialState")] = true;

                CalamityTank(player);

                CalamityBoots(player);
            }

            if (Fargowiltas.Instance.BlueMagicLoaded)
            {
                BlueTank(player);
            }
        }

        private void CalamityTank(Player player)
        {
            CalamityMod.CalamityPlayer calamityPlayer = player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity);
            
            calamityPlayer.elysianAegis = true;
            calamityPlayer.dashMod = 4;
            calamityPlayer.aSpark = true;
            calamityPlayer.gShell = true;
            calamityPlayer.fCarapace = true;
            calamityPlayer.absorber = true;
        }

        private void CalamityBoots(Player player)
        {
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).IBoots = true;
            player.GetModPlayer<CalamityMod.CalamityPlayer>(_calamity).elysianFire = true;
        }

        private void BlueTank(Player player)
        {
            player.GetModPlayer<Bluemagic.BluemagicPlayer>(ModLoader.GetMod("Bluemagic")).lifeMagnet2 = true;
            player.GetModPlayer<Bluemagic.BluemagicPlayer>(ModLoader.GetMod("Bluemagic")).crystalCloak = true;
        }

        // ReSharper disable twice RedundantAssignment
        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
            ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 0.85f;
            ascentWhenRising = 0.25f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 3f;
            constantAscend = 0.135f;
        }

        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 18f;
            acceleration *= 3.5f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(null, "ColossusSoul");
            recipe.AddIngredient(null, "SupersonicSoul");
            recipe.AddIngredient(null, "FlightMasterySoul");
            recipe.AddIngredient(null, "WorldShaperSoul");
            recipe.AddIngredient(null, "TrawlerSoul");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

}