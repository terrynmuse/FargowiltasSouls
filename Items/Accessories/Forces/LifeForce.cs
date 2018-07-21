using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Forces
{
    public class LifeForce : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Force of Life");
            Tooltip.SetDefault("'Rare is a living thing that dare disobey your will' \n" +
                                "25% increased defense \n" +
                                "100% of damage taken by melee attacks is reflected\n" +
                                "Greatly increases life regen \n" +
                                "Enemies drop hearts more often \n" +
                                "Enemies are more likely to target you \n" +
                                "Increases the strength of friendly bees \n" +
                                "Summon damage causes venom and has a chance to spawn additional bees and spiders\n" +
                                 "Beetles protect you from damage\n" +
                                "Summons several pets");
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 300000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            //crimson
            modPlayer.CrimsonEnchant = true;
            player.crimsonRegen = true;

            //bee + spider 
            if (Soulcheck.GetValue("Bees on Hit"))
            {
                modPlayer.BeeEnchant = true;
            }
            modPlayer.SpiderEnchant = true;
            player.strongBees = true;

            //beetle + turtle
            modPlayer.TurtleEnchant = true;
            player.aggro += 50;
            player.thorns = 1f;
            player.turtleThorns = true;
            player.statDefense = (int)(player.statDefense * 1.25);
            if (Soulcheck.GetValue("Beetles"))
            {
                player.beetleDefense = true;
                player.beetleCounter += 1f;
                int num5 = 180;
                if (player.beetleCounter >= num5)
                {
                    if (player.beetleOrbs > 0 && player.beetleOrbs < 3)
                    {
                        for (int k = 0; k < 22; k++)
                        {
                            if (player.buffType[k] >= 95 && player.buffType[k] <= 96)
                            {
                                player.DelBuff(k);
                            }
                        }
                    }
                    if (player.beetleOrbs < 3)
                    {
                        player.AddBuff(95 + player.beetleOrbs, 5, false);
                        player.beetleCounter = 0f;
                    }
                    else
                    {
                        player.beetleCounter = num5;
                    }
                }

                if (!player.beetleDefense && !player.beetleOffense)
                {
                    player.beetleCounter = 0f;
                }
                else
                {
                    player.beetleFrameCounter++;
                    if (player.beetleFrameCounter >= 1)
                    {
                        player.beetleFrameCounter = 0;
                        player.beetleFrame++;
                        if (player.beetleFrame > 2)
                        {
                            player.beetleFrame = 0;
                        }
                    }
                    for (int l = player.beetleOrbs; l < 3; l++)
                    {
                        player.beetlePos[l].X = 0f;
                        player.beetlePos[l].Y = 0f;
                    }
                    for (int m = 0; m < player.beetleOrbs; m++)
                    {
                        player.beetlePos[m] += player.beetleVel[m];
                        Vector2[] expr_6EcCp0 = player.beetleVel;
                        int expr_6EcCp1 = m;
                        expr_6EcCp0[expr_6EcCp1].X = expr_6EcCp0[expr_6EcCp1].X + Main.rand.Next(-100, 101) * 0.005f;
                        Vector2[] expr71ACp0 = player.beetleVel;
                        int expr71ACp1 = m;
                        expr71ACp0[expr71ACp1].Y = expr71ACp0[expr71ACp1].Y + Main.rand.Next(-100, 101) * 0.005f;
                        float num6 = player.beetlePos[m].X;
                        float num7 = player.beetlePos[m].Y;
                        float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                        if (num8 > 100f)
                        {
                            num8 = 20f / num8;
                            num6 *= -num8;
                            num7 *= -num8;
                            int num9 = 10;
                            player.beetleVel[m].X = (player.beetleVel[m].X * (num9 - 1) + num6) / num9;
                            player.beetleVel[m].Y = (player.beetleVel[m].Y * (num9 - 1) + num7) / num9;
                        }
                        else if (num8 > 30f)
                        {
                            num8 = 10f / num8;
                            num6 *= -num8;
                            num7 *= -num8;
                            int num10 = 20;
                            player.beetleVel[m].X = (player.beetleVel[m].X * (num10 - 1) + num6) / num10;
                            player.beetleVel[m].Y = (player.beetleVel[m].Y * (num10 - 1) + num7) / num10;
                        }
                        num6 = player.beetleVel[m].X;
                        num7 = player.beetleVel[m].Y;
                        num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
                        if (num8 > 2f)
                        {
                            player.beetleVel[m] *= 0.9f;
                        }
                        player.beetlePos[m] -= player.velocity * 0.25f;
                    }
                }
            }

            //pets
            if (player.whoAmI == Main.myPlayer)
            {
                if (Soulcheck.GetValue("Baby Face Monster Pet"))
                {
                    modPlayer.CrimsonPet = true;

                    if (player.FindBuffIndex(154) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.BabyFaceMonster] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.BabyFaceMonster, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.CrimsonPet = false;
                }
            }

            if (player.whoAmI == Main.myPlayer)
            {
                if (Soulcheck.GetValue("Turtle Pet"))
                {
                    modPlayer.TurtlePet = true;

                    if (player.FindBuffIndex(42) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.Turtle] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.Turtle, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.TurtlePet = false;
                }

                modPlayer.AddPet("Baby Hornet Pet", BuffID.BabyHornet, ProjectileID.BabyHornet);

                if (Soulcheck.GetValue("Spider Pet"))
                {
                    modPlayer.SpiderPet = true;

                    if (player.FindBuffIndex(81) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.Spider] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.Spider, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.SpiderPet = false;
                }

                //if(Soulcheck.GetValue("Baby Eater Pet"))
                //{
                modPlayer.CrimsonPet2 = true;

                if (player.FindBuffIndex(155) == -1)
                {
                    if (player.ownedProjectileCounts[ProjectileID.CrimsonHeart] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.CrimsonHeart, 0, 2f, Main.myPlayer);
                    }
                }
                //}
                //else
                //{
                //		modPlayer.crimsonPet2 = false;
                //}
            }



        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "CrimsonEnchant");
            recipe.AddIngredient(null, "BeeEnchant");
            recipe.AddIngredient(null, "SpiderEnchant");
            recipe.AddIngredient(null, "TurtleEnchant");
            recipe.AddIngredient(null, "BeetleEnchant");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


