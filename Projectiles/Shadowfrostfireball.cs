using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class Shadowfrostfireball : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_253";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadowfrostfireball");
        }

        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.timeLeft = 360;
        }

        public override void AI()
        {
            int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity.X *= 0.3f;
            Main.dust[index2].velocity.Y *= 0.3f;
            index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, new Color(), 2f);
            Main.dust[index2].noGravity = true;
            Main.dust[index2].velocity.X *= 0.3f;
            Main.dust[index2].velocity.Y *= 0.3f;
            
            if (projectile.ai[0] >= 0 && projectile.ai[0] < 200f)
            {
                int ai0 = (int)projectile.ai[0];
                if (Main.npc[ai0].CanBeChasedBy(ai0))
                {
                    Vector2 dist = Main.npc[ai0].Center - projectile.Center;
                    dist.Normalize();
                    dist *= 8f;
                    projectile.velocity.X = (projectile.velocity.X * 14 + dist.X) / 15;
                    projectile.velocity.Y = (projectile.velocity.Y * 14 + dist.Y) / 15;
                }
                else
                {
                    projectile.ai[0] = -1f;
                    projectile.netUpdate = true;
                }
            }
            else
            {
                if (--projectile.localAI[0] < 0f)
                {
                    projectile.localAI[0] = 10f;
                    float maxDistance = 1000f;
                    int possibleTarget = -1;
                    for (int i = 0; i < 200; i++)
                    {
                        NPC npc = Main.npc[i];
                        if (npc.CanBeChasedBy(projectile))// && Collision.CanHitLine(projectile.Center, 0, 0, npc.Center, 0, 0))
                        {
                            float npcDistance = projectile.Distance(npc.Center);
                            if (npcDistance < maxDistance)
                            {
                                maxDistance = npcDistance;
                                possibleTarget = i;
                            }
                        }
                    }
                    projectile.ai[0] = possibleTarget;
                    projectile.netUpdate = true;
                }
            }

            projectile.spriteDirection = projectile.direction = projectile.velocity.X > 0 ? 1 : -1;
            projectile.rotation += 0.3f * projectile.direction;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
            for (int index1 = 0; index1 < 10; ++index1)
            {
                int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, new Color(), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 2f;
                index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, new Color(), 2f);
                Main.dust[index2].noGravity = true;
                Main.dust[index2].velocity *= 2f;
                int index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, new Color(), 1f);
                Main.dust[index3].velocity *= 2f;
                index3 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, new Color(), 1f);
                Main.dust[index3].velocity *= 2f;
            }
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 240);
            target.AddBuff(BuffID.ShadowFlame, 240);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(200, 200, 200, 25);
        }
    }
}