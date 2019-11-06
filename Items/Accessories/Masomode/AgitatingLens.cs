using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Accessories.Masomode
{
    public class AgitatingLens : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Agitating Lens");
            Tooltip.SetDefault(@"'The irritable remnant of a defeated foe'
Grants immunity to Berserked
10% increased damage when below half HP
While dashing or running quickly you will create a trail of demon scythes");
            DisplayName.AddTranslation(GameCulture.Chinese, "躁动晶状体");
            Tooltip.AddTranslation(GameCulture.Chinese, @"'被打败的敌人的躁动残渣'
免疫狂暴
生命低于50%时,增加10%伤害
冲刺或快速奔跑时发射一串恶魔之镰");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.accessory = true;
            item.rare = 2;
            item.value = Item.sellPrice(0, 1);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[mod.BuffType("Berserked")] = true;
            
            if(player.statLife < player.statLifeMax2 / 2)
                player.GetModPlayer<FargoPlayer>().AllDamageUp(.10f);

            player.GetModPlayer<FargoPlayer>().AgitatingLens = true;
        }
    }
}
