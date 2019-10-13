using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class PhantasmalRing : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_454";

        private const float PI = (float)Math.PI;
        private const float rotationPerTick = PI / 57f;
        private const float threshold = 350;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phantasmal Ring");
            Main.projFrames[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 46;
            projectile.height = 46;
            projectile.scale *= 2f;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.alpha = 255;
        }

        public override void AI()
        {
            if (Main.player[projectile.owner].active && !Main.player[projectile.owner].dead && Main.player[projectile.owner].GetModPlayer<FargoPlayer>().PhantasmalRing)
            {
                projectile.alpha = 0;
            }
            else
            {
                projectile.Kill();
                return;
            }

            projectile.Center = Main.player[projectile.owner].Center;

            if (projectile.damage == 0)
                projectile.damage = (int)(900 * Main.player[projectile.owner].minionDamage);

            projectile.timeLeft = 2;
            projectile.scale = (1f - projectile.alpha / 255f) * 0.5f;
            projectile.ai[0] += rotationPerTick;
            if (projectile.ai[0] > PI)
            {
                projectile.ai[0] -= 2f * PI;
                projectile.netUpdate = true;
            }

            projectile.frameCounter++;
            if (projectile.frameCounter >= 6)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame > 1)
                    projectile.frame = 0;
            }

            if (++projectile.localAI[0] > 20)
            {
                projectile.localAI[0] = 0;
                if (projectile.owner == Main.myPlayer && SoulConfig.Instance.GetValue("Phantasmal Ring Minion"))
                {
                    if (++projectile.localAI[1] >= 7)
                        projectile.localAI[1] = 0;
                    if (++projectile.localAI[1] >= 7)
                        projectile.localAI[1] = 0;
                    int target = HomeOnTarget();
                    if (target != -1)
                    {
                        Projectile.NewProjectile(projectile.Center, projectile.DirectionTo(Main.npc[target].Center), mod.ProjectileType("RingDeathray"), projectile.damage, 0f, projectile.owner, projectile.whoAmI, projectile.localAI[1]);
                    }
                }
            }
        }

        private int HomeOnTarget()
        {
            NPC minionAttackTargetNpc = projectile.OwnerMinionAttackTargetNPC;
            if (minionAttackTargetNpc != null && minionAttackTargetNpc.CanBeChasedBy(projectile))
                return minionAttackTargetNpc.whoAmI;

            const float homingMaximumRangeInPixels = 2000;
            int selectedTarget = -1;
            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC n = Main.npc[i];
                if (n.CanBeChasedBy(projectile))
                {
                    float distance = projectile.Distance(n.Center);
                    if (distance <= homingMaximumRangeInPixels &&
                        (
                            selectedTarget == -1 || //there is no selected target
                            projectile.Distance(Main.npc[selectedTarget].Center) > distance) //or we are closer to this target than the already selected target
                    )
                        selectedTarget = i;
                }
            }

            return selectedTarget;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = projectile.GetAlpha(lightColor);

            for (int x = 0; x < 7; x++)
            {
                Vector2 drawOffset = new Vector2(threshold * projectile.scale / 2f, 0f).RotatedBy(projectile.ai[0]);
                drawOffset = drawOffset.RotatedBy(2f * PI / 7f * x);
                const int max = 4;
                for (int i = 0; i < max; i++)
                {
                    Color color27 = color26;
                    color27 *= (float)(max - i) / max;
                    Vector2 value4 = projectile.Center + drawOffset.RotatedBy(rotationPerTick * -i);
                    float num165 = projectile.rotation;
                    Main.spriteBatch.Draw(texture2D13, value4 - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, SpriteEffects.None, 0f);
                }
                Main.spriteBatch.Draw(texture2D13, projectile.Center + drawOffset - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, projectile.rotation, origin2, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White * projectile.Opacity * .3f;
        }
    }
}