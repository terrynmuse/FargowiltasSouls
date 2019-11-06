using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class FishStick : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fish Stick");
            Tooltip.SetDefault("'The carcass of a defeated foe shoved violently on a stick..'");
            DisplayName.AddTranslation(GameCulture.Chinese, "鱼杖");
            Tooltip.AddTranslation(GameCulture.Chinese, "'一个被打败的敌人的尸体,用棍子粗暴地串起来..'");
        }

        public override void SetDefaults()
        {
            item.damage = 66;
            item.summon = true;
            item.mana = 10;
            item.width = 24;
            item.height = 24;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.UseSound = SoundID.Item1;
            item.value = 50000;
            item.rare = 5;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("FishStickProj");
            item.shootSpeed = 20f;
            item.noUseGraphic = true;
        }
    }
}