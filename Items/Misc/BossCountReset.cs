using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

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
                FargoWorld.SlimeCount = 0;
                FargoWorld.EyeCount = 0;
                FargoWorld.EaterCount = 0;
                FargoWorld.BrainCount = 0;
                FargoWorld.BeeCount = 0;
                FargoWorld.SkeletronCount = 0;
                FargoWorld.WallCount = 0;
                FargoWorld.TwinsCount = 0;
                FargoWorld.DestroyerCount = 0;
                FargoWorld.PrimeCount = 0;
                FargoWorld.PlanteraCount = 0;
                FargoWorld.GolemCount = 0;
                FargoWorld.FishronCount = 0;
                FargoWorld.CultistCount = 0;
                FargoWorld.MoonlordCount = 0;
                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            }
            return true;
        }
    }
}
