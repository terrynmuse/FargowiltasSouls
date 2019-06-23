using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class PrimeStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prime Staff");
            Tooltip.SetDefault("'An old foe's limbs reanimated for your desires..'");
            DisplayName.AddTranslation(GameCulture.Chinese, "至尊法杖");
            Tooltip.AddTranslation(GameCulture.Chinese, "'一个老对手的肢体,随你的意愿而复活..'");
        }

        public override bool Autoload(ref string name)
        {
            return false;
        }

        public override void SetDefaults()
        {
            item.damage = 32;
            item.summon = true;
            item.mana = 10;
            item.width = 26;
            item.height = 28;
            item.useTime = 36;
            item.useAnimation = 36;
            item.useStyle = 1;
            item.noMelee = true;
            item.knockBack = 3;
            item.rare = 2;
            item.UseSound = SoundID.Item44;
            item.shoot = mod.ProjectileType("PrimeCannon");
            item.shootSpeed = 10f;
            //item.buffType = mod.BuffType("BrainMinion");
            //item.buffTime = 3600;
            item.autoReuse = true;
        }
    }
}