using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class EaterStaff : ModItem
    {
        private int _shoot;
        public float PermaX, PermaY, PermaXPos, PermaYPos;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater of Worlds Wand");
            Tooltip.SetDefault("'An old foe beaten into submission..'"); //
        }

        public override void SetDefaults()
        {
            item.damage = 8;
            item.summon = true;
            item.mana = 20;
            item.width = 40;
            item.height = 40;
            item.useTime = 4;
            item.useAnimation = 32;
            item.useStyle = 5;
            item.knockBack = 2;
            item.value = 10000; //
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("EaterHead");
            item.shootSpeed = 8f;
            item.reuseDelay = 80;


            item.noMelee = true;

            item.UseSound = SoundID.Item44; //
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -15);
        }

        public override bool UseItem(Player player)
        {
            _shoot = 0;
            return true;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY,
            ref int type, ref int damage, ref float knockback)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_shoot)
            {
                case 0:
                    Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EaterHead"),
                        damage, knockback, player.whoAmI);
                    PermaX = speedX;
                    PermaY = speedY;
                    PermaXPos = position.X;
                    PermaYPos = position.Y;
                    break;
                case 1:
                case 2:
                    Projectile.NewProjectile(PermaXPos, PermaYPos, PermaX, PermaY, mod.ProjectileType("EaterBody"),
                        damage, knockback, player.whoAmI);
                    break;
                case 3:
                    Projectile.NewProjectile(PermaXPos, PermaYPos, PermaX, PermaY, mod.ProjectileType("EaterTail"),
                        damage, knockback, player.whoAmI);
                    break;
                case 7:
                    _shoot = 0;
                    return false;
            }

            _shoot++;
            return false;
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(BuffID.Slimed, 120);
        }
    }
}