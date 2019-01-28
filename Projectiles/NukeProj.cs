using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
    public class NukeProj : ModProjectile
    {
        public int countdown = 4;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nuke");
        }

        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 32;
            projectile.aiStyle = 16; //explosives AI
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 800;
            Main.projFrames[projectile.type] = 4;
        }

        public override void AI()
        {
            if (projectile.timeLeft % 200 == 0)
            {
                Main.NewText(countdown.ToString(), 51, 102, 0);
                countdown--;
            }
        }

        public override bool PreDraw(SpriteBatch sb, Color lightColor)
        {
            projectile.frameCounter++;   //Making the timer go up.
            if (projectile.frameCounter >= 200)  //how fast animation is
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
            Vector2 position = projectile.Center;
            int radius = 300;     //bigger = boomer

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    int xPosition = (int)(x + position.X / 16.0f);
                    int yPosition = (int)(y + position.Y / 16.0f);

                    if (Math.Sqrt(x * x + y * y) <= radius + 0.5)   //change the shape
                    {
                        Tile tile = Main.tile[xPosition, yPosition];
                        if (tile == null) continue;
                        
                        FargoGlobalTile.ClearTile(tile);
                        FargoGlobalTile.ClearWall(tile);
                        FargoGlobalTile.ClearLiquid(tile, xPosition, yPosition);
                        FargoGlobalTile.SquareUpdate(xPosition, yPosition);

                    }
                }
            }

            for(int i = 0; i < 200; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.active && !npc.boss)
                {
                    npc.StrikeNPC(npc.lifeMax, 0, 0, true);
                }
            }

            // Play explosion sound
            Main.PlaySound(SoundID.Item15, projectile.position);
        }
    }
}