using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class FishNuke : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Nuke");
        }

        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 24;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 600;
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
            if (damage < target.lifeMax / 25)
                damage = target.lifeMax / 25;
            if (projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FishNukeExplosion"),
                    damage, projectile.knockBack * 2f, projectile.owner);

            target.AddBuff(mod.BuffType("OceanicMaul"), 900);
            target.AddBuff(mod.BuffType("MutantNibble"), 900);
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 900);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (projectile.owner == Main.myPlayer)
                Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType("FishNukeExplosion"),
                    projectile.damage, projectile.knockBack * 2f, projectile.owner);
            return true;
        }

        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item84, projectile.Center);
            if (projectile.owner == Main.myPlayer)
                SpawnRazorbladeRing(10, 14f, 0.6f * (projectile.velocity.X > 0 ? -1f : 1f));
        }

        private void SpawnRazorbladeRing(int max, float speed, float rotationModifier)
        {
            float rotation = 2f * (float)Math.PI / max;
            Vector2 vel = projectile.velocity;
            vel.Normalize();
            vel *= speed;
            int type = mod.ProjectileType("RazorbladeTyphoonFriendly");
            for (int i = 0; i < max; i++)
            {
                vel = vel.RotatedBy(rotation);
                Projectile.NewProjectile(projectile.Center, vel, type, projectile.damage / 6,
                    projectile.knockBack, projectile.owner, rotationModifier, 1f);
            }
        }
    }
}