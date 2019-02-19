using Microsoft.Xna.Framework;
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
            item.damage = 200;
            item.ranged = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = new LegacySoundStyle(2, 62);
            item.value = 50000;
            item.rare = 9;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FishNuke");
            item.shootSpeed = 30f;
        }

        //make them hold it different
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12, 0);
        }
    }
}