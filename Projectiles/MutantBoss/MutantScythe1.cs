using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantScythe1 : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant Sickle");
        }

        public override void SetDefaults()
        {
            projectile.width = 48;
            projectile.height = 48;
            projectile.alpha = 100;
            projectile.light = 0.2f;
            projectile.hostile = true;
            projectile.timeLeft = 90;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.aiStyle = -1;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            NPC mutant = Main.npc[(int)projectile.ai[0]];
            if (!mutant.active || mutant.type != mod.NPCType("MutantBoss"))
            {
                projectile.Kill();
                return;
            }
            if (projectile.localAI[0] == 0)
            {
                projectile.localAI[0] = 1;
                Main.PlaySound(SoundID.Item8, projectile.Center);
            }
            projectile.rotation += 0.8f;
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, 27, 0f, 0f, 100);
                Main.dust[d].noGravity = true;
            }
        }

        public override void Kill(int timeLeft)
        {
            if (Main.netMode != 1)
                for (int i = 0; i < 8; i++)
                    Projectile.NewProjectile(projectile.Center, Vector2.UnitX.RotatedBy(Math.PI / 4 * i), mod.ProjectileType("MutantScythe2"), projectile.damage, 0f, projectile.owner);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("MutantFang"), 300);
            target.AddBuff(mod.BuffType("Shadowflame"), 300);
            target.AddBuff(BuffID.Bleeding, 600);
        }
    }
}