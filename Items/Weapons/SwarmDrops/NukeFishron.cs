using Terraria.Audio;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
	public class NukeFishron : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Nuke Fishron");
			Tooltip.SetDefault("'The highly weaponized remains of a defeated foe..'");
		}

		 public override void SetDefaults()
	    {
			item.damage = 1;
			item.ranged = true;
			item.width = 24;
			item.height = 24;
			item.useTime = 10;
			item.useAnimation = 100;
			item.useStyle = 5; 
			item.noMelee = true; 
			item.knockBack = 1.5f; 
			item.UseSound = new LegacySoundStyle(4, 13);
			item.value = 50000; 
			item.rare = 5; 
			item.autoReuse = true; 
			item.shoot = mod.ProjectileType("FishNuke"); 
			item.shootSpeed = 20f; 
		}
		
		/*public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

			Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI, 0f, 0f);
			
            return false;
        }*/
	}
}