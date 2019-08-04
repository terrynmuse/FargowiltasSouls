using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    //[AutoloadEquip(EquipType.Shoes)]
    public class SupersonicSoul : ModItem
    {
        private readonly Mod thorium = ModLoader.GetMod("ThoriumMod");
        private readonly Mod calamity = ModLoader.GetMod("CalamityMod");

        //air walker meme'
        public bool jumped;
        public bool canHover;
        public int hoverTimer;
        public int jumpTimer;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Supersonic Soul");

            string tooltip = 
@"'I am speed'
Allows Supersonic running, flight, and extra mobility on ice
Allows the holder to quuintuple jump if no wings are equipped
Increases jump height, jump speed, and allows auto-jump
Grants the ability to swim and greatly extends underwater breathing
Provides the ability to walk on water and lava
Grants immunity to lava and fall damage
Effects of Flying Carpet";
            string tooltip_ch =
@"'我就是速度'
获得超音速奔跑,飞行,以及额外的冰上移动力
在没有装备翅膀时,允许使用者进行四段跳
增加跳跃高度,跳跃速度,允许自动跳跃
获得游泳能力以及极长的水下呼吸时间
获得水/岩浆上行走能力
免疫岩浆和坠落伤害
拥有飞毯效果";

            if (thorium != null)
            {
                tooltip += "\nEffects of Air Walkers, Survivalist Boots, and Weighted Winglets";
                tooltip_ch += "\n拥有履空靴,我命至上主义者之飞靴和举足轻重靴的效果";
            }

            Tooltip.SetDefault(tooltip);
            DisplayName.AddTranslation(GameCulture.Chinese, "超音速之魂");
            Tooltip.AddTranslation(GameCulture.Chinese, tooltip_ch);
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.value = 750000;
            item.rare = 11;
        }

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            foreach (TooltipLine tooltipLine in list)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color?(new Color(238, 0, 69));
                }
            }
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            //frost spark plus super speed
            if (Soulcheck.GetValue("Supersonic Speed Boosts") && !player.GetModPlayer<FargoPlayer>().noSupersonic)
            {
                player.maxRunSpeed += 10f;
                player.runAcceleration += .25f;
                //frog legs
                player.autoJump = true;
                player.jumpSpeedBoost += 2.4f;
                player.maxFallSpeed += 5f;
                player.jumpBoost = true;
            }
            /*else
            {
                player.maxRunSpeed += 5f;
                player.runAcceleration += .1f;
            }*/

            player.moveSpeed += 0.5f;
            player.rocketBoots = 3;
            player.rocketTimeMax = 10;
            player.iceSkate = true;
            //arctic diving gear
            player.arcticDivingGear = true;
            player.accFlipper = true;
            player.accDivingHelm = true;
            //lava waders
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaImmune = true;
            player.noFallDmg = true;
            //bundle
            if(player.wingTime == 0)
            {
                player.doubleJumpCloud = true;
                player.doubleJumpSandstorm = true;
                player.doubleJumpBlizzard = true;
                player.doubleJumpFart = true;
            }
            //magic carpet
             player.carpet = true;

            if (Fargowiltas.Instance.ThoriumLoaded) Thorium(player);
        }

        private void Thorium(Player player)
        {
            ThoriumPlayer thoriumPlayer = player.GetModPlayer<ThoriumPlayer>(thorium);
            //terrarium particle sprinters dust
            if (Collision.SolidCollision(player.position, player.width, player.height + 4) && Math.Abs(player.velocity.X) >= 2)
            {
                for (int i = 0; i < 1; i++)
                {
                    int num = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 57, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].noLight = true;
                    Dust dust = Main.dust[num];
                    dust.velocity *= 0f;
                }
                for (int j = 0; j < 1; j++)
                {
                    int num2 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 61, 0f, 0f, 100, default(Color), 1.35f);
                    Main.dust[num2].noGravity = true;
                    Main.dust[num2].noLight = true;
                    Dust dust2 = Main.dust[num2];
                    dust2.velocity *= 0f;
                }
                for (int k = 0; k < 1; k++)
                {
                    int num3 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 229, 0f, 0f, 100, default(Color), 1.15f);
                    Main.dust[num3].noGravity = true;
                    Main.dust[num3].noLight = true;
                    Dust dust3 = Main.dust[num3];
                    dust3.velocity *= 0f;
                }
                for (int l = 0; l < 1; l++)
                {
                    int num4 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 60, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num4].noGravity = true;
                    Main.dust[num4].noLight = true;
                    Dust dust4 = Main.dust[num4];
                    dust4.velocity *= 0f;
                }
                for (int m = 0; m < 1; m++)
                {
                    int num5 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 127, 0f, 0f, 100, default(Color), 1.75f);
                    Main.dust[num5].noGravity = true;
                    Main.dust[num5].noLight = true;
                    Dust dust5 = Main.dust[num5];
                    dust5.velocity *= 0f;
                }
                for (int n = 0; n < 1; n++)
                {
                    int num6 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 59, 0f, 0f, 100, default(Color), 1.4f);
                    Main.dust[num6].noGravity = true;
                    Main.dust[num6].noLight = true;
                    Dust dust6 = Main.dust[num6];
                    dust6.velocity *= 0f;
                }
                for (int num7 = 0; num7 < 1; num7++)
                {
                    int num8 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 62, 0f, 0f, 100, default(Color), 1.35f);
                    Main.dust[num8].noGravity = true;
                    Main.dust[num8].noLight = true;
                    Dust dust7 = Main.dust[num8];
                    dust7.velocity *= 0f;
                }
            }
            //air walkers
            if (Soulcheck.GetValue("Air Walkers"))
            {
                if (player.controlDown)
                {
                    jumped = true;
                }
                else
                {
                    jumped = false;
                }
                if (!Collision.SolidCollision(player.position, player.width, player.height + 4))
                {
                    hoverTimer++;
                }
                else
                {
                    hoverTimer = 0;
                }
                if (hoverTimer >= 250)
                {
                    canHover = false;
                }
                else
                {
                    canHover = true;
                }
                if (canHover && !jumped && !Collision.SolidCollision(player.position, player.width, player.height + 4))
                {
                    player.maxFallSpeed = 0f;
                    player.fallStart = (int)(player.position.Y / 16f);
                    int num = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 4, 4, 229, 0f, 0f, 100, default(Color), 1.25f);
                    Main.dust[num].noGravity = true;
                    Main.dust[num].noLight = true;
                    Dust dust = Main.dust[num];
                    dust.velocity *= 0f;
                }
            }
            //survivalist boots
            if (Math.Abs(player.velocity.X) > 2f)
            {
                player.lifeRegen += 2;
                player.lifeRegenTime++;
                player.manaRegenBonus += 2;
                player.manaRegenDelayBonus++;
                thoriumPlayer.bardResourceRecharge += 2;
            }
            //weighted winglets
            if (player.controlDown && !player.controlUp)
            {
                player.maxFallSpeed *= (player.wet ? 2.4f : 1.6f);
            }
            if (player.controlUp && !player.controlDown)
            {
                player.maxFallSpeed *= 0.4f;
                player.fallStart = (int)(player.position.Y / 16f);
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            if (Fargowiltas.Instance.ThoriumLoaded)
            {
                recipe.AddIngredient(thorium.ItemType("TerrariumBoots"));
                recipe.AddIngredient(thorium.ItemType("AirWalkers"));
                recipe.AddIngredient(thorium.ItemType("SurvivalistBoots"));
                recipe.AddIngredient(thorium.ItemType("WeightedWinglets"));
                recipe.AddIngredient(ItemID.ArcticDivingGear);
            }
            else
            {
                recipe.AddIngredient(ItemID.FrostsparkBoots);
                recipe.AddIngredient(ItemID.LavaWaders);
                recipe.AddIngredient(ItemID.ArcticDivingGear);
            }

            if (Fargowiltas.Instance.CalamityLoaded)
            {
                recipe.AddIngredient(calamity.ItemType("MOAB"));
            }
            else
            {
                recipe.AddIngredient(ItemID.FrogLeg);
                recipe.AddIngredient(ItemID.BundleofBalloons);
            }

            recipe.AddIngredient(ItemID.BalloonHorseshoeFart);
            recipe.AddIngredient(ItemID.FlyingCarpet);
            recipe.AddIngredient(ItemID.MinecartMech);
            recipe.AddIngredient(ItemID.BlessedApple);
            recipe.AddIngredient(ItemID.AncientHorn);
            recipe.AddIngredient(ItemID.ReindeerBells);
            recipe.AddIngredient(ItemID.BrainScrambler);

            recipe.AddTile(mod, "CrucibleCosmosSheet");
                
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
