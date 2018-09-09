using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items.Weapons.BossDrops
{
    public class EyeFlail : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leash of Cthulhu");
            Tooltip.SetDefault("'The mutilated carcass of a defeated foe..'");
        }

        public override void SetDefaults()
        {
            item.damage = 16;
            item.width = 30;
            item.height = 10;
            item.value = Item.sellPrice(0, 1);
            item.rare = 1;
            item.noMelee = true;
            item.useStyle = 5;
            item.useAnimation = 25;
            item.useTime = 25;
            item.knockBack = 4f;
            item.noUseGraphic = true;
            item.shoot = mod.ProjectileType("LeashFlail");
            item.shootSpeed = 15f;
            item.UseSound = SoundID.Item1;
            item.melee = true;
        }
    }
}