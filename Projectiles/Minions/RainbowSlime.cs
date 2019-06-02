using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Minions
{
    public class RainbowSlime : ModProjectile
    {
        public override string Texture => "Terraria/Projectile_266";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rainbow Slime");
            Main.projFrames[projectile.type] = 6;
            //ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.alpha = 75;
            projectile.width = 24;
            projectile.height = 16;
            projectile.timeLeft *= 5;
            projectile.aiStyle = 26;
            aiType = ProjectileID.BabySlime;
            projectile.friendly = true;
            projectile.minion = true;
            projectile.penetrate = -1;

            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 10;
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            if (player.active && !player.dead && player.GetModPlayer<FargoPlayer>().RainbowSlime)
                projectile.timeLeft = 2;
            else
                projectile.Kill();

            if (projectile.damage == 0)
            {
                projectile.damage = (int)(35 * player.minionDamage);
                if (player.GetModPlayer<FargoPlayer>().MasochistSoul)
                    projectile.damage *= 2;
            }

            //Main.NewText(projectile.ai[0].ToString() + " " + projectile.ai[1].ToString() + " " + projectile.localAI[0].ToString() + " " + projectile.localAI[1].ToString());
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = Main.player[projectile.owner].Center.Y > projectile.position.Y + projectile.height;
            return base.TileCollideStyle(ref width, ref height, ref fallThrough);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("FlamesoftheUniverse"), 120);
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, projectile.alpha);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D texture2D13 = Main.projectileTexture[projectile.type];
            int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type]; //ypos of lower right corner of sprite to draw
            int y3 = num156 * projectile.frame; //ypos of upper left corner of sprite to draw
            Rectangle rectangle = new Rectangle(0, y3, texture2D13.Width, num156);
            Vector2 origin2 = rectangle.Size() / 2f;

            Main.spriteBatch.Draw(texture2D13, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY - 4),
                new Microsoft.Xna.Framework.Rectangle?(rectangle), projectile.GetAlpha(lightColor), projectile.rotation, origin2,
                projectile.scale, projectile.spriteDirection < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
            return false;
        }
    }
}