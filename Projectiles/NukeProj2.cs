using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class NukeProj2 : ModProjectile
    {
        public int countdown = 4;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nuke");
        }

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = 16; //explosives AI
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 2400;
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            if (projectile.timeLeft % 600 == 0)
            {
                Main.NewText(countdown.ToString(), 51, 102, 0);
                countdown--;
            }
            
            projectile.scale += .01f;            
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;   //Making the timer go up.
            if (projectile.frameCounter >= 600)  //how fast animation is
            {
                projectile.frame++; //Making the frame go up...
                projectile.frameCounter = 0; //Resetting the timer.
                if (projectile.frame > 3) //amt of frames - 1
                {
                    projectile.frame = 0;
                }
            }

            return true;
        }

        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < Main.maxTilesX; i++)
            {
                for (int j = 0; j < Main.maxTilesY; j++)
                {
                    FargoGlobalTile.ClearEverything(i, j);
                    
                    if (WorldGen.InWorld(i, j))
                        Main.Map.Update(i, j, 255);
                }
            }
            
            Main.refreshMap = true;
            
            //custom sound when
            Main.PlaySound(SoundID.Item15, projectile.position);
        }
    }
}