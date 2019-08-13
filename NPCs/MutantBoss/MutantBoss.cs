using Microsoft.Xna.Framework;
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
            DisplayName.SetDefault("???");
            //Main.npcFrameCount[npc.type] = 4;
            NPCID.Sets.TrailCacheLength[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 18;
            npc.height = 40;
            npc.damage = 200;
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
            npc.buffImmune[mod.BuffType("TimeFrozen")] = true;
            music = MusicID.TheTowers;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 200;
            npc.lifeMax = (int)(7000000 * bossLifeScale);
        }

        public override void AI()
        {
            if (npc.localAI[3] == 0)
            {
                npc.TargetClosest();
                npc.localAI[3] = 1;
                Main.PlaySound(15, (int)npc.Center.X, (int)npc.Center.Y, 0);
                if (Main.netMode != 1)
                    Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType("MutantRitual"), npc.damage / 6, 0f, Main.myPlayer, 0f, npc.whoAmI);
            }

            if (Main.player[Main.myPlayer].active && npc.Distance(Main.player[Main.myPlayer].Center) < 3000f)
                Main.player[Main.myPlayer].AddBuff(mod.BuffType("MutantPresence"), 2);

            Player player = Main.player[npc.target];
            Vector2 targetPos;
            float speedModifier;
            switch ((int)npc.ai[0])
            {
                case -2: //fade out, drop a mutant
                    npc.dontTakeDamage = true;
                    npc.damage = 0;
                    npc.alpha++;
                    if (npc.alpha > 255)
                    {
                        npc.alpha = 255;
                        npc.life = 0;
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
                    if (++npc.ai[1] > 120 && npc.velocity.Length() < 0.5f)
                    {
                        npc.velocity = Vector2.Zero;
                        npc.ai[0]--;
                        npc.netUpdate = true;
                        if (Main.netMode != 1) //shoot harmless mega rays
                            Main.NewText("cinematic ray placeholder");
                    }
                    break;

                case 0: //track player, throw penetrators
                    if (!AliveCheck(player))
                        break;
                    targetPos = player.Center;
                    targetPos.X += 500 * (npc.Center.X < targetPos.X ? -1 : 1);
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

                    if (++npc.ai[1] > 120)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 60;
                        if (++npc.ai[2] > 3)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.velocity = npc.DirectionTo(player.Center) * 2f;
                        }
                        else if (Main.netMode != 1)
                        {
                            Main.NewText("penetrator placeholder");
                        }
                    }
                    break;

                case 1: //slow drift, shoot phantasmal rings
                    Aura(800f, mod.BuffType("GodEater"), true, 86);
                    if (--npc.ai[1] < 0)
                    {
                        npc.netUpdate = true;
                        npc.ai[1] = 120;
                        if (++npc.ai[2] > 2)
                        {
                            npc.ai[0]++;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[3] = 0;
                            npc.netUpdate = true;
                        }
                        else if (Main.netMode != 1)
                        {
                            Main.NewText("phantasmal ring placeholder");
                        }
                    }
                    break;

                case 2: //fly up to corner above player and then dive
                    if (!AliveCheck(player))
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
                        else if (Main.netMode != 1)
                        {
                            Main.NewText("true eye placeholder");
                        }
                    }
                    break;

                case 4: //maneuvering under player while spinning penetrator
                    if (npc.ai[3] == 0)
                    {
                        if (!AliveCheck(player))
                            break;
                        npc.ai[3] = 1;
                        if (Main.netMode != 1)
                            Main.NewText("spinning penetrator placeholder");
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
                    if (++npc.ai[1] > 120)
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
                                Main.NewText("dashing penetrator placeholder");
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

                default:
                    npc.ai[0] = 0;
                    goto case 0;
            }
        }

        private void Aura(float distance, int buff, bool reverse = false, int dustid = DustID.GoldFlame, bool checkDuration = false)
        {
            //works because buffs are client side anyway :ech:
            Player p = Main.player[Main.myPlayer];
            float range = npc.Distance(p.Center);
            if (reverse ? range > distance && range < 3000f : range < distance)
                p.AddBuff(buff, checkDuration && Main.expertMode && Main.expertDebuffTime > 1 ? 1 : 2);

            for (int i = 0; i < 20; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * distance);
                offset.Y += (float)(Math.Cos(angle) * distance);
                Dust dust = Main.dust[Dust.NewDust(
                    npc.Center + offset - new Vector2(4, 4), 0, 0,
                    dustid, 0, 0, 100, Color.White, 1f
                    )];
                dust.velocity = npc.velocity;
                if (Main.rand.Next(3) == 0)
                    dust.velocity += Vector2.Normalize(offset) * (reverse ? 5f : -5f);
                dust.noGravity = true;
            }
        }

        public bool AliveCheck(Player player)
        {
            if (!player.active || player.dead || Vector2.Distance(npc.Center, player.Center) > 5000f)
            {
                npc.TargetClosest();
                if (npc.timeLeft > 60)
                    npc.timeLeft = 60;
                npc.velocity.Y -= 1f;
                return false;
            }
            return true;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("MutantFang"), 420);
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 5; k++)
                Dust.NewDust(npc.position, npc.width, npc.height, 5, hitDirection, -1f, 0, default(Color), 0.6f);
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
            npc.DropItemInstanced(npc.position, npc.Size, mod.ItemType("Sadism"), Main.rand.Next(10) + 1);
        }
    }
}