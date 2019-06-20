using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.NPCs
{
    [AutoloadBossHead]
    public class BrainIllusion : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Brain of Cthulhu");
        }

        public override void SetDefaults()
        {
            npc.width = 160;
            npc.height = 110;
            npc.damage = 0;
            npc.defense = 9999;
            npc.lifeMax = 9999;
            npc.dontTakeDamage = true;
            npc.hide = true;
            npc.HitSound = SoundID.NPCHit9;
            npc.DeathSound = SoundID.NPCDeath11;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            npc.lavaImmune = true;
            npc.aiStyle = -1;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 0;
            npc.lifeMax = 9999;
        }

        public override void AI()
        {
            if (npc.ai[0] < 0f || npc.ai[0] >= 200f)
            {
                npc.StrikeNPCNoInteraction(9999, 0f, 0);
                npc.active = false;
                return;
            }
            NPC brain = Main.npc[(int)npc.ai[0]];
            if (!brain.active || brain.type != NPCID.BrainofCthulhu)
            {
                npc.StrikeNPCNoInteraction(9999, 0f, 0);
                npc.active = false;
                return;
            }

            npc.target = brain.target;
            if (npc.HasPlayerTarget)
            {
                Vector2 distance = Main.player[npc.target].Center - brain.Center;
                npc.Center = Main.player[npc.target].Center;
                npc.position.X += distance.X * npc.ai[1];
                npc.position.Y += distance.Y * npc.ai[2];
            }
            else
            {
                npc.Center = brain.Center;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Confused, Main.rand.Next(300));
            target.AddBuff(mod.BuffType("ClippedWings"), Main.rand.Next(300));
            target.AddBuff(mod.BuffType("Infested"), Main.rand.Next(300));
            target.AddBuff(mod.BuffType("Flipped"), Main.rand.Next(90));
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

        public override bool PreNPCLoot()
        {
            return false;
        }
    }
}