using Terraria.Audio;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class FleshHand : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flesh Hand");
            Tooltip.SetDefault("'The enslaved minions of a defeated foe..'");
            DisplayName.AddTranslation(GameCulture.Chinese, "血肉之手");
            Tooltip.AddTranslation(GameCulture.Chinese, "'战败敌人的仆从..'");
        }

        public override void SetDefaults()
        {
            item.damage = 32;
            item.thrown = true;
            item.width = 24;
            item.height = 24;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = new LegacySoundStyle(4, 13);
            item.value = 50000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("Hungry");
            item.shootSpeed = 20f;
            item.noUseGraphic = true;
        }
    }
}