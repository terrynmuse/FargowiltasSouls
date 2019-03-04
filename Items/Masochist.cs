using Terraria;
using Terraria.ModLoader;

namespace FargowiltasSouls.Items
{
    public class Masochist : ModItem
    {
        public override string Texture => "FargowiltasSouls/Items/Placeholder";

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Mutant's Gift");
            Tooltip.SetDefault("'Use this to turn on/off Masochist Mode'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.maxStack = 999;
            item.rare = 1;
            item.useAnimation = 30;
            item.useTime = 30;
            item.useStyle = 4;
            item.consumable = false;
        }

        public override bool UseItem(Player player)
        {
            bool bossExists = false;
            for (int i = 0; i < 200; i++)
            {
                if (Main.npc[i].active && Main.npc[i].boss)
                {
                    bossExists = true;
                    break;
                }
            }

            if (!bossExists)
            {
                FargoWorld.MasochistMode = !FargoWorld.MasochistMode;
                Main.expertMode = true;

                Main.NewText(FargoWorld.MasochistMode
                    ? "Masochist Mode initiated!"
                    : "Masochist Mode deactivated!", 175, 75);

                Main.PlaySound(15, (int)player.position.X, (int)player.position.Y, 0);
            }
            return true;
        }
    }
}