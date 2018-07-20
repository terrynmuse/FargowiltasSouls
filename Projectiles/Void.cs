using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Projectiles
{
	public class Void : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Void");
		}
		public override void SetDefaults()
		{
			projectile.width = 20;
			projectile.height = 24;
			projectile.aiStyle = 1; //
			projectile.friendly = true;
			projectile.magic = true; //
			projectile.penetrate = -1; //
			projectile.timeLeft = 1000;
			aiType = ProjectileID.Bullet; //
		}
		
		public override string Texture
		{
			get
			{
				return "FargowiltasSouls/Items/Placeholder";
			}
		}
		
		public override void AI()
		{	
			for(int i = 0; i < 200; i++)
			{
				float distance = projectile.Distance(Main.npc[i].Center);
                NPC npc = Main.npc[i];

				if(npc.active && !npc.townNPC && npc.type != NPCID.DD2EterniaCrystal && npc.type != NPCID.DD2LanePortal && !Main.npc[i].boss && distance < 400) 
				{
					Vector2 dir = projectile.position - npc.Center;
					npc.velocity = dir.SafeNormalize(Vector2.Zero) * 10;
				}
			}
		}
		
		public override void Kill(int timeleft)
		{
			for (int num468 = 0; num468 < 20; num468++)
			{
				int num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 60, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100, default(Color), 2f);
				Main.dust[num469].noGravity = true;
				Main.dust[num469].velocity *= 2f;
				num469 = Dust.NewDust(new Vector2(projectile.Center.X, projectile.Center.Y), projectile.width, projectile.height, 60, -projectile.velocity.X * 0.2f, -projectile.velocity.Y * 0.2f, 100);
				Main.dust[num469].velocity *= 2f;
			}
		}
	}
}