using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.NPCs.MutantBoss
{
    [AutoloadBossHead]
    public class MutantBoss : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant");
            DisplayName.AddTranslation(GameCulture.Chinese, "突变体");
            Main.npcFrameCount[npc.type] = 4;
        }

        public override void SetDefaults()
        {
            npc.width = 34;
            npc.height = 50;
            npc.damage = 360;
            npc.defense = 400;
            npc.lifeMax = 7700000;
            npc.HitSound = SoundID.NPCHit57;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.npcSlots = 50f;
            npc.knockBackResist = 0f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.aiStyle = -1;
            npc.netAlways = true;
            npc.hide = true;
            npc.buffImmune[mod.BuffType("Sadism")] = true;
            npc.buffImmune[mod.BuffType("ClippedWings")] = true;
            npc.buffImmune[mod.BuffType("MutantNibble")] = true;
            npc.buffImmune[mod.BuffType("OceanicMaul")] = true;
            npc.buffImmune[mod.BuffType("TimeFrozen")] = true;
            npc.timeLeft = NPC.activeTime * 30;
            if (FargoSoulsWorld.AngryMutant || Fargowiltas.Instance.CalamityLoaded)
            {
                npc.lifeMax = 377000000;
                npc.damage *= 2;
                npc.defense *= 10;
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("ExoFreeze")] = true;
                    npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("GlacialState")] = true;
                    npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TemporalSadness")] = true;
                    npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("SilvaStun")] = true;
                    npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("TimeSlow")] = true;
                    npc.buffImmune[ModLoader.GetMod("CalamityMod").BuffType("PearlAura")] = true;
                }
            }
            npc.GetGlobalNPC<FargoSoulsGlobalNPC>().SpecialEnchantImmune = true;
            //music = MusicID.TheTowers;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/rePrologue");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 360;
            npc.lifeMax = (int)(7700000 * bossLifeScale);
            if (FargoSoulsWorld.AngryMutant || Fargowiltas.Instance.CalamityLoaded)
            {
                npc.lifeMax = (int)(377000000 * bossLifeScale);
                npc.damage *= 2;
            }
        }

        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            cooldownSlot = 1;
            return true;
        }

        public override void AI()
        {
            FargoSoulsGlobalNPC.mutantBoss = npc.whoAmI;

            if (npc.localAI[3] == 0)
            {
                npc.TargetClosest();
                if (npc.timeLeft < 60)
                    npc.timeLeft = 60;
                if (npc.Distance(Main.player[npc.target].Center) < 2000)
                {
                    npc.localAI[3] = 1;
                    Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    EdgyBossText("I hope you're ready to embrace suffering.");
                    if (Main.netMode != 1)
                    {
                        int number = 0;
                        for (int index = 999; index >= 0; --index)
                        {
                            if (!Main.projectile[index].active)
                            {
                                number = index;
                                break;
                            }
                        }
                        if (number >= 0)
                        {
                            if (Main.netMode == 0)
                            {
                                Projectile projectile = Main.projectile[number];
                                projectile.SetDefaults(mod.ProjectileType("MutantBoss"));
                                projectile.Center = npc.Center;
                                projectile.owner = Main.myPlayer;
                                projectile.velocity.X = 0;
                                projectile.velocity.Y = 0;
                                projectile.damage = 0;
                                projectile.knockBack = 0f;
                                projectile.identity = number;
                                projectile.gfxOffY = 0f;
                                projectile.stepSpeed = 1f;
                                projectile.ai[1] = npc.whoAmI;
                            }
                            else if (Main.netMode == 2)
                            {
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantBoss"), 0, 0f, Main.myPlayer, 0, npc.whoAmI);
                            }
                        }
                    }
                }
            }
            else if (npc.localAI[3] == 1)
            {
                Aura(2000f, mod.BuffType("GodEater"), true, 86);
            }
            else if (Main.player[Main.myPlayer].active && npc.Distance(Main.player[Main.myPlayer].Center) < 3000f)
            {
                Main.player[Main.myPlayer].AddBuff(mod.BuffType("MutantPresence"), 2);
                if (Fargowiltas.Instance.CalamityLoaded)
                {
                    Main.player[Main.myPlayer].buffImmune[ModLoader.GetMod("CalamityMod").BuffType("RageMode")] = true;
                    Main.player[Main.myPlayer].buffImmune[ModLoader.GetMod("CalamityMod").BuffType("AdrenalineMode")] = true;
                }
            }

            Player player = Main.player[npc.target];
            npc.direction = npc.spriteDirection = npc.position.X < player.position.X ? 1 : -1;
            Vector2 targetPos;
            float speedModifier;
            switch ((int)npc.ai[0])
            {
                case -7: //fade out, drop a mutant
                    npc.velocity = Vector2.Zero;
                    npc.dontTakeDamage = true;
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 2.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                    if (++npc.alpha > 255)
                    {
                        npc.alpha = 255;
                        npc.NPCLoot();
                        npc.life = 0;
                        npc.active = false;
                        if (Main.netMode != 1 && Fargowiltas.Instance.FargosLoaded && !NPC.AnyNPCs(ModLoader.GetMod("Fargowiltas").NPCType("Mutant")))
                        {
                            int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, ModLoader.GetMod("Fargowiltas").NPCType("Mutant"));
                            if (n != 200 && Main.netMode == 2)
                                NetMessage.SendData(23, -1, -1, null, n);
                        }
                        EdgyBossText("Oh, right... my revive...");
                    }
                    break;

                case -6: //actually defeated
                    if (!AliveCheck(player))
                        break;
                    npc.ai[3] -= (float)Math.PI / 6f / 60f;
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 120)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]--;
                        npc.ai[1] = 0;
                        npc.ai[3] = (float)-Math.PI / 2;
                        npc.netUpdate = true;
                        if (Main.netMode != 1) //shoot harmless mega ray
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * -1, mod.ProjectileType("MutantGiantDeathray"), 0, 0f, Main.myPlayer, 0, npc.whoAmI);
                        EdgyBossText("I have not a single regret in my existence!");
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    break;

                case -5: //FINAL SPARK
                    if (!AliveCheck(player))
                        break;
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 90)
                    {
                        npc.ai[1] = 0;
                        if (Main.netMode != 1)
                        {
                            SpawnSphereRing(7, 7f, npc.defDamage / 2, 0.5f);
                            SpawnSphereRing(7, 7f, npc.defDamage / 2, -.5f);
                        }
                    }
                    if (++npc.ai[2] > 1020)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]--;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        for (int i = 0; i < 1000; i++)
                            if (Main.projectile[i].active && Main.projectile[i].hostile && Main.projectile[i].timeLeft > 2)
                                Main.projectile[i].timeLeft = 2;
                    }
                    else if (npc.ai[2] == 420 && Main.netMode != 1)
                    {
                        npc.ai[3] = npc.DirectionFrom(player.Center).ToRotation();
                        Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(npc.ai[3]), mod.ProjectileType("MutantGiantDeathray2"), npc.defDamage, 0f, Main.myPlayer, 0, npc.whoAmI);
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    npc.ai[3] -= 2f * (float)Math.PI * 1f / 6f / 60f;
                    break;

                case -4: //true boundary
                    if (!AliveCheck(player))
                        break;
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 3)
                    {
                        Main.PlaySound(SoundID.Item12, npc.Center);
                        npc.ai[1] = 0;
                        npc.ai[2] += (float)Math.PI / 5 / 480 * npc.ai[3];
                        if (npc.ai[2] > (float)Math.PI)
                            npc.ai[2] -= (float)Math.PI * 2;
                        if (Main.netMode != 1)
                            for (int i = 0; i < 8; i++)
                                Projectile.NewProjectile(npc.Center, new Vector2(6f, 0).RotatedBy(npc.ai[2] + Math.PI / 4 * i), mod.ProjectileType("MutantEye"), npc.defDamage / 3, 0f, Main.myPlayer);
                    }
                    if (++npc.ai[3] > 480)
                    {
                        npc.TargetClosest();
                        npc.ai[0]--;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    break;

                case -3: //okuu nonspell
                    if (!AliveCheck(player))
                        break;
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 10 && npc.ai[3] > 60 && npc.ai[3] < 300)
                    {
                        npc.ai[1] = 0;
                        SpawnSphereRing(12, 12f, npc.defDamage / 3, -1f);
                        SpawnSphereRing(12, 12f, npc.defDamage / 3, 1f);
                    }
                    if (++npc.ai[3] > 420)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]--;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.TargetClosest();
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    break;

                case -2: //final void rays
                    if (!AliveCheck(player))
                        break;
                    npc.velocity = Vector2.Zero;
                    if (--npc.ai[1] < 0)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, new Vector2(2, 0).RotatedBy(npc.ai[2]), mod.ProjectileType("MutantMark1"), npc.defDamage / 3, 0f, Main.myPlayer);
                        npc.ai[1] = 2;
                        npc.ai[2] += npc.ai[3];
                        if (npc.localAI[0]++ == 30 || npc.localAI[0] == 60)
                        {
                            npc.netUpdate = true;
                            npc.ai[2] -= npc.ai[3] / 2;
                        }
                        else if (npc.localAI[0] == 90)
                        {
                            npc.netUpdate = true;
                            npc.ai[0]--;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            npc.localAI[0] = 0;
                        }
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    break;

                case -1: //defeated
                    npc.dontTakeDamage = true;
                    npc.damage = 0;
                    if (npc.buffType[0] != 0)
                        npc.DelBuff(0);
                    if (++npc.ai[1] > 120)
                    {
                        targetPos = player.Center;
                        targetPos.Y -= 300;
                        Movement(targetPos, 1f);
                        if (npc.Distance(targetPos) < 50 || npc.ai[1] > 300)
                        {
                            npc.netUpdate = true;
                            npc.velocity = Vector2.Zero;
                            npc.localAI[0] = 0;
                            npc.ai[0]--;
                            npc.ai[1] = 0;
                            npc.ai[2] = npc.DirectionFrom(player.Center).ToRotation();
                            npc.ai[3] = (float)Math.PI / 15f;
                            Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                            if (player.Center.X < npc.Center.X)
                                npc.ai[3] *= -1;
                            EdgyBossText("But we're not done yet!");
                        }
                    }
                    else
                    {
                        npc.velocity *= 0.9f;
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    break;

                case 0: //track player, throw penetrators
                    if (!AliveCheck(player))
                        break;
                    if (Phase2Check())
                        break;
                    npc.dontTakeDamage = false;
                    targetPos = player.Center;
                    targetPos.X += 500 * (npc.Center.X < targetPos.X ? -1 : 1);
                    if (npc.Distance(targetPos) > 50)
                    {
                        speedModifier = npc.localAI[3] > 0 ? 0.5f : 2f;
                        if (npc.Center.X < targetPos.X)
                        {
                            npc.velocity.X += speedModifier;
                            if (npc.velocity.X < 0)
                                npc.velocity.X += speedModifier * 2;
                        }
                        else
                        {
                            npc.velocity.X -= speedModifier;
                            if (npc.velocity.X > 0)
                                npc.velocity.X -= speedModifier * 2;
                        }
                        if (npc.Center.Y < targetPos.Y)
                        {
                            npc.velocity.Y += speedModifier;
                            if (npc.velocity.Y < 0)
                                npc.velocity.Y += speedModifier * 2;
                        }
                        else
                        {
                            npc.velocity.Y -= speedModifier;
                            if (npc.velocity.Y > 0)
                                npc.velocity.Y -= speedModifier * 2;
                        }
                        if (npc.localAI[3] > 0)
                        {
                            if (Math.Abs(npc.velocity.X) > 24)
                                npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                            if (Math.Abs(npc.velocity.Y) > 24)
                                npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                        }
                    }
                    if (npc.localAI[3] > 0)
                        npc.ai[1]++;
                    if (npc.ai[1] > 120)
                    {
                        npc.netUpdate = true;
                        npc.TargetClosest();
                        npc.ai[1] = 60;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.velocity = npc.DirectionTo(player.Center) * 2f;
                        }
                        else if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center, npc.DirectionTo(player.Center) * 25f, mod.ProjectileType("MutantSpearThrown"), npc.damage / 4, 0f, Main.myPlayer, npc.target);
                        }
                    }
                    else if (npc.ai[1] == 61 && npc.ai[2] < 5 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearAim"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    break;

                case 1: //slow drift, shoot phantasmal rings
                    if (Phase2Check())
                        break;
                    if (--npc.ai[1] < 0)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 90;
                        if (++npc.ai[2] > 4)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            npc.netUpdate = true;
                            npc.TargetClosest();
                        }
                        else if (Main.netMode != 1)
                        {
                            SpawnSphereRing(6, 9f, npc.damage / 5, 1f);
                            SpawnSphereRing(6, 9f, npc.damage / 5, -0.5f);
                        }
                    }
                    break;

                case 2: //fly up to corner above player and then dive
                    if (!AliveCheck(player))
                        break;
                    if (Phase2Check())
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 400;
                    Movement(targetPos, 0.6f);
                    if (npc.Distance(targetPos) < 50 || ++npc.ai[1] > 180) //dive here
                    {
                        npc.velocity.X = 35f * (npc.position.X < player.position.X ? 1 : -1);
                        if (npc.velocity.Y < 0)
                            npc.velocity.Y *= -1;
                        npc.velocity.Y *= 0.3f;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    }
                    break;

                case 3: //diving, spawning true eyes
                    npc.velocity *= 0.99f;
                    if (--npc.ai[1] < 0)
                    {
                        npc.ai[1] = 20;
                        if (++npc.ai[2] > 3)
                        {
                            npc.ai[0]++;
                            npc.ai[2] = 0;
                            npc.netUpdate = true;
                            npc.TargetClosest();
                        }
                        else
                        {
                            if (Main.netMode != 1)
                            {
                                int type = mod.ProjectileType("MutantTrueEyeL");
                                if (npc.ai[2] == 2)
                                    type = mod.ProjectileType("MutantTrueEyeR");
                                else if (npc.ai[2] == 3)
                                    type = mod.ProjectileType("MutantTrueEyeS");
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, type, npc.damage / 5, 0f, Main.myPlayer, npc.target);
                            }
                            Main.PlaySound(SoundID.Item92, npc.Center);
                            for (int i = 0; i < 30; i++)
                            {
                                int d = Dust.NewDust(npc.position, npc.width, npc.height, 135, 0f, 0f, 0, default(Color), 3f);
                                Main.dust[d].noGravity = true;
                                Main.dust[d].noLight = true;
                                Main.dust[d].velocity *= 12f;
                            }
                        }
                    }
                    break;

                case 4: //maneuvering under player while spinning penetrator
                    if (Phase2Check())
                        break;
                    if (npc.ai[3] == 0)
                    {
                        if (!AliveCheck(player))
                            break;
                        npc.ai[3] = 1;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearSpin"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    targetPos = player.Center;
                    targetPos.Y += 400f;
                    Movement(targetPos, 0.7f, false);
                    if (++npc.ai[1] > 240)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                    }
                    break;

                case 5: //pause and then initiate dash
                    if (Phase2Check())
                        break;
                    npc.velocity *= 0.9f;
                    if (++npc.ai[1] > 10)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++; //go to next attack after dashes
                            npc.ai[2] = 0;
                        }
                        else
                        {
                            npc.velocity = npc.DirectionTo(player.Center + player.velocity) * 30f;
                            if (Main.netMode != 1)
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearDash"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                        }
                    }
                    break;

                case 6: //while dashing
                    if (++npc.ai[1] > 30)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]--;
                        npc.ai[1] = 0;
                    }
                    break;

                case 7: //approach for lasers
                    if (!AliveCheck(player))
                        break;
                    if (Phase2Check())
                        break;
                    targetPos = player.Center + player.DirectionTo(npc.Center) * 250;
                    if (npc.Distance(targetPos) > 50 && ++npc.ai[2] < 180)
                    {
                        Movement(targetPos, 0.5f);
                    }
                    else
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = player.DirectionTo(npc.Center).ToRotation();
                        npc.ai[3] = (float)Math.PI / 10f;
                        if (player.Center.X < npc.Center.X)
                            npc.ai[3] *= -1;
                    }
                    break;
                
                case 8: //fire lasers in ring
                    if (Phase2Check())
                        break;
                    npc.velocity = Vector2.Zero;
                    if (--npc.ai[1] < 0)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, new Vector2(2, 0).RotatedBy(npc.ai[2]), mod.ProjectileType("MutantMark1"), npc.damage / 4, 0f, Main.myPlayer);
                        npc.ai[1] = 5;
                        npc.ai[2] += npc.ai[3];
                        if (npc.localAI[0]++ == 20)
                        {
                            npc.netUpdate = true;
                            npc.ai[2] -= npc.ai[3] / 2;
                        }
                        else if (npc.localAI[0] == 40)
                        {
                            npc.netUpdate = true;
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            npc.localAI[0] = 0;
                        }
                    }
                    break;

                case 9: //boundary lite
                    if (npc.ai[3] == 0)
                    {
                        if (AliveCheck(player))
                            npc.ai[3] = 1;
                        else
                            break;
                    }
                    if (Phase2Check())
                        break;
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 2)
                    {
                        Main.PlaySound(SoundID.Item12, npc.Center);
                        npc.ai[1] = 0;
                        npc.ai[2] += (float)Math.PI / 77f;
                        if (Main.netMode != 1)
                            for (int i = 0; i < 4; i++)
                                Projectile.NewProjectile(npc.Center, new Vector2(6f, 0).RotatedBy(npc.ai[2] + Math.PI / 2 * i), mod.ProjectileType("MutantEye"), npc.damage / 4, 0f, Main.myPlayer);
                    }
                    if (++npc.ai[3] > 241)
                    {
                        npc.TargetClosest();
                        npc.ai[0] = 0;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 10: //phase 2 begins
                    npc.velocity *= 0.9f;
                    npc.dontTakeDamage = true;
                    if (npc.buffType[0] != 0)
                        npc.DelBuff(0);
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    if (++npc.ai[1] > 120)
                    {
                        npc.localAI[3] = 2;
                        if (++npc.ai[2] > 15)
                        {
                            int heal = (int)(npc.lifeMax / 2 / 60 * Main.rand.NextFloat(1.5f, 2f));
                            npc.life += heal;
                            if (npc.life > npc.lifeMax)
                                npc.life = npc.lifeMax;
                            CombatText.NewText(npc.Hitbox, CombatText.HealLife, heal);
                        }
                        if (npc.ai[1] > 210)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            npc.netUpdate = true;
                        }
                    }
                    else if (npc.ai[1] == 120)
                    {
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual"), npc.damage, 0f, Main.myPlayer, 0f, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual5"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                        }
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        for (int i = 0; i < 50; i++)
                        {
                            int d = Dust.NewDust(Main.player[Main.myPlayer].position, Main.player[Main.myPlayer].width, Main.player[Main.myPlayer].height, 229, 0f, 0f, 0, default(Color), 2.5f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].noLight = true;
                            Main.dust[d].velocity *= 9f;
                        }
                        if (player.GetModPlayer<FargoPlayer>().TerrariaSoul)
                            EdgyBossText("Hand it over. That thing, your soul toggles.");
                    }
                    break;

                case 11: //approach for laser
                    npc.dontTakeDamage = false;
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(npc.Center) * 250;
                    if (npc.Distance(targetPos) > 50 && ++npc.ai[2] < 180)
                    {
                        Movement(targetPos, 0.8f);
                    }
                    else
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = player.DirectionTo(npc.Center).ToRotation();
                        npc.ai[3] = (float)Math.PI / 10f;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        if (player.Center.X < npc.Center.X)
                            npc.ai[3] *= -1;
                    }
                    break;

                case 12: //fire lasers in ring
                    npc.velocity = Vector2.Zero;
                    if (--npc.ai[1] < 0)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, new Vector2(2, 0).RotatedBy(npc.ai[2]), mod.ProjectileType("MutantMark1"), npc.damage / 4, 0f, Main.myPlayer);
                        npc.ai[1] = 3;
                        npc.ai[2] += npc.ai[3];
                        if (npc.localAI[0]++ == 20 || npc.localAI[0] == 40)
                        {
                            npc.netUpdate = true;
                            npc.ai[2] -= npc.ai[3] / 2;
                        }
                        else if (npc.localAI[0] == 60)
                        {
                            npc.TargetClosest();
                            npc.netUpdate = true;
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            npc.localAI[0] = 0;
                        }
                    }
                    break;

                case 13: //maneuvering under player while spinning penetrator
                    if (npc.ai[3] == 0)
                    {
                        if (!AliveCheck(player))
                            break;
                        npc.ai[3] = 1;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearSpin"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    targetPos = player.Center;
                    targetPos.Y += 400f;
                    Movement(targetPos, 0.7f, false);
                    if (++npc.ai[1] > 180)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.TargetClosest();
                    }
                    break;

                case 14: //pause and then initiate dash
                    npc.velocity *= 0.9f;
                    if (++npc.ai[1] > 30)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++; //go to next attack after dashes
                            npc.ai[2] = 0;
                        }
                        else
                        {
                            npc.velocity = npc.DirectionTo(player.Center + player.velocity * 30f) * 45f;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.velocity), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, -Vector2.Normalize(npc.velocity), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearDash"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 1f);
                            }
                        }
                    }
                    break;

                case 15: //while dashing
                    if (++npc.ai[1] > 30)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]--;
                        npc.ai[1] = 0;
                    }
                    break;

                case 16: //approach for bullet hell
                    goto case 11;

                case 17: //BOUNDARY OF WAVE AND PARTICLE
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 2)
                    {
                        Main.PlaySound(SoundID.Item12, npc.Center);
                        npc.ai[1] = 0;
                        npc.ai[2] += (float)Math.PI / 8 / 480 * npc.ai[3];
                        if (npc.ai[2] > (float)Math.PI)
                            npc.ai[2] -= (float)Math.PI * 2;
                        if (Main.netMode != 1)
                            for (int i = 0; i < 6; i++)
                                Projectile.NewProjectile(npc.Center, new Vector2(6f, 0).RotatedBy(npc.ai[2] + Math.PI / 3 * i), mod.ProjectileType("MutantEye"), npc.defDamage / 3, 0f, Main.myPlayer);
                    }
                    if (++npc.ai[3] > 360)
                    {
                        npc.TargetClosest();
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 18: //spawn illusions for next attack
                    Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    if (Main.netMode != 1)
                    {
                        int n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MutantIllusion"), npc.whoAmI, npc.whoAmI, -1, 1, 60);
                        if (n != 200 && Main.netMode == 2)
                            NetMessage.SendData(23, -1, -1, null, n);
                        n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MutantIllusion"), npc.whoAmI, npc.whoAmI, 1, -1, 120);
                        if (n != 200 && Main.netMode == 2)
                            NetMessage.SendData(23, -1, -1, null, n);
                        n = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("MutantIllusion"), npc.whoAmI, npc.whoAmI, 1, 1, 180);
                        if (n != 200 && Main.netMode == 2)
                            NetMessage.SendData(23, -1, -1, null, n);
                    }
                    npc.ai[0]++;
                    break;

                case 19: //QUADRUPLE PILLAR ROAD ROLLER
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y += 200;
                    if (npc.Distance(targetPos) > 50)
                    {
                        Movement(targetPos, 0.6f);
                    }
                    if (++npc.ai[1] > 360)
                    {
                        npc.TargetClosest();
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    else if (npc.ai[1] == 240)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * -10, mod.ProjectileType("MutantPillar"), npc.damage / 3, 0, Main.myPlayer, 3, npc.whoAmI);
                    }
                    break;

                case 20: //blood sickle mines
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(npc.Center) * 400;
                    if (npc.Distance(targetPos) > 50)
                    {
                        Movement(targetPos, 0.5f);
                    }
                    if (++npc.ai[1] > 60)
                    {
                        npc.ai[1] = 0;
                        if (++npc.ai[2] > 3)
                        {
                            npc.ai[0]++;
                            npc.ai[2] = 0;
                            npc.TargetClosest();
                        }
                        else
                        {
                            if (Main.netMode != 1)
                                for (int i = 0; i < 8; i++)
                                    Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(Math.PI / 4 * i) * 10f, mod.ProjectileType("MutantScythe1"), npc.damage / 5, 0f, Main.myPlayer, npc.whoAmI);
                            Main.PlaySound(36, (int)npc.Center.X, (int)npc.Center.Y, -1, 1f, 0f);
                        }
                        npc.netUpdate = true;
                        break;
                    }
                    break;

                case 21: //maneuver above while spinning penetrator
                    if (npc.ai[3] == 0)
                    {
                        if (!AliveCheck(player))
                            break;
                        npc.ai[3] = 1;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearSpin"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    targetPos = player.Center;
                    targetPos.Y -= 400f;
                    Movement(targetPos, 0.7f, false);
                    if (++npc.ai[1] > 180)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.TargetClosest();
                    }
                    break;

                case 22: //pause and then initiate dash
                    npc.velocity *= 0.9f;
                    if (++npc.ai[1] > 10)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++; //go to next attack after dashes
                            npc.ai[2] = 0;
                        }
                        else
                        {
                            npc.velocity = npc.DirectionTo(player.Center) * 45f;
                            if (Main.netMode != 1)
                            {
                                Projectile.NewProjectile(npc.Center, Vector2.Normalize(npc.velocity), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, -Vector2.Normalize(npc.velocity), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                                Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearDash"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                            }
                        }
                    }
                    break;

                case 23: //while dashing
                    goto case 15;

                case 24: //destroyers
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + npc.DirectionFrom(player.Center) * 300;
                    if (npc.Distance(targetPos) > 50)
                    {
                        Movement(targetPos, 0.9f);
                    }
                    if (++npc.ai[1] > 60)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 30;
                        if (++npc.ai[2] > 5)
                        {
                            npc.TargetClosest();
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                        }
                        else
                        {
                            Main.PlaySound(4, (int)npc.Center.X, (int)npc.Center.Y, 13);
                            if (Main.netMode != 1) //spawn worm
                            {
                                Vector2 vel = Vector2.Normalize(npc.velocity) * 10f;
                                int current = Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantDestroyerHead"), npc.damage / 4, 0f, Main.myPlayer, npc.target);
                                for (int i = 0; i < 18; i++)
                                    current = Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantDestroyerBody"), npc.damage / 4, 0f, Main.myPlayer, current);
                                int previous = current;
                                current = Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantDestroyerTail"), npc.damage / 4, 0f, Main.myPlayer, current);
                                Main.projectile[previous].localAI[1] = current;
                                Main.projectile[previous].netUpdate = true;
                            }
                        }
                    }
                    break;

                case 25: //improved throw
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 500 * (npc.Center.X < targetPos.X ? -1 : 1);
                    if (npc.Distance(targetPos) > 50)
                    {
                        Movement(targetPos, 0.4f);
                    }
                    if (++npc.ai[1] > 60)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 0;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.TargetClosest();
                        }
                        else if (Main.netMode != 1)
                        {
                            Vector2 vel = npc.DirectionTo(player.Center + player.velocity * 30f) * 30f;
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(vel), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, -Vector2.Normalize(vel), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantSpearThrown"), npc.damage / 4, 0f, Main.myPlayer, npc.target, 1f);
                        }
                    }
                    else if (npc.ai[1] == 1 && npc.ai[2] < 5 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearAim"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    break;

                case 26: //back away, prepare for ultra laser spam
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 250;
                    if (npc.Distance(targetPos) > 50)
                    {
                        Movement(targetPos, 0.5f);
                    }
                    if (++npc.ai[1] > 120)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        npc.TargetClosest();
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        int d = Dust.NewDust(npc.Center, 0, 0, DustID.Fire, 0f, 0f, 0, default(Color), 3f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                    break;

                case 27: //DEATHRAY SPAM
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 15)
                    {
                        npc.ai[1] = 0;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.UnitX, mod.ProjectileType("MutantDeathray3"), npc.damage / 4, 0, Main.myPlayer, MathHelper.ToRadians(260) / -90f, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center, -Vector2.UnitX, mod.ProjectileType("MutantDeathray3"), npc.damage / 4, 0, Main.myPlayer, MathHelper.ToRadians(260) / 90f, npc.whoAmI);
                        }
                    }
                    if (++npc.ai[3] > 180)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        npc.TargetClosest();
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        int d = Dust.NewDust(npc.Center, 0, 0, DustID.Fire, 0f, 0f, 0, default(Color), 3f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                    break;

                case 28: //rain primes
                    if (++npc.ai[1] > 4 && npc.ai[3] > 30)
                    {
                        npc.ai[1] = 0;
                        Main.PlaySound(SoundID.Item21, npc.Center);
                        if (Main.netMode != 1)
                        {
                            Vector2 spawnPos = npc.Center;
                            spawnPos.X += Main.rand.Next(-600, 601);
                            spawnPos.Y -= 1200;
                            Vector2 vel = npc.Center;
                            vel.Y += 600;
                            vel -= spawnPos;
                            vel.Normalize();
                            vel *= 18;
                            Projectile.NewProjectile(spawnPos, vel, mod.ProjectileType("MutantGuardian"), npc.damage / 3, 0f, Main.myPlayer);
                        }
                    }
                    if (++npc.ai[3] > 120) //also backdash for next attack
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 29: //prepare to fishron dive
                    if (!AliveCheck(player))
                        break;
                    npc.ai[0]++;
                    npc.velocity.X = 35f * (npc.position.X < player.position.X ? 1 : -1);
                    npc.velocity.Y = -10f;
                    break;

                case 30: //spawn fishrons
                    npc.velocity *= 0.99f;
                    if (--npc.ai[1] < 0)
                    {
                        npc.ai[1] = 20;
                        if (++npc.ai[2] > 3)
                        {
                            npc.ai[0]++;
                            npc.ai[2] = 0;
                            npc.netUpdate = true;
                            npc.TargetClosest();
                        }
                        else
                        {
                            if (Main.netMode != 1)
                                Projectile.NewProjectile(npc.Center, Vector2.UnitY * -3f, mod.ProjectileType("MutantFishron"), npc.damage / 4, 0f, Main.myPlayer, npc.target);
                            for (int i = 0; i < 30; i++)
                            {
                                int d = Dust.NewDust(npc.position, npc.width, npc.height, 135, 0f, 0f, 0, default(Color), 3f);
                                Main.dust[d].noGravity = true;
                                Main.dust[d].noLight = true;
                                Main.dust[d].velocity *= 12f;
                            }
                        }
                    }
                    break;

                case 31: //maneuver nearby for dive
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y += 400;
                    Movement(targetPos, 1f);
                    if (++npc.ai[1] > 90) //dive here
                    {
                        npc.velocity.X = 30f * (npc.position.X < player.position.X ? 1 : -1);
                        if (npc.velocity.Y > 0)
                            npc.velocity.Y *= -1;
                        npc.velocity.Y *= 0.3f;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 32: //spawn eyes
                    goto case 3;

                case 33: //toss nuke, set vel
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.Y -= 400;
                    Movement(targetPos, 0.6f, false);
                    if (++npc.ai[1] > 180)
                    {
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                        if (Main.netMode != 1)
                        {
                            float gravity = 0.2f;
                            const float time = 180f;
                            Vector2 distance = player.Center - npc.Center;
                            distance.X = distance.X / time;
                            distance.Y = distance.Y / time - 0.5f * gravity * time;
                            Projectile.NewProjectile(npc.Center, distance, mod.ProjectileType("MutantNuke"), npc.damage / 3, 0f, Main.myPlayer, gravity);
                        }
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                        npc.TargetClosest();
                    }
                    break;

                case 34: //slow drift, protective aura above self
                    if (!AliveCheck(player))
                        break;
                    npc.velocity.Normalize();
                    npc.velocity *= 2f;
                    if (npc.ai[1] > 180 && Main.netMode != 1)
                    {
                        Vector2 safeZone = npc.Center;
                        safeZone.Y -= 100;
                        const float safeRange = 150 + 354;
                        for (int i = 0; i < 3; i++)
                        {
                            Vector2 spawnPos = npc.Center + Main.rand.NextVector2Circular(1200, 1200);
                            if (Vector2.Distance(safeZone, spawnPos) < safeRange)
                            {
                                Vector2 directionOut = spawnPos - safeZone;
                                directionOut.Normalize();
                                spawnPos = safeZone + directionOut * safeRange;
                            }
                            Projectile.NewProjectile(spawnPos, Vector2.Zero, mod.ProjectileType("MutantBomb"), npc.damage / 4, 0f, Main.myPlayer);
                        }
                    }
                    if (++npc.ai[1] > 360)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.netUpdate = true;
                        npc.TargetClosest();
                    }
                    for (int i = 0; i < 20; i++)
                    {
                        Vector2 offset = new Vector2();
                        offset.Y -= 100;
                        double angle = Main.rand.NextDouble() * 2d * Math.PI;
                        offset.X += (float)(Math.Sin(angle) * 150);
                        offset.Y += (float)(Math.Cos(angle) * 150);
                        Dust dust = Main.dust[Dust.NewDust(npc.Center + offset - new Vector2(4, 4), 0, 0, 229, 0, 0, 100, Color.White, 1.5f)];
                        dust.velocity = npc.velocity;
                        if (Main.rand.Next(3) == 0)
                            dust.velocity += Vector2.Normalize(offset) * 5f;
                        dust.noGravity = true;
                    }
                    break;

                case 35: //flee while slime raining
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 700 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y += 200;
                    if (npc.Distance(targetPos) > 50)
                        Movement(targetPos, 0.7f);
                    if (++npc.ai[1] > 6)
                    {
                        npc.ai[1] = 0;
                        Main.PlaySound(SoundID.Item34, player.Center);
                        if (Main.netMode != 1)
                        {
                            Vector2 spawnPos = npc.Center;
                            spawnPos.X += (npc.Center.X < player.Center.X) ? 900 : -900;
                            spawnPos.Y -= 1200;
                            for (int i = 0; i < 15; i++)
                                Projectile.NewProjectile(spawnPos.X + Main.rand.Next(-300, 301), spawnPos.Y + Main.rand.Next(-100, 101), Main.rand.NextFloat(-5f, 5f), Main.rand.NextFloat(30f, 35f), mod.ProjectileType("MutantSlimeBall"), npc.damage / 5, 0f, Main.myPlayer);
                        }
                    }
                    if (npc.ai[3] == 0)
                    {
                        npc.ai[3] = 1;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSlimeRain"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    if (++npc.ai[2] > 180)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        npc.TargetClosest();
                    }
                    break;

                case 36: //go below to initiate dash
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 400 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 400;
                    Movement(targetPos, 0.9f);
                    if (++npc.ai[1] > 60) //dive here
                    {
                        npc.velocity.X = 35f * (npc.position.X < player.position.X ? 1 : -1);
                        npc.velocity.Y = -10f;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                        npc.TargetClosest();
                    }
                    break;

                case 37: //spawn fishrons
                    goto case 30;

                case 38: //rain slime again
                    goto case 35;

                case 39: //SPHERE RING SPAMMMMM
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 10 && npc.ai[3] > 60)
                    {
                        npc.ai[1] = 0;
                        SpawnSphereRing(12, 12f, npc.damage / 4, -1f);
                        SpawnSphereRing(12, 12f, npc.damage / 4, 1f);
                    }
                    if (npc.ai[2] == 0)
                    {
                        npc.ai[2] = 1;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    }
                    if (++npc.ai[3] > 300)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.TargetClosest();
                    }
                    for (int i = 0; i < 5; i++)
                    {
                        int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 4f;
                    }
                    break;

                case 40: //throw penetrator again
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 500 * (npc.Center.X < targetPos.X ? -1 : 1);
                    if (npc.Distance(targetPos) > 50)
                        Movement(targetPos, 0.4f);
                    if (++npc.ai[1] > 210)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 180;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.TargetClosest();
                        }
                        else if (Main.netMode != 1)
                        {
                            Vector2 vel = npc.DirectionTo(player.Center) * 30f;
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(vel), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, -Vector2.Normalize(vel), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantSpearThrown"), npc.damage / 4, 0f, Main.myPlayer, npc.target);
                        }
                    }
                    else if (npc.ai[1] == 181 && npc.ai[2] < 5 && Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearAim"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 1);
                    }
                    break;

                case 41: //spawn leaf crystals
                    Main.PlaySound(SoundID.Item84, npc.Center);
                    if (Main.netMode != 1)
                    {
                        Projectile.NewProjectile(npc.Center, Vector2.UnitY * 10f, mod.ProjectileType("MutantMark2"), npc.damage / 4, 0f, Main.myPlayer, 30, 30 + 120);
                        Projectile.NewProjectile(npc.Center, Vector2.UnitY * -10f, mod.ProjectileType("MutantMark2"), npc.damage / 4, 0f, Main.myPlayer, 30, 30 + 240);
                    }
                    npc.ai[0]++;
                    break;

                case 42: //boomerangs
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 30)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 0;
                        Main.PlaySound(SoundID.Item92, npc.Center);
                        npc.ai[2] = npc.ai[2] > 0 ? -1 : 1;
                        if (Main.netMode != 1)
                        {
                            const float retiRad = 500;
                            const float spazRad = 250;
                            float retiSpeed = 2 * (float)Math.PI * retiRad / 240;
                            float spazSpeed = 2 * (float)Math.PI * spazRad / 120;
                            float retiAcc = retiSpeed * retiSpeed / retiRad * npc.ai[2];
                            float spazAcc = spazSpeed * spazSpeed / spazRad * -npc.ai[2];
                            for (int i = 0; i < 4; i++)
                            {
                                Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(Math.PI / 2 * i) * retiSpeed, mod.ProjectileType("MutantRetirang"), npc.damage / 4, 0f, Main.myPlayer, retiAcc, 240);
                                Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(Math.PI / 2 * i + Math.PI / 4) * spazSpeed, mod.ProjectileType("MutantSpazmarang"), npc.damage / 4, 0f, Main.myPlayer, spazAcc, 120);
                            }
                        }
                    }
                    if (++npc.ai[3] > 300)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.TargetClosest();
                    }
                    break;

                /*case 43: //spawn sword
                    npc.velocity = Vector2.Zero;
                    if (npc.ai[2] == 0 && Main.netMode != 1)
                    {
                        double angle = npc.position.X < player.position.X ? -Math.PI / 4 : Math.PI / 4;
                        npc.ai[2] = (float)angle * -4f / 30;
                        const int spacing = 80;
                        Vector2 offset = Vector2.UnitY.RotatedBy(angle) * -spacing;
                        for (int i = 0; i < 12; i++)
                            Projectile.NewProjectile(npc.Center + offset * i, Vector2.Zero, mod.ProjectileType("MutantSword"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, spacing * i);
                        Projectile.NewProjectile(npc.Center + offset.RotatedBy(MathHelper.ToRadians(20)) * 7, Vector2.Zero, mod.ProjectileType("MutantSword"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 60 * 4);
                        Projectile.NewProjectile(npc.Center + offset.RotatedBy(MathHelper.ToRadians(-20)) * 7, Vector2.Zero, mod.ProjectileType("MutantSword"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 60 * 4);
                        Projectile.NewProjectile(npc.Center + offset.RotatedBy(MathHelper.ToRadians(40)) * 28, Vector2.Zero, mod.ProjectileType("MutantSword"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 60 * 4);
                        Projectile.NewProjectile(npc.Center + offset.RotatedBy(MathHelper.ToRadians(-40)) * 28, Vector2.Zero, mod.ProjectileType("MutantSword"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI, 60 * 4);
                    }
                    if (++npc.ai[1] > 120)
                    {
                        targetPos = player.Center;
                        targetPos.X += 300 * (npc.Center.X < targetPos.X ? -1 : 1);
                        npc.velocity = (targetPos - npc.Center) / 30;

                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                case 44: //swinging sword dash
                    npc.ai[3] += npc.ai[2];
                    if (++npc.ai[1] > 35)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    break;*/

                default:
                    npc.ai[0] = 11;
                    goto case 11;
            }
        }

        private void SpawnSphereRing(int max, float speed, int damage, float rotationModifier)
        {
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = Vector2.UnitY * speed;
            int type = mod.ProjectileType("MutantSphereRing");
            for (int i = 0; i < max; i++)
            {
                vel = vel.RotatedBy(rotation);
                Projectile.NewProjectile(npc.Center, vel, type, damage, 0f, Main.myPlayer, rotationModifier * npc.spriteDirection, speed);
            }
            Main.PlaySound(SoundID.Item84, npc.Center);
        }

        private void Aura(float distance, int buff, bool reverse = false, int dustid = DustID.GoldFlame, bool checkDuration = false)
        {
            //works because buffs are client side anyway :ech:
            Player p = Main.player[Main.myPlayer];
            float range = npc.Distance(p.Center);
            if (reverse ? range > distance && range < 5000f : range < distance)
                p.AddBuff(buff, checkDuration && Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

            for (int i = 0; i < 30; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * distance);
                offset.Y += (float)(Math.Cos(angle) * distance);
                Dust dust = Main.dust[Dust.NewDust(
                    npc.Center + offset - new Vector2(4, 4), 0, 0,
                    dustid, 0, 0, 100, Color.White, 1.5f)];
                dust.velocity = npc.velocity;
                if (Main.rand.Next(3) == 0)
                    dust.velocity += Vector2.Normalize(offset) * (reverse ? 5f : -5f);
                dust.noGravity = true;
            }
        }

        private bool AliveCheck(Player player)
        {
            if ((!player.active || player.dead || Vector2.Distance(npc.Center, player.Center) > 5000f) && npc.localAI[3] > 0)
            {
                npc.TargetClosest();
                if (!player.active || player.dead || Vector2.Distance(npc.Center, player.Center) > 5000f)
                {
                    if (npc.timeLeft > 60)
                        npc.timeLeft = 60;
                    npc.velocity.Y -= 1f;
                    return false;
                }
            }
            if (npc.timeLeft < 600)
                npc.timeLeft = 600;
            return true;
        }

        private bool Phase2Check()
        {
            if (npc.life < npc.lifeMax / 2)
            {
                if (Main.netMode != 1)
                {
                    npc.ai[0] = 10;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 0;
                    npc.netUpdate = true;
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual2"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual3"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual4"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    for (int i = 0; i < 1000; i++)
                        if (Main.projectile[i].active && Main.projectile[i].hostile)
                            Main.projectile[i].Kill();
                    for (int i = 0; i < 1000; i++)
                        if (Main.projectile[i].active && Main.projectile[i].hostile)
                            Main.projectile[i].Kill();
                    EdgyBossText("Time to stop playing around.");
                }
                return true;
            }
            return false;
        }

        private void Movement(Vector2 targetPos, float speedModifier, bool fastX = true)
        {
            if (npc.Center.X < targetPos.X)
            {
                npc.velocity.X += speedModifier;
                if (npc.velocity.X < 0)
                    npc.velocity.X += speedModifier * (fastX ? 2 : 1);
            }
            else
            {
                npc.velocity.X -= speedModifier;
                if (npc.velocity.X > 0)
                    npc.velocity.X -= speedModifier * (fastX ? 2 : 1);
            }
            if (npc.Center.Y < targetPos.Y)
            {
                npc.velocity.Y += speedModifier;
                if (npc.velocity.Y < 0)
                    npc.velocity.Y += speedModifier * 2;
            }
            else
            {
                npc.velocity.Y -= speedModifier;
                if (npc.velocity.Y > 0)
                    npc.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(npc.velocity.X) > 24)
                npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
            if (Math.Abs(npc.velocity.Y) > 24)
                npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
        }

        private void EdgyBossText(string text)
        {
            if (Fargowiltas.Instance.CalamityLoaded) //edgy boss text
            {
                if (Main.netMode == 0)
                    Main.NewText(text, Color.LimeGreen);
                else if (Main.netMode == 2)
                    NetMessage.BroadcastChatMessage(NetworkText.FromLiteral(text), Color.LimeGreen);
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.GetModPlayer<FargoPlayer>().MaxLifeReduction += 100;
            target.AddBuff(mod.BuffType("OceanicMaul"), 5400);
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 600);
            target.AddBuff(mod.BuffType("MutantFang"), 300);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 3; i++)
            {
                int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 3f;
            }
        }

        public override bool CheckDead()
        {
            npc.life = 1;
            npc.active = true;
            npc.localAI[3] = 2;
            if (Main.netMode != 1 && npc.ai[0] > -1)
            {
                if (npc.ai[0] < 11 && Main.netMode != 1)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual"), npc.damage, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual5"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                }
                npc.ai[0] = -1;
                npc.ai[1] = 0;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
                for (int i = 0; i < 1000; i++)
                    if (Main.projectile[i].active && Main.projectile[i].hostile)
                        Main.projectile[i].Kill();
                for (int i = 0; i < 1000; i++)
                    if (Main.projectile[i].active && Main.projectile[i].hostile)
                        Main.projectile[i].Kill();
                EdgyBossText("You're pretty good...");
            }
            return false;
        }

        public override void NPCLoot()
        {
            FargoSoulsWorld.downedMutant = true;
            if (Main.netMode == 2)
                NetMessage.SendData(7); //sync world
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("MutantBag"));
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.SuperHealingPotion;
        }

        public override void FindFrame(int frameHeight)
        {
            if (++npc.frameCounter > 6)
            {
                npc.frameCounter = 0;
                npc.frame.Y += frameHeight;
                if (npc.frame.Y >= 4 * frameHeight)
                    npc.frame.Y = 0;
            }
        }
    }
}