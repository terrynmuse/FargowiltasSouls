using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class FishNukeExplosion : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/Explosion";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Nuke");
            //ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            //ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            projectile.width = 500;
            projectile.height = 500;
            projectile.aiStyle = 0;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2;
            projectile.tileCollide = false;
            projectile.light = 0.75f;
            projectile.ignoreWater = true;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }

        /*public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (target.whoAmI == NPCs.FargoGlobalNPC.fishBossEX)
            {
                target.life += damage;
                if (target.life > target.lifeMax)
                    target.life = target.lifeMax;
                CombatText.NewText(target.Hitbox, CombatText.HealLife, damage);
                damage = 0;
                crit = false;
            }
        }*/

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.immune[projectile.owner] = 0;
            target.AddBuff(mod.BuffType("OceanicMaul"), 900);
            target.AddBuff(mod.BuffType("MutantNibble"), 900);
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 900);
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, projectile.Center, 14);
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 31, 0f, 0f, 100, default(Color), 3f);
                Main.dust[dust].velocity *= 1.4f;
            }
            for (int i = 0; i < 30; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 135, 0f, 0f, 0, default(Color), 3.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].noLight = true;
                Main.dust[d].velocity *= 4f;
            }
            for (int i = 0; i < 20; i++)
            {
                int dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 6, 0f, 0f, 100, default(Color), 3.5f);
                Main.dust[dust].noGravity = true;
                Main.dust[dust].velocity *= 7f;
                dust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width,
                    projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[dust].velocity *= 3f;
            }

            for (int i = 0; i < 5; i++)
            {
                float scaleFactor9 = 0.5f;
                if (i == 1 || i == 3) scaleFactor9 = 1f;

                for (int j = 0; j < 4; j++)
                {
                    int gore = Gore.NewGore(new Vector2(projectile.Center.X, projectile.Center.Y),
                        default(Vector2),
                        Main.rand.Next(61, 64));

                    Main.gore[gore].velocity *= scaleFactor9;
                    Main.gore[gore].velocity.X += 1f;
                    Main.gore[gore].velocity.Y += 1f;
                }
            }


        }
    }
}