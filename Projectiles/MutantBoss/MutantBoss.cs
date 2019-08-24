using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using FargowiltasSouls.NPCs;

namespace FargowiltasSouls.Projectiles.MutantBoss
{
    public class MutantBoss : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/NPCs/MutantBoss/MutantBoss";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mutant");
            Main.projFrames[projectile.type] = 4;
        }

        public override void SetDefaults()
        {
            projectile.width = 34;
            projectile.height = 50;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }

        public override bool PreAI()
        {
            projectile.GetGlobalProjectile<FargoGlobalProjectile>().TimeFrozen = 0;
            return true;
        }

        public override void AI()
        {
            int ai1 = (int)projectile.ai[1];
            if (projectile.ai[1] >= 0f && projectile.ai[1] < 200f &&
                Main.npc[ai1].active && Main.npc[ai1].type == mod.NPCType("MutantBoss"))
            {
                projectile.Center = Main.npc[ai1].Center;
                projectile.alpha = Main.npc[ai1].alpha;
                projectile.direction = projectile.spriteDirection = Main.npc[ai1].direction;
                projectile.timeLeft = 2;
            }
            else
            {
                projectile.Kill();
                return;
            }

            if (++projectile.frameCounter > 6)
            {
                projectile.frameCounter = 0;
                if (++projectile.frame >= 4)
                    projectile.frame = 0;
            }
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
            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2, projectile.scale, projectile.spriteDirection > 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            return false;
        }
    }
}