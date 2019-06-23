using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class BrainStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mind Break");
            Tooltip.SetDefault("'An old foe beaten into submission..'\n Needs 2 minion slots");
            DisplayName.AddTranslation(GameCulture.Chinese, "精神崩坏");
            Tooltip.AddTranslation(GameCulture.Chinese, "'一个被迫屈服的老对手..'\n需要2个召唤栏");
        }

        public override void SetDefaults()
        {
            item.damage = 35;
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
            item.shoot = mod.ProjectileType("BrainProj");
            item.shootSpeed = 10f;
            item.buffType = mod.BuffType("BrainMinion");
            item.buffTime = 3600;
            item.autoReuse = true;
            item.value = Item.sellPrice(0, 2);
        }

        public override bool CanUseItem(Player player)
        {
            return player.maxMinions >= 2;
        }
    }
}
