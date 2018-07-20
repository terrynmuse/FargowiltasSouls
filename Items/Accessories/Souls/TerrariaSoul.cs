using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Accessories.Souls
{
    public class TerrariaSoul : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul of Terraria");
            Tooltip.SetDefault("'Welcome back master'\n" +
                                "All effects of material Forces, some upgraded");

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 24));
        }
        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
            item.rare = 10;
            item.value = 500000;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            FargoPlayer modPlayer = player.GetModPlayer<FargoPlayer>(mod);

            modPlayer.TerrariaSoul = true;
            if (Soulcheck.GetValue("Spore Explosion"))
            {
                modPlayer.JungleEnchant = true;
            }
            //miner
            player.pickSpeed -= 0.3f;
            if (Soulcheck.GetValue("Shine Buff"))
            {
                Lighting.AddLight(player.Center, 0.8f, 0.8f, 0f);
            }
            //cobalt
            modPlayer.CobaltEnchant = true;

            //palladium
            player.onHitRegen = true;

            //mythril	
            if (Soulcheck.GetValue("Increase Use Speed"))
            {
                modPlayer.FiringSpeed += .5f;
                modPlayer.CastingSpeed += .5f;
                modPlayer.ThrowingSpeed += .5f;
                modPlayer.RadiantSpeed += .5f;
                modPlayer.SymphonicSpeed += .5f;
                modPlayer.HealingSpeed += .5f;
                modPlayer.AxeSpeed += .5f;
                modPlayer.HammerSpeed += .5f;
                modPlayer.PickSpeed += .5f;
            }

            //orichalcum
            if (Soulcheck.GetValue("Orichalcum Fireball"))
            {
                player.onHitPetal = true;
                modPlayer.OriEnchant = true;
            }
            //titanium
            player.onHitDodge = true;
            player.kbBuff = true;

            if (!(player.FindBuffIndex(59) == -1))
            {
                player.magicDamage += .25f;
                player.meleeDamage += .25f;
                player.rangedDamage += .25f;
                player.minionDamage += .25f;
                player.thrownDamage += .25f;
            }

            //molten
            if (Soulcheck.GetValue("Inferno Buff"))
            {
                player.inferno = true;
                Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.65f, 0.4f, 0.1f);
                int num = 24;
                float num2 = 200f;
                bool flag = player.infernoCounter % 60 == 0;
                int damage = 100;
                if (player.whoAmI == Main.myPlayer)
                {
                    for (int l = 0; l < 200; l++)
                    {
                        NPC nPc = Main.npc[l];
                        if (nPc.active && !nPc.friendly && nPc.damage > 0 && !nPc.dontTakeDamage && !nPc.buffImmune[num] && Vector2.Distance(player.Center, nPc.Center) <= num2)
                        {
                            if (nPc.FindBuffIndex(num) == -1)
                            {
                                nPc.AddBuff(num, 120);
                            }
                            if (flag)
                            {
                                player.ApplyDamageToNPC(nPc, damage, 0f, 0, false);
                            }
                        }
                    }
                }
            }
            player.lavaImmune = true;

            //chloro
            if (Soulcheck.GetValue("Leaf Crystal"))
            {
                modPlayer.ChloroEnchant = true;

                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts[mod.ProjectileType("Chlorofuck")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("Chlorofuck"), 0, 0f, Main.myPlayer);
                    }
                }
            }

            //shroomite
            if (Soulcheck.GetValue("Shroomite Stealth"))
            {
                player.shroomiteStealth = true;
                modPlayer.ShroomEnchant = true;
            }

            //crimson
            player.crimsonRegen = true;

            //bee + spider 
            player.strongBees = true;
            if (Soulcheck.GetValue("Bees on Hit"))
            {
                modPlayer.BeeEnchant = true;
            }
            modPlayer.SpiderEnchant = true;

            if (Soulcheck.GetValue("Spelunker Buff"))
            {
                player.findTreasure = true;
            }
            if (Soulcheck.GetValue("Hunter Buff"))
            {
                player.detectCreature = true;
            }
            if (Soulcheck.GetValue("Dangersense Buff"))
            {
                player.dangerSense = true;
            }

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

            //necro
            //skeletons
            player.npcTypeNoAggro[21] = true;
            player.npcTypeNoAggro[31] = true;
            player.npcTypeNoAggro[32] = true;
            player.npcTypeNoAggro[33] = true;
            player.npcTypeNoAggro[34] = true;
            player.npcTypeNoAggro[44] = true;
            player.npcTypeNoAggro[45] = true;
            player.npcTypeNoAggro[77] = true;
            player.npcTypeNoAggro[110] = true;
            player.npcTypeNoAggro[167] = true;
            player.npcTypeNoAggro[172] = true;
            player.npcTypeNoAggro[197] = true;
            player.npcTypeNoAggro[201] = true;
            player.npcTypeNoAggro[202] = true;
            player.npcTypeNoAggro[203] = true;
            player.npcTypeNoAggro[269] = true;
            player.npcTypeNoAggro[270] = true;
            player.npcTypeNoAggro[271] = true;
            player.npcTypeNoAggro[272] = true;
            player.npcTypeNoAggro[273] = true;
            player.npcTypeNoAggro[274] = true;
            player.npcTypeNoAggro[275] = true;
            player.npcTypeNoAggro[276] = true;
            player.npcTypeNoAggro[277] = true;
            player.npcTypeNoAggro[278] = true;
            player.npcTypeNoAggro[279] = true;
            player.npcTypeNoAggro[280] = true;
            player.npcTypeNoAggro[281] = true;
            player.npcTypeNoAggro[282] = true;
            player.npcTypeNoAggro[283] = true;
            player.npcTypeNoAggro[284] = true;
            player.npcTypeNoAggro[285] = true;
            player.npcTypeNoAggro[286] = true;
            player.npcTypeNoAggro[287] = true;
            player.npcTypeNoAggro[291] = true;
            player.npcTypeNoAggro[292] = true;
            player.npcTypeNoAggro[293] = true;
            player.npcTypeNoAggro[294] = true;
            player.npcTypeNoAggro[295] = true;
            player.npcTypeNoAggro[296] = true;
            player.npcTypeNoAggro[322] = true;
            player.npcTypeNoAggro[323] = true;
            player.npcTypeNoAggro[324] = true;
            player.npcTypeNoAggro[449] = true;
            player.npcTypeNoAggro[450] = true;
            player.npcTypeNoAggro[451] = true;
            player.npcTypeNoAggro[452] = true;

            //forbidden
            player.buffImmune[194] = true;

            if (Soulcheck.GetValue("Forbidden Storm"))
            {
                player.setForbidden = true;
                player.UpdateForbiddenSetLock();
                Lighting.AddLight(player.Center, 0.8f, 0.7f, 0.2f);
            }
            //hallowed

            player.noKnockback = true;

            //shield and sword
            if (Soulcheck.GetValue("Hallowed Shield"))
            {
                modPlayer.HallowEnchant = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.ownedProjectileCounts[mod.ProjectileType("HallowProj")] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, mod.ProjectileType("HallowProj"), 80/*dmg*/, 2f, Main.myPlayer);
                    }
                }
            }
            //spectre
            modPlayer.SpectreEnchant = true;

            player.ghostHeal = true;
            player.ghostHurt = true;

            //spooky
            if (Soulcheck.GetValue("Spooky Scythes"))
            {
                modPlayer.SpookyEnchant = true;
            }
            modPlayer.MeteorEnchant = true;

            //solar
            if (Soulcheck.GetValue("Solar Shield"))
            {
                player.AddBuff(172, 5, false);
                player.setSolar = true;
                player.solarCounter++;
                int num11 = 240;
                if (player.solarCounter >= num11)
                {
                    if (player.solarShields > 0 && player.solarShields < 3)
                    {
                        for (int num12 = 0; num12 < 22; num12++)
                        {
                            if (player.buffType[num12] >= 170 && player.buffType[num12] <= 171)
                            {
                                player.DelBuff(num12);
                            }
                        }
                    }
                    if (player.solarShields < 3)
                    {
                        player.AddBuff(170 + player.solarShields, 5, false);
                        for (int num13 = 0; num13 < 16; num13++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(player.position, player.width, player.height, 6, 0f, 0f, 100)];
                            dust.noGravity = true;
                            dust.scale = 1.7f;
                            dust.fadeIn = 0.5f;
                            dust.velocity *= 5f;
                        }
                        player.solarCounter = 0;
                    }
                    else
                    {
                        player.solarCounter = num11;
                    }
                }
                for (int num14 = player.solarShields; num14 < 3; num14++)
                {
                    player.solarShieldPos[num14] = Vector2.Zero;
                }
                for (int num15 = 0; num15 < player.solarShields; num15++)
                {
                    player.solarShieldPos[num15] += player.solarShieldVel[num15];
                    Vector2 value = (player.miscCounter / 100f * 6.28318548f + num15 * (6.28318548f / player.solarShields)).ToRotationVector2() * 6f;
                    value.X = player.direction * 20;
                    player.solarShieldVel[num15] = (value - player.solarShieldPos[num15]) * 0.2f;
                }
                if (player.dashDelay >= 0)
                {
                    player.solarDashing = false;
                    player.solarDashConsumedFlare = false;
                }
                bool flag = player.solarDashing && player.dashDelay < 0;
                if (player.solarShields > 0 || flag)
                {
                    player.dash = 3;
                }
            }
            //vortex
            player.meleeCrit = FargoPlayer.VortexCrit;
            player.rangedCrit = FargoPlayer.VortexCrit;
            player.magicCrit = FargoPlayer.VortexCrit;
            player.thrownCrit = FargoPlayer.VortexCrit;

            //nebula
            if (player.nebulaCD > 0)
            {
                player.nebulaCD--;
            }
            player.setNebula = true;

            //stardust
            if (Soulcheck.GetValue("Stardust Guardian"))
            {
                modPlayer.StardustEnchant = true;
                player.setStardust = true;
                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.FindBuffIndex(187) == -1)
                    {
                        player.AddBuff(187, 3600);
                    }
                    if (player.ownedProjectileCounts[623] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, 623, 0, 0f, Main.myPlayer);
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

                if (Soulcheck.GetValue("Baby Dino Pet"))
                {
                    modPlayer.DinoPet = true;

                    if (player.FindBuffIndex(61) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.BabyDino] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.BabyDino, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.DinoPet = false;
                }

                if (Soulcheck.GetValue("Baby Eater Pet"))
                {
                    modPlayer.ShadowPet = true;

                    if (player.FindBuffIndex(45) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.BabyEater] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.BabyEater, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.ShadowPet = false;
                }

                if (Soulcheck.GetValue("Cursed Sapling Pet"))
                {
                    modPlayer.SaplingPet = true;

                    if (player.FindBuffIndex(85) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.CursedSapling] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.CursedSapling, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.SaplingPet = false;
                }

                if (Soulcheck.GetValue("Wisp Pet"))
                {
                    modPlayer.SpectrePet = true;

                    if (player.FindBuffIndex(57) == -1)
                    {
                        if (player.ownedProjectileCounts[ProjectileID.Wisp] < 1)
                        {
                            Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.Wisp, 0, 2f, Main.myPlayer);
                        }
                    }
                }
                else
                {
                    modPlayer.SpectrePet = false;
                }

                //if(Soulcheck.GetValue("Baby Eater Pet"))
                //{
                modPlayer.ShadowPet2 = true;

                if (player.FindBuffIndex(19) == -1)
                {
                    if (player.ownedProjectileCounts[ProjectileID.ShadowOrb] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.ShadowOrb, 0, 2f, Main.myPlayer);
                    }
                }
                //}
                //else
                //{
                //		modPlayer.shadowPet2 = false;
                //}

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

                //if(Soulcheck.GetValue("Baby Face Monster Pet"))
                //{
                modPlayer.LanternPet = true;

                if (player.FindBuffIndex(152) == -1)
                {
                    if (player.ownedProjectileCounts[ProjectileID.MagicLantern] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, ProjectileID.MagicLantern, 0, 2f, Main.myPlayer);
                    }
                }
                //}
                //else
                //{
                //	modPlayer.lanternPet = false;
                //}
            }


        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "EarthForce");
            recipe.AddIngredient(null, "NatureForce");
            recipe.AddIngredient(null, "LifeForce");
            recipe.AddIngredient(null, "SpiritForce");
            recipe.AddIngredient(null, "CosmoForce");

            //recipe.AddTile(null, "CrucibleCosmosSheet");
            recipe.SetResult(this);
            recipe.AddRecipe();

        }
    }
}


