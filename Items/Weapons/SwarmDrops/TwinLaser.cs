using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.SwarmDrops
{
    public class TwinLaser : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gemini Cannon");
            Tooltip.SetDefault("");
        }

        public override void SetDefaults()
        {
            item.damage = 45; //
            item.magic = true;
            item.width = 24;
            item.height = 24;
            item.channel = true;
            item.mana = 5; //
            item.useTime = 20;
            item.useAnimation = 20; //
            item.reuseDelay = 20;
            item.useStyle = 5;
            item.noMelee = true;
            //item.UseSound = new LegacySoundStyle(4, 13);
            item.value = 50000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("GeminiLaser1");
            item.shootSpeed = 14f;



        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {

            //Main.NewText("mouse:" + Main.MouseWorld + " pos:" + position);



            Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI );

            return true;
        }
    }
}