using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class BossCountMax : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boss Count Max");
            Tooltip.SetDefault(@"Maximizes all boss kill counts
Results not guaranteed in multiplayer
You probably shouldn't be reading this...");
            DisplayName.AddTranslation(GameCulture.Chinese, "Boss击杀数最大化");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"最大化所有Boss击杀数
无法保证在多人游戏中的效果
你也许不应该看到这个...");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 1;
            item.useStyle = 4;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                FargoSoulsWorld.SlimeCount = FargoSoulsWorld.MaxCountPreHM;
                FargoSoulsWorld.EyeCount = FargoSoulsWorld.MaxCountPreHM;
                FargoSoulsWorld.EaterCount = FargoSoulsWorld.MaxCountPreHM;
                FargoSoulsWorld.BrainCount = FargoSoulsWorld.MaxCountPreHM;
                FargoSoulsWorld.BeeCount = FargoSoulsWorld.MaxCountPreHM;
                FargoSoulsWorld.SkeletronCount = FargoSoulsWorld.MaxCountPreHM;
                FargoSoulsWorld.WallCount = FargoSoulsWorld.MaxCountPreHM;

                FargoSoulsWorld.TwinsCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.DestroyerCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.PrimeCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.PlanteraCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.GolemCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.FishronCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.CultistCount = FargoSoulsWorld.MaxCountHM;
                FargoSoulsWorld.MoonlordCount = FargoSoulsWorld.MaxCountHM;
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            }
            return true;
        }
    }
}
