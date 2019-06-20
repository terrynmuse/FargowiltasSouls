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
            DisplayName.AddTranslation(GameCulture.Chinese, "叶绿水晶");
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.damage = 0;
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

            if (npc.HasPlayerTarget && Main.player[npc.target].active)
            {
                npc.ai[2]++;
                if (npc.ai[2] > 240f) //projectile timer
                {
                    npc.ai[2] = 0;
                    npc.netUpdate = true;
                    Main.PlaySound(6, (int)npc.position.X, (int)npc.position.Y);
                    if (Main.netMode != -1)
                    {
                        Vector2 distance = Main.player[npc.target].Center - npc.Center + Main.player[npc.target].velocity * 30f;
                        distance.Normalize();
                        distance *= 16f;
                        int damage = 24;
                        if (!Main.player[npc.target].ZoneJungle)
                            damage = damage * 2;
                        else if (Main.expertMode)
                            damage = damage * 9 / 10;
                        damage = (int)(damage * (1 + FargoWorld.PlanteraCount * .0125));
                        Projectile.NewProjectile(npc.Center, distance, mod.ProjectileType("CrystalLeafShot"), damage, 0f, Main.myPlayer);
                    }
                    for (int index1 = 0; index1 < 30; ++index1)
                    {
                        int index2 = Dust.NewDust(npc.position, npc.width, npc.height, 157, 0f, 0f, 0, new Color(), 2f);
                        Main.dust[index2].noGravity = true;
                        Main.dust[index2].velocity *= 5f;
                    }
                }
            }
            else
            {
                npc.TargetClosest(false);
            }

            Lighting.AddLight(npc.Center, 0.1f, 0.4f, 0.2f);
            npc.scale = (Main.mouseTextColor / 200f - 0.35f) * 0.2f + 0.95f;
            npc.life = npc.lifeMax;

            npc.position = plantera.Center + new Vector2(npc.ai[1], 0f).RotatedBy(npc.ai[3]);
            npc.position.X -= npc.width / 2;
            npc.position.Y -= npc.height / 2;
            float rotation = 0.03f;
            npc.ai[3] += rotation;
            if (npc.ai[3] > (float)Math.PI)
            {
                npc.ai[3] -= 2f * (float)Math.PI;
                npc.netUpdate = true;
            }
            npc.rotation = npc.ai[3] + (float)Math.PI / 2f;
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