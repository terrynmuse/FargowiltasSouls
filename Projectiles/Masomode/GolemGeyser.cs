using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles.Masomode
{
    public class GolemGeyser : ModProjectile
    {
        public override string Texture => "FargowiltasSouls/Projectiles/Explosion";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Geyser");
        }

        public override void SetDefaults()
        {
            projectile.width = 2;
            projectile.height = 2;
            projectile.aiStyle = -1;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 600;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.hide = true;
        }

        public override bool CanDamage()
        {
            return false;
        }

        public override void AI()
        {
            int ai0 = (int)projectile.ai[0];
            if (!(ai0 > -1 && ai0 < 200 && Main.npc[ai0].active && Main.npc[ai0].type == NPCID.Golem))
            {
                projectile.Kill();
                return;
            }

            Tile tile = Framing.GetTileSafely(projectile.Center);
            if (tile.nactive() && Main.tileSolid[tile.type]) //if inside solid tile
            {
                //go up, make warning dust
                projectile.position.Y -= 8;
                int d = Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, 0f, -8f);
                Main.dust[d].velocity *= 3f;
            }
            else //if in air, go down
            {
                projectile.position.Y += 8;
            }

            NPC golem = Main.npc[ai0];
            if (golem.GetGlobalNPC<NPCs.FargoSoulsGlobalNPC>().Counter == 2 && Main.netMode != 1) //when golem does second stomp, erupt
            {
                Projectile.NewProjectile(projectile.Center, Vector2.UnitY * -8, ProjectileID.GeyserTrap, projectile.damage, 0f, Main.myPlayer);
                projectile.Kill();
                return;
            }
        }
    }
}