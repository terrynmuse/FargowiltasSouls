using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace FargowiltasSouls.Items.Misc
{
	public class PlanterasFruit : ModItem
	{
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Plantera's Fruit");
            DisplayName.AddTranslation(GameCulture.Chinese, "世纪之花的果实");
		}

		public override void SetDefaults()
		{
            item.width = 20;
            item.height = 20;
            item.rare = 1;
            item.maxStack = 20;
            item.useStyle = 4;
            item.useAnimation = 45;
            item.useTime = 45;
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneJungle && !NPC.AnyNPCs(NPCID.Plantera);
        }

        public override bool UseItem(Player player)
        {
            if (player.itemAnimation > 0 && player.itemTime == 0)
            {
                Main.PlaySound(15, player.Center, 0);
                NPC.SpawnOnPlayer(player.whoAmI, NPCID.Plantera);
                if (NPC.downedPlantBoss)
                {
                    for (int i = 0; i < 30; i++)
                        Projectile.NewProjectile(player.position + new Vector2(Main.rand.Next(player.width), Main.rand.Next(player.height)),
                            new Vector2(Main.rand.Next(-30, 31), Main.rand.Next(-30, 31)), ProjectileID.ThornBall, 56, 0f, Main.myPlayer);
                }
            }
            return true;
        }
    }
}
