using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.BossWeapons
{
    public class HentaiSpear : ModProjectile
    {
        // In here the AI uses this example, to make the code more organized and readible
        // Also showcased in ExampleJavelinProjectile.cs
        public float MovementFactor // Change this value to alter how fast the spear moves
        {
            get { return projectile.ai[0]; }
            set { projectile.ai[0] = value; }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Penetrator");
        }

        public override void SetDefaults()
        {
            projectile.width = 58;
            projectile.height = 58;
            projectile.aiStyle = 19;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.scale = 1.3f;
            projectile.hide = true;
            projectile.melee = true;
            projectile.alpha = 0;
            projectile.usesLocalNPCImmunity = true;
            projectile.localNPCHitCooldown = 0;
        }
        // It appears that for this AI, only the ai0 field is used!

        public override void AI()
        {
            //dust!
            int dustId = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width / 2, projectile.height + 5, 15, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId].noGravity = true;
            int dustId3 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y + 2f), projectile.width / 2, projectile.height + 5, 15, projectile.velocity.X * 0.2f,
                projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
            Main.dust[dustId3].noGravity = true;

            Player projOwner = Main.player[projectile.owner];
            // Here we set some of the projectile's owner properties, such as held item and itemtime, along with projectile directio and position based on the player
            Vector2 ownerMountedCenter = projOwner.RotatedRelativePoint(projOwner.MountedCenter);
            projectile.direction = projOwner.direction;
            projOwner.heldProj = projectile.whoAmI;
            projOwner.itemTime = projOwner.itemAnimation;
            projectile.position.X = ownerMountedCenter.X - projectile.width / 2;
            projectile.position.Y = ownerMountedCenter.Y - projectile.height / 2;
            // Change the spear position based off of the velocity and the movementFactor
            projectile.position += projectile.velocity * MovementFactor;
            // As long as the player isn't frozen, the spear can move
            if (!projOwner.frozen)
            {
                if (MovementFactor == 0f) // When intially thrown out, the ai0 will be 0f
                {
                    MovementFactor = 3f; // Make sure the spear moves forward when initially thrown out
                    projectile.netUpdate = true; // Make sure to netUpdate this spear
                }

                if (projOwner.itemAnimation < projOwner.itemAnimationMax / 3) // Somewhere along the item animation, make sure the spear moves back
                {
                    MovementFactor -= 2.4f;
                    if (projectile.localAI[0] == 0f)
                    {
                        projectile.localAI[0] = 1f;
                        if (projectile.ai[1] == 0f && projectile.owner == Main.myPlayer) //only right click
                        {
                            Vector2 vel = Vector2.Normalize(projectile.velocity) * 8f;
                            for (int i = -2; i <= 2; i++)
                            {
                                Projectile.NewProjectile(projectile.Center, vel.RotatedBy(Math.PI / 36 * i), mod.ProjectileType("PhantasmalBolt"),
                                    projectile.damage / 3, projectile.knockBack, projectile.owner);
                            }
                        }
                    }
                }
                else // Otherwise, increase the movement factor
                {
                    MovementFactor += 2.1f;
                }
            }

            // When we reach the end of the animation, we can kill the spear projectile
            if (projOwner.itemAnimation == 0) projectile.Kill();
            // Apply proper rotation, with an offset of 135 degrees due to the sprite's rotation, notice the usage of MathHelper, use this class!
            // MathHelper.ToRadians(xx degrees here)
            projectile.rotation = (float) Math.Atan2(projectile.velocity.Y, projectile.velocity.X) + MathHelper.ToRadians(135f);
            // Offset by 90 degrees here
            if (projectile.spriteDirection == -1) projectile.rotation -= MathHelper.ToRadians(90f);
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (projectile.owner == Main.myPlayer)
            {
                if (projectile.ai[1] != 0f)
                {
                    Projectile.NewProjectile(target.position + new Vector2(Main.rand.Next(target.width), Main.rand.Next(target.height)),
                        Vector2.Zero, mod.ProjectileType("PhantasmalBlast"), projectile.damage, projectile.knockBack * 3f, projectile.owner);
                    Projectile.NewProjectile(target.position + new Vector2(Main.rand.Next(target.width), Main.rand.Next(target.height)),
                        Vector2.Zero, mod.ProjectileType("PhantasmalBlast"), projectile.damage, projectile.knockBack * 3f, projectile.owner);
                    Projectile.NewProjectile(target.position + new Vector2(Main.rand.Next(target.width), Main.rand.Next(target.height)),
                        Vector2.Zero, mod.ProjectileType("PhantasmalBlast"), projectile.damage, projectile.knockBack * 3f, projectile.owner);

                }
                else if (projectile.numHits % 4 == 0)
                {
                    Projectile.NewProjectile(target.position + new Vector2(Main.rand.Next(target.width), Main.rand.Next(target.height)),
                        Vector2.Zero, mod.ProjectileType("PhantasmalBlast"), projectile.damage, projectile.knockBack * 3f, projectile.owner);
                }
            }
            target.AddBuff(mod.BuffType("CurseoftheMoon"), 600);
        }
    }
}