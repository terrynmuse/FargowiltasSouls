using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class Dicer : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Dicer");
            Tooltip.SetDefault("'A defeated foe's attack now on a string'");

            ItemID.Sets.Yoyo[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.useStyle = 5;
            item.width = 24;
            item.height = 24;
            item.noUseGraphic = true;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.shoot = mod.ProjectileType("DicerProj");
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 16f;
            item.knockBack = 2.5f;
            item.damage = 75;
            item.value = Item.sellPrice(0, 30);
            item.rare = 8;
        }
    }
}