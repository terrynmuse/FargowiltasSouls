using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.NPCs
{
    public class BrainClone : ModNPC
    {
        public override string Texture => "Terraria/NPC_266";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brain of Cthulhu");
            DisplayName.AddTranslation(GameCulture.Chinese, "克苏鲁之脑");
            Main.npcFrameCount[npc.type] = Main.npcFrameCount[NPCID.BrainofCthulhu];
        }

        public override void SetDefaults()
        {
            npc.width = 160;
            npc.height = 110;
            npc.scale += 0.25f;
            npc.damage = 30;
            npc.defense = 14;
            npc.lifeMax = 1000;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath11;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.aiStyle = -1;
        }

        public override void AI()
        {
            if (!npc.GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[0])
            {
                npc.GetGlobalNPC<FargoSoulsGlobalNPC>().masoBool[0] = true;
                Main.npcTexture[npc.type] = Main.npcTexture[NPCID.BrainofCthulhu];
            }

            if (FargoSoulsGlobalNPC.brainBoss < 0f || FargoSoulsGlobalNPC.brainBoss >= 200f)
            {
                npc.StrikeNPCNoInteraction(9999, 0f, 0);
                npc.active = false;
                return;
            }
            NPC brain = Main.npc[FargoSoulsGlobalNPC.brainBoss];
            if (!brain.active || brain.type != NPCID.BrainofCthulhu)
            {
                npc.StrikeNPCNoInteraction(9999, 0f, 0);
                npc.active = false;
                return;
            }

            if (npc.buffType[0] != 0) //constant debuff cleanse
            {
                npc.buffImmune[npc.buffType[0]] = true;
                npc.DelBuff(0);
            }

            npc.target = brain.target;
            npc.damage = brain.damage;
            npc.defDamage = brain.defDamage;
            npc.defense = brain.defense;
            npc.defDefense = brain.defDefense;
            npc.life = brain.life;
            npc.lifeMax = brain.lifeMax;
            npc.knockBackResist = brain.knockBackResist;

            Vector2 vector2 = new Vector2(npc.Center.X, npc.Center.Y);
            float num1 = Main.player[npc.target].Center.X - vector2.X;
            float num2 = Main.player[npc.target].Center.Y - vector2.Y;
            float num3 = 8f / (float)Math.Sqrt(num1 * num1 + num2 * num2);
            float num4 = num1 * num3;
            float num5 = num2 * num3;
            npc.velocity.X = (npc.velocity.X * 50 + num4) / 51f;
            npc.velocity.Y = (npc.velocity.Y * 50 + num5) / 51f;
            if (npc.ai[0] == -2)
            {
                npc.velocity *= 0.9f;
                if (Main.netMode != 0)
                    npc.ai[3] += 15f;
                else
                    npc.ai[3] += 25f;
                if (npc.ai[3] >= 255)
                {
                    npc.ai[3] = 255;
                    npc.position.X = npc.ai[1] * 16f - (float)(npc.width / 2);
                    npc.position.Y = npc.ai[2] * 16f - (float)(npc.height / 2);
                    Main.PlaySound(SoundID.Item8, npc.Center);
                    npc.ai[0] = -3f;
                    npc.netUpdate = true;
                    npc.netSpam = 0;
                }
                npc.alpha = (int)npc.ai[3];
            }
            else if (npc.ai[0] == -3)
            {
                if (Main.netMode != 0)
                    npc.ai[3] -= 15f;
                else
                    npc.ai[3] -= 25f;
                if (npc.ai[3] <= 0)
                {
                    npc.ai[3] = 0.0f;
                    npc.ai[0] = -1f;
                    npc.netUpdate = true;
                    npc.netSpam = 0;
                }
                npc.alpha = (int)npc.ai[3];
            }
            else
            {
                if (Main.netMode != 1)
                {
                    npc.localAI[1]++;
                    if (npc.justHit)
                        npc.localAI[1] -= Main.rand.Next(5);
                    int num6 = 60 + Main.rand.Next(120);
                    if (Main.netMode != 0)
                        num6 += Main.rand.Next(30, 90);
                    if (npc.localAI[1] >= num6)
                    {
                        npc.localAI[1] = 0f;
                        npc.TargetClosest(true);
                        int num7 = 0;
                        do
                        {
                            ++num7;
                            int num8 = (int)Main.player[npc.target].Center.X / 16;
                            int num9 = (int)Main.player[npc.target].Center.Y / 16;
                            int i = Main.rand.Next(2) != 0 ? num8 - Main.rand.Next(7, 13) : num8 + Main.rand.Next(7, 13);
                            int j = Main.rand.Next(2) != 0 ? num9 - Main.rand.Next(7, 13) : num9 + Main.rand.Next(7, 13);
                            if (!WorldGen.SolidTile(i, j))
                            {
                                npc.ai[3] = 0.0f;
                                npc.ai[0] = -2f;
                                npc.ai[1] = (float)i;
                                npc.ai[2] = (float)j;
                                npc.netUpdate = true;
                                npc.netSpam = 0;
                                break;
                            }
                        }
                        while (num7 <= 100);
                    }
                }
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (FargoSoulsGlobalNPC.brainBoss > -1 && FargoSoulsGlobalNPC.brainBoss < 200)
            {
                NPC brain = Main.npc[FargoSoulsGlobalNPC.brainBoss];
                if (brain.active && brain.type == NPCID.BrainofCthulhu)
                    npc.frame.Y = brain.frame.Y;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300));
            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300));
            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(30));
            target.AddBuff(BuffID.Rabies, Main.rand.Next(900, 1800));
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                //Main.PlaySound(npc.DeathSound, npc.Center);
                for (int i = 0; i < 40; i++)
                {
                    int d = Dust.NewDust(npc.position, npc.width, npc.height, 5);
                    Main.dust[d].velocity *= 2.5f;
                    Main.dust[d].scale += 0.5f;
                }
            }
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool CheckDead()
        {
            if (FargoSoulsGlobalNPC.brainBoss > -1 && FargoSoulsGlobalNPC.brainBoss < 200)
            {
                NPC brain = Main.npc[FargoSoulsGlobalNPC.brainBoss];
                if (brain.active && brain.type == NPCID.BrainofCthulhu)
                {
                    npc.active = true;
                    npc.life = brain.life;
                    return false;
                }
            }
            return true;
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}