using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantSlimeRain : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Items/Weapons/FinalUpgrades/SlimeRain";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Rain");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[projectile.type] = 2;
        }

        public override void SetDefaults()
        {
            projectile.width = 90;
            projectile.height = 28;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            cooldownSlot = 1;
        }

        public override void AI()
        {
            int ai0 = (int)projectile.ai[0];
            if (ai0 > -1 && ai0 < 200 && Main.npc[ai0].active && Main.npc[ai0].type == mod.NPCType("MutantBoss") 
                && (Main.npc[ai0].ai[0] == 35 || Main.npc[ai0].ai[0] == 38))
            {
                projectile.timeLeft = 2;
                projectile.Center = Main.npc[ai0].Center;
                projectile.position.X += projectile.width / 2 * Main.npc[ai0].spriteDirection;
                projectile.spriteDirection = Main.npc[ai0].spriteDirection;
                projectile.rotation = (float)Math.PI / 4 * projectile.spriteDirection;
            }
            else
            {
                projectile.Kill();
                return;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(BuffID.Slimed, 300);
            target.AddBuff(mod.BuffType("MutantFang"), 300);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Color color26 = lightColor;
            color26 = projectile.GetAlpha(color26);

            SpriteEffects effects = projectile.spriteDirection > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            for (int i = 0; i < ProjectileID.Sets.TrailCacheLength[projectile.type]; i += 2)
            {
                Color color27 = color26;
                color27 *= (float)(ProjectileID.Sets.TrailCacheLength[projectile.type] - i) / ProjectileID.Sets.TrailCacheLength[projectile.type];
                Vector2 value4 = projectile.oldPos[i];
                float num165 = projectile.oldRot[i];
                Main.spriteBatch.Draw(texture2D13, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color27, num165, origin2, projectile.scale, effects, 0f);
            }

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, effects, 0f);
            return false;
        }
    }
}