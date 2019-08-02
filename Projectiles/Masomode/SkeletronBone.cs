using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class SkeletronBone : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_471";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone");
        }

        public override void SetDefaults()
        {
            projectile.CloneDefaults(ProjectileID.SkeletonBone);
            aiType = ProjectileID.SkeletonBone;
            projectile.light = 1f;
            projectile.scale = 1.5f;
            cooldownSlot = 1;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (FargoGlobalNPC.BossIsAlive(ref FargoGlobalNPC.guardBoss, NPCID.DungeonGuardian))
            {
                target.AddBuff(mod.BuffType("GodEater"), 420);
                target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 420);
                target.AddBuff(mod.BuffType("MarkedforDeath"), 420);
                target.immune = false;
                target.immuneTime = 0;
                target.hurtCooldowns[1] = 0;
            }
        }
    }
}