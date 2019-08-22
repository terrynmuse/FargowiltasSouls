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
            npc.damage = 250;
            npc.defense = 200;
            npc.lifeMax = 7000000;
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
            npc.GetGlobalNPC<FargoSoulsGlobalNPC>().SpecialEnchantImmune = true;
            //music = MusicID.TheTowers;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/rePrologue");
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 250;
            npc.lifeMax = (int)(7000000 * bossLifeScale);
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
                    }
                }
            }
            if (npc.localAI[3] == 1)
                Aura(2000f, mod.BuffType("GodEater"), true, 86);
            else if (Main.player[Main.myPlayer].active && npc.Distance(Main.player[Main.myPlayer].Center) < 3000f)
                Main.player[Main.myPlayer].AddBuff(mod.BuffType("MutantPresence"), 2);

            Player player = Main.player[npc.target];
            npc.direction = npc.spriteDirection = npc.position.X < player.position.X ? 1 : -1;
            Vector2 targetPos;
            float speedModifier;
            switch ((int)npc.ai[0])
            {
                case -2: //fade out, drop a mutant
                    npc.dontTakeDamage = true;
                    npc.damage = 0;
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
                    }
                    break;

                case -1: //defeated
                    npc.dontTakeDamage = true;
                    npc.velocity *= 0.9f;
                    if (npc.buffType[0] != 0)
                        npc.DelBuff(0);
                    if (++npc.ai[1] > 120 && npc.velocity.Length() < 0.5f)
                    {
                        npc.velocity = Vector2.Zero;
                        npc.ai[0]--;
                        npc.ai[3] = (float)-Math.PI / 2;
                        npc.netUpdate = true;
                        if (Main.netMode != 1) //shoot harmless mega rays
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * -1, mod.ProjectileType("MutantGiantDeathray"), 0, 0f, Main.myPlayer, 0, npc.whoAmI);
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
                    break;

                case 1: //slow drift, shoot phantasmal rings
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
                    speedModifier = 0.6f;
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
                    if (Math.Abs(npc.velocity.X) > 24)
                        npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                    if (Math.Abs(npc.velocity.Y) > 24)
                        npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                    if (npc.Distance(targetPos) < 50 || ++npc.ai[1] > 180)
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
                    if (npc.ai[3] == 0)
                    {
                        if (!AliveCheck(player))
                            break;
                        if (Phase2Check())
                            break;
                        npc.ai[3] = 1;
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantSpearSpin"), npc.damage / 4, 0f, Main.myPlayer, npc.whoAmI);
                    }
                    targetPos = player.Center;
                    targetPos.Y += 400f;
                    speedModifier = 0.7f;
                    if (npc.Center.X < targetPos.X)
                    {
                        npc.velocity.X += speedModifier;
                        if (npc.velocity.X < 0)
                            npc.velocity.X += speedModifier;
                    }
                    else
                    {
                        npc.velocity.X -= speedModifier;
                        if (npc.velocity.X > 0)
                            npc.velocity.X -= speedModifier;
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
                    if (++npc.ai[1] > 240)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                    }
                    break;

                case 5: //pause and then initiate dash
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
                        speedModifier = 0.5f;
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
                        if (Math.Abs(npc.velocity.X) > 24)
                            npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                        if (Math.Abs(npc.velocity.Y) > 24)
                            npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
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
                        if (AliveCheck(player) && !Phase2Check())
                            npc.ai[3] = 1;
                        else
                            break;
                    }
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 2)
                    {
                        Main.PlaySound(SoundID.Item12, npc.Center);
                        npc.ai[1] = 0;
                        npc.ai[2] += (float)Math.PI / 77f;
                        if (Main.netMode != 1)
                            for (int i = 0; i < 4; i++)
                                Projectile.NewProjectile(npc.Center, new Vector2(6f, 0).RotatedBy(npc.ai[2] + Math.PI / 2 * i), mod.ProjectileType("MutantEye"), npc.damage / 5, 0f, Main.myPlayer);
                    }
                    if (++npc.ai[3] > 241)
                    {
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
                    if (++npc.ai[1] > 120)
                    {
                        npc.localAI[3] = 2;
                        for (int i = 0; i < 5; i++)
                        {
                            int d = Dust.NewDust(npc.position, npc.width, npc.height, 229, 0f, 0f, 0, default(Color), 1.5f);
                            Main.dust[d].noGravity = true;
                            Main.dust[d].noLight = true;
                            Main.dust[d].velocity *= 4f;
                        }
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
                            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual"), npc.damage / 5, 0f, Main.myPlayer, 0f, npc.whoAmI);
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
                    }
                    break;

                case 11: //approach for laser
                    npc.dontTakeDamage = false;
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(npc.Center) * 250;
                    if (npc.Distance(targetPos) > 50 && ++npc.ai[2] < 180)
                    {
                        speedModifier = 0.5f;
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
                        if (Math.Abs(npc.velocity.X) > 24)
                            npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                        if (Math.Abs(npc.velocity.Y) > 24)
                            npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
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
                    speedModifier = 0.7f;
                    if (npc.Center.X < targetPos.X)
                    {
                        npc.velocity.X += speedModifier;
                        if (npc.velocity.X < 0)
                            npc.velocity.X += speedModifier;
                    }
                    else
                    {
                        npc.velocity.X -= speedModifier;
                        if (npc.velocity.X > 0)
                            npc.velocity.X -= speedModifier;
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
                    if (++npc.ai[1] > 240)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
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
                        npc.ai[2] += (float)Math.PI / 87f + (float)Math.PI / 23f * npc.ai[3] / 360;
                        if (Main.netMode != 1)
                            for (int i = 0; i < 4; i++)
                                Projectile.NewProjectile(npc.Center, new Vector2(6f, 0).RotatedBy(npc.ai[2] + Math.PI / 2 * i), mod.ProjectileType("MutantEye"), npc.damage / 5, 0f, Main.myPlayer);
                    }
                    if (++npc.ai[3] > 360)
                    {
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
                    targetPos.Y += 300;
                    if (npc.Distance(targetPos) > 50)
                    {
                        speedModifier = 0.6f;
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
                        if (Math.Abs(npc.velocity.X) > 24)
                            npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                        if (Math.Abs(npc.velocity.Y) > 24)
                            npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                    }
                    if (++npc.ai[1] > 360)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    else if (npc.ai[1] == 240)
                    {
                        if (Main.netMode != 1)
                            Projectile.NewProjectile(npc.Center, Vector2.UnitY * -10, mod.ProjectileType("MutantPillar"), npc.damage / 4, 0, Main.myPlayer, 3, npc.whoAmI);
                    }
                    break;

                case 20: //aproach for sickles
                    npc.dontTakeDamage = false;
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + player.DirectionTo(npc.Center) * 400;
                    if (npc.Distance(targetPos) > 50)
                    {
                        speedModifier = 0.5f;
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
                        if (Math.Abs(npc.velocity.X) > 24)
                            npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                        if (Math.Abs(npc.velocity.Y) > 24)
                            npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                    }
                    if (++npc.ai[1] > 120)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        break;
                    }
                    break;

                case 21: //blood sickle mines
                    if (Main.netMode != 1)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(Math.PI / 4 * i) * 10f, mod.ProjectileType("MutantScythe1"), npc.damage / 5, 0f, Main.myPlayer, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center, Vector2.UnitX.RotatedBy(Math.PI / 4 * i), mod.ProjectileType("MutantScythe2"), npc.damage / 5, 0f, Main.myPlayer, npc.whoAmI);
                        }
                    }
                    npc.velocity = npc.DirectionFrom(player.Center) * 15f;
                    npc.ai[0]++;
                    npc.netUpdate = true;
                    Main.PlaySound(36, (int)npc.Center.X, (int)npc.Center.Y, -1, 1f, 0f);
                    break;

                case 22: //destroyers
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center + npc.DirectionFrom(player.Center) * 300;
                    if (npc.Distance(targetPos) > 50)
                    {
                        speedModifier = 0.9f;
                        if (npc.Center.X < targetPos.X)
                        {
                            npc.velocity.X += speedModifier;
                            if (npc.velocity.X < 0)
                                npc.velocity.X += speedModifier;
                        }
                        else
                        {
                            npc.velocity.X -= speedModifier;
                            if (npc.velocity.X > 0)
                                npc.velocity.X -= speedModifier;
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
                    if (++npc.ai[1] > 180)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 120;
                        if (++npc.ai[2] > 3)
                        {
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
                                for (int i = 0; i < 9; i++)
                                    current = Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantDestroyerBody"), npc.damage / 4, 0f, Main.myPlayer, current);
                                int previous = current;
                                current = Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantDestroyerTail"), npc.damage / 4, 0f, Main.myPlayer, current);
                                Main.projectile[previous].localAI[1] = current;
                                Main.projectile[previous].netUpdate = true;
                            }
                        }
                    }
                    break;

                case 23: //improved throw
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 500 * (npc.Center.X < targetPos.X ? -1 : 1);
                    if (npc.Distance(targetPos) > 50)
                    {
                        speedModifier = 0.4f;
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
                        if (Math.Abs(npc.velocity.X) > 24)
                            npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                        if (Math.Abs(npc.velocity.Y) > 24)
                            npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                    }
                    if (++npc.ai[1] > 120)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 60;
                        if (++npc.ai[2] > 5)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                        }
                        else if (Main.netMode != 1)
                        {
                            Vector2 vel = npc.DirectionTo(player.Center + player.velocity * 30f) * 30f;
                            Projectile.NewProjectile(npc.Center, Vector2.Normalize(vel), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, -Vector2.Normalize(vel), mod.ProjectileType("MutantDeathray2"), npc.damage / 5, 0f, Main.myPlayer);
                            Projectile.NewProjectile(npc.Center, vel, mod.ProjectileType("MutantSpearThrown"), npc.damage / 4, 0f, Main.myPlayer, npc.target, 1f);
                        }
                    }
                    break;

                case 24: //back away, prepare for ultra laser spam
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 600 * (npc.Center.X < targetPos.X ? -1 : 1);
                    targetPos.Y -= 250;
                    if (npc.Distance(targetPos) > 50)
                    {
                        speedModifier = 0.5f;
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
                        if (Math.Abs(npc.velocity.X) > 24)
                            npc.velocity.X = 24 * Math.Sign(npc.velocity.X);
                        if (Math.Abs(npc.velocity.Y) > 24)
                            npc.velocity.Y = 24 * Math.Sign(npc.velocity.Y);
                    }
                    if (++npc.ai[1] > 120)
                    {
                        npc.netUpdate = true;
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[2] = 0;
                        npc.ai[3] = 0;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        int d = Dust.NewDust(npc.Center, 0, 0, DustID.Fire, 0f, 0f, 0, default(Color), 3f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                    break;

                case 25: //DEATHRAY SPAM
                    npc.velocity = Vector2.Zero;
                    if (++npc.ai[1] > 20)
                    {
                        npc.ai[1] = 0;
                        if (Main.netMode != 1)
                        {
                            Projectile.NewProjectile(npc.Center, Vector2.UnitX, mod.ProjectileType("MutantDeathray3"), npc.damage / 4, 0, Main.myPlayer, MathHelper.ToRadians(260) / -90f, npc.whoAmI);
                            Projectile.NewProjectile(npc.Center, -Vector2.UnitX, mod.ProjectileType("MutantDeathray3"), npc.damage / 4, 0, Main.myPlayer, MathHelper.ToRadians(260) / 90f, npc.whoAmI);
                        }
                    }
                    if (++npc.ai[3] > 300)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        int d = Dust.NewDust(npc.Center, 0, 0, DustID.Fire, 0f, 0f, 0, default(Color), 3f);
                        Main.dust[d].noGravity = true;
                        Main.dust[d].noLight = true;
                        Main.dust[d].velocity *= 12f;
                    }
                    break;

                case 26: //rain primes
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
                    if (++npc.ai[3] > 180)
                    {
                        npc.ai[0]++;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                    }
                    break;

                default:
                    npc.ai[0] = 11;
                    goto case 10;
            }
        }

        private void SpawnSphereRing(int max, float speed, int damage, float rotationModifier)
        {
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = Main.player[npc.target].Center - npc.Center;
            vel.Normalize();
            vel *= speed;
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
                if (npc.timeLeft > 60)
                    npc.timeLeft = 60;
                npc.velocity.Y -= 1f;
                return false;
            }
            return true;
        }

        private bool Phase2Check()
        {
            if (npc.life < npc.lifeMax / 2)
            {
                npc.ai[0] = 10;
                npc.ai[1] = 0;
                npc.ai[2] = 0;
                npc.ai[3] = 0;
                npc.netUpdate = true;
                if (Main.netMode != 1)
                {
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual2"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual3"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual4"), 0, 0f, Main.myPlayer, 0f, npc.whoAmI);
                }
                return true;
            }
            return false;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
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
            if (npc.ai[0] == -2)
                return true;

            npc.life = 1;
            npc.active = true;
            if (Main.netMode != 1)
            {
                npc.ai[0] = -1;
                npc.ai[1] = 0;
                npc.dontTakeDamage = true;
                npc.netUpdate = true;
            }
            return false;
        }

        public override void NPCLoot()
        {
            FargoSoulsWorld.downedMutant = true;
            if (Main.netMode == 2)
                NetMessage.SendData(7); //sync world
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("Sadism"), Main.rand.Next(5) + 5);
        }

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.GreaterHealingPotion;
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