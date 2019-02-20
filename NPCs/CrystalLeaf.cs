using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.NPCs
{
    public class CrystalLeaf : ModNPC
    {
        public override string Texture => "Terraria/Projectile_226";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Leaf");
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.damage = 60;
            npc.defense = 9999;
            npc.lifeMax = 9999;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.knockBackResist = 0f;
            //npc.alpha = 255;
            npc.lavaImmune = true;
            for (int i = 0; i < npc.buffImmune.Length; i++)
                npc.buffImmune[i] = true;
            npc.aiStyle = -1;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 120;
            npc.lifeMax = 9999;
            npc.life = 9999;
        }

        public override void AI()
        {
            if (npc.ai[0] < 0f || npc.ai[0] >= 200f)
            {
                npc.active = false;
                npc.netUpdate = true;
                return;
            }
            NPC plantera = Main.npc[(int)npc.ai[0]];
            if (!plantera.active || plantera.type != NPCID.Plantera)
            {
                npc.active = false;
                npc.netUpdate = true;
                return;
            }

            Lighting.AddLight(npc.Center, 0.1f, 0.4f, 0.2f);
            npc.scale = (Main.mouseTextColor / 200f - 0.35f) * 0.2f + 0.95f;
            npc.life = npc.lifeMax;

            npc.position = plantera.Center + new Vector2(npc.ai[1], 0f).RotatedBy(npc.ai[3]);
            npc.position.X -= npc.width / 2;
            npc.position.Y -= npc.height / 2;
            float rotation = 0.0237f;
            npc.ai[3] += rotation;
            if (npc.ai[3] > (float)Math.PI)
            {
                npc.ai[3] -= 2f * (float)Math.PI;
                npc.netUpdate = true;
            }
        }

        public override void OnHitPlayer(Player player, int damage, bool crit)
        {
            player.AddBuff(BuffID.Poisoned, Main.rand.Next(120, 600));
            player.AddBuff(mod.BuffType("Infested"), Main.rand.Next(60, 300));
            bool isVenomed = false;
            for (int i = 0; i < 22; i++)
            {
                if (player.buffType[i] == BuffID.Venom && player.buffTime[i] > 1)
                {
                    isVenomed = true;
                    player.buffTime[i] += Main.rand.Next(60, 300);
                    if (player.buffTime[i] > 1200)
                    {
                        player.AddBuff(mod.BuffType("Infested"), player.buffTime[i]);
                        Main.PlaySound(15, (int)player.Center.X, (int)player.Center.Y, 0);
                    }
                    break;
                }
            }
            if (!isVenomed)
            {
                player.AddBuff(BuffID.Venom, Main.rand.Next(60, 300));
            }
        }

        public override void ModifyHitByItem(Player player, Item item, ref int damage, ref float knockback, ref bool crit)
        {
            damage = 0;
            npc.life++;
        }

        public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (!projectile.minion)
                projectile.penetrate = 0;
            damage = 0;
            npc.life++;
        }

        public override bool CheckActive()
        {
            return false;
        }

        public override bool PreNPCLoot()
        {
            return false;
        }

        public override bool? DrawHealthBar(byte hbPos, ref float scale, ref Vector2 Pos)
        {
            return false;
        }

        public override Color? GetAlpha(Color drawColor)
        {
            float num4 = Main.mouseTextColor / 200f - 0.3f;
            int num5 = (int)(byte.MaxValue * num4) + 50;
            if (num5 > byte.MaxValue)
                num5 = byte.MaxValue;
            return new Color(num5, num5, num5, 200);
        }
    }
}