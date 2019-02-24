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

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (FargoWorld.MasochistMode && NPCs.FargoGlobalNPC.BossIsAlive(ref NPCs.FargoGlobalNPC.fishBossEX, NPCID.DukeFishron))
            {
                target.life += damage + target.lifeMax / 25;
                if (target.life > target.lifeMax)
                    target.life = target.lifeMax;
                CombatText.NewText(target.Hitbox, CombatText.HealLife, damage);
                damage = 0;
            }
            else if (target.lifeMax / 25 > damage)
            {
                damage = target.lifeMax / 25;
            }
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType<Explosion>(), damage, 5f, Main.myPlayer);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Projectile.NewProjectile(projectile.Center, Vector2.Zero, mod.ProjectileType<Explosion>(), projectile.damage, 5f, Main.myPlayer);

            return true;
        }
    }
}