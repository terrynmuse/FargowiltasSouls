using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class PlanterasChild : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Plantera's Child");
            //ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.width = 25;
            projectile.height = 25;
            projectile.scale = 1.5f;
            projectile.timeLeft *= 5;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead && player.GetModPlayer<FargoPlayer>().MagicalBulb)
                projectile.timeLeft = 2;

            if (projectile.damage == 0)
                projectile.damage = (int)(60f * player.minionDamage);

            NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
            if (minionAttackTargetNpc != null && projectile.ai[0] != minionAttackTargetNpc.whoAmI && minionAttackTargetNpc.CanBeChasedBy(projectile))
            {
                projectile.ai[0] = minionAttackTargetNpc.whoAmI;
                projectile.netUpdate = true;
            }

            if (projectile.ai[0] >= 0 && projectile.ai[0] < 200) //has target
            {
                NPC npc = Main.npc[(int)projectile.ai[0]];

                if (npc.CanBeChasedBy(projectile))
                {
                    Vector2 target = npc.Center - projectile.Center;
                    float length = target.Length();
                    if (length > 1000f) //too far, lose target
                    {
                        projectile.ai[0] = -1f;
                        projectile.netUpdate = true;
                    }
                    else if (length > 50f)
                    {
                        target.Normalize();
                        target *= 16f;
                        projectile.velocity = (projectile.velocity * 40f + target) / 41f;
                    }

                    projectile.localAI[0]++;
                    if (projectile.localAI[0] > 15f) //shoot seed/spiky ball
                    {
                        projectile.localAI[0] = 0f;
                        if (projectile.owner == Main.myPlayer)
                        {
                            Vector2 speed = projectile.velocity;
                            speed.Normalize();
                            speed *= 17f;
                            int damage = projectile.damage * 2 / 3;
                            int type;
                            if (Main.rand.Next(2) == 0)
                            {
                                damage = damage * 5 / 4;
                                type = mod.ProjectileType("PoisonSeedPlanterasChild");
                                Main.PlaySound(SoundID.Item17, projectile.position);
                            }
                            else if (Main.rand.Next(6) == 0)
                            {
                                damage = damage * 3 / 2;
                                type = mod.ProjectileType("SpikyBallPlanterasChild");
                            }
                            else
                            {
                                type = mod.ProjectileType("SeedPlanterasChild");
                                Main.PlaySound(SoundID.Item17, projectile.position);
                            }
                            if (projectile.owner == Main.myPlayer)
                                Projectile.NewProjectile(projectile.Center, speed, type, damage, projectile.knockBack, projectile.owner);
                        }
                    }
                }
                else //forget target
                {
                    projectile.ai[0] = -1f;
                    projectile.netUpdate = true;
                }
            }
            else //no target
            {
                Vector2 target = player.Center - projectile.Center;
                target.Y -= 50f;
                float length = target.Length();
                if (length > 2000f)
                {
                    projectile.Center = player.Center;
                    projectile.ai[0] = -1f;
                    projectile.netUpdate = true;
                }
                else if (length > 70f)
                {
                    target.Normalize();
                    target *= (length > 200f) ? 10f : 6f;
                    projectile.velocity = (projectile.velocity * 40f + target) / 41f;
                }

                projectile.localAI[1]++;
                if (projectile.localAI[1] > 6f)
                {
                    projectile.localAI[1] = 0f;

                    float maxDistance = 1000f;
                    int possibleTarget = -1;
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.CanBeChasedBy(projectile))// && Collision.CanHitLine(projectile.Center, 0, 0, npc.Center, 0, 0))
                        {
                            float npcDistance = player.Distance(npc.Center);
                            if (npcDistance < maxDistance)
                            {
                                maxDistance = npcDistance;
                                possibleTarget = i;
                            }
                        }
                    }

                    if (possibleTarget >= 0)
                    {
                        projectile.ai[0] = possibleTarget;
                        projectile.netUpdate = true;
                    }
                }
            }

            projectile.rotation = projectile.velocity.ToRotation();
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Infested"), 360);
            target.AddBuff(BuffID.Venom, 360);
            target.AddBuff(BuffID.Poisoned, 360);
        }
    }
}