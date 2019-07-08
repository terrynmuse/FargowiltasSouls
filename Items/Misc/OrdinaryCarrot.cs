using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class OrdinaryCarrot : ModItem
	{
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ordinary Carrot");
            Tooltip.SetDefault(@"'Plucked from the face of a defeated foe'
Increases night vision
Minor improvements to all stats
1 minute duration");
            DisplayName.AddTranslation(GameCulture.Chinese, "普通的胡萝卜");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"'从被打败的敌人的脸上拔下来的'
提高夜视能力
小幅提升所有属性
1分钟持续时间
(失落军团掉落)");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.maxStack = 30;
            item.rare = 4;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.consumable = true;
            item.UseSound = SoundID.Item2;
            item.value = Item.sellPrice(0, 0, 10, 0);
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                player.AddBuff(BuffID.NightOwl, 3600);
                player.AddBuff(BuffID.WellFed, 3600);
            }
            return true;
        }
    }
}
