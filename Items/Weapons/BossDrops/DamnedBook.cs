using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
	public class DamnedBook : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cultist's Spellbook");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
		{
            item.damage = 60;                        
            item.magic = true;
            item.width = 24;
            item.height = 28;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;        
            item.value = 1000;
            item.rare = 6;
            item.mana = 1;
            item.UseSound = SoundID.Item21;
            item.autoReuse = true;
            item.shoot = 1;
            item.shootSpeed = 8f;
		}
		
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
            int rand = Main.rand.Next(4);
            int shoot = 0;

            if (rand == 0)
            {
                shoot = ProjectileID.CultistBossIceMist;
            }
            else if (rand == 1)
            {
                shoot = ProjectileID.CultistBossFireBall;
            }
            else if (rand == 2)
            {
                shoot = ProjectileID.VortexLightning;
            }
            else
            {
                shoot = ProjectileID.CultistBossFireBallClone;
            }

            Projectile p = Projectile.NewProjectileDirect(position, new Vector2(speedX, speedY), shoot, damage, knockBack, player.whoAmI);
            p.hostile = false;
            p.friendly = true;
            //p.playerImmune[player.whoAmI] = 1;

            return false;
        }
	}
}