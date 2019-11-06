using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class BossCountReset : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Boss Count Reset");
            Tooltip.SetDefault(@"Resets all boss kill counts to zero
Results not guaranteed in multiplayer
You probably shouldn't be reading this...");
            DisplayName.AddTranslation(GameCulture.Chinese, "Boss击杀数重置");
            Tooltip.AddTranslation(GameCulture.Chinese, 
@"重置所有Boss击杀数为0
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
                FargoSoulsWorld.SlimeCount = 0;
                FargoSoulsWorld.EyeCount = 0;
                FargoSoulsWorld.EaterCount = 0;
                FargoSoulsWorld.BrainCount = 0;
                FargoSoulsWorld.BeeCount = 0;
                FargoSoulsWorld.SkeletronCount = 0;
                FargoSoulsWorld.WallCount = 0;
                FargoSoulsWorld.TwinsCount = 0;
                FargoSoulsWorld.DestroyerCount = 0;
                FargoSoulsWorld.PrimeCount = 0;
                FargoSoulsWorld.PlanteraCount = 0;
                FargoSoulsWorld.GolemCount = 0;
                FargoSoulsWorld.FishronCount = 0;
                FargoSoulsWorld.CultistCount = 0;
                FargoSoulsWorld.MoonlordCount = 0;
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            }
            return true;
        }
    }
}
